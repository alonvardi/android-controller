using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;


namespace windowsController
{
    public partial class Form1 : Form
    {
        private bool isEnabled;
        private static Thread serverThread;
        private static bool isServerRunning;
        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Icon = System.Drawing.SystemIcons.Application;
            notifyIcon1.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void SetVisibleCore(bool value)
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
                value = false;
            }
            base.SetVisibleCore(value);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enableControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            menuItem.Checked = !menuItem.Checked;
            isEnabled = menuItem.Checked;
            // create a new thread
            if (isEnabled)
            {
                // Code to enable application's functionality

                
                StartServer();


            }
            else
            {
                StopServer();
                // Code to disable application's functionality
            }
        }

        static void StartServer()
        {
            if (!isServerRunning)
            {
                Debug.WriteLine("NEW SERVER");
                isServerRunning = true;
                serverThread = new Thread(RunServer);
                serverThread.Start();
            }
        }

        static void StopServer()
        {
            if (isServerRunning)
            {
                Debug.WriteLine("END SERVER");

                isServerRunning = false;
                serverThread.Join();
            }
        }

        static void RunServer()
        {
            int port = 12345;
            TcpListener server = new TcpListener(IPAddress.Any, port);

            server.Start();
            Debug.WriteLine($"Server is listening on port {port}");

            while (isServerRunning)
            {
                if (server.Pending())
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
                else
                {
                    // Sleep for a short time to avoid excessive CPU usage.
                    Thread.Sleep(100);
                }
            }

            server.Stop();
        }
        static void HandleClient(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string touchpadData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                Debug.WriteLine($"Received touchpad data: {touchpadData}");

                // Process touchpad data...

                stream.Close();
            }
            client.Close();
        }
    }
}