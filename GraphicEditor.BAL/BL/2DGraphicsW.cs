using GraphicEditor.BAL.Entities;
using GraphicEditor.BAL.Parser.Models;
using NLog;
using System;
using System.Collections.Generic;

namespace GraphicEditor.BAL
{
    /// <summary>
    /// A derived class which creates vector graphic objects 
    /// </summary>
    public class VectorGraphicsW : BaseGraphicsW
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private List<GraphicObject> _lstVectorShapes = new List<GraphicObject>();
               
        /// <summary>
        /// Maps the user defined inputs to the actual grpahic objects.
        /// </summary>
        /// <param name="udShapeRoot"></param>
        /// <returns></returns>
        public override List<GraphicObject> MapAndCreate(UDShapeRoot udShapeRoot)
        {            
            //GraphicObject vectorShape;

            try
            {
                Logger.Info("Begin mapping of " + this.GetType());
                foreach (var udShapeRo in udShapeRoot.Shapes)
                {
                    if (String.Equals(udShapeRo.Type, GeoType.Circle.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        _lstVectorShapes.Add(Circle(udShapeRo));
                    }
                    else if (String.Equals(udShapeRo.Type, GeoType.Line.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        foreach (var line in udShapeRo.Lines)
                        {
                            _lstVectorShapes.Add(Line(udShapeRo,line));                            
                        }
                    }
                    else if ((String.Equals(udShapeRo.Type, GeoType.Triangle.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                    {
                        _lstVectorShapes.Add(Triangle(udShapeRo));                        
                    }
                }
                return _lstVectorShapes;
            }
            catch (Exception exception)
            {
                Logger.Log(LogLevel.Error, exception.Message);
                throw exception;
            } finally
            {
                _lstVectorShapes = null;
            }

        }


        #region "Private Constructors"
        private Circle Circle(UDShape usrshape)
        {
            return new Circle()
            {
                // "0,0"
                Center = GetVectorObjFromStringCoord(usrshape.Center),
                Radius = (double)usrshape.Radius,
                FilledShape = usrshape.Filled,
                Color = (String.IsNullOrEmpty(usrshape.Color)) ? base.DefaultColor : GetColorFromString(usrshape.Color),
                Thickness = base.DefaultThickness
            };

        }

        private Line Line(UDShape udShapeRo, string line)
        {
            string[] coord = line.Trim().Split(';');
            return new Line()
            {
                Color = (String.IsNullOrEmpty(udShapeRo.Color)) ? base.DefaultColor : GetColorFromString(udShapeRo.Color),
                StartPoint = GetVectorObjFromStringCoord(coord[0]),
                EndPoint = GetVectorObjFromStringCoord(coord[1]),
                Thickness = base.DefaultThickness
            };         
        }

        private Triangle Triangle(UDShape udShapeRo)
        {
            List<Coordinate> lstvertices = new List<Coordinate>();
            foreach (var vertex in udShapeRo.Vertices)
                lstvertices.Add(GetVectorObjFromStringCoord(vertex));

            return new Triangle()
            {
                Color = (String.IsNullOrEmpty(udShapeRo.Color)) ? base.DefaultColor : GetColorFromString(udShapeRo.Color),
                FilledShape = udShapeRo.Filled,
                Vertices = lstvertices,
                Thickness = base.DefaultThickness
            };

        }

        #endregion


    }
}