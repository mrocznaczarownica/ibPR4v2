using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ibPR4v2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string log;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void passReyUp(object sender, KeyEventArgs e)
        {

        }

        private void passKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void loginButtonClick(object sender, RoutedEventArgs e)
        {
            string pass = passTb.Password;
            if (loginTb.Text.Length > 0)
            {
                Regex reg = new Regex(@"^([^а-я]+)$");
                if (pass.Length > 7 && reg.IsMatch(pass))
                {
                    DataTable dtUsers = this.Select($"SELECT*FROM [dbo].[passwords] WHERE [login] = '{loginTb.Text}' AND [pass] = '{pass}'");
                    if (dtUsers.Rows.Count > 0)
                    {
                        Window1 window = new Window1();
                        window.Show();
                        log = loginTb.Text;
                    }
                    else MessageBox.Show("Такого пользователя не существует");
                }
                else passNof.Content = "Пароль не может быть короче 7 символов";
            }
            else loginNof.Content = "Введите логин";
        }

        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlConnection sqlConnection = new SqlConnection("Data Source=shepard.keenetic.link;Initial Catalog=IB4;User ID=sa;Password=Alternativa0;Connect Timeout=30;" +
                "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
