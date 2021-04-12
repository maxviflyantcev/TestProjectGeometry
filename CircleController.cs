using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TestProjectGeometry
{
    public class CircleController :  IDrawController
    {
        public Shape Draw(double size_a, double size_b)
        {
            Ellipse _ellCircle = new Ellipse()
            {
                Height = size_a,
                Width = size_a,
                Fill = Brushes.Red,
                StrokeThickness = 2
            };
            return _ellCircle;
        }
    }
}
