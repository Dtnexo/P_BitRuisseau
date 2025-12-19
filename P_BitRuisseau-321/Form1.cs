using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using TagLib;
using BitRuisseau;

namespace P_BitRuisseau_321
{
    public partial class Form1 : Form
    {

        private readonly IProtocol protocol; 

        public Form1()
        {
            InitializeComponent();
            protocol = new MqttService(); 
            LoadTagMusic();
            protocol.SayOnline();
            protocol.GetOnlineMediatheque();
        }

        private void fileNetwork_Click(object sender, EventArgs e)
        {
             RefreshNetworkUsers();
        }

        private void RequestFileNetwork_Click(object sender, EventArgs e)
        {
             protocol.GetOnlineMediatheque(); 
             RefreshNetworkUsers();
        }

        private void RefreshNetworkUsers()
        {
            
             cbUsers.Items.Clear();
             foreach (var ownerName in Program.mediathequeSongs.Keys)
             {
                 cbUsers.Items.Add(ownerName);
             }
             
             panel1.Controls.Clear();
             int y = 0;
             foreach (var ownerName in Program.mediathequeSongs.Keys)
             {
                 var userCard = new MediathequeCard(ownerName);
                 userCard.Location = new Point(10, y);
                 userCard.AskCatalogOnClicked += OnMediathequeClicked;
                 panel1.Controls.Add(userCard);
                 y += 60;
             }
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUsers.SelectedItem != null)
            {
                string ownerName = cbUsers.SelectedItem.ToString();
                OnMediathequeClicked(ownerName);
            }
        }

        private void checkNetworkFile_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            panel1.Visible = cb.Checked;
            if (cb.Checked) RefreshNetworkUsers();
        }

        private void OnMediathequeClicked(string ownerName)
        {
            
            panel1.Controls.Clear();
            
            protocol.AskCatalog(ownerName);

            if (Program.mediathequeSongs.ContainsKey(ownerName))
            {
                var songs = Program.mediathequeSongs[ownerName];
                int index = 0;
                foreach(var s in songs)
                {
                     var card = CreateMusicCard(index * 170, s);
                     card.SetDownloadVisible(true);
                     card.Tag = ownerName;
                     card.DownloadClicked += HandleDownload;
                     panel1.Controls.Add(card); // Explicitly add to panel1
                     index++;
                }
            }
        }
        private void HandleDownload(MusicCard card)
        {
            string owner = card.Tag as string;
            if (string.IsNullOrEmpty(owner)) return;
            
            int bytes = card.BoundSong.Size * 1024 * 1024;
            if (bytes == 0) bytes = 10 * 1024 * 1024; 
            
            protocol.AskMedia(card.BoundSong, owner, 0, bytes);
            MessageBox.Show($"Demande envoyée à {owner} pour {card.BoundSong.Title}!");
        }

        private void checkLocalFile_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            panelLocal.Visible = cb.Checked;
        }

        public MusicCard CreateMusicCard(int y, Song song)
        {
            MusicCard card = new MusicCard();
            card.SetData(song);
            card.Location = new Point(10, y);
            card.UpdateDescriptionClicked += HandleUpdateDescription;

            // Removed hardcoded panelLocal.Controls.Add(card);
            // card.BringToFront(); // Caller should handle this if needed
            return card;
        }

        private void ImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "mp3 files | *.mp3";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = dialog.FileName;

                Helper.HashFile(sourcePath);

                string folder = Path.Combine(Application.StartupPath, "fileMp3");
                Directory.CreateDirectory(folder);

                string destPath = Path.Combine(folder, Path.GetFileName(sourcePath));

                System.IO.File.Copy(sourcePath, destPath, true);

                Song song = LoadSongWithTagLib(destPath);



                LoadTagMusic();

            }
        }

        public void LoadTagMusic()
        {
            panelLocal.Controls.Clear();
            Program.MySongs.Clear(); 

            string folder = Path.Combine(Application.StartupPath, "fileMp3");
            if (!Directory.Exists(folder)) return;

            var files = Directory.GetFiles(folder, "*.mp3");

            int index = 0;
            files.ToList().ForEach(file =>
            {
                Song song = LoadSongWithTagLib(file);
                Program.MySongs.Add(song);

                var card = CreateMusicCard(index * 170, song);
                panelLocal.Controls.Add(card); // Explicitly add to panelLocal

                index++;
            });
        }

        public Song LoadSongWithTagLib(string filePath)
        {
            var tagFile = TagLib.File.Create(filePath);

            Song s = new Song(filePath)
            {
                Title = tagFile.Tag.Title,
                Artist = tagFile.Tag.FirstPerformer,
                Year = (int)tagFile.Tag.Year,
                Duration = tagFile.Properties.Duration,
                Featuring = tagFile.Tag.Performers.Length > 1
                    ? tagFile.Tag.Performers.Skip(1).ToArray()
                    : new string[0],
                Description = tagFile.Tag.Description,
            };
            return s;
        }
        private void HandleUpdateDescription(MusicCard card)
        {

            string originalDescription = card.BoundSong.Description ?? "";
            string newDescription = Microsoft.VisualBasic.Interaction.InputBox(
                "Nouvelle description :",
                "Modifier la description",
                originalDescription
            );
            bool isLikelyCancellation = (newDescription == "" && originalDescription != "");


            if (newDescription != originalDescription && !isLikelyCancellation)
            {
                try
                {
                    var tagFile = TagLib.File.Create(card.BoundSong.FilePath);

                    tagFile.Tag.Description = newDescription;

                    tagFile.Save();

                    card.BoundSong.Description = newDescription;

                    card.SetData(card.BoundSong);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}", "Erreur de Sauvegarde", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
        }
        private void btnUpdateDes_Click(object sender, EventArgs e)
        {

        }
        private void fileName_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
