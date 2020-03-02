using EstusShots.Avalonia.ViewModels;

namespace EstusShots.Avalonia
{
    public static class Global
    {
        public static Navigator Navigator;
    }

    public class Navigator
    {
        private readonly MainWindowViewModel _model;

        public Navigator(MainWindowViewModel model)
        {
            _model = model;
        }

        public void GoTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var o = _model.Container.GetService(typeof(TViewModel));
            if (!(o is ViewModelBase viewModel)) return;
            _model.Content = viewModel;
        }
    }
}