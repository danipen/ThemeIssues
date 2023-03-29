using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;

namespace ThemeIssues
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.F1)
            {
                this.Styles.Resources["TextColor"] = Color.FromRgb(255, 0, 0);
            }

            if (e.Key == Key.F2)
            {
                this.Styles.Resources["TextColor"] = Color.FromRgb(0, 255, 0);
            }

            if (e.Key == Key.F3)
            {
                this.Styles.Resources["TextColor"] = Color.FromRgb(0, 0, 255);
            }
        }

        ButtonGroup mButtonGroup;
    }
}