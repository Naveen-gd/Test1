using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Device_52294_Lib;
using FtdiLib;
using Extensions;

namespace _52294_Socket_Master
{
    internal partial class MainForm : Form
    {
        private delegate void SafeCallStringDelegate(string text);
        private delegate void SafeCallByteDelegate(byte b);

        private AsynchronousSocketListener _asyncSocketListener;
        private ConnectionTimeout _connectionTimeout;
        private UcanMaster _master;
        private CommandParser _commandParser;

        private int _port = 11002;

        private void _setLabelSocketStateSafe(String text)
        {
            if (label_socket_state.InvokeRequired)
            {
                label_socket_state.Invoke(new SafeCallStringDelegate(_setLabelSocketStateSafe), new object[] { text });
            }
            else
            {
                label_socket_state.Text = text;
            }
        }

        private void _setLabelFtdiStateSafe(String text)
        {
            if (label_ftdi_state.InvokeRequired)
            {
                label_ftdi_state.Invoke(new SafeCallStringDelegate(_setLabelFtdiStateSafe), new object[] { text });
            }
            else
            {
                label_ftdi_state.Text = text;
            }
        }

        private void _setLabelPortSafe(String text)
        {
            if (label_socket_state.InvokeRequired)
            {
                label_port.Invoke(new SafeCallStringDelegate(_setLabelPortSafe), new object[] { text });
            }
            else
            {
                label_port.Text = text;
            }
        }

        private void _setLabelNodeAddrSafe(byte addr)
        {
            if (label_node_addr.InvokeRequired)
            {
                label_node_addr.Invoke(new SafeCallByteDelegate(_setLabelNodeAddrSafe), new object[] { addr });
            }
            else
            {
                label_node_addr.Text = String.Format("{0:D}", addr);
            }
        }

        private void _setLabelPeriodicSafe(byte enabled)
        {
            if (label_periodic.InvokeRequired)
            {
                label_periodic.Invoke(new SafeCallByteDelegate(_setLabelPeriodicSafe), new object[] { enabled });
            }
            else
            {
                label_periodic.Text = String.Format("{0:D}", enabled);
            }
        }

        private void _setLabelTimeoutSafe(String text)
        {
            if (label_timeout.InvokeRequired)
            {
                label_timeout.Invoke(new SafeCallStringDelegate(_setLabelTimeoutSafe), new object[] { text });
            }
            else
            {
                label_timeout.Text = text;
            }
        }


        private void _socketWaitConnectCallback(int port)
        {
            _setLabelSocketStateSafe("WAITING");
            _setLabelPortSafe(String.Format("{0:D}", port));
        }

        private void _socketConnectedCallback()
        {
            _setLabelSocketStateSafe("CONNECTED");
            _connectionTimeout.Enable();
        }

        private void _socketStoppedCallback()
        {
            //TODO: delegate tracerControl.Stop();
            _connectionTimeout.Disable();
            _connectionTimeout.DefaultTimeout();
            _setLabelSocketStateSafe("STOPPED");
            _setLabelPortSafe("n/a");
            _setLabelTimeoutSafe("n/a");
        }


        private void _timeoutKillConnectionCallback()
        {
            _asyncSocketListener.CloseConnection();
        }

        private void _timeoutEnableChangeCallback(bool enabled)
        {
            if (enabled) _setLabelTimeoutSafe(String.Format("Enabled ({0:D} sec)", _connectionTimeout.TimeoutSeconds));
            else _setLabelTimeoutSafe("Disabled");
        }


        private void _nodeAddrChangeCallback(byte devId, byte addr)
        {
            if (devId == 0)
                _setLabelNodeAddrSafe(addr);
        }

        private void _autoNodeEnabledChangeCallback(bool enabled)
        {
            _setLabelPeriodicSafe((byte)(enabled ? 1 : 0));
        }


        private void _ftdiConnectedChangeCallback(bool connected)
        {
            if (connected) _setLabelFtdiStateSafe("CONNECTED");
            else _setLabelFtdiStateSafe("DISCONNECTED");
        }

        public MainForm()
        {
            InitializeComponent();

            if (DeviceType.IsM52294A) this.Text += " (M52294A)";
            if (DeviceType.IsE52294A) this.Text += " (E52294A)";

            _asyncSocketListener = new AsynchronousSocketListener();
            _connectionTimeout = new ConnectionTimeout();

            _master = new UcanMaster();
            _commandParser = new CommandParser(_asyncSocketListener, _connectionTimeout, _master);

            _asyncSocketListener.waitConnectCallback = _socketWaitConnectCallback;
            _asyncSocketListener.connectedCallback = _socketConnectedCallback;
            _asyncSocketListener.stoppedCallback = _socketStoppedCallback;

            _connectionTimeout.killConnectionCallback = _timeoutKillConnectionCallback;
            _connectionTimeout.enableChangeCallback = _timeoutEnableChangeCallback;

            _master.ucanCommRef.connectedChangeCallback = _ftdiConnectedChangeCallback;

            _master.nodeAddrChangeCallback = _nodeAddrChangeCallback;
            _master.autoNodeEnabledChangeCallback = _autoNodeEnabledChangeCallback;

            // everything is done with one device (more than one devices are not supported at the moment)
            _master.AddDevice();
            _master.SetAutoNodeId(0);

            // tracer
            _master.ucanCommRef.sendBreakCallback = tracerControl1.tracer.NewFrame;
            _master.ucanCommRef.receiveDataCallback = tracerControl1.tracer.FrameData;

            // default
            _asyncSocketListener.StartListening(_port);
            _master.ucanCommRef.Close();
            _master.SetAutoNodeEnabled(false);
            _master.SetDeviceNode(0, 1);
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            _asyncSocketListener.StartListening(_port);
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            _asyncSocketListener.StopListening();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _master.AbortThread();
        }

        private void label_version_Click(object sender, EventArgs e)
        {
            VersionForm vf = new VersionForm();

            string versionString = "";

            versionString += "Version 7:\r\n";
            versionString += "- improved Wakeup Symbol generation\r\n";
            versionString += "\r\n";

            versionString += "Version 6:\r\n";
            versionString += "- corrected FTDI open/close handling\r\n";
            versionString += "- any Open command will always Close a connection first\r\n";
            versionString += "\r\n";

            versionString += "Version 5:\r\n";
            versionString += "- support dual channel devices\r\n";
            versionString += "- removed OpenList command\r\n";
            versionString += "\r\n";

            versionString += "Version 4:\r\n";
            versionString += "- improved connection handling and stability\r\n";
            versionString += "\r\n";

            versionString += "Version 3:\r\n";
            versionString += "- accept only one connection at a time\r\n";
            versionString += "\r\n";

            versionString += "Version 2:\r\n";
            versionString += "- added tracer\r\n";
            versionString += "- display of some internal states\r\n";
            versionString += "\r\n";

            versionString += "Version 1:\r\n";
            versionString += "- initial version\r\n";
            versionString += "\r\n";

            versionString += "(c) EBL";

            vf.SetText(versionString);

            vf.ShowDialog();
        }


    }
}
