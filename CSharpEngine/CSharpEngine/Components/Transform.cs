namespace CSharpEngine.Components
{
    public class Transform : Component
    {
        public Vector2D Position { get; set; }
        public float Rotation { get; set; }
        public Vector2D Scale { get; set; }
        
        public Transform(Vector2D position, float rotation, Vector2D scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}