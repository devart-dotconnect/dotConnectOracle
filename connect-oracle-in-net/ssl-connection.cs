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