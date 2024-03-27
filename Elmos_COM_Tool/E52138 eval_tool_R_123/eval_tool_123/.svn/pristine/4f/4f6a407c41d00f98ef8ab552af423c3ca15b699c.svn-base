# coding: utf-8

from abc import ABCMeta, abstractmethod
import re
import ctypes
import time

class TransmissionException(Exception):
    pass

class _DiffUARTAPIBase:
    # use python2 base class define to be compatible to IronPython2.7
    __metaclass__ = ABCMeta
    
    @abstractmethod
    def __init__(self, com_port, baudrate):
        pass
        
    @abstractmethod
    def subscribe(self, address, rx_frame_length):
        pass
        
    @abstractmethod
    def publish(self, address, frame_id, magic):
        pass
    
    @abstractmethod
    def close(self):
        pass
    

class DiffUARTAPIExecuteable(_DiffUARTAPIBase):
    
    def __init__(self, com_port, baudrate):
        
        self._com = com_port
        self._baudrate = baudrate
        
        import subprocess
        self._subprocess = subprocess
        
    def subscribe(self, address, rx_frame_length, diagnose = False):
        
        out = self._subprocess.Popen(['./DiffUARTAPI/DiffUART-TPMsg.exe', 'DiffUARTda:Port=\\.\{}'.format(self._com),
                                '0x{:02x}'.format(address), '{}'.format(int(diagnose)), 'set_baud',
                                str(self._baudrate), 'subscribe', '0', '1', '0', '0x{:02X}'.format(rx_frame_length)],
                                stdout=self._subprocess.PIPE,
                                stderr=self._subprocess.STDOUT)
        stdout, stderr = out.communicate()
        
        if diagnose:
            print(stdout.decode())
        
        raw = re.search(r'0x[0-9]{4}:( 0x[0-9A-Fa-f]{2}){' + re.escape(str(rx_frame_length)) + r'}',
                        stdout.decode()).group(0)
        raw_bytes = raw.split()
        
        return raw_bytes

    def publish(self, address, frame_id, payload, diagnose = False):
    
        out = self._subprocess.Popen(['./DiffUARTAPI/DiffUART-TPMsg.exe', 'DiffUARTda:Port=\\.\{com}'.format(com=self._com),
                                '0x{:02x}'.format(address), '{}'.format(int(diagnose)), 'set_baud',
                                str(self._baudrate), 'publish', "0","1", "0", "{}".format(frame_id)] + payload,
                               stdout=self._subprocess.PIPE,
                               stderr=self._subprocess.STDOUT)
        stdout,stderr = out.communicate()
        
        if diagnose:
            print(stdout.decode())
            
            
    #def _check_subscribe(self, baud=500000, id=1, rx_frame_length=3, check_data="0x0a 0x01 0x03"):
    #    os.system('"DiffUART-TPMsg.exe DiffUARTda:Port=\\.\COM7'
    #              ' {} 1 set_baud {} check_subscribe 0 1 0 0x{:02X} {} "'.format(id,
    #                                                                             baud, rx_frame_length, check_data))
    
    def close(self):
        '''Not necessary to end subprocess'''
        pass
        

