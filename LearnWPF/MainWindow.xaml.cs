using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using LearnWPF.Model;
using LearnWPF.Presenter;

namespace LearnWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillGridView();
        }

        void FillGridView()
        {
            List<Book> bookList = new List<Book>();

            Book book = new Book();

            bookList = book.GetBooks();

            dataGridViewBooks.ItemsSource = bookList;

        }

        private void btnNewBookForm_Click(object sender, RoutedEventArgs e)
        {
            NewBookWindow newBookWindow = new NewBookWindow();
            newBookWindow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            FillGridView();
        }

        private void btnEditBookWindow_Click(object sender, RoutedEventArgs e)
        {
            EditBook();
        }

        void EditBook()
        {
            try
            {
                
                Book row = dataGridViewBooks.SelectedCells[0].Item as Book;

                int bookId = row.BookId;

                // Debug.WriteLine(bookId);

                EditBookWindow editBookWindow = new EditBookWindow(bookId);
                editBookWindow.ShowDialog();
                
            }
            catch
            {
                string message = "Please choose book to edit!";
                var dr = MessageBox.Show(message, "Warning", MessageBoxButton.OK);
            }
        }

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            DeleteBook();
        }

        void DeleteBook()
        {

            //TODO: get the bookid data from datagrid

            try
            {
                Book row = dataGridViewBooks.SelectedCells[0].Item as Book;
                var bookId = row.BookId;
                string bookTitle = row.Title;


                string message = "Are you sure want to delete the book '" + bookTitle + "'?";
                var dr = MessageBox.Show(message, "Delete", MessageBoxButton.YesNo);

                if (dr == MessageBoxResult.Yes)
                {
                    Book book = new Book();

                    try
                    {


                        book.DeleteBook(bookId);
                        FillGridView();
                    }
                    catch
                    {


                        string error = "Something went wrong, cannot delete book";
                        MessageBox.Show(error, "Warning", MessageBoxButton.OK);

                    }
                  
                    
                }
            } catch
            {
                string message = "Please choose book to delete!";
                var dr = MessageBox.Show(message, "Warning", MessageBoxButton.OK);
            }
            

            
            

        }

       

        private void Window_Activated_1(object sender, EventArgs e)
        {
            FillGridView();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
