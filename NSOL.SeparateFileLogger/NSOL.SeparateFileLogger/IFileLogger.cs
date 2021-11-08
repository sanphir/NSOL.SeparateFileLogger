using System;

namespace NSOL.SeparateFileLogger
{
	public interface IFileLogger
	{
		/// <summary>
		/// Сообщение
		/// </summary>
		/// <param name="message">Текст сообщения</param>
		void Info(string message);

		/// <summary>
		/// Предупреждение
		/// </summary>
		/// <param name="message">Текст предупреждения</param>
		void Warn(string message);

		/// <summary>
		/// Ошибка
		/// </summary>
		/// <param name="message">Текст ошибки</param>
		void Error(string message);

		/// <summary>
		/// Фатальная ошибка
		/// </summary>
		/// <param name="message">Текст ошибки</param>
		void Fatal(string message);

		/// <summary>
		/// Исключение
		/// </summary>
		/// <param name="e">Исключение</param>
		void Exception(Exception e);
	}
}
