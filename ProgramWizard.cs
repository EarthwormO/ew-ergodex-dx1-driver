﻿using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DX1Interface;

namespace DX1Utility
{
    public partial class ProgramWizard : Form
    {

        //Tab Order when clicking Next
        static int[] SingleKey = new int[4] {0, 1, 5, 6};
        static int[] MultiKey = new int[4] { 0, 2, 5, 6 };
        static int[] MacroKey = new int[3] { 0, 3, 6 };
        static int[] SpecialKey = new int[3] { 0, 4, 6 };

        private int[] CurrentTabOrder;
        private int CurrentIndex;
        private KeyMap CurrentKeyMap = new KeyMap();

        public ProgramWizard()
        {
            InitializeComponent();
        }

        public void InitProgramWizard(byte Dx1Key)
        {
            CurrentKeyMap.Dx1Key = Dx1Key;
            T_DXKey.Text = Dx1Key.ToString();
            BuildMacroList();
        }

        public void InitKeyProperties(KeyMap CurrentKey)
        {
            //Assign the CurrentKeyMap to the entire KeyMap passed in
            CurrentKeyMap = CurrentKey;
            
            //Go straight to the Confimation Page and populate it
            TB_Wizard.SelectedIndex = 6;
            T_Conf_Type.Text = "Ignore me";
            T_Conf_Desc.Text = CurrentKeyMap.Description;
            T_Conf_Actual.Text = "Ignore me";
            //Enable the textboxes

            BuildMacroList();
        }

        public KeyMap WizardResult()
        {
            //Return the resulting KeyMap
            return CurrentKeyMap;
        }

        private void BuildMacroList()
        {
            //Build the Macro list
            MacroList.Items.Clear();
            //MacroList.Items.Add("NEW MACRO");

            // Add the files from the directory
            if (!System.IO.Directory.Exists(Globals.macroDir))
                System.IO.Directory.CreateDirectory(Globals.macroDir);

            String[] files = System.IO.Directory.GetFiles(Globals.macroDir, "*.mac");
            foreach (String name in files)
            {
                System.IO.FileStream stream = new System.IO.FileStream(name, System.IO.FileMode.Open);
                MacroPlayer.MacroDefinition macro = MacroPlayer.MacroDefinition.Read(stream);
                stream.Close();

                String[] pathComponents = name.Split('\\');
                pathComponents = pathComponents.Last().Split('.');
                macro.name = pathComponents.First();

                MacroList.Items.Add(macro.name);
            }
        }

        private void B_Next_Click(object sender, EventArgs e)
        {
            //Code to handle stepping forward through Tabs
            switch (TB_Wizard.SelectedIndex)
            {
                case 0:
                    {
                        //If Leaving the Welcome screen set current Order
                        if (R_Single.Checked)
                        {
                            CurrentTabOrder = SingleKey;
                            T_Conf_Type.Text = "Single Key";
                        }
                        else if (R_Multi.Checked)
                        {
                            CurrentTabOrder = MultiKey;
                            T_Conf_Type.Text = "Multi Key Macro";
                        }
                        else if (R_Macro.Checked)
                        {
                            CurrentTabOrder = MacroKey;
                            T_Conf_Type.Text = "Timed Macro";
                        }
                        else
                        {
                            CurrentTabOrder = SpecialKey;
                            T_Conf_Type.Text = "Special Function";
                        }
                        //Enable Back Button before leaving welcome tab
                        B_Back.Enabled = true;
                        CurrentIndex = 1; 
                        break;
                    }
                case 1:
                    {
                        //Leaving Single Key tab
                        T_Conf_Actual.Text = T_Key.Text;
                        CurrentIndex++;
                        break;
                    }
                case 3:
                    {
                        //Leaving Macro Tab
                        CurrentIndex++;
                        break;
                    }
                case 5:
                    {
                        //Leaving Description tab
                        CurrentKeyMap.Description = T_Description.Text;
                        T_Conf_Desc.Text = CurrentKeyMap.Description;
                        CurrentIndex++;
                        break;
                    }
                default:
                    {
                        //Move to the Next tab in this sequence
                        CurrentIndex++;
                        break;
                    }
            }

            //Set Next Tab according to Current Wizard type
            TB_Wizard.SelectedIndex = CurrentTabOrder[CurrentIndex];
            
            if (TB_Wizard.SelectedIndex == 6)
                {
                    //On Confirmation page Change buttons
                    B_Next.Enabled = false;
                    B_OK.Enabled = true;
                }
        }

        private void B_Back_Click(object sender, EventArgs e)
        {
            //Code to handle stepping backward through Tabs
            if (TB_Wizard.SelectedIndex == 6)
            {
                //Leaving Confirmation Page, re-enable Next button
                B_Next.Enabled = true;
            }
            CurrentIndex--;

            //Set Previous Tab according to Current Wizard type
            TB_Wizard.SelectedIndex = CurrentTabOrder[CurrentIndex];

            if (TB_Wizard.SelectedIndex == 0)
            {
                //Back on Welcome Tab, disable Back and Finish Button
                B_Back.Enabled = false;
                B_OK.Enabled = false;
            }

        }

        // Allows us to get current keyboard state.
        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] lpKeyState);

        private void ProgramWizard_KeyDown(object sender, KeyEventArgs e)
        {
            switch(TB_Wizard.SelectedIndex)
            {
                case 1:
                    {
                        //On Single Key programming, capture all keystrokes and assign last keystroke to key
                        
                        int tempkey = (int)e.KeyCode;
                        T_Key.Text = e.KeyCode.ToString();

                        // Seperate left and right shift/ctrl/alt
                        if (tempkey >= 0x10 && tempkey <= 0x12)
                        {
                            Byte[] state = new Byte[256];
                            GetKeyboardState(state);
                            tempkey = 0xa0 + 2 * (tempkey - 0x10);
                            if ((state[tempkey + 1] & 0x80) != 0)
                                tempkey++;          // RHS version
                        }
                        CurrentKeyMap.Description = e.KeyCode.ToString();
                        
                        T_Description.Text = CurrentKeyMap.Description;
                        T_Conf_Desc.Text = CurrentKeyMap.Description;
                        if (!CurrentKeyMap.AssignSingleKey(tempkey))
                        {
                            //Error converting key
                            T_Key.Text = "Error";
                            T_Description.Text = "Error converting Key";
                        }
                        e.SuppressKeyPress = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }


        }

        private void MacroList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //The Selection within the Macro List has changed
            if(MacroList.SelectedIndex >= 0)
            {
                CurrentKeyMap.Action = 0;
                CurrentKeyMap.Type = 0x3;
                CurrentKeyMap.Description = MacroList.SelectedItem.ToString();
                T_Description.Text = CurrentKeyMap.Description;
                T_Conf_Desc.Text = CurrentKeyMap.Description;
            }

        }

    }
}
