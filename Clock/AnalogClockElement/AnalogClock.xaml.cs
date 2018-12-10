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
    public partial class AnalogClock : UserControl
    {
        //public double parentHeight = 460;
        //public double parentWidth = 443;
        public bool mIsForvard = false;
        public double mAdditive = 0.0d;

        System.Windows.Threading.DispatcherTimer dispatcherTimer =
            new System.Windows.Threading.DispatcherTimer();
        public AnalogClock()
        {
            InitializeComponent();

            //Console.WriteLine(this.DependencyObjectType);
            //Console.WriteLine(FindParent<Window>(this.DependencyObjectType));
            //canvasWindow.Parent.

            //Console.WriteLine(this.Parent.GetValue(Window.HeightProperty));
            
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (mIsForvard)
            {
                dispatcherTimer.Stop();
            }

            double hourAngle = (DateTime.Now.Hour + (DateTime.Now.Minute / 60d)) * 30;
            //Console.WriteLine(hourAngle);
            ((RotateTransform)((TransformGroup)hoursArrow.RenderTransform)
                .Children[1]).Angle = hourAngle;

            double minAngle = DateTime.Now.Minute * 6;
            //Console.WriteLine(minAngle);
            ((RotateTransform)((TransformGroup)minuteArrow.RenderTransform)
                .Children[1]).Angle = minAngle;

            int sec = DateTime.Now.Second;
            double secAngle = sec * 6;
            //Console.WriteLine(secAngle);
            ((RotateTransform)((TransformGroup)secondArrow.RenderTransform)
                .Children[1]).Angle = secAngle;

            //Thread.Sleep(1000);
            //Console.WriteLine(this.Parent.GetValue(Window.HeightProperty));
            //this.Height = (double)this.Parent.GetValue(Window.HeightProperty);
            //this.Width = (double)this.Parent.GetValue(Window.WidthProperty);
            //Console.WriteLine(canvasWindow.ActualHeight);
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

            minuteArrow.X2 = canvasWindow.ActualWidth / 2;
            minuteArrow.Y2 = canvasWindow.ActualHeight / 2;
            ((RotateTransform)((TransformGroup)minuteArrow.RenderTransform)
                .Children[1]).CenterX = canvasWindow.ActualWidth / 2;
            ((RotateTransform)((TransformGroup)minuteArrow.RenderTransform)
                .Children[1]).CenterY = canvasWindow.ActualHeight / 2;

            hoursArrow.X2 = canvasWindow.ActualWidth / 2;
            hoursArrow.Y2 = canvasWindow.ActualHeight / 2;
            ((RotateTransform)((TransformGroup)hoursArrow.RenderTransform)
                .Children[1]).CenterX = canvasWindow.ActualWidth / 2;
            ((RotateTransform)((TransformGroup)hoursArrow.RenderTransform)
                .Children[1]).CenterY = canvasWindow.ActualHeight / 2;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (!mIsForvard)
            {
                dispatcherTimer.Start();
            }
        }

        public void start() {

            if (!mIsForvard)
            {
                dispatcherTimer.Start();
            }
        }
    }
}
