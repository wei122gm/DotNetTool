using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinFormControls;

namespace DemoHttpServer
{
    public partial class Main : Form
    {
        Thread thread = null;
        HttpServer httpServer = null;

        public Main()
        {
            InitializeComponent();
            thread = new Thread(Start);
            thread.IsBackground = true;
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            thread.Start();
            FolderPath_TextBox.Enabled = false;
            Port_TextBox.Enabled = false;
            Start_Button.Enabled = false;
            Open_Button.Enabled = true;
        }

        private void Open_Button_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:" + int.Parse(Port_TextBox.Text));
        }

        void Start()
        {
            httpServer = new HttpServer();
            httpServer.Start(FolderPath_TextBox.Text, new System.Net.IPEndPoint(System.Net.IPAddress.Any, int.Parse(Port_TextBox.Text)));
        }

        private void Folder_Button_Click(object sender, EventArgs e)
        {
            FolderSelectDialog folderSelectDialog = new FolderSelectDialog();
            folderSelectDialog.InitialDirectory = FolderPath_TextBox.Text;
            if (folderSelectDialog.ShowDialog(IntPtr.Zero))
            {
                FolderPath_TextBox.Text = folderSelectDialog.FileName;
            }
        }
    }
}
