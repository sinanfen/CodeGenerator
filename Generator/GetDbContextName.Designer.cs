namespace Generator
{
    partial class GetDbContextName
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtDbContextName = new TextBox();
            label1 = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            SuspendLayout();
            // 
            // txtDbContextName
            // 
            txtDbContextName.Location = new Point(119, 6);
            txtDbContextName.Name = "txtDbContextName";
            txtDbContextName.Size = new Size(148, 23);
            txtDbContextName.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(101, 15);
            label1.TabIndex = 1;
            label1.Text = "DbContextName: ";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(119, 35);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(71, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(196, 35);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(71, 23);
            btnSave.TabIndex = 2;
            btnSave.Text = "Continue";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // GetDbContextName
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(277, 67);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(label1);
            Controls.Add(txtDbContextName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "GetDbContextName";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GetDbContextName";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDbContextName;
        private Label label1;
        private Button btnCancel;
        private Button btnSave;
    }
}