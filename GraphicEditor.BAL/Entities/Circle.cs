using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicEditor.BAL;

namespace GraphicEditor.BAL.Entities
{
    public class Circle : GraphicObject
    {        
        private Coordinate _center;
        private double _radius;
        
        #region "Constructors"
        public Circle() { }
        public Circle(Coordinate center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        #endregion

        #region "Properties"
        public Coordinate Center
        {
            get  => _center;
            set => _center = value; 
        }

        public double Radius
        {
            get => _radius; 
            set =>_radius = value; 
        }

        public double Diameter
        {
            get => this.Radius * 2.0;
        }

       // public override string Type => GeoType.Circle.ToString();
        #endregion
    }
}
