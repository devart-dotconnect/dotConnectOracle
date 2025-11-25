using Microsoft.EntityFrameworkCore;
using Devart.Data.Oracle.EFCore;

public class Actor
{
  public int ActorId { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
}

public class SakilaContext : DbContext
{
  public DbSet<Actor> Actors { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseOracle(
      "Server=127.0.0.1;" +
      "Port=1521;" +
      "Service Name=orcl;" +
      "User Id=TestUser;" +
      "Password=TestPassword;" +
      "Direct=True;" +
      "License Key=**********"
    );
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Actor>().ToTable("ACTOR");
    modelBuilder.Entity<Actor>().HasKey(a => a.ActorId);
    modelBuilder.Entity<Actor>().Property(a => a.ActorId).HasColumnName("ACTOR_ID");
    modelBuilder.Entity<Actor>().Property(a => a.FirstName).HasColumnName("FIRST_NAME");
    modelBuilder.Entity<Actor>().Property(a => a.LastName).HasColumnName("LAST_NAME");
  }
}

class Program
{
  static void Main()
  {
    using (var context = new SakilaContext())
    {
      try
      {
        // Optional: Check connection and ensure mapping works
        Console.WriteLine("Connected to Oracle database via EF Core successfully!");

        var actors = context.Actors.Take(10);
        foreach (var actor in actors)
        {
          Console.WriteLine($"Actor ID: {actor.ActorId}, Name: {actor.FirstName} {actor.LastName}");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}