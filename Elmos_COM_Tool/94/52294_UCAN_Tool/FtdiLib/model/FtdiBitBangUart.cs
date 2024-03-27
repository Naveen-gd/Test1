using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FtdiLib
{
    // Like UART class but optional with pattern gen as transmit
    // requires dual channel UART FTDI for mixed mode

    public class FtdiBitBangUart : FtdiUart         
    {
        private static byte _OutputMask = 0x01;

        public enum Mode
        {
            UART_RX_TX,
            UART_RX_PG_TX
        }

        private FtdiBitBang _ftdiBitBang;

        new public SendBreakCallback sendBreakCallback
        {
            set { 
                _sendBreakCallback = value;
                base.sendBreakCallback = value;
            }
        }

        new public ReceiveDataCallback receiveDataCallback
        {
            set { base.receiveDataCallback = value; }
        }


        #region "Config"
        private int _overSampling = 5;
        private Mode _mode;

        public int overSampling
        {
            get { return _overSampling; }
            set { _overSampling = value; }
        }

        public void SetMode(Mode mode)
        {
            if (!Connected()){
             _mode = mode; 
            }
        }

        new public void SetBitrate(uint bitrate)
        {
            base.SetBitrate(bitrate);
            // Example:
            // 500kbits @ oversampling(10) =  4Msps
            // 2Mbits   @ oversampling(5)  = 10Msps (10Msps is max!)
            // 4Msps / 5 = 200000
            uint sampleRateDiv5 = (uint)(bitrate * _overSampling / 5);
            _ftdiBitBang.SetSampleRate_div5(sampleRateDiv5);
        }
        #endregion

        public FtdiBitBangUart()
        {
            _ftdiBitBang = new FtdiBitBang(_OutputMask, _OutputMask);
        }

        #region "Connection Handling"

        new public void OpenChannelByDevice(DeviceListEntry device, uint channel = 0)
        {
            base.OpenChannelByDevice(device, channel);

            if (_mode == Mode.UART_RX_PG_TX)
            {
                _ftdiBitBang.OpenChannelByDevice(device, channel + 1);
                base.SendByte(0x00);    // send dummy byte to enable receiving ???
            }

            if (_connectedChangeCallback != null) _connectedChangeCallback(Connected());
        }

        new public void OpenChannelBySerialNumber(String serialNumber, uint channel = 0)
        {
            base.OpenChannelBySerialNumber(serialNumber, channel);

            if (_mode == Mode.UART_RX_PG_TX)
            {
                _ftdiBitBang.OpenChannelBySerialNumber(serialNumber, channel + 1);
                base.SendByte(0x00);    // send dummy byte to enable receiving ???
            }

            if (_connectedChangeCallback != null) _connectedChangeCallback(Connected());
        }

        new public bool Connected()
        {
            bool ret = true;
            ret &= base.Connected();
            if (_mode == Mode.UART_RX_PG_TX)
                ret &= _ftdiBitBang.Connected();
            return ret;
        }

        new public bool Reset()
        {
            base.Reset();
            if (_mode == Mode.UART_RX_PG_TX)
            {
                _ftdiBitBang.Reset();
                _ftdiBitBang.StartCollect();

                base.SendByte(0x00);    // send dummy byte to enable receiving ???
            }
            return true;
        }

        new public void Close()
        {
            base.Close();
            _ftdiBitBang.Close();

            if (_connectedChangeCallback != null) _connectedChangeCallback(Connected());
        }
        #endregion

        #region "Pattern Gen specific"

        public void ExpectByte(int bitTimes = 15)
        {
            if (_mode == Mode.UART_RX_PG_TX)
            {
                for (byte b = 0; b < bitTimes; b += 1)
                {
                    SendBit(true);
                }
            }
        }

        public void StartCollect()
        {
            if (_mode == Mode.UART_RX_PG_TX)
            {
                _ftdiBitBang.StartCollect();
            }
        }

        public bool SendPattern()
        {
            if (_mode == Mode.UART_RX_PG_TX)
            {
                return _ftdiBitBang.SendPattern();
            }
            return true;
        }
        #endregion

        private bool _SetOutputImmediately(bool high)
        {
            if (_mode == Mode.UART_RX_PG_TX)
            {
                byte data = (byte)(high ? _OutputMask : 0);
                _ftdiBitBang.StartCollect();
                _ftdiBitBang.AddSample(data);
                return _ftdiBitBang.SendPattern();
            }

            return true;
        }

        public void SendBit(bool val)
        {
            if (_mode == Mode.UART_RX_PG_TX)
            {
                byte data = (byte)(val ? _OutputMask : 0);
            
                for (byte b = 0; b < _overSampling; b += 1)
                {
                    _ftdiBitBang.AddSample(data);
                }
            }
        }

        new public bool SetBreak()
        {
            if (_mode == Mode.UART_RX_PG_TX)
                return _SetOutputImmediately(false);
            else
                return base.SetBreak();
        }

        new public bool ClearBreak()
        {
            if (_mode == Mode.UART_RX_PG_TX)
                return _SetOutputImmediately(true);
            else
                return base.ClearBreak();
        }

        new public bool SendWakeup()
        {
            // calc constant time to bits
            double bit_length = 1.0 / _bitrate;

            double wake_bits = (double)_wakeupLength / 1000000 / bit_length;

            if (_mode == Mode.UART_RX_PG_TX)
            {
                _ftdiBitBang.StartCollect();
            }

            if (!SendBreak(wake_bits)) return false;

            if (_mode == Mode.UART_RX_PG_TX)
            {
                if (!_ftdiBitBang.SendPattern()) return false;
            }
            
            return true;
        }

        new public bool SendBreak(double breakLength = 0.0)
        {
            if (_mode == Mode.UART_RX_PG_TX)
            {
                // send break length
                if (breakLength < 1.0)
                    breakLength = _breakLength;

                SendBit(true);   // just some idle space

                uint bits = (uint)breakLength; // floor
                for (uint b = 0; b < bits; b += 1)
                {
                    SendBit(false);
                }

                uint rest = (uint)((breakLength - Convert.ToDouble(bits)) * _overSampling);
                for (uint b = 0; b < rest; b += 1)
                {
                    _ftdiBitBang.AddSample(0);
                }
            
                SendBit(true);
            
                if (_sendBreakCallback != null) _sendBreakCallback();
            }
            else
            {
                return base.SendBreak(breakLength);
            }
            return true;
        }

        new public void SendByte(byte val)
        {
            if (_mode == Mode.UART_RX_PG_TX)
            {
                SendBit(false);  // start
            
                bool even_ones = false;
                for (byte b = 0; b < 8; b += 1)
                {
                    bool bit = (((val >> b) & 1) == 1);
                    even_ones ^= bit;
            
                    SendBit(bit);
                }
            
                /* parity
                 * 0: EVEN: even number of ones
                 * 1: ODD:  even number of ones
                 * 2: ZERO: Parity bit = 0
                 * 3: NONE: no Parity bit
                 */
                switch (_parity)
                {
                    case 0: SendBit(even_ones); break;
                    case 1: SendBit(!even_ones); break;
                    case 2: SendBit(false); break;
                    case 3: break;
                }
            
                SendBit(true);   // stop
            }
            else
            {
                base.SendByte(val);
            }
        }

        new public bool SendData(byte[] data)
        {
            if (_mode == Mode.UART_RX_PG_TX)
            {
                for (int i = 0; i < data.Length; i += 1)
                {
                    SendByte(data[i]);
                }
            }
            else
            {
              return base.SendData(data);
            }

            return true;
        }

        new public bool ReceiveData(ref byte[] data)
        {
            return base.ReceiveData(ref data);
        }
    }
}
