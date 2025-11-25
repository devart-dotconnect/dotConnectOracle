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