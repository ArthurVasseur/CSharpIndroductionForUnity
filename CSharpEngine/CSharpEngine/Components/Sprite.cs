namespace CSharpEngine.Components
{
    public class Sprite : Component
    {
        public string Path { get; set; }

        public Sprite(string path)
        {
            Path = path;
        }
    }
}