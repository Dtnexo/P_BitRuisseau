using static System.Formats.Asn1.AsnWriter;

namespace P_BitRuisseau_321
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            fileLocal = new Label();
            fileNetwork = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            ImportFile = new Button();
            button2 = new Button();
            panelLocal = new Panel();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // fileLocal
            // 
            fileLocal.AutoSize = true;
            fileLocal.Location = new Point(199, 18);
            fileLocal.Name = "fileLocal";
            fileLocal.Size = new Size(56, 15);
            fileLocal.TabIndex = 0;
            fileLocal.Text = "Local File";
            // 
            // fileNetwork
            // 
            fileNetwork.AutoSize = true;
            fileNetwork.Location = new Point(712, 18);
            fileNetwork.Name = "fileNetwork";
            fileNetwork.Size = new Size(73, 15);
            fileNetwork.TabIndex = 1;
            fileNetwork.Text = "Network File";
            fileNetwork.Click += fileNetwork_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(188, 88);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(78, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Local  File";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(700, 88);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(95, 19);
            checkBox2.TabIndex = 4;
            checkBox2.Text = "Network  File";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // ImportFile
            // 
            ImportFile.Location = new Point(159, 48);
            ImportFile.Name = "ImportFile";
            ImportFile.Size = new Size(145, 23);
            ImportFile.TabIndex = 5;
            ImportFile.Text = "Import File";
            ImportFile.UseVisualStyleBackColor = true;
            ImportFile.Click += ImportFile_Click;
            // 
            // button2
            // 
            button2.Location = new Point(671, 48);
            button2.Name = "button2";
            button2.Size = new Size(145, 23);
            button2.TabIndex = 6;
            button2.Text = "Request File Network";
            button2.UseVisualStyleBackColor = true;
            // 
            // panelLocal
            // 
            panelLocal.AutoScroll = true;
            panelLocal.BackColor = Color.LightGray;
            panelLocal.Location = new Point(34, 113);
            panelLocal.Name = "panelLocal";
            panelLocal.Size = new Size(420, 394);
            panelLocal.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.LightGray;
            panel1.Location = new Point(539, 114);
            panel1.Name = "panel1";
            panel1.Size = new Size(420, 393);
            panel1.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1005, 530);
            Controls.Add(panel1);
            Controls.Add(checkBox2);
            Controls.Add(panelLocal);
            Controls.Add(checkBox1);
            Controls.Add(button2);
            Controls.Add(ImportFile);
            Controls.Add(fileNetwork);
            Controls.Add(fileLocal);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label fileLocal;
        private Label fileNetwork;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Button ImportFile;
        private Button button2;
        private Panel panelLocal;
        private Panel panel1;
    }
}
