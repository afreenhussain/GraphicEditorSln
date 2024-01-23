using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphicEditor.BAL.Parser;
using GraphicEditor.BAL.Entities;
using GraphicEditor.BAL;
using NLog;

namespace GraphicEditor
{
    public partial class GraphicEditorForm : Form
    {
        #region "Private Members"
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private List<GraphicObject> lstGraphicObjects = new List<GraphicObject>();
        private BAL.Parser.Models.UDShapeRoot _udShapeRoot = null;
        #endregion

        public GraphicEditorForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Hide();           

        }    

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {               
                Logger.Info("Processing Input File");

                /// Reading input file
                string filepath = txtBoxFilePath.Text;
                if (String.IsNullOrEmpty(filepath))
                {
                    MessageBox.Show("Please select file...", "", MessageBoxButtons.OK);
                }
                else
                {
                    string fileExtension = System.IO.Path.GetExtension(filepath).Replace('.', ' ').Trim();
                    FileType fileType = (FileType)Enum.Parse(typeof(FileType), fileExtension, true);

                    /// Calling the Parser based on file type
                    var parserFactory = new ParserFactory();
                    var parser = parserFactory.GetEntityBasedOnType(fileType);
                    if (parser != null)
                        _udShapeRoot = parser.ReadAndParseFile(filepath);

                    ///Mapping User defined ShapeRoot to Graphic Objects
                    if (_udShapeRoot != null)
                    {
                       Logger.Info("Initate: Mapping user inputs to Graphic Objects");
                       var graphicsWFactory = new GraphicsWFactory();
                        var graphicsW = graphicsWFactory.GetEntityBasedOnType(GraphicsWType.VectorGraphicsW);
                        lstGraphicObjects = graphicsW.MapAndCreate(_udShapeRoot);
                    }

                    this.Invalidate(true);
                    //this.Update();
                    RenderGraphicObjects();
                }
            } 
            catch(Exception ex)
            {
                Logger.Error("Error Occurred: ", ex.Message);
                MessageBox.Show("Error Ocurred !!! Please check logs","Alert",MessageBoxButtons.OK);
            } finally
            {
               _udShapeRoot = null; lstGraphicObjects = null;
            }            
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "json files (*.json)|*.json",
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                txtBoxFilePath.Text = openFileDialog.FileName;
        }

        #region "Private methods"
        private void RenderGraphicObjects()
        {
            try
            {
                Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb); //1000, 1000, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics graphics = Graphics.FromImage(bitmap);

                // Graphics graphics = this.CreateGraphics();
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PageUnit = GraphicsUnit.Pixel;

                //Matrix matrix = new Matrix(1, 0, 0, -1, 0, 0);
                //matrix.Scale(2, 1);
                //graphics.Transform = matrix;
                //graphics.TranslateTransform(50, 50, MatrixOrder.Append);

                // Default coordinate system : x-axis on right and y-axis on down
                // Changing axis
                graphics.ScaleTransform(1.0f, -1.0f);
                graphics.TranslateTransform(0, -this.ClientRectangle.Height); //move origin to bottom-left corner
                graphics.TranslateTransform(120, 150); //move origin to right 100 and up 100 pixel
                
                // Rendering graphic Objects on the form                
                foreach (GraphicObject graphicObj in lstGraphicObjects)
                {
                    Logger.Info("Rendering graphic object: {0}",graphicObj.GetType());

                    if (graphicObj is Line oLine)
                    {
                        graphics.DrawLine(new Pen(oLine.Color, oLine.Thickness), oLine.StartPoint.ToPointF, oLine.EndPoint.ToPointF);
                    }
                    else if (graphicObj is Triangle oTriangle)
                    {
                        if ((bool)oTriangle.FilledShape)
                            graphics.FillPolygon(new SolidBrush(oTriangle.Color), oTriangle.Vertices.Select(t => t.ToPointF).ToArray<PointF>());
                        else
                            graphics.DrawPolygon(new Pen(oTriangle.Color, oTriangle.Thickness), oTriangle.Vertices.Select(t => t.ToPointF).ToArray());
                    }
                    else if (graphicObj is Circle oCircle)
                    {
                        if ((bool)oCircle.FilledShape)                        
                            graphics.FillEllipse(new SolidBrush(oCircle.Color), (float)(oCircle.Center.X - oCircle.Radius), (float)(oCircle.Center.Y - oCircle.Radius), (float)oCircle.Diameter, (float)oCircle.Diameter);
                        else
                            graphics.DrawEllipse(new Pen(oCircle.Color, oCircle.Thickness), (float)(oCircle.Center.X - oCircle.Radius), (float)(oCircle.Center.Y - oCircle.Radius), (float)oCircle.Diameter, (float)oCircle.Diameter);  //(centerX - radius), (centerY - radius), circle, diameter);

                    }

                }

                // Rendering            
                pictureBox1.Image = bitmap;
                pictureBox1.Show();

                //Clean-up
                graphics.ResetTransform();
                txtBoxFilePath.Clear();
            }
            catch(Exception exception)
            {
                throw exception;
            }

        }

        #endregion
    }

}