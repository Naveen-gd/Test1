# ******************************************************************************
# @file          e52138.py
# @author        SeDa
# @copyright     Smart Mechatronics GmbH
# @version       V2.1
# @date          2020/05/05
# @brief         Contains Wrapper for the E52138 used with DiffUARTda box
# @changes       V2.1: This API now supports GUARDIC Firmware
# # ******************************************************************************
__version__ = 'V2.1'

import time
import struct
from abc import ABCMeta, abstractmethod
from DiffUARTAPI import DiffUARTAPIExecuteable, DiffUARTAPI


class E52138ComMaster():
    # use python2 base class define to be compatible to IronPython2.7
    __metaclass__ = ABCMeta

    CONTROLLER_TIMESLICE = 0.004

    COMMAND_IDS = {
        "COM_TYPES_AUTO_ADDRESSING_ID": 0x01,
        "COM_TYPES_ALLPWM_ID": 0x11,
        "COM_TYPES_CHANNELPWM_ID": 0x12,
        "COM_TYPES_SETCURRENTBOOST_ID": 0x13,
        "COM_TYPES_GETCURRENTBOOST_ID": 0x14,
        "COM_TYPES_GETCHANNELPWM_ID": 0x15,
        "COM_TYPES_SETLEDRGB_ID": 0x16,
        "COM_TYPES_SETCHANNELMATRIX_ID": 0x81,
        "COM_TYPES_SETLEDCURRENT_ID": 0x82,
        "COM_TYPES_ISMULTIPLEXINGACTIVE_ID": 0x83,
        "COM_TYPES_GETVDIF_ID": 0xA1,
        "COM_TYPES_GETVLED_ID": 0xA0,
        "COM_TYPES_GETILED_ID": 0xA2,
        "COM_TYPES_GETVAUX_ID": 0xA3,
        "COM_TYPES_GETGATESTATE_ID": 0xA4,
        "COM_TYPES_GETCOMPERATORBIST_ID": 0xA5,
        "COM_TYPES_GETOPENSTATUS_ID": 0xA6,
        "COM_TYPES_GETSHORTSTATUS_ID": 0xA7,
        "COM_TYPES_GETMEM_ID": 0xF1,
        "COM_TYPES_SETMEM_ID": 0xF0,
        "COM_TYPES_RESET_ID": 0xF2,
        "COM_TYPE_GETFWVERSION_ID": 0xF3,
        "COM_TYPE_GETRESETSTATUS_ID": 0xF4,
        "COM_TYPE_GETCOMSTATUS_ID": 0xF5,
        "COM_TYPE_GETFWVARIANT_ID": 0xF6,
        "COM_TYPE_GETHWVERSION_ID": 0xB0,
        "COM_TYPE_SET_VBBSWITCH_ID": 0xB1,
        "COM_TYPES_SET_TSTCOND_ID": 0xB2,
        "COM_TYPES_GETGUARDFLAGS_ID": 0xB3,
        "COM_TYPES_GETTSTCONDSTATUS_ID": 0xB4,
    }

    class Message:

        SUBSCRIBE_DELAY = 0.001

        def __init__(self, context, id, payload, device_address=None, diagnose=None):
            self._context = context
            self._id = id
            self._payload = payload
            self._device_address = device_address
            self._diagnose = diagnose
            self._send = None

        def send(self):
            self._context._send_publish(self._id, self._payload, self._device_address, self._diagnose)
            self._send = time.time()

        def recv(self, length):
            duration = time.time() - self._send
            if duration < self.SUBSCRIBE_DELAY:
                time.sleep(self.SUBSCRIBE_DELAY - duration)
            return self._context._subscribe(length, self._device_address, self._diagnose)

    def __init__(self, com=None, device_address=1, baud=1000000, diag=False, blocking=True, api="NoApi",
                 publish_retries=1, subscribe_retries=2, *args, **kwargs):

        self.device_address = device_address
        self.diag = diag
        self._current_boost_send = 0

        self.is_fpga = False
        if self.is_fpga:
            print("Executing Test on FPGA:")
        else:
            print("Executing Test on Develop Board:")

        if api != "NoApi":
            self._api = api
            self._api_selfinitiated = False
        else:
            if com != None:
                self._api = DiffUARTAPI(com, baud)
                self._api_selfinitiated = True
            else:
                # autodetect
                import serial.tools.list_ports
                com_ports = [a[0] for a in (list(serial.tools.list_ports.comports()))]
                for detected_port in com_ports:
                    try:
                        self._api = DiffUARTAPI(detected_port, baud)
                        self._api_selfinitiated = True
                    except:
                        print("Could not connect to port ", detected_port)

        self._publish_retries = publish_retries
        self._subscribe_retires = subscribe_retries

        self._blocking = blocking

        self._last_transfer = time.time()

    def _subscribe(self, rx_frame_length, device_address=None, diagnose=False):

        diagnose = diagnose or self.diag
        if device_address == None:
            device_address = self.device_address

        if self._blocking:
            duration = time.time() - self._last_transfer
            if duration < self.CONTROLLER_TIMESLICE:
                time.sleep(self.CONTROLLER_TIMESLICE - duration)
            self._last_transfer = time.time()

        try:
            ret = self._api.subscribe(device_address, rx_frame_length, diagnose, retries=self._subscribe_retires)
        except:
            if self._api == None:
                raise Exception('No API for transfer found')
            raise

        if diagnose:
            print(ret)

        return ret

    def _send_publish(self, frame_id, payload, device_address=None, diagnose=False):

        if device_address == None:
            device_address = self.device_address

        diagnose = diagnose or self.diag
        if diagnose:
            if isinstance(frame_id, list):
                print 'Send: ', device_address, '  ', '{0:02X}'.format(frame_id[0]), '  ', ' '.join(['{0:02X}'.format(ord(b)) for l in payload for b in l])
            else:
                print 'Send: ', device_address, '  ', '{0:02X}'.format(frame_id), '  ', ' '.join(['{0:02X}'.format(ord(b)) for b in payload])

        if self._blocking:
            duration = time.time() - self._last_transfer
            if duration < self.CONTROLLER_TIMESLICE:
                time.sleep(self.CONTROLLER_TIMESLICE - duration)
            self._last_transfer = time.time()

        try:
            self._api.publish(device_address, frame_id, payload, diagnose, retries=self._publish_retries)
        except:
            if self._api == None:
                raise Exception('No API for transfer found')
            raise

    def set_api(self, api):

        self._api = api

    def __enter__(self):

        return self

    def __exit__(self, type, value, traceback):

        self.close()

    def close(self):

        if self._api_selfinitiated:
            self._api.close()


class E52138DiffUART(E52138ComMaster):
    # Definition of Auxiliary Measurements
    AUX_CHANNEL = {
        "VREF_0_MEASUREMENT": 0,
        "VREF_1_MEASUREMENT": 1,
        "VREF_2_MEASUREMENT": 2,
        "VS_MEASUREMENT": 3,
        "ATBUS0_MEASUREMENT": 4,
        "ATBUS1_MEASUREMENT": 5,
        "VT_MEASUREMENT": 6,
        "NO_MEASUREMENT": 7
    }

    class ComStatus:
        def __init__(self, raw):
            self.sync_byte_error = (raw[0] >> 0) & 1
            self.sync_break_timeout = (raw[0] >> 1) & 1
            self.baud_out_of_range = (raw[0] >> 2) & 1

        def __str__(self):
            return str(self.__dict__)

    class GateStatus:
        def __init__(self, raw):
            self.toff_max_fail = (raw >> 0) & 1
            self.toff_min_fail = (raw >> 1) & 1
            self.ton_max_fail = (raw >> 2) & 1
            self.ton_min_fail = (raw >> 3) & 1
            self.timeout = (raw >> 4) & 1
            self.meas_fail = (raw >> 5) & 1
            self.meas_done = (raw >> 6) & 1

        def __str__(self):
            return str(self.__dict__)

    class ChannelMatrix:
        def __init__(self, led, red, green, blue):
            '''Set red, green, blue to -1 for not connected'''

            self.led = led
            self.red = red
            self.green = green
            self.blue = blue

        def raw(self):

            byte = [0] * 3
            byte[0] = self.led

            if self.red > -1 and self.red < 18:
                byte[0] += 1 << 3
                byte[1] += self.red

            if self.green > -1 and self.green < 18:
                byte[0] += 1 << 4
                byte[1] += ((self.green) & 0x7) << 5
                byte[2] += self.green >> 3

            if self.blue > -1 and self.blue < 18:
                byte[0] += 1 << 5
                byte[2] += self.blue << 2

            return bytes(byte)

        def __str__(self):
            return str(self.__dict__)

    class ComperatorBistStatus:
        def __init__(self, raw):
            self.vddd_uv = (raw[0] >> 0) & 1
            self.vddd_ov = (raw[0] >> 1) & 1
            self.vdda_uv = (raw[0] >> 2) & 1
            self.vdda_ov = (raw[0] >> 3) & 1
            self.vs_uv = (raw[0] >> 4) & 1
            self.vs_ov = (raw[0] >> 5) & 1
            self.iref_vbg1_err = (raw[0] >> 6) & 1
            self.iref_vbg2_err = (raw[0] >> 7) & 1
            self.iref_low = (raw[1] >> 0) & 1
            self.iref_high = (raw[1] >> 1) & 1
            self.ovt = (raw[1] >> 2) & 1
            self.vs_crit = (raw[1] >> 3) & 1

        def __str__(self):
            return str(self.__dict__)

    class ResetStatus:
        def __init__(self, raw):
            self.vcore_ok = (raw[0] >> 0) & 1
            self.sys_clk_fail = (raw[0] >> 1) & 1
            self.cpu_lockup = (raw[0] >> 2) & 1
            self.debug_reset = (raw[0] >> 3) & 1
            self.software = (raw[0] >> 4) & 1
            self.sram_error = (raw[0] >> 5) & 1
            self.imem_error = (raw[0] >> 6) & 1
            self.window_watchdog = (raw[0] >> 7) & 1

            self.watchdog = (raw[1] >> 0) & 1
            self.watchdog_zero = (raw[1] >> 1) & 1
            self.crc16_mismatch = (raw[1] >> 2) & 1
            self.trim_parity = (raw[1] >> 3) & 1

        def __str__(self):
            return str(self.__dict__)

    class DiagStatus:
        def __init__(self, raw):
            self.led0 = (raw[0] >> 0) & 1
            self.led1 = (raw[0] >> 1) & 1
            self.led2 = (raw[0] >> 2) & 1
            self.led3 = (raw[0] >> 3) & 1
            self.led4 = (raw[0] >> 4) & 1
            self.led5 = (raw[0] >> 5) & 1
            self.led6 = (raw[0] >> 6) & 1
            self.led7 = (raw[0] >> 7) & 1
            self.led8 = (raw[1] >> 0) & 1
            self.led9 = (raw[1] >> 1) & 1
            self.led10 = (raw[1] >> 2) & 1
            self.led11 = (raw[1] >> 3) & 1
            self.led12 = (raw[1] >> 4) & 1
            self.led13 = (raw[1] >> 5) & 1
            self.led14 = (raw[1] >> 6) & 1
            self.led15 = (raw[1] >> 7) & 1
            self.led16 = (raw[2] >> 0) & 1
            self.led17 = (raw[2] >> 1) & 1

        def __str__(self):
            return str(self.__dict__)

        def to_list(self):
            ret = []
            for led in self.__dict__.values():
                ret += [led]

            return ret

    def send_pulse_all(self, pwm_val=0):

        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_ALLPWM_ID"], payload=bytes([pwm_val]))

    def send_pwm_channel(self, channel, pwm_values):

        if isinstance(pwm_values, list):

            assert len(pwm_values) <= 28
            FRAME_CAPACITY = 14

            ids = [self.COMMAND_IDS["COM_TYPES_CHANNELPWM_ID"]]

            payload = [bytes([channel] + pwm_values[:FRAME_CAPACITY])]

            if len(pwm_values) > FRAME_CAPACITY:
                ids += [self.COMMAND_IDS["COM_TYPES_CHANNELPWM_ID"]]
                payload += [bytes([channel + FRAME_CAPACITY]) + bytes(pwm_values[FRAME_CAPACITY: FRAME_CAPACITY * 2])]

            self._send_publish(ids, payload)

        elif isinstance(pwm_values, int):
            self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_CHANNELPWM_ID"],
                               payload=bytes([channel] + [pwm_values]))

        else:
            raise Exception('Unknown format of pwm_values')

    def _multichannel_get(self, command, channel, length, rx_factor=1):

        FRAME_CAPACITY = 14
        assert length <= FRAME_CAPACITY / rx_factor

        ids = self.COMMAND_IDS[command]
        channel_list = list(range(channel, channel + min(length, FRAME_CAPACITY / rx_factor)))
        payload = bytes([0x1D] + channel_list)

        msg = self.Message(self, ids, payload)
        msg.send()

        ret_val = msg.recv(len(channel_list) * rx_factor + 1)

        if rx_factor == 1:
            return list(struct.unpack('B' * len(channel_list), ret_val[1:]))
        elif rx_factor == 2:
            return list(struct.unpack('H' * len(channel_list), ret_val[1:]))

    def get_pwm_channel(self, channel, length=1):

        FRAME_CAPACITY = 14
        pwm_values = []

        if length > FRAME_CAPACITY:
            pwm_values = self._multichannel_get("COM_TYPES_GETCHANNELPWM_ID", channel, FRAME_CAPACITY, rx_factor=1)
            length -= FRAME_CAPACITY
            channel += FRAME_CAPACITY

        pwm_values += self._multichannel_get("COM_TYPES_GETCHANNELPWM_ID", channel, length, rx_factor=1)

        if (self.diag):
            print("PWM Values:", pwm_values)

        return pwm_values

    def send_reset_request(self):

        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_RESET_ID"], payload=b'\x2A')

    def _voltage_conversion(self, value):
        # V = 3 * (1 + ADCCTRL.PRESCALER.div) * (result_val / 4096) * 3.0V
        # This is taken from the FW, where the prescaler bit has to be set. Only tested on FPGA
        ADC_CTRL_PRESCALER = 1
        # The voltage-divider on the fpga baord is 1:12, whereas the divider on the ic is 1:6
        if self.is_fpga:
            scaler = 6.0
        else:
            scaler = 3.0
        return scaler * (1 + ADC_CTRL_PRESCALER) * (float(value) / 4096) * 3

    def get_vdif(self, channel=0, length=1, valid_flags=False):

        vdif_values = self._multichannel_get("COM_TYPES_GETVDIF_ID", channel, length, rx_factor=2)
        
        # FIXME: The valid flags are not transmitted by the current validation firmware
        vdif_valid_flags = [((1 << 13) & value) for value in vdif_values]
        vdif_valid_flags = [(value != 0) for value in vdif_valid_flags]

        vdif_values = [(0x0FFF & value) for value in vdif_values]
        vdif_values = [self._voltage_conversion(value) for value in vdif_values]

        if (self.diag):
            print("VDIF Values:", vdif_values)
            print("VDIF Valid Flags:", vdif_valid_flags)

        if valid_flags:
            return vdif_values, vdif_valid_flags
        else:
            return vdif_values

    def get_vled(self, channel=0, length=1, valid_flags=False):

        vled_values = self._multichannel_get("COM_TYPES_GETVLED_ID", channel, length, rx_factor=2)

        # FIXME: The valid flags are not transmitted by the current validation firmware
        vled_valid_flags = [((1 << 13) & value) for value in vled_values]
        vled_valid_flags = [(value != 0) for value in vled_valid_flags]

        vled_values = [(0x0FFF & value) for value in vled_values]
        vled_values = [self._voltage_conversion(value) for value in vled_values]

        if (self.diag):
            print("VLED Values:", vled_values)
            print("VLED Valid Flags:", vled_valid_flags)

        if valid_flags:
            return vled_values, vled_valid_flags
        else:
            return vled_values

        return vled_values

    def get_vaux(self, channel=0):

        if isinstance(channel, int):
            loc_channel = channel
        else:
            loc_channel = self.AUX_CHANNEL[channel]

        payload = b'\x41' + bytes([loc_channel])
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETVAUX_ID"], payload)
        msg.send()

        ret_val = msg.recv(3)
        vaux = struct.unpack('H', ret_val[1:])

        if self.diag:
            try:
                print("Measured VAUX-Channel{}: 0x{:04X}".format(channel, vaux))
            except:
                print("Measured VAUX-Channel{}: ".format(channel), vaux)

        return vaux

    def get_gate_state(self, channel=0):

        payload = b'\x1d' + bytes([channel])
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETGATESTATE_ID"], payload)
        msg.send()

        ret_val = msg.recv(2)

        if self.diag:
            print("Gate Status{}: {}".format(channel, ret_val))

        return self.GateStatus(ret_val[1])

    def get_fw_version(self):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPE_GETFWVERSION_ID"], b'\x1d')
        msg.send()

        ret_val = msg.recv(5)
        ret_val = struct.unpack('BBBB', ret_val[1:])
        fw_version = '{:02x}.{:02x}.{:02x}{:02x}'.format(*ret_val)

        if self.diag:
            print("FW-Version: ", fw_version)

        return fw_version

    def get_fw_variant(self):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPE_GETFWVARIANT_ID"], b'\x1d')
        msg.send()

        ret_val = msg.recv(16)
        fw_variant = ret_val[1:].decode().strip()
        if self.diag:
            print("FW-Version: ", fw_variant)

        return fw_variant

    def get_comperator_bist_results(self):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETCOMPERATORBIST_ID"], b'\x1d')
        msg.send()

        ret_val = msg.recv(3)

        status = self.ComperatorBistStatus(ret_val[1:])
        if self.diag:
            print("Comperator Bist:", str(status))

        return status

    def get_reset_status(self):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPE_GETRESETSTATUS_ID"], b'\x1d')
        msg.send()

        ret_val = msg.recv(3)

        if self.diag:
            print("Reset Status: {:04X}".format(ret_val[1], ret_val[2]))

        return self.ResetStatus(ret_val[1:])

    def get_mem(self, address, length):

        payload = struct.pack('<IB', address, length)

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETMEM_ID"], payload)
        msg.send()

        return ['0x' + hex(byte)[2:].upper() for byte in msg.recv(length)]

    def set_mem(self, address, value):

        payload = struct.pack('<IH', address, value)
        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_SETMEM_ID"], payload=payload)

    def set_current_boost(self, enable, channel=None):
        """
        @param channel: integer{0..17}
        @param enable: bool{True, False}, int{0..1}
        """
        if isinstance(enable, (bool, int)) and isinstance(channel, int):
            if enable == True:
                self._current_boost_send |= (1 << channel)
                enable = self._current_boost_send
            else:
                self._current_boost_send &= 0x3FFFF ^ (1 << channel)
                enable = self._current_boost_send

        elif isinstance(enable, list) or type(enable).__name__ == 'Array[bool]':
            self._current_boost_send = 0
            for num, bit in enumerate(enable):
                self._current_boost_send |= bit << num
                enable = self._current_boost_send
        else:
            raise Exception(enable.__class__.__name__ + " not supported")

        payload = bytes([enable & 0xFF, (enable >> 8) & 0xFF, (enable >> 16) & 0x3])
        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_SETCURRENTBOOST_ID"], payload=payload)

    def get_current_boost(self, channel=None):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETCURRENTBOOST_ID"], b'\x1d')
        msg.send()

        ret_val = msg.recv(4)

        status = [(ret_val[1] >> i) & 1 for i in range(8)]
        status += [(ret_val[2] >> i) & 1 for i in range(8)]
        status += [(ret_val[3] >> i) & 1 for i in range(2)]

        if self.diag:
            print("Current Boost Status:", status)

        if channel == None:
            return status
        else:
            return status[channel]

    def get_short_status(self):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETSHORTSTATUS_ID"], b'\x1d')
        msg.send()

        ret_val = msg.recv(4)
        return self.DiagStatus(ret_val[1:])

    def get_open_status(self):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETOPENSTATUS_ID"], b'\x1d')
        msg.send()

        ret_val = msg.recv(4)
        return self.DiagStatus(ret_val[1:])

    def get_com_status(self):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPE_GETCOMSTATUS_ID"], b'\x1d')
        msg.send()

        ret_val = msg.recv(2)
        return self.ComStatus(ret_val[1])

    def set_led_rgb(self, led, rgb):

        assert isinstance(rgb, (tuple, list)) and isinstance(rgb[0],
                                                             int), '"rgb" should be a list or tuple of integer values'

        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_SETLEDRGB_ID"], payload=bytes(rgb),
                           device_address=self.device_address + led)

    def set_led_current(self, led, current):

        assert isinstance(current, (tuple, list)) and isinstance(current[0],
                                                                 int), '"current" should be a list or tuple of integer values'

        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_SETLEDCURRENT_ID"], payload=bytes(current),
                           device_address=self.device_address + led)

    def set_channel_matrix(self, channel_matrix):

        if isinstance(channel_matrix, list):

            assert len(channel_matrix) <= 5, "channel_matrix must be a list with less than 6 elements"

            payload = b''.join([mat.raw() for mat in channel_matrix])
            self._send_publish(self.COMMAND_IDS["COM_TYPES_SETCHANNELMATRIX_ID"], payload)

        elif isinstance(channel_matrix, self.ChannelMatrix):

            self._send_publish(self.COMMAND_IDS["COM_TYPES_SETCHANNELMATRIX_ID"], channel_matrix.raw())

        else:
            raise Exception("Type not supported: ", type(channel_matrix))

    def is_multiplexing_active(self):

        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_ISMULTIPLEXINGACTIVE_ID"], b'\x1d')
        msg.send()
        ret_val = msg.recv(2)
        print(ret_val)
        return ret_val[1] == 1

    """
     +-----------------------------------------------------------------------------------------------------------+
     |       Guard IC Specific functions                                                                         |
     +-----------------------------------------------------------------------------------------------------------+
    """
    GUARD_AUX_CHANNELS = {
        "MEAS_SERVICE_VREF_0_MEASUREMENT": 0,
        "MEAS_SERVICE_VREF_1_MEASUREMENT": 1,
        "MEAS_SERVICE_VREF_2_MEASUREMENT": 2,
        "MEAS_SERVICE_VS_MEASUREMENT": 3,
        "MEAS_SERVICE_ATBUS0_MEASUREMENT": 4,
        "MEAS_SERVICE_ATBUS1_MEASUREMENT": 5,
        "MEAS_SERVICE_AA_MEASUREMENT": 6,
        "MEAS_SERVICE_MT_RESERVED0": 7,
        "MEAS_SERVICE_MT_RESERVED1": 8,
        "MEAS_SERVICE_MT_V5V": 9,
        "MEAS_SERVICE_MT_VBB": 10,
        "MEAS_SERVICE_MT_ULC12V": 11,
        "MEAS_SERVICE_MT_TST_SIG": 12,
        "MEAS_SERVICE_MT_V17V": 13,
        "MEAS_SERVICE_NO_MEASUREMENT": 14,
        "MEAS_SERVICE_VT_MEASUREMENT": 15,
    }

    class GuardHwRevision:
        def __init__(self, raw):
            self.revision = raw[0]
            self.patch = raw[1]
            self.r19_reading = raw[2]

        def __str__(self):
            return str(self.__dict__)

    class GuardStatusFlags:
        def __init__(self, raw):
            self.tst_condition_active_flag = (raw >> 0) & 1
            self.switch_vbb_vbbp_active_flag = (raw >> 1) & 1
            self.vbb_p_overvoltage_flag = (raw >> 2) & 1
            self.jtag_rel_strg_sc_flag = (raw >> 3) & 1
            self.jtag_rel_strg_ol_flag = (raw >> 4) & 1
            self.reserved = (raw >> 5)

        def __str__(self):
            return str(self.__dict__)

    GuardErrorCodes = {
        "GUARD_ERROR_NO_ERROR": 0,
        "GUARD_ERROR_TST_COND_BUSY": 1,
        "GUARD_ERROR_HWR_UNSUPPORTED": -1,
        "GUARD_ERROR_HWP_UNSUPPORTED": -2,
        "GUARD_ERROR_BLOCKED_BY_TST_COND": -3,
        "GUARD_ERROR_ABOVE_VS": -4,
        "GUARD_ERROR_NOT_HIGH_R_BEFORE_COND": -5,
        "GUARD_ERROR_NOT_HIGH_R_DURING_COND": -6,
        "GUARD_ERROR_DEACTIVATE_SWITCH_VBB_VBBP_FIRST": -7,
        "GUARD_ERROR_HW_INVALID": -8
    }

    def decode_guard_error(self, error_code):
        """! Decode an int-type error code as string

        @param error_code  Error code as int

        @return Error string to the GuardErrorCodes error_code
        """
        for error, code in self.GuardErrorCodes.items():
            if error_code == code:
                return error

        return str(error_code)

    def Activate_Switch_VBB_VBB_P(self):
        """! CMD-Line tool activate the switch between VBB and VBB_P

        """
        return self.set_vbb_switch(1)

    def Deactivate_Switch_VBB_VBB_P(self):
        """! CMD-Line tool deactivate the switch between VBB and VBB_P

        """
        return self.set_vbb_switch(0)

    def Deactivate_TST_Condition(self):
        """! CMD-Line tool deactivate tst condition

        """
        return self.set_tst_cond_switch(0)

    def Activate_TST_Condition(self):
        """! CMD-Line tool activate tst condition

        """
        return_value = self.GuardErrorCodes["GUARD_ERROR_NO_ERROR"]
        if self.GuardStatusFlags(self.get_guard_flags()).vbb_p_overvoltage_flag:
            return_value = self.GuardErrorCodes["GUARD_ERROR_DEACTIVATE_SWITCH_VBB_VBBP_FIRST"]
        elif self.GuardStatusFlags(self.get_guard_flags()).switch_vbb_vbbp_active_flag:
            return_value = self.GuardErrorCodes["GUARD_ERROR_DEACTIVATE_SWITCH_VBB_VBBP_FIRST"]
        elif self.GuardStatusFlags(self.get_guard_flags()).tst_condition_active_flag:
            return_value = self.GuardErrorCodes["GUARD_ERROR_NO_ERROR"]
        else:
            ret = self.set_tst_cond_switch(1)
            if ret < 0:
                return_value = ret
            else:
                while self.get_tst_cond_status() == 1:
                    pass
                return_value = self.get_tst_cond_status()
        return return_value

    def set_tst_cond_switch(self, status):
        """! Setter for the TST Condition Switch

        @param status  Enable or disable the tst_condition (1/0)

        @detailed use 'get_tst_cond_status' to poll the status of the switch after sending a request.
                  For a single command use 'Activate_TST_Condition'

        @return response as type GuardErrorCodes, returns GUARD_ERROR_TST_COND_BUSY on success and errorcode on failure
        """
        payload = b'\x41' + bytes([status])
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_SET_TSTCOND_ID"], payload)
        msg.send()
        ret_val = msg.recv(2)
        return struct.unpack('b', ret_val[1:])[0]

    def get_tst_cond_status(self):
        """! Returns the current status of the TST_condition

        As the activation is split into a sequence of actions.

        @return returns a int GuardErrorCodes
        """
        payload = b'\x51'
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETTSTCONDSTATUS_ID"], payload)
        msg.send()
        ret_val = msg.recv(2)
        flags, = struct.unpack('b', ret_val[1:])
        return flags

    def set_vbb_switch(self, status):
        """! Send a vbb vbbp switch request to dut

        @param status  Enable or disable the vbb_vbbp_switch (1/0)

        @return response as type GuardErrorCodes, returns GUARD_ERROR_NO_ERROR on success and errorcode on failure
        """
        payload = b'\x41' + bytes([status])
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPE_SET_VBBSWITCH_ID"], payload)
        msg.send()
        ret_val = msg.recv(2)
        return struct.unpack('b', ret_val[1:])[0]

    def get_HW_Revision(self):
        version, patch, r19 = self.get_hw_version()
        if 0 < r19 < 5:
            if version == 1 and patch < 2:
                return version
            else:
                return self.GuardErrorCodes["GUARD_ERROR_HWR_UNSUPPORTED"]
        elif 42 < r19 < 52:
            if version == 1 and patch == 2:
                return version
            else:
                return self.GuardErrorCodes["GUARD_ERROR_HWR_UNSUPPORTED"]
        elif 57 < r19 < 67:
            if version == 2 and patch == 1:
                return version
            else:
                return self.GuardErrorCodes["GUARD_ERROR_HWR_UNSUPPORTED"]
        else:
            return self.GuardErrorCodes["GUARD_ERROR_HW_INVALID"]

    def get_HW_Patch(self):
        version, patch, r19 = self.get_hw_version()
        if 0 < r19 < 5:
            if version == 1 and patch < 2:
                return patch
            else:
                return self.GuardErrorCodes["GUARD_ERROR_HWR_UNSUPPORTED"]
        elif 42 < r19 < 52:
            if version == 1 and patch == 2:
                return patch
            else:
                return self.GuardErrorCodes["GUARD_ERROR_HWR_UNSUPPORTED"]
        elif 57 < r19 < 67:
            if version == 2 and patch == 1:
                return patch
            else:
                return self.GuardErrorCodes["GUARD_ERROR_HWR_UNSUPPORTED"]
        else:
            return self.GuardErrorCodes["GUARD_ERROR_HW_INVALID"]

    def get_hw_version(self):
        """! Read the Guard HW information

        @see: Use with get_HW_revision and get_HW_Patch

        @return returns revision, patch and the raw r19_measurement
        """
        payload = b'\x42'
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPE_GETHWVERSION_ID"], payload)
        msg.send()
        ret_val = msg.recv(5)
        revision, patch, r19_measurement = struct.unpack('BBH', ret_val[1:])
        return revision, patch, r19_measurement

    def show_guard_flags(self):
        return "".join(["{:>2}:{}\n".format(2 ** n, a) for n, a in enumerate(self.GuardStatusFlags(0).__dict__.keys())])

    def show_error_codes(self):
        return "".join(["{:>2}: {}\n".format(code, error) for error, code in self.GuardErrorCodes.items()])

    def get_guard_flags(self):
        """! Reads the Guard Status flags from DUT

        @return raw value of status flags, can be decoded with GuardStatusFlags
        """
        payload = b'\x41'
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETGUARDFLAGS_ID"], payload)
        msg.send()
        ret_val = msg.recv(3)
        flags, = struct.unpack('H', ret_val[1:])
        return flags

    def get_guard_vaux(self, channel):
        """! Read the voltage of a specified GuardAuxChannel

        @param channel  Use dict GUARD_AUX_CHANNELS to specify a channel

        @return Returns the voltage reading as float
        """
        if isinstance(channel, int):
            loc_channel = channel
        else:
            loc_channel = self.GUARD_AUX_CHANNELS[channel]
        payload = b'\x41' + bytes([loc_channel])
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_GETVAUX_ID"], payload)
        msg.send()

        ret_val = msg.recv(3)
        vaux, = struct.unpack('H', ret_val[1:])
        if self.diag:
            print(vaux)
        if loc_channel == 15:
            return vaux
        else:
            return self._voltage_conversion(vaux)


