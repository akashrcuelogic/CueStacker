using System;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace TestDemo
{
	public class NetworkReachabilityManager
	{
		public NetworkReachabilityManager()
		{

		}
		public static bool isInternetAvailable()
		{
			return CrossConnectivity.Current.IsConnected;
		}
	}
}
