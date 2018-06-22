using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ibby_cms.Entities.Entitites;

namespace ibby_cms.Data.DAL
{
    public class EntitiesContext : DbContext
    {
        public DbSet<PageContentEssence> PageContentEssences { get; set; }
        public DbSet<PageSeoEssence> PageSeoEssences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Course>()
            .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            .Map(t => t.MapLeftKey("CourseID")
            .MapRightKey("InstructorID")
            .ToTable("CourseInstructor"));*/

            modelBuilder.Entity<PageContentEssence>().ToTable("dbo.MainEssence");
            modelBuilder.Entity<PageContentEssence>()
                .HasOptional(c => c.Seo).WithMany(a => a.Main).HasForeignKey(k => k.SeoID);
        }
    }
}