using System;
namespace TestDemo
{
	public class BaseAPI
	{
		public BaseAPI()
		{
			
		}

		private String BASE_URL = "http://13.65.243.78/task_manager/v1";

		protected String getCompleteURL(String endPoint) {
			return BASE_URL + endPoint;
		}
	}
}
