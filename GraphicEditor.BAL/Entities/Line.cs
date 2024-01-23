using GraphicEditor.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.BAL.Entities
{
    public class Line : GraphicObject
    {   
        private Coordinate _startPoint;
        private Coordinate _endPoint;
        //private string _type;

        #region "Constructors"
        public Line() { }
        public Line(Coordinate start, Coordinate end)
        {
            this.StartPoint = start;
            this.EndPoint = end;
        }

        #endregion

        #region "Properties"
        
        public Coordinate StartPoint
        {
            get =>  _startPoint; 
            set =>  _startPoint = value; 
        }

        public Coordinate EndPoint
        {
            get => _endPoint;
            set => _endPoint = value;
        }

        //public override string Type => GeoType.Line.ToString();
        #endregion
    }

}
