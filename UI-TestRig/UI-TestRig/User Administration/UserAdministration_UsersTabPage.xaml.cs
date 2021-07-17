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

        public UserTemplate selectedUser;
        public DiagnosticsPage parent { get; set; }
        string initial = "";
        public string final = "";
        public bool isCurrentUserAdmin = false;

        public UserAdministration_UsersTabPage(DiagnosticsPage p)
        {
            InitializeComponent();
            this.DataContext = this;
            parent = p;
            CreateUserDataGrid();
            CreateGroupsDataTable();
            LoadGroups();
        }

        public void RefreshData()
        {
            LoadUsers();
            isAdmin();
            CheckUser();
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

        private bool CheckIfAdminChanged()
        {
            if (UserAdministrationGlobalConfig.uAdmin_CurrentUser != null && UserAdministrationGlobalConfig.uAdmin_CurrentUser.Group.GroupId == 1)
            {
                return true;
            }
            else
            {
                return false;
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
                usersDataGrid.CanUserAddRows = true;
                deleteContextMenu.IsEnabled = true;
                usersDataGrid.IsReadOnly = false;
                foreach(UserTemplate user in UserAdministrationGlobalConfig.uAdmin_UsersList)
                {
                    user.CanChangeGroup = true;
                }
                return true;
            }
            else
            {

                try
                {
                    usersDataGrid.IsReadOnly = true;
                }
                catch (Exception)
                {
                    LoadUsers();
                    usersDataGrid.IsReadOnly = true;
                }
                deleteContextMenu.IsEnabled = false;

                foreach (UserTemplate user in UserAdministrationGlobalConfig.uAdmin_UsersList)
                {
                    user.CanChangeGroup = false;
                }
                return false;
            }
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

        public void LoadUsers()
        {
            usersDataGrid.ItemsSource = null;
            usersDataGrid.ItemsSource = UserAdministrationGlobalConfig.uAdmin_UsersList;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<UserTemplate> toRemoveUsers = new List<UserTemplate>();

            foreach(UserTemplate user in UserAdministrationGlobalConfig.uAdmin_UsersList)
            {
                if (!(user.UserId != null && user.UserId.Length != 0 && user.Password != null && user.Password.Length != 0 && user.Group != null))
                {
                    toRemoveUsers.Add(user);
                }
            }
            foreach(UserTemplate user in toRemoveUsers)
            {
                UserAdministrationGlobalConfig.uAdmin_UsersList.Remove(user);
            }
            parent.userTabPage = this;
            ContainerWindow.container.ChangeFrame(parent);
        }

        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            DeleteUser();
        }

        private bool DeleteUser()
        {
            if (CheckIfAdminChanged())
            {
                if (!(usersDataGrid.SelectedItem is UserTemplate))
                {
                    return false;
                }
                UserTemplate user = (UserTemplate)usersDataGrid.SelectedItem;
                if ((UserTemplate)usersDataGrid.SelectedItem != null && user.UserId != null && user.UserId.Length != 0 && user.Password != null && user.Password.Length != 0 && user.Group != null)
                {
                    if (MessageBox.Show($"Delete '{user.UserId}' ?", "Confirmation", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                    {
                        if (((UserTemplate)usersDataGrid.SelectedItem) != null && UserAdministrationGlobalConfig.uAdmin_CurrentUser.UserId == ((UserTemplate)usersDataGrid.SelectedItem).UserId)
                        {
                            UserAdministrationGlobalConfig.uAdmin_CurrentUser = null;
                        }
                        UserAdministrationGlobalConfig.uAdmin_UsersList.Remove(user);
                        //RefreshData();
                        return true;
                    }
                }

            }
            return false;
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isCurrentUserAdmin && usersDataGrid.CurrentCell.Column != null)
            {
                string header = usersDataGrid.CurrentCell.Column.Header.ToString();

                if (header == "Password")
                {
                    ChangePasswordWindow window = new ChangePasswordWindow(this, (UserTemplate)usersDataGrid.SelectedItem);                   
                    window.Owner = Application.Current.MainWindow;
                    window.ShowDialog();
                }
            }
        }

        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isCurrentUserAdmin)
            {
                if (e.OriginalSource != null)
                {
                    Type type = e.OriginalSource.GetType();

                    if (type == typeof(DataGrid))
                    {
                        
                        CheckUser();
                        if (CheckIfAdminChanged() == false && isCurrentUserAdmin == true)
                        {
                            isAdmin();
                            LoadUsers();
                        }
                        try
                        {
                            GlobalConfig.userAdministrationConnection.SaveUsersToFile();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Could not save changes to file");
                        }

                    }
                   
                }
            }
        }

        private void usersDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            GlobalConfig.userAdministrationConnection.SaveUsersToFile();
        }

        private void usersDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            string header = e.Column.Header.ToString();

            if (header == "User ID")
            {

                TextBox text = e.EditingElement as TextBox;
                string userid = text.Text;
                foreach (UserTemplate user in UserAdministrationGlobalConfig.uAdmin_UsersList)
                {
                    if (user != (UserTemplate)e.Row.Item)
                    {
                        if (user.UserId == userid && user.UserId != initial && user.UserId != "")
                        {
                            MessageBox.Show("User ID already registered.");
                            text.Text = initial;
                            initial = "";
                            return;
                        }
                    }
                }
                if (!(userid.Any(char.IsUpper) || userid.Any(char.IsLower)) || userid.Any(char.IsWhiteSpace) || userid.Length < 5 || userid.Length > 20)
                {
                    if(initial.Length != 0)
                    {
                        MessageBox.Show("1. User ID must be unique, cannot contain spaces, must be 5-20 Characters long and must contain atleast one alphabet character.", "Invalid User ID",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    }
                    text.Text = initial;
                    initial = "";
                    return;
                }
            }
            initial = "";
        }

        private void usersDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            string header = usersDataGrid.CurrentCell.Column.Header.ToString();
            if (header == "User ID" && usersDataGrid.SelectedItem is UserTemplate)
            {
                initial = ((UserTemplate)usersDataGrid.SelectedItem).UserId;
            }
        }

        private void usersDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            CheckUser();
            UserTemplate user = (UserTemplate)e.Row.Item;
            if (user.UserId != null && user.UserId.Length != 0 && user.Password != null && user.Password.Length != 0 && user.Group != null)
            {
                
            }
            else
            {
                //if (!(user.UserId.Length == 0 && user.Password.Length == 0 && user.Group == null))
                //{
                //    MessageBox.Show("Please fill in all the credentials.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //LoadUsers();
                //usersDataGrid.SelectedItem = UserAdministrationGlobalConfig.uAdmin_CurrentUser;
                e.Cancel = true;
            }
        }
        private void usersDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                DeleteUser();
            }
        }

        private void dataGridCellEnter(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isCurrentUserAdmin)
            {
                if (usersDataGrid.SelectedItem is UserTemplate == false)
                {
                    if (isAdmin() && usersDataGrid.CurrentCell.Column != null)
                    {
                        usersDataGrid.BeginEdit();
                    }
                }
            }
        }
    }
}
