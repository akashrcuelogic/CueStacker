using System;
namespace TestDemo
{
	public class NetworkRequestModel
	{
		public NetworkRequestModel()
		{
		}

		/// <summary>
		/// The URL.
		/// </summary>
		private string url = String.Empty;
		public string Url {
			get { return url;}
			set { url = value;}
		}
	}
}
