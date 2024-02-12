using ProductNine.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Threading;
using ProductNine.Pages;
using ProductNine.DB;

namespace ProductNine.mvvm.vm
{
    public class LoginVM : BaseVM
    {
        bool needCapcha = false;

        private readonly PasswordBox passwordBox;
        private readonly Canvas capchaField;
        private bool notBlocked = true;

        public bool NotBlocked
        {
            get => notBlocked;
            set
            {
                notBlocked = value;
                Signal();
            }
        }
        public string Login { get; set; }
        private Visibility capchaVisibility = Visibility.Collapsed;

        public Visibility CapchaVisibility
        {
            get => capchaVisibility;
            set
            {
                capchaVisibility = value;
                Signal();
            }
        }
        public string CapchaText { get; set; }
        string generateCapcha;

        public VmCommand RunLogin { get; set; }
        public VmCommand GuestLogin { get; set; }

        Timer timer;

        public LoginVM(PasswordBox passwordBox, Canvas capchaField)
        {
            this.passwordBox = passwordBox;
            this.capchaField = capchaField;

            RunLogin = new VmCommand(() => {
                try
                {
                    var user = DataBase.Instance().Users.FirstOrDefault(s => s.Login == Login &&
                        s.Password == passwordBox.Password);
                    bool success = user != null;
                    if (needCapcha && generateCapcha != CapchaText)
                        success = false;

                    if (!success && needCapcha)
                    {
                        GenerateCapcha();
                        timer = new Timer(EnableInput, null, 0, 10000);
                        MessageBox.Show("Неверный логин или пароль. Система заблокирована на 10 секунд.");
                        return;
                    }
                    if (!success)
                    {
                        MessageBox.Show("Неверный логин или пароль. Требуется подтверждение входа.");
                        needCapcha = true;
                        GenerateCapcha();
                        CapchaVisibility = Visibility.Visible;
                        return;
                    }

                    Static.User.Logged = user;
                    PageNavigator.CurrentPage = new ProductList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при авторизации. Не найден сервер бд.");
                }
            },

            () => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(passwordBox.Password));

            GuestLogin = new VmCommand(() =>
            {
                Static.User.Logged = new DB.User { Id = -1, Surname = "Гость" };
                PageNavigator.CurrentPage = new ProductList();
            }, () => true);
        }

        private void EnableInput(object? state)
        {
            NotBlocked = !NotBlocked;
            if (NotBlocked)
            {
                timer.Dispose();
            }
        }
        Random random = new Random();

        char GenerateChar()
        {
            if (random.Next(0, 10) > 5)
                return (char)random.Next(97, 123);
            else if (random.Next(0, 10) > 5)
                return (char)random.Next(65, 91);
            else
                return (char)random.Next(48, 58);
        }

        private void GenerateCapcha()
        {
            capchaField.Children.Clear();
            generateCapcha = null;
            for (int i = 0; i < 4; i++)
            {
                char symbol = GenerateChar();
                generateCapcha += symbol;
                var label = new Label();
                label.Content = symbol;
                label.FontSize = 20;
                Canvas.SetTop(label, 20 + random.Next(-10, 11));
                Canvas.SetLeft(label, 10 * generateCapcha.Length + random.Next(-5, 5));
                capchaField.Children.Add(label);
            }
        }
    }
}
