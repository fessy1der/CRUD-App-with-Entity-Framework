using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bisaTest
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "PNG |*.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic.Image = Image.FromFile(ofd.FileName);
                    Student obj = studentBindingSource.Current as Student;
                    if (obj != null)
                        obj.ImageUrl = ofd.FileName;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using(ModelContext db = new ModelContext())
            {
                studentBindingSource.DataSource = db.StudentList.ToList();
            }
            pContainer.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pic.Image = null;
            pContainer.Enabled = true;
            studentBindingSource.Add(new Student() { ObjectState = 1});
            studentBindingSource.MoveLast();
            txtFullName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pContainer.Enabled = true;
            txtFullName.Focus();
            Student obj = studentBindingSource.Current as Student;
            if (obj != null)
                obj.ObjectState = 2;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MetroFramework.MetroMessageBox.Show(this,"Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using(ModelContext db = new ModelContext())
                {
                    Student obj = studentBindingSource.Current as Student;
                    if (obj != null)
                    {
                        if (db.Entry<Student>(obj).State == System.Data.Entity.EntityState.Detached)
                            db.Set<Student>().Attach(obj);
                        db.Entry<Student>(obj).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        studentBindingSource.RemoveCurrent();
                        pContainer.Enabled = false;
                        pic.Image = null;
                    } 

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pContainer.Enabled = false;
            studentBindingSource.ResetBindings(false);
            Form1_Load(sender, e);
        }

        private void metroGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Student obj = studentBindingSource.Current as Student;
            if (obj != null)
                pic.Image = Image.FromFile(obj.ImageUrl);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using(ModelContext db  = new ModelContext())
            {
                Student obj = studentBindingSource.Current as Student;
                if (obj != null)
                {
                    if (db.Entry<Student>(obj).State == System.Data.Entity.EntityState.Detached)
                        db.Set<Student>().Attach(obj);
                    if (obj.ObjectState == 1)
                        db.Entry<Student>(obj).State = System.Data.Entity.EntityState.Added;
                    else if (obj.ObjectState == 2)
                        db.Entry<Student>(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    metroGrid1.Refresh();
                    pContainer.Enabled = false;
                    obj.ObjectState = 0;
                }
            }
        }
    }
}
