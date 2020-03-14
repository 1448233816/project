using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProject
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseDal baseDal = new BaseDal();
            Company company = baseDal.Find<Company>(2);
            List<Company> company2 = baseDal.FindAll<Company>();
        }
    }
}
