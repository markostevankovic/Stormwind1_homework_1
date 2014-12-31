using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Stormwind_domaci
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\pc\Desktop\Stormwind_domaci\Stormwind_domaci\Database1.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmd.Connection = cn;
            loadlist();
        }

        private void button_Insert_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "")
            {
                cn.Open();
                cmd.CommandText = "INSERT into Movies (Id, Name) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "') ";
                cmd.ExecuteNonQuery();
                cmd.Clone();
                cn.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                loadlist();
            }
        }

        private void loadlist()
        {
            listBox1.Items.Clear();
            cn.Open();
            cmd.CommandText = "SELECT * FROM Movies";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[0].ToString() + ". " + dr[1].ToString());
                }
            }
            cn.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox l = sender as ListBox;
            if (l.SelectedIndex != -1)
            {
                listBox1.SelectedIndex = l.SelectedIndex;
                //nisam hteo 2 listBoxa, pa cu se sad muciti sa ovim substring-ovima
                string s = listBox1.SelectedItem.ToString();
                int position = s.IndexOf(". ");
                string number = s.Substring(0, position);
                string name_of_movie = s.Substring(s.IndexOf(".") + 2);
                textBox1.Text = number.ToString();
                textBox2.Text = name_of_movie;
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "")
            {
                cn.Open();
                cmd.CommandText = "DELETE from Movies WHERE Id='" + textBox1.Text + "' and Name='" + textBox2.Text + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                textBox1.Text = textBox2.Text = "";
                loadlist();
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "" & listBox1.SelectedIndex != -1)
            {
                cn.Open();
                string s = listBox1.SelectedItem.ToString();
                int position = s.IndexOf(". ");
                int number = Convert.ToInt32(s.Substring(0, position));
                string name_of_movie = s.Substring(s.IndexOf(".") + 2);
                cmd.CommandText = "UPDATE Movies SET Id='" + textBox1.Text + "',Name='" + textBox2.Text + "' WHERE Id='" + number + "' and Name='" + name_of_movie + "'";
                cmd.ExecuteNonQuery();
                cn.Close();
                textBox1.Text = textBox2.Text = "";
                loadlist();
            }
        }
    }
}
