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
    /// Логика взаимодействия для edirPass.xaml
    /// </summary>
    public partial class edirPass : Page
    {
        SqlConnection conn = new SqlConnection("Data Source=shepard.keenetic.link;Initial Catalog=IB4;User ID=sa;Password=Alternativa0;Connect Timeout=30;" +
                "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public edirPass()
        {
            InitializeComponent();
        }

        private void saveChangedClick(object sender, RoutedEventArgs e)
        {
            if (newPass.Password == newPassRepeat.Password)
            {
                if (newPass.Password.Length > 7)
                {
                    Regex reg = new Regex(@"^[а-яё]+$");
                    if (reg.IsMatch(newPass.Password))
                    {
                        string log = loginTb.Text;
                        string query = $"update [dbo].[passwords] set [pass] = @pass where [login] = '{log}'";
                        using (conn)
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = newPass.Password;
                                int rowsAdded = cmd.ExecuteNonQuery();
                                if (rowsAdded > 0)
                                {
                                    MessageBox.Show("Данные обновлены");
                                    this.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                    }
                    else MessageBox.Show("Пароль может состоять только из прописных русских букв");
                }
                else MessageBox.Show("Пароль не может быть короче 7 символов");
            }
            else MessageBox.Show("Пароли не совпадают");
        }

        private void exitClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
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
