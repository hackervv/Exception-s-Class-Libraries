﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Exception_ClassLibrary
{
    class ConfigHelper
    {
        public static string Email_Config = ConfigurationManager.AppSettings["Email_Config"];
    }
}
