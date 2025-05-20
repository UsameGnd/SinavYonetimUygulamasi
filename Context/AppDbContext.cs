using Microsoft.EntityFrameworkCore;
using SinavYonetimUygulamasi.Models;

namespace SinavYonetimUygulamasi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<SubTopic> SubTopics { get; set; }
    public DbSet<QuestionBank> QuestionBanks { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamQuestion> ExamQuestions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Course
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Topics)
            .WithOne(t => t.Course)
            .HasForeignKey(t => t.CourseId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Questions)
            .WithOne(q => q.Course)
            .HasForeignKey(q => q.CourseId)
            .OnDelete(DeleteBehavior.NoAction);

        // Topic
        modelBuilder.Entity<Topic>()
            .HasMany(t => t.SubTopics)
            .WithOne(st => st.Topic)
            .HasForeignKey(st => st.TopicId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Topic>()
            .HasMany(t => t.Questions)
            .WithOne(q => q.Topic)
            .HasForeignKey(q => q.TopicId)
            .OnDelete(DeleteBehavior.NoAction);

        // SubTopic
        modelBuilder.Entity<SubTopic>()
            .HasMany(st => st.Questions)
            .WithOne(q => q.SubTopic)
            .HasForeignKey(q => q.SubTopicId)
            .OnDelete(DeleteBehavior.NoAction);

        // QuestionBank
        modelBuilder.Entity<QuestionBank>()
            .HasMany(q => q.Options)
            .WithOne(o => o.Question)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Exam
        modelBuilder.Entity<Exam>()
            .HasMany(e => e.Questions)
            .WithOne(eq => eq.Exam)
            .HasForeignKey(eq => eq.ExamId)
            .OnDelete(DeleteBehavior.NoAction);
    }
} 