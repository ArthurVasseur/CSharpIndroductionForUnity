namespace CSharpEngine.Components
{
    public class Vector2D : Component
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D Zero() => new(0, 0);

        public static Vector2D Up() => new(0, 1);

        public static Vector2D Down() => new(0, -1);

        public static Vector2D Left() => new(-1, 0);

        public static Vector2D Right() => new(1, 0);

        public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2D operator *(Vector2D a, Vector2D b) => new(a.X * b.X, a.Y * b.Y);
        public static Vector2D operator /(Vector2D a, Vector2D b) => new(a.X / b.X, a.Y / b.Y);
    }
}