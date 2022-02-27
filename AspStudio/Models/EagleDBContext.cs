using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EagleApp.Models
{
    public partial class EagleDBContext : DbContext
    {
        public EagleDBContext()
        {
        }

        public EagleDBContext(DbContextOptions<EagleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<BidLog> BidLogs { get; set; }
        public virtual DbSet<Competitor> Competitors { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<JeffJob> JeffJobs { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobLog> JobLogs { get; set; }
        public virtual DbSet<JobLog1222021> JobLog1222021s { get; set; }
        public virtual DbSet<JobLog2132022> JobLog2132022s { get; set; }
        public virtual DbSet<JobLog21920222> JobLog21920222s { get; set; }
        public virtual DbSet<JobLogBackup> JobLogBackups { get; set; }
        public virtual DbSet<JobStatus> JobStatuses { get; set; }
        public virtual DbSet<Joblink> Joblinks { get; set; }
        public virtual DbSet<Ocawa> Ocawas { get; set; }
        public virtual DbSet<Sheet1> Sheet1s { get; set; }
        public virtual DbSet<Sheet2> Sheet2s { get; set; }
        public virtual DbSet<Sheet3> Sheet3s { get; set; }
        public virtual DbSet<Sheet4> Sheet4s { get; set; }
        public virtual DbSet<Sheet5> Sheet5s { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<VDashboardDataBackup> VDashboardDataBackups { get; set; }
        public virtual DbSet<VDashboardDatum> VDashboardData { get; set; }
        public virtual DbSet<VGetJobLog> VGetJobLogs { get; set; }
        public virtual DbSet<VGetJobLogs2172022> VGetJobLogs2172022s { get; set; }
        public virtual DbSet<Wip> Wips { get; set; }
        public virtual DbSet<Wip2> Wip2s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:eagleappdbserver.database.windows.net,1433;Initial Catalog=EagleDB;Persist Security Info=False;User ID=eagleadmin;Password=P@ssword#1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.LogAction).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.JobLog)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.JobLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditLog_JobLog");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AuditLog_AspNetUsers");
            });

            modelBuilder.Entity<BidLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BidLogs$");

                entity.Property(e => e.AcceptedDate)
                    .HasMaxLength(255)
                    .HasColumnName("Accepted Date");

                entity.Property(e => e.AwardedTo)
                    .HasMaxLength(255)
                    .HasColumnName("Awarded  To");

                entity.Property(e => e.Bid).HasColumnName("Bid #");

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.CloseoutsDoneDate)
                    .HasMaxLength(255)
                    .HasColumnName("Closeouts Done Date");

                entity.Property(e => e.CollectedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Collected Amount");

                entity.Property(e => e.CommPaidDate)
                    .HasMaxLength(255)
                    .HasColumnName("Comm Paid Date");

                entity.Property(e => e.CompetitorPrice)
                    .HasMaxLength(255)
                    .HasColumnName("Competitor Price");

                entity.Property(e => e.Contact).HasMaxLength(255);

                entity.Property(e => e.DeadRejected).HasColumnName("Dead Rejected");

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.EagleSBidFormPrice)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Price");

                entity.Property(e => e.EagleSBidFormSales)
                    .HasMaxLength(255)
                    .HasColumnName("Eagle's BID FORM Sales");

                entity.Property(e => e.FinalInvoice)
                    .HasMaxLength(255)
                    .HasColumnName("Final Invoice #");

                entity.Property(e => e.FinalInvoicedDate)
                    .HasMaxLength(255)
                    .HasColumnName("FINAL Invoiced Date");

                entity.Property(e => e.FinishDate)
                    .HasMaxLength(255)
                    .HasColumnName("Finish Date");

                entity.Property(e => e.JeipMeetingDate)
                    .HasMaxLength(255)
                    .HasColumnName("JEIP Meeting Date");

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.MissedBy)
                    .HasMaxLength(255)
                    .HasColumnName("Missed By %");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OpenDate)
                    .HasMaxLength(255)
                    .HasColumnName("Open Date");

                entity.Property(e => e.OriginalOrder).HasColumnName("Original Order");

                entity.Property(e => e.PaidInFullDate)
                    .HasMaxLength(255)
                    .HasColumnName("Paid in Full Date");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(255)
                    .HasColumnName("Product Type");

                entity.Property(e => e.ProjectJobClient)
                    .HasMaxLength(255)
                    .HasColumnName("Project # : Job : Client");

                entity.Property(e => e.ProjectOcAwa)
                    .HasMaxLength(255)
                    .HasColumnName("Project OC / AWA");

                entity.Property(e => e.Rep).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(255)
                    .HasColumnName("Start Date");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Competitor>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("ContactType");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(200);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Currency).HasMaxLength(50);

                entity.Property(e => e.CustomerClass).HasMaxLength(200);

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.Terms).HasMaxLength(50);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<JeffJob>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BidNumber).HasMaxLength(255);

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.Rep).HasMaxLength(255);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.JobName).HasMaxLength(100);
            });

            modelBuilder.Entity<JobLog>(entity =>
            {
                entity.ToTable("JobLog");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.AwardedTo).HasMaxLength(100);

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName).HasMaxLength(100);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseoutDoneDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.CommPaidDate).HasColumnType("datetime");

                entity.Property(e => e.CompetitorPrice).HasColumnType("money");

                entity.Property(e => e.Contact).HasMaxLength(10);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DemoDoneDate).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(10);

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.EquipReturnDate).HasColumnType("datetime");

                entity.Property(e => e.FinalInvoice).HasMaxLength(100);

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.InvReturnDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasMaxLength(100);

                entity.Property(e => e.MobilizationDate).HasColumnType("datetime");

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.PercentageDone).HasMaxLength(10);

                entity.Property(e => e.Prep12).HasColumnName("Prep1/2");

                entity.Property(e => e.Prep12Date).HasColumnType("datetime");

                entity.Property(e => e.Prep14).HasColumnName("Prep1/4");

                entity.Property(e => e.Prep14Date).HasColumnType("datetime");

                entity.Property(e => e.Prep34).HasColumnName("Prep3/4");

                entity.Property(e => e.Prep34Date).HasColumnType("datetime");

                entity.Property(e => e.PrepDoneDate).HasColumnType("datetime");

                entity.Property(e => e.ProductType).HasMaxLength(100);

                entity.Property(e => e.ProjectOc)
                    .HasMaxLength(1000)
                    .HasColumnName("ProjectOC");

                entity.Property(e => e.Removal12).HasColumnName("Removal 1/2");

                entity.Property(e => e.Removal12Date).HasColumnType("datetime");

                entity.Property(e => e.RemovalDoneDate).HasColumnType("datetime");

                entity.Property(e => e.Rep).HasMaxLength(1000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.JobLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_JobLog_AspNetUsers");
            });

            modelBuilder.Entity<JobLog1222021>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("JobLog_1222021");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.AwardedTo).HasMaxLength(100);

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName).HasMaxLength(100);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseoutDoneDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.CommPaidDate).HasColumnType("datetime");

                entity.Property(e => e.CompetitorPrice).HasColumnType("money");

                entity.Property(e => e.Contact).HasMaxLength(10);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(10);

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.FinalInvoice).HasMaxLength(100);

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasMaxLength(100);

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.ProductType).HasMaxLength(100);

                entity.Property(e => e.ProjectOc)
                    .HasMaxLength(1000)
                    .HasColumnName("ProjectOC");

                entity.Property(e => e.Rep).HasMaxLength(1000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<JobLog2132022>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("JobLog_2132022");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.AwardedTo).HasMaxLength(100);

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName).HasMaxLength(100);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseoutDoneDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.CommPaidDate).HasColumnType("datetime");

                entity.Property(e => e.CompetitorPrice).HasColumnType("money");

                entity.Property(e => e.Contact).HasMaxLength(10);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DemoDoneDate).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(10);

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.EquipReturnDate).HasColumnType("datetime");

                entity.Property(e => e.FinalInvoice).HasMaxLength(100);

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvReturnDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasMaxLength(100);

                entity.Property(e => e.MobilizationDate).HasColumnType("datetime");

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.PercentageDone).HasMaxLength(10);

                entity.Property(e => e.Prep12).HasColumnName("Prep1/2");

                entity.Property(e => e.Prep12Date).HasColumnType("datetime");

                entity.Property(e => e.Prep14).HasColumnName("Prep1/4");

                entity.Property(e => e.Prep14Date).HasColumnType("datetime");

                entity.Property(e => e.Prep34).HasColumnName("Prep3/4");

                entity.Property(e => e.Prep34Date).HasColumnType("datetime");

                entity.Property(e => e.PrepDoneDate).HasColumnType("datetime");

                entity.Property(e => e.ProductType).HasMaxLength(100);

                entity.Property(e => e.ProjectOc)
                    .HasMaxLength(1000)
                    .HasColumnName("ProjectOC");

                entity.Property(e => e.Removal12).HasColumnName("Removal 1/2");

                entity.Property(e => e.Removal12Date).HasColumnType("datetime");

                entity.Property(e => e.RemovalDoneDate).HasColumnType("datetime");

                entity.Property(e => e.Rep).HasMaxLength(1000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<JobLog21920222>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("JobLog_21920222");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.AwardedTo).HasMaxLength(100);

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName).HasMaxLength(100);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseoutDoneDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.CommPaidDate).HasColumnType("datetime");

                entity.Property(e => e.CompetitorPrice).HasColumnType("money");

                entity.Property(e => e.Contact).HasMaxLength(10);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DemoDoneDate).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(10);

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.EquipReturnDate).HasColumnType("datetime");

                entity.Property(e => e.FinalInvoice).HasMaxLength(100);

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvReturnDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasMaxLength(100);

                entity.Property(e => e.MobilizationDate).HasColumnType("datetime");

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.PercentageDone).HasMaxLength(10);

                entity.Property(e => e.Prep12).HasColumnName("Prep1/2");

                entity.Property(e => e.Prep12Date).HasColumnType("datetime");

                entity.Property(e => e.Prep14).HasColumnName("Prep1/4");

                entity.Property(e => e.Prep14Date).HasColumnType("datetime");

                entity.Property(e => e.Prep34).HasColumnName("Prep3/4");

                entity.Property(e => e.Prep34Date).HasColumnType("datetime");

                entity.Property(e => e.PrepDoneDate).HasColumnType("datetime");

                entity.Property(e => e.ProductType).HasMaxLength(100);

                entity.Property(e => e.ProjectOc)
                    .HasMaxLength(1000)
                    .HasColumnName("ProjectOC");

                entity.Property(e => e.Removal12).HasColumnName("Removal 1/2");

                entity.Property(e => e.Removal12Date).HasColumnType("datetime");

                entity.Property(e => e.RemovalDoneDate).HasColumnType("datetime");

                entity.Property(e => e.Rep).HasMaxLength(1000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<JobLogBackup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("JobLog_backup");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseoutDoneDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.CommPaidDate).HasColumnType("datetime");

                entity.Property(e => e.CompetitorPrice).HasColumnType("money");

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasColumnType("money");

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectOc).HasColumnName("ProjectOC");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<JobStatus>(entity =>
            {
                entity.ToTable("JobStatus");
            });

            modelBuilder.Entity<Joblink>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(255)
                    .HasColumnName("BidNUmber");

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.JobFolderLink).HasColumnName("Job Folder Link");

                entity.Property(e => e.Rep).HasMaxLength(255);
            });

            modelBuilder.Entity<Ocawa>(entity =>
            {
                entity.ToTable("OCAWA");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Ocawaname)
                    .HasMaxLength(200)
                    .HasColumnName("OCAWAName");
            });

            modelBuilder.Entity<Sheet1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sheet1$");

                entity.Property(e => e.AcceptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Accepted Date");

                entity.Property(e => e.AwardedTo)
                    .HasMaxLength(255)
                    .HasColumnName("Awarded  To");

                entity.Property(e => e.Bid).HasColumnName("Bid #");

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.CloseoutsDoneDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Closeouts Done Date");

                entity.Property(e => e.CollectedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Collected Amount");

                entity.Property(e => e.CommPaidDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Comm Paid Date");

                entity.Property(e => e.CompetitorPrice)
                    .HasMaxLength(255)
                    .HasColumnName("Competitor Price");

                entity.Property(e => e.Contact).HasMaxLength(255);

                entity.Property(e => e.DeadRejected).HasColumnName("Dead Rejected");

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.EagleSBidFormPrice)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Price");

                entity.Property(e => e.EagleSBidFormSales)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Sales");

                entity.Property(e => e.FinalInvoice)
                    .HasMaxLength(255)
                    .HasColumnName("Final Invoice #");

                entity.Property(e => e.FinalInvoicedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FINAL Invoiced Date");

                entity.Property(e => e.FinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Finish Date");

                entity.Property(e => e.JeipMeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIP Meeting Date");

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.JobFolderLink)
                    .HasMaxLength(255)
                    .HasColumnName("Job Folder Link");

                entity.Property(e => e.MissedBy).HasColumnName("Missed By %");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OpenDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Open Date");

                entity.Property(e => e.PaidInFullDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Paid in Full Date");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(255)
                    .HasColumnName("Product Type");

                entity.Property(e => e.ProjectJobClient)
                    .HasMaxLength(255)
                    .HasColumnName("Project # : Job : Client");

                entity.Property(e => e.ProjectOcAwa)
                    .HasMaxLength(255)
                    .HasColumnName("Project OC / AWA");

                entity.Property(e => e.Rep).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start Date");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Sheet2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sheet2$");

                entity.Property(e => e.AcceptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Accepted Date");

                entity.Property(e => e.AwardedTo)
                    .HasMaxLength(255)
                    .HasColumnName("Awarded  To");

                entity.Property(e => e.Bid).HasColumnName("Bid #");

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.CloseoutsDoneDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Closeouts Done Date");

                entity.Property(e => e.CollectedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Collected Amount");

                entity.Property(e => e.CommPaidDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Comm Paid Date");

                entity.Property(e => e.CompetitorPrice)
                    .HasMaxLength(255)
                    .HasColumnName("Competitor Price");

                entity.Property(e => e.Contact).HasMaxLength(255);

                entity.Property(e => e.DeadRejected).HasColumnName("Dead Rejected");

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.EagleSBidFormPrice)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Price");

                entity.Property(e => e.EagleSBidFormSales)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Sales");

                entity.Property(e => e.FinalInvoice)
                    .HasMaxLength(255)
                    .HasColumnName("Final Invoice #");

                entity.Property(e => e.FinalInvoicedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FINAL Invoiced Date");

                entity.Property(e => e.FinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Finish Date");

                entity.Property(e => e.JeipMeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIP Meeting Date");

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.JobFolderLink)
                    .HasMaxLength(255)
                    .HasColumnName("Job Folder Link");

                entity.Property(e => e.MissedBy).HasColumnName("Missed By %");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OpenDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Open Date");

                entity.Property(e => e.PaidInFullDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Paid in Full Date");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(255)
                    .HasColumnName("Product Type");

                entity.Property(e => e.ProjectJobClient)
                    .HasMaxLength(255)
                    .HasColumnName("Project # : Job : Client");

                entity.Property(e => e.ProjectOcAwa)
                    .HasMaxLength(255)
                    .HasColumnName("Project OC / AWA");

                entity.Property(e => e.Rep).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start Date");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Sheet3>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sheet3$");

                entity.Property(e => e.AcceptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Accepted Date");

                entity.Property(e => e.AwardedTo)
                    .HasMaxLength(255)
                    .HasColumnName("Awarded  To");

                entity.Property(e => e.Bid).HasColumnName("Bid #");

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.CloseoutsDoneDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Closeouts Done Date");

                entity.Property(e => e.CollectedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Collected Amount");

                entity.Property(e => e.CommPaidDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Comm Paid Date");

                entity.Property(e => e.CompetitorPrice)
                    .HasMaxLength(255)
                    .HasColumnName("Competitor Price");

                entity.Property(e => e.Contact).HasMaxLength(255);

                entity.Property(e => e.DeadRejected).HasColumnName("Dead Rejected");

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.EagleSBidFormPrice)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Price");

                entity.Property(e => e.EagleSBidFormSales)
                    .HasMaxLength(255)
                    .HasColumnName("Eagle's BID FORM Sales");

                entity.Property(e => e.FinalInvoice)
                    .HasMaxLength(255)
                    .HasColumnName("Final Invoice #");

                entity.Property(e => e.FinalInvoicedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FINAL Invoiced Date");

                entity.Property(e => e.FinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Finish Date");

                entity.Property(e => e.JeipMeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIP Meeting Date");

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.JobFolderLink)
                    .HasMaxLength(255)
                    .HasColumnName("Job Folder Link");

                entity.Property(e => e.MissedBy)
                    .HasMaxLength(255)
                    .HasColumnName("Missed By %");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OpenDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Open Date");

                entity.Property(e => e.OriginalOrder).HasColumnName("Original Order");

                entity.Property(e => e.PaidInFullDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Paid in Full Date");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(255)
                    .HasColumnName("Product Type");

                entity.Property(e => e.ProjectJobClient)
                    .HasMaxLength(255)
                    .HasColumnName("Project # : Job : Client");

                entity.Property(e => e.ProjectOcAwa)
                    .HasMaxLength(255)
                    .HasColumnName("Project OC / AWA");

                entity.Property(e => e.Rep).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start Date");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Sheet4>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sheet4$");

                entity.Property(e => e.AcceptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Accepted Date");

                entity.Property(e => e.AwardedTo)
                    .HasMaxLength(255)
                    .HasColumnName("Awarded  To");

                entity.Property(e => e.Bid).HasColumnName("Bid #");

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.CloseoutsDoneDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Closeouts Done Date");

                entity.Property(e => e.CollectedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Collected Amount");

                entity.Property(e => e.CommPaidDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Comm Paid Date");

                entity.Property(e => e.CompetitorPrice)
                    .HasMaxLength(255)
                    .HasColumnName("Competitor Price");

                entity.Property(e => e.Contact).HasMaxLength(255);

                entity.Property(e => e.DeadRejected).HasColumnName("Dead Rejected");

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.EagleSBidFormPrice)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Price");

                entity.Property(e => e.EagleSBidFormSales)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Sales");

                entity.Property(e => e.FinalInvoice)
                    .HasMaxLength(255)
                    .HasColumnName("Final Invoice #");

                entity.Property(e => e.FinalInvoicedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FINAL Invoiced Date");

                entity.Property(e => e.FinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Finish Date");

                entity.Property(e => e.JeipMeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIP Meeting Date");

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.JobFolderLink)
                    .HasMaxLength(255)
                    .HasColumnName("Job Folder Link");

                entity.Property(e => e.MissedBy).HasColumnName("Missed By %");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OpenDate)
                    .HasColumnType("money")
                    .HasColumnName("Open Date");

                entity.Property(e => e.PaidInFullDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Paid in Full Date");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(255)
                    .HasColumnName("Product Type");

                entity.Property(e => e.ProjectJobClient)
                    .HasMaxLength(255)
                    .HasColumnName("Project # : Job : Client");

                entity.Property(e => e.ProjectOcAwa)
                    .HasMaxLength(255)
                    .HasColumnName("Project OC / AWA");

                entity.Property(e => e.Rep).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start Date");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Sheet5>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sheet5$");

                entity.Property(e => e.AcceptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Accepted Date");

                entity.Property(e => e.AwardedTo)
                    .HasMaxLength(255)
                    .HasColumnName("Awarded  To");

                entity.Property(e => e.Bid).HasColumnName("Bid #");

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.CloseoutsDoneDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Closeouts Done Date");

                entity.Property(e => e.CollectedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Collected Amount");

                entity.Property(e => e.CommPaidDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Comm Paid Date");

                entity.Property(e => e.CompetitorPrice)
                    .HasMaxLength(255)
                    .HasColumnName("Competitor Price");

                entity.Property(e => e.Contact).HasMaxLength(255);

                entity.Property(e => e.DeadRejected).HasColumnName("Dead Rejected");

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.EagleSBidFormPrice)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Price");

                entity.Property(e => e.EagleSBidFormSales)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Sales");

                entity.Property(e => e.FinalInvoice)
                    .HasMaxLength(255)
                    .HasColumnName("Final Invoice #");

                entity.Property(e => e.FinalInvoicedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("FINAL Invoiced Date");

                entity.Property(e => e.FinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Finish Date");

                entity.Property(e => e.JeipMeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIP Meeting Date");

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.JobFolderLink)
                    .HasMaxLength(255)
                    .HasColumnName("Job Folder Link");

                entity.Property(e => e.MissedBy).HasColumnName("Missed By %");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OpenDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Open Date");

                entity.Property(e => e.PaidInFullDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Paid in Full Date");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(255)
                    .HasColumnName("Product Type");

                entity.Property(e => e.ProjectJobClient)
                    .HasMaxLength(255)
                    .HasColumnName("Project # : Job : Client");

                entity.Property(e => e.ProjectOcAwa)
                    .HasMaxLength(255)
                    .HasColumnName("Project OC / AWA");

                entity.Property(e => e.Rep).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start Date");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Type1)
                    .HasMaxLength(200)
                    .HasColumnName("Type");
            });

            modelBuilder.Entity<VDashboardDataBackup>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_dashboard_data_backup");

                entity.Property(e => e.AcceptedDate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Accepted Date");

                entity.Property(e => e.AwardedTo)
                    .HasMaxLength(255)
                    .HasColumnName("Awarded  To");

                entity.Property(e => e.Bid).HasColumnName("Bid #");

                entity.Property(e => e.Client).HasMaxLength(255);

                entity.Property(e => e.CollectedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Collected Amount");

                entity.Property(e => e.CommPaidDate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Comm Paid Date");

                entity.Property(e => e.CompetitorPrice)
                    .HasMaxLength(255)
                    .HasColumnName("Competitor Price");

                entity.Property(e => e.Contact).HasMaxLength(255);

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.EagleSBidFormPrice)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Price");

                entity.Property(e => e.EagleSBidFormSales)
                    .HasMaxLength(255)
                    .HasColumnName("Eagle's BID FORM Sales");

                entity.Property(e => e.FinalInvoice)
                    .HasMaxLength(255)
                    .HasColumnName("Final Invoice #");

                entity.Property(e => e.FinalInvoicedDate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("FINAL Invoiced Date");

                entity.Property(e => e.FinishDate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Finish Date");

                entity.Property(e => e.Job).HasMaxLength(255);

                entity.Property(e => e.MissedBy)
                    .HasMaxLength(255)
                    .HasColumnName("Missed By %");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OpenDate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Open Date");

                entity.Property(e => e.OriginalOrder).HasColumnName("Original Order");

                entity.Property(e => e.PaidInFullDate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Paid in Full Date");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(255)
                    .HasColumnName("Product Type");

                entity.Property(e => e.ProjectJobClient)
                    .HasMaxLength(255)
                    .HasColumnName("Project # : Job : Client");

                entity.Property(e => e.ProjectOcAwa)
                    .HasMaxLength(255)
                    .HasColumnName("Project OC / AWA");

                entity.Property(e => e.Rep).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Start Date");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<VDashboardDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_dashboard_data");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.AwardedTo).HasMaxLength(100);

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseoutDoneDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.CommPaidDate).HasColumnType("datetime");

                entity.Property(e => e.CompetitorPrice).HasColumnType("money");

                entity.Property(e => e.Contact).HasMaxLength(200);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.FinalInvoice).HasMaxLength(100);

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasMaxLength(100);

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.ProductType).HasMaxLength(100);

                entity.Property(e => e.ProjectOc)
                    .HasMaxLength(200)
                    .HasColumnName("ProjectOC");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<VGetJobLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_GetJobLogs");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.AwardedTo).HasMaxLength(100);

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseoutDoneDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.CommPaidDate).HasColumnType("datetime");

                entity.Property(e => e.CompetitorPrice).HasColumnType("money");

                entity.Property(e => e.ContactType).HasMaxLength(200);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.FinalInvoice).HasMaxLength(100);

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasMaxLength(4000);

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.ProductType).HasMaxLength(100);

                entity.Property(e => e.ProjectOc)
                    .HasMaxLength(200)
                    .HasColumnName("ProjectOC");

                entity.Property(e => e.Rep).HasMaxLength(1000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<VGetJobLogs2172022>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_GetJobLogs_2172022");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.AwardedTo).HasMaxLength(100);

                entity.Property(e => e.BidNumber)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CloseoutDoneDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.CommPaidDate).HasColumnType("datetime");

                entity.Property(e => e.CompetitorPrice).HasColumnType("money");

                entity.Property(e => e.ContactType).HasMaxLength(200);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.FinalInvoice).HasMaxLength(100);

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasMaxLength(100);

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.ProductType).HasMaxLength(100);

                entity.Property(e => e.ProjectOc)
                    .HasMaxLength(200)
                    .HasColumnName("ProjectOC");

                entity.Property(e => e.Rep).HasMaxLength(1000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<Wip>(entity =>
            {
                entity.ToTable("WIP");

                entity.Property(e => e.Wipid).HasColumnName("WIPId");

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.EquipReturnDate).HasColumnType("datetime");

                entity.Property(e => e.InvReturnDate).HasColumnType("datetime");

                entity.Property(e => e.MobilizationDate).HasColumnType("datetime");

                entity.Property(e => e.Prep12).HasColumnName("Prep1/2");

                entity.Property(e => e.Prep12Date).HasColumnType("datetime");

                entity.Property(e => e.Prep13).HasColumnName("Prep1/3");

                entity.Property(e => e.Prep14).HasColumnName("Prep1/4");

                entity.Property(e => e.Prep14Date).HasColumnType("datetime");

                entity.Property(e => e.Prep34Date).HasColumnType("datetime");

                entity.Property(e => e.PrepDoneDate).HasColumnType("datetime");

                entity.Property(e => e.Removal12).HasColumnName("Removal 1/2");

                entity.Property(e => e.Removal12Date).HasColumnType("datetime");

                entity.Property(e => e.RemovalDoneDate).HasColumnType("datetime");

                entity.Property(e => e.TotalComplete)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Joblog)
                    .WithMany(p => p.Wips)
                    .HasForeignKey(d => d.JoblogId)
                    .HasConstraintName("FK_WIP_JobLog");
            });

            modelBuilder.Entity<Wip2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WIP2");

                entity.Property(e => e.AcceptedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Accepted Date");

                entity.Property(e => e.AmountDone)
                    .HasColumnType("money")
                    .HasColumnName("Amount Done");

                entity.Property(e => e.DaysInWip)
                    .HasMaxLength(255)
                    .HasColumnName("Days In WIP");

                entity.Property(e => e.Department).HasMaxLength(255);

                entity.Property(e => e.Done).HasColumnName("% Done");

                entity.Property(e => e.EagleSBidFormSales)
                    .HasColumnType("money")
                    .HasColumnName("Eagle's BID FORM Sales");

                entity.Property(e => e.EquipmentReturned).HasColumnName("Equipment Returned");

                entity.Property(e => e.F21).HasMaxLength(255);

                entity.Property(e => e.InventoryReturned).HasColumnName("Inventory  Returned");

                entity.Property(e => e.ProjectJobClient)
                    .HasMaxLength(255)
                    .HasColumnName("Project # : Job : Client");

                entity.Property(e => e.Rep).HasMaxLength(255);

                entity.Property(e => e.ScheduledFinishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Scheduled Finish Date");

                entity.Property(e => e.ScheduledTimeExpired).HasColumnName("Scheduled Time Expired");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start Date");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e._15Prep12).HasColumnName(" 15% Prep 1/2");

                entity.Property(e => e._15Prep14).HasColumnName(" 15% Prep 1/4");

                entity.Property(e => e._15Prep34).HasColumnName(" 15% Prep 3/4");

                entity.Property(e => e._15PrepDone).HasColumnName(" 15% Prep Done");

                entity.Property(e => e._15Removal12).HasColumnName(" 15% Removal 1/2");

                entity.Property(e => e._15RemovalDone).HasColumnName(" 15% Removal Done");

                entity.Property(e => e._5DemobDone).HasColumnName(" 5% Demob Done");

                entity.Property(e => e._5MobilizationDone).HasColumnName(" 5% Mobilization Done");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
