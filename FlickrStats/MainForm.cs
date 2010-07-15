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
            statfile.WriteLine("Date;Photos;Albums;Flux;Expos");
            while (day > Program.LastUpdate)
            {
                try
                {
                    var s = flickr.StatsGetTotalViews(day);
                    statfile.WriteLine(day.ToShortDateString() + ";" + Utility.toCSV(s));
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
                    "Vues albums: " + stats.PhotosetViews + endl +
                    "Vues galerie: " + stats.PhotostreamViews + endl +
                    "Vues expos: " + stats.CollectionViews;
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
                    var s = flickr.StatsGetPhotostreamDomains(day, 1, 100);
                    StatusLabel.Text = "Statistiques du flux pour le " + day.ToShortDateString();
                    Application.DoEvents();
                    statfile.Write(Utility.toCSV(s, day.ToShortDateString()));

                    foreach (StatDomain dom in s)
                    {
                        var r = flickr.StatsGetPhotostreamReferrers(day, dom.Name, 1, 100);
                        reffile.Write(Utility.toCSV(r, day.ToShortDateString() + ";" + dom.Name));
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

        private void ButtonPhoto_Click(object sender, EventArgs e)
        {
            var flickr = new Flickr(Program.ApiKey, Program.SharedSecret, Program.AuthToken);
            // Referrers
            DateTime day = DateTime.Today;
            var statfile = new System.IO.StreamWriter("stats_dom.csv");
            var reffile = new System.IO.StreamWriter("stats_referrers.csv");
            while (day > Program.LastUpdate)
            {
                try
                {
                    var s = flickr.StatsGetPhotoDomains(day, 1, 100);
                    day -= TimeSpan.FromDays(1);
                    StatusLabel.Text = "Chargement des domaines " + day.ToShortDateString();
                    Application.DoEvents();
                    statfile.Write(Utility.toCSV(s, day.ToShortDateString()));
                    foreach (StatDomain dom in s)
                    {
                        var r = flickr.StatsGetPhotoReferrers(day, dom.Name, 1, 100);
                        reffile.Write(Utility.toCSV(r, day.ToShortDateString() + ";" + dom.Name));
                    }
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

        private void ButtonSets_Click(object sender, EventArgs e)
        {
            var flickr = new Flickr(Program.ApiKey, Program.SharedSecret, Program.AuthToken);
            // Liste des albums
            var sets = flickr.PhotosetsGetList();
            string setlist = "";
            foreach (Photoset pset in sets) setlist += pset.Title + "\n";
            MessageBox.Show("Liste des albums\n" + setlist, "Liste des albums",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Statistiques
            var statfile = new System.IO.StreamWriter("albums_stats.csv");
            var reffile = new System.IO.StreamWriter("albums_referrers.csv");
            DateTime day = DateTime.Today;
            statfile.WriteLine("Date;Album;Vues;Favoris;Commentaires");
            reffile.WriteLine("Date;Album;Domain;Views");
            while (day > Program.LastUpdate)
            {
                foreach (Photoset pset in sets)
                {
                    StatusLabel.Text = "Stats de l'album " + 
                        pset.Title + " pour le " + day.ToShortDateString();
                    Application.DoEvents();
                    try
                    {
                        var s = flickr.StatsGetPhotosetStats(day, pset.PhotosetId);
                        statfile.WriteLine(day.ToShortDateString() + ";" +
                            pset.Title + ";" + Utility.toCSV(s));
                        var r = flickr.StatsGetPhotosetDomains(day, pset.PhotosetId, 1, 100);
                        reffile.WriteLine(Utility.toCSV(r, day.ToShortDateString() + ";" + pset.Title));
                    }
                    catch(FlickrApiException ex)
                    { }
                }

                day -= TimeSpan.FromDays(1);
            }
            statfile.Close(); reffile.Close();
        }
    }
}
