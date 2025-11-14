using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace P_BitRuisseau_321
{
    public class FileName : Label
    {


        public FileName(Form parent, int x, string fileName)
        {

            this.Location = new Point(200, 160 + x); // Positionnement de l'ennemi aux coordonnées (x, y)
            this.Name = "this"; // Nom de l'objet (peut être modifié selon le besoin)
            this.Text = fileName;
            this.Size = new Size(70, 42); // Taille de l'ennemi
            this.TabIndex = 1; // Index de tabulation pour l'ordre d'accès
            this.TabStop = false; // Désactive l'arrêt de tabulation sur cet élément
            parent.Controls.Add(this); // Ajoute l'ennemi aux contrôles du formulaire parent
            ((System.ComponentModel.ISupportInitialize)this).EndInit(); // Fin de l'initialisation pour PictureBox

            
        }
    }
}
