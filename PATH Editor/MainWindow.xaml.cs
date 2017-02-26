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
using System.IO;
using Microsoft.Win32;

namespace PATH_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<string> pathValues = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            LoadPathes();
        }

        private void SaveEnviromentVariable()
        {
            try
            {
                string list = string.Join(";", listBox.Items.Cast<string>());
                Environment.SetEnvironmentVariable("path", list, EnvironmentVariableTarget.Machine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void LoadPathes()
        {
            var pathValues = Environment.GetEnvironmentVariable("path").Split(';').Where(i => i != "").ToList();
            for (int i = 0; i < pathValues.Count; i++)
                listBox.Items.Add(pathValues[i]);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                listBox.Items.Add(folderBrowserDialog.SelectedPath);
            }
            SaveEnviromentVariable();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Remove(listBox.SelectedItem);
            SaveEnviromentVariable();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void maxButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Minimized)
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Application.Current.MainWindow.Width = Application.Current.MainWindow.Width + e.HorizontalChange < 220f ? Application.Current.MainWindow.Width : Application.Current.MainWindow.Width + e.HorizontalChange;
            Application.Current.MainWindow.Height = Application.Current.MainWindow.Height + e.VerticalChange < 120f ? Application.Current.MainWindow.Height : Application.Current.MainWindow.Height + e.VerticalChange;
        }

        
        private void ListBoxItemTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var textBox = (sender as TextBox);
            textBox.Focusable = true;
        }

        private void ListBoxItemTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Focusable = false;
        }

        private void ListBoxItemTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SaveEnviromentVariable();
            }
        }
    }
}
