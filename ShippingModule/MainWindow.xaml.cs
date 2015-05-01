using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShippingModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle curRect;
        double rectStartX;
        double rectStartY;
        bool isDragging;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvas1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            curRect = new Rectangle();
            curRect.Stroke = new SolidColorBrush(Color.FromRgb((byte)0, (byte)191, (byte)255));
            curRect.Fill = new SolidColorBrush(Color.FromArgb((byte)100, (byte)0, (byte)191, (byte)255));
            rectStartX = e.GetPosition(canvas2).X;
            rectStartY = e.GetPosition(canvas2).Y;

            Canvas.SetLeft(curRect, rectStartX);
            Canvas.SetTop(curRect, rectStartY);

            canvas2.Children.Add(curRect);
            canvas2.CaptureMouse();
            isDragging = true;
        }

        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point mousePos = e.GetPosition(canvas2);

                if (rectStartX < mousePos.X)
                {
                    Canvas.SetLeft(curRect, rectStartX);
                    curRect.Width = mousePos.X - rectStartX;
                }
                else
                {
                    Canvas.SetLeft(curRect, mousePos.X);
                    curRect.Width = rectStartX - mousePos.X;
                }

                if (rectStartY < mousePos.Y)
                {
                    Canvas.SetTop(curRect, rectStartY);
                    curRect.Height = mousePos.Y - rectStartY;
                }
                else
                {
                    Canvas.SetTop(curRect, mousePos.Y);
                    curRect.Height = rectStartY - mousePos.Y;
                }
            }
        }

        private void canvas1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                if (curRect != null)
                {
                    canvas2.Children.Remove(curRect);
                    canvas1.Children.Add(curRect);
                }

                curRect = null;
                canvas2.ReleaseMouseCapture();
                isDragging = false;
            }
        }
    }
}
