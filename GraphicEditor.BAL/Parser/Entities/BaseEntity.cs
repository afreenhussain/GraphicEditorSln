using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.BAL.Parser.Models
{
    /// <summary>
    /// A model for accepting user defined inputs.
    /// </summary>
    public class UDShapeRoot
    {
        public List<UDShape> Shapes { get; set; }
    }
    public class UDShape
    {
        public  string Type { get; set; }
        public  bool? Filled { get; set; }
        public  string Color { get; set; }
        public  List<string> Vertices { get; set; }
        public  string Center { get; set; }
        public  double? Radius { get; set; }
        public  List<string> Lines { get; set; }

    }

}