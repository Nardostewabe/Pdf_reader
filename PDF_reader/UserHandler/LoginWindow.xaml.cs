using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PDF_reader.Pdf_Manager;

namespace PDF_reader.UserHandler
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            UserManager User = new UserManager();


            if (User.LoginUser(username, password))
            {
                Mypdf MyWindow = new Mypdf();
                MyWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }


        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            UserManager User = new UserManager();

            if (User.RegisterUser(username, password))
            {
                MessageBox.Show("Account created! Please log in.");
            }
            else
            {
                MessageBox.Show("Username already exists.");
            }

        }
    }
}
