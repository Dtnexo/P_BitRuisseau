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
            checkLocalFile = new CheckBox();
            checkNetworkFile = new CheckBox();
            ImportFile = new Button();
            RequestFileNetwork = new Button();
            panelLocal = new Panel();
            panel1 = new Panel();
            cbUsers = new ComboBox();
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
            fileNetwork.Location = new Point(709, 9);
            fileNetwork.Name = "fileNetwork";
            fileNetwork.Size = new Size(73, 15);
            fileNetwork.TabIndex = 1;
            fileNetwork.Text = "Network File";
            fileNetwork.Click += fileNetwork_Click;
            // 
            // checkLocalFile
            // 
            checkLocalFile.AutoSize = true;
            checkLocalFile.Location = new Point(188, 88);
            checkLocalFile.Name = "checkLocalFile";
            checkLocalFile.Size = new Size(78, 19);
            checkLocalFile.TabIndex = 3;
            checkLocalFile.Text = "Local  File";
            checkLocalFile.UseVisualStyleBackColor = true;
            checkLocalFile.CheckedChanged += checkLocalFile_CheckedChanged;
            // 
            // checkNetworkFile
            // 
            checkNetworkFile.AutoSize = true;
            checkNetworkFile.Location = new Point(700, 88);
            checkNetworkFile.Name = "checkNetworkFile";
            checkNetworkFile.Size = new Size(95, 19);
            checkNetworkFile.TabIndex = 4;
            checkNetworkFile.Text = "Network  File";
            checkNetworkFile.UseVisualStyleBackColor = true;
            checkNetworkFile.CheckedChanged += checkNetworkFile_CheckedChanged;
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
            // RequestFileNetwork
            // 
            RequestFileNetwork.Location = new Point(671, 27);
            RequestFileNetwork.Name = "RequestFileNetwork";
            RequestFileNetwork.Size = new Size(145, 23);
            RequestFileNetwork.TabIndex = 6;
            RequestFileNetwork.Text = "Request File Network";
            RequestFileNetwork.UseVisualStyleBackColor = true;
            RequestFileNetwork.Click += RequestFileNetwork_Click;
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
            // cbUsers
            // 
            cbUsers.FormattingEnabled = true;
            cbUsers.Location = new Point(671, 62);
            cbUsers.Name = "cbUsers";
            cbUsers.Size = new Size(145, 23);
            cbUsers.TabIndex = 7;
            cbUsers.SelectedIndexChanged += cbUsers_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1005, 530);
            Controls.Add(cbUsers);
            Controls.Add(panel1);
            Controls.Add(checkNetworkFile);
            Controls.Add(panelLocal);
            Controls.Add(checkLocalFile);
            Controls.Add(RequestFileNetwork);
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
        private CheckBox checkLocalFile;
        private CheckBox checkNetworkFile;
        private Button ImportFile;
        private Button RequestFileNetwork;
        private Panel panelLocal;
        private Panel panel1;
        private ComboBox cbUsers;
    }
}
