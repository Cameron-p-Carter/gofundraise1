using Microsoft.EntityFrameworkCore;
using gofundraise3.Entities;

namespace gofundraise3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Project entity
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Status).HasConversion<string>();
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.UpdatedDate).IsRequired();
            });

            // Configure TaskItem entity
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Status).HasConversion<string>();
                entity.Property(e => e.Priority).HasConversion<string>();
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.UpdatedDate).IsRequired();
                
                // Configure relationship
                entity.HasOne(e => e.Project)
                      .WithMany(p => p.Tasks)
                      .HasForeignKey(e => e.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Use static dates to avoid migration issues
            var baseDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Project>().HasData(
                new Project 
                { 
                    Id = 1, 
                    Name = "Website Redesign", 
                    Description = "Redesign company website with modern UI/UX", 
                    Status = ProjectStatus.Active, 
                    DueDate = baseDate.AddDays(30),
                    CreatedDate = baseDate, 
                    UpdatedDate = baseDate 
                },
                new Project 
                { 
                    Id = 2, 
                    Name = "Mobile App Development", 
                    Description = "Develop cross-platform mobile application", 
                    Status = ProjectStatus.Planning, 
                    DueDate = baseDate.AddDays(90),
                    CreatedDate = baseDate, 
                    UpdatedDate = baseDate 
                },
                new Project 
                { 
                    Id = 3, 
                    Name = "API Documentation", 
                    Description = "Document all REST API endpoints with examples", 
                    Status = ProjectStatus.Completed, 
                    DueDate = baseDate.AddDays(-5),
                    CreatedDate = baseDate.AddDays(-30), 
                    UpdatedDate = baseDate.AddDays(-5) 
                }
            );

            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem 
                { 
                    Id = 1, 
                    ProjectId = 1, 
                    Title = "Create wireframes", 
                    Description = "Design initial wireframes for all pages", 
                    Status = Entities.TaskStatus.Done, 
                    Priority = TaskPriority.High, 
                    DueDate = baseDate.AddDays(5),
                    CreatedDate = baseDate, 
                    UpdatedDate = baseDate 
                },
                new TaskItem 
                { 
                    Id = 2, 
                    ProjectId = 1, 
                    Title = "Implement responsive design", 
                    Description = "Make website mobile-friendly and responsive", 
                    Status = Entities.TaskStatus.InProgress, 
                    Priority = TaskPriority.High, 
                    DueDate = baseDate.AddDays(15),
                    CreatedDate = baseDate, 
                    UpdatedDate = baseDate 
                },
                new TaskItem 
                { 
                    Id = 3, 
                    ProjectId = 2, 
                    Title = "Research frameworks", 
                    Description = "Evaluate React Native vs Flutter for mobile development", 
                    Status = Entities.TaskStatus.Todo, 
                    Priority = TaskPriority.Medium, 
                    DueDate = baseDate.AddDays(10),
                    CreatedDate = baseDate, 
                    UpdatedDate = baseDate 
                },
                new TaskItem 
                { 
                    Id = 4, 
                    ProjectId = 3, 
                    Title = "Write API documentation", 
                    Description = "Document all endpoints with request/response examples", 
                    Status = Entities.TaskStatus.Done, 
                    Priority = TaskPriority.Medium, 
                    DueDate = baseDate.AddDays(-10),
                    CreatedDate = baseDate.AddDays(-25), 
                    UpdatedDate = baseDate.AddDays(-5) 
                }
            );
        }
    }
}
