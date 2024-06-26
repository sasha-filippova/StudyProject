﻿using Microsoft.EntityFrameworkCore;
using StudyProject.Models;
using System.Configuration;
using System.Xml.Linq;

namespace StudyProject
{
    // <summary>
    /// Контекст базы данных для управления сущностями проектного менеджмента.
    /// </summary>
    public class ProjectManagementContext : DbContext
    {
        /// <summary>
        /// Конструктор для ProjectManagementContext.
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста.</param>
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options)
        { }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Status> Statuses { get; set; }

        /// <summary>
        /// Настройка сущностей и их отношений.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели, используемый для настройки сущностей и их отношений в базе данных.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskAssignment>().HasKey(t => t.Id);
            modelBuilder.Entity<Member>().HasKey(m => new { m.ProjectId, m.StudentId });

            modelBuilder.Entity<User>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(us => us.RoleId);

            modelBuilder.Entity<User>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(us => us.StudentId);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne<Models.Task>()
                .WithMany()
                .HasForeignKey(t => t.TaskId);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(t => t.StudentId);

            modelBuilder.Entity<Models.Task>()
                .HasOne<Status>()
                .WithMany()
                .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Models.Task>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(t => t.CategoryId);

            modelBuilder.Entity<Report>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(r => r.ProjectId);

            modelBuilder.Entity<Project>()
                .HasOne<Status>()
                .WithMany()
                .HasForeignKey(s => s.StatusId);

            modelBuilder.Entity<Member>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(m => m.StudentId);

            modelBuilder.Entity<Member>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(m => m.ProjectId);

            modelBuilder.Entity<Comment>()
                .HasOne<Models.Task>()
                .WithMany()
                .HasForeignKey(com => com.TaskId);

            modelBuilder.Entity<Comment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(com => com.UserId);

            
        }
        /// <summary>
        /// Настройка контекста базы данных для работы с MySQL.
        /// </summary>
        /// <param name="optionsBuilder">Построитель опций, используемый для настройки параметров контекста базы данных.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = WebApplication.CreateBuilder();
            
            builder.Services.AddDbContext<ProjectManagementContext>(op =>
            {
                op.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
                new MySqlServerVersion(new Version(10, 4, 32)));
            });
        }
    }
}
