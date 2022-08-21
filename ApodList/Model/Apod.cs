using System.Text.Json.Serialization;

namespace ApodList.ViewModel;

public class Apod
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("explanation")]
    public string explanation { get; set; }
    [JsonPropertyName("url")]
    public string ImageUrl { get; set; }
}

