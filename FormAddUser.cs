using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalEF
{
    public partial class FormAddUser : Form
    {
        public FormAddUser()
        {
            InitializeComponent();
        }

        private void FormAddUser_Load(object sender, EventArgs e)
        {

        }
        //zakoncz
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //zatwierdz
        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new BlogContext())
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("wymagane jest podanie nazwy usera!");
                }
                else
                {
                    string description;
                    if (richTextBox1.Text == "")
                    {
                        description = "brak opisu.";
                    }
                    else
                    {
                        description = richTextBox1.Text;
                    }
                    var query = from u in db.Users
                                where u.UserName == textBox1.Text
                                select u.UserName;
                    if (query.Contains<string>(textBox1.Text))
                    {
                        MessageBox.Show("podany uzytkownik juz istnieje!");
                        return;
                    }
                    else
                    {

                        var user = new User();
                        user.UserName = textBox1.Text;
                        user.Description = description;
                        db.Users.Add(user);
                        db.SaveChanges();
                        MessageBox.Show("Pomyslnie dodano usera!");
                        this.Close();
                    }
                }
            }
        }
    }
}
