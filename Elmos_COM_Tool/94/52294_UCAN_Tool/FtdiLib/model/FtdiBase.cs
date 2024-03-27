using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using FTD2XX_NET;

namespace FtdiLib
{
    public class FtdiBase
    {
        #region "Callback"
        public delegate void ConnectedChangeCallback(bool connected);

        protected ConnectedChangeCallback _connectedChangeCallback;

        public ConnectedChangeCallback connectedChangeCallback
        {
            set { _connectedChangeCallback = value; }
        }
        #endregion

        #region "Device List"
        public class DeviceListEntry
        {
            // The device description
            public string Description;
            // The device serialNumber number
            public string SerialNumber;
            // The physical location identifiers of the device
            public List<uint> LocIds;
            // Channels
            public int Channels;

            public DeviceListEntry()
            {
                LocIds = new List<uint>();
            }
        }

        private FTDI.FT_DEVICE_INFO_NODE[] _ftdiDeviceList;
        private List<DeviceListEntry> _deviceList;

        private void _ProcessDeviceList(bool mergeSameSerialNumbers)
        {
            FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            // Determine the number of FTDI devices connected to the machine
            UInt32 ftdiDeviceCount = 0;
            ftStatus = _ftdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return;
            }

            // This is the original FTDI Api Device List
            _ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];
            ftStatus = _ftdiDevice.GetDeviceList(_ftdiDeviceList);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return;
            }

            // Process this list into own local list
            List<DeviceListEntry> locDeviceList = new List<DeviceListEntry>();
            for (int f = 0; f < _ftdiDeviceList.Length; f += 1)
            {
                if ((_ftdiDeviceList[f].Type == FTDI.FT_DEVICE.FT_DEVICE_232H) ||
                    (_ftdiDeviceList[f].Type == FTDI.FT_DEVICE.FT_DEVICE_2232H))
                {
                    DeviceListEntry e = new DeviceListEntry();
                    e.Description = _ftdiDeviceList[f].Description;
                    e.SerialNumber = _ftdiDeviceList[f].SerialNumber;
                    e.LocIds.Add(_ftdiDeviceList[f].LocId);
                    e.Channels = 1;
                    locDeviceList.Add(e);
                }
            }

            // Sort local list by Serial
            locDeviceList.Sort((x, y) => x.SerialNumber.CompareTo(y.SerialNumber));

            // put sorted list into public list
            // optional merge multi channel devices

            if (locDeviceList.Count > 0)
            {
                _deviceList.Add(locDeviceList[0]);
            }

            for (int f = 1; f < locDeviceList.Count; f += 1)
            {
                DeviceListEntry currDev = locDeviceList[f];
                DeviceListEntry prevDev = _deviceList[_deviceList.Count-1];


                if (mergeSameSerialNumbers &&
                    (currDev.SerialNumber.Length > 1) &&
                    (currDev.SerialNumber.Length == prevDev.SerialNumber.Length) &&
                    (currDev.SerialNumber.Substring(0, currDev.SerialNumber.Length - 1) == prevDev.SerialNumber.Substring(0, prevDev.SerialNumber.Length - 1)) &&
                    ((currDev.SerialNumber[currDev.SerialNumber.Length - 1] - 'A') == (prevDev.LocIds.Count)))
                {
                    prevDev.Channels += 1;
                    prevDev.LocIds.Add(locDeviceList[f].LocIds[0]);
                }
                else
                {
                    _deviceList.Add(currDev);
                }
            }

            #if DEBUG
            if (_deviceList.Count == 0)
            {
                DeviceListEntry dummy = new DeviceListEntry();
                dummy.Channels = 0;
                dummy.Description = "Emulation";
                _deviceList.Add(dummy);
            }
            #endif
        }

        public List<DeviceListEntry> GetDevices()
        {
            return _deviceList;
        }
        #endregion

        protected FTDI _ftdiDevice;
        private bool _connected;

        virtual protected bool _InitialConfigAfterOpen()
        {
            return true;
        }

        public FtdiBase(bool bundleSameSerialNumbers = true)
        {
            _ftdiDevice = new FTDI();
            _deviceList = new List<DeviceListEntry>();

            _connected = false;

            _ProcessDeviceList(bundleSameSerialNumbers);

            Close();
        }

        public bool Connected()
        {
            return _connected;
        }

        public bool Reset()
        {
            FTDI.FT_STATUS ftStatus = _ftdiDevice.Purge(FTDI.FT_PURGE.FT_PURGE_RX | FTDI.FT_PURGE.FT_PURGE_TX);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return false;
            }
            return true;
        }

        public void _PostOpen(FTDI.FT_STATUS ftStatus)
        {
            if (ftStatus != FTDI.FT_STATUS.FT_OK) return;

            if (!_InitialConfigAfterOpen()) return;

            _connected = true;

            if (_connectedChangeCallback != null) _connectedChangeCallback(_connected);
        }

        public void OpenChannelByDevice(DeviceListEntry device, uint channel = 0)
        {
            Close();

            if (!_connected)
            {
                FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

                if (device.LocIds.Count > channel)
                {
                    // OpenChannelBySerialNumber by location
                    ftStatus = _ftdiDevice.OpenByLocation(device.LocIds[(int)channel]);
                }

                _PostOpen(ftStatus);
            }
        }

        public void OpenChannelBySerialNumber(String serialNumber, uint channel = 0)
        {
            Close();

            if (!_connected)
            {
                FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

                // A + 0 = A
                // A + 1 = B
                // B + 0 = B

                String channelSerialNumber = serialNumber.Substring(0, serialNumber.Length - 1);

                Char key = serialNumber[serialNumber.Length - 1];
                key += (char) channel;

                channelSerialNumber += key;

                ftStatus = _ftdiDevice.OpenBySerialNumber(channelSerialNumber);

                _PostOpen(ftStatus);
            }
        }

        public void Close()
        {
            if (_connected)
            {
                _ftdiDevice.Close();
            }

            _connected = false;

            if (_connectedChangeCallback != null) _connectedChangeCallback(_connected);
        }

    }
}
