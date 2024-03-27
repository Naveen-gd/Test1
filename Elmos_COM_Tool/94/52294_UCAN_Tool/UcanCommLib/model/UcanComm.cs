using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Linq;

using FtdiLib;
using Extensions;

namespace UcanCommLib
{

    public class UcanCommParameters
    {
        public uint sync = 8;
        public uint header = 3;

        public bool debugHeaderCrcError = false;
        public bool debugWriteCrcError = false;
        public bool debugUseLastLiveCounter = false;
    }

    public class UcanComm : FtdiBitBangUart
    {
        private UcanCommParameters _ucanCommParamtersRef;

        #region "LiveCounter"
        // Single Livecounter for whole communication!
        private byte _liveCounter = 0;
        private byte _lastLiveCounter = 0;

        private void _IncrementLivecounter()
        {
            if (!_ucanCommParamtersRef.debugUseLastLiveCounter)
            {
                _liveCounter += 1;
                _liveCounter &= 0x3F;
                if (_liveCounter == 0) _liveCounter = 1;   // invalid
            }
        }

        private void _SaveLastLiveCounter()
        {
            _lastLiveCounter = _liveCounter;
        }

        public byte GetLastLiveCounter()
        {
            return _lastLiveCounter;
        }
        #endregion

        public UcanComm(UcanCommParameters ucanCommParamtersRef)
        {
            _ucanCommParamtersRef = ucanCommParamtersRef;
        }

        private byte[] _BuildSync()
        {
            byte sync_len = 1;                    // SYNC

            if (_ucanCommParamtersRef.sync == 32)
                sync_len += 3;

            byte[] sync = new byte[sync_len];
            byte index = 0;
            sync[index] = 0x55; index += 1;     // SYNC
            if (_ucanCommParamtersRef.sync == 32)
            {
                sync[index] = 0x55; index += 1;      // SYNC32
                sync[index] = 0x55; index += 1;      // SYNC32
                sync[index] = 0x55; index += 1;      // SYNC32
            }

            return sync;
        }

        private byte[] _BuildHeader(byte node, ushort addr, byte num_words, bool write)
        {
            byte[] sync = _BuildSync();

            int header_len = sync.Length;
            
            header_len += 3;                    // 3 byte header format
            if (_ucanCommParamtersRef.header == 4)
                header_len += 1;                    // 4 byte header format

            byte[] header = new byte[header_len];
            Buffer.BlockCopy(sync, 0, header, 0, sync.Length);

            int index = sync.Length;

            header[index] = (byte)(addr >> 1); index += 1;      // Memory Address

            header[index] = 0x00;                    // Device + W/nR + num words
            header[index] |= (byte)(node & 0x1F);
            header[index] |= (byte)((num_words & 0xF) << 6);
            if (write) header[index] |= 0x20;                   // write
            index += 1;

            header[index] = 0x00;      // num words + crc6/livecnt
            header[index] |= (byte)((num_words & 0xF) >> 2);
            if (_ucanCommParamtersRef.header == 4)
            {
                _IncrementLivecounter();
                header[index] |= (byte)(_liveCounter << 2);     // live counter
                index += 1;
                _SaveLastLiveCounter();
            }

            // header crc
            byte rev_poly;
            byte crc;
            if (_ucanCommParamtersRef.header == 4)
            {
                rev_poly = 0xF4;    // 0x97
                crc = 0xFF;
            }
            else
            {
                rev_poly = 0x26;    // 0x2C
                crc = 0x3F;
            }

            int crc_len;
            if (_ucanCommParamtersRef.header == 4) crc_len = (header_len - 1);  // excl CRC byte
            else crc_len = header_len;                                           // incl CRC byte with num_words

            int crc_start = sync.Length;

            for (int d = crc_start; d < crc_len; d += 1)
            {
                byte temp = header[d];
                for (int b = 0; b < 8; b += 1)
                {
                    if ((crc & 1) != (temp & 1))
                        crc = (byte)((byte)(crc >> 1) ^ rev_poly);
                    else
                        crc = (byte)(crc >> 1);
                    temp >>= 1;
                }
            }

            if (_ucanCommParamtersRef.debugHeaderCrcError) crc ^= 0xFF;

            if (_ucanCommParamtersRef.header == 4) header[index] = crc;    // CRC8
            else header[index] |= (byte)(crc << 2);                        // CRC6

            return header;
        }

        private bool _SendAndCheckBreak()
        {
            StartCollect();

            if (!SendBreak())
                return false;

            if (!SendPattern())
                return false;

            byte[] rdata = new byte[1];

            if (!ReceiveData(ref rdata))
                return false;

            if (rdata[0] != 0)
                return false;

            return true;
        }

        private bool _SendHeader(byte[] header)
        {
            if (!SendData(header))
                return false;

            return true;
        }

        private byte _CalcDataCRC(byte[] data, byte num_words, byte offset, byte end)
        {
            byte rev_poly;
            byte crc = 0xFF;
            if (num_words < 11) rev_poly = 0xf4;    // 0x97
            else rev_poly = 0xb2;    // 0xA6

            for (int d = offset; d < end; d += 1)
            {
                byte temp = data[d];
                for (int b = 0; b < 8; b += 1)
                {
                    if ((crc & 1) != (temp & 1))
                        crc = (byte)((byte)(crc >> 1) ^ rev_poly);
                    else
                        crc = (byte)(crc >> 1);
                    temp >>= 1;
                }
            }
            return crc;
        }

        private bool _VerifyHeaderReadback(byte[] header, byte[] rdata)
        {
            for (int i = 0; i < header.Length; i += 1)          // Header check
            {
                if (rdata[i] != header[i])
                    return false;
            }
            return true;
        }

        private bool _VerifyResponse(byte resp)
        {
            int zeroes = 0;
            for (int i = 0; i < 8; i += 1)
            {
                if (((resp >> i) & 1) == 0)
                    zeroes += 1;
            }

            /* Requirements:
               The master has to evaluate a response byte with less or equal than 4 zero-bits as an OKAY response.
               The master has to evaluate a response byte with more than 4 zero-bits as an ERROR response.
            */
            if (zeroes <= 4)
                return true;

            return false;
        }

        private bool _VerifyDataCRC(byte[] data, byte num_words, byte header_len)
        {
            byte calc_crc = _CalcDataCRC(data, num_words, header_len, (byte) (data.Length - 1));
            byte recv_crc = data[data.Length - 1];
            return true;
        }

        public bool WriteData(byte node, ushort addr, ushort[] data)
        {
            try
            {
                Reset();

                byte words = (byte)data.Length;
                byte offset = 0;

                while (words > 0)
                {
                    byte write_words = words;
                    if (write_words > 16)
                        write_words = 16;

                    byte num_words = (byte)(write_words - 1); // +1 by UART

                    _SendAndCheckBreak();

                    byte[] header = _BuildHeader(node, addr, num_words, true);

                    StartCollect();

                    if (!_SendHeader(header))
                        return false;

                    ExpectByte();

                    if (!SendPattern())
                      return false;

                    byte header_len = (byte)header.Length;
                    header_len += 1;                            // response

                    byte exp_len = (byte)header.Length;
                    exp_len += 1; // response

                    byte[] rdata = new byte[exp_len];
                    if (!ReceiveData(ref rdata))
                        return false;

                    if (!_VerifyHeaderReadback(header, rdata))
                        return false;

                    if (!_VerifyResponse(rdata[header_len - 1]))  // Header Response
                        return false;

                    // Data
                    byte dat_len = (byte)((write_words * 10) / 8);
                    if (((write_words * 10) % 8) > 0)
                        dat_len += 1;

                    dat_len += 1; // CRC

                    byte[] wdata = new byte[dat_len];

                    // convert 10 -> 8 bit
                    int byte_index = 0;
                    int shift = 0;
                    UInt16 word10 = 0;
                    for (int i = 0; i < write_words; i += 1)
                    {
                        word10 = (ushort) (data[offset + i] & 0x3FF);
                        wdata[byte_index] |= (byte)(word10 << shift);
                        wdata[byte_index + 1] |= (byte)(word10 >> (8 - shift));

                        shift += 2;
                        if (shift == 8)
                        {
                            shift = 0;
                            byte_index += 1;
                        }

                        byte_index += 1;
                    }

                    byte crc = _CalcDataCRC(wdata, num_words, 0, (byte)(dat_len - 1));
                    if (_ucanCommParamtersRef.debugWriteCrcError) crc ^= 0xFF;

                    wdata[dat_len - 1] = crc;

                    StartCollect();

                    if (!SendData(wdata))
                        return false;

                    ExpectByte();

                    if (!SendPattern())
                        return false;

                    exp_len = (byte)wdata.Length;
                    exp_len += 1; // response

                    rdata = new byte[exp_len];
                    ReceiveData(ref rdata);

                    if (!_VerifyResponse(rdata[exp_len - 1]))  // Write Data Response
                        return false;

                    words -= write_words;
                    offset += write_words;
                    addr += (ushort)(2 * write_words);
                }

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception Information: \n\t" + e.Message);
            }

            return false;
        }

        public bool ReadData(byte node, ushort addr, ref ushort[] data)
        {
            try
            {
                byte words = (byte) data.Length;
                byte offset = 0;

                Reset();

                while (words > 0)
                {
                    byte read_words = words;
                    if (read_words > 16) 
                        read_words = 16;

                    byte num_words = (byte) (read_words - 1); // +1 by UART

                    _SendAndCheckBreak();

                    byte[] header = _BuildHeader(node, addr, num_words, false);

                    byte header_len = (byte)header.Length;
                    header_len += 1; // response

                    byte data_len = (byte)((read_words * 10) / 8);
                    if (((read_words * 10) % 8) > 0)
                        data_len += 1;

                    byte exp_len = (byte) (header_len + data_len);
                    exp_len += 1; // CRC

                    StartCollect();

                    if (!_SendHeader(header))
                        return false;

                    for (int f = header_len;  f < exp_len; f += 1){
                        ExpectByte();
                    }

                    if (!SendPattern())
                        return false;

                    byte[] rdata = new byte[exp_len];
                    if (!ReceiveData(ref rdata))                // Read Timeout
                        return false;

                    if (!_VerifyHeaderReadback(header, rdata))
                        return false;

                    if (!_VerifyResponse(rdata[header_len - 1]))            // Header Response
                        return false;

                    if (!_VerifyDataCRC(rdata, num_words, header_len))
                        return false;

                    // convert 8 -> 10 bit
                    // step byte
                    // 0    8 + 0
                    // 1    2 + 6
                    // 2    4 + 4
                    // 3    6 + 2
                    // 4    8 + 0

                    int word_index = 0;
                    int shift = 0;
                    int bits = 0;
                    UInt16 word10 = 0;
                    UInt16 temp10 = 0;
                    for (int i = 0; i < data_len; i += 1)
                    {
                        UInt16 curr = rdata[header_len + i];

                        // lsb part of byte
                        temp10 = (UInt16)(curr);
                        word10 |= (UInt16)(temp10 << bits);
                        int needed = 10 - bits;
                        bits += 8;
                        if (bits >= 10)
                        {
                            word10 &= 0x3FF;
                            data[offset + word_index] = word10;
                            word_index += 1;
                            bits = 0;
                            word10 = 0;
                        }

                        // msb part of byte
                        if (needed < 8)
                        {
                            shift = needed;
                            bits = 8 - needed;

                            temp10 = (UInt16)(curr >> shift);
                            word10 |= (UInt16)(temp10);
                        }
                    }

                    words -= read_words;
                    offset += read_words;
                    addr += (ushort) (2 * read_words);
                }

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception Information: \n\t" + e.Message);
            }

            return false;
        }

    }

}
