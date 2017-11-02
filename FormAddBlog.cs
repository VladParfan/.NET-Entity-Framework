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
    public partial class FormAddBlog : Form
    {
        public FormAddBlog()
        {
            InitializeComponent();
        }

        private void FormAddBlog_Load(object sender, EventArgs e)
        {

        }
        //zakoncz
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //zatwierdz
        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new BlogContext())
            {
                if (textBox2.Text == "") {
                    MessageBox.Show("wymagane jest podanie nazwy bloga!");
                }
                else
                {
                    string url;
                    if (textBox1.Text == "")
                    {
                        url = "brak url.";
                    }
                    else
                    {
                        url = textBox1.Text;
                    }
                    var blog = new Blog();
                    blog.Name = textBox2.Text;
                    blog.Url = url;
                    db.Blogs.Add(blog);
                    db.SaveChanges();
                    MessageBox.Show("Pomyslnie dodano blog!");
                    this.Close();
                }
            }
        }
    }
}