class E52138AutoAddressingMaster(E52138ComMaster):
    # Dict describes Sub-Commands which are used for the Automatic-Addressing
    AA_SUB_COMMAND_IDS = {
        "AA_SUB_START_ID": 0x00,
        "AA_SUB_MEASURE_ID": 0x01,
        "AA_SUB_ID_ID": 0x02,
        "AA_SUB_ARBITRATION_ID": 0x03,
        "AA_SUB_STOP_ID": 0x04,
    }

    def aa_sequence_start(self):
        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_AUTO_ADDRESSING_ID"],
                           payload=bytes([self.AA_SUB_COMMAND_IDS["AA_SUB_START_ID"]]),
                           device_address=0)

    def aa_sequence_measure(self, number_of_samples):
        data = bytes(number_of_samples)
        payload = bytes([self.AA_SUB_COMMAND_IDS["AA_SUB_MEASURE_ID"]])
        payload += data

        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_AUTO_ADDRESSING_ID"], payload=payload, device_address=0)

    def aa_sequence_id(self, id, address):
        payload = bytes([self.AA_SUB_COMMAND_IDS["AA_SUB_ID_ID"], id, address])
        msg = self.Message(self, self.COMMAND_IDS["COM_TYPES_AUTO_ADDRESSING_ID"], payload, device_address=0)
        msg.send()

        ret_val = msg.recv(3)

        print(ret_val)

    def aa_sequence_stop(self):
        self._send_publish(frame_id=self.COMMAND_IDS["COM_TYPES_AUTO_ADDRESSING_ID"],
                           payload=bytes([self.AA_SUB_COMMAND_IDS["AA_SUB_STOP_ID"]]),
                           device_address=0)

    def run_complete_aa_sequence(self, number_of_slaves=1, number_of_sample=8, id_number=0, start_address=1):
        """@todo needs implementation"""
        pass


if __name__ == '__main__':
    with E52138DiffUART(com="COM6", diag=True, blocking=False, device_address=0x01) as dut:
        pass
        