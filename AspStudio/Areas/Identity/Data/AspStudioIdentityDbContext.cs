using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EagleApp.Models;
using EagleApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Type = EagleApp.Models.Type;

namespace EagleApp.Areas.Identity.Data
{
    public class AspStudioIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AspStudioIdentityDbContext(DbContextOptions<AspStudioIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<VWipReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_WipReport");

                entity.Property(e => e.AmountDone).HasColumnType("money");

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CollectedAmount).HasColumnType("money");

                entity.Property(e => e.DateAddedStr)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DemoDoneDate).HasColumnType("datetime");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.EagleBidPrice).HasColumnType("money");

                entity.Property(e => e.EagleBidSales).HasColumnType("money");

                entity.Property(e => e.EquipReturnDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.InvReturnDate).HasColumnType("datetime");

                entity.Property(e => e.MobilizationDate).HasColumnType("datetime");

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PercentageDone).HasMaxLength(10);

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

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TotalComplete)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            builder.Entity<Wip>(entity =>
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
                    .IsFixedLength(true);

                entity.HasOne(d => d.Joblog)
                    .WithMany(p => p.Wips)
                    .HasForeignKey(d => d.JoblogId)
                    .HasConstraintName("FK_WIP_JobLog");
            });

            builder.Entity<JobLog>(entity =>
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

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.JobLogs)
                //    .HasForeignKey(d => d.UserId)
                //    .HasConstraintName("FK_JobLog_AspNetUsers");
            });

            builder.Entity<VDashboardDatum>(entity =>
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

                entity.Property(e => e.Rep).HasMaxLength(1000);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<VGetJobLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_GetJobLogs");

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

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

                entity.Property(e => e.FinalInvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.JeipmeetingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("JEIPMeetingDate");

                entity.Property(e => e.JobName).HasMaxLength(200);

                entity.Property(e => e.MissedBy).HasMaxLength(200);

                entity.Property(e => e.OpenDate).HasColumnType("datetime");

                entity.Property(e => e.PaidInFullDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectOc)
                    .HasMaxLength(200)
                    .HasColumnName("ProjectOC");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            builder.Entity<Competitor>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            builder.Entity<ContactType>(entity =>
            {
                entity.ToTable("ContactType");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(200);
            });

            builder.Entity<Customer>(entity =>
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

            builder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            builder.Entity<Job>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.JobName).HasMaxLength(100);
            });

            

            builder.Entity<Ocawa>(entity =>
            {
                entity.ToTable("OCAWA");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Ocawaname)
                    .HasMaxLength(200)
                    .HasColumnName("OCAWAName");
            });

            builder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Type1)
                    .HasMaxLength(200)
                    .HasColumnName("Type");
            });

            builder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.LogAction).HasMaxLength(100);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.JobLog)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.JobLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditLog_JobLog");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.AuditLogs)
                //    .HasForeignKey(d => d.UserId)
                //    .HasConstraintName("FK_AuditLog_AspNetUsers");
            });
        }

        public virtual DbSet<JobLog> JobLog { get; set; }
        public virtual DbSet<Competitor> Competitors { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        //public virtual DbSet<JobLog> JobLogs { get; set; }
        public virtual DbSet<Ocawa> Ocawas { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<VDashboardDatum> VDashboardData { get; set; }

        public virtual DbSet<VGetJobLog> VGetJobLog { get; set; }

        public virtual DbSet<JobStatus> JobStatus { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }

        public virtual DbSet<Wip> Wips { get; set; }
        public virtual DbSet<VWipReport> VWipReports { get; set; }

        public virtual async Task<int> SaveChangesAsync(string userId = null, int jobId = 0, string logAction = null)
        {
            if (jobId != 0)
            {
                OnBeforeSaveChanges(userId, jobId, logAction);
            }
            var result = await base.SaveChangesAsync();
            return result;
        }

        private void OnBeforeSaveChanges(string userId, int jobId, string logAction)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                var jobObj = (JobLog)entry.Entity;
                auditEntry.LogAction = $"Action = {jobObj.ProjectNumber}";
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                auditEntry.JobLogId = jobId;
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }

    }
}
