using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor.BAL.Parser
{
    /// <summary>
    /// A factory class that creates the parser class 
    /// </summary>
    public class ParserFactory
    {
        private Dictionary<FileType, Func<IFileParser>> _entityTypeMapper;

        // Create an object of concrete Parser implemetations
        public ParserFactory()
        {
            _entityTypeMapper = new Dictionary<FileType, Func<IFileParser>> ();
            _entityTypeMapper.Add(FileType.Json, () => { return new JsonParser(); });            
        }

        public IFileParser GetEntityBasedOnType(FileType fileType)
        {
            return _entityTypeMapper[fileType]();
        }
    }
}
