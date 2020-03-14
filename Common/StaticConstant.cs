using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class StaticConstant
    {
        public static readonly string SqlConfig = ConfigurationManager.ConnectionStrings["SqlConfig"].ConnectionString;
            
    }
}
