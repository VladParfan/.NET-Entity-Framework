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
    public partial class FormAddPost : Form
    {
        public FormAddPost()
        {
            InitializeComponent();
        }

        private void FormAddPost_Load(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new BlogContext())
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("wymagane jest podanie id bloga, do ktorego ma byc przypisany post!");
                    return;
                }
                else
                {
                    try
                    {
                        int id = int.Parse(textBox1.Text);
                        var query = from b in db.Blogs
                                    where b.BlogId == id
                                    select b.BlogId;
                        if (!query.Contains<int>(id))
                        {
                            MessageBox.Show("blog o podanym id nie istnieje!");
                            return;
                        }
                        
                    }
                    catch {
                        MessageBox.Show("blog o podanym id nie istnieje! (prawdopodobnie bledny format id!)");
                        return;
                    }
                    
                }
                if (textBox3.Text == "")
                {
                    MessageBox.Show("wymagane jest podanie tytulu posta!");
                    return;
                }
                if(richTextBox1.Text == "")
                {
                    MessageBox.Show("wymagane jest podanie zawartosci posta!");
                    return;
                }
                else
                {
                    string author;
                    if (textBox2.Text == "")
                    {
                        author = "anonymous";
                        //dodaj anonymousa do db
                        try
                        {
                            var anonymousID = from u in db.Users
                                         where u.UserName == author
                                         select u.UserName;
                            if (!anonymousID.Contains<string>(author))
                            {
                                var user = new User();
                                user.Description = "anonymous user";
                                user.UserName = author;
                                db.Users.Add(user);
                                db.SaveChanges();
                            }

                            MessageBox.Show("dodales post jako anonimowy uzytkownik!");
                        }
                        catch
                        {
                            MessageBox.Show("blad przy dodawaniu anonimowego posta!");
                            return;
                        }
                    }
                    else
                    {
                        author = textBox2.Text;
                        try
                        {
                            var query = from u in db.Users
                                              where u.UserName == author
                                              select u.UserName;
                            if (!query.Contains<string>(author))
                            {
                                MessageBox.Show("podany uzytkownik nie istnieje!");
                                return;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("blad przy dodawaniu posta (walidacja uzytkownika)!");
                            return;
                        }
                    }
                    var post = new Post();
                    int blogid = int.Parse(textBox1.Text);
                    post.BlogId =  blogid;
                    db.Blogs.FirstOrDefault(b=>b.BlogId== blogid).IloscPostow+=1;
                    post.UserName = author;
                    post.Title = textBox3.Text;
                    post.Content = richTextBox1.Text;
                    db.Posts.Add(post);
                    db.SaveChanges();
                    MessageBox.Show("Pomyslnie dodano post!");
                    this.Close();
                }
            }
        }

        //blogID       = textbox1
        //author       = textbox2
        //post title   = textbox3
        //text Content = richTextBox1
    }

}