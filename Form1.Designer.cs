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
            this.ButtonList = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.EditMacros = new System.Windows.Forms.Button();
            this.MacroList = new System.Windows.Forms.ListBox();
            this.V_Profiles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.B_EditProfile = new System.Windows.Forms.Button();
            this.C_Debug = new System.Windows.Forms.CheckBox();
            this.B_Delete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonList
            // 
            this.ButtonList.Location = new System.Drawing.Point(12, 34);
            this.ButtonList.Multiline = true;
            this.ButtonList.Name = "ButtonList";
            this.ButtonList.ReadOnly = true;
            this.ButtonList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ButtonList.Size = new System.Drawing.Size(178, 408);
            this.ButtonList.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 94);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.MacroList.Location = new System.Drawing.Point(409, 34);
            this.MacroList.Name = "MacroList";
            this.MacroList.Size = new System.Drawing.Size(188, 407);
            this.MacroList.TabIndex = 8;
            this.MacroList.SelectedIndexChanged += new System.EventHandler(this.MacroList_SelectedIndexChanged);
            // 
            // V_Profiles
            // 
            this.V_Profiles.FormattingEnabled = true;
            this.V_Profiles.Location = new System.Drawing.Point(75, 9);
            this.V_Profiles.Name = "V_Profiles";
            this.V_Profiles.Size = new System.Drawing.Size(121, 21);
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
            this.B_EditProfile.Location = new System.Drawing.Point(202, 8);
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
            this.C_Debug.Location = new System.Drawing.Point(517, 9);
            this.C_Debug.Name = "C_Debug";
            this.C_Debug.Size = new System.Drawing.Size(58, 17);
            this.C_Debug.TabIndex = 13;
            this.C_Debug.Text = "Debug";
            this.C_Debug.UseVisualStyleBackColor = true;
            this.C_Debug.CheckedChanged += new System.EventHandler(this.C_Debug_CheckedChanged);
            // 
            // B_Delete
            // 
            this.B_Delete.Location = new System.Drawing.Point(234, 8);
            this.B_Delete.Name = "B_Delete";
            this.B_Delete.Size = new System.Drawing.Size(51, 23);
            this.B_Delete.TabIndex = 14;
            this.B_Delete.Text = "Delete";
            this.B_Delete.UseVisualStyleBackColor = true;
            this.B_Delete.Click += new System.EventHandler(this.B_Delete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 497);
            this.Controls.Add(this.B_Delete);
            this.Controls.Add(this.C_Debug);
            this.Controls.Add(this.B_EditProfile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.V_Profiles);
            this.Controls.Add(this.MacroList);
            this.Controls.Add(this.EditMacros);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ButtonList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(745, 526);
            this.MinimumSize = new System.Drawing.Size(645, 526);
            this.Name = "Form1";
            this.Text = "DX1Utility";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ButtonList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button EditMacros;
        private System.Windows.Forms.ListBox MacroList;
        private System.Windows.Forms.ComboBox V_Profiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_EditProfile;
        private System.Windows.Forms.CheckBox C_Debug;
        private System.Windows.Forms.Button B_Delete;
    }
}

