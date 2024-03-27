using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;

namespace Elmos_COM_Tool
{
    public partial class ComportComm : Form
    {
        static SerialPort serialPort;
        public ComportComm()
        {
            InitializeComponent();

            //ManagementObjectCollection collection;
            //using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub")) collection = searcher.Get();
            //foreach (var device in collection)
            //{
            //    //devices.Add(new USBDeviceInfo(
            //    //(string)device.GetPropertyValue("DeviceID"),
            //    //(string)device.GetPropertyValue("PNPDeviceID"),
            //    //(string)device.GetPropertyValue("Description")
            //    //));
            //    //cmbComPort.Items.Add((string)device.GetPropertyValue("DeviceID"));
            //    cmbComPort.Items.Add((string)device.GetPropertyValue("PNPDeviceID"));
            //}

            //collection.Dispose();
            //foreach (var port in SerialPort.GetPortNames())
            //    cmbComPort.Items.Add(port);
            serialPort = new SerialPort("COM5", 9600);
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        static void btnSend_Click(object sender, EventArgs e)
        {
            //SerialPort.GetPortNames();
            //serialPort = new SerialPort("COM5", 9600); 
            //serialPort.DataReceived += SerialPort_DataReceived;

            try
            {
                // Open the serial port
                serialPort.Open();

                // Write data to the serial port
                string dataToSend = "Hello, World!";
                serialPort.WriteLine(dataToSend);
                Console.WriteLine($"Sent: {dataToSend}");

                // Wait for a response
                Console.WriteLine("Waiting for response...");
                Console.ReadLine(); // Wait for user to press Enter

                // Close the serial port
                serialPort.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read data from the serial port
            string dataReceived = serialPort.ReadLine();
            Console.WriteLine($"Received: {dataReceived}");
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {

        }
    }
}
