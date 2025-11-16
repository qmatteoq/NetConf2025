using Microsoft.EntityFrameworkCore;
using FeedbackApi.Models;

namespace FeedbackApi.Data;

public class FeedbackDbContext : DbContext
{
    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
    {
    }

    public DbSet<FeedbackEntry> Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FeedbackEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reaction).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Feedback).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}
