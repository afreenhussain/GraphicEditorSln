using NUnit.Framework;
using GraphicEditor.BAL;
using GraphicEditor.BAL.Parser;
using System;
using System.IO;
using System.Linq;
using GraphicEditor.BAL.Parser.Models;
using System.Drawing;
using GraphicEditor.BAL.Entities;
using System.Collections.Generic;

namespace GraphicEditor.Tests
{
    /// <summary>
    /// NUnit Tests to test business logic classes.
    /// </summary>
    public class Tests
    {      

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestReadAndParseFile()
        {
           string sFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\test-circle.json"));
           //System.IO.Path.Combine(sCurrentDirectory, @"data\cat");
           
            JsonParser jsonParser = new JsonParser();
            UDShapeRoot udShapeRoot = jsonParser.ReadAndParseFile(sFilePath);

            Assert.IsNotNull(udShapeRoot);
            Assert.AreEqual("circle", udShapeRoot.Shapes.FirstOrDefault().Type);
            Assert.AreEqual("0,0", udShapeRoot.Shapes.FirstOrDefault().Center);

            //Assert.AreEqual("C:\\Users\\afree\\source\\repos\\GraphicEditor\\GraphicEditor.Tests\\bin\\Debug\\net472\\data\\test-circle.json", sFilePath);

        }

        [Test]
        public void TestCreateCircle()
        {
            UDShapeRoot udShapeRoot = LoadFile(@"data\test-circle.json");

            VectorGraphicsW graphicsW = new VectorGraphicsW();
            List<GraphicObject> lstGraphicObjects = graphicsW.MapAndCreate(udShapeRoot);
            Circle oActualCircle = (Circle)lstGraphicObjects.Select(t => t).FirstOrDefault();

            ///Excepted object
            Circle oExpectedCircle = new Circle()
            {
                Center = new Coordinate(0, 0),
                Radius = 55,
                FilledShape = false,
                Color = Color.FromArgb(127, 255, 0, 0), //127; 255; 0; 0"
                Thickness = 3
            };

            Assert.IsNotNull(oActualCircle);
            Assert.AreEqual(oExpectedCircle.Radius, oActualCircle.Radius);
            Assert.AreEqual(oExpectedCircle.Center.X, oActualCircle.Center.X);
            Assert.AreEqual(oExpectedCircle.Center.Y, oActualCircle.Center.Y);

        }

        [Test]
        public void TestCreateLine()
        {
            UDShapeRoot udShapeRoot = LoadFile(@"data\test-line.json");

            VectorGraphicsW graphicsW = new VectorGraphicsW();
            List<GraphicObject> lstGraphicObjects = graphicsW.MapAndCreate(udShapeRoot);
            Line oActualLine = (Line)lstGraphicObjects.Select(t => t).FirstOrDefault();

            ///Excepted object
            Line oExpectedLine = new Line()
            {
                StartPoint = new Coordinate(0, 0),
                EndPoint = new Coordinate(0, 0),
                Color = Color.FromArgb(127, 255, 255, 0), //127; 255; 0; 0"
                Thickness = 3
            };

            Assert.IsNotNull(oActualLine);
            Assert.AreEqual(oExpectedLine.StartPoint.X, oActualLine.StartPoint.X);
            Assert.AreEqual(oExpectedLine.EndPoint.Y, oActualLine.EndPoint.Y);
            Assert.AreEqual(oExpectedLine.Thickness, oActualLine.Thickness);

        }

        [Test]
        public void TestCreateTriangle() {

            UDShapeRoot udShapeRoot = LoadFile(@"data\test-triangle.json");

            VectorGraphicsW graphicsW = new VectorGraphicsW();
            List<GraphicObject> lstGraphicObjects = graphicsW.MapAndCreate(udShapeRoot);
            Triangle oActualTriangle = (Triangle)lstGraphicObjects.Select(t => t).FirstOrDefault();

            Assert.IsNotNull(oActualTriangle);
        }

        private UDShapeRoot LoadFile(string file)
        {
            string sFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,file));           

            JsonParser jsonParser = new JsonParser();
            return jsonParser.ReadAndParseFile(sFilePath);
        }

    }
}