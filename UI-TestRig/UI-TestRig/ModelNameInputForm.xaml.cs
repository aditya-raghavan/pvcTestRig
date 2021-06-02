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
using TestRigLibrary;

namespace UI_TestRig
{
    /// <summary>
    /// Interaction logic for ModelNameInputForm.xaml
    /// </summary>
    public partial class ModelNameInputForm : Window
    {
        /// <summary>
        /// A Form which has implemented IModelRequestor Interface.
        /// </summary>
        
        /// <summary>
        /// List of existing Model names.
        /// </summary>
        List<string> ModelNames;
        public ModelNameInputForm()
        {
            InitializeComponent();
            
            ModelNames = GlobalConfig.Connection.GetAllModelNames();
            loadListBox();
            
        }

        private void loadListBox()
        {
            modelNamesListBox.ItemsSource = null;
            modelNamesListBox.ItemsSource = ModelNames;
            modelNamesListBox.SelectedIndex = 0;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (modelNamesListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a model to be deleted.");
            }
            else
            {
                if (MessageBox.Show($"Delete Model?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GlobalConfig.Connection.DeleteModel(modelNamesListBox.SelectedItem.ToString());
                    ModelNames.Remove(modelNamesListBox.SelectedItem.ToString());
                    loadListBox();
                    MessageBox.Show("Model Deleted");
                    
                }
            }
            
        }

        
    }
}
