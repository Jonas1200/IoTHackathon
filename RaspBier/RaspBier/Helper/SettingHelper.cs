using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RaspBier.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RaspBier.Helper
{
    public static class SettingsHelper
    {
        private static Settings settings;
        public static Settings Settings
        {
            get
            {
                if (settings == null)
                {
                    var builder = new ConfigurationBuilder()
                                      .SetBasePath(Directory.GetCurrentDirectory())
                                      .AddJsonFile("customSettings.json", optional: false, reloadOnChange: true);

                    var config = builder.Build();

                    settings = new Settings();
                    config.Bind(settings);
                }
                return settings;
            }
        }
      
    }

}
