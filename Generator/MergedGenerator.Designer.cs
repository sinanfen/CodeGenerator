namespace Generator
{
    partial class MergedGenerator
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
            components = new System.ComponentModel.Container();
            btnReset = new Button();
            btnGenerate = new Button();
            chkUpdateDto = new CheckBox();
            chkAddDto = new CheckBox();
            chkDto = new CheckBox();
            txtProperties = new TextBox();
            btnBrowse = new Button();
            txtPath = new TextBox();
            folderBrowserDialog = new FolderBrowserDialog();
            toolTip = new ToolTip(components);
            txtNamespace = new TextBox();
            txtEntityName = new TextBox();
            txtModuleName = new TextBox();
            chkInterface = new CheckBox();
            SuspendLayout();
            // 
            // btnReset
            // 
            btnReset.Location = new Point(405, 288);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(75, 23);
            btnReset.TabIndex = 20;
            btnReset.Text = "Clear";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(486, 288);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(207, 23);
            btnGenerate.TabIndex = 21;
            btnGenerate.Text = "Generate";
            toolTip.SetToolTip(btnGenerate, "Click to generate the selected DTO files.");
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // chkUpdateDto
            // 
            chkUpdateDto.AutoSize = true;
            chkUpdateDto.Location = new Point(136, 292);
            chkUpdateDto.Name = "chkUpdateDto";
            chkUpdateDto.Size = new Size(83, 19);
            chkUpdateDto.TabIndex = 19;
            chkUpdateDto.Text = "UpdateDto";
            toolTip.SetToolTip(chkUpdateDto, "Select this option to generate an UpdateDTO class for update operations.");
            chkUpdateDto.UseVisualStyleBackColor = true;
            // 
            // chkAddDto
            // 
            chkAddDto.AutoSize = true;
            chkAddDto.Location = new Point(63, 292);
            chkAddDto.Name = "chkAddDto";
            chkAddDto.Size = new Size(67, 19);
            chkAddDto.TabIndex = 18;
            chkAddDto.Text = "AddDto";
            toolTip.SetToolTip(chkAddDto, "Select this option to generate an AddDTO class for insert operations.");
            chkAddDto.UseVisualStyleBackColor = true;
            // 
            // chkDto
            // 
            chkDto.AutoSize = true;
            chkDto.Location = new Point(12, 292);
            chkDto.Name = "chkDto";
            chkDto.Size = new Size(45, 19);
            chkDto.TabIndex = 17;
            chkDto.Text = "Dto";
            toolTip.SetToolTip(chkDto, "Select this option to generate a standard DTO class.");
            chkDto.UseVisualStyleBackColor = true;
            // 
            // txtProperties
            // 
            txtProperties.ForeColor = Color.Gray;
            txtProperties.Location = new Point(12, 70);
            txtProperties.Multiline = true;
            txtProperties.Name = "txtProperties";
            txtProperties.ScrollBars = ScrollBars.Both;
            txtProperties.Size = new Size(681, 212);
            txtProperties.TabIndex = 16;
            txtProperties.Text = "Entity Properties (one per line)";
            toolTip.SetToolTip(txtProperties, "Enter the entity properties (e.g., public string Name { get; set; }). One per line.");
            txtProperties.Enter += RemovePlaceholder;
            txtProperties.Leave += AddPlaceholder;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(618, 12);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 12;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            txtPath.ForeColor = Color.Gray;
            txtPath.Location = new Point(12, 12);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(600, 23);
            txtPath.TabIndex = 11;
            txtPath.Text = "Project Path";
            toolTip.SetToolTip(txtPath, "The path where the project files will be saved.");
            txtPath.Enter += RemovePlaceholder;
            txtPath.Leave += AddPlaceholder;
            // 
            // txtNamespace
            // 
            txtNamespace.ForeColor = Color.Gray;
            txtNamespace.Location = new Point(12, 41);
            txtNamespace.Name = "txtNamespace";
            txtNamespace.Size = new Size(179, 23);
            txtNamespace.TabIndex = 13;
            txtNamespace.Text = "Enter Namespace";
            toolTip.SetToolTip(txtNamespace, "Enter the short name of the module (e.g., CRM, HR).");
            txtNamespace.Enter += RemovePlaceholder;
            txtNamespace.Leave += AddPlaceholder;
            // 
            // txtEntityName
            // 
            txtEntityName.ForeColor = Color.Gray;
            txtEntityName.Location = new Point(383, 41);
            txtEntityName.Name = "txtEntityName";
            txtEntityName.Size = new Size(310, 23);
            txtEntityName.TabIndex = 15;
            txtEntityName.Text = "Enter Entity Name";
            toolTip.SetToolTip(txtEntityName, "Enter the short name of the module (e.g., CRM, HR).");
            txtEntityName.Enter += RemovePlaceholder;
            txtEntityName.Leave += AddPlaceholder;
            // 
            // txtModuleName
            // 
            txtModuleName.ForeColor = Color.Gray;
            txtModuleName.Location = new Point(197, 41);
            txtModuleName.Name = "txtModuleName";
            txtModuleName.Size = new Size(180, 23);
            txtModuleName.TabIndex = 14;
            txtModuleName.Text = "Enter Module Short Name";
            toolTip.SetToolTip(txtModuleName, "Enter the short name of the module (e.g., CRM, HR).");
            txtModuleName.Enter += RemovePlaceholder;
            txtModuleName.Leave += AddPlaceholder;
            // 
            // chkInterface
            // 
            chkInterface.AutoSize = true;
            chkInterface.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            chkInterface.Location = new Point(225, 292);
            chkInterface.Name = "chkInterface";
            chkInterface.Size = new Size(95, 19);
            chkInterface.TabIndex = 22;
            chkInterface.Text = "AddServices";
            chkInterface.UseVisualStyleBackColor = true;
            // 
            // MergedGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(705, 322);
            Controls.Add(chkInterface);
            Controls.Add(btnReset);
            Controls.Add(btnGenerate);
            Controls.Add(chkUpdateDto);
            Controls.Add(chkAddDto);
            Controls.Add(chkDto);
            Controls.Add(txtProperties);
            Controls.Add(btnBrowse);
            Controls.Add(txtPath);
            Controls.Add(txtNamespace);
            Controls.Add(txtEntityName);
            Controls.Add(txtModuleName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MergedGenerator";
            Text = "MergedGenerator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnReset;
        private Button btnGenerate;
        private ToolTip toolTip;
        private CheckBox chkUpdateDto;
        private CheckBox chkAddDto;
        private CheckBox chkDto;
        private TextBox txtProperties;
        private Button btnBrowse;
        private TextBox txtPath;
        private FolderBrowserDialog folderBrowserDialog;
        private TextBox txtNamespace;
        private TextBox txtEntityName;
        private TextBox txtModuleName;
        private CheckBox chkInterface;
    }
}