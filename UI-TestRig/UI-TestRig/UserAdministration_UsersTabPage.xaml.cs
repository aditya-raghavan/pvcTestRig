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
using TestRigLibrary;
using TestRigLibrary.Templates;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for UserAdministration_UsersTabPage.xaml
    /// </summary>
    public partial class UserAdministration_UsersTabPage : Page,IPage
    {
        public DiagnosticsPage parent { get; set; }

        public AddUserWindow addUserWindow { get; set; }

        bool buttonFlag = false;
        public UserAdministration_UsersTabPage(DiagnosticsPage p)
        {
            InitializeComponent();
            parent = p;
            CreateUserGrid();
            LoadUsers();
            LoadGroups();
        }

        public void RefreshData()
        {
            CreateUserGrid();
            LoadUsers();
            isAdmin();
            CheckUser();
        }

        public void CheckUser()
        {
            if (GlobalConfig.uAdmin_CurrentUser != null)
            {
                userTextBox.Text = GlobalConfig.uAdmin_CurrentUser.UserId;
                
            }
            else
            {
                userTextBox.Text = "";

            }
        }

        public bool isAdmin()
        {
            if(GlobalConfig.uAdmin_CurrentUser!= null && GlobalConfig.uAdmin_CurrentUser.Group.GroupId == 1)
            {
                addUserButton.Content = "Add User";
                return true;
            }
            else
            {
                addUserButton.Content = "";
                return false;
            }

        }

        public void LoadGroups()
        {
            int count = 1;
            foreach (GroupTemplate group in GlobalConfig.GroupsList)
            {

                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                groupGrid.RowDefinitions.Add(row);
                var bc = new BrushConverter();

                Border border1 = new Border();
                border1.BorderThickness = new Thickness(2);
                border1.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
                TextBlock groupName = new TextBlock();
                groupName.Text = group.GroupName;
                border1.Child = groupName;
                Grid.SetRow(border1, count);
                Grid.SetColumn(border1, 0);

                Border border2 = new Border();
                border2.BorderThickness = new Thickness(2);
                border2.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
                TextBlock functions = new TextBlock();
                functions.Text = "";
                string str = "";
                if(group.AllowedFunctions.Count != 0)
                {
                    foreach (string function in group.AllowedFunctions)
                    {
                        str = str + function + ", ";
                    }
                    str = str.Substring(0, str.Length - 2);
                }
                
                
                functions.Text = str;

                border2.Child = functions;
                Grid.SetRow(border2, count);
                Grid.SetColumn(border2, 1);

                groupGrid.Children.Add(border1);
                groupGrid.Children.Add(border2);

                count++;
            }
        }

        public void CreateUserGrid()
        {
            usersGrid.Children.Clear();
            
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            usersGrid.RowDefinitions.Add(row);

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = GridLength.Auto;
            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = GridLength.Auto;
            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = GridLength.Auto;
            ColumnDefinition col4 = new ColumnDefinition();
            col4.Width = GridLength.Auto;
            ColumnDefinition col5 = new ColumnDefinition();
            col5.Width = GridLength.Auto;
            ColumnDefinition col6 = new ColumnDefinition();
            col6.Width = GridLength.Auto;

            usersGrid.ColumnDefinitions.Add(col1);
            usersGrid.ColumnDefinitions.Add(col2);
            usersGrid.ColumnDefinitions.Add(col3);
            usersGrid.ColumnDefinitions.Add(col4);
            usersGrid.ColumnDefinitions.Add(col5);
            usersGrid.ColumnDefinitions.Add(col6);

            var bc = new BrushConverter();

            Border border1 = new Border();
            border1.BorderThickness = new Thickness(2);
            border1.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
            TextBlock userid = new TextBlock();
            userid.Background = new SolidColorBrush(Colors.Orange);
            userid.Foreground = new SolidColorBrush(Colors.White);
            userid.FontSize = 12;
            userid.Padding = new Thickness(8, 12, 8, 12);
            userid.FontWeight = FontWeights.Bold;
            userid.TextAlignment = TextAlignment.Left;
            userid.Text = "User ID";
            border1.Child = userid;
            Grid.SetRow(border1, 0);
            Grid.SetColumn(border1, 0);

            Border border2 = new Border();
            border2.BorderThickness = new Thickness(2);
            border2.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
            TextBlock password = new TextBlock();
            password.Background = new SolidColorBrush(Colors.Orange);
            password.Foreground = new SolidColorBrush(Colors.White);
            password.FontSize = 12;
            password.Padding = new Thickness(8, 12, 8, 12);
            password.FontWeight = FontWeights.Bold;
            password.TextAlignment = TextAlignment.Left;
            password.Text = "Password";
            border2.Child = password;
            Grid.SetRow(border2, 0);
            Grid.SetColumn(border2, 1);

            Border border3 = new Border();
            border3.BorderThickness = new Thickness(2);
            border3.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
            TextBlock groups = new TextBlock();
            groups.Background = new SolidColorBrush(Colors.Orange);
            groups.Foreground = new SolidColorBrush(Colors.White);
            groups.FontSize = 12;
            groups.Padding = new Thickness(8, 12, 8, 12);
            groups.FontWeight = FontWeights.Bold;
            groups.TextAlignment = TextAlignment.Left;
            groups.Text = "Groups";
            border3.Child = groups;
            Grid.SetRow(border3, 0);
            Grid.SetColumn(border3, 2);

            Border border4 = new Border();
            border4.BorderThickness = new Thickness(2);
            border4.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
            TextBlock functions = new TextBlock();
            functions.Background = new SolidColorBrush(Colors.Orange);
            functions.Foreground = new SolidColorBrush(Colors.White);
            functions.FontSize = 12;
            functions.Padding = new Thickness(8, 12, 8, 12);
            functions.FontWeight = FontWeights.Bold;
            functions.TextAlignment = TextAlignment.Left;
            functions.Text = "Functions";
            border4.Child = functions;
            Grid.SetRow(border4 , 0);
            Grid.SetColumn(border4, 3);

            usersGrid.Children.Add(border1);
            usersGrid.Children.Add(border2);
            usersGrid.Children.Add(border3);
            usersGrid.Children.Add(border4);

            


        }

        public void LoadUsers()
        {
            int count = 1;
            foreach (UserTemplate user in GlobalConfig.UsersList)
            {

                RowDefinition row = new RowDefinition();
                row.Name = $"dynamicrow{count - 1}";
                row.Height = GridLength.Auto;
                
                usersGrid.RowDefinitions.Add(row);
                var bc = new BrushConverter();

                Border border1 = new Border();
                border1.BorderThickness = new Thickness(2);
                border1.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
                TextBox userid = new TextBox();
                userid.Name = $"dynamicuseridTextBox{count - 1}";
                userid.IsReadOnly = true;
                userid.Background = (Brush)bc.ConvertFrom("#ccffff");
                userid.Foreground = new SolidColorBrush(Colors.Black);
                userid.FontSize = 12;
                userid.Padding = new Thickness(8, 12, 8, 12);
                userid.FontWeight = FontWeights.Bold;
                userid.TextAlignment = TextAlignment.Left;
                userid.Text = user.UserId;
                border1.Child = userid;
                Grid.SetRow(border1, count);
                Grid.SetColumn(border1, 0);

                Border border2 = new Border();
                border2.BorderThickness = new Thickness(2);
                border2.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
                PasswordBox password = new PasswordBox();
                password.Background = new SolidColorBrush(Colors.White);
                password.Foreground = new SolidColorBrush(Colors.Black);
                password.FontSize = 12;
                password.Padding = new Thickness(8, 12, 8, 12);
                password.FontWeight = FontWeights.Bold;
                password.HorizontalContentAlignment = HorizontalAlignment.Left;
                password.Password = user.Password;
                password.Focusable = false;                
                password.Name = $"dynamicpasswordTextBox{count - 1}";
                border2.Child = password;
                Grid.SetRow(border2, count);
                Grid.SetColumn(border2, 1);

                Border border3 = new Border();
                border3.BorderThickness = new Thickness(2);
                border3.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
                TextBlock groupName = new TextBlock();
                groupName.Background = new SolidColorBrush(Colors.White);
                groupName.Foreground = new SolidColorBrush(Colors.Black);
                groupName.FontSize = 12;
                groupName.Padding = new Thickness(8, 12, 8, 12);
                groupName.FontWeight = FontWeights.Bold;
                groupName.TextAlignment = TextAlignment.Left;
                groupName.Text = user.Group.GroupName;
                groupName.Name = $"dynamicuserGroupTextBlock{count - 1 }";
                border3.Child = groupName;
                Grid.SetRow(border3, count);
                Grid.SetColumn(border3, 2);

                Border border4 = new Border();
                border4.BorderThickness = new Thickness(2);
                border4.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
                TextBlock functions = new TextBlock();
                functions.Name = $"dynamicuserFunctionsTextBlock{count - 1}";
                functions.Background = new SolidColorBrush(Colors.White);
                functions.Foreground = new SolidColorBrush(Colors.Black);
                functions.FontSize = 12;
                functions.Padding = new Thickness(8, 12, 8, 12);
                functions.FontWeight = FontWeights.Bold;
                functions.TextAlignment = TextAlignment.Left;
                functions.Text = "";
                string str = "";
                if(user.Group.AllowedFunctions.Count != 0)
                {
                    foreach (string function in user.Group.AllowedFunctions)
                    {
                        str = str + function + ", ";
                    }
                    str = str.Substring(0, str.Length - 2);
                }
                
                
                functions.Text = str;

                border4.Child = functions;
                Grid.SetRow(border4, count);
                Grid.SetColumn(border4, 3);

                if(GlobalConfig.uAdmin_CurrentUser!=null && GlobalConfig.uAdmin_CurrentUser.Group.GroupId == 1)
                {
                    Border border5 = new Border();
                    border5.BorderThickness = new Thickness(2);
                    border5.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");

                    Border border6 = new Border();
                    border6.BorderThickness = new Thickness(2);
                    border6.BorderBrush = (Brush)bc.ConvertFrom("#4d4d4d");
                    

                    Button modify = new Button();
                    modify.FontWeight = FontWeights.Bold;
                    modify.Padding = new Thickness(6, 3, 6, 3);
                    modify.Background = (Brush)bc.ConvertFrom("#999999");
                    modify.Foreground = new SolidColorBrush(Colors.White);
                   
                    modify.Name = $"dynamicmodifyUserButton{count - 1}";
                    modify.Content = "Edit";
                    modify.MinWidth = 50;
                    modify.Click += ModifyUser;

                    Button delete = new Button();
                    delete.Name = $"dynamicdeleteUserButton{count - 1}";
                    delete.FontWeight = FontWeights.Bold;
                    delete.Padding = new Thickness(6, 3, 6, 3);
                    delete.Background = (Brush)bc.ConvertFrom("#999999");
                    delete.Foreground = new SolidColorBrush(Colors.White);
                    delete.Content = "Delete";
                    delete.Click += DeleteUser;

                    border5.Child = modify;
                    border6.Child = delete;
                    Grid.SetRow(border5, count);
                    Grid.SetColumn(border5, 4);

                    Grid.SetRow(border6, count);
                    Grid.SetColumn(border6, 5);

                    usersGrid.Children.Add(border5);
                    usersGrid.Children.Add(border6);
                }

                
                usersGrid.Children.Add(border1);
                usersGrid.Children.Add(border2);
                usersGrid.Children.Add(border3);
                usersGrid.Children.Add(border4);

                count++;
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete User?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender as Button;
                string name = btn.Name.ToString();
                char userIndex = name[name.Length - 1];
                int index = int.Parse(userIndex.ToString());
                UserTemplate user = GlobalConfig.UsersList[index];
                if (user == GlobalConfig.uAdmin_CurrentUser)
                {
                    GlobalConfig.uAdmin_CurrentUser = null; 
                }
                GlobalConfig.UsersList.Remove(user);
                GlobalConfig.Connection.SaveUsersToFile();
                RefreshData();
            }
            
            
        }

        public bool WarnUser()
        {
            if ((MessageBox.Show("You are about to change credentials of an Administrator. Are you sure you want to continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                        == MessageBoxResult.Yes))
            {
                return true;
            }
            return false;
        }

        private void ModifyUser(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender as Button;
            string name = btn.Name.ToString();
            char userIndex = name[name.Length - 1];
            int index = int.Parse(userIndex.ToString());
            UserTemplate user = GlobalConfig.UsersList[index];
            ModifyUserWindow window = new ModifyUserWindow(user, this);
            window.Owner = Application.Current.MainWindow;
            window.Show();
            
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            parent.userTabPage = this;
            ContainerWindow.container.ChangeFrame(parent);
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin())
            {
                if(buttonFlag == false)
                {
                    addUserWindow = new AddUserWindow(this);
                    addUserWindow.Owner = Application.Current.MainWindow;
                    addUserWindow.Closing += addUserClose;
                    buttonFlag = true;
                    addUserWindow.Show();
                }
            }
        }

        private void addUserClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            buttonFlag = false;
        }

        public void AddUser(UserTemplate user)
        {
            GlobalConfig.UsersList.Add(user);
            try
            {
                GlobalConfig.Connection.SaveUsersToFile();
            }
            catch (Exception)
            {
                MessageBox.Show("Could not save users to file.");
            }
            LoadUsers();
        }

        
    }
}
