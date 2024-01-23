using System;
using System.Collections.Generic;

namespace GraphicEditor.BAL.Entities
{
    public class Triangle : GraphicObject
    {        
        private List<Coordinate> _lstVertices;        

        #region "Constructors"
        public Triangle() { }
        public Triangle(List<Coordinate> vertices)
        {
            this._lstVertices = vertices;           
        }

        #endregion

        #region "Properties"     

        public List<Coordinate> Vertices
        {
            get => _lstVertices; 
            set  =>  _lstVertices = value; 
        }
        //public override string Type => GeoType.Line.ToString();
        #endregion
    }

}
