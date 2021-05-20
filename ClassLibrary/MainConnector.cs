using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ClassLibrary
{
	public class MainConnector
	{
		public SqlConnection connection;
		public async Task<bool> ConnectAsync()
		{
			bool result;
			try
			{
				connection = new SqlConnection(ConnectionString.MsSqlConnection);
				await connection.OpenAsync();
				result = true;
			}
			catch
			{
				result = false;
			}

			return result;
		}

		public async void DisconnectAsync()
		{
			if (connection.State == ConnectionState.Open)
			{
				await connection.CloseAsync();
			}
		}
	}
}
