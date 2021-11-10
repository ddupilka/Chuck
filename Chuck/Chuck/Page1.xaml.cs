using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;

namespace Chuck
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        List<Class1> Chucks = new List<Class1>();
        Class1 obj = new Class1();

        public static readonly HttpClient client = new HttpClient();

        public Page1(bool delete, int index)
        {
            InitializeComponent();

            if(delete && index >= 0)
            {
                DeleteButton.IsVisible = true;
                obj.id = Chucks.ElementAt(index).id;
            }
        }

        async private void DeleteFact(object sender, EventArgs e)
        {
            await Delete(obj);
            Chucks.Remove(obj);
            await Navigation.PopAsync();
        }
        async private void SaveBut(object sender, EventArgs e)
        {
            
            if (factEntry.Text == null)
            {
                await DisplayAlert("Error", "Please enter a fact.", "OK");
            }
            else
            {
                await Post(obj);
                Chucks.Add(obj);
                await Navigation.PopAsync();
            }
        }

        async private void CancelFact(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public async Task Post(Class1 obj)
        {
            obj.ChuckQuote = factEntry.Text;

            var uri = new Uri("https://mobileclass.azurewebsites.net/api/chucks");

            String json = JsonConvert.SerializeObject(obj);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = new HttpResponseMessage();
            response = await client.PostAsync(uri, strContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Post Complete");
            }
        }
        public async Task Delete(Class1 obj)
        {
            HttpClient client;
            client = new HttpClient();

            var uri = new Uri("https://mobileclass.azurewebsites.net/api/chucks/" + obj.id);

            Class1 toDelete = new Class1();
            toDelete.id = obj.id;
            toDelete.userID = obj.userID;

            String json = JsonConvert.SerializeObject(toDelete);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Delete;
            request.RequestUri = uri;
            request.Content = strContent;

            HttpResponseMessage response = await client.SendAsync(request);
        }
    }
}