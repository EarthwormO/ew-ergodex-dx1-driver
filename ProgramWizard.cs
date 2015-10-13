using System;
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
        static int[] ToggleKey = new int[3] { 0, 7, 6 };                                //Added for issue #19

        private int[] CurrentTabOrder;
        private int CurrentIndex;
        private KeyMap CurrentKeyMap = new KeyMap();
        private string[] sKeyBindings = new string[] { "", "Single Key", "Modifier Key", "Macro", "Special", "Toggle" };
        private SpecialKeyPlayer _SpecialKeyPlayer;

        public ProgramWizard()
        {
            InitializeComponent();
        }

        public void InitProgramWizard(SpecialKeyPlayer SpecialKeyPlayer, byte Dx1Key)
        {
            CurrentKeyMap.Dx1Key = Dx1Key;
            T_DXKey.Text = Dx1Key.ToString();
            BuildMacroList();
            _SpecialKeyPlayer = SpecialKeyPlayer;

            //Assign Special Key Data Grid
            G_Special.AutoGenerateColumns = false;
            G_Special.DataSource = _SpecialKeyPlayer.SpecialKeys;
            G_Special.AllowUserToResizeRows = false;
            G_Special.AllowUserToResizeColumns = false;
            
            //Add Name Column
            DataGridViewTextBoxColumn DescColumn = new DataGridViewTextBoxColumn();
            DescColumn.Width = 150;
            DescColumn.DataPropertyName = "SpecialName";
            DescColumn.HeaderText = "Name";
            DescColumn.ReadOnly = true;
            G_Special.Columns.Add(DescColumn);

        }

        public void InitKeyProperties(KeyMap CurrentKey)
        {
            //Assign the CurrentKeyMap to the entire KeyMap passed in
            CurrentKeyMap = CurrentKey;
            KeysConverter KC = new KeysConverter();
            
            //Go straight to the Confimation Page and populate it
            TB_Wizard.SelectedIndex = 6;
            T_Conf_Type.Text = sKeyBindings[CurrentKeyMap.Type];
            T_Conf_Desc.Text = CurrentKeyMap.Description;
            T_Conf_Actual.Text = CurrentKeyMap.Action.ToString();
            T_Conf_Desc.Focus();
            T_Conf_Type.Enabled = false;
            T_Conf_Actual.Enabled = false;
            B_Back.Enabled = false;
            B_Next.Enabled = false;
            B_OK.Enabled = true;

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
                        else if (R_Toggle.Checked)
                        {
                            //Added for issue #19
                            CurrentTabOrder = ToggleKey;
                            T_Conf_Type.Text = "Toggle Key";
                        }
                        else
                        {
                            CurrentTabOrder = SpecialKey;
                            T_Conf_Type.Text = "Special";
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
                case 4:
                    {
                        //Leaving Special Tab
                        CurrentIndex++;
                        ProcessExtraData();
                        //assign current data to Key
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
                case 7:
                    {
                        //Added for issue #19
                        //Leaving Toggle tab
                        CurrentKeyMap.Description = T_Description.Text;
                        T_Conf_Actual.Text = T_TKey.Text;
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
                        CurrentKeyMap.KeyName = e.KeyCode.ToString();

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
                case 2:
                        {
                            //On MultiKey programming, capture all keystrokes and assign up to 4 to the L_MultiKey ListBox
                            break;
                        }
                case 7:
                    {
                        //Added for issue #19
                        //On Toggle Key programming, capture all keystrokes and assign last keystroke to key

                        int tempkey = (int)e.KeyCode;
                        T_TKey.Text = e.KeyCode.ToString();
                        // Seperate left and right shift/ctrl/alt
                        if (tempkey >= 0x10 && tempkey <= 0x12)
                        {
                            Byte[] state = new Byte[256];
                            GetKeyboardState(state);
                            tempkey = 0xa0 + 2 * (tempkey - 0x10);
                            if ((state[tempkey + 1] & 0x80) != 0)
                                tempkey++;          // RHS version
                        }

                        CurrentKeyMap.Description = e.KeyCode.ToString() + " (T)";
                        CurrentKeyMap.KeyName = e.KeyCode.ToString() + " (T)";
                        CurrentKeyMap.Action = (byte)tempkey;
                        CurrentKeyMap.Type = 5;

                        T_Description.Text = CurrentKeyMap.Description;
                        T_Conf_Desc.Text = CurrentKeyMap.Description;

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
                CurrentKeyMap.MacroName = MacroList.SelectedItem.ToString();
                T_Description.Text = CurrentKeyMap.Description;
                T_Conf_Desc.Text = CurrentKeyMap.Description;
            }

        }

        private void B_OK_Click(object sender, EventArgs e)
        {
            CurrentKeyMap.Description = T_Conf_Desc.Text;

        }

        private void G_Special_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Force selection of the row that was clicked on no matter which mouse button
            if (e.RowIndex >= 0)
            {
                G_Special.CurrentCell = G_Special.Rows[e.RowIndex].Cells[e.ColumnIndex];
                
                //Check to determine if any additional information is needed
                if (_SpecialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ReqData)
                {
                    //Get extra data from user based on ExtraDataType
                    switch (_SpecialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ExtraDataType)
                    {
                        case DX1Utility.SpecialKey.SpecialKeysExtraData.Boolean:
                            {
                                //Use the True False Radio Buttons
                                RB_True.Visible = true;
                                RB_False.Visible = true;

                                //Get the names to be used instead of True False fromt he ExtraDataDictionary
                                RB_True.Text = _SpecialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ExtraDataParams["True"];
                                RB_False.Text = _SpecialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ExtraDataParams["False"];
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    //MessageBox.Show("Extra Data Type =" + );
                }

                //Assign Default Description of DX1 Key to current Special Key name
                CurrentKeyMap.Type = 4;
                CurrentKeyMap.Action = (byte)G_Special.CurrentRow.Index;
                CurrentKeyMap.Description = G_Special.Rows[G_Special.CurrentRow.Index].Cells[0].Value.ToString();
                T_Description.Text = CurrentKeyMap.Description;
                T_Conf_Desc.Text = CurrentKeyMap.Description;
                
                

            }


        }

        private void ProcessExtraData()
        {
            //Custom process to Take the extra data for Special Keys and convert it to CustomData for the CurrentKey
            switch (_SpecialKeyPlayer.SpecialKeys[G_Special.CurrentRow.Index].ExtraDataType)
            {
                case DX1Utility.SpecialKey.SpecialKeysExtraData.Boolean:
                    {
                        //For a Boolean Extra data, pass in the value of Checked for True
                        CurrentKeyMap.CustomData = _SpecialKeyPlayer.GetCustomData(G_Special.CurrentRow.Index, RB_True.Checked.ToString());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

    }
}
