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