using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using GraphicEditor.BAL.Parser.Models;
using NLog;

namespace GraphicEditor.BAL.Parser
{
    /// <summary>
    /// Class: Json Parser for parsing json file type.
    /// </summary>
    public class JsonParser : IFileParser
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Read and Parse input in Json form
        /// </summary>
        /// <param name="fileName"></param>
        public UDShapeRoot ReadAndParseFile(string fileName)
        {
            UDShapeRoot shapes = new UDShapeRoot();
            try
            {
                var serializer = new JsonSerializer();                
                using (var streamReader = new StreamReader(fileName))
                using (var textReader = new JsonTextReader(streamReader))
                {
                    shapes = serializer.Deserialize<UDShapeRoot>(textReader);
                }
                return shapes;
            }
            catch (Exception exception)
            {
                Logger.Log(LogLevel.Error, exception.Message);
                throw exception;
            }
            finally { shapes = null; }
        }

    }
}