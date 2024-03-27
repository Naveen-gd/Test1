using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using FTD2XX_NET;

namespace FtdiLib
{
    public class FtdiBitBang : FtdiBase
    {
        private byte _outputs;
        private byte _defaultState;

        private Queue<byte> _dataOutFifo;

        override protected bool _InitialConfigAfterOpen()
        {
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            // Synchronous mode has gaps and is not equidistant
            // so unfortunately we cannot use input sampling
            byte mode = FTDI.FT_BIT_MODES.FT_BIT_MODE_ASYNC_BITBANG;

            // Pins configured as output will drive immediate a low level.
            // Therefore it is necessary to configure all pins as input, set the default level, then reconfigure to the outputs.
            ftStatus = _ftdiDevice.SetBitMode(0x00, mode);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }

            uint numBytesWritten = 0;
            byte[] defaults = new byte[1];
            defaults[0] = _defaultState;
            ftStatus = _ftdiDevice.Write(defaults, defaults.Length, ref numBytesWritten);
            if ((ftStatus != FTDI.FT_STATUS.FT_OK) || (numBytesWritten != 1))
            {
                return false;
            }

            // Set Mode
            ftStatus = _ftdiDevice.SetBitMode(_outputs, mode);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }

            // Timeouts need to be large otherwise the frames will be interrupted after the timeout time
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

            return true;
        }

        public FtdiBitBang(byte outputs, byte defaultState)
            : base()
        {
            _dataOutFifo = new Queue<byte>(0);
            _outputs = outputs;
            _defaultState = defaultState;
        }

        ///<summary>
        /// Set Sample Rate which will be multiplied by 5. Max 2000000 = 10Msps.
        /// </summary>
        public bool SetSampleRate_div5(uint sampleRate_div5)
        {
            FTDI.FT_STATUS ftStatus = _ftdiDevice.SetBaudRate(sampleRate_div5);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }
            return true;
        }

        ///<summary>
        /// Only in asynchronous mode
        /// </summary>
        public bool DataAsyncOut(byte[] outData)
        {
            uint numBytesWritten = 0;

            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
            ftStatus = _ftdiDevice.Write(outData, outData.Length, ref numBytesWritten);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }

            return (numBytesWritten == outData.Length);
        }

        ///<summary>
        /// Start collection of val into byte FIFO
        /// </summary>
        public void StartCollect()
        {
            _dataOutFifo.Clear();
        }

        ///<summary>
        /// Add sample to end of byte FIFO
        /// </summary>
        public void AddSample(byte data)
        {
            _dataOutFifo.Enqueue(data);
        }

        ///<summary>
        /// Send sample FIFO to output but keep content
        /// </summary>
        public bool SendPattern()
        {
            byte[] patGenOutData = new byte[_dataOutFifo.Count];

            for (int i = 0; i < _dataOutFifo.Count; i += 1)
            {
                patGenOutData[i] = _dataOutFifo.ElementAt(i);
            }

            return DataAsyncOut(patGenOutData);
        }


    }
}
