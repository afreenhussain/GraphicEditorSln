using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using GraphicEditor.BAL.Entities;
using GraphicEditor.BAL.Parser.Models;
using NLog;

namespace GraphicEditor.BAL
{
    public class Factory
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private List<GraphicObject> lstVectorShapes = new List<GraphicObject>();
        private ShapeRoot _shapeRoot = new ShapeRoot();
        private Color _defaultColor = Color.FromArgb(0, 0, 0);

        public List<GraphicObject> Create(ShapeRoot shapeRoot)
        {

            this._shapeRoot = shapeRoot;
            GraphicObject vectorShape;
            try
            {
                Logger.Info("Begin " + this.GetType());
                foreach (var usrshape in _shapeRoot.Shapes)
                {
                    if (String.Equals(usrshape.Type, GeoType.Circle.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        lstVectorShapes.Add(Circle(usrshape));                     
                    }

                    else if (String.Equals(usrshape.Type, GeoType.Line.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        foreach (var line in usrshape.Lines)
                        {
                            string[] coord = line.Select(t => t.Split(';')).FirstOrDefault();
                            vectorShape = new Line()
                            {
                                Color = (String.IsNullOrEmpty(usrshape.Color)) ? _defaultColor : GetColorFromString(usrshape.Color),
                                StartPoint = GetVectorObjFromStringCoord(coord[0]),
                                EndPoint = GetVectorObjFromStringCoord(coord[1])
                            };

                            lstVectorShapes.Add(vectorShape);
                        }
                    }
                    else if ((String.Equals(usrshape.Type, GeoType.Triangle.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                    {
                        List<Vector> lstvertces = new List<Vector>();
                        foreach (var vertex in usrshape.Vertices)
                        {
                            foreach (var vert in vertex)
                                lstvertces.Add(GetVectorObjFromStringCoord(vert));
                        }

                        vectorShape = new Triangle()
                        {
                            Color = (String.IsNullOrEmpty(usrshape.Color)) ? _defaultColor : GetColorFromString(usrshape.Color),
                            FilledShape = usrshape.Filled,
                            Vertices = lstvertces
                        };
                        lstVectorShapes.Add(vectorShape);
                        lstvertces = null;
                    }

                }
            }
            catch(Exception exception)
            {
                Logger.Log(LogLevel.Error, exception.Message);
            }
            return lstVectorShapes;
        }

        private Circle Circle(Shape usrshape)
        {
            return new Circle()
            {
                // "0,0"
                Center = GetVectorObjFromStringCoord(usrshape.Center),
                Radius = (double)usrshape.Radius,
                FilledShape = usrshape.Filled,
                Color = (String.IsNullOrEmpty(usrshape.Color)) ? _defaultColor : GetColorFromString(usrshape.Color)
            };

        }

        private void Line(Shape usrshape)
        {
            lstVectorShapes.Add(new Line());

            //string[] coord = usrshape.Lines.Split(';');
            //return new Line()
            //{
            //    StartPoint = GetVectorObjFromStringCoord(usrshape.Lines[0]); //new Vector(0, 0)
            //    EndPoint = new Vector(100, 0),
            //    Color = (String.IsNullOrEmpty(usrshape.Color)) ? _defaultColor : GetColorFromString(usrshape.Color) //"127, 255, 0, 255"
            //};

        }

        private Vector GetVectorObjFromStringCoord(string coordinates)
        {
            Int32[] cord = coordinates.Split(',').Select(t => Convert.ToInt32(t)).ToArray();
            return new Vector(cord[0], cord[1]);

            //return coordinates.Split(',').Select( t => new Vector
            //{
            //    X= (double)t[0], 
            //    Y= (double)t[1]
            //} ).FirstOrDefault();

        }

        private Color GetColorFromString(string strColor)
        {
            int[] plattes = strColor.Split(';').Select(t => Convert.ToInt32(t)).ToArray();
            if (plattes.Length == 4)
            {
                return Color.FromArgb(plattes[0], plattes[1], plattes[2], plattes[3]);
            }
            else if (plattes.Length == 3)
                return Color.FromArgb(plattes[0], plattes[1], plattes[2]);
            else
                return _defaultColor;

        }

        
    }
}
