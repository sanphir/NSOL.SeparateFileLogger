using System;
using System.IO;

namespace NSOL.SeparateFileLogger
{
	/// <summary>
	/// Класс для логирования по отдельным файлам
	/// </summary>
	public class FileLogger : IFileLogger, IDisposable
	{
		/// <summary>
		/// наименование логка
		/// </summary>
		private string _logTypeName;

		/// <summary>
		/// Папка логов + имя файла
		/// </summary>
		private string _logFilePath;

		/// <summary>
		/// папка логов(как в конфиге)
		/// </summary>
		private string _logFolderPath;

		/// <summary>
		/// имя файла
		/// </summary>
		private string _logFileName;

		/// <summary>
		/// Полный путь к лог файлу
		/// </summary>

		private string _fullLogFilePath;

		//TO DO
		//Add to config
		private const string LOG_PATH = "";

		/// <summary>
		/// Полный путь к лог файлу
		/// </summary>
		public string FullLogFilePath => _fullLogFilePath;

		public Guid LogId { get; private set; }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="logTypeName">Название систем по которой ведется логирование</param>
		public FileLogger(string logTypeName) : this(logTypeName, LOG_PATH)
		{
		}

		/// <summary>
		/// Класс для логирования по отдельным файлам
		/// </summary>
		/// <param name="logTypeName">Название систем по которой ведется логирование</param>
		/// <param name="logPath">Путь к папке логов</param>
		public FileLogger(string logTypeName, string logPath)
		{
			LogId = Guid.NewGuid();

			if (string.IsNullOrEmpty(logTypeName))
			{
				throw new ArgumentException();
			}

			_logTypeName = logTypeName.Replace(' ', '_').Replace(':', '_').Replace('"', '_');

			var createdDate = DateTime.Now;
			_logFolderPath = Path.Combine(logPath, _logTypeName, createdDate.ToString("yyyy-MM-dd"));
			if (!Directory.Exists(_logFolderPath))
			{
				Directory.CreateDirectory(_logFolderPath);
			}

			_logFileName = $"{createdDate:yyyy-MM-dd_HHmmss}_{_logTypeName}_log.txt";

			_logFilePath = Path.Combine(_logFolderPath, _logFileName);

			_fullLogFilePath = GetRealFullPath();

			//LogInfoService.Instance.AddLogInfo(new LogInfo()
			//{
			//	LogID = LogId,
			//	Name = _logTypeName,
			//	FullPath = _fullLogFilePath,
			//	CreatedDate = createdDate
			//});
		}

		/// <summary>
		/// актуальный полный путь к файлу
		/// </summary>
		/// <returns></returns>
		private string GetRealFullPath()
		{
			using (StreamWriter writer = System.IO.File.AppendText(_logFilePath))
			{
				return ((FileStream)(writer.BaseStream)).Name;
			}
		}

		private void WriteFile(string message)
		{
			using (StreamWriter writer = System.IO.File.AppendText(_logFilePath))
			{
				writer.WriteLine(message);
				Console.WriteLine(message);
			}
		}

		/// <summary>
		/// Сообщение
		/// </summary>
		/// <param name="message">Текст сообщения</param>
		public void Info(string message)
		{
			WriteFile($"{DateTime.Now:yyyyy-MM-dd HH:mm:ss.fff}: {message}");
		}

		/// <summary>
		/// Предупреждение
		/// </summary>
		/// <param name="message">Текст предупреждения</param>
		public void Warn(string message)
		{
			WriteFile($"{DateTime.Now:yyyyy-MM-dd HH:mm:ss.fff} WARNING: {message}");
		}

		/// <summary>
		/// Ошибка
		/// </summary>
		/// <param name="message">Текст ошибки</param>
		public void Error(string message)
		{
			WriteFile($"{DateTime.Now:yyyyy-MM-dd HH:mm:ss.fff} ERROR: {message}");
		}

		/// <summary>
		/// Фатальная ошибка
		/// </summary>
		/// <param name="message">Текст ошибки</param>
		public void Fatal(string message)
		{
			WriteFile($"{DateTime.Now:yyyyy-MM-dd HH:mm:ss.fff} FATAL ERROR: {message}");
		}

		/// <summary>
		/// Исключение
		/// </summary>
		/// <param name="e">Исключение</param>
		public void Exception(Exception e)
		{
			WriteFile($"{DateTime.Now:yyyyy-MM-dd HH:mm:ss.fff} FATAL ERROR: {e}");
		}

		/// <summary>
		/// IDisposable.Dispose
		/// </summary>
		public void Dispose()
		{
		}
	}
}
