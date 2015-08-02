using System.Collections.Generic;
using System.Configuration;
using ServiceStack;

namespace Domain
{
	public static class ConfigurationService
	{
		public static List<string> Crosses
		{
			get { return ConfigurationSettings.AppSettings["Crosses"].FromJson<List<string>>(); }
		}

		public static List<string> Providers
		{
			get { return ConfigurationSettings.AppSettings["Providers"].FromJson<List<string>>(); }
		}
	}
}
