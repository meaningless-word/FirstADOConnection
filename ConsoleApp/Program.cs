using System;

namespace ConsoleApp
{
	class Program
	{
		// Давайте создадим enum со всеми командами CRUD, которые будут в нашей консольной программе, и добавим команду stop. 
		public enum Commands
		{
			stop,
			add,
			delete,
			update,
			show
		}

		static Manager manager = new Manager();
		static void Main(string[] args)
		{
			// первичная реализация
			// теперь перенесено в класс Manager

			//var connector = new MainConnector();
			//var data = new DataTable();

			//var result = connector.ConnectAsync();

			//if (result.Result)
			//{
			//	Console.WriteLine("Подключено успешно!");

			//	// Отсоединённая модель
			//	var db = new DbExecutor(connector);

			//	var tablename = "NetworkUser";

			//	Console.WriteLine("Получаем данные таблицы " + tablename);

			//	data = db.SelectAll(tablename);

			//	Console.WriteLine("Отключаем БД!");
			//	connector.DisconnectAsync();

			//	Console.WriteLine("Количество строк в " + tablename + ": " + data.Rows.Count);

			//	foreach (DataRow row in data.Rows)
			//	{
			//		var cells = row.ItemArray;
			//		foreach (var cell in cells)
			//		{
			//			Console.Write($"{cell}\t");
			//		}
			//		// К элементам строки также можно обращаться по имени столбца, то есть использовать его как индекс.
			//		Console.Write($"{row[data.Columns[0].ColumnName]}\t");

			//		Console.WriteLine();
			//	}

			//	// Присоединённаям модель
			//	result = connector.ConnectAsync();
			//	var reader = db.SelectAllCommandReader(tablename);
			//	/*
			//	 * В первую очередь, давайте условимся, что при работе с SqlDataReader мы будем обращаться к данным по имени столбца.
			//	 * Это поможет избежать путаницы, если мы вдруг захотим переставить столбцы местами. 
			//	 */
			//	var columnList = new List<string>();

			//	for (int i = 0; i < reader.FieldCount; i++)
			//	{
			//		var name = reader.GetName(i);
			//		columnList.Add(name);
			//	}

			//	/*
			//	 * Мы берём все имена столбцов в таблице и запоминанием их в список.
			//	 * Далее по именам мы будем использовать reader, чтобы отображать значения каждого столбца в строке. 
			//	 * Но для начала выведем все имена столбцов на экран: 
			//	 */
			//	for (int i = 0; i < columnList.Count; i++)
			//	{
			//		Console.Write($"{columnList[i]}\t");
			//	}
			//	Console.WriteLine();

			//	//После этого приступим к чтению данных:
			//	while (reader.Read())
			//	{
			//		for (int i = 0; i < columnList.Count; i++)
			//		{
			//			var value = reader[columnList[i]];
			//			Console.Write($"{value}\t");
			//		}

			//		Console.WriteLine();
			//	}
			//	connector.DisconnectAsync();
			//}
			//else
			//{
			//	Console.WriteLine("Ошибка подключения!");
			//}


			// вторичная реализация
			// для удобства неплохо бы реализовать каждое действие через методы
			//// Теперь «визуальное» представление отвечает только за то, чтобы запустить процесс. Далее мы будем обрабатывать команды пользователя. 
			//var manager = new Manager();

			//manager.Connect();

			//// реализация буквы R из CRUD-концепции
			//manager.ShowData();

			//// реализация D из CRUD-концепции
			//Console.WriteLine("Введите логин для удаления:");
			//var count = manager.DeleteUserByLogin(Console.ReadLine());
			//Console.WriteLine("Количество удаленных строк " + count);

			//manager.ShowData();

			//// реалищация C из CRUD-концепции посредством вызова хранимой процедуры
			//Console.WriteLine("Введите логин для добавления:");
			//var login = Console.ReadLine();
			//Console.WriteLine("Введите имя для добавления:");
			//var name = Console.ReadLine();
			//manager.AddUser(login, name);

			//manager.ShowData();

			//manager.Disconnect();


			// Для того чтобы сделать полноценное приложение по работе с пользователями, давайте добавим некоторую систему управления.
			// Пусть на данный момент все будет в классе Program нашего консольного приложения. Но давайте вынесем все операции по методам. 
			// необходимы следующие методы: Add (Create), Delete (Delete), Update (Update).
			// Операцию Read мы не будем выносить дополнительно, так как как она реализована у нас в одну команду для нашего метода из класса Manager.
			manager.Connect();
			// Теперь давайте обозначим команды для работы с нашей программой  — текстовые выражения, которые «пользователь» вобьет, чтобы выполнить какую-либо операцию. 
			// Выведем на экран подсказку пользователя для этих команд:
			Console.WriteLine("Список команд для работы консоли:");
			Console.WriteLine(Commands.stop + ": прекращение работы");
			Console.WriteLine(Commands.add + ": добавление данных");
			Console.WriteLine(Commands.delete + ": удаление данных");
			Console.WriteLine(Commands.update + ": обновление данных");
			Console.WriteLine(Commands.show + ": просмотр данных");
			// Теперь добавим обработчик команд в цикле do… while. После чтения команды добавьте пустую строку.
			string command;
			do
			{
				Console.WriteLine("Введите команду:");
				command = Console.ReadLine();
				Console.WriteLine();
				switch (command)
				{
					case
					nameof(Commands.add):
						{
							Add();
							break;
						}

					case
					nameof(Commands.delete):
						{
							Delete();
							break;
						}
					case
					nameof(Commands.update):
						{
							Update();
							break;
						}
					case
					nameof(Commands.show):
						{
							manager.ShowData();
							break;
						}

				}
			}
			while (command != nameof(Commands.stop));


			manager.Disconnect();

			Console.ReadKey();
		}

		static void Update()
		{
			Console.WriteLine("Введите логин для обновления:");

			var login = Console.ReadLine();

			Console.WriteLine("Введите имя для обновления:");
			var name = Console.ReadLine();

			var count = manager.UpdateUserByLogin(login, name);

			Console.WriteLine("Строк обновлено" + count);

			manager.ShowData();
		}

		static void Add()
		{
			Console.WriteLine("Введите логин для добавления:");

			var login = Console.ReadLine();

			Console.WriteLine("Введите имя для добавления:");
			var name = Console.ReadLine();

			manager.AddUser(login, name);

			manager.ShowData();
		}

		static void Delete()
		{
			Console.WriteLine("Введите логин для удаления:");

			var count = manager.DeleteUserByLogin(Console.ReadLine());

			Console.WriteLine("Количество удаленных строк " + count);

			manager.ShowData();
		}
	}
}
