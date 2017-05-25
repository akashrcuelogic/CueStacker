using System;
namespace TestDemo
{
	public class GPError
	{
		public GPError()
		{
		}
		private String message = String.Empty;
		public string Message
		{
			get
			{
				return message;
			}
		}
		public GPError(string message)
		{
			this.message = message;
		}
	}
}
