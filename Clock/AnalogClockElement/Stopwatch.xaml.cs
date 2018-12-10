using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AnalogClockElement
{
    /// <summary>
    /// Логика взаимодействия для AnalogClock.xaml
    /// </summary>
    public partial class Stopwatch : UserControl
    {
        //public double parentHeight = 460;
        //public double parentWidth = 443;
        public bool mIsForvard = false;
        public double mAdditive = 0.0d;
        public int Seconds = 0;


        public System.Windows.Threading.DispatcherTimer dispatcherTimer =
            new System.Windows.Threading.DispatcherTimer();
        public Stopwatch()
        {
            InitializeComponent();

            //Console.WriteLine(this.DependencyObjectType);
            //Console.WriteLine(FindParent<Window>(this.DependencyObjectType));
            //canvasWindow.Parent.

            //Console.WriteLine(this.Parent.GetValue(Window.HeightProperty));


            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherTimer.Stop();
        }

        public void ResetTimer()
        {
            Seconds = 0;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            Thread.Sleep(2);
            dispatcherTimer.Stop();
        }

        public void StartTimer()
        {
            dispatcherTimer.Start();
        }

        public void StopTimer()
        {
            dispatcherTimer.Stop();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            double secAngle = Seconds * 6;
            //Console.WriteLine(secAngle);
            ((RotateTransform)((TransformGroup)secondArrow.RenderTransform)
                .Children[1]).Angle = secAngle;
            Seconds++;
            if (Seconds == 60)
            {
                Seconds = 0;
            }
        }

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = LogicalTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }

        private void canvasWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Console.WriteLine(canvasWindow.ActualHeight);
            secondArrow.X2 = canvasWindow.ActualWidth / 2;
            secondArrow.Y2 = canvasWindow.ActualHeight / 2;
            double scale =
                System.Math.Min(canvasWindow.ActualWidth, canvasWindow.ActualHeight) / 400.0d;
            secondArrow.Y1 = secondArrow.Y1 * scale;
            secondArrow.X1 = secondArrow.X1 * scale;
            ((RotateTransform)((TransformGroup)secondArrow.RenderTransform)
                .Children[1]).CenterX = canvasWindow.ActualWidth / 2;
            ((RotateTransform)((TransformGroup)secondArrow.RenderTransform)
                .Children[1]).CenterY = canvasWindow.ActualHeight / 2;

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);
            //if (!mIsForvard)
            //{
            //    dispatcherTimer.Start();
            //}
        }

    }
}
