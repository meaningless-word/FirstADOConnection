using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
	public class DbExecutor
	{
		private MainConnector connector;
		public DbExecutor(MainConnector connector)
		{
			this.connector = connector;
		}

		public DataTable SelectAll(string table)
		{
			var selectcommandtext = "select * from " + table;

			return new DataTable();
		}
	}
}
