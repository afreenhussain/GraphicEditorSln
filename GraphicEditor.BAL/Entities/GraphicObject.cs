using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.BAL.Entities
{
    public abstract class GraphicObject
    {
       
        private System.Drawing.Color _color;
        private bool? _filledShape = false;
        private Int32 _thickness;
        //private readonly string _type = string.Empty;
               
        public virtual System.Drawing.Color Color
        {
            get => _color;
            set => _color = value;
        }
        public virtual bool? FilledShape
        {
            get => _filledShape; 
            set => _filledShape = value; 
        }
        public virtual Int32 Thickness
        {
            get => _thickness;
            set => _thickness = value;
        }

        //public virtual string Type
        //{
        //    get => _type;
        //    //set => _type = value;
        //}

    }
}
