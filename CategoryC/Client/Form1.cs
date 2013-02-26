﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using Shared;
using System.Threading;
using Shared.Opcodes;

namespace Client
{
    public partial class Client : Form
    {
        private const int PortNumber = 8888;
        private ASCIIEncoding Encoder;
        DateTime TimeNow;
        private ConnectionHandler connectionHandler;

        public Client()
        {
            InitializeComponent();
            this.connectionHandler = new ConnectionHandler(this);
            Encoder = new ASCIIEncoding();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!checkIpAddress())
            {
                lblConnection.Text = "Server Connection Failed";
            }
            else
            {
              if (txtName.Text.Equals("")) {
                lblConnection.Text = "No name entered!";
              }
              else {
                connectionHandler.Connect(txtIp.Text, PortNumber);
                lblConnection.Text = "Server Connection!";
                Name = txtName.Text;
              }
                
            }
        }

        private bool checkIpAddress()
        {
            bool isOk = true;
            try
            {
                IPAddress.Parse(txtIp.Text);
            }
            catch
            {
                isOk = false;
            }
            return isOk;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!TimeNow.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
            {
                TimeNow = DateTime.Now;
                AppendText(TimeNow.ToShortDateString() + "\n", Color.Blue);
                AppendText(TimeNow.ToShortTimeString() + "\n", Color.Red);
            }
            if (!TimeNow.ToShortTimeString().Equals(DateTime.Now.ToShortTimeString()))
            {
                AppendText(TimeNow.ToShortTimeString() + "\n", Color.Red);
            }
            TimeNow = DateTime.Now;
            txtMess.AppendText(txtSend.Text + "\n");

            // CREATE
            byte[] message = Encoder.GetBytes(txtName.Text + "|" + txtSend.Text);
            PacketHandler.sendPacket(connectionHandler.ServerSocket, ClientMessage.CMSG_BROADCAST, message);
            txtSend.ResetText();
        }

        public void AppendText(string text, Color color)
        {
            txtMess.SelectionStart = txtMess.TextLength;
            txtMess.SelectionLength = 0;

            txtMess.SelectionColor = color;
            txtMess.AppendText(text);
            txtMess.SelectionColor = txtMess.ForeColor;
        }
        public void AppendText(string text) {
          txtMess.SelectionStart = txtMess.TextLength;
          txtMess.SelectionLength = 0;

          txtMess.SelectionColor = Color.Black;
          txtMess.AppendText(text);
          txtMess.SelectionColor = txtMess.ForeColor;
        }
    }
}