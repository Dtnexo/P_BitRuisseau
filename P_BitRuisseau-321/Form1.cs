using System.Diagnostics;
using System.IO;
using System.Text;
using TagLib;
using System.Linq;

namespace P_BitRuisseau_321
{
    public partial class Form1 : Form
    {
        private List<Song> mySongsList = new List<Song>();
        public Form1()
        {
            InitializeComponent();
            List<FileName> files = new List<FileName>();
        }

        private void fileNetwork_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public FileName  allFileName (int x , string FileName)
        {
            var file = new FileName(this, x, FileName);
            return file;
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

                MessageBox.Show($"Importé : {song.Title} - {song.Duration}");
            }
        }

        public void LoadTagMusic()
        {

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
                    : new string[0]
            };

            return s;
        }

        private void fileName_Click(object sender, EventArgs e)
        {

        }
    }
}