class DiffUARTAPI(_DiffUARTAPIBase):

    LIB_NAME = 'DiffUARTLibDLL.dll'
    INTERFRAME_DELAY = 8000
    
    DUTSS_SUCCESS = 0x03
    
    def __init__(self, com_port, baudrate):
        
        # DLL is compiled for win32
        import sys
        assert sys.platform == 'win32' or sys.platform == 'cli', 'Not a 32-bit windows platform'
        
        self._dll = self._setuplibrary()
        
        print(self._dll.DiffUARTLibrary_GetVersionString().decode(), '\n')
        self._dll.DiffUARTLibrary_Setup()
        
        self._master = self._dll.DiffUARTLibrary_GetMaster();
        self._transport =  self._dll.DiffUARTMaster_CreateTransportByURL(self._master, 'DiffUARTda:Port=\\\\.\\{0}'.format(com_port).encode());
        
        if self._transport == 0:
            raise Exception('Could not establish transport')
        
        if not self._dll.CDiffUARTTransport_OpenTransport(self._transport):
            raise Exception('Could not open transport')
        
        print('Transport open.')
        self._transport_opened = True
        
        self._dll.CDiffUARTTransport_SetTransportInitialBaudrate(self._transport, baudrate);

        # Set delay between two frames to 1000 us
        self._dll.CDiffUARTTransport_SetTransportInitialDataStepDelay(self._transport, self.INTERFRAME_DELAY);
        
        self._mutex = False
        
    def _setuplibrary(self):
    
        import ctypes, os
        if os.path.dirname(__file__):
            dll = ctypes.CDLL(os.path.dirname(__file__) + '/' + self.LIB_NAME)
        else:
            dll = ctypes.CDLL(self.LIB_NAME)
        
        dll.DiffUARTLibrary_GetVersionString.restype = ctypes.c_char_p
        
        dll.DiffUARTMaster_CreateTransportByURL.argtypes = [ctypes.c_void_p, ctypes.c_char_p]
        dll.DiffUARTMaster_CreateTransportByURL.restype = ctypes.c_void_p
        dll.CDiffUARTTransport_OpenTransport.restype = ctypes.c_bool
        
        dll.CDiffUARTTransport_SetTransportInitialBaudrate.argtypes = [ctypes.c_void_p, ctypes.c_int]
        dll.CDiffUARTTransport_SetTransportInitialDataStepDelay.argtypes = [ctypes.c_void_p, ctypes.c_int]
        
        dll.CDiffUARTTransport_CreateTransportSequence.argtypes = [ctypes.c_void_p, ctypes.c_int, ctypes.c_bool]
        dll.CDiffUARTTransportSequence_AddLabel.argtypes = [ctypes.c_void_p, ctypes.c_char_p]
        dll.CDiffUARTTransportSequence_SetLabel.argtypes = [ctypes.c_void_p, ctypes.c_void_p]
        dll.CDiffUARTTransportSequence_AppendStepUART3ByteHeaderWrite.argtypes = [ctypes.c_void_p, ctypes.c_int, ctypes.c_int, ctypes.c_int, ctypes.c_char_p]
        dll.CDiffUARTTransportSequence_GetInUseFlag.restype = ctypes.c_bool
        
        dll.CDiffUARTTransportSequence_GetStatus.argtypes = [ctypes.c_void_p, ctypes.POINTER(ctypes.c_ulonglong), ctypes.POINTER(ctypes.c_int)]
        dll.CDiffUARTTransportSequence_GetStatus.restype = ctypes.c_int
        dll.CDiffUARTTransportSequence_CreateStatusDescription.argtypes = [ctypes.c_void_p, ctypes.c_int]
        dll.CDiffUARTTransportSequence_CreateStatusDescription.restype = ctypes.c_char_p
        
        dll.CDiffUARTTransportSequenceStep_UARTMsg_GetRxUserDataLen.restype = ctypes.c_uint
        dll.CDiffUARTTransportSequenceStep_UARTMsg_GetRxUserDataFill.restype = ctypes.c_uint
        dll.CDiffUARTTransportSequenceStep_UARTMsg_GetRxUserData.restype = ctypes.POINTER(ctypes.c_char)
        
        return dll
        
    def _build_payload(self, frame_id, payload):
    
        ret = bytes([frame_id])
        
        for byte in payload:
            ret += bytes([int(byte[-2:], 16)])
            
        return ret
        
    def _acquire_semaphore(self):
    
        if self._mutex:
            for i in range(50):
                time.sleep(0.0001)
                if not self._mutex:
                    break
                
        self._mutex = True
            
    def _release_semaphore(self):
    
        self._mutex = False
        
    def subscribe(self, address, rx_frame_length, diagnose = False, retries = 2):
        
        ret = b''
        
        transportSequence_Ptr = self._dll.CDiffUARTTransport_CreateTransportSequence(self._transport, 1, False)
        
        label_3ByteHeaderReadFrame_Ptr = self._dll.CDiffUARTTransportSequence_AddLabel(transportSequence_Ptr, b'3ByteHeaderReadFrame')
        self._dll.CDiffUARTTransportSequence_SetLabel(transportSequence_Ptr, label_3ByteHeaderReadFrame_Ptr);
        #! Append a 3 byte header read fame to the tansport sequence
        #                                                                                          device_adress    channel_number   user_data_words_len  
        self._dll.CDiffUARTTransportSequence_AppendStepUART3ByteHeaderRead(transportSequence_Ptr,  address,         0x02,            rx_frame_length)

        self._acquire_semaphore()
        
        for retry in range(retries):
        
            self_mutex = True
            
            self._dll.CDiffUARTTransport_SubmitTransportSequence(self._transport, transportSequence_Ptr)
        
            while(self._dll.CDiffUARTTransportSequence_GetInUseFlag(transportSequence_Ptr)):
                time.sleep(0.0005)

            sequence_Status = self._dll.CDiffUARTTransportSequence_GetStatus(transportSequence_Ptr, ctypes.byref(ctypes.c_ulonglong(0)), ctypes.byref(ctypes.c_int(0)))
            
            if diagnose:
                status_Description = self._dll.CDiffUARTTransportSequence_CreateStatusDescription(transportSequence_Ptr, sequence_Status)
                print(status_Description.decode())
                
            if sequence_Status == self.DUTSS_SUCCESS:
                break
        
        self._release_semaphore()
        
        if sequence_Status == self.DUTSS_SUCCESS: #DUTSS_SUCCESS = 0x03
        
            step_3ByteHeaderReadFame_Ptr = self._dll.CDiffUARTTransportSequence_GetStepByLabel(transportSequence_Ptr, label_3ByteHeaderReadFrame_Ptr);
            self._dll.CDiffUARTTransportSequenceStep_Lock(step_3ByteHeaderReadFame_Ptr);

            rxUserDataLen_3ByteHeaderReadFame = self._dll.CDiffUARTTransportSequenceStep_UARTMsg_GetRxUserDataLen(step_3ByteHeaderReadFame_Ptr);
            rxUserDataFill_3ByteHeaderReadFame = self._dll.CDiffUARTTransportSequenceStep_UARTMsg_GetRxUserDataFill(step_3ByteHeaderReadFame_Ptr);
            rxUserData_3ByteHeaderReadFame = self._dll.CDiffUARTTransportSequenceStep_UARTMsg_GetRxUserData(step_3ByteHeaderReadFame_Ptr);

            self._dll.CDiffUARTTransportSequenceStep_Release(step_3ByteHeaderReadFame_Ptr);
            
            ret = ctypes.cast(rxUserData_3ByteHeaderReadFame, ctypes.POINTER(ctypes.c_ubyte * rx_frame_length)).contents
            ret = bytearray(ret)
            
        else:
            self._dll.CDiffUARTTransport_ReleaseTransportSequence(self._transport, transportSequence_Ptr);
            status_Description = self._dll.CDiffUARTTransportSequence_CreateStatusDescription(transportSequence_Ptr, sequence_Status)
            raise TransmissionException("Transmission failed: " + status_Description.decode())
        
        self._dll.CDiffUARTTransport_ReleaseTransportSequence(self._transport, transportSequence_Ptr);
        
        return ret
        
    def publish(self, address, frame_id, payload, diagnose = False, retries = 1):
        
        transportSequence_Ptr = self._dll.CDiffUARTTransport_CreateTransportSequence(self._transport, 1, False)
        
        label_3ByteHeaderWriteFrame_Ptr = self._dll.CDiffUARTTransportSequence_AddLabel(transportSequence_Ptr, b'3ByteHeaderWriteFrame')
        self._dll.CDiffUARTTransportSequence_SetLabel(transportSequence_Ptr, label_3ByteHeaderWriteFrame_Ptr)
        
        if not isinstance(frame_id, list):
            frame_id = [frame_id] 
            payload = [payload]
        
        for frame_id_, payload_ in zip(frame_id, payload):
            
            payload_ = bytes([frame_id_]) + payload_
            #! Append a 3 byte header write fame to the tansport sequence
            #                                                                                          device_adress    channel_number   user_data_words_len    data
            self._dll.CDiffUARTTransportSequence_AppendStepUART3ByteHeaderWrite(transportSequence_Ptr, address,         0x02,            len(payload_),          payload_)
        
        self._acquire_semaphore()
        
        for retry in range(retries):
            self._dll.CDiffUARTTransport_SubmitTransportSequence(self._transport, transportSequence_Ptr)
            
            while(self._dll.CDiffUARTTransportSequence_GetInUseFlag(transportSequence_Ptr)):
                time.sleep(0.0005)
                
            sequence_Status = self._dll.CDiffUARTTransportSequence_GetStatus(transportSequence_Ptr, ctypes.byref(ctypes.c_ulonglong(0)), ctypes.byref(ctypes.c_int(0)))
            
            if diagnose:
                status_Description = self._dll.CDiffUARTTransportSequence_CreateStatusDescription(transportSequence_Ptr, sequence_Status)
                print(status_Description.decode())
                
            if sequence_Status == self.DUTSS_SUCCESS:
                break
    
        self._release_semaphore()
    
        #TODO this kills python
        #self._dll.CDiffUARTTransportSequence_ReleaseStatusDescription(transportSequence_Ptr, status_Description)
    
        self._dll.CDiffUARTTransport_ReleaseTransportSequence(self._transport, transportSequence_Ptr);
    
        if sequence_Status != self.DUTSS_SUCCESS:
            status_Description = self._dll.CDiffUARTTransportSequence_CreateStatusDescription(transportSequence_Ptr, sequence_Status)
            raise TransmissionException("Transmission failed: " + status_Description.decode())
        
    def close(self):
        
        if self._transport_opened:
            self._dll.CDiffUARTTransport_CloseTransport(self._transport);
            
        self._dll.DiffUARTMaster_ReleaseTransport(self._master , self._transport);
        self._dll.DiffUARTLibrary_Cleanup();
        
    def __enter__(self):
        return self
        
    def __exit__(self, type, value, traceback):
        self.close()
        
if __name__ == '__main__':
    
    with DiffUARTAPI('COM9', 50000) as api:
        
        pass