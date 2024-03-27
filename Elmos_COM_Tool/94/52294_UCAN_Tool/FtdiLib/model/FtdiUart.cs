using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using FTD2XX_NET;

namespace FtdiLib
{
    public class FtdiUart : FtdiBase
    {
        protected ushort TIMEOUT_ms = 10;

        #region "Callbacks"
        public delegate void SendBreakCallback();
        public delegate void ReceiveDataCallback(byte[] data, uint numBytesRead, bool timeout);

        protected SendBreakCallback _sendBreakCallback;
        protected ReceiveDataCallback _receiveDataCallback;

        public SendBreakCallback sendBreakCallback
        {
            set { _sendBreakCallback = value; }
        }

        public ReceiveDataCallback receiveDataCallback
        {
            set { _receiveDataCallback = value; }
        }
        #endregion

        #region "Config"
        internal uint _parity = 0;
        internal uint _bitrate = 500000;
        internal double _breakLength = 13.5;
        internal uint _wakeupLength = 500;

        public double GetBreakLength()
        {
            return _breakLength;
        }

        public void SetBreakLength(double breakLength)
        {
            _breakLength = breakLength;
        }

        public uint GetWakeupLength()
        {
            return _wakeupLength;
        }

        public void SetWakeupLength(uint wakeupLength)
        {
            _wakeupLength = wakeupLength;
        }

        public uint GetBitrate()
        {
            return _bitrate;
        }

        public void SetBitrate(uint bitrate)
        {
            _lastBitrate = 0;
            _bitrate = bitrate;
        }

        public uint GetParity()
        {
            return _parity;
        }

        public void SetParity(uint parity)
        {
            _parity = parity;
            _UpdateParity();
        }
        #endregion

        private uint _lastBitrate = 0;
        private bool _breakAdd1;

        private void _UpdateParity(){
            /*
             * Remark ok _breakAdd1:
             * defines if there is an additional 9th bit when sending the Break 0 symbol
             * EVEN: even number of ones -> Parity bit = 0 (+1)
             * ODD:  even number of ones -> Parity bit = 1
             * ZERO:                        Parity bit = 0 (+1)
             * NONE:                     no Parity bit
             */

            switch (_parity)
            {
                case 0: _ftdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_EVEN); _breakAdd1 = true; break;
                case 1: _ftdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_ODD); _breakAdd1 = false; break;
                case 2: _ftdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_SPACE); _breakAdd1 = true; break;    // ZERO
                case 3: _ftdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_NONE); _breakAdd1 = false; break;
            }

        }

        private bool _ChangeBitrate(uint newBitrate){
            if (_lastBitrate != newBitrate){
                FTDI.FT_STATUS ftStatus = _ftdiDevice.SetBaudRate(newBitrate);
                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    return false;
                }
            }
            _lastBitrate = newBitrate;
            return true;
        }

        override protected bool _InitialConfigAfterOpen()
        {
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            // Set Mode
            ftStatus = _ftdiDevice.SetBitMode(0xFF, FTDI.FT_BIT_MODES.FT_BIT_MODE_RESET);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }

            // Set flow control - set RTS/CTS flow control
            ftStatus = _ftdiDevice.SetFlowControl(FTDI.FT_FLOW_CONTROL.FT_FLOW_NONE, 0x11, 0x13);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }

            ftStatus = _ftdiDevice.SetTimeouts(1, 1);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }

            ftStatus = _ftdiDevice.SetLatency(1);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }

            // dafault config
            SetParity(0);
            SetBitrate(500000);

            return true;
        }

        private bool _SetOutputImmediately(bool high)
        {
            FTDI.FT_STATUS ftStatus;

            ftStatus = _ftdiDevice.SetBreak(!high);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }

            return true;
        }

        public bool SetBreak()
        {
            return _SetOutputImmediately(false);
        }

        public bool ClearBreak()
        {
            return _SetOutputImmediately(true);
        }

        public FtdiUart()
            : base()
        {
        }

        public bool SendWakeup()
        {
            // calc constant time to bits
            double bit_length = 1.0 / _bitrate;

            double wake_bits = (double) _wakeupLength / 1000000 / bit_length;

            return SendBreak(wake_bits);
        }

        public bool SendBreak(double breakLength = 0.0)
        {
            // HW zero symbol
            double breakBitrateFactor = 9.0;    // start + outData
            if (_breakAdd1)
                breakBitrateFactor += 1.0;      // 0 parity bit

            // send break length
            if (breakLength < 1.0)
                breakLength = _breakLength;

            // ratio required vs. HW zero symbol
            breakBitrateFactor /= breakLength;

            uint break_bitrate = (uint)(breakBitrateFactor * Convert.ToDouble(_bitrate));
            if (!_ChangeBitrate(break_bitrate)) 
                return false;

            uint numBytesWritten = 0;
            byte[] breakSymbol = new byte[1];
            breakSymbol[0] = 0;
            FTDI.FT_STATUS ftStatus = _ftdiDevice.Write(breakSymbol, breakSymbol.Length, ref numBytesWritten);

            if (_sendBreakCallback != null) _sendBreakCallback();
            
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
                return false;
            if (numBytesWritten != breakSymbol.Length)
                return false;

            return true;
        }

        public bool SendByte(byte val)
        {
            if (!_ChangeBitrate(_bitrate))
                return false;

            byte[] data = new byte[1];
            data[0] = val;

            uint numBytesWritten = 0;
            _ftdiDevice.Write(data, data.Length, ref numBytesWritten);

            // Wait until own outData has been sent or Timeout
            int timeout = TIMEOUT_ms;
            uint numBytesAvailable = 0;
            do
            {
                FTDI.FT_STATUS ftStatus = _ftdiDevice.GetRxBytesAvailable(ref numBytesAvailable);
                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    return false;
                }
                if (timeout == 0)
                    return false;
                if (numBytesAvailable < numBytesWritten)
                {
                    System.Threading.Thread.Sleep(1);
                    timeout--;
                }
            } while (numBytesAvailable < numBytesWritten);

            return true;
        }

        public bool SendData(byte[] data)
        {
            if (!_ChangeBitrate(_bitrate))
                return false;

            uint numBytesWritten = 0;
            _ftdiDevice.Write(data, data.Length, ref numBytesWritten);

            // Wait until own outData has been sent or Timeout
            int timeout = TIMEOUT_ms;
            uint numBytesAvailable = 0;
            do
            {
                FTDI.FT_STATUS ftStatus = _ftdiDevice.GetRxBytesAvailable(ref numBytesAvailable);
                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    return false;
                }
                if (timeout == 0)
                    return false;
                if (numBytesAvailable < numBytesWritten)
                {
                    System.Threading.Thread.Sleep(1);
                    timeout--;
                }
            } while (numBytesAvailable < numBytesWritten);

            return true;
        }

        public bool ReceiveData(ref byte[] data)
        {
            FTDI.FT_STATUS ftStatus;
            int timeout = TIMEOUT_ms;
            uint numBytesAvailable = 0;
            do
            {
                ftStatus = _ftdiDevice.GetRxBytesAvailable(ref numBytesAvailable);
                if ((ftStatus != FTDI.FT_STATUS.FT_OK) || (numBytesAvailable < data.Length))
                {
                    System.Threading.Thread.Sleep(1);
                    timeout--;
                }
            } while ((numBytesAvailable < data.Length) && (timeout != 0));

            // limit to size of receive buffer
            uint recBytes = numBytesAvailable;
            if (recBytes > data.Length)
                recBytes = (uint) data.Length;

            // receive outData anyway for debug reasons
            uint numBytesRead = 0;
            ftStatus = _ftdiDevice.Read(data, recBytes, ref numBytesRead);

            if (_receiveDataCallback != null) _receiveDataCallback(data, numBytesRead, (timeout == 0) ? true : false);
            
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
                return false;
            if (timeout == 0)
                return false;

            return true;
        }

    }
}
