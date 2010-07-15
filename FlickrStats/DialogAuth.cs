using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FlickrNet;

namespace FlickrStats
{
    public partial class DialogAuth : Form
    {
        public DialogAuth()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Create Flickr instance
            Flickr flickr = new Flickr(Program.ApiKey, Program.SharedSecret);
            try
            {
                // use the temporary Frob to get the authentication
                Auth auth = flickr.AuthGetToken(Program.tempFrob);
                // Store this Token for later usage, 
                // or set your Flickr instance to use it.
                string info = "Authentification réussie\n" +
                    "Utilisateur identifié: " + auth.User.UserName + "\n" +
                    "L'identifiant pour cette session est " + auth.Token + "\n";
                Program.AuthToken = auth.Token;
                Program.Username = auth.User.UserName;
                MessageBox.Show(info, "Identification Flickr",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.WriteConfig();
                Hide();
                var main_window = new MainForm();
                main_window.Show();
            }
            catch (FlickrException ex)
            {
                // If user did not authenticate your application 
                // then a FlickrException will be thrown.
                string info = "L'authentification a échoué:\n" + ex.Message;
                MessageBox.Show(info, "Identification Flickr",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Quit application
                Application.Exit();
            }
        }
    }
}
