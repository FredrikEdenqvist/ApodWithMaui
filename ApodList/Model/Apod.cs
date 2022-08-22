using System.Text.Json.Serialization;

namespace ApodList.ViewModel;

public class Apod
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("explanation")]
    public string Explanation { get; set; }
    [JsonPropertyName("url")]
    public string ImageUrl { get; set; }
    [JsonPropertyName("media_type")]
    public string MediaType { get; set; }
    [JsonIgnore]
    public bool IsImage => MediaType == "image";
}

