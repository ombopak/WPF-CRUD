using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace LearnWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        // Set the base address of the API
        string baseAddress = "https://localhost:5001/api/Books/";
        string editBook = "EditBook?BookId={id}&BookTitle={title}&ISBN={isbn}&PublisherName={publisherName}&AuthorName={authorName}&CategoryName={categoryName}";
        string addBook = "AddBook?Title={title}&Isbn={isbn}&PublisherName={publisherName}&AuthorName={authorName}&CategoryName={categoryName}";

        


        public MainWindow()
        {
            InitializeComponent();
            FillGridView();


            // Create a new HttpClient and set its base address
           


            // Send a GET request to the API and get the response asynchronously
            //Task<HttpResponseMessage> responseTask = client.GetAsync("endpoint");

            // Wait for the response to complete
           // responseTask.Wait();

            // Get the response
            //HttpResponseMessage response = responseTask.Result;

            // Get the content of the response
            //HttpContent content = response.Content;

            // Read the content as a string
           // string result = content.ReadAsStringAsync().Result;

            // Do something with the result
           // Console.WriteLine(result);
        }

        void FillGridView()
        {
            //USING REST API
            string getAllBooks = "GetAllBooks";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);

            Task<HttpResponseMessage> responseTask = client.GetAsync(getAllBooks);
            responseTask.Wait();
            HttpResponseMessage response = responseTask.Result;
            HttpContent content = response.Content;
            string result = content.ReadAsStringAsync().Result;
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(result);
            dataGridViewBooks.ItemsSource = books;

            //USING SQL SERVER CONNECTION
            //List<Book> bookList = new List<Book>();
            //Book book = new Book();
            //bookList = book.GetBooks();
            //dataGridViewBooks.ItemsSource = bookList;
            
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
            //USING DATABASE SQL CONNECTION
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
                        Debug.WriteLine(bookId);
                        string deleteBook = $"DeleteBook?BookId={bookId}";

                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(baseAddress);


                        HttpResponseMessage responseTask = client.DeleteAsync(deleteBook).Result;

                        if(responseTask.StatusCode == HttpStatusCode.OK)
                        {
                            MessageBox.Show("Book Deleted");

                        }
                        else
                        {
                            MessageBox.Show("Something Wrong, Book didnt get deleted");
                        }


                        //book.DeleteBook(bookId);
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
