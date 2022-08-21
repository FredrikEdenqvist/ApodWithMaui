using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApodList.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private static readonly string ApodUrl = "https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY";
        private static string Url => $"{ApodUrl}&start_date={DateTime.Now.AddDays(-7):yyyy-MM-dd}";
        private readonly HttpClient client;

        [ObservableProperty]
        ObservableCollection<Apod> headers = new();

        public MainViewModel(HttpClient client)
        {
            this.client = client;
        }

        [RelayCommand]
        async Task ReFresh()
        {
            HttpRequestMessage request = new(HttpMethod.Get, Url);
            request.Headers.Add("Accept", "application/json");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(contentString));
                var json = await JsonSerializer.DeserializeAsync<Apod[]>(stream);
                if (json != null && json.Length > 0)
                {
                    Headers.Clear();
                    foreach (var item in json)
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
