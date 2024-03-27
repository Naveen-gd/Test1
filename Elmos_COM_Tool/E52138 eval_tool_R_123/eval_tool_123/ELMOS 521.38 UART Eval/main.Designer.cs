
using System;

namespace ELMOS_521._38_UART_Eval
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.LabToolVersion = new System.Windows.Forms.Label();
            this.labCopyMark = new System.Windows.Forms.Label();
            this.grpUART = new System.Windows.Forms.GroupBox();
            this.butUARTClose = new System.Windows.Forms.Button();
            this.butUARTOpen = new System.Windows.Forms.Button();
            this.comUARTCOMPort = new System.Windows.Forms.ComboBox();
            this.cmbUARTBaudrate = new System.Windows.Forms.ComboBox();
            this.cmbAutoAnim = new System.Windows.Forms.ComboBox();
            this.grpAutoAnim = new System.Windows.Forms.GroupBox();
            this.chkAnimStart = new System.Windows.Forms.CheckBox();
            this.numSpeedInPercVal = new System.Windows.Forms.NumericUpDown();
            this.butAnimLoad = new System.Windows.Forms.Button();
            this.chkSpeedInPerc = new System.Windows.Forms.CheckBox();
            this.grpDataLink = new System.Windows.Forms.GroupBox();
            this.cmbDLHeaderType = new System.Windows.Forms.ComboBox();
            this.grpAutomatic = new System.Windows.Forms.GroupBox();
            this.numAUTOSendPWM = new System.Windows.Forms.NumericUpDown();
            this.chkAUTOStatusUpdates = new System.Windows.Forms.CheckBox();
            this.chkAUTOSendPWM = new System.Windows.Forms.CheckBox();
            this.labAddDevice = new System.Windows.Forms.Label();
            this.tabDevices = new System.Windows.Forms.TabControl();
            this.tabNoDevice = new System.Windows.Forms.TabPage();
            this.labNoDeviceInfo = new System.Windows.Forms.Label();
            this.grpAnimation = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.butAutoAddressing2 = new System.Windows.Forms.Button();
            this.chkAutoAnimRun = new System.Windows.Forms.CheckBox();
            this.MenMainStrip = new System.Windows.Forms.MenuStrip();
            this.MenItmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmNewDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmRemoveDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.MenSepFile = new System.Windows.Forms.ToolStripSeparator();
            this.MenItmOpenSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmSaveSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmSaveSetupAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenSepFile2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenItmFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmTransport = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmTransportOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmTransportClose = new System.Windows.Forms.ToolStripMenuItem();
            this.MenSepTransport = new System.Windows.Forms.ToolStripSeparator();
            this.MenItmBaudRate = new System.Windows.Forms.ToolStripMenuItem();
            this.kBaudToolStripMenuItem57 = new System.Windows.Forms.ToolStripMenuItem();
            this.kBaudToolStripMenuItem460 = new System.Windows.Forms.ToolStripMenuItem();
            this.kBaudToolStripMenuItem500 = new System.Windows.Forms.ToolStripMenuItem();
            this.kBaudToolStripMenuItem921 = new System.Windows.Forms.ToolStripMenuItem();
            this.kBaudToolStripMenuItem1M = new System.Windows.Forms.ToolStripMenuItem();
            this.kBaudToolStripMenuItem2M = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmPort = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmDataLink = new System.Windows.Forms.ToolStripMenuItem();
            this.byteHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byteHeaderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmAnimations = new System.Windows.Forms.ToolStripMenuItem();
            this.MenItmAnimationsLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenItmAnimationsRun = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grpAdapter = new System.Windows.Forms.GroupBox();
            this.grpExpertMode = new System.Windows.Forms.GroupBox();
            this.btnExpert = new System.Windows.Forms.Button();
            this.txtAuthenticateExpert = new System.Windows.Forms.TextBox();
            this.cmbAdapter = new System.Windows.Forms.ComboBox();
            this.grpUART.SuspendLayout();
            this.grpAutoAnim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeedInPercVal)).BeginInit();
            this.grpDataLink.SuspendLayout();
            this.grpAutomatic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAUTOSendPWM)).BeginInit();
            this.tabDevices.SuspendLayout();
            this.tabNoDevice.SuspendLayout();
            this.grpAnimation.SuspendLayout();
            this.MenMainStrip.SuspendLayout();
            this.grpAdapter.SuspendLayout();
            this.grpExpertMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabToolVersion
            // 
            this.LabToolVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabToolVersion.Location = new System.Drawing.Point(667, 127);
            this.LabToolVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabToolVersion.Name = "LabToolVersion";
            this.LabToolVersion.Size = new System.Drawing.Size(208, 23);
            this.LabToolVersion.TabIndex = 3;
            this.LabToolVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labCopyMark
            // 
            this.labCopyMark.AutoSize = true;
            this.labCopyMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCopyMark.Location = new System.Drawing.Point(895, 939);
            this.labCopyMark.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labCopyMark.Name = "labCopyMark";
            this.labCopyMark.Size = new System.Drawing.Size(231, 18);
            this.labCopyMark.TabIndex = 3;
            this.labCopyMark.Text = "© 2022 Elmos Semiconductor SE";
            // 
            // grpUART
            // 
            this.grpUART.Controls.Add(this.butUARTClose);
            this.grpUART.Controls.Add(this.butUARTOpen);
            this.grpUART.Controls.Add(this.comUARTCOMPort);
            this.grpUART.Controls.Add(this.cmbUARTBaudrate);
            this.grpUART.Location = new System.Drawing.Point(227, 28);
            this.grpUART.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpUART.Name = "grpUART";
            this.grpUART.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpUART.Size = new System.Drawing.Size(281, 95);
            this.grpUART.TabIndex = 3;
            this.grpUART.TabStop = false;
            this.grpUART.Text = "UART";
            // 
            // butUARTClose
            // 
            this.butUARTClose.Enabled = false;
            this.butUARTClose.Location = new System.Drawing.Point(156, 54);
            this.butUARTClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butUARTClose.Name = "butUARTClose";
            this.butUARTClose.Size = new System.Drawing.Size(113, 28);
            this.butUARTClose.TabIndex = 4;
            this.butUARTClose.Text = "CLOSE";
            this.butUARTClose.UseVisualStyleBackColor = true;
            this.butUARTClose.Click += new System.EventHandler(this.butUARTClose_Click);
            // 
            // butUARTOpen
            // 
            this.butUARTOpen.Location = new System.Drawing.Point(156, 21);
            this.butUARTOpen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butUARTOpen.Name = "butUARTOpen";
            this.butUARTOpen.Size = new System.Drawing.Size(113, 28);
            this.butUARTOpen.TabIndex = 4;
            this.butUARTOpen.Text = "OPEN";
            this.butUARTOpen.UseVisualStyleBackColor = true;
            this.butUARTOpen.Click += new System.EventHandler(this.butUARTOpen_Click);
            // 
            // comUARTCOMPort
            // 
            this.comUARTCOMPort.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comUARTCOMPort.FormattingEnabled = true;
            this.comUARTCOMPort.Location = new System.Drawing.Point(12, 58);
            this.comUARTCOMPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comUARTCOMPort.Name = "comUARTCOMPort";
            this.comUARTCOMPort.Size = new System.Drawing.Size(135, 22);
            this.comUARTCOMPort.TabIndex = 4;
            this.comUARTCOMPort.DropDown += new System.EventHandler(this.comUARTCOMPort_DropDown);
            this.comUARTCOMPort.SelectedIndexChanged += new System.EventHandler(this.comUARTCOMPort_TextUpdate);
            this.comUARTCOMPort.TextUpdate += new System.EventHandler(this.comUARTCOMPort_TextUpdate);
            // 
            // cmbUARTBaudrate
            // 
            this.cmbUARTBaudrate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUARTBaudrate.Items.AddRange(new object[] {
            " 57.6 kBaud",
            "460.8 kBaud",
            "500 kBaud",
            "921.6 kBaud",
            "1 MBaud",
            "2 MBaud"});
            this.cmbUARTBaudrate.Location = new System.Drawing.Point(12, 23);
            this.cmbUARTBaudrate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbUARTBaudrate.Name = "cmbUARTBaudrate";
            this.cmbUARTBaudrate.Size = new System.Drawing.Size(135, 22);
            this.cmbUARTBaudrate.TabIndex = 4;
            this.cmbUARTBaudrate.Text = "1 MBaud";
            this.cmbUARTBaudrate.SelectedIndexChanged += new System.EventHandler(this.cmbUARTBaudrate_SelectedIndexChanged);
            // 
            // cmbAutoAnim
            // 
            this.cmbAutoAnim.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAutoAnim.FormattingEnabled = true;
            this.cmbAutoAnim.Items.AddRange(new object[] {
            "Full Color Wheel",
            "Moving Light",
            "Blinker"});
            this.cmbAutoAnim.Location = new System.Drawing.Point(12, 23);
            this.cmbAutoAnim.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbAutoAnim.Name = "cmbAutoAnim";
            this.cmbAutoAnim.Size = new System.Drawing.Size(180, 22);
            this.cmbAutoAnim.TabIndex = 5;
            this.cmbAutoAnim.Text = "<select>";
            this.cmbAutoAnim.DropDown += new System.EventHandler(this.cmbAutoAnim_DropDown);
            this.cmbAutoAnim.SelectedIndexChanged += new System.EventHandler(this.cmbAutoAnim_SelectedIndexChanged);
            // 
            // grpAutoAnim
            // 
            this.grpAutoAnim.Controls.Add(this.chkAnimStart);
            this.grpAutoAnim.Controls.Add(this.numSpeedInPercVal);
            this.grpAutoAnim.Controls.Add(this.butAnimLoad);
            this.grpAutoAnim.Controls.Add(this.chkSpeedInPerc);
            this.grpAutoAnim.Controls.Add(this.cmbAutoAnim);
            this.grpAutoAnim.Location = new System.Drawing.Point(227, 121);
            this.grpAutoAnim.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAutoAnim.Name = "grpAutoAnim";
            this.grpAutoAnim.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAutoAnim.Size = new System.Drawing.Size(432, 89);
            this.grpAutoAnim.TabIndex = 4;
            this.grpAutoAnim.TabStop = false;
            this.grpAutoAnim.Text = "Animations";
            // 
            // chkAnimStart
            // 
            this.chkAnimStart.AutoSize = true;
            this.chkAnimStart.Enabled = false;
            this.chkAnimStart.Location = new System.Drawing.Point(211, 26);
            this.chkAnimStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAnimStart.Name = "chkAnimStart";
            this.chkAnimStart.Size = new System.Drawing.Size(115, 20);
            this.chkAnimStart.TabIndex = 4;
            this.chkAnimStart.Text = "Run Animation";
            this.chkAnimStart.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAnimStart.UseVisualStyleBackColor = true;
            this.chkAnimStart.CheckedChanged += new System.EventHandler(this.chkAnimStart_CheckedChanged);
            // 
            // numSpeedInPercVal
            // 
            this.numSpeedInPercVal.Location = new System.Drawing.Point(331, 54);
            this.numSpeedInPercVal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numSpeedInPercVal.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSpeedInPercVal.Name = "numSpeedInPercVal";
            this.numSpeedInPercVal.Size = new System.Drawing.Size(76, 22);
            this.numSpeedInPercVal.TabIndex = 7;
            this.numSpeedInPercVal.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // butAnimLoad
            // 
            this.butAnimLoad.Location = new System.Drawing.Point(12, 52);
            this.butAnimLoad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butAnimLoad.Name = "butAnimLoad";
            this.butAnimLoad.Size = new System.Drawing.Size(181, 28);
            this.butAnimLoad.TabIndex = 3;
            this.butAnimLoad.Text = "LOAD CUSTOM";
            this.butAnimLoad.UseVisualStyleBackColor = true;
            this.butAnimLoad.Click += new System.EventHandler(this.butAnimLoad_Click);
            // 
            // chkSpeedInPerc
            // 
            this.chkSpeedInPerc.Location = new System.Drawing.Point(212, 53);
            this.chkSpeedInPerc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkSpeedInPerc.Name = "chkSpeedInPerc";
            this.chkSpeedInPerc.Size = new System.Drawing.Size(111, 30);
            this.chkSpeedInPerc.TabIndex = 6;
            this.chkSpeedInPerc.Text = "Speed in %";
            // 
            // grpDataLink
            // 
            this.grpDataLink.Controls.Add(this.cmbDLHeaderType);
            this.grpDataLink.Location = new System.Drawing.Point(516, 28);
            this.grpDataLink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDataLink.Name = "grpDataLink";
            this.grpDataLink.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDataLink.Size = new System.Drawing.Size(143, 95);
            this.grpDataLink.TabIndex = 4;
            this.grpDataLink.TabStop = false;
            this.grpDataLink.Text = "Data Link";
            // 
            // cmbDLHeaderType
            // 
            this.cmbDLHeaderType.Enabled = false;
            this.cmbDLHeaderType.FormattingEnabled = true;
            this.cmbDLHeaderType.Items.AddRange(new object[] {
            "3 Byte Header",
            "4 Byte Header"});
            this.cmbDLHeaderType.Location = new System.Drawing.Point(12, 23);
            this.cmbDLHeaderType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDLHeaderType.Name = "cmbDLHeaderType";
            this.cmbDLHeaderType.Size = new System.Drawing.Size(119, 24);
            this.cmbDLHeaderType.TabIndex = 5;
            this.cmbDLHeaderType.Text = "3 Byte Header";
            // 
            // grpAutomatic
            // 
            this.grpAutomatic.Controls.Add(this.numAUTOSendPWM);
            this.grpAutomatic.Controls.Add(this.chkAUTOStatusUpdates);
            this.grpAutomatic.Controls.Add(this.chkAUTOSendPWM);
            this.grpAutomatic.Location = new System.Drawing.Point(667, 28);
            this.grpAutomatic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAutomatic.Name = "grpAutomatic";
            this.grpAutomatic.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAutomatic.Size = new System.Drawing.Size(271, 95);
            this.grpAutomatic.TabIndex = 5;
            this.grpAutomatic.TabStop = false;
            this.grpAutomatic.Text = "Automatic";
            // 
            // numAUTOSendPWM
            // 
            this.numAUTOSendPWM.Location = new System.Drawing.Point(152, 21);
            this.numAUTOSendPWM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numAUTOSendPWM.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAUTOSendPWM.Name = "numAUTOSendPWM";
            this.numAUTOSendPWM.Size = new System.Drawing.Size(76, 22);
            this.numAUTOSendPWM.TabIndex = 2;
            this.numAUTOSendPWM.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numAUTOSendPWM.ValueChanged += new System.EventHandler(this.numAUTOSendPWM_ValueChanged);
            // 
            // chkAUTOStatusUpdates
            // 
            this.chkAUTOStatusUpdates.AutoSize = true;
            this.chkAUTOStatusUpdates.Location = new System.Drawing.Point(12, 57);
            this.chkAUTOStatusUpdates.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAUTOStatusUpdates.Name = "chkAUTOStatusUpdates";
            this.chkAUTOStatusUpdates.Size = new System.Drawing.Size(118, 20);
            this.chkAUTOStatusUpdates.TabIndex = 1;
            this.chkAUTOStatusUpdates.Text = "Status updates";
            this.chkAUTOStatusUpdates.UseVisualStyleBackColor = true;
            this.chkAUTOStatusUpdates.CheckedChanged += new System.EventHandler(this.chkAUTOStatusUpdates_CheckedChanged);
            // 
            // chkAUTOSendPWM
            // 
            this.chkAUTOSendPWM.AutoSize = true;
            this.chkAUTOSendPWM.Location = new System.Drawing.Point(12, 23);
            this.chkAUTOSendPWM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAUTOSendPWM.Name = "chkAUTOSendPWM";
            this.chkAUTOSendPWM.Size = new System.Drawing.Size(214, 20);
            this.chkAUTOSendPWM.TabIndex = 0;
            this.chkAUTOSendPWM.Text = "Send PWM each                      ms";
            this.chkAUTOSendPWM.UseVisualStyleBackColor = true;
            this.chkAUTOSendPWM.CheckedChanged += new System.EventHandler(this.chkAUTOSendPWM_CheckedChanged);
            // 
            // labAddDevice
            // 
            this.labAddDevice.BackColor = System.Drawing.Color.Transparent;
            this.labAddDevice.Font = new System.Drawing.Font("Wingdings 2", 9F);
            this.labAddDevice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labAddDevice.Location = new System.Drawing.Point(100, 214);
            this.labAddDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labAddDevice.Name = "labAddDevice";
            this.labAddDevice.Size = new System.Drawing.Size(21, 23);
            this.labAddDevice.TabIndex = 8;
            this.labAddDevice.Text = "Ê";
            this.labAddDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labAddDevice.Click += new System.EventHandler(this.labAddDevice_Click);
            // 
            // tabDevices
            // 
            this.tabDevices.Controls.Add(this.tabNoDevice);
            this.tabDevices.Enabled = false;
            this.tabDevices.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabDevices.Location = new System.Drawing.Point(11, 213);
            this.tabDevices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabDevices.Name = "tabDevices";
            this.tabDevices.SelectedIndex = 0;
            this.tabDevices.Size = new System.Drawing.Size(1143, 722);
            this.tabDevices.TabIndex = 1;
            this.tabDevices.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabDevices_Selecting);
            this.tabDevices.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tabDevices_ControlAdded);
            this.tabDevices.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.tabDevices_ControlRemoved);
            // 
            // tabNoDevice
            // 
            this.tabNoDevice.Controls.Add(this.labNoDeviceInfo);
            this.tabNoDevice.Location = new System.Drawing.Point(4, 25);
            this.tabNoDevice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabNoDevice.Name = "tabNoDevice";
            this.tabNoDevice.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabNoDevice.Size = new System.Drawing.Size(1135, 693);
            this.tabNoDevice.TabIndex = 0;
            this.tabNoDevice.Text = "No Device";
            this.tabNoDevice.UseVisualStyleBackColor = true;
            // 
            // labNoDeviceInfo
            // 
            this.labNoDeviceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labNoDeviceInfo.Location = new System.Drawing.Point(4, 4);
            this.labNoDeviceInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labNoDeviceInfo.Name = "labNoDeviceInfo";
            this.labNoDeviceInfo.Size = new System.Drawing.Size(1127, 685);
            this.labNoDeviceInfo.TabIndex = 0;
            this.labNoDeviceInfo.Text = "Press plus \'+\' button to add device";
            this.labNoDeviceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpAnimation
            // 
            this.grpAnimation.Controls.Add(this.button1);
            this.grpAnimation.Controls.Add(this.butAutoAddressing2);
            this.grpAnimation.Location = new System.Drawing.Point(945, 28);
            this.grpAnimation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAnimation.Name = "grpAnimation";
            this.grpAnimation.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAnimation.Size = new System.Drawing.Size(203, 95);
            this.grpAnimation.TabIndex = 12;
            this.grpAnimation.TabStop = false;
            this.grpAnimation.Text = "Device Management";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 54);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "ADD DEVICE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.labAddDevice_Click);
            // 
            // butAutoAddressing2
            // 
            this.butAutoAddressing2.Location = new System.Drawing.Point(12, 21);
            this.butAutoAddressing2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butAutoAddressing2.Name = "butAutoAddressing2";
            this.butAutoAddressing2.Size = new System.Drawing.Size(179, 28);
            this.butAutoAddressing2.TabIndex = 4;
            this.butAutoAddressing2.Text = "AUTO ADDRESSING";
            this.butAutoAddressing2.UseVisualStyleBackColor = true;
            this.butAutoAddressing2.Click += new System.EventHandler(this.butAutoAddressing_Click);
            // 
            // chkAutoAnimRun
            // 
            this.chkAutoAnimRun.AutoSize = true;
            this.chkAutoAnimRun.Location = new System.Drawing.Point(254, 11);
            this.chkAutoAnimRun.Name = "chkAutoAnimRun";
            this.chkAutoAnimRun.Size = new System.Drawing.Size(97, 17);
            this.chkAutoAnimRun.TabIndex = 6;
            this.chkAutoAnimRun.Text = "Run Automation";
            this.chkAutoAnimRun.UseVisualStyleBackColor = true;
            // 
            // MenMainStrip
            // 
            this.MenMainStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenMainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenItmFile,
            this.MenItmTransport,
            this.MenItmAnimations});
            this.MenMainStrip.Location = new System.Drawing.Point(0, 0);
            this.MenMainStrip.Name = "MenMainStrip";
            this.MenMainStrip.Size = new System.Drawing.Size(1165, 28);
            this.MenMainStrip.TabIndex = 13;
            this.MenMainStrip.Text = "MenMainStrip";
            // 
            // MenItmFile
            // 
            this.MenItmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenItmNewDevice,
            this.MenItmRemoveDevice,
            this.MenSepFile,
            this.MenItmOpenSetup,
            this.MenItmSaveSetup,
            this.MenItmSaveSetupAs,
            this.MenSepFile2,
            this.MenItmFileClose});
            this.MenItmFile.Name = "MenItmFile";
            this.MenItmFile.Size = new System.Drawing.Size(46, 24);
            this.MenItmFile.Text = "File";
            // 
            // MenItmNewDevice
            // 
            this.MenItmNewDevice.Name = "MenItmNewDevice";
            this.MenItmNewDevice.Size = new System.Drawing.Size(193, 26);
            this.MenItmNewDevice.Text = "New device";
            this.MenItmNewDevice.Click += new System.EventHandler(this.labAddDevice_Click);
            // 
            // MenItmRemoveDevice
            // 
            this.MenItmRemoveDevice.Name = "MenItmRemoveDevice";
            this.MenItmRemoveDevice.Size = new System.Drawing.Size(193, 26);
            this.MenItmRemoveDevice.Text = "Remove device";
            this.MenItmRemoveDevice.Click += new System.EventHandler(this.removeCurrentDeviceToolStripMenuItem_Click);
            // 
            // MenSepFile
            // 
            this.MenSepFile.Name = "MenSepFile";
            this.MenSepFile.Size = new System.Drawing.Size(190, 6);
            // 
            // MenItmOpenSetup
            // 
            this.MenItmOpenSetup.Name = "MenItmOpenSetup";
            this.MenItmOpenSetup.Size = new System.Drawing.Size(193, 26);
            this.MenItmOpenSetup.Text = "Open setup";
            this.MenItmOpenSetup.Click += new System.EventHandler(this.MenItmOpenSetup_Click);
            // 
            // MenItmSaveSetup
            // 
            this.MenItmSaveSetup.Enabled = false;
            this.MenItmSaveSetup.Name = "MenItmSaveSetup";
            this.MenItmSaveSetup.Size = new System.Drawing.Size(193, 26);
            this.MenItmSaveSetup.Text = "Save setup";
            this.MenItmSaveSetup.Click += new System.EventHandler(this.MenItmSaveSetup_Click);
            // 
            // MenItmSaveSetupAs
            // 
            this.MenItmSaveSetupAs.Name = "MenItmSaveSetupAs";
            this.MenItmSaveSetupAs.Size = new System.Drawing.Size(193, 26);
            this.MenItmSaveSetupAs.Text = "Save setup as..";
            this.MenItmSaveSetupAs.Click += new System.EventHandler(this.MenItmSaveSetupAs_Click);
            // 
            // MenSepFile2
            // 
            this.MenSepFile2.Name = "MenSepFile2";
            this.MenSepFile2.Size = new System.Drawing.Size(190, 6);
            // 
            // MenItmFileClose
            // 
            this.MenItmFileClose.Name = "MenItmFileClose";
            this.MenItmFileClose.Size = new System.Drawing.Size(193, 26);
            this.MenItmFileClose.Text = "Close";
            this.MenItmFileClose.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // MenItmTransport
            // 
            this.MenItmTransport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenItmTransportOpen,
            this.MenItmTransportClose,
            this.MenSepTransport,
            this.MenItmBaudRate,
            this.MenItmPort,
            this.MenItmDataLink});
            this.MenItmTransport.Name = "MenItmTransport";
            this.MenItmTransport.Size = new System.Drawing.Size(85, 24);
            this.MenItmTransport.Text = "Transport";
            this.MenItmTransport.Click += new System.EventHandler(this.comUARTCOMPort_DropDown);
            // 
            // MenItmTransportOpen
            // 
            this.MenItmTransportOpen.Name = "MenItmTransportOpen";
            this.MenItmTransportOpen.Size = new System.Drawing.Size(156, 26);
            this.MenItmTransportOpen.Text = "Open";
            this.MenItmTransportOpen.Click += new System.EventHandler(this.butUARTOpen_Click);
            // 
            // MenItmTransportClose
            // 
            this.MenItmTransportClose.Enabled = false;
            this.MenItmTransportClose.Name = "MenItmTransportClose";
            this.MenItmTransportClose.Size = new System.Drawing.Size(156, 26);
            this.MenItmTransportClose.Text = "Close";
            this.MenItmTransportClose.Click += new System.EventHandler(this.butUARTClose_Click);
            // 
            // MenSepTransport
            // 
            this.MenSepTransport.Name = "MenSepTransport";
            this.MenSepTransport.Size = new System.Drawing.Size(153, 6);
            // 
            // MenItmBaudRate
            // 
            this.MenItmBaudRate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kBaudToolStripMenuItem57,
            this.kBaudToolStripMenuItem460,
            this.kBaudToolStripMenuItem500,
            this.kBaudToolStripMenuItem921,
            this.kBaudToolStripMenuItem1M,
            this.kBaudToolStripMenuItem2M});
            this.MenItmBaudRate.Name = "MenItmBaudRate";
            this.MenItmBaudRate.Size = new System.Drawing.Size(156, 26);
            this.MenItmBaudRate.Text = "Baud rate";
            // 
            // kBaudToolStripMenuItem57
            // 
            this.kBaudToolStripMenuItem57.Name = "kBaudToolStripMenuItem57";
            this.kBaudToolStripMenuItem57.Size = new System.Drawing.Size(172, 26);
            this.kBaudToolStripMenuItem57.Text = "57.6 kBaud";
            this.kBaudToolStripMenuItem57.Click += new System.EventHandler(this.cmbUARTBaudrate_SelectedIndexChanged);
            // 
            // kBaudToolStripMenuItem460
            // 
            this.kBaudToolStripMenuItem460.Name = "kBaudToolStripMenuItem460";
            this.kBaudToolStripMenuItem460.Size = new System.Drawing.Size(172, 26);
            this.kBaudToolStripMenuItem460.Text = "460.8 kBaud";
            this.kBaudToolStripMenuItem460.Click += new System.EventHandler(this.cmbUARTBaudrate_SelectedIndexChanged);
            // 
            // kBaudToolStripMenuItem500
            // 
            this.kBaudToolStripMenuItem500.Name = "kBaudToolStripMenuItem500";
            this.kBaudToolStripMenuItem500.Size = new System.Drawing.Size(172, 26);
            this.kBaudToolStripMenuItem500.Text = "500 kBaud";
            this.kBaudToolStripMenuItem500.Click += new System.EventHandler(this.cmbUARTBaudrate_SelectedIndexChanged);
            // 
            // kBaudToolStripMenuItem921
            // 
            this.kBaudToolStripMenuItem921.Name = "kBaudToolStripMenuItem921";
            this.kBaudToolStripMenuItem921.Size = new System.Drawing.Size(172, 26);
            this.kBaudToolStripMenuItem921.Text = "921.6 kBaud";
            this.kBaudToolStripMenuItem921.Click += new System.EventHandler(this.cmbUARTBaudrate_SelectedIndexChanged);
            // 
            // kBaudToolStripMenuItem1M
            // 
            this.kBaudToolStripMenuItem1M.Checked = true;
            this.kBaudToolStripMenuItem1M.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kBaudToolStripMenuItem1M.Name = "kBaudToolStripMenuItem1M";
            this.kBaudToolStripMenuItem1M.Size = new System.Drawing.Size(172, 26);
            this.kBaudToolStripMenuItem1M.Text = "1 MBaud";
            this.kBaudToolStripMenuItem1M.Click += new System.EventHandler(this.cmbUARTBaudrate_SelectedIndexChanged);
            // 
            // kBaudToolStripMenuItem2M
            // 
            this.kBaudToolStripMenuItem2M.Name = "kBaudToolStripMenuItem2M";
            this.kBaudToolStripMenuItem2M.Size = new System.Drawing.Size(172, 26);
            this.kBaudToolStripMenuItem2M.Text = "2 MBaud";
            this.kBaudToolStripMenuItem2M.Click += new System.EventHandler(this.cmbUARTBaudrate_SelectedIndexChanged);
            // 
            // MenItmPort
            // 
            this.MenItmPort.Name = "MenItmPort";
            this.MenItmPort.Size = new System.Drawing.Size(156, 26);
            this.MenItmPort.Text = "Port";
            // 
            // MenItmDataLink
            // 
            this.MenItmDataLink.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byteHeaderToolStripMenuItem,
            this.byteHeaderToolStripMenuItem1});
            this.MenItmDataLink.Name = "MenItmDataLink";
            this.MenItmDataLink.Size = new System.Drawing.Size(156, 26);
            this.MenItmDataLink.Text = "Data link";
            // 
            // byteHeaderToolStripMenuItem
            // 
            this.byteHeaderToolStripMenuItem.Checked = true;
            this.byteHeaderToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.byteHeaderToolStripMenuItem.Name = "byteHeaderToolStripMenuItem";
            this.byteHeaderToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.byteHeaderToolStripMenuItem.Text = "3 Byte Header";
            // 
            // byteHeaderToolStripMenuItem1
            // 
            this.byteHeaderToolStripMenuItem1.Enabled = false;
            this.byteHeaderToolStripMenuItem1.Name = "byteHeaderToolStripMenuItem1";
            this.byteHeaderToolStripMenuItem1.Size = new System.Drawing.Size(186, 26);
            this.byteHeaderToolStripMenuItem1.Text = "4 Byte Header";
            // 
            // MenItmAnimations
            // 
            this.MenItmAnimations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenItmAnimationsLoad,
            this.toolStripSeparator1,
            this.MenItmAnimationsRun});
            this.MenItmAnimations.Name = "MenItmAnimations";
            this.MenItmAnimations.Size = new System.Drawing.Size(98, 24);
            this.MenItmAnimations.Text = "Animations";
            this.MenItmAnimations.Click += new System.EventHandler(this.MenItmAnimations_Click);
            // 
            // MenItmAnimationsLoad
            // 
            this.MenItmAnimationsLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFromFileToolStripMenuItem});
            this.MenItmAnimationsLoad.Name = "MenItmAnimationsLoad";
            this.MenItmAnimationsLoad.Size = new System.Drawing.Size(190, 26);
            this.MenItmAnimationsLoad.Text = "Load";
            // 
            // loadFromFileToolStripMenuItem
            // 
            this.loadFromFileToolStripMenuItem.Name = "loadFromFileToolStripMenuItem";
            this.loadFromFileToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.loadFromFileToolStripMenuItem.Text = "Load from file";
            this.loadFromFileToolStripMenuItem.Click += new System.EventHandler(this.butAnimLoad_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // MenItmAnimationsRun
            // 
            this.MenItmAnimationsRun.CheckOnClick = true;
            this.MenItmAnimationsRun.Name = "MenItmAnimationsRun";
            this.MenItmAnimationsRun.Size = new System.Drawing.Size(190, 26);
            this.MenItmAnimationsRun.Text = "Run Animation";
            this.MenItmAnimationsRun.CheckedChanged += new System.EventHandler(this.chkAnimStart_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // grpAdapter
            // 
            this.grpAdapter.Controls.Add(this.grpExpertMode);
            this.grpAdapter.Controls.Add(this.cmbAdapter);
            this.grpAdapter.Location = new System.Drawing.Point(19, 30);
            this.grpAdapter.Name = "grpAdapter";
            this.grpAdapter.Size = new System.Drawing.Size(200, 175);
            this.grpAdapter.TabIndex = 15;
            this.grpAdapter.TabStop = false;
            this.grpAdapter.Text = "Adapter";
            // 
            // grpExpertMode
            // 
            this.grpExpertMode.Controls.Add(this.btnExpert);
            this.grpExpertMode.Controls.Add(this.txtAuthenticateExpert);
            this.grpExpertMode.Location = new System.Drawing.Point(7, 76);
            this.grpExpertMode.Name = "grpExpertMode";
            this.grpExpertMode.Size = new System.Drawing.Size(187, 91);
            this.grpExpertMode.TabIndex = 1;
            this.grpExpertMode.TabStop = false;
            this.grpExpertMode.Text = "Expert Mode";
            // 
            // btnExpert
            // 
            this.btnExpert.Location = new System.Drawing.Point(31, 59);
            this.btnExpert.Name = "btnExpert";
            this.btnExpert.Size = new System.Drawing.Size(89, 26);
            this.btnExpert.TabIndex = 1;
            this.btnExpert.Text = "Expert";
            this.btnExpert.UseVisualStyleBackColor = true;
            this.btnExpert.Click += new System.EventHandler(this.btnExpert_Click);
            // 
            // txtAuthenticateExpert
            // 
            this.txtAuthenticateExpert.Location = new System.Drawing.Point(28, 28);
            this.txtAuthenticateExpert.Name = "txtAuthenticateExpert";
            this.txtAuthenticateExpert.Size = new System.Drawing.Size(100, 22);
            this.txtAuthenticateExpert.TabIndex = 0;
            // 
            // cmbAdapter
            // 
            this.cmbAdapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdapter.FormattingEnabled = true;
            this.cmbAdapter.Items.AddRange(new object[] {
            "DiffUART",
            "COM BOX"});
            this.cmbAdapter.Location = new System.Drawing.Point(7, 32);
            this.cmbAdapter.Name = "cmbAdapter";
            this.cmbAdapter.Size = new System.Drawing.Size(187, 24);
            this.cmbAdapter.TabIndex = 0;
            this.cmbAdapter.SelectedIndexChanged += new System.EventHandler(this.cmbAdapter_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1165, 964);
            this.Controls.Add(this.grpAdapter);
            this.Controls.Add(this.labCopyMark);
            this.Controls.Add(this.LabToolVersion);
            this.Controls.Add(this.grpAnimation);
            this.Controls.Add(this.labAddDevice);
            this.Controls.Add(this.grpAutomatic);
            this.Controls.Add(this.grpDataLink);
            this.Controls.Add(this.grpUART);
            this.Controls.Add(this.tabDevices);
            this.Controls.Add(this.MenMainStrip);
            this.Controls.Add(this.grpAutoAnim);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenMainStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormMain";
            this.Text = "ELMOS 521.38 UART Eval";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.grpUART.ResumeLayout(false);
            this.grpAutoAnim.ResumeLayout(false);
            this.grpAutoAnim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeedInPercVal)).EndInit();
            this.grpDataLink.ResumeLayout(false);
            this.grpAutomatic.ResumeLayout(false);
            this.grpAutomatic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAUTOSendPWM)).EndInit();
            this.tabDevices.ResumeLayout(false);
            this.tabNoDevice.ResumeLayout(false);
            this.grpAnimation.ResumeLayout(false);
            this.MenMainStrip.ResumeLayout(false);
            this.MenMainStrip.PerformLayout();
            this.grpAdapter.ResumeLayout(false);
            this.grpExpertMode.ResumeLayout(false);
            this.grpExpertMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox grpUART;
        private System.Windows.Forms.Button butUARTOpen;
        private System.Windows.Forms.ComboBox cmbUARTBaudrate;
        private System.Windows.Forms.GroupBox grpDataLink;
        private System.Windows.Forms.GroupBox grpAutoAnim;
        private System.Windows.Forms.ComboBox cmbDLHeaderType;
        private System.Windows.Forms.ComboBox cmbAutoAnim;
        private System.Windows.Forms.GroupBox grpAutomatic;
        private System.Windows.Forms.NumericUpDown numAUTOSendPWM;
        private System.Windows.Forms.CheckBox chkAUTOStatusUpdates;
        private System.Windows.Forms.CheckBox chkAUTOSendPWM;
        private System.Windows.Forms.Label labAddDevice;
        private System.Windows.Forms.TabControl tabDevices;
        private System.Windows.Forms.ComboBox comUARTCOMPort;
        private System.Windows.Forms.Button butUARTClose;
        private System.Windows.Forms.TabPage tabNoDevice;
        private System.Windows.Forms.Label labNoDeviceInfo;
        private System.Windows.Forms.GroupBox grpAnimation;
        private System.Windows.Forms.CheckBox chkAnimStart;
        private System.Windows.Forms.CheckBox chkAutoAnimRun;
        private System.Windows.Forms.Button butAnimLoad;
        private System.Windows.Forms.MenuStrip MenMainStrip;
        private System.Windows.Forms.ToolStripMenuItem MenItmFile;
        private System.Windows.Forms.ToolStripMenuItem MenItmNewDevice;
        private System.Windows.Forms.ToolStripMenuItem MenItmRemoveDevice;
        private System.Windows.Forms.ToolStripSeparator MenSepFile;
        private System.Windows.Forms.ToolStripMenuItem MenItmFileClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenItmTransport;
        private System.Windows.Forms.ToolStripMenuItem MenItmBaudRate;
        private System.Windows.Forms.ToolStripMenuItem kBaudToolStripMenuItem57;
        private System.Windows.Forms.ToolStripMenuItem kBaudToolStripMenuItem460;
        private System.Windows.Forms.ToolStripMenuItem kBaudToolStripMenuItem500;
        private System.Windows.Forms.ToolStripMenuItem kBaudToolStripMenuItem921;
        private System.Windows.Forms.ToolStripMenuItem kBaudToolStripMenuItem1M;
        private System.Windows.Forms.ToolStripMenuItem kBaudToolStripMenuItem2M;
        private System.Windows.Forms.ToolStripMenuItem MenItmAutoAnim;
        private System.Windows.Forms.ToolStripMenuItem autoAnimMenuItemFullColorWheel;
        private System.Windows.Forms.ToolStripMenuItem autoAnimMenuItemWhiperBlinker;
        private System.Windows.Forms.ToolStripMenuItem autoAnimMenuItemBlinker;
        private System.Windows.Forms.ToolStripSeparator MenSepTransport;
        private System.Windows.Forms.ToolStripMenuItem MenItmTransportOpen;
        private System.Windows.Forms.ToolStripMenuItem MenItmTransportClose;
        private System.Windows.Forms.ToolStripMenuItem MenItmDataLink;
        private System.Windows.Forms.ToolStripMenuItem byteHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byteHeaderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenItmPort;
        private System.Windows.Forms.ToolStripMenuItem MenItmAnimations;
        private System.Windows.Forms.ToolStripMenuItem MenItmAnimationsLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenItmAnimationsRun;
        private System.Windows.Forms.ToolStripMenuItem loadFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenItmOpenSetup;
        private System.Windows.Forms.ToolStripMenuItem MenItmSaveSetup;
        private System.Windows.Forms.ToolStripMenuItem MenItmSaveSetupAs;
        private System.Windows.Forms.ToolStripSeparator MenSepFile2;
        private System.Windows.Forms.Label LabToolVersion;
        private System.Windows.Forms.Label labCopyMark;
        private System.Windows.Forms.NumericUpDown numSpeedInPercVal;
        private System.Windows.Forms.CheckBox chkSpeedInPerc;
        private System.Windows.Forms.Button butAutoAddressing2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox grpAdapter;
        private System.Windows.Forms.GroupBox grpExpertMode;
        private System.Windows.Forms.Button btnExpert;
        private System.Windows.Forms.TextBox txtAuthenticateExpert;
        private System.Windows.Forms.ComboBox cmbAdapter;
    }
}

