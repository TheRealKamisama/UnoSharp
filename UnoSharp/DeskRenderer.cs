using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp
{
    public static class DeskRenderer
    {
        public static Image RenderBlankCards(int count)
        {
            if (count == 0) return new Bitmap(1, 1);
            if (count == 1) return Card.MainCardImage;
            
            var width = Card.MainCardImage.Width * count;
            var eachWidth = (int) (Card.MainCardImage.Width / 5.0 * 2.0);
            
        }

    }
}
