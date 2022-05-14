using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;

namespace FrontEnd
{
    class Flight
    {
        public List<double> X = new List<double>() { 0 };
        public List<double> Y = new List<double>() { 0 };
        public List<double> T = new List<double>() { 0 };
        public List<double> Vx = new List<double>();
        public List<double> Vy = new List<double>();
        public Flight(double V, double a)
        {
            a = a * Math.PI / 180;
            const double g = 9.806;
            const double m = 10;
            double k = Math.Sin(a);
            
            Vx.Add (V * Math.Cos(a) );
            Vy.Add (V * Math.Sin(a) );
            double dT = 0.05;
            int i = 0;
            while (Y[i] >= 0)
            {
                X.Add(X[i] + dT * Vx[i]);
                Y.Add(Y[i] + dT * Vy[i]);
                T.Add(T[i] + dT);
                Vx.Add(Vx[i] - dT * k * Vx[i] / m);
                Vy.Add(Vy[i] - dT * (g + k * Vy[i] / m));
                i++;
            }
        }
    }

    class Enter : Window
    {
        
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new Enter());
        }
        public Enter()
        {

            Title = "Полёт";

            ResizeMode = ResizeMode.CanMinimize;
            StackPanel stackMain = new StackPanel();
            stackMain.Orientation = Orientation.Horizontal;
            stackMain.Margin = new Thickness(5);
            Content = stackMain;

            StackPanel stackChild1 = new StackPanel();
            stackMain.Children.Add(stackChild1);

            Label lbl1 = new Label();
            lbl1.Content = "Введите скорость тела: ";
            lbl1.Margin = new Thickness(5);
            stackChild1.Children.Add(lbl1);
            Label lbl2 = new Label();
            lbl2.Content = "Введите угол: ";
            lbl2.Margin = new Thickness(5);
            stackChild1.Children.Add(lbl2);

            StackPanel stackChild2 = new StackPanel();
            stackMain.Children.Add(stackChild2);

            TextBox txtbox1 = new TextBox();
            txtbox1.Margin = new Thickness(5);
            txtbox1.Width = 64;
            stackChild2.Children.Add(txtbox1);
            TextBox txtbox2 = new TextBox();
            txtbox2.Margin = new Thickness(5);
            stackChild2.Children.Add(txtbox2);

            ScrollViewer sv = new ScrollViewer();
            stackMain.Children.Add(sv);

            Canvas canv = new Canvas();
            canv.Width = 200;
            canv.Height = 100;
            stackMain.Children.Add(canv);

            Button btn = new Button();
            btn.Name = "Рассчитать";
            btn.Content = btn.Name;
            btn.Margin = new Thickness(5);
            btn.Height = 20;
            btn.Click += ButtonOnClick;
            stackChild2.Children.Add(btn);

            void ButtonOnClick(object sender, RoutedEventArgs args)
            {
                Flight b = new Flight(Convert.ToDouble(txtbox1.Text), Convert.ToDouble(txtbox2.Text));

                string enter = "X\tY\tT\n";
                for (int n = 0; n < b.T.Count; n++)
                {
                    enter += b.X[n] + "\t" + b.Y[n] + "\t" + b.T[n] + "\n";
                }
                MessageBox.Show(b.X[b.X.Count - 1] + " " + b.Y[b.Y.Count - 1],"Тело упало");
                sv.Content = enter;

                Polyline poly = new Polyline();
                canv.Children.Add(poly);
                Point[] pts = new Point[b.T.Count]; 
                for (int i = 0; i < b.T.Count; i++) 
                {  
                    pts[i].X = b.X[i]; 
                    pts[i].Y = -b.Y[i]+100; 
                }
                poly.Points = new PointCollection(pts);
                poly.Stroke = Brushes.Black;
                poly.StrokeThickness = 4;
            }
        }
    }
}
