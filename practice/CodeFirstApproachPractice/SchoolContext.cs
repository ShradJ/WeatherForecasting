using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachPractice
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base()
{
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
