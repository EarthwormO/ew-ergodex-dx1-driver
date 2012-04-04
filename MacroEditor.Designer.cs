namespace DX1Utility
{
    partial class MacroEditor
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
            this.MacroName = new System.Windows.Forms.TextBox();
            this.MacroCommands = new System.Windows.Forms.ListView();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.TimedMacro = new System.Windows.Forms.RadioButton();
            this.MultikeyMacro = new System.Windows.Forms.RadioButton();
            this.UseScanCodes = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // MacroName
            // 
            this.MacroName.Location = new System.Drawing.Point(246, 13);
            this.MacroName.MaxLength = 128;
            this.MacroName.Name = "MacroName";
            this.MacroName.Size = new System.Drawing.Size(257, 20);
            this.MacroName.TabIndex = 1;
            // 
            // MacroCommands
            // 
            this.MacroCommands.AutoArrange = false;
            this.MacroCommands.FullRowSelect = true;
            this.MacroCommands.GridLines = true;
            this.MacroCommands.Location = new System.Drawing.Point(13, 13);
            this.MacroCommands.MultiSelect = false;
            this.MacroCommands.Name = "MacroCommands";
            this.MacroCommands.Size = new System.Drawing.Size(227, 456);
            this.MacroCommands.TabIndex = 2;
            this.MacroCommands.UseCompatibleStateImageBehavior = false;
            this.MacroCommands.View = System.Windows.Forms.View.Details;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(262, 342);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(262, 371);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // TimedMacro
            // 
            this.TimedMacro.AutoSize = true;
            this.TimedMacro.Location = new System.Drawing.Point(318, 98);
            this.TimedMacro.Name = "TimedMacro";
            this.TimedMacro.Size = new System.Drawing.Size(87, 17);
            this.TimedMacro.TabIndex = 5;
            this.TimedMacro.TabStop = true;
            this.TimedMacro.Text = "Timed Macro";
            this.TimedMacro.UseVisualStyleBackColor = true;
            // 
            // MultikeyMacro
            // 
            this.MultikeyMacro.AutoSize = true;
            this.MultikeyMacro.Location = new System.Drawing.Point(318, 131);
            this.MultikeyMacro.Name = "MultikeyMacro";
            this.MultikeyMacro.Size = new System.Drawing.Size(99, 17);
            this.MultikeyMacro.TabIndex = 6;
            this.MultikeyMacro.TabStop = true;
            this.MultikeyMacro.Text = "Multi key macro";
            this.MultikeyMacro.UseVisualStyleBackColor = true;
            // 
            // UseScanCodes
            // 
            this.UseScanCodes.AutoSize = true;
            this.UseScanCodes.Location = new System.Drawing.Point(318, 175);
            this.UseScanCodes.Name = "UseScanCodes";
            this.UseScanCodes.Size = new System.Drawing.Size(102, 17);
            this.UseScanCodes.TabIndex = 7;
            this.UseScanCodes.Text = "Use Scancodes";
            this.UseScanCodes.UseVisualStyleBackColor = true;
            // 
            // MacroEditor
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(669, 481);
            this.Controls.Add(this.UseScanCodes);
            this.Controls.Add(this.MultikeyMacro);
            this.Controls.Add(this.TimedMacro);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.MacroCommands);
            this.Controls.Add(this.MacroName);
            this.KeyPreview = true;
            this.Name = "MacroEditor";
            this.Text = "MacroEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MacroName;
        private System.Windows.Forms.ListView MacroCommands;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.RadioButton TimedMacro;
        private System.Windows.Forms.RadioButton MultikeyMacro;
        private System.Windows.Forms.CheckBox UseScanCodes;
    }
}