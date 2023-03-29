using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

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
    }
}