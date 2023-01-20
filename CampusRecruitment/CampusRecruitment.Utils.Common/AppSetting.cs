using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CampusRecruitment.Utils.Common
{
    public static class AppSetting
    {
        public static string GetConfigValue(string key)
        {
            return AppConfigBuilder._configuration[key];
        }

        public static T GetConfigValue<T>(string fieldId)
        {
            var t = typeof(T);
            string value = GetConfigValue(fieldId);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                t = Nullable.GetUnderlyingType(t);
            }
            return (!string.IsNullOrEmpty(value) ? (T)Convert.ChangeType(value, t) : default(T));
        }
    }
    public class AppConfigBuilder
    {
        protected internal static IConfiguration _configuration = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigBuilder"/> class.
        /// </summary>
        public AppConfigBuilder()
        {
        }

        public void BuildAppSettingProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
