# How to connect to Oracle in .NET with C#

Based on [https://www.devart.com/dotconnect/oracle/connect-to-oracle-database.html](https://www.devart.com/dotconnect/oracle/connect-to-oracle-database.html)

dotConnect for Oracle is a robust ADO.NET data provider designed for high-performance connectivity between .NET applications and Oracle databases. It offers direct access to Oracle servers without requiring Oracle Client installation, along with advanced features including SSL/SSH security, connection pooling, and comprehensive ORM support.

This guide demonstrates various methods for establishing connections to Oracle databases in your C# applications, from basic connectivity to advanced security configurations and ORM integration.

## Connect to Oracle using C#

Get started with Oracle connectivity using the OracleConnection class. This section covers the fundamentals of creating a connection object, configuring basic connection parameters, and executing your first database operations against an Oracle server.

```cs
using Devart.Data.Oracle;

class Program
{
  static void Main()
  {
    string connectionString = "" +
      "Server=127.0.0.1;" +
      "Port=1521;" +
      "User Id=TestUser;" +
      "Password=TestPassword;" +
      "Service Name=orcl;" +
      "Direct=True;" +
      "License Key=**********";

    using (OracleConnection connection = new OracleConnection(connectionString))
    {
      try
      {
        connection.Open();
        Console.WriteLine("Connection to Oracle successful!");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}
```

## Connect to Oracle using connection string builder

The OracleConnectionStringBuilder class provides a type-safe, programmatic way to construct connection strings. Learn how to build complex connection strings dynamically, avoiding syntax errors and improving code maintainability through IntelliSense support.

```cs
using Devart.Data.Oracle;

class Program
{
  static void Main()
  {
    try
    {
      OracleConnectionStringBuilder oraCSB = new OracleConnectionStringBuilder();
      oraCSB.Server = "127.0.0.1";
      oraCSB.Port = 1521;
      oraCSB.ServiceName = "orcl";
      oraCSB.UserId = "TestUser";
      oraCSB.Password = "TestPassword";
      oraCSB.Direct = true;
      oraCSB.LicenseKey = "**********";

      using (OracleConnection connection = new OracleConnection(oraCSB.ConnectionString))
      {
        connection.Open();
         Console.WriteLine("Connection to Oracle successful!");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Connection failed: {ex.Message}");
    }
  }
}
```

## Connect to Oracle using SSL connection

Secure your data in transit with SSL/TLS encryption. This section demonstrates how to configure SSL connections to Oracle databases, ensuring encrypted communication between your .NET application and the database server for enhanced security compliance.

```cs
using Devart.Data.Oracle;

class Program
{
  static void Main()
  {
    string connectionString = "" +
      "Direct=True;" +
      "Server=127.0.0.1;" +
      "Port=1521;" +
      "User Id=TestUser;" +
      "Password=TestPassword;" +
      "Service Name=orcl;" +
      "SslKey=/server.key;" +
      "SslCert=/server.crt;" +
      "SslVerification=False;" +
      "License Key=**********";

    using (OracleConnection connection = new OracleConnection(connectionString))
    {
      try
      {
        connection.Open();
        Console.WriteLine("SSL connection to Oracle successful!");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}
```

## Connect to Oracle using SSH connection

SSH tunneling provides an additional security layer for Oracle connections, especially useful for accessing databases through firewalls or across untrusted networks. Learn how to establish secure SSH tunnels and route your Oracle traffic through them.

```cs
using Devart.Data.Oracle;

class Program
{
  static void Main()
  {
    try
    {
      string connectionString =
        "Direct=True;" +
        "Host=ssh://127.0.0.1;" +
        "Port=1521;" +
        "Service Name=orcl;" +
        "User Id=TestUser;" +
        "Password=TestPassword;" +
        "License Key=**********";

      using (OracleConnection connection = new OracleConnection(connectionString))
      {
        // SSH tunnel setup
        connection.SshOptions.AuthenticationType = SshAuthenticationType.Password;
        connection.SshOptions.Host = "OracleSSH";
        connection.SshOptions.Port = 22;
        connection.SshOptions.User = "sshUser";
        connection.SshOptions.Password = "sshPassword";
        connection.Open();
        Console.WriteLine("SSH connection to Oracle established successfully!");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Connection failed: {ex.Message}");
    }
  }
}
```

## Connect to Oracle using Unicode connection

Working with international applications requires proper Unicode support. This section covers configuring your Oracle connections to handle multi-language data correctly, ensuring proper storage and retrieval of characters from various writing systems.

```cs
using Devart.Data.Oracle;

class Program
{
  static void Main()
  {
    try
    {
      string connectionString =
        "Server=127.0.0.1;" +
        "Port=1521;" +
        "Service Name=orcl;" +
        "User Id=TestUser;" +
        "Password=TestPassword;" +
        "Direct=True;" +
        "License Key=**********";

      using (OracleConnection connection = new OracleConnection(connectionString))
      {
        connection.Open();
        connection.Unicode = true;
        Console.WriteLine("Connection with Unicode support established successfully!");
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Connection failed: {ex.Message}");
    }
  }
}
```

## Connect to Oracle with EF Core

Leverage the power of Entity Framework Core with Oracle databases. dotConnect for Oracle provides full EF Core integration, enabling you to use LINQ queries, migrations, and code-first development patterns. This section demonstrates setup and common usage scenarios.

```cs
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
```