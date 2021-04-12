using Autofac;
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

namespace TestProjectGeometry
{
    public partial class MainWindow : Window
    {
        //объявление интерфейсов для Autofac
        private IContainer Container;
        private IDrawController Controller;

        public MainWindow()
        {
            //регистрация реализаций
            var builder = new ContainerBuilder();
            builder.RegisterType<RectController>();
            builder.RegisterType<EllipseController>();
            builder.RegisterType<CircleController>();
            builder.RegisterType<RectController>().As<IDrawController>().PropertiesAutowired();
            builder.RegisterType<EllipseController>().As<IDrawController>().PropertiesAutowired();
            builder.RegisterType<CircleController>().As<IDrawController>().PropertiesAutowired();
            Container = builder.Build();

            InitializeComponent();

            stP_Shapes.Visibility = Visibility.Hidden;
            lbl_RadA_SideA.Visibility = Visibility.Hidden;
            lbl_RadB_SideB.Visibility = Visibility.Hidden;
            txB_NumbA.Visibility = Visibility.Hidden;
            txB_NumbB.Visibility = Visibility.Hidden;
            lbl_Xcoords_lbl.Visibility = Visibility.Hidden;
            lbl_Xcoords.Visibility = Visibility.Hidden;
            lbl_Ycoords_lbl.Visibility = Visibility.Hidden;
            lbl_Ycoords.Visibility = Visibility.Hidden;
        }

        private void chB_Selection_Checked(object sender, RoutedEventArgs e)
        {
            stP_Shapes.Visibility = Visibility.Hidden;
            chB_Drawing.IsChecked = false;
            chB_drawRect.IsEnabled = false;
            chB_drawEllipse.IsEnabled = false;
            chB_drawCircle.IsEnabled = false;

            lbl_RadA_SideA.Visibility = Visibility.Hidden;
            lbl_RadB_SideB.Visibility = Visibility.Hidden;
            txB_NumbA.Visibility = Visibility.Hidden;
            txB_NumbB.Visibility = Visibility.Hidden;
        }

        private void chB_drawRect_Checked(object sender, RoutedEventArgs e)
        {
            chB_drawEllipse.IsChecked = false;
            chB_drawCircle.IsChecked = false;
            lbl_RadA_SideA.Visibility = Visibility.Visible;
            lbl_RadB_SideB.Visibility = Visibility.Visible;
            txB_NumbA.Visibility = Visibility.Visible;
            txB_NumbB.Visibility = Visibility.Visible;
            lbl_RadA_SideA.Content = "A side: ";
            lbl_RadB_SideB.Content = "B side: ";
        }

        private void chB_drawCircle_Checked(object sender, RoutedEventArgs e)
        {
            chB_drawEllipse.IsChecked = false;
            chB_drawRect.IsChecked = false;
            lbl_RadB_SideB.Visibility = Visibility.Hidden;
            txB_NumbB.Visibility = Visibility.Hidden;
            lbl_RadA_SideA.Content = "Radius: ";
        }

        private void chB_drawEllipse_Checked(object sender, RoutedEventArgs e)
        {
            chB_drawRect.IsChecked = false;
            chB_drawCircle.IsChecked = false;
            lbl_RadA_SideA.Visibility = Visibility.Visible;
            lbl_RadB_SideB.Visibility = Visibility.Visible;
            txB_NumbA.Visibility = Visibility.Visible;
            txB_NumbB.Visibility = Visibility.Visible;
            lbl_RadA_SideA.Content = "A radius: ";
            lbl_RadB_SideB.Content = "B radius: ";
        }

        private void chB_Drawing_Checked(object sender, RoutedEventArgs e)
        {
            chB_Selection.IsChecked = false;
            chB_drawRect.IsEnabled = true;
            chB_drawEllipse.IsEnabled = true;
            chB_drawCircle.IsEnabled = true;
            stP_Shapes.Visibility = Visibility.Visible;
            lbl_RadA_SideA.Visibility = Visibility.Visible;
            lbl_RadB_SideB.Visibility = Visibility.Visible;
            txB_NumbA.Visibility = Visibility.Visible;
            txB_NumbB.Visibility = Visibility.Visible;
        }

