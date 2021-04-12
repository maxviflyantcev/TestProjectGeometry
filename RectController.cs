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
    public class RectController :  IDrawController 
    {
        public Shape Draw(double size_a, double size_b)
        {
            Rectangle _rect = new Rectangle()
            {
                StrokeThickness = 2,
                Fill = Brushes.Red,
                Height = size_b,
                Width = size_a
            };
            return _rect;
        }
    }
}
