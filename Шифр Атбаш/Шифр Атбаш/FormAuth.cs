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
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            InitializeComponent();
        }

        private SQLiteConnection Db;
        private void Form1_Load(object sender, EventArgs e)
        {
            Db = new SQLiteConnection("Data Source=DataBas.db; Version=3");
            Db.Open();

        }

       
        private void buttonAuth_Click(object sender, EventArgs e)
        {
            string hash_text = Hash(textBoxPass.Text);
            if (textBoxLogin.Text != null && textBoxPass.Text != null)
            {
                SQLiteCommand cnd = Db.CreateCommand();
            cnd.CommandText = "SELECT * FROM user WHERE login like @userlogin and password like @userPass";
            cnd.Parameters.Add("@userlogin", System.Data.DbType.String).Value = textBoxLogin.Text;
                cnd.Parameters.Add("@userPass", System.Data.DbType.String).Value =
               hash_text;
                SQLiteDataReader sqlreader = cnd.ExecuteReader();
                if (sqlreader.HasRows)
                {
                    //Db.Close();
                    this.Hide();
                    var formMain1 = new Приложение();
                    formMain1.Closed += (s, args) => this.Close();
                    formMain1.Show();
                }
                else
                {
                    MessageBox.Show("Error occured...");
                }

            }

        }
        private string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private void buttonReg_Click(object sender, EventArgs e)
        {
            if (new User(textBoxLogin.Text, textBoxPass.Text).CheckUser())
            {
                Db.Close();
              
            this.Hide();
                var formMain = new Registration();
                formMain.Closed += (s, args) => this.Close();
                formMain.Show();
            }
            else
            {
                this.Close();
            }
        }
       
    private void buttonCancel_Click(object sender, EventArgs e)
        {
            Db.Close();
            this.Close();
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
