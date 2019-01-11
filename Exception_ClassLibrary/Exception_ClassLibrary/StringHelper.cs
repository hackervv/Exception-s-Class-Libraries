using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace Exception_ClassLibrary
{
    
    public class StringHelper
    {
        
        public bool ValidFileName(string fileName)
        {
            if (fileName.EndsWith(".SLF"))
            {
                return false;
            }
            return true;
        }
    }
}
