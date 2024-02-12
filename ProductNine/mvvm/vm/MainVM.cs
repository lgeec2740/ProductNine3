using ProductNine.Pages;
using ProductNine.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProductNine.mvvm.vm
{
    public class MainVM : BaseVM
    {
        public Visibility LoggedIn
        {
            get => User.Logged.Id != 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public string UserFIO
        {
            get => $"{User.Logged.Surname} {User.Logged.NameAndPatronymicName}";
        }

        public VmCommand Logout { get; set; }

        public Page CurrentPage
        {
            get => PageNavigator.CurrentPage;
        }

        public MainVM()
        {
            User.LoggedChanged += User_LoggedChanged;
            PageNavigator.CurrentPageChanged += PageNavigator_CurrentPageChanged;
            Logout = new VmCommand(
                () => {
                    User.Logged = new DB.User();
                    PageNavigator.CurrentPage = new Login();
                }, () => true);
            PageNavigator.CurrentPage = new Login();
        }

        private void PageNavigator_CurrentPageChanged(object? sender, Page e)
        {
            Signal(nameof(CurrentPage));
        }

        private void User_LoggedChanged(object? sender, DB.User e)
        {
            Signal(nameof(UserFIO));
            Signal(nameof(LoggedIn));
        }
    }
}
