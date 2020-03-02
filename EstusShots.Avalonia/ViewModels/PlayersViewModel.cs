using EstusShots.Client;

namespace EstusShots.Avalonia.ViewModels
{
    public class PlayersViewModel : ViewModelBase
    {
        private readonly EstusShotsClient _apiClient;

        public PlayersViewModel(EstusShotsClient apiClient)
        {
            _apiClient = apiClient;
        }
    }
}