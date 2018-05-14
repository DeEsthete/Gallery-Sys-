using System;
using System.Collections.Generic;
using System.IO;
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

/*
 * Добавить сразу объекты Image с анимацией загрузки
 * Потом уже загружать картину
 */

namespace ImageObserver
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] files;
        private int currentIndex;
        //double[] points = new double[10000];
        //int i = 0;

        public MainWindow()
        {
            InitializeComponent();

            currentIndex = 0;
            files = Directory.GetFiles(@"C:\Users\ПавловЕ\Desktop\images", "*.jpg", SearchOption.AllDirectories);

            Parallel.Invoke(Loading);
        }

        private void Loading()
        {
            int loadIndex = currentIndex + 2;
            for (int i = currentIndex; i < files.Length && i < loadIndex; i++)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(files[i]));
                imageListBox.Items.Add(image);
            }
        }

        private void ImageScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (imageScrollViewer.ScrollableHeight == e.VerticalOffset)
            {
                currentIndex = imageListBox.Items.Count - 1;
                Parallel.Invoke(Loading);
            }
        }

        private void ImageListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (imageListBox.SelectedIndex == imageListBox.Items.Count - 1)
            {
                currentIndex = imageListBox.SelectedIndex;
                Parallel.Invoke(Loading);
            }
        }
    }
}
