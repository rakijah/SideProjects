using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCommit
{
    public class Settings
    {
        private static string defaultAppSettings = $"<?xml version=\"1.0\" encoding=\"utf-8\" ?>{Environment.NewLine}<configuration>{Environment.NewLine}\t<startup> {Environment.NewLine}\t\t<supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.5.2\" />{Environment.NewLine}\t</startup>{Environment.NewLine}</configuration>";

        private static Configuration _configuration;
        public static string FolderToWatch
        {
            get
            {
                return GetSetting(nameof(FolderToWatch));
            }
            set
            {
                SetSetting(nameof(FolderToWatch), value);
            }
        }

        public static string GitUsername
        {
            get
            {
                return GetSetting(nameof(GitUsername));
            }
            set
            {
                SetSetting(nameof(GitUsername), value);
            }
        }

        public static string GitPassword
        {
            get
            {
                return GetSetting(nameof(GitPassword));
            }
            set
            {
                SetSetting(nameof(GitPassword), value);
            }
        }

        public static string GitEmail
        {
            get
            {
                return GetSetting(nameof(GitEmail));
            }
            set
            {
                SetSetting(nameof(GitEmail), value);
            }
        }

        static Settings()
        {
            if(!File.Exists(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile))
            {
                File.WriteAllText(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, defaultAppSettings);
            }
            _configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        public static void Save()
        {
            _configuration.Save(ConfigurationSaveMode.Modified);
        }
        
        private static string GetSetting(string setting)
        {
            if (_configuration.AppSettings.Settings[setting] == null)
                SetSetting(setting, "");

            return _configuration.AppSettings.Settings[setting].Value;
        }

        private static void SetSetting(string setting, string value)
        {
            if(!_configuration.AppSettings.Settings.AllKeys.Contains(setting))
            {
                _configuration.AppSettings.Settings.Add(setting, value);
            }else
            {
                _configuration.AppSettings.Settings[setting].Value = value;
            }
        }
    }
}
