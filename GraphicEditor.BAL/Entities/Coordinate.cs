using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.BAL
{
    public class Coordinate
    {
        public double _x;
        public double _y;
        public double _z;

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public System.Drawing.PointF ToPointF
        {
            get { return new System.Drawing.PointF((float)X, (float)Y); }
        }
        public Coordinate(double x, double y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0.0;
        }
        public Coordinate() {  }
    }
}
