using MySql.Data.MySqlClient;

namespace Spendr.DAL
{
    public sealed class DbConnection
    {
        private static MySqlConnection? instance = null;
        public static MySqlConnection GetConnection
        {
            get
            {

                if (instance == null)
                {
                    var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build();
                    string? conn = config.GetValue<string>("ConnectionStrings:MySQL");
                    instance = new MySqlConnection(conn);
                    try
                    {
                        instance.Open();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return instance;
            }
        }
    }
}