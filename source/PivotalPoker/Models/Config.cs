using System;
using System.Configuration;

namespace PivotalPoker.Models
{
    public class Config : IConfig
    {
        public T Get<T>(string key)
        {
            var @value = ReadSetting(key);
            if (String.IsNullOrWhiteSpace(@value))
                throw new ConfigurationErrorsException(String.Format("Configuration '{0}' is either not defined or empty.", key));

            try
            {
                return (T) Convert.ChangeType(@value, typeof (T));
            }
            catch (FormatException)
            {
                throw new ConfigurationErrorsException(String.Format("Unable to convert the value for '{0}' to type {1}", key, typeof (T)));
            }
        }

        protected virtual string ReadSetting(string key)
        {
            var @value = Environment.GetEnvironmentVariable(key);
            if (!String.IsNullOrWhiteSpace(@value))
                return @value;

            @value = ConfigurationManager.AppSettings.Get(key);
            if (!String.IsNullOrWhiteSpace(@value))
                return @value;

            return null;
        }
    }
}