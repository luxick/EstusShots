using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EstusShots.Avalonia.Views
{
    public class PlayersView : UserControl
    {
        public PlayersView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}