using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Kirabo.Controls
{
    public class CustomPanel :Panel
    {
public CustomPanel() : base()
        {

        }

        //First measure all children and return available size of panel
        protected override Size MeasureOverride(Size availableSize)
        {
            Size availableSpace = new Size(double.PositiveInfinity, double.PositiveInfinity);
            Size desiredSize = new Size(0, 0);

            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSpace);
                desiredSize.Width = Math.Max(desiredSize.Height, child.DesiredSize.Width);
                desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
            }
            return desiredSize;
        }
        //Second arrange all children and return final size of panel
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size returnSize = finalSize;
            //The location where the child will be displayed            
            foreach (FrameworkElement child in Children)
            {
                Point location = new Point(0, 0);                
                child.Arrange(new Rect(location, child.DesiredSize));

                returnSize.Width = Math.Max(returnSize.Height, child.DesiredSize.Width);
                returnSize.Height = Math.Max(returnSize.Height, child.DesiredSize.Height);
            }
            //Return the total size used            
            return returnSize;// new Size(216, 156);
        }
    
    }
}
