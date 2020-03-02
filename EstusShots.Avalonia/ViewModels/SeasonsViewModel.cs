using System.Collections.Generic;
using System.Reactive;
using EstusShots.Client;
using EstusShots.Shared.Dto;
using ReactiveUI;

namespace EstusShots.Avalonia.ViewModels
{
    public class SeasonsViewModel : ViewModelBase
    {
        private readonly EstusShotsClient _apiClient;
        private readonly MainWindowViewModel _main;

        public List<Season> Seasons { get; set; }

        public ReactiveCommand<Unit, Unit> ToPlayers { get; }
        
        public SeasonsViewModel(EstusShotsClient apiClient, MainWindowViewModel main)
        {
            _apiClient = apiClient;
            _main = main;

            ToPlayers = ReactiveCommand.Create(() => { Global.Navigator.GoTo<PlayersViewModel>(); });
        }
    }
}