using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProductNine.Static
{
    internal class PageNavigator
    {
        private static Page currentPage;

        public static Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                CurrentPageChanged?.Invoke(null, currentPage);
            }
        }

        public static event EventHandler<Page> CurrentPageChanged;
    }
}
