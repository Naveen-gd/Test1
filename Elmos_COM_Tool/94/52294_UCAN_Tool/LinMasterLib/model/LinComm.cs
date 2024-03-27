using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Linq;

using FtdiLib;
using Extensions;

namespace LinMasterLib
{

    public class LinComm
    {
        private FtdiUart _ftdiUart;
        private byte _lastHeaderByte;

        public BitFlag commError;

        public FtdiUart ftdiUartRef
        {
            get { return _ftdiUart; }
        }

        public bool SetCommError()
        {
            commError.SetValue(true);
            return false;
        }

        public bool ClearCommError()
        {
            commError.SetValue(false);
            return true;
        }

        public LinComm()
        {
            _ftdiUart = new FtdiUart();

            commError  = new BitFlag(false);
        }

        public bool SendHeader(byte id)
        {
            try
            {
                _ftdiUart.Reset();

                if (!_ftdiUart.SendBreak())
                    return SetCommError();

                byte[] header = new byte[2];    // SYNC + ID
                header[0] = 0x55;
                header[1] = id;
                // add parity

                if (!_ftdiUart.SendData(header))
                    return SetCommError();

                if (!_VerifyHeaderReadback(id))
                    return SetCommError();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool SendResponse(byte[] data, bool enhanced_checksum = false)
        {
            try
            {
                byte[] response = new byte[data.Length + 1];    // Data + Checksum

                if (!_ftdiUart.SendData(response))
                    return SetCommError();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ReceiveHeader(ref byte id)
        {
            byte[] rdata = new byte[3];     // Break + Sync + ID

            if (!_ftdiUart.ReceiveData(ref rdata))
                return false;

            if (rdata[0] != 0)                                  // BREAK
                return false;

            if (rdata[1] != 0x55)                               // SYNC
                return false;

            // TODO: ID check

            _lastHeaderByte = rdata[2];

            id = (byte)(rdata[2] & 0x3F);

            return true;
        }

        public bool ReceiveResponse(ref byte[] data, bool enhanced_checksum = false)
        {
            if (!_ftdiUart.ReceiveData(ref data))
                return false;

            return true;
        }

        private bool _VerifyHeaderReadback(byte exp_id)
        {
            byte got_id = 0;

            if (!ReceiveHeader(ref got_id))
                return SetCommError();

            if (exp_id != got_id)
                return SetCommError();

            return true;
        }

        private bool _VerifyResponseReadback(byte[] response, bool enhanced_checksum = false)
        {
            byte [] rdata = new byte[response.Length];

            if (!ReceiveResponse(ref rdata, enhanced_checksum))
                return SetCommError();

            // TODO: compare

            return true;
        }

        public void Reset()
        {
            _ftdiUart.Reset();
            commError.Clear();
        }

    }

}
