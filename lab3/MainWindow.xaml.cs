using System.Windows;
using System.Windows.Controls;

namespace Lab3
{
    public partial class MainWindow : Window
    {
        private IFigureFactory currentFactory;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorComboBox.SelectedItem is not ComboBoxItem selectedItem) return;

            DrawingPanel.Children.Clear();

            switch (selectedItem.Content.ToString())
            {
                case "Красный":
                    currentFactory = new RedFactory();
                    break;
                case "Синий":
                    currentFactory = new BlueFactory();
                    break;
                case "Зелёный":
                    currentFactory = new GreenFactory();
                    break;
                default:
                    return;
            }

            DrawingPanel.Children.Add(currentFactory.CreateCircle().CreateUIElement());
            DrawingPanel.Children.Add(currentFactory.CreateSquare().CreateUIElement());
            DrawingPanel.Children.Add(currentFactory.CreateTriangle().CreateUIElement());
        }
    }
}