using System;
using System.Collections.Generic;
using GraphicEditor.BAL.Parser.Models;

namespace GraphicEditor.BAL.Parser
{
    /// <summary>
    /// Interface to act as base class for any kind of parsers e.g. json, xml
    /// </summary>
    public interface IFileParser
    {
        UDShapeRoot ReadAndParseFile(string fileName);
        
    }
}