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
    public partial class FormMain : Form
    {
        public static int sortCheckBox = 0;
        private int minNumOfPosts = 0;
        private int numberOfPosts = 0;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            
            comboBox1.SelectedIndex = 6;
            this.akutalizujIloscPostow();
            this.odswiez();

        }

        private void akutalizujIloscPostow()
        {
            using (var db = new BlogContext())
            {
                int iloscPostow = db.Posts.Count();
                this.numberOfPosts = iloscPostow;
                label6.Text = numberOfPosts.ToString();
            }
        }

        //dodaj Post
        private void button4_Click(object sender, EventArgs e)
        {
            FormAddPost f = new FormAddPost();
            f.ShowDialog();
            this.akutalizujIloscPostow();
            this.odswiez();

        }
        //dodaj Blog
        private void button1_Click(object sender, EventArgs e)
        {
            FormAddBlog f = new FormAddBlog();
            f.ShowDialog();
            this.odswiez();
        }
        //zamknij okno
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void odswiez()
        {
            using (var db = new BlogContext())
            {
                if (sortCheckBox == 0)
                {
                    dataGridView1.DataSource = db.Blogs.Where(b => b.IloscPostow >= minNumOfPosts).ToList();
                }
                else if (sortCheckBox == 1)
                {
                    dataGridView1.DataSource = db.Blogs.Where(b => b.IloscPostow >= minNumOfPosts).OrderByDescending(b => b.BlogId).ToList();

                }
                else if (sortCheckBox == 2)
                {
                    dataGridView1.DataSource = db.Blogs.Where(b => b.IloscPostow >= minNumOfPosts).OrderBy(b => b.Name).ToList();

                }
                else if (sortCheckBox == 3)
                {
                    dataGridView1.DataSource = db.Blogs.Where(b => b.IloscPostow >= minNumOfPosts).OrderByDescending(b => b.Name).ToList();

                }
                else if (sortCheckBox == 4)
                {
                    dataGridView1.DataSource = db.Blogs.Where(b => b.IloscPostow >= minNumOfPosts).OrderBy(b => b.Url).ToList();

                }
                else if (sortCheckBox == 5)
                {

                    dataGridView1.DataSource = db.Blogs.Where(b => b.IloscPostow >= minNumOfPosts).OrderByDescending(b => b.Url).ToList();
                }
                else if (sortCheckBox == 6)
                {

                    dataGridView1.DataSource = db.Blogs.Where(b => b.IloscPostow >= minNumOfPosts).OrderByDescending(b => b.IloscPostow).ToList();
                }
                else if (sortCheckBox == 7)
                {

                    dataGridView1.DataSource = db.Blogs.Where(b => b.IloscPostow >= minNumOfPosts).OrderBy(b => b.IloscPostow).ToList();
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sortCheckBox = comboBox1.SelectedIndex;

            this.odswiez();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //dodaj usera
        private void button5_Click(object sender, EventArgs e)
        {
            FormAddUser f = new FormAddUser();
            f.ShowDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.minNumOfPosts = int.Parse(numericUpDown1.Value.ToString());
            this.odswiez();
        }
        //find particular user posts
        private void button3_Click(object sender, EventArgs e)
        {
            FormFindUserPosts f = new FormFindUserPosts();
            f.ShowDialog();
        }
        //delete particular blog
        private void button6_Click(object sender, EventArgs e)
        {
            FormDeleteBlog f = new FormDeleteBlog();
            f.ShowDialog();
            this.akutalizujIloscPostow();
            this.odswiez();
        }

    }
}
