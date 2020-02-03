using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Data.Models;

namespace Data.DataAccessLayer
{
    public class GarmentContext : IdentityDbContext<ApplicationUser>
    {
        public GarmentContext()
            : base("GarmentContext")
        {

        }

        public static GarmentContext Create()
        {
            return new GarmentContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("IdentityUserClaim");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("IdentityUserLogin");
            //modelBuilder.Entity<IdentityRole>().ToTable("IdentityRole");
            //modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("IdentityUserRole");       
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Goal> Goals { get; set; }

        public DbSet<GoalDetail> GoalDetails { get; set; }

        public DbSet<ProduceHistory> ProduceHistories { get; set; }

        public DbSet<LatestSummary> LatestSummaries { get; set; }

        public DbSet<GoalDetailTemplate> GoalDetailTemplates { get; set; }

        public DbSet<TeamSetting> TeamSettings { get; set; }

        public DbSet<Factory> Factories { get; set; }

        public DbSet<Reason> Reasons { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<GoalDetailRevision> GoalDetailRevisions { get; set; }

        public DbSet<ProductDetail> ProductDetails { get; set; } 

        public DbSet<FactorySession> FactorySessions { get; set; }

        public DbSet<TeamSession> TeamSessions { get; set; }

        public DbSet<QC> QCs { get; set; }

        public DbSet<QCTeam> QCTeams { get; set; }

        public DbSet<QCReport> QCReports { get; set; }
        public DbSet<QCError> QCErrors { get; set; }
        public DbSet<QCSpecification> QCSpecifications { get; set; }
    }
}
