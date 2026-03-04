using System.Windows;
using System.Windows.Controls;

namespace Lab3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorComboBox.SelectedItem is not ComboBoxItem selectedItem) return;

            DrawingPanel.Children.Clear();

            CircleCreator circleCreator;
            SquareCreator squareCreator;
            TriangleCreator triangleCreator;

            switch (selectedItem.Content.ToString())
            {
                case "Красный":
                    circleCreator = new RedCircleCreator();
                    squareCreator = new RedSquareCreator();
                    triangleCreator = new RedTriangleCreator();
                    break;
                case "Синий":
                    circleCreator = new BlueCircleCreator();
                    squareCreator = new BlueSquareCreator();
                    triangleCreator = new BlueTriangleCreator();
                    break;
                case "Зелёный":
                    circleCreator = new GreenCircleCreator();
                    squareCreator = new GreenSquareCreator();
                    triangleCreator = new GreenTriangleCreator();
                    break;
                default:
                    return;
            }

            DrawingPanel.Children.Add(circleCreator.CreateCircle().CreateUIElement());
            DrawingPanel.Children.Add(squareCreator.CreateSquare().CreateUIElement());
            DrawingPanel.Children.Add(triangleCreator.CreateTriangle().CreateUIElement());
        }
    }
}