using System;
using System.Drawing;
using System.Windows.Forms;

namespace P_BitRuisseau_321
{
    public class MediathequeCard : UserControl
    {
        private Label lbl_name;
        private string _name;
        public event Action<string> AskCatalogOnClicked;

        public MediathequeCard(string name)
        {
            this.Size = new Size(300, 50);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;

            lbl_name = new Label();
            lbl_name.Text = name;
            lbl_name.Location = new Point(10, 15);
            lbl_name.AutoSize = true;
            lbl_name.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            
            this.Controls.Add(lbl_name);

            this._name = name;
            
            if (name == Environment.MachineName)
            {
                this.lbl_name.Text = name + " (Vous)";
                this.BackColor = Color.FromArgb(214, 255, 225);
            }

            // Click events
            this.Click += MediathequeCard_Click;
            this.lbl_name.Click += MediathequeCard_Click;
        }

        private void MediathequeCard_Click(object sender, EventArgs e)
        {
            AskCatalogOnClicked?.Invoke(this._name);
        }
    }
}
