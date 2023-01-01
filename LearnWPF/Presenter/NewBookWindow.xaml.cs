using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using Newtonsoft.Json;

namespace LearnWPF.Presenter
{
    /// <summary>
    /// Interaction logic for NewBookWindow.xaml
    /// </summary>
    public partial class NewBookWindow : Window
    {
        string baseAddress = "https://localhost:5001/api/Books/";

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

        async void SaveBookData()
        {
            //USING REST API
            string getAllBooks = "AddBook";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);

            // Set the content type to JSON
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Create a new book object with the desired data
            Book book = new Book();
            book.Title = txtTitle.Text;
            book.Isbn = txtISBN.Text;
            book.PublisherName = txtPublisher.Text;
            book.AuthorName = txtAuthor.Text;
            book.CategoryName = txtCategory.Text;

            // Serialize the book object to a JSON string
            var json = JsonConvert.SerializeObject(book);

            // Create a new HttpContent object with the serialized JSON string
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send a POST request to the API, with the book data in the request body
            var response = await client.PostAsync(getAllBooks, content);

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



            //USING SQL SERVER CONNECTION
            //Book book = new Book();
            //book.Title = txtTitle.Text;
            //book.Isbn = txtISBN.Text;
            //book.PublisherName = txtPublisher.Text;
            //book.AuthorName = txtAuthor.Text;
            //book.CategoryName = txtCategory.Text;
            //book.CreateBook(book);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
