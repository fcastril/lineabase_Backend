using System;
namespace Api6.Common
{
	public static class ConstantsAPI
	{
        public static string NameProject => "API INTEGRATIONS LINEA BASE";
        public static string DescriptionProject => "API for consuming services";
        public const string VersionProject = "v1";
        public const string UriForDefaultWebApi = $"api/{VersionProject}/";

    }
    public static class ContactProject
    {
        public static string Email => "info@orangesoftware.co";
        public static string Name => "OrangeSoftware.CO";
        public static Uri Url  => new Uri("www.orangesoftware.co");
    }
}

