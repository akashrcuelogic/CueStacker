using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestDemo
{
	public class NetworkRequestManager
	{
		private HttpClient client;

		private static NetworkRequestManager sharedmanager = new NetworkRequestManager();
		public static NetworkRequestManager Sharedmanager
		{
			get
			{
				return sharedmanager;
			}
		}

		public NetworkRequestManager() {
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 2560000;
		}

		#region Protocol Methods
		public async Task<APIResult> sendPostRequest(String inputJson, String url)
		{
			var uri = new Uri(url);
			var content = new StringContent(inputJson, Encoding.UTF8, "application/json");

			HttpResponseMessage response = null;
			response = await client.PostAsync(uri, content);
			var contentBody = await response.Content.ReadAsStringAsync();

			ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(contentBody);

			APIResult apiResult = new APIResult();
			if(response.IsSuccessStatusCode) {				
				if (errorResponse.error) {
					apiResult.Error = new GPError(errorResponse.message);
				}
				else {
					apiResult.ResponseJSON = contentBody;
				}
			} else {
				apiResult.Error = new GPError("There was a problem with your request");
			}

			return apiResult;		
		}

		public async Task<APIResult> sendGetRequest(String url)
		{
			var uri = new Uri(url);

			HttpResponseMessage response = null;

			HttpClientHandler handler = new HttpClientHandler()
			{
				AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
			};


            client = new HttpClient(handler);
			client.MaxResponseContentBufferSize = 2560000;

			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


			response = await client.GetAsync(uri);

            var contentBody = await response.Content.ReadAsStreamAsync();

			APIResult apiResult = new APIResult();
			if (response.IsSuccessStatusCode) {
                byte[] bytes = ((System.IO.MemoryStream)contentBody).ToArray();

                apiResult.ResponseJSON = System.Text.Encoding.UTF8.GetString(bytes);
			}
			else {
				apiResult.Error = new GPError("There was a problem with your request");
			}
			return apiResult;
		}
		#endregion
	}


	public class ErrorResponse {
		public bool error;
		public string message;
	}

}
