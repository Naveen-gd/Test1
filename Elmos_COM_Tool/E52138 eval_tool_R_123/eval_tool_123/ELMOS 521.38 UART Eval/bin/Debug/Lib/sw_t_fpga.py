# ******************************************************************************
# @file          sw_t_fpga.py
# @author        SeDa
# @copyright     Smart Mechatronics GmbH
# @version       V1.0
# @date          2020/06/26
# @brief         Software test functions, verifying all features created for the tapeout firmware
# ******************************************************************************
from e52138 import E52138DiffUART, E52138AutoAddressingMaster
from DiffUARTAPI import DiffUARTAPI
import time


def validate_aa_measurement(dut):
    print("Verifying Automatic Address Measurement !")

    time.sleep(1)
    dut.device_address = 0x0

    print("Sending AA start")
    dut.aa_sequence_start()
    print("Sending AA-Measure with for n measurements")
    dut.aa_sequence_measure(6)
    dut.aa_sequence_id(0, 0x21)
    dut.aa_sequence_measure(6)
    time.sleep(1)
    try:
        dut.aa_sequence_id(0, 0x22)
        time.sleep(0.1)
    except:
        print("No second device connected!")
    print("Sending AA stop")
    dut.aa_sequence_stop()

    dut.device_address = 0x21
    
    time.sleep(1)
    print("Reverting Address to 0x01")
    dut.device_address = 0x0
    dut.aa_sequence_start()
    print("Sending AA-Measure with for n measurements")
    dut.aa_sequence_measure(6)
    dut.aa_sequence_id(0, 0x1)
    dut.aa_sequence_measure(6)
    dut.aa_sequence_stop()
    dut.device_address = 1
    

def validate_automatic_baudrate(dut):
    print("Verifying Automatic Baud Adaption and !")

    dut.baud = 450000
    dut.send_pulse_all(0x55)
    dut.get_com_status()
    dut.send_pulse_all(0x05)
    print("Setting Baudrate to value within 12,5%: 440000Baud")
    dut.baud = 450000
    dut.send_pulse_all(0x55)
    dut.send_pulse_all(0x05)
    dut.get_short_status()
    status = dut.get_com_status()
    print(status)
    print("Expecting COM status to show no errors")
    if (status.baud_out_of_range==1 or status.sync_break_timeout==1 or status.sync_byte_error==1):
        print("Status not as expected! Should not fail")
        exit()
    print("Reducing Baudrate to value with error greater than 12,5%: 350000Baud")

    dut.baud = 440000
    print(dut.baud)
    dut.send_pulse_all(0x55)
    dut.baud = 420000
    print(dut.baud)
    dut.send_pulse_all(0x55)
    dut.baud = 400000
    print(dut.baud)
    dut.send_pulse_all(0x55)
    dut.baud = 420000
    print(dut.baud)
    dut.send_pulse_all(0x55)
    dut.baud = 440000
    print(dut.baud)
    dut.send_pulse_all(0x55)
    dut.baud = 460000
    print(dut.baud)
    dut.send_pulse_all(0x55)
    dut.baud = 480000
    print(dut.baud)
    dut.send_pulse_all(0x55)

    status = dut.get_com_status()
    print(status)
    print("Expecting COM status to show baud_out_of_range")
    print("On second read, status should read no errors anymore")
    status = dut.get_com_status()
    if (status.baud_out_of_range == 1 or status.sync_break_timeout == 1 or status.sync_byte_error == 1):
        print("Status not as expected! Should not fail")
        exit()
    print("Done!")

def validate_short_status(dut):
    print("Validating Short Status ")
    print("On Fpga, all channels show short status, if enabled")

    dut.send_pulse_all(44)
    input("Connect all Jumpers to LEDs (All LEDs illuminated) (y/n)?")
    print(dut.get_short_status())
    input("Disconnect all Jumpers to LEDs (All LEDs illuminated) (y/n)?")
    print(dut.get_short_status())
    print("Expected: All LEDs on FPGA show short error before jumpers are disconnected,"
          " and no short error after disconnecting jumpers")

def validate_open_status(dut):
    print("Validating Open Status ")

    dut.send_pulse_all(44)
    input("Connect all Jumpers to LEDs (All LEDs illuminated) (y/n)?")
    print(dut.get_open_status())
    input("Disconnect all Jumpers to LEDs (All LEDs illuminated) (y/n)?")
    print(dut.get_open_status())
    print("Expected: All LEDs on FPGA show open error before jumpers are disconnected,"
          " and no open error after disconnecting jumpers")

def validate_current_boost(dut):
    print("Validating Current Boost ")

    dut.send_pulse_all(44)
    dut.set_current_boost([0]*18)
    
    for i in range(0, 18):
        print("Verifying current boost of channel {}... ".format(i))
        dut.set_current_boost(channel=i, enable=1)
        time.sleep(0.2)
        if False == dut.get_current_boost(channel=i):
            print("Error, Enabled current boost for {} , "
                  "however readout of current boost status failed to read true after enabling".format(i))
        #print("current boost of channel {}: ".format(i) + str(dut.get_current_boost(channel=i)))
        #print("Deactivating current boost of channel {}... ".format(i))
        dut.set_current_boost(channel=i, enable=0)
        time.sleep(0.2)
        if True == dut.get_current_boost(channel=i):
            print("Error, expected to read False!, readout of channel{} failed after disabling".format(i))
        #print("Read current boost of channel {}: ".format(i) + str(dut.get_current_boost(channel=i)))

def validate_reset_status(dut, aa_master):
    print("Validating Reset Status Request")
    
    time.sleep(0.4)
    dut.send_reset_request()
    time.sleep(0.1)
    
    # TODO autoaddressing must be executed after reset to assign sub addresses to device
    # should be saved persistent in future versions
    validate_aa_measurement(aa_master)
    
    print(dut.get_reset_status())
    print(dut.get_reset_status())

def validate_sw_reset(dut, aa_master):
    print("Validating SW Reset")

    dut.send_pulse_all(44)
    first_meas = dut.get_vdif(8)
    dut.send_reset_request()
    time.sleep(0.5)
    
    # TODO autoaddressing must be executed after reset to assign sub addresses to device
    # should be saved persistent in future versions
    validate_aa_measurement(aa_master)
    
    second_meas = dut.get_vdif(8)

    if (first_meas == second_meas):
        print("Unsuccessful")
    else:
        print("Success, pulse values cleared after reset!")
def validate_vled(dut):
    print("Validating VLED")

    dut.send_pulse_all(100)
    time.sleep(1)
    for i in range(0, 18):
        x = dut.get_vled(channel=i)[0]
        print("Measured Voltage VLED{}: {}V".format(i, x))
        
    time.sleep(0.1)
    print("Get multiple channels")
    x = dut.get_vled(0, 6)
    print("Multiple channel VLED return:", x)
        
    print("VDIF Test passed")

def validate_vdif(dut):
    print("Validating VDIFF")

    dut.send_pulse_all(100)
    time.sleep(1)
    for i in range(0, 18):
        x = dut.get_vdif(channel=i)[0]
        print("Measured Voltage VDIF{}: {}V".format(i,x))
    print("VDIF Test passed")

def validate_vaux(dut):
    print("Validating VAUX")

    dut.send_pulse_all(255)
    for measurement in dut.AUX_CHANNEL:
        print(measurement)
        x = dut.get_vaux(measurement)[0]
        print("{}: Voltage: {} V".format(measurement, x * 0.005))

