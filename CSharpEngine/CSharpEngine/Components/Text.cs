using System.Runtime.InteropServices;

namespace CSharpEngine.Components
{
    public class Color : Component
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }
        
        public Color(byte r, byte g, byte b, byte a = 0)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
    
    public class Text : Component
    {
        public string Font { get; set; }
        public string Txt { get; set; }
        public Color Color { get; set; }
        public int Size { get; set; }
        
        public Text(string font, string text, Color color, int size)
        {
            Font = font;
            Color = color;
            Size = size;
            Txt = text;
        }
    }
}