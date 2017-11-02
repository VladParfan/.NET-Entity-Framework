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
    public partial class FormDeleteBlog : Form
    {
        public FormDeleteBlog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new BlogContext())
            {
                    
                    var query = from b in db.Blogs
                                where b.BlogId.ToString() == textBox1.Text
                                select b.BlogId.ToString();
                    if (!query.Contains<string>(textBox1.Text))
                    {
                        string name = textBox1.Text;
                        query = from b in db.Blogs
                                where b.Name == name
                                select b.Name;
                        if (!query.Contains<string>(name))
                        {
                            MessageBox.Show("blog o podanej nazwie/id nie istnieje!");
                            return;
                        }
                        else
                        {
                            var id = db.Blogs.Where(b => b.Name == name).ToArray()[0].BlogId;
                            foreach (var entity in db.Posts)
                            {
                                if (entity.BlogId == id)
                                {
                                    db.Posts.Remove(entity);
                                }
                            }
                            db.SaveChanges();

                            db.Blogs.Remove(db.Blogs.FirstOrDefault(b => b.Name == name));
                        
                            db.SaveChanges();
                            MessageBox.Show("usunieto blog o nazwie: " + name);
                        }
                    }
                    else
                    {
                        var id = int.Parse(textBox1.Text);
                        //removing posts
                        foreach (var entity in db.Posts)
                        {
                            if(entity.BlogId == id)
                            {
                                db.Posts.Remove(entity);
                            }
                        }
                        db.SaveChanges();
                    //removing blogs
                    
                        db.Blogs.Remove(db.Blogs.FirstOrDefault(b => b.BlogId == id));
                    
                        
                        db.SaveChanges();
                        MessageBox.Show("usunieto blog o id: " + id.ToString());
                    }

                this.Close();






            }
        }
    }
}
