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
    /// Interaction logic for UserAdministration.xaml
    /// </summary>
    public partial class UserAdministration : Page
    {
        public DiagnosticsPage parent { get; set; }
        public UserAdministration(DiagnosticsPage p)
        {

            InitializeComponent();
            parent = p;
            LoadGroups();
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

        public void LoadGroups()
        {
            int count = 1;
            foreach(GroupTemplate group in GlobalConfig.GroupsList)
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
                foreach(string function in group.AllowedFunctions)
                {
                    str = str + function + ", ";
                }
                str = str.Substring(0, str.Length - 2);
                functions.Text = str;
                
                border2.Child = functions;
                Grid.SetRow(border2, count);
                Grid.SetColumn(border2, 1);

                groupGrid.Children.Add(border1);
                groupGrid.Children.Add(border2);

                count++;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            parent.userAdminPage = this;
            ContainerWindow.container.ChangeFrame(parent);
        }
    }
}