        private void cnv_Main_MouseEnter(object sender, MouseEventArgs e)
        {
            if (chB_Drawing.IsChecked == true && chB_drawCircle.IsChecked == true || chB_drawEllipse.IsChecked == true || chB_drawRect.IsChecked == true)
            {
                lbl_Xcoords_lbl.Visibility = Visibility.Visible;
                lbl_Xcoords.Visibility = Visibility.Visible;
                lbl_Ycoords_lbl.Visibility = Visibility.Visible;
                lbl_Ycoords.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_Xcoords_lbl.Visibility = Visibility.Hidden;
                lbl_Xcoords.Visibility = Visibility.Hidden;
                lbl_Ycoords_lbl.Visibility = Visibility.Hidden;
                lbl_Ycoords.Visibility = Visibility.Hidden;
            }

        }

        private void cnv_Main_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_Xcoords_lbl.Visibility = Visibility.Hidden;
            lbl_Xcoords.Visibility = Visibility.Hidden;
            lbl_Ycoords_lbl.Visibility = Visibility.Hidden;
            lbl_Ycoords.Visibility = Visibility.Hidden;
        }

        private void cnv_Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //определение контроллера в зависимости от выбранной фигуры для рисования
            //функция DrawMethod вынесена отдельно чтобы избежать дублирования кода
            if (e.LeftButton == MouseButtonState.Pressed && chB_Drawing.IsChecked == true)
            {
                if (chB_drawCircle.IsChecked == true)
                {
                    Controller = Container.Resolve<CircleController>();
                    DrawMethod(Controller, e.GetPosition(cnv_Main).X, e.GetPosition(cnv_Main).Y);

                } 
                else if (chB_drawRect.IsChecked == true)
                {
                    Controller = Container.Resolve<RectController>();
                    DrawMethod(Controller, e.GetPosition(cnv_Main).X, e.GetPosition(cnv_Main).Y);

                } else if (chB_drawEllipse.IsChecked == true)
                {
                    Controller = Container.Resolve<EllipseController>();
                    DrawMethod(Controller, e.GetPosition(cnv_Main).X, e.GetPosition(cnv_Main).Y);
                }
            }
        }

        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (chB_Selection.IsChecked == true)
            {
                if (sender is Rectangle)
                {
                    Rectangle send = sender as Rectangle;
                    send.Stroke = Brushes.Green;
                } else if (sender is Ellipse)
                {
                    Ellipse send = sender as Ellipse;
                    send.Stroke = Brushes.Green;
                }                
            } 
        }

        private void cnv_Main_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_Xcoords.Content = e.GetPosition(cnv_Main).X;
            lbl_Ycoords.Content = e.GetPosition(cnv_Main).Y;
        }

        private void DrawMethod(IDrawController Controller, double X, double Y)
        {
            Shape _shape = Controller.Draw(double.Parse(txB_NumbA.Text), double.Parse(txB_NumbB.Text));
            cnv_Main.Children.Add(_shape);
            Canvas.SetTop(_shape, Y - _shape.Height / 2);
            Canvas.SetLeft(_shape, X - _shape.Width / 2);
            _shape.MouseDown += Shape_MouseDown;
        }

        //очищение области Canvas от всех элементов
        private void btn_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            cnv_Main.Children.Clear();
        }

        //удаление элементов, Stroke которых выделен определнным цветом (удаление "выделенных" элементов)
        private void btn_ClearSelected_Click(object sender, RoutedEventArgs e)
        {
            List<Shape> shapes = new List<Shape>();
            foreach (Shape shape in cnv_Main.Children)
            {
                if (shape.Stroke == Brushes.Green)
                {
                    shapes.Add(shape);
                }
            }
            foreach(var item in shapes)
            {
                cnv_Main.Children.Remove(item);
            }
        }

        private void chB_Drawing_Unchecked(object sender, RoutedEventArgs e)
        {
            chB_drawRect.IsEnabled = false;
            chB_drawEllipse.IsEnabled = false;
            chB_drawCircle.IsEnabled = false;
            stP_Shapes.Visibility = Visibility.Hidden;
            lbl_RadA_SideA.Visibility = Visibility.Hidden;
            lbl_RadB_SideB.Visibility = Visibility.Hidden;
            txB_NumbA.Visibility = Visibility.Hidden;
            txB_NumbB.Visibility = Visibility.Hidden;
        }
    }
}
