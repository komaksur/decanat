﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace decanat.obj
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();

            SQLiteConnection con = new SQLiteConnection(@"data source=decan.db");
            con.Open();

            string query = "SELECT Номер_Студента, Имя||\" \"|| Фамилия as name  FROM Студенты ";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            SQLiteCommand CreateCommand = new SQLiteCommand(query, con);


            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(CreateCommand);
            adapter.Fill(dt);

            comboBox_student.DataSource = dt;
            comboBox_student.DisplayMember = "name";
            comboBox_student.ValueMember = "Номер_Студента";

        }

        private void menu_Load(object sender, EventArgs e)
        {
            button_update_student_Click(sender, e);
        }

        private void button_update_student_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source=decan.db");
            con.Open();

            string query = "SELECT * FROM Студенты ";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            System.Data.DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);

            dataGridView_student.DataSource = dt;

            con.Close();
        }

        private void button_update_disciplines_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source=decan.db");
            con.Open();

            string query = "SELECT *  FROM Дисциплины ";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            System.Data.DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);

            dataGridView_disciplines.DataSource = dt;

            con.Close();
        }

        private void button_update_evaluations_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source=decan.db");
            con.Open();

            string query = "SELECT Номер, Имя ||\" \"|| Фамилия AS ФИО, Название, Оценка FROM Студенты INNER JOIN Оценки ON Студенты.Номер_Студента = Оценки.Номер_Студента INNER JOIN Дисциплины ON Дисциплины.Номер_Дисциплины = Оценки.Номер_Дисциплины";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            System.Data.DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);

            dataGridView_evaluations.DataSource = dt;

            con.Close();
        }
        private void tabControl_menu_SelectedIndexChanged(object sender, EventArgs e)
        {

            Console.WriteLine(tabControl_menu.SelectedIndex);
            if (tabControl_menu.SelectedIndex == 0)
            {
                button_update_student_Click(sender, e);
            }
            else if (tabControl_menu.SelectedIndex == 1)
            {
                button_update_disciplines_Click(sender, e);
            }
            else if (tabControl_menu.SelectedIndex == 2)
            {
                button_update_evaluations_Click(sender, e);
            }
        }

        private void button_add_student_Click(object sender, EventArgs e)
        {

            student_add windows = new student_add();
            DialogResult res = windows.ShowDialog();
            if (res == DialogResult.OK)
            {
                button_update_student_Click(sender, e);
            }
        }

        private void button_edit_student_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView_student.CurrentRow.Cells[0].Value.ToString());
            edit_student windows = new edit_student(id);
            DialogResult res = windows.ShowDialog();
            if (res == DialogResult.OK)
            {
                button_update_student_Click(sender, e);
            }
        }

        private void button_delete_student_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView_student.CurrentRow.Cells[0].Value.ToString());
            SQLiteConnection con = new SQLiteConnection("data source = decan.db");
            con.Open();

            var res = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                string sql = "DELETE FROM Студенты WHERE Номер_Студента = " + id;
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                button_update_student_Click(sender, e);
            }
            con.Close();
        }

        private void button_add_disciplines_Click(object sender, EventArgs e)
        {
            disciplines_add windows = new disciplines_add();
            DialogResult res = windows.ShowDialog();
            if (res == DialogResult.OK)
            {
                button_update_disciplines_Click(sender, e);
            }
        }

        private void button_edit_disciplines_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView_disciplines.CurrentRow.Cells[0].Value.ToString());
            disciplines_edit windows = new disciplines_edit(id);
            DialogResult res = windows.ShowDialog();
            if (res == DialogResult.OK)
            {
                button_update_disciplines_Click(sender, e);
            }
        }

        private void button_delete_disciplines_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView_disciplines.CurrentRow.Cells[0].Value.ToString());
            SQLiteConnection con = new SQLiteConnection("data source = decan.db");
            con.Open();

            var res = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                string sql = "DELETE FROM Дисциплины WHERE Номер_Дисциплины = " + id;
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                button_update_disciplines_Click(sender, e);
            }
            con.Close();
        }

        private void button_add_evaluations_Click(object sender, EventArgs e)
        {
            evaluations_add windows = new evaluations_add();
            DialogResult res = windows.ShowDialog();
            if (res == DialogResult.OK)
            {
                button_update_evaluations_Click(sender, e);
            }
        }

        private void button_edit_evaluations_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView_evaluations.CurrentRow.Cells[0].Value.ToString());
            evaluations_edit windows = new evaluations_edit(id);
            DialogResult res = windows.ShowDialog();
            if (res == DialogResult.OK)
            {
                button_update_evaluations_Click(sender, e);
            }
        }

        private void button_delete_evaluations_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView_evaluations.CurrentRow.Cells[0].Value.ToString());
            SQLiteConnection con = new SQLiteConnection("data source = decan.db");
            con.Open();

            var res = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                string sql = "DELETE FROM Оценки WHERE Номер = " + id;
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                button_update_evaluations_Click(sender, e);
            }
            con.Close();
        }

        private void button_student_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"data source=decan.db");
            con.Open();

            string query = "SELECT Дисциплины.Название, Оценки.Оценка FROM Студенты INNER JOIN Оценки ON Студенты.Номер_Студента =  Оценки.Номер_Студента INNER JOIN Дисциплины ON Оценки.Номер_Дисциплины = Дисциплины.Номер_Дисциплины WHERE Студенты.Номер_Студента =  '" + comboBox_student.SelectedValue.ToString() + "' ";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            System.Data.DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}