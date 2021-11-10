using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;

namespace Chuck
{
    public partial class MainPage : ContentPage
    {
        public static readonly HttpClient client = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            myListView.ItemsSource = await RefreshDataAsync();
        }

        public async Task<List<Class1>> RefreshDataAsync()
        {
            var uri = new Uri("https://mobileclass.azurewebsites.net/api/chucks/647dad188");

            HttpResponseMessage response = await client.GetAsync(uri);
            List<Class1> Items = null;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                Items = JsonConvert.DeserializeObject<List<Class1>>(content);
            }
            return Items;
        }

        async private void AddFact_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1(false,-1));
        }

        async private void ListClickedNow(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new Page1(true, e.ItemIndex));
        }
    }
}
