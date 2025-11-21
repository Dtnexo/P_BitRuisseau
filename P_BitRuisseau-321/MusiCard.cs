using System;
using System.Drawing;
using System.Windows.Forms;

namespace P_BitRuisseau_321
{
    public class MusicCard : UserControl
    {
        private Label lblTitle;
        private Label lblArtist;
        private Label lblDuration;
        private Label lblDescription;
        private Label lblDescriptionValue;
        private Label lblFeat;
        private Label lblYear;
        private Label lblFileName;
        private Label lblFileSize;
        private Button btnUpdateDes;

        public MusicCard()
        {
            this.Size = new Size(360, 150);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            lblTitle = new Label()
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            lblArtist = new Label()
            {
                Font = new Font("Segoe UI", 9),
                Location = new Point(140, 13),
                AutoSize = true
            };

            lblDuration = new Label()
            {
                Font = new Font("Segoe UI", 9),
                Location = new Point(257, 13),
                AutoSize = true
            };

            lblDescription = new Label()
            {
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Location = new Point(11, 35),
                AutoSize = true,
                Text = "Description :"
            };

            lblDescriptionValue = new Label()
            {
                Font = new Font("Segoe UI", 8),
                Location = new Point(11, 55),
                Size = new Size(240, 40),
                AutoSize = false
            };

            btnUpdateDes = new Button()
            {
                Text = "Update",
                Font = new Font("Segoe UI", 8),
                Location = new Point(265, 40),
                Size = new Size(70, 25)
            };
            lblFeat = new Label()
            {
                Font = new Font("Segoe UI", 8),
                Location = new Point(11, 95),
                AutoSize = true
            };

            lblYear = new Label()
            {
                Font = new Font("Segoe UI", 8),
                Location = new Point(250, 95),
                AutoSize = true
            };

            lblFileName = new Label()
            {
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                Location = new Point(11, 120),
                AutoSize = true
            };

            lblFileSize = new Label()
            {
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                Location = new Point(90, 120),
                AutoSize = true
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblArtist);
            this.Controls.Add(lblDuration);
            this.Controls.Add(lblDescription);
            this.Controls.Add(lblDescriptionValue);
            this.Controls.Add(btnUpdateDes);
            this.Controls.Add(lblFeat);
            this.Controls.Add(lblYear);
            this.Controls.Add(lblFileName);
            this.Controls.Add(lblFileSize);
        }

        public void SetData(Song song)
        {
            lblTitle.Text = song.Title;
            lblArtist.Text = song.Artist;
            lblDuration.Text = $"Durée: {song.Duration.Minutes}:{song.Duration.Seconds:D2}";
            lblDescriptionValue.Text = song.Description ?? "Pas de description.";
            lblFeat.Text = song.Featuring.Length > 0 ? $"featuring: {string.Join(", ", song.Featuring)}" : "featuring: Aucun";
            lblYear.Text = $"Année : {song.Year}";
            lblFileName.Text = song.FileName;
            lblFileSize.Text = song.Size + "Mo";
        }
    }
}
