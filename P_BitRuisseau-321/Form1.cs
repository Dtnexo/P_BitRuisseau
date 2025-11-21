using System.Diagnostics;
using System.IO;
using System.Text;
using TagLib;
using System.Linq;
using BitRuisseau;

namespace P_BitRuisseau_321
{
    public partial class Form1 : Form
    {
        private List<Song> mySongsList = new List<Song>();
        public Form1()
        {
            InitializeComponent();
            LoadTagMusic();


        }

        private void fileNetwork_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public MusicCard CreateMusicCard(int y, Song song)
        {
            MusicCard card = new MusicCard();
            card.SetData(song);
            card.Location = new Point(10, y);

            panelLocal.Controls.Add(card);
            card.BringToFront();
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

                mySongsList.Add(song);

                LoadTagMusic();

            }
        }

        public void LoadTagMusic()
        {
            panelLocal.Controls.Clear(); // nettoyage du panel pour éviter les doublons

            string folder = Path.Combine(Application.StartupPath, "fileMp3");
            if (!Directory.Exists(folder)) return;

            var files = Directory.GetFiles(folder, "*.mp3");

            int index = 0;
            files.ToList().ForEach(file =>
            {
                Song song = LoadSongWithTagLib(file);

                CreateMusicCard(index * 170, song);

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
            // ouvrir une InputBox (MessageBox custom)
            string newDescription = Microsoft.VisualBasic.Interaction.InputBox(
                "Nouvelle description :",
                "Modifier la description",
                card.BoundSong.Description
            );
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
