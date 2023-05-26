using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace decanat
{
    public partial class disciplines_edit : Form
    {
        int id { get; set; }
        public disciplines_edit(int id_con)
        {
            InitializeComponent();

            id = id_con;
            SQLiteConnection con = new SQLiteConnection("data source = decan.db");
            con.Open();

            string sql = "SELECT * FROM Дисциплины WHERE Номер_Дисциплины =" + id;
            SQLiteCommand cmd = new SQLiteCommand(sql, con);

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    textBox1.Text = reader.GetString("Название");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection("data source = decan.db");
            con.Open();

            string sql = "UPDATE Дисциплины " +
                "SET Название = '" + textBox1.Text + "' WHERE Номер_Дисциплины = " + id;


            SQLiteCommand cmd = new SQLiteCommand(sql, con);

            cmd.ExecuteNonQuery();

            this.DialogResult = DialogResult.OK;

            con.Close();
            this.Close();
        }
    }
}
