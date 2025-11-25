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