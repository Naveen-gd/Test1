using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Device_52294_Lib;
using UcanCommLib;
using FtdiLib;

namespace _52294_Socket_Master
{
    internal class CommandParser
    {
        private AsynchronousSocketListener _asyncSocketListenerRef;
        private ConnectionTimeout _connectionTimeoutRef;
        private UcanMaster _masterRef;

        internal CommandParser(AsynchronousSocketListener asyncSocketListenerRef, ConnectionTimeout connectionTimeoutRef, UcanMaster masterRef)
        {
            _asyncSocketListenerRef = asyncSocketListenerRef;
            _connectionTimeoutRef = connectionTimeoutRef;
            _masterRef = masterRef;

            _asyncSocketListenerRef.receivedCallback = Parse;
        }

        private void _SendReply(String reply)
        {
            _asyncSocketListenerRef.Send(reply + "\r\n");
        }

        private void _SendReplyConnected()
        {
            _SendReply(String.Format("connected={0:D}", _masterRef.GetConnected() ? 1 : 0));
        }

        private void _SendReplyPeriodic()
        {
            _SendReply(String.Format("periodic={0:D}", _masterRef.GetAutoNodeEnabled() ? 1 : 0));
        }

        private bool _ParseNoParam(String data, String command)
        {
            MatchCollection matches;
            matches = Regex.Matches(data, @"^" + command);
            if (matches.Count > 0)
            {
                _connectionTimeoutRef.Trigger();
                return true;
            }
            return false;
        }

        private bool _ParseBoolParam(String data, String command, ref bool param)
        {
            MatchCollection matches;
            matches = Regex.Matches(data, @"^" + command + @"\(([0-1]+)\)");
            if ((matches.Count > 0) && (matches[0].Groups.Count > 1))
            {
                param = matches[0].Groups[1].Value.ParseAsBool();
                _connectionTimeoutRef.Trigger();
                return true;
            }
            return false;
        }

        private bool _ParseDecParam(String data, String command, ref uint param)
        {
            MatchCollection matches;
            matches = Regex.Matches(data, @"^" + command + @"\(([0-9x]+)\)");
            if ((matches.Count > 0) && (matches[0].Groups.Count > 1))
            {
                param = matches[0].Groups[1].Value.ParseAsUInt();
                _connectionTimeoutRef.Trigger();
                return true;
            }
            return false;
        }

        private bool _ParseDecDecParam(String data, String command, ref uint param0, ref uint param1)
        {
            MatchCollection matches;
            matches = Regex.Matches(data, @"^" + command + @"\(([0-9x]+)\s*,\s*([0-9x]+)\)");
            if ((matches.Count > 0) && (matches[0].Groups.Count > 2))
            {
                param0 = matches[0].Groups[1].Value.ParseAsUInt();
                param1 = matches[0].Groups[2].Value.ParseAsUInt();
                _connectionTimeoutRef.Trigger();
                return true;
            }
            return false;
        }

        private bool _ParseDecDecDecParam(String data, String command, ref uint param0, ref uint param1, ref uint param2)
        {
            MatchCollection matches;
            matches = Regex.Matches(data, @"^" + command + @"\(([0-9x]+)\s*,\s*([0-9x]+)\s*,\s*([0-9x]+)\)");
            if ((matches.Count > 0) && (matches[0].Groups.Count > 3))
            {
                param0 = matches[0].Groups[1].Value.ParseAsUInt();
                param1 = matches[0].Groups[2].Value.ParseAsUInt();
                param2 = matches[0].Groups[3].Value.ParseAsUInt();
                _connectionTimeoutRef.Trigger();
                return true;
            }
            return false;
        }

        private bool _ParseDecDecDecDecParam(String data, String command, ref uint param0, ref uint param1, ref uint param2, ref uint param3)
        {
            MatchCollection matches;
            matches = Regex.Matches(data, @"^" + command + @"\(([0-9x]+)\s*,\s*([0-9x]+)\s*,\s*([0-9x]+)\s*,\s*([0-9x]+)\)");
            if ((matches.Count > 0) && (matches[0].Groups.Count > 4))
            {
                param0 = matches[0].Groups[1].Value.ParseAsUInt();
                param1 = matches[0].Groups[2].Value.ParseAsUInt();
                param2 = matches[0].Groups[3].Value.ParseAsUInt();
                param3 = matches[0].Groups[4].Value.ParseAsUInt();
                _connectionTimeoutRef.Trigger();
                return true;
            }
            return false;
        }

        private bool _ParseDoubleParam(String data, String command, ref double param)
        {
            MatchCollection matches;
            matches = Regex.Matches(data, @"^" + command + @"\(([0-9,]+)\)");
            if ((matches.Count > 0) && (matches[0].Groups.Count > 1))
            {
                param = double.Parse(matches[0].Groups[1].Value);
                _connectionTimeoutRef.Trigger();
                return true;
            }
            return false;
        }

        private bool _ParseStringParam(String data, String command, ref string param)
        {
            MatchCollection matches;
            matches = Regex.Matches(data, @"^" + command + @"\((.*)\)");
            if ((matches.Count > 0) && (matches[0].Groups.Count > 1))
            {
                param = matches[0].Groups[1].Value;
                _connectionTimeoutRef.Trigger();
                return true;
            }
            return false;
        }

        internal void Parse(String data)
        {
            uint decVal0 = 0;
            uint decVal1 = 0;
            uint decVal2 = 0;
            uint decVal3 = 0;
            string stringVal = String.Empty;
            double doubleVal = 0.0;
            bool boolVal = false;

            if (_ParseNoParam(data, "Exit"))
            {
                _asyncSocketListenerRef.CloseConnection();
            }

            // ---------------------------------------------------------------------------------------------------------------

            else if (_ParseDecParam(data, "SetTimeout", ref decVal0))
            {
                _connectionTimeoutRef.TimeoutSeconds = decVal0;

                _SendReply(String.Format("timeout={0:D}", _connectionTimeoutRef.TimeoutSeconds));
            }
            else if (_ParseNoParam(data, "KeepAlive"))
            {
                _SendReply("1");
            }

            // ---------------------------------------------------------------------------------------------------------------

            else if (_ParseNoParam(data, "ListDevices"))
            {
                List<FtdiBase.DeviceListEntry> devList = _masterRef.GetUCanCommDevices();
                _SendReply(String.Format("{0:D}", devList.Count));
                foreach (FtdiBase.DeviceListEntry dev in devList)
                {
                    _SendReply(dev.SerialNumber+";"+dev.Description+";"+String.Format("{0:D}ch", dev.Channels));
                }
            }
            else if (_ParseStringParam(data, "OpenSerial", ref stringVal))
            {
                try
                {
                    _masterRef.OpenChannelBySerialNumber(stringVal);
                }
                catch
                {
                }
                _SendReplyConnected();
            }
            else if (_ParseNoParam(data, "Close"))
            {
                _masterRef.SetAutoNodeEnabled(false);
                _masterRef.Close();
                _SendReplyConnected();
            }

            // ---------------------------------------------------------------------------------------------------------------

            else if (_ParseDecParam(data, "SetBitrate", ref decVal0))
            {
                _masterRef.SetCommBitrate(decVal0);
                _SendReply(String.Format("bitrate={0:D}", _masterRef.GetCommBitrate()));
            }
            else if (_ParseDecParam(data, "SetSync", ref decVal0))
            {
                if (decVal0 == 8) _masterRef.SetCommSync(decVal0);
                if (decVal0 == 32) _masterRef.SetCommSync(decVal0);
                UcanCommParameters commParameters = _masterRef.GetUcanCommParametersCopy();
                _SendReply(String.Format("sync={0:D}", commParameters.sync));
            }
            else if (_ParseDecParam(data, "SetHeader", ref decVal0))
            {
                if (decVal0 == 3) _masterRef.SetCommHeader(decVal0);
                if (decVal0 == 4) _masterRef.SetCommHeader(decVal0);
                UcanCommParameters ucanCommParameters = _masterRef.GetUcanCommParametersCopy();
                _SendReply(String.Format("header={0:D}", ucanCommParameters.header));
            }
            else if (_ParseDecParam(data, "SetParity", ref decVal0))
            {
                if (decVal0 == 0) _masterRef.SetCommParity(decVal0);
                if (decVal0 == 1) _masterRef.SetCommParity(decVal0);
                if (decVal0 == 2) _masterRef.SetCommParity(decVal0);
                if (decVal0 == 3) _masterRef.SetCommParity(decVal0);
                _SendReply(String.Format("parity={0:D}", _masterRef.GetCommParity()));
            }
            else if (_ParseDoubleParam(data, "SetBreak", ref doubleVal))
            {
                _masterRef.SetCommBreakLength(doubleVal);
                _SendReply(String.Format("break={0:F}", _masterRef.GetCommBreakLength()));
            }

            // ---------------------------------------------------------------------------------------------------------------

            else if (_ParseDecParam(data, "SetNodeAddr", ref decVal0))
            {
                _masterRef.SetDeviceNode(0, (byte)decVal0);
                _SendReply(String.Format("addr={0:D}", _masterRef.GetDeviceNode(0)));
            }
            else if (_ParseDecDecParam(data, "SendWakeup", ref decVal0, ref decVal1))
            {
                _masterRef.SendWakeup((decVal0 > 0) ? true : false, (decVal1 > 0) ? true : false);
                _SendReply("1");
            }
            else if (_ParseNoParam(data, "SendSleepBroadcast"))
            {
                _masterRef.SendSleepBroadcast();
                _SendReply("1");
            }

            // ---------------------------------------------------------------------------------------------------------------

            else if (_ParseDecDecDecParam(data, "Write", ref decVal0, ref decVal1, ref decVal2))
            {
                ushort[] wdata = new ushort[decVal1];
                for (int i = 0; i < decVal1; i += 1)
                {
                    wdata[i] = (ushort)decVal2;
                }
                bool ret = _masterRef.WriteData(0, (ushort)decVal0, wdata);
                _SendReply(String.Format("{0:D}", ret ? 1 : 0));
            }
            else if (_ParseDecDecParam(data, "Read", ref decVal0, ref decVal1))
            {
                ushort[] rdata = new ushort[decVal1];
                bool ret = _masterRef.ReadData(0, (ushort)decVal0, ref rdata);
                if (ret)
                {
                    _SendReply("1");
                    for (int i = 0; i < decVal1; i += 1)
                    {
                        _SendReply(rdata[i].ToHexString());
                    }
                }
                else
                {
                    _SendReply("0");
                }
            }
            else if (_ParseDecDecDecDecParam(data, "Verify", ref decVal0, ref decVal1, ref decVal2, ref decVal3))
            {
                ushort[] ret = _masterRef.VerifyData(0, (ushort)decVal0, (ushort)decVal1, (ushort)decVal2, (ushort)decVal3);
                if (ret.Length > 0)
                {
                    _SendReply("0");
                    for (int i = 0; i < decVal1; i += 1)
                    {
                        _SendReply(ret[i].ToHexString());
                    }
                }
                else
                {
                    _SendReply("1");
                }
            }

            // ---------------------------------------------------------------------------------------------------------------

            else if (_ParseDecDecDecParam(data, "SetPeriodicWrite", ref decVal0, ref decVal1, ref decVal2))
            {
                _masterRef.SetAutoNodeWrite((ushort)decVal0, (ushort)decVal1, (ushort)decVal2);
                _SendReply("1");
            }
            else if (_ParseDecDecDecDecParam(data, "SetPeriodicVerify", ref decVal0, ref decVal1, ref decVal2, ref decVal3))
            {
                _masterRef.SetAutoNodeVerify((ushort)decVal0, (ushort)decVal1, (ushort)decVal2, (ushort)decVal3);
                _SendReply("1");
            }
            else if (_ParseDecParam(data, "SetPeriodicIntervalMs", ref decVal0))
            {
                _masterRef.SetAutoWriteIntervalMs((ushort) decVal0);
                _SendReply(String.Format("interval={0:D}", _masterRef.GetAutoWriteIntervalMs()));
            }
            else if (_ParseNoParam(data, "StartPeriodic"))
            {
                _masterRef.SetAutoNodeEnabled(true);
                _SendReplyPeriodic();
            }
            else if (_ParseNoParam(data, "StopPeriodic"))
            {
                _masterRef.SetAutoNodeEnabled(false);
                _SendReplyPeriodic();
            }

            // ---------------------------------------------------------------------------------------------------------------

            else if (_ParseNoParam(data, "GetStatus"))
            {
                _SendReply(String.Format("com_error={0:D}", _masterRef.GetDeviceCommError(0) ? 1 : 0));
                _SendReply(String.Format("verify_error={0:D}", _masterRef.GetDeviceVerifyError(0) ? 1 : 0));
            }

            else if (_ParseNoParam(data, "GetLastLiveCounter"))
            {
                _SendReply(String.Format("{0:D}", _masterRef.GetCommLastLiveCounter()));
            }

            // ---------------------------------------------------------------------------------------------------------------

            else if (_ParseBoolParam(data, "SetDebugHeaderCrcError", ref boolVal))
            {
                _masterRef.SetCommDebugHeaderCrcError(boolVal);
                _SendReply(String.Format("debugHeaderCrcError={0:D}", _masterRef.GetCommDebugHeaderCrcError() ? 1 : 0));
            }

            else if (_ParseBoolParam(data, "SetDebugWriteCrcError", ref boolVal))
            {
                _masterRef.SetCommDebugWriteCrcError(boolVal);
                _SendReply(String.Format("debugWriteCrcError={0:D}", _masterRef.GetCommDebugWriteCrcError() ? 1 : 0));
            }

            else if (_ParseBoolParam(data, "SetDebugUseLastLiveCounter", ref boolVal))
            {
                _masterRef.SetCommDebugUseLastLiveCounter(boolVal);
                _SendReply(String.Format("debugUseLastLiveCounter={0:D}", _masterRef.GetCommDebugUseLastLiveCounter() ? 1 : 0));
            }

            // ---------------------------------------------------------------------------------------------------------------
            else
            {
                _SendReply("E");
            }
        }
    }
}
