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
using LearnWPF.Model;
using Microsoft.IdentityModel.Tokens;

namespace LearnWPF.Presenter
{
    /// <summary>
    /// Interaction logic for NewBookWindow.xaml
    /// </summary>
    public partial class NewBookWindow : Window
    {
        public NewBookWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //if(txtTitle.Text.IsNullOrEmpty)
            try
            {

                SaveBookData();
                this.Close();
            }
            catch
            {
                string error = "Something went wrong, cannot add book";
                MessageBox.Show(error, "Warning", MessageBoxButton.OK);
            }
            

        }

        void SaveBookData()
        {
            Book book = new Book();

            book.Title = txtTitle.Text;
            book.Isbn = txtISBN.Text;
            book.PublisherName = txtPublisher.Text;
            book.AuthorName = txtAuthor.Text;
            book.CategoryName = txtCategory.Text;

          
            
            book.CreateBook(book);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
