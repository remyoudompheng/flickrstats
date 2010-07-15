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
    public partial class MainForm : Form
    {
        static string endl = Environment.NewLine;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ButtonStats_Click(object sender, EventArgs e)
        {
            var flickr = new Flickr(Program.ApiKey, Program.SharedSecret, Program.AuthToken);
            // Statistiques générales
            var statfile = new System.IO.StreamWriter("stats.csv");
            DateTime day = DateTime.Today;
            while (day > Program.LastUpdate)
            {
                try
                {
                    var s = flickr.StatsGetTotalViews(day);
                    statfile.WriteLine(Utility.toCSV(s));
                    StatusLabel.Text = "Chargement des stats " + day.ToShortDateString();
                    Application.DoEvents();
                    day -= TimeSpan.FromDays(1);
                }
                catch (FlickrApiException ex) {
                    MessageBox.Show("Erreur lors du chargement des statistiques du "
                        + day.ToShortDateString() + " : " + ex.OriginalMessage,
                        "Erreur", MessageBoxButtons.OK);
                    break;
                }
            }
            statfile.Close();

            // Referrers
            day = DateTime.Today;
            statfile = new System.IO.StreamWriter("stats_dom.csv");
            while (day > Program.LastUpdate)
            {
                try {
                    var s = flickr.StatsGetPhotoDomains(day);
                    day -= TimeSpan.FromDays(1);
                    StatusLabel.Text = "Chargement des domaines " + day.ToShortDateString();
                    Application.DoEvents();
                    statfile.Write(Utility.toCSV(s, day));
                }
                catch (FlickrApiException ex)
                {
                    MessageBox.Show("Erreur lors du chargement des domaines référents du "
                        + day.ToShortDateString() + " : " + ex.OriginalMessage,
                        "Erreur", MessageBoxButtons.OK);
                    break;
                }
            }
            statfile.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label.Text = "Statistiques pour " + Program.Username + endl;
            label.Text += "Dernière mise à jour le " + Program.LastUpdate.ToShortDateString() + endl;

            var flickr = new Flickr(Program.ApiKey, Program.SharedSecret, Program.AuthToken);
            string info = "Aujourd'hui :\n";
            try
            {
                var stats = flickr.StatsGetTotalViews();
                info += "Vues totales: " + stats.TotalViews + endl +
                    "Vues photos: " + stats.PhotoViews + endl +
                    "Vues photosets: " + stats.PhotosetViews + endl +
                    "Vues photostream: " + stats.PhotostreamViews + endl +
                    "Vues collection: " + stats.CollectionViews;
                // MessageBox.Show(info, "Stats", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FlickrException ex)
            {
                // MessageBox.Show(ex.ToString(), "Erreur",
                //   MessageBoxButtons.OK, MessageBoxIcon.Error);
                info += ex.Message;
            }

            label.Text += info;
        }

        private void ButtonStream_Click(object sender, EventArgs e)
        {
            var flickr = new Flickr(Program.ApiKey, Program.SharedSecret, Program.AuthToken);
            // Referrers
            DateTime day = DateTime.Today;
            var statfile = new System.IO.StreamWriter("stream_domains.csv");
            var reffile = new System.IO.StreamWriter("stream_referrers.csv");
            while (day > Program.LastUpdate)
            {
                try
                {
                    // Liste des domaines référents
                    var s = flickr.StatsGetPhotostreamDomains(day);
                    StatusLabel.Text = "Statistiques du flux pour le " + day.ToShortDateString();
                    Application.DoEvents();
                    statfile.Write(Utility.toCSV(s, day));

                    foreach (StatDomain dom in s)
                    {
                        var r = flickr.StatsGetPhotostreamReferrers(day, dom.Name);
                        reffile.Write(Utility.toCSV(r, dom, day));
                    }
                }
                catch (FlickrApiException ex)
                {
                    MessageBox.Show("Erreur lors du chargement des domaines référents du "
                        + day.ToShortDateString() + " : " + ex.OriginalMessage,
                        "Erreur", MessageBoxButtons.OK);
                    break;
                }
                day -= TimeSpan.FromDays(1);
            }
            // Fermeture des fichiers
            statfile.Close();
            reffile.Close();
        }
    }
}
