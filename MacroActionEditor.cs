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
    public partial class MacroActionEditor : Form
    {

        public MacroActionEditor()
        {
            InitializeComponent();

            Time.KeyPress += new KeyPressEventHandler(LimitToNumber);
            textBox1.KeyPress += new KeyPressEventHandler(LimitToNumber);
        }

        private void LimitToNumber(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }


        public void Init(ListViewItem item)
        {
            Time.Text = item.SubItems[0].Text;
            comboBox1.SelectedIndex = item.SubItems[1].Text == "KeyUp" ? 1 : 0;
            textBox1.Text = item.SubItems[2].Text;
        }

        public ListViewItem GetListViewItem()
        {
            ListViewItem item = new ListViewItem("" + UInt32.Parse(Time.Text));
            item.SubItems.Add((String)(comboBox1.Items[comboBox1.SelectedIndex]));
            item.SubItems.Add("" + UInt32.Parse(textBox1.Text));
            return item;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

        }
  
    }
}
