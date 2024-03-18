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
using Dictionary.MVVM.ViewModel;

namespace Dictionary.MVVM.View
{
    /// <summary>
    /// Interaction logic for RemoveWordDialog.xaml
    /// </summary>
    public partial class RemoveWordDialog : Window
    {
        public RemoveWordDialog()
        {
            InitializeComponent();
            Loaded += RemoveDialog_Loaded;
        }

        private void RemoveDialog_Loaded(object sender, RoutedEventArgs e)
        {
            // Retrieve the DataContext (AdminViewModel)
            var viewModel = DataContext as AdminViewModel;
            if (viewModel != null)
            {
                // Populate categories
                viewModel.PopulateCategories();

                // Bind Categories to ComboBox
                CategoryComboBox.ItemsSource = viewModel.Categories;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve entered data from TextBox controls
            string word = WordTextBox.Text;
            string category = CategoryComboBox.Text;

            // Call method in AdminViewModel to add the word
            ((AdminViewModel)DataContext).manager.RemoveWord(word, category);

            // Close the dialog
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog
            Close();
        }
    }
}
