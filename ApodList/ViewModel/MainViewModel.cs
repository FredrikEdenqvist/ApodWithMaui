using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApodList.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private static readonly string ApodUrl = "https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY";
        private static string Url => $"{ApodUrl}&start_date={DateTime.Now.AddDays(-10):yyyy-MM-dd}";
        private readonly HttpClient client;
        private readonly IConnectivity connectivity;
        [ObservableProperty]
        ObservableCollection<Apod> headers = new();

        public MainViewModel(HttpClient client, IConnectivity connectivity)
        {
            this.client = client;
            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task ReFresh()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No Internet", "No Internet access available", "Ok");
                return;
            }

            HttpRequestMessage request = new(HttpMethod.Get, Url);
            request.Headers.Add("Accept", "application/json");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await JsonSerializer.DeserializeAsync<Apod[]>(await response.Content.ReadAsStreamAsync());
                if (json != null && json.Length > 0)
                {
                    Headers.Clear();
                    foreach (var item in json.Where(x => x.IsImage)) // Only supports Images
                    {
                        Headers.Add(item);
                    }
                }
            }
        }

        [RelayCommand]
        async Task Tap(Apod item)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", new Dictionary<string, object>
            {
                { "Apod", item }
            });
        }
    }
}
