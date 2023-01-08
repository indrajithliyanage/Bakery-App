using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
                    numericUpDown2.Value = 0;
                    numericUpDown3.Value = 0.00M;
                }
            }
            else
            {
                numericUpDown1.Value = 0.00M;
                numericUpDown2.Value = 0;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0 && numericUpDown1.Value != 0 && numericUpDown2.Value != 0 && numericUpDown3.Value != 0)
            {
                var item = itemList.FirstOrDefault(i => i.ItemName == comboBox1.SelectedItem.ToString());
                if (item != null)
                {
                    dataGridView1.Rows.Add(item.ItemID, comboBox1.SelectedItem.ToString(), numericUpDown1.Value, numericUpDown2.Value, numericUpDown3.Value);
                }
                else
                {
                    MessageBox.Show("Something went Wrong! Couldn't get the Item ID");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    numericUpDown4.Value = numericUpDown4.Value - Convert.ToDecimal(row.Cells[4].Value);
                    dataGridView1.Rows.RemoveAt(row.Index);
                }
            }
            else
            {
                MessageBox.Show("Please Select a Row!");
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                decimal sum = 0.00M;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                }
                numericUpDown4.Value = sum;
                numericUpDown6.Value = numericUpDown4.Value - (numericUpDown4.Value * (numericUpDown5.Value / 100));
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown4.Value != 0.00M)
            {
                numericUpDown6.Value = numericUpDown4.Value - (numericUpDown4.Value * (numericUpDown5.Value / 100));
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown7.Value > 0.00M && numericUpDown7.Value > numericUpDown6.Value)
            {
                numericUpDown8.Value = numericUpDown7.Value - numericUpDown6.Value;
            }
        }

        private void numericUpDown5_Enter(object sender, EventArgs e)
        {
            if (numericUpDown4.Value != 0.00M)
            {
                numericUpDown6.Value = numericUpDown4.Value - (numericUpDown4.Value * (numericUpDown5.Value / 100));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton1.ForeColor = Control.DefaultForeColor;
            radioButton2.ForeColor = Control.DefaultForeColor;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Please Select");
            comboBox1.SelectedIndex = 0;
            foreach (var item in itemList)
            {
                comboBox1.Items.Add(item.ItemName);
            }
            comboBox1.SelectedIndex = 0;
            dataGridView1.Rows.Clear();
            numericUpDown4.Value = 0.00M;
            numericUpDown5.Value = 0.00M;
            numericUpDown6.Value = 0.00M;
            numericUpDown7.Value = 0.00M;
            numericUpDown8.Value = 0.00M;
        }
    }
}