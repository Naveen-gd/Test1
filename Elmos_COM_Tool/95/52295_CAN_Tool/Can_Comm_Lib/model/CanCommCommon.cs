using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Can_Comm_Lib
{

    public enum CanCommAdapter
    {
        NONE,
        PEAK,
        VECTOR
    };

    public enum CanCommBitrate
    {
        BITRATE_500_SP80_500_SP80,
        BITRATE_500_SP80_1000_SP70,
        BITRATE_500_SP60_2000_SP60,
        BITRATE_500_SP70_2000_SP70,
        BITRATE_500_SP80_2000_SP60,
        BITRATE_500_SP80_2000_SP70,
        BITRATE_500_SP80_4000_SP70,
        BITRATE_1000_SP70_2000_SP70
    };

    public enum CanCommDlc
    {
        DLC_Bytes_0,
        DLC_Bytes_1,
        DLC_Bytes_2,
        DLC_Bytes_3,
        DLC_Bytes_4,
        DLC_Bytes_5,
        DLC_Bytes_6,
        DLC_Bytes_7,
        DLC_Bytes_8,
        DLC_Bytes_FD_12,
        DLC_Bytes_FD_16,
        DLC_Bytes_FD_20,
        DLC_Bytes_FD_24,
        DLC_Bytes_FD_32,
        DLC_Bytes_FD_48,
        DLC_Bytes_FD_64
    };



}
