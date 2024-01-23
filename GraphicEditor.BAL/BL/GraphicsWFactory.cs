using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.BAL
{
    /// <summary>
    /// A factory class that creates the concerte graphics world class 
    /// </summary>
    public class GraphicsWFactory
    {
        private Dictionary<GraphicsWType, Func<BaseGraphicsW>> _entityTypeMapper;

        // Create an object of concrete Parser implemetations
        public GraphicsWFactory()
        {
            _entityTypeMapper = new Dictionary<GraphicsWType, Func<BaseGraphicsW>>();
            _entityTypeMapper.Add(GraphicsWType.VectorGraphicsW, () => { return new VectorGraphicsW(); });
        }

        public BaseGraphicsW GetEntityBasedOnType(GraphicsWType graphicsWType)
        {
            return _entityTypeMapper[graphicsWType]();
        }
    }

}
