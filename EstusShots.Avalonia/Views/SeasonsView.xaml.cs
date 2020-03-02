using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EstusShots.Avalonia.Views
{
    public class SeasonsView : UserControl
    {
        public SeasonsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}