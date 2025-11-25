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