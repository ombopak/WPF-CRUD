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

namespace LearnWPF.Presenter
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        int selectedBookId;
        public EditBookWindow(int bookId)
        {
            InitializeComponent();
            selectedBookId = bookId;
            GetBookData();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditBookData();
                this.Close();
            }
            catch
            {
                string error = "Something went wrong, cannot edit book";
                MessageBox.Show(error, "Warning", MessageBoxButton.OK);
            }
        }

        void GetBookData()
        {
            Book book = new Book();
            book = book.GetBookData(selectedBookId);

            txtTitle.Text = book.Title;
            txtISBN.Text = book.Isbn;
            txtPublisher.Text = book.PublisherName;
            txtAuthor.Text = book.AuthorName;
            txtCategory.Text = book.CategoryName;

        }

        void EditBookData()
        {
                Book book = new Book();

                book.BookId = selectedBookId;
                book.Title = txtTitle.Text;
                book.Isbn = txtISBN.Text;
                book.PublisherName = txtPublisher.Text;
                book.AuthorName = txtAuthor.Text;
                book.CategoryName = txtCategory.Text;

                book.EditBook(book);
           

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