def validate_channel_matrix(dut):
    print("Validating Channel Matrix")

    dut.send_pulse_all(0)
    
    dut.set_channel_matrix([dut.ChannelMatrix(0, 0, 1, 2), dut.ChannelMatrix(1, 3, 4, 5)])
    dut.set_channel_matrix(dut.ChannelMatrix(2, 6, 7, 8))
    dut.set_channel_matrix(dut.ChannelMatrix(3, 9, 10, 11))
    
    dut.set_led_rgb(1, (0, 0, 0xFF))
    time.sleep(0.1)
    assert dut.get_pwm_channel(0, 8) == [0, 0, 0, 0, 0, 0xFF, 0, 0], 'The blue channel of the second LED shall be active'
    
    dut.set_led_rgb(0, (0xFF, 0, 0xEF))
    time.sleep(0.1)
    assert dut.get_pwm_channel(0, 8) == [0xFF, 0, 0xEF, 0, 0, 0xFF, 0, 0], 'The red and blue channel of the first LED shall be active'
    
    dut.set_led_rgb(2, (0, 0xF0, 0))
    time.sleep(0.1)
    assert dut.get_pwm_channel(0, 9) == [0xFF, 0, 0xEF, 0, 0, 0xFF, 0, 0xF0, 0], 'The red and blue channel of the first LED shall be active'
    
    dut.set_led_rgb(3, (0xFF, 0xFF, 0xFF))
    time.sleep(0.1)
    assert dut.get_pwm_channel(9, 3) == [0xFF, 0xFF, 0xFF], 'All channels of the fourth LED shall be active'
    
def validate_current_setting(dut):
    print("Validating current settings")
    
    dut.send_pulse_all(0xFF//4*3)
    
    dut.set_channel_matrix([dut.ChannelMatrix(0, 0, 1, 2), dut.ChannelMatrix(1, 3, 4, 5)])
    dut.set_channel_matrix(dut.ChannelMatrix(2, 6, 7, 8))
    dut.set_channel_matrix(dut.ChannelMatrix(3, 9, 10, 11))
    
    dut.set_led_current(0, ( 0,  0, 15))
    time.sleep(0.03)
    input()
    dut.set_led_current(1, (15,  0,  0))
    time.sleep(0.03)
    dut.set_led_current(2, ( 0, 15,  0))
    time.sleep(0.03)
    dut.set_led_current(3, (15, 15, 15))
    time.sleep(0.03)
    
    if input('Check if LED 0 has a higher proportion of blue (y/n)') != 'y':
        raise Exception('Current setting not successfull')
        
    
    dut.set_led_current(0, (3, 3, 3))
    
    
def validate_fw_version(dut):

    print("Read out Firmware Version: "+dut.get_fw_version())

def validate_gate_state(dut):
    print("Validating GateState")

    dut.send_pulse_all(22)
    for i in range(0, 18):
        print("GateState of Channel {}: ".format(i)+ str(dut.get_gate_state(channel=i)))
    dut.send_pulse_all(0)
    for i in range(0, 18):
        print("GateState of Channel {}: ".format(i)+ str(dut.get_gate_state(channel=i)))

def validate_selftest_comperator(dut):
    print("Validating Analog Comperator BIST at startup")

    print(dut.get_comperator_bist_results())

def validate_off_behavior(dut):
    dut.send_pulse_all(0)
    assert "n" != input("Are all leds turned off?"), "Expecting all LEDs to be off"

def verify_switchbuffer_hardfault(dut):
    for u in range(256):
        dut.send_pulse_all(u)
        time.sleep(0.01)
    for u in range(256):
        dut.send_pulse_all(255-u)
        time.sleep(0.01)

def complete_testrun(port):
    print("Verifying all test created....\r\n\r\n")
    with E52138AutoAddressingMaster(com = port, publish_retries = 10, subscribe_retries = 20) as aa_master:
        #validate_aa_measurement(aa_master) #Done!
    
        with E52138DiffUART(com = port, publish_retries = 100, subscribe_retries = 100, api = aa_master._api) as dut:
            validate_fw_version(dut)
            validate_reset_status(dut, aa_master) #Done!
            
            validate_vdif(dut)     #Done!
            validate_vled(dut)     #Done!
            validate_off_behavior(dut)
            verify_switchbuffer_hardfault(dut)
            #TODO needs rework
            # validate_automatic_baudrate(dut)
            validate_short_status(dut)  #Done!
            validate_open_status(dut)  #Done!
            validate_gate_state(dut)  #Done!
            validate_sw_reset(dut, aa_master) #Done!
            validate_vaux(dut) #Done!
            validate_channel_matrix(dut)
            validate_current_setting(dut)
            validate_selftest_comperator(dut) #Done!
            validate_reset_status(dut, aa_master) #Done!
            validate_current_boost(dut)
    print("\r\n Sucess, all test executed")

if __name__ == "__main__":
    complete_testrun('COM39')
