using CommunityToolkit.Mvvm.ComponentModel;

namespace ApodList.ViewModel;

[QueryProperty("Apod", "Apod")]
public partial class DetailsViewModel : ObservableObject
{
    [ObservableProperty]
    Apod apod;
}

