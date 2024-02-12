using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductNine.Static
{
    internal class User
    {
        static ProductNine.DB.User logged = new();

        public static ProductNine.DB.User Logged
        {
            get => logged;
            set
            {
                logged = value;
                LoggedChanged?.Invoke(null, value);
            }
        }

        public static event EventHandler<ProductNine.DB.User> LoggedChanged;
    }
}
