using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Whiteboard.View
{
    /// <summary>
    /// Interaction logic for WhiteboardView.xaml
    /// </summary>
    public partial class WhiteboardView : UserControl
    {
        private enum DrawingTool { None, Rectangle, Ellipse, Line}
        private DrawingTool currentTool = DrawingTool.None;
        
        private Point startPoint;
        
        private Shape drawingShape;
        private Polyline freeLine;

        private bool isWriting = false;
        private bool isErasing = false;
        
        private Brush currentColor = Brushes.Black; 
        private Brush pen;
        private Brush eraser = Brushes.White;
        private double penThickness = 3;

        public WhiteboardView()
        {
            InitializeComponent();
        }

        private void SetDrawingTool(DrawingTool tool)
        {
            currentTool = tool;
        }

        private void Pic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (currentTool != DrawingTool.None)
            {
                startPoint = e.GetPosition(Pic);

                switch (currentTool)
                {
                    case DrawingTool.Rectangle:
                        drawingShape = new Rectangle
                        {
                            Stroke = currentColor,
                            StrokeThickness = penThickness,
                            Fill = Brushes.Transparent
                        };

                        Canvas.SetLeft(drawingShape, startPoint.X);
                        Canvas.SetTop(drawingShape, startPoint.Y);

                        break;

                    case DrawingTool.Ellipse:
                        drawingShape = new Ellipse
                        {
                            Stroke = currentColor,
                            StrokeThickness = penThickness,
                            Fill = Brushes.Transparent
                        };

                        Canvas.SetLeft(drawingShape, startPoint.X);
                        Canvas.SetTop(drawingShape, startPoint.Y);

                        break;

                    case DrawingTool.Line:
                        drawingShape = new Line
                        {
                            X1 = startPoint.X,
                            Y1 = startPoint.Y,
                            Stroke = currentColor,
                            StrokeThickness = penThickness
                        };

                        break;
                }

                Pic.Children.Add(drawingShape);
            }
            else if (e.ButtonState == MouseButtonState.Pressed && isWriting == true)
            {
                startPoint = e.GetPosition(Pic);
            }
            else if (e.ButtonState == MouseButtonState.Pressed && isErasing == true)
            {
                startPoint = e.GetPosition(Pic);
            }

                
        }

        private void Pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentTool != DrawingTool.None && drawingShape != null && e.LeftButton == MouseButtonState.Pressed)
            {
                if (currentTool == DrawingTool.Line)
                {
                    Line line = drawingShape as Line;
                    if (line != null)
                    {
                        line.X2 = e.GetPosition(Pic).X;
                        line.Y2 = e.GetPosition(Pic).Y;
                    }
                }
                else
                {
                    double width = e.GetPosition(Pic).X - startPoint.X;
                    double height = e.GetPosition(Pic).Y - startPoint.Y;

                    if (width > 0 && height > 0)
                    {
                        drawingShape.Width = width;
                        drawingShape.Height = height;
                    }
                }
            }
            else if (e.LeftButton == MouseButtonState.Pressed && isWriting == true)
            {
                Line line = new Line();

                line.Stroke = pen;
                line.X1 = startPoint.X;
                line.Y1 = startPoint.Y;
                line.X2 = e.GetPosition(Pic).X;
                line.Y2 = e.GetPosition(Pic).Y;
                line.StrokeThickness = penThickness;

                startPoint = e.GetPosition(Pic);

                Pic.Children.Add(line);
            }
            else if (e.LeftButton == MouseButtonState.Pressed && isErasing == true)
            {
                Line line = new Line();

                line.Stroke = eraser;
                line.X1 = startPoint.X;
                line.Y1 = startPoint.Y;
                line.X2 = e.GetPosition(Pic).X;
                line.Y2 = e.GetPosition(Pic).Y;
                line.StrokeThickness = penThickness;

                startPoint = e.GetPosition(Pic);

                Pic.Children.Add(line);
            }
        }

        private void Pic_MouseUp(object sender, MouseButtonEventArgs e)
        {
            drawingShape = null;
        }

        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            SetDrawingTool(DrawingTool.Ellipse);
            
            isWriting = false;
            isErasing = false;
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            SetDrawingTool(DrawingTool.Rectangle);
            
            isWriting = false;
            isErasing = false;
        }

        private void LineBtn_Click(object sender, RoutedEventArgs e)
        {
            SetDrawingTool(DrawingTool.Line);
            
            isWriting = false;
            isErasing = false;
        }

        private void EraserBtn_Click(object sender, RoutedEventArgs e)
        {
            SetDrawingTool(DrawingTool.None);
            isWriting = false;
            isErasing = true;
        }

        private void PencilBtn_Click(object sender, RoutedEventArgs e)
        {
            SetDrawingTool(DrawingTool.None);
            isWriting = true;
            isErasing = false;
        }

        private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorName = ((ComboBoxItem)ColorPicker.SelectedItem).Content.ToString();
            
            switch (colorName)
            {
                case "Red":
                    ColorBtn.Background = new SolidColorBrush(Colors.Red);
                    currentColor = Brushes.Red;
                    pen = Brushes.Red;
                    break;
                case "Green":
                    ColorBtn.Background = new SolidColorBrush(Colors.Green);
                    currentColor = Brushes.Green;
                    pen = Brushes.Green;
                    break;
                case "Blue":
                    ColorBtn.Background = new SolidColorBrush(Colors.Blue);
                    currentColor = Brushes.Blue;
                    pen = Brushes.Blue;
                    break;
                case "Black":
                    ColorBtn.Background = new SolidColorBrush(Colors.Black);
                    currentColor = Brushes.Black;
                    pen = Brushes.Black;
                    break;
                case "Gray":
                    ColorBtn.Background = new SolidColorBrush(Colors.Gray);
                    currentColor = Brushes.Gray;
                    pen = Brushes.Gray;
                    break;
                case "Yellow":
                    ColorBtn.Background = new SolidColorBrush(Colors.Yellow);
                    currentColor = Brushes.Yellow;
                    pen = Brushes.Yellow;
                    break;
                case "Orange":
                    ColorBtn.Background = new SolidColorBrush(Colors.Orange);
                    currentColor = Brushes.Orange;
                    pen = Brushes.Orange;
                    break;
                case "Purple":
                    ColorBtn.Background = new SolidColorBrush(Colors.Purple);
                    currentColor = Brushes.Purple;
                    pen = Brushes.Purple;
                    break;
            }
        }

        private void ThicknessPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            penThickness = Convert.ToDouble(((ComboBoxItem)ThicknessPicker.SelectedItem).Content.ToString());
        }
    }
}
