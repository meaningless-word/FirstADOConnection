using System.Data;
using System.Data.SqlClient;

namespace ClassLibrary
{
	public class DbExecutor
	{
		private MainConnector connector;
		public DbExecutor(MainConnector connector)
		{
			this.connector = connector;
		}

		/// <summary>
		/// Этот метод будет принимать на вход имя таблицы, а далее возвращать данные из этой таблицы в объекте DataTable.
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public DataTable SelectAll(string table)
		{
			/// Теперь создадим объект SqlDataAdapter для получения данных по инструкции SQL и подключения из класса MainConnector:
			var selectcommandtext = "select * from " + table;

			var adapter = new SqlDataAdapter(selectcommandtext, connector.GetConnection());

			/*
			 * объект DataSet из пространства имен System.Data представляет хранилище или кэш данных в памяти, извлеченных из источника данных. 
			 * Объект DataSet содержит таблицы, которые представлены типом DataTable.
			 * Таблица DataTable, в свою очередь, состоит из столбцов и строк.
			 * Каждый столбец представляет объект DataColumn, а строка — объект DataRow.
			 */
			var ds = new DataSet();

			// После этого требуется использовать метод заполнения DataAdapter — это метод Fill. В качестве параметра ему передается DataSet. 
			adapter.Fill(ds);
			/*
			 * Важно! Метод Fill() неявно открывает объект подключения, который хранится в свойстве Connection объекта DataAdapter, если это подключение не открыто.
			 * Если метод Fill() открывает подключение, то после получения данных метод Fill закрывает это подключение.
			 */

			// Далее вернём первый элемент коллекции таблиц DataSet:
			return ds.Tables[0];
		}

		/// <summary>
		/// Этот метод будет принимать на вход имя таблицы, а возвращать в «основную» программу объект для чтения результата объект SqlDataReader
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public SqlDataReader SelectAllCommandReader(string table)
		{
			var command = new SqlCommand
			{
				CommandType = CommandType.Text,
				CommandText = "select * from " + table,
				Connection = connector.GetConnection(),
			};

			SqlDataReader reader = command.ExecuteReader();

			if (reader.HasRows)
			{
				return reader;
			}

			return null;
		}

		/// <summary>
		/// метод удаления
		/// </summary>
		/// <param name="table"></param>
		/// <param name="column"></param>
		/// <param name="value"></param>
		/// <returns>количество затронутых при выполнении команды строк</returns>
		public int DeleteByColumn(string table, string column, string value)
		{
			var command = new SqlCommand
			{
				CommandType = CommandType.Text,
				CommandText = "delete from " + table + " where " + column + " = '" + value + "';",
				Connection = connector.GetConnection(),
			};

			return command.ExecuteNonQuery();
		}

		/// <summary>
		/// метод добавления
		/// </summary>
		/// <param name="name"></param>
		/// <param name="login"></param>
		/// <returns>количество затронутых при выполнении команды строк</returns>
		public int ExecProcedureAdding(string name, string login)
		{
			var command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "AddingUserProc",
				Connection = connector.GetConnection(),
			};

			command.Parameters.Add(new SqlParameter("@Name", name));
			command.Parameters.Add(new SqlParameter("@Login", login));

			return command.ExecuteNonQuery();
		}

		/// <summary>
		/// метод изменения
		/// </summary>
		/// <param name="table"></param>
		/// <param name="columntocheck"></param>
		/// <param name="valuecheck"></param>
		/// <param name="columntoupdate"></param>
		/// <param name="valueupdate"></param>
		/// <returns>количество затронутых при выполнении команды строк</returns>
		public int UpdateByColumn(string table, string columntocheck, string valuecheck, string columntoupdate, string valueupdate)
		{
			var command = new SqlCommand
			{
				CommandType = CommandType.Text,
				CommandText = "update   " + table + " set " + columntoupdate + " = '" + valueupdate + "'  where " + columntocheck + " = '" + valuecheck + "';",
				Connection = connector.GetConnection(),
			};

			return command.ExecuteNonQuery();
		}
	}
}
