using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductNine.Static
{
    internal class DataBase
    {
        static User03Context db;

        public static User03Context Instance()
        {
            if (db == null)
                db = new User03Context();
            return db;
        }
    }
}
