using GraphicEditor.BAL.Entities;
using GraphicEditor.BAL.Parser.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphicEditor.BAL
{   
    /// <summary>
    /// Base abstract class
    /// </summary>
    public abstract class BaseGraphicsW
    {
        private Color _defaultColor = Color.FromArgb(0, 0, 0);
        private Int32 _defaultThickness = 3;
        public abstract List<GraphicObject> MapAndCreate(UDShapeRoot shapeRoot);

        protected Color DefaultColor
        {
            get => _defaultColor;
        }
        protected int DefaultThickness
        {
            get => _defaultThickness;
        }
        protected Coordinate GetVectorObjFromStringCoord(string coordinates)
        {
            Int32[] cord = coordinates.Split(',').Select(t => Convert.ToInt32(t)).ToArray();
            return new Coordinate(cord[0], cord[1]);
        }

        protected Color GetColorFromString(string strColor)
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
