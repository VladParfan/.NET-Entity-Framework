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
    public partial class FormFindUserPosts : Form
    {
        public FormFindUserPosts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new BlogContext())
            {
                var author = textBox1.Text;

                var query = from u in db.Users
                            where u.UserName == author
                            select u.UserName;
                if (!query.Contains<string>(author))
                {
                    MessageBox.Show("podany uzytkownik nie istnieje!");
                    return;
                }

                
                
                var query2 = db.Posts.Where(p => p.UserName == author);
                if (!query2.Any())
                {
                    MessageBox.Show("podany uzytkownik nie napisal zadnych postow!");
                    return;
                }

                dataGridView1.DataSource = query2.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormFindUserPosts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_FinalEF_BlogContextDataSet2.Posts' table. You can move, or remove it, as needed.
            //this.postsTableAdapter.Fill(this._FinalEF_BlogContextDataSet2.Posts);

        }
    }
}
