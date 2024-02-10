using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerialKillerPredictionUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _modelPath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            _modelPath = $"{Environment.CurrentDirectory}\\TestModel";
            ModelPath.Content = _modelPath;
        }

        private void GetResult_Click(object sender, RoutedEventArgs e)
        {
            double[] input =
            {
                X1.SelectedIndex,
                double.Parse(X2.Text),
                X3.SelectedIndex,
                X4.SelectedIndex,
                X5.SelectedIndex,
                X6.SelectedIndex,
                X7.SelectedIndex,
                X8.SelectedIndex,
                X9.SelectedIndex,
                double.Parse(X10.Text),
                X11.SelectedIndex
            };
            
            for (int i  = 0; i < input.Length; i++)
            {
                input[i] += 1;
            }

            Predictor predictor = new(_modelPath);
            double result = predictor.Predict(input);
            Result.Content = result;
            if (result > 0.5)
            {
                TextResult.Content = "Является серийным убийцей";
                TextResult.Foreground = Brushes.Red;
            }
            else
            {
                TextResult.Content = "Не является серийным убийцей";
                TextResult.Foreground = Brushes.Green;
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Time to select a folder",
                UseDescriptionForTitle = true,
                SelectedPath = Environment.CurrentDirectory,
                ShowNewFolderButton = true
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _modelPath = dialog.SelectedPath;
                ModelPath.Content = _modelPath;
            }
        }
    }
}
