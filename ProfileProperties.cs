﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DX1Utility
{
    public partial class ProfileProperties : Form
    {
        public ProfileProperties()
        {
            InitializeComponent();
            
        }

        public void EditProfile(Profiles CurrentProfile)
        {
            T_ProfileName.Text = CurrentProfile.ProfName;
            T_ProfilePath.Text = CurrentProfile.ProfPath;
            C_Enabled.Checked = CurrentProfile.ProfEnabled;
            if (CurrentProfile.ProfName != "New") { T_ProfileName.Enabled = false; }
        }
        
        public string GetEditedProfile(ref Profiles CurrentProfile)
        {
            CurrentProfile.ProfName  = T_ProfileName.Text;
            CurrentProfile.ProfPath = T_ProfilePath.Text.ToLower();
            CurrentProfile.ProfEnabled = C_Enabled.Checked;
            CurrentProfile.QuickMenu = C_QuickMenu.Checked;
            return T_ProfileName.Text;
        }

        public string GetProfileNameOnly()
        {
            return T_ProfileName.Text;
        }


        private void B_FindPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Executable Files(*.exe)|*.exe";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                T_ProfilePath.Text = dialog.FileName;
            }
        }

        private void B_OK_Click(object sender, EventArgs e)
        {
                            
        }

        private void ProfileProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProfileSearcher Searcher = new ProfileSearcher();

            //Check to ensure name is not "New"
            if (T_ProfileName.Text == "New" && this.DialogResult != DialogResult.Cancel)
            {
                MessageBox.Show("Profile Name cannot be 'New'","",MessageBoxButtons.OK);
                e.Cancel = true;
            }
        }
    }
}
