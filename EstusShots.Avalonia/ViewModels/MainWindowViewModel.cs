using System;
using ReactiveUI;

namespace EstusShots.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IServiceProvider Container;
        
        private ViewModelBase _content;

        public MainWindowViewModel(IServiceProvider container)
        {
            Container = container;
        }

        public ViewModelBase Content    
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        
    }
}
