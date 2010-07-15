using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FlickrNet;

namespace FlickrStats
{
    static class Utility
    {
        static string endl = Environment.NewLine;

        public static string toCSV(StatViews stat)
        {
            string line = stat.TotalViews + ";" + stat.PhotoViews
                       + ";" + stat.PhotosetViews + ";" + stat.PhotostreamViews
                       + ";" + stat.CollectionViews;
            return line;
        }

        public static string toCSV(StatDomainCollection stat, DateTime day)
        {
            string lines = "";
            foreach (StatDomain s in stat)
            {
                // StatDomainCollection: Name => domain, Views => nb of views
                string line = day.ToShortDateString() + ";" +
                    s.Name + ";" + s.Views;
                lines += line + endl;
            }
            return lines;
        }

        public static string toCSV(StatReferrerCollection stat, StatDomain dom, DateTime day)
        {
            string lines = "";
            foreach (StatReferrer s in stat)
            {
                // StatDomainCollection: Name => domain, Views => nb of views
                string line = day.ToShortDateString() + ";" +
                    s.Url + ";" + dom.Name + ";" + s.SearchTerm + ";" + s.Views;
                lines += line + endl;
            }
            return lines;
        }
    }

    static class Program
    {
        public static string tempFrob;
        public static string ApiKey = "300ba8bd30907464df991410561db6e0";
        public static string SharedSecret = "26446b14200c0142";
        public static string AuthToken;
        public static string Username;

        public static DateTime LastUpdate;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ReadConfig();
            if (AuthToken.Length == 0)
            {
                // Create Flickr instance
                Flickr flickr = new Flickr(Program.ApiKey, Program.SharedSecret);
                // Get Frob
                Program.tempFrob = flickr.AuthGetFrob();
                // Calculate the URL at Flickr to redirect the user to
                string flickrUrl = flickr.AuthCalcUrl(Program.tempFrob, AuthLevel.Read);
                // The following line will load the URL in the users default browser.
                System.Diagnostics.Process.Start(flickrUrl);
                Application.Run(new DialogAuth());
            }
            else
                Application.Run(new MainForm());
        }

        // Lecture de la configuration
        static void ReadConfig()
        {
            string config_file = "config.xml";
            if (!System.IO.File.Exists(config_file))
            {
                var newconf = new System.Xml.XmlTextWriter(config_file, null);
                newconf.WriteStartDocument();
                newconf.WriteStartElement("config");
                newconf.WriteElementString("last_update", "0");
                newconf.WriteElementString("auth_token", "");
                newconf.WriteEndElement();
                newconf.WriteEndDocument();
                newconf.Close();
            }

            var conf = new System.Xml.XmlTextReader(config_file);
            while (conf.Read())
            {
                if (conf.LocalName == "last_update")
                    LastUpdate = UtilityMethods.UnixTimestampToDate(conf.Name);
                if (conf.LocalName == "auth_token")
                    AuthToken = conf.Name;
            }
            conf.Close();

            // On vérifie que le token est encore valide
            if (AuthToken.Length > 0)
            {
                var flickr = new Flickr(Program.ApiKey, Program.SharedSecret);
                try
                {
                    Auth auth = flickr.AuthCheckToken(AuthToken);
                    Username = auth.User.UserName;
                }
                catch (FlickrApiException ex)
                {
                    //MessageBox.Show(ex.Message, "Authentification requise",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    AuthToken = "";
                }
            }
        }

        // Lecture de la configuration
        public static void WriteConfig()
        {
            string config_file = "config.xml";
            var newconf = new System.Xml.XmlTextWriter(config_file, null);
            newconf.WriteStartDocument();
            newconf.WriteStartElement("config");
            newconf.WriteElementString("last_update",
                UtilityMethods.DateToUnixTimestamp(LastUpdate));
            newconf.WriteElementString("auth_token", AuthToken);
            newconf.WriteEndElement();
            newconf.WriteEndDocument();
            newconf.Close();
        }            
    }
}
