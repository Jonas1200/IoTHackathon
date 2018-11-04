using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaspBier.Models
{
    public class Settings
    {
        public string DBPath { get; set; }
      
        public string Url { get; set; }

        public string MailServer { get; set; }

        public int MailPort { get; set; }

        public string MailHost { get; set; }

        public string MailHostPassword { get; set; }

        public string MailReceiver { get; set; }

    }

}
