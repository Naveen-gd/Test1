using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using PCANBasic_NET;

namespace Can_Comm_Lib
{
    public class CanComm
    {
        private PeakComm _peakComm;
        private VectorComm _vectorComm;
        private CanCommAdapter _adapter = CanCommAdapter.VECTOR;

        public static int GetBytesFromDLC(CanCommDlc dlc)
        {
            if (dlc <= CanCommDlc.DLC_Bytes_8)
                return Convert.ToInt16(dlc);

            switch (dlc)
            {
                case CanCommDlc.DLC_Bytes_FD_12: return 12;
                case CanCommDlc.DLC_Bytes_FD_16: return 16;
                case CanCommDlc.DLC_Bytes_FD_20: return 20;
                case CanCommDlc.DLC_Bytes_FD_24: return 24;
                case CanCommDlc.DLC_Bytes_FD_32: return 32;
                case CanCommDlc.DLC_Bytes_FD_48: return 48;
                case CanCommDlc.DLC_Bytes_FD_64: return 64;
            }
            return 0;
        }

        public CanComm(string appName)
        {
            _peakComm = new PeakComm();
            _vectorComm = new VectorComm(appName);
        }

        public void SelectPeak()
        {
            _adapter = CanCommAdapter.PEAK;
        }

        public void SelectVector()
        {
            _adapter = CanCommAdapter.VECTOR;
        }

        public bool Connected()
        {
            switch (_adapter)
            {
                case CanCommAdapter.PEAK: return _peakComm.Connected();
                case CanCommAdapter.VECTOR: return _vectorComm.Connected();
            }
            return false;
        }

        public void Reset()
        {
            switch (_adapter)
            {
                case CanCommAdapter.PEAK: _peakComm.Reset(); break;
                case CanCommAdapter.VECTOR: _vectorComm.Reset(); break;
            }
        }

        public static CanCommBitrateConfig GetBitrateConfigForBitrate(CanCommAdapter adapter, CanCommBitrate bitrate)
        {
            switch (adapter)
            {
                case CanCommAdapter.PEAK: return PeakComm.GetBitrateConfigForBitrate(bitrate);
                case CanCommAdapter.VECTOR: return VectorComm.GetBitrateConfigForBitrate(bitrate);
            }

            throw new SystemException("invalid adapter");
        }

        public static bool ValidateBitrateConfig(CanCommAdapter adapter, ref CanCommBitrateConfig bitrateConfig)
        {
            switch (adapter)
            {
                case CanCommAdapter.PEAK: return PeakComm.ValidateBitrateConfig(ref bitrateConfig);
                case CanCommAdapter.VECTOR: return VectorComm.ValidateBitrateConfig(ref bitrateConfig);
            }

            throw new SystemException("invalid adapter");
        }

        public void Open(CanCommAdapter adapter, CanCommBitrateConfig bitrateConfig)
        {
            switch (adapter)
            {
                case CanCommAdapter.PEAK: SelectPeak(); break;
                case CanCommAdapter.VECTOR: SelectVector(); break;
            }
            Open(bitrateConfig);
        }

        public void Open(CanCommBitrateConfig bitrateConfig)
        {
            switch (_adapter)
            {
                case CanCommAdapter.PEAK: _peakComm.Open(bitrateConfig); break;
                case CanCommAdapter.VECTOR: _vectorComm.Open(bitrateConfig); break;
            }
        }

        public void Close()
        {
            switch (_adapter)
            {
                case CanCommAdapter.PEAK: _peakComm.Close(); break;
                case CanCommAdapter.VECTOR: _vectorComm.Close(); break;
            }
        }

        public void SendMsg(uint id, CanCommDlc dlc, byte[] data)
        {
            switch (_adapter)
            {
                case CanCommAdapter.PEAK: _peakComm.SendMsg(id, dlc, data); break;
                case CanCommAdapter.VECTOR: _vectorComm.SendMsg(id, dlc, data); break;
            }
        }

        public bool ReceiveMsg(ref uint id, ref CanCommDlc dlc, ref byte[] data)
        {
            switch (_adapter)
            {
                case CanCommAdapter.PEAK: return _peakComm.ReceiveMsg(ref id, ref dlc, ref data);
                case CanCommAdapter.VECTOR: return _vectorComm.ReceiveMsg(ref id, ref dlc, ref data);
            }
            return false;
        }
    }
}
