// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Models;
namespace WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Login> dataoric { get; set; }

        public DbSet<ric_form_1> ric_form_1 { get; set; }
        public DbSet<ResearchProjectSubmittedHEC> ResearchProjectSubmittedHEC { get; set; }
        public DbSet<ResearchProjectSubmittedNonHEC> ResearchProjectSubmittedNonHEC { get; set; }
        public DbSet<ResearchProjectApprovedHEC> ResearchProjectApprovedHEC { get; set; }
        public DbSet<ResearchProjectApprovedNonHEC> ResearchProjectApprovedNonHEC { get; set; }
        public DbSet<HECFundedResearchProjectCompleted> HECFundedResearchProjectCompleted { get; set; }
        public DbSet<NonHECFundedResearchProjectCompleted> NonHECFundedResearchProjectCompleted { get; set; }
        public DbSet<JointResearchProjectsSubmitted> JointResearchProjectsSubmitted { get; set; }
        public DbSet<JointResearchProjectsApproved> JointResearchProjectsApproved { get; set; }
        public DbSet<JointResearchProjectsCompleted> JointResearchProjectsCompleted { get; set; }
        public DbSet<ContractResearchAwarded> ContractResearchAwarded { get; set; }
        public DbSet<PolicyAdvocacies> PolicyAdvocacies { get; set; }
        public DbSet<ResearchLinks> ResearchLinks { get; set; }
        public DbSet<CivicEngagementEvents> CivicEngagements { get; set; }
        public DbSet<ConsultancyContracts> ConsultancyContracts { get; set; }
        public DbSet<LiaisonsASRB> LiaisonsASRB { get; set; }



        public DbSet<ric_form_2> ric_form_2 { get; set; }

        public DbSet<RicForm3> ric_form_3 { get; set; }
        public DbSet<FacultyStartups> FacultyStartups { get; set; }
        public DbSet<SpinOffs> SpinOffs { get; set; }
        public DbSet<Funding> Funding { get; set; }
        public DbSet<Events> Events { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Events>().ToTable("events");
            modelBuilder.Entity<FacultyStartups>().ToTable("faculty_startups");
            modelBuilder.Entity<SpinOffs>().ToTable("spin_offs");
            modelBuilder.Entity<Funding>().ToTable("funding");


            modelBuilder.Entity<ric_form_1>(entity =>
            {
                base.OnModelCreating(modelBuilder);
                entity.ToTable("ric_form_1"); // Match the table name
                entity.HasKey(e => e.ric_form_1_id); // Match the primary key column name
                entity.Property(e => e.ric_form_1_id).HasColumnName("ric_form_1_id");
                entity.Property(e => e.dataoric_id).HasColumnName("dataoric_id");
                entity.Property(e => e.faculty_name).HasColumnName("faculty_name");
                entity.Property(e => e.department_name).HasColumnName("department_name");
                entity.Property(e => e.faculty_email).HasColumnName("faculty_email");
                // Add the rest of the properties in a similar manner...

            });

            {
                modelBuilder.Entity<ResearchProjectSubmittedHEC>()
                    .HasOne(r => r.ric_form_1)
                    .WithMany(f => f.ResearchProjectSubmittedHEC)
                    .HasForeignKey(r => r.ric_form_1_id)
                    .OnDelete(DeleteBehavior.Cascade);
            }

            {
                modelBuilder.Entity<ric_form_2>(entity =>
                {
                    entity.HasKey(e => e.ric_form_2_id); // Define ric_form_2_id as the primary key
                });


            }
        }
    }
}