using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DX1Utility
{
    public partial class MacroEditor : Form
    {
        public MacroEditor()
        {
            InitializeComponent();

            MacroCommands.Columns.Add("Time");
            MacroCommands.Columns.Add("Action");
            MacroCommands.Columns.Add("KeyCode");
            ListViewItem item = new ListViewItem(""+0);
            item.SubItems.Add("KeyDown");
            item.SubItems.Add(""+0x21);

            MacroCommands.Items.Add(item);
            MacroCommands.MouseDoubleClick+=new MouseEventHandler(MacroCommands_MouseDoubleClick);


        }

        void ValidateMacroTiming()
        {
            uint time = 0;
            foreach(ListViewItem item in MacroCommands.Items)
            {
                if (UInt32.Parse(item.SubItems[0].Text) < time)
                    item.SubItems[0].Text = ("" + time);
                time = UInt32.Parse(item.SubItems[0].Text);
            }

        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {                
                if (MacroCommands.SelectedIndices.Count > 0)
                {
                    int index = MacroCommands.SelectedIndices[0];
                    MacroCommands.Items.RemoveAt(index);
                }

            }
            if (e.KeyCode == System.Windows.Forms.Keys.Insert)
            {
                if (MacroCommands.SelectedIndices.Count > 0)
                {
                    int index = MacroCommands.SelectedIndices[0];
                    ListViewItem item = new ListViewItem("" + 0);
                    item.SubItems.Add("KeyDown");
                    item.SubItems.Add("" + 0x21);
                    MacroCommands.Items.Insert(index, item);
                    ValidateMacroTiming();
                }
                else if (MacroCommands.Focused)
                {
                    ListViewItem item = new ListViewItem("" + 0);
                    item.SubItems.Add("KeyDown");
                    item.SubItems.Add("" + 0x21);
                    MacroCommands.Items.Add(item);
                    ValidateMacroTiming();
                }

            }

            
            base.OnKeyDown(e);
        }



        public void RebuildFromMacroDefinition(MacroPlayer.MacroDefinition macro)
        {
            MacroCommands.Items.Clear();
            if (macro.sequence != null)
            {
                for (int i = 0; i < macro.sequence.Length; i++)
                {
                    MacroPlayer.KeySequenceEntry entry = macro.sequence[i];
                    ListViewItem item = new ListViewItem("" + entry.time);
                    item.SubItems.Add(entry.keyUp ? "KeyUp" : "KeyDown");
                    item.SubItems.Add("" + entry.keyCode);

                    MacroCommands.Items.Add(item);
                }
            }
            MacroName.Text = macro.name;
            TimedMacro.Checked = (macro.macroType & MacroPlayer.MacroDefinition.MacroType.kMacroMultiKey) == 0;
            MultikeyMacro.Checked = !TimedMacro.Checked;
            UseScanCodes.Checked = (macro.macroType & MacroPlayer.MacroDefinition.MacroType.kMacroUseScanCodes) != 0;

        }

         public MacroPlayer.MacroDefinition GetMacroDefinition()
         {
             List<MacroPlayer.KeySequenceEntry> keyList = new List<MacroPlayer.KeySequenceEntry>();
             foreach (ListViewItem item in MacroCommands.Items)
             {
                 uint time = UInt32.Parse(item.SubItems[0].Text);
                 bool keyUp = item.SubItems[1].Text == "KeyUp";
                 Byte keyCode = Byte.Parse(item.SubItems[2].Text);

                 keyList.Add(new MacroPlayer.KeySequenceEntry(time, keyUp, keyCode));

             }
             String name = MacroName.Text;
             MacroPlayer.MacroDefinition.MacroType type = 0;
             type |= MultikeyMacro.Checked ? MacroPlayer.MacroDefinition.MacroType.kMacroMultiKey : 0;
             type |= UseScanCodes.Checked ? MacroPlayer.MacroDefinition.MacroType.kMacroUseScanCodes : 0;

             return new MacroPlayer.MacroDefinition(name, keyList.ToArray(), type);
         }

        private void MacroCommands_MouseDoubleClick(object sender, EventArgs e)
        {
            MacroActionEditor edit = new MacroActionEditor();
            ListViewItem item = MacroCommands.SelectedItems[0];
            edit.Init(item);
            edit.Location = System.Windows.Forms.Control.MousePosition;
            if (edit.ShowDialog() == DialogResult.OK)
            {
                item = edit.GetListViewItem();
                MacroCommands.Items[MacroCommands.SelectedIndices[0]] = item;
                ValidateMacroTiming();
            }
            MacroCommands.Invalidate();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

        }


    }
}
