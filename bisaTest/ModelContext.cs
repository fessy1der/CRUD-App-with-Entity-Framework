using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bisaTest
{
    public class ModelContext:DbContext
    {
        public ModelContext() : base("name = cn") { }
        public DbSet<Student> StudentList {get; set;}
    }
}
