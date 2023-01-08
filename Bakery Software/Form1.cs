using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bakery_Software
{
    public partial class Form1 : Form
    {
        private List<PopulateItems_Result> itemList = new List<PopulateItems_Result>();

        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Green;
            radioButton2.ForeColor = Control.DefaultForeColor;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Please Select");
            comboBox1.SelectedIndex = 0;
            var catOneList = itemList.Where(i => i.CategoryID_FK == 1);
            foreach (var item in catOneList)
            {
                comboBox1.Items.Add(item.ItemName);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.ForeColor = Color.Green;
            radioButton1.ForeColor = Control.DefaultForeColor;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Please Select");
            comboBox1.SelectedIndex = 0;
            var catTwoList = itemList.Where(i => i.CategoryID_FK == 2);
            foreach (var item in catTwoList)
            {
                comboBox1.Items.Add(item.ItemName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (BakeryDBEntities db = new BakeryDBEntities())
            {
                itemList = db.PopulateItems(null).ToList();
                comboBox1.Items.Add("Please Select");
                comboBox1.SelectedIndex = 0;
                foreach (var item in itemList)
                {
                    comboBox1.Items.Add(item.ItemName);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                var item = itemList.FirstOrDefault(i => i.ItemName == comboBox1.SelectedItem.ToString());
                if (item != null)
                {
                    numericUpDown1.Value = item.ItemPrice;
                }
            }
            else
            {
                numericUpDown1.Value = 0.00M;
                numericUpDown2.Value = 0.00M;
                numericUpDown3.Value = 0.00M;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0.00M && numericUpDown2.Value != 0)
            {
                numericUpDown3.Value = numericUpDown1.Value * numericUpDown2.Value;
            }
        }
    }
}