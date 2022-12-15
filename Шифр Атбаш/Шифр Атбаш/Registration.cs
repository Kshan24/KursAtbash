using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SQLite;
namespace Шифр_Атбаш
{
    public partial class Registration : Form
    {
        public SQLiteConnection DataBaseCP;
        public Registration()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password.Text));
            if (Password.Text == PasswordRepeat.Text)
            {
                int temp_login = 0;
                SQLiteCommand commandvar = DataBaseCP.CreateCommand();
                commandvar.CommandText = "select * from StorageDB";
                SQLiteDataReader SQL = commandvar.ExecuteReader(); while
                (SQL.Read())
                   
             {
                    if (Login.Text == SQL[1].ToString())
                    {
                        MessageBox.Show($"Такой пользователь уже существует");
                        temp_login = 1;
                        break;
                    }
                }
                if (temp_login == 0)
                {
                    SQLiteCommand commandvarreg = DataBaseCP.CreateCommand();
                    commandvarreg.CommandText = "insert into StorageDB(Login, Password) values(@login, @password)";
                commandvarreg.Parameters.Add("@login", DbType.String).Value = Login.Text;
                    commandvarreg.Parameters.Add("@password", DbType.String).Value =
                   Convert.ToBase64String(hash);
                    commandvarreg.ExecuteNonQuery();
                    MessageBox.Show($"Поздравляем с регистрацией, {Login.Text}");
                    Hide();
                    Form1 code = new Form1();
                    code.ShowDialog();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
            }
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            Hide();
            FormAuth auth = new FormAuth();
            auth.ShowDialog();
            Close();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            DataBaseCP = new SQLiteConnection("DataSource=DataBaseCP.db; Version = 3");
            DataBaseCP.Open();
        }
        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataBaseCP.Close();
        }
    }
}
