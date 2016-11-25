using GameSlam.Core.Enums;
using GameSlam.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GameSlam.Infrastructure.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

            // disable lazy loading via virtual to foreign keys
            this.Configuration.LazyLoadingEnabled = false;

            this.SetCommandTimeOut(300);
        }
        public void SetCommandTimeOut(int Timeout)
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Timeout;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<GameDetail> GameDetails { get; set; }
        public DbSet<PublicResponse> PublicResponse { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApprovalStatus> ApprovalStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /*
            modelBuilder.Entity<Course>()
                 .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                 .Map(t => t.MapLeftKey("CourseID")
                     .MapRightKey("InstructorID")
                     .ToTable("CourseInstructor"));
             */
            //modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}
