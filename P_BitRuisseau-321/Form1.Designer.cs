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
            label3 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            ImportFile = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // fileLocal
            // 
            fileLocal.AutoSize = true;
            fileLocal.Location = new Point(235, 28);
            fileLocal.Name = "fileLocal";
            fileLocal.Size = new Size(56, 15);
            fileLocal.TabIndex = 0;
            fileLocal.Text = "Local File";
            // 
            // fileNetwork
            // 
            fileNetwork.AutoSize = true;
            fileNetwork.Location = new Point(562, 28);
            fileNetwork.Name = "fileNetwork";
            fileNetwork.Size = new Size(73, 15);
            fileNetwork.TabIndex = 1;
            fileNetwork.Text = "Network File";
            fileNetwork.Click += fileNetwork_Click;
            // 
            // label3
            // 
            label3.BackColor = Color.LightGray;
            label3.Location = new Point(143, 95);
            label3.Name = "label3";
            label3.Size = new Size(540, 334);
            label3.TabIndex = 2;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(222, 113);
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
            checkBox2.Location = new Point(549, 113);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(95, 19);
            checkBox2.TabIndex = 4;
            checkBox2.Text = "Network  File";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // ImportFile
            // 
            ImportFile.Location = new Point(195, 58);
            ImportFile.Name = "ImportFile";
            ImportFile.Size = new Size(145, 23);
            ImportFile.TabIndex = 5;
            ImportFile.Text = "Import File";
            ImportFile.UseVisualStyleBackColor = true;
            ImportFile.Click += ImportFile_Click;
            // 
            // button2
            // 
            button2.Location = new Point(521, 58);
            button2.Name = "button2";
            button2.Size = new Size(145, 23);
            button2.TabIndex = 6;
            button2.Text = "Request File Network";
            button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(ImportFile);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label3);
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
        private Label label3;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Button ImportFile;
        private Button button2;
    }
}
