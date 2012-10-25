namespace DX1Utility
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.B_QuickPrg = new System.Windows.Forms.Button();
            this.EditMacros = new System.Windows.Forms.Button();
            this.MacroList = new System.Windows.Forms.ListBox();
            this.V_Profiles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.B_EditProfile = new System.Windows.Forms.Button();
            this.C_Debug = new System.Windows.Forms.CheckBox();
            this.B_Delete = new System.Windows.Forms.Button();
            this.G_KeyMap = new System.Windows.Forms.DataGridView();
            this.CM_KeyMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.G_KeyMap)).BeginInit();
            this.CM_KeyMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_QuickPrg
            // 
            this.B_QuickPrg.Location = new System.Drawing.Point(195, 426);
            this.B_QuickPrg.Name = "B_QuickPrg";
            this.B_QuickPrg.Size = new System.Drawing.Size(89, 31);
            this.B_QuickPrg.TabIndex = 0;
            this.B_QuickPrg.TabStop = false;
            this.B_QuickPrg.Text = "Quick Program";
            this.B_QuickPrg.UseVisualStyleBackColor = true;
            this.B_QuickPrg.Click += new System.EventHandler(this.button1_Click);
            // 
            // EditMacros
            // 
            this.EditMacros.Location = new System.Drawing.Point(474, 445);
            this.EditMacros.Name = "EditMacros";
            this.EditMacros.Size = new System.Drawing.Size(75, 23);
            this.EditMacros.TabIndex = 7;
            this.EditMacros.Text = "Edit Macro";
            this.EditMacros.UseVisualStyleBackColor = true;
            this.EditMacros.Click += new System.EventHandler(this.EditMacros_Click);
            // 
            // MacroList
            // 
            this.MacroList.FormattingEnabled = true;
            this.MacroList.Location = new System.Drawing.Point(450, 37);
            this.MacroList.Name = "MacroList";
            this.MacroList.Size = new System.Drawing.Size(148, 407);
            this.MacroList.TabIndex = 8;
            this.MacroList.SelectedIndexChanged += new System.EventHandler(this.MacroList_SelectedIndexChanged);
            // 
            // V_Profiles
            // 
            this.V_Profiles.FormattingEnabled = true;
            this.V_Profiles.Location = new System.Drawing.Point(76, 10);
            this.V_Profiles.Name = "V_Profiles";
            this.V_Profiles.Size = new System.Drawing.Size(254, 21);
            this.V_Profiles.Sorted = true;
            this.V_Profiles.TabIndex = 9;
            this.V_Profiles.SelectionChangeCommitted += new System.EventHandler(this.V_Profiles_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Profiles:";
            // 
            // B_EditProfile
            // 
            this.B_EditProfile.Location = new System.Drawing.Point(336, 8);
            this.B_EditProfile.Name = "B_EditProfile";
            this.B_EditProfile.Size = new System.Drawing.Size(26, 23);
            this.B_EditProfile.TabIndex = 11;
            this.B_EditProfile.Text = "...";
            this.B_EditProfile.UseVisualStyleBackColor = true;
            this.B_EditProfile.Click += new System.EventHandler(this.B_EditProfile_Click);
            // 
            // C_Debug
            // 
            this.C_Debug.AutoSize = true;
            this.C_Debug.Location = new System.Drawing.Point(565, 12);
            this.C_Debug.Name = "C_Debug";
            this.C_Debug.Size = new System.Drawing.Size(58, 17);
            this.C_Debug.TabIndex = 13;
            this.C_Debug.Text = "Debug";
            this.C_Debug.UseVisualStyleBackColor = true;
            this.C_Debug.CheckedChanged += new System.EventHandler(this.C_Debug_CheckedChanged);
            // 
            // B_Delete
            // 
            this.B_Delete.Location = new System.Drawing.Point(368, 8);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(51, 23);
            this.B_Delete.TabIndex = 14;
            this.B_Delete.Text = "Delete";
            this.B_Delete.UseVisualStyleBackColor = true;
            this.B_Delete.Click += new System.EventHandler(this.B_Delete_Click);
            // 
            // G_KeyMap
            // 
            this.G_KeyMap.AllowUserToAddRows = false;
            this.G_KeyMap.AllowUserToDeleteRows = false;
            this.G_KeyMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.G_KeyMap.ContextMenuStrip = this.CM_KeyMap;
            this.G_KeyMap.Location = new System.Drawing.Point(20, 37);
            this.G_KeyMap.MultiSelect = false;
            this.G_KeyMap.Name = "G_KeyMap";
            this.G_KeyMap.RowHeadersVisible = false;
            this.G_KeyMap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.G_KeyMap.Size = new System.Drawing.Size(169, 420);
            this.G_KeyMap.TabIndex = 15;
            this.G_KeyMap.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.G_KeyMap_CellFormatting);
            this.G_KeyMap.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.G_KeyMap_CellMouseDown);
            // 
            // CM_KeyMap
            // 
            this.CM_KeyMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.PropertiesStripMenuItem});
            this.CM_KeyMap.Name = "CM_KeyMap";
            this.CM_KeyMap.Size = new System.Drawing.Size(128, 70);
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.programToolStripMenuItem.Text = "Program";
            this.programToolStripMenuItem.Click += new System.EventHandler(this.programToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // PropertiesStripMenuItem
            // 
            this.PropertiesStripMenuItem.Name = "PropertiesStripMenuItem";
            this.PropertiesStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.PropertiesStripMenuItem.Text = "Properties";
            this.PropertiesStripMenuItem.Click += new System.EventHandler(this.PropertiesStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.G_KeyMap);
            this.Controls.Add(this.MacroList);
            this.Controls.Add(this.C_Debug);
            this.Controls.Add(this.B_Delete);
            this.Controls.Add(this.V_Profiles);
            this.Controls.Add(this.B_EditProfile);
            this.Controls.Add(this.EditMacros);
            this.Controls.Add(this.B_QuickPrg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(745, 526);
            this.MinimumSize = new System.Drawing.Size(645, 526);
            this.Name = "Form1";
            this.Text = "Dx1Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.G_KeyMap)).EndInit();
            this.CM_KeyMap.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_QuickPrg;
        private System.Windows.Forms.Button EditMacros;
        private System.Windows.Forms.ListBox MacroList;
        private System.Windows.Forms.ComboBox V_Profiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_EditProfile;
        private System.Windows.Forms.CheckBox C_Debug;
        private System.Windows.Forms.Button B_Delete;
        private System.Windows.Forms.DataGridView G_KeyMap;
        private System.Windows.Forms.ContextMenuStrip CM_KeyMap;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PropertiesStripMenuItem;
    }
}

