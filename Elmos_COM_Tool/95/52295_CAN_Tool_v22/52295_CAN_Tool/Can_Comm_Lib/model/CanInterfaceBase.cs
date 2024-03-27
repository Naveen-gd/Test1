using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Can_Comm_Lib
{
    internal abstract class CanInterfaceBase
    {
        protected ushort canTimeoutMs = 100;

        public abstract bool Connected();

        public abstract void Reset();

        public abstract void Open(CanCommBitrateConfig bitrateConfig);

        public abstract void Close();

        public abstract void SendMsg(uint id, CanCommDlc dlc, byte[] data);

        public abstract bool ReceiveMsg(ref uint id, ref CanCommDlc dlc, ref byte[] data);

    }
}
