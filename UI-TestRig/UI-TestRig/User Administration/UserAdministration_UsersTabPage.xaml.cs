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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for UserAdministration_UsersTabPage.xaml
    /// </summary>
    public partial class UserAdministration_UsersTabPage : Page, IPage
    {
        DataTable groupsTable;
        
        
        public DiagnosticsPage parent { get; set; }

        

        string initial = "";
       
      
        DataTable user;
        public UserAdministration_UsersTabPage(DiagnosticsPage p)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = p;
            CreateUserDataGrid();
            CreateGroupsDataTable();
            LoadGroups();
            CreateUserDataTable();
            RefreshAddUserDataGrid();
        }

        public void RefreshData()
        {
            
            LoadUsers();
            isAdmin();
            CheckUser();
            RefreshAddUserDataGrid();

        }

        public void CheckUser()
        {
            if (UserAdministrationGlobalConfig.uAdmin_CurrentUser != null)
            {
                userTextBox.Text = UserAdministrationGlobalConfig.uAdmin_CurrentUser.UserId;

            }
            else
            {
                userTextBox.Text = "";

            }
        }

        /// <summary>
        /// Enables and disables all admin related functionalities.
        /// </summary>
        /// <returns></returns>
        public bool isAdmin()
        {
            if (UserAdministrationGlobalConfig.uAdmin_CurrentUser != null && UserAdministrationGlobalConfig.uAdmin_CurrentUser.Group.GroupId == 1)
            {
                
                deleteContextMenu.IsEnabled = true;
                usersDataGrid.IsReadOnly = false;
                addUserStackPanel.Visibility = Visibility.Visible;
                return true;
            }
            else
            {
                
                deleteContextMenu.IsEnabled = false;
                usersDataGrid.IsReadOnly = true;
                addUserStackPanel.Visibility = Visibility.Collapsed;
                return false;
            }

        }

        private void RefreshAddUserDataGrid()
        {

            addUserDataGrid.DataContext = null;
            addUserDataGrid.DataContext = user;
        }
        

        public void CreateGroupsDataTable()
        {
            groupsTable = new DataTable();
            
            groupsTable.Columns.Add("Name", typeof(string));
            foreach (string function in UserAdministrationGlobalConfig.uAdmin_FunctionsList)
            {
                groupsTable.Columns.Add(function, typeof(bool));
            }
            DataRow row;
            foreach (GroupTemplate group in UserAdministrationGlobalConfig.uAdmin_GroupsList)
            {
                row = groupsTable.NewRow();
                row["Name"] = group.GroupName;
                foreach (string function in UserAdministrationGlobalConfig.uAdmin_FunctionsList)
                {
                    if (group.AllowedFunctions.Contains(function))
                    {
                        row[function] = true;
                    }
                    else
                    {
                        row[function] = false;
                    }
                }
                groupsTable.Rows.Add(row);
            }
        }

        public void LoadGroups()
        {

            groupsDataGrid.DataContext = null;
            groupsDataGrid.DataContext = groupsTable.DefaultView;
            DataGridTextColumn nameColumn = new DataGridTextColumn();
            nameColumn.Header = "GROUP NAME";
            nameColumn.Binding = new Binding("Name");
            groupsDataGrid.Columns.Add(nameColumn);
            foreach (string function in UserAdministrationGlobalConfig.uAdmin_FunctionsList)
            {
                DataGridCheckBoxColumn column = new DataGridCheckBoxColumn();
                column.Header = function;
                column.Binding = new Binding(function);
                groupsDataGrid.Columns.Add(column);
            }
        }

        private void CreateUserDataGrid()
        {
            usersDataGrid.ItemsSource = null;
            usersDataGrid.ItemsSource = UserAdministrationGlobalConfig.uAdmin_UsersList;

            DataGridCheckBoxColumn functioncolumn = new DataGridCheckBoxColumn();
            foreach (string function in UserAdministrationGlobalConfig.uAdmin_FunctionsList)
            {
                functioncolumn = new DataGridCheckBoxColumn();
                functioncolumn.Header = function;
                functioncolumn.Binding = new Binding($"Group.FunctionsBool[{function}]");
                functioncolumn.IsReadOnly = true;
                usersDataGrid.Columns.Add(functioncolumn);
            }
        }

        private void CreateUserDataTable()
        {
            user = new DataTable();
            user.Columns.Add("UserId", typeof(string));
            user.Columns.Add("Password", typeof(string));
            user.Columns.Add("ConfirmPassword", typeof(string));
            user.Columns.Add("Group", typeof(List<GroupTemplate>));
            user.Columns.Add("Action", typeof(Button));
            DataRow row = user.NewRow();
            row["UserId"] = "";
            row["Password"] = "";
            row["ConfirmPassword"] = "";
            row["Group"] = UserAdministrationGlobalConfig.uAdmin_GroupsList;
            Button addUser = new Button();
            addUser.Click += AddUserButtonClick;
            addUser.Content = "Add User";
            row["Action"] = addUser;
            user.Rows.Add(row);
        }

        private void AddUserButtonClick(object sender, RoutedEventArgs e)
        {
            if (validation())
            {
                int i = 3;
                ContentPresenter myCp = addUserDataGrid.Columns[i].GetCellContent(addUserDataGrid.SelectedItem) as ContentPresenter;
                var myTemplate = myCp.ContentTemplate;
                ComboBox combo = myTemplate.FindName("groupComboBox", myCp) as ComboBox;
                GroupTemplate group = (GroupTemplate)combo.SelectedItem;

                i = 1;
                myCp = addUserDataGrid.Columns[i].GetCellContent(addUserDataGrid.SelectedItem) as ContentPresenter;
                myTemplate = myCp.ContentTemplate;
                PasswordBox passwordBox = myTemplate.FindName("passwordTextBox", myCp) as PasswordBox;
                string password = passwordBox.Password.ToString();

                i = 0;
                myCp = addUserDataGrid.Columns[i].GetCellContent(addUserDataGrid.SelectedItem) as ContentPresenter;
                myTemplate = myCp.ContentTemplate;
                TextBox useridBox = myTemplate.FindName("useridTextBox", myCp) as TextBox;
                string userid = useridBox.Text;

                UserTemplate newUser = new UserTemplate();
                newUser.UserId = userid;
                newUser.Password = password;
                newUser.Group = group;
                UserAdministrationGlobalConfig.uAdmin_UsersList.Add(newUser);
                LoadUsers();
                try
                {
                    GlobalConfig.userAdministrationConnection.SaveUsersToFile();
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not save changes to file");
                }
                RefreshAddUserDataGrid();
            }
            else
            {
                MessageBox.Show("1. User ID must be unique, cannot contain spaces, must be 5-20 Characters long and must contain atleast one alphabet character." +
                    "\n2. Password must be 8-16 characters long, must contain atleast one digit, one uppercase and one lowercase character.", "Invalid credentials", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private bool validation()
        {
            bool result = true;

            int i = 3; 
            ContentPresenter myCp = addUserDataGrid.Columns[i].GetCellContent(addUserDataGrid.SelectedItem) as ContentPresenter;
            var myTemplate = myCp.ContentTemplate;
            ComboBox combo = myTemplate.FindName("groupComboBox", myCp) as ComboBox;
            GroupTemplate group = (GroupTemplate)combo.SelectedItem;

            i = 1; 
            myCp = addUserDataGrid.Columns[i].GetCellContent(addUserDataGrid.SelectedItem) as ContentPresenter;
            myTemplate = myCp.ContentTemplate;
            PasswordBox passwordBox = myTemplate.FindName("passwordTextBox", myCp) as PasswordBox;
            string password = passwordBox.Password.ToString();

            i = 0; 
            myCp = addUserDataGrid.Columns[i].GetCellContent(addUserDataGrid.SelectedItem) as ContentPresenter;
            myTemplate = myCp.ContentTemplate;
            TextBox useridBox = myTemplate.FindName("useridTextBox", myCp) as TextBox;
            string userid = useridBox.Text;

            i = 2; 
            myCp = addUserDataGrid.Columns[i].GetCellContent(addUserDataGrid.SelectedItem) as ContentPresenter;
            myTemplate = myCp.ContentTemplate;
            PasswordBox confirmPasswordBox = myTemplate.FindName("confirmPasswordTextBox", myCp) as PasswordBox;
            string confirmPassword = confirmPasswordBox.Password.ToString();

           


            bool isUserIdValid = true;
            if (!(userid.Any(char.IsUpper) || userid.Any(char.IsLower)) || userid.Any(char.IsWhiteSpace) || userid.Length < 5 || userid.Length > 20)
            {
                isUserIdValid = false;
            }
            foreach (UserTemplate user in UserAdministrationGlobalConfig.uAdmin_UsersList)
            {
                if (userid.Length == 0 || user.UserId == userid)
                {
                    useridBox.Background = new SolidColorBrush(Colors.Yellow);
                    useridBox.Foreground = new SolidColorBrush(Colors.Red);
                    isUserIdValid = false;
                    result = false;
                    break;
                }
            }
            if (isUserIdValid)
            {
                useridBox.Background = new SolidColorBrush(Colors.White);
                useridBox.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (!(password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Length >= 8 && password.Length <= 16))
            {
                passwordBox.Background = new SolidColorBrush(Colors.Yellow);
                passwordBox.Foreground = new SolidColorBrush(Colors.Red);
                result = false;
            }
            else
            {
                passwordBox.Background = new SolidColorBrush(Colors.White);
                passwordBox.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (confirmPassword != password)
            {
                confirmPasswordBox.Background = new SolidColorBrush(Colors.Yellow);
                confirmPasswordBox.Foreground = new SolidColorBrush(Colors.Red);
                result = false;
            }
            else
            {
                confirmPasswordBox.Background = new SolidColorBrush(Colors.White);
                confirmPasswordBox.Foreground = new SolidColorBrush(Colors.Black);
            }
            return result;

        }

        private void useridTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            TextBox box = sender as TextBox;
            box.Foreground = new SolidColorBrush(Colors.Black);
            box.Background = (Brush)bc.ConvertFrom("#ffccff");
        }

        private void useridTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            box.Foreground = new SolidColorBrush(Colors.Black);
            box.Background = new SolidColorBrush(Colors.White);
        }

        private void passwordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            PasswordBox box = sender as PasswordBox;
            box.Foreground = new SolidColorBrush(Colors.Black);
            box.Background = (Brush)bc.ConvertFrom("#ffccff");
        }

        private void passwordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;
            box.Foreground = new SolidColorBrush(Colors.Black);
            box.Background = new SolidColorBrush(Colors.White);
        }


        public void LoadUsers()
        {
            usersDataGrid.ItemsSource = null;
            usersDataGrid.ItemsSource = UserAdministrationGlobalConfig.uAdmin_UsersList;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            parent.userTabPage = this;
            ContainerWindow.container.ChangeFrame(parent);
        }

        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            if (isAdmin())
            {
                if(MessageBox.Show("Delete User?","Confirmation",MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                {
                    if(((UserTemplate)usersDataGrid.SelectedItem) != null && UserAdministrationGlobalConfig.uAdmin_CurrentUser.UserId == ((UserTemplate)usersDataGrid.SelectedItem).UserId)
                    {
                        UserAdministrationGlobalConfig.uAdmin_CurrentUser = null;
                    }
                    UserAdministrationGlobalConfig.uAdmin_UsersList.Remove((UserTemplate)usersDataGrid.SelectedItem);
                    
                    RefreshData();
                }
            }
        }

        private void usersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (isAdmin() && usersDataGrid.CurrentCell.Column != null)
            {
                string header = usersDataGrid.CurrentCell.Column.Header.ToString();

                if (header == "Password")
                {
                    ChangePasswordWindow window = new ChangePasswordWindow(this, (UserTemplate)usersDataGrid.SelectedItem);
                    window.Owner = Application.Current.MainWindow;
                    window.Show();
                }
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isAdmin() && usersDataGrid.CurrentCell.Column != null)
            {
                string header = usersDataGrid.CurrentCell.Column.Header.ToString();

                if (header == "Password")
                {
                    ChangePasswordWindow window = new ChangePasswordWindow(this, (UserTemplate)usersDataGrid.SelectedItem);
                    window.Owner = Application.Current.MainWindow;
                    window.Show();
                }
            }
        }


        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != null)
            {
                Type type = e.OriginalSource.GetType();
                if (type == typeof(DataGrid))
                {
                    isAdmin();
                    try
                    {
                        GlobalConfig.userAdministrationConnection.SaveUsersToFile();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not save changes to file");
                    }
                    
                }
                else if (type == typeof(ComboBox))
                {
                    
                }
            }
            
        }

        private void usersDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            GlobalConfig.userAdministrationConnection.SaveUsersToFile();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            initial = textBox.Text;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            string final = textBox.Text;
            foreach (UserTemplate user in UserAdministrationGlobalConfig.uAdmin_UsersList)
            {
                if (user.UserId == final && user.UserId != initial)
                {
                    MessageBox.Show("User ID already registered.");
                    textBox.Text = initial;
                    return;
                }
            }
            if(!(final.Any(char.IsUpper) || final.Any(char.IsLower)) || final.Any(char.IsWhiteSpace) || final.Length < 5 || final.Length > 20)
            {
                MessageBox.Show("1. User ID must be unique, cannot contain spaces, must be 5-20 Characters long and must contain atleast one alphabet character.", "Invalid User ID", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                textBox.Text = initial;
            }
        }
    }
}
