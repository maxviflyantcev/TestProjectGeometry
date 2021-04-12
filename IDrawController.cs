using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace TestProjectGeometry
{
    public interface IDrawController
    {        
        Shape Draw(double size_a, double size_b);
    }
}
