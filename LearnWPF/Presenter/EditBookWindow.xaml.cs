using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
using System.Windows.Shapes;
using LearnWPF.Model;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;

namespace LearnWPF.Presenter
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        int selectedBookId;
        string baseAddress = "https://localhost:5001/api/Books/";

        public EditBookWindow(int bookId)
        {
            Debug.WriteLine( bookId + " THIS IS YOUR BOOK ID" );
            selectedBookId = bookId;
            InitializeComponent();
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

            //USING REST API
            int id = selectedBookId;
            string GetBookById = $"GetBookById?BookId={id}";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);


            Task<HttpResponseMessage> responseTask = client.GetAsync(GetBookById);

         

            responseTask.Wait();
            HttpResponseMessage response = responseTask.Result;
            HttpContent content = response.Content;
            string result = content.ReadAsStringAsync().Result;
            List<Book> book = JsonConvert.DeserializeObject<List<Book>>(result);




            txtTitle.Text = book[0].Title;
            txtISBN.Text = book[0].Isbn;
             txtPublisher.Text = book[0].PublisherName;
            txtAuthor.Text = book[0].AuthorName;
            txtCategory.Text = book[0].CategoryName;


            //USING SQL SERVER CONNECTION
            //Book book = new Book();
            //book = book.GetBookData(selectedBookId);

            //txtTitle.Text = book.Title;
            //txtISBN.Text = book.Isbn;
            // txtPublisher.Text = book.PublisherName;
            //txtAuthor.Text = book.AuthorName;
            //txtCategory.Text = book.CategoryName;

        }

        async void EditBookData()
        {

            //USING REST API
            string EditBook = "EditBook";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);

            // Set the content type to JSON
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Create a new book object with the desired data
            Book book = new Book();
            book.BookId = selectedBookId;
            book.Title = txtTitle.Text;
            book.Isbn = txtISBN.Text;
            book.PublisherName = txtPublisher.Text;
            book.AuthorName = txtAuthor.Text;
            book.CategoryName = txtCategory.Text;

            var json = JsonConvert.SerializeObject(book);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(EditBook, content);

            // Check the status code of the response
            if (response.IsSuccessStatusCode)
            {
                // If the status code is 200 OK, retrieve the response content
                //var responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to an object
                //var responseData = JsonConvert.DeserializeObject(responseContent);

                // Use the data in the response object as needed
                // ...
                MessageBox.Show("Book Added");
            }
            else
            {
                // If the status code is not 200 OK, handle the error as needed
                // ...
                MessageBox.Show("Failed To Add new Book");

            }


            //Book book = new Book();

            //book.BookId = selectedBookId;
            //book.Title = txtTitle.Text;
            //book.Isbn = txtISBN.Text;
            //book.PublisherName = txtPublisher.Text;
            //book.AuthorName = txtAuthor.Text;
            //book.CategoryName = txtCategory.Text;

            //book.EditBook(book);


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
