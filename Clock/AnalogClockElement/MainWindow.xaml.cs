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
using System.Windows.Threading;

namespace AnalogClockElement
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool scaled = false;
        private static Action EmptyDelegate = null;

        public MainWindow()
        {
            InitializeComponent();
            EmptyDelegate = clock.start;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            double scale = ((Window)sender).Height / ((Window)sender).Width;

            // 400 400

            // 800 400 - 2
            // 800 400 * 2

            // 200 400 - 0.5
            // 200 400 * 0.5

            // 400 800 - 0.5
            // 400 / 0.5  800

            // 400 200 - 2
            // 400 / 2  200

            

            if (!scaled)
            {
                if (e.NewSize.Width != e.PreviousSize.Width && Math.Abs(e.NewSize.Width - e.PreviousSize.Width) >= SystemParameters.MinimumHorizontalDragDistance)
                {
                    ((Window)sender).Height = e.NewSize.Width;
                }

                if (e.NewSize.Height != e.PreviousSize.Height && Math.Abs(e.NewSize.Height - e.PreviousSize.Height) >= SystemParameters.MinimumVerticalDragDistance)
                {
                    ((Window)sender).Width = e.NewSize.Height;
                }
                e.Handled = true;
                scaled = true;
            }
            else {

                scaled = false;
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clock.mIsForvard = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clock.mIsForvard = false;
            clock.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
