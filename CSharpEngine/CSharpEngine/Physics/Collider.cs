using CSharpEngine.Components;

namespace CSharpEngine.Physics;

public class Collider : Component
{
    public Vector2D Bounds { get; }
    public bool IsTrigger = false;
    private bool _lasFrameColliding = false;
    
    public Collider(Vector2D bounds)
    {
        Bounds = bounds;
    }

    public bool CheckCollisionEnter(Collider other)
    {
        if (Transform == null || other.Transform == null )
            return false;
        
        float leftEdge = Transform.Position.X;
        float rightEdge = leftEdge + Bounds.X;
        float topEdge = Transform.Position.Y;
        float bottomEdge = topEdge + Bounds.Y;

        float otherLeftEdge = other.Transform.Position.X;
        float otherRightEdge = otherLeftEdge + other.Bounds.X;
        float otherTopEdge = other.Transform.Position.Y;
        float otherBottomEdge = otherTopEdge + other.Transform.Position.Y;
    
        bool isColliding = (rightEdge > otherLeftEdge && leftEdge < otherRightEdge &&
                topEdge < otherBottomEdge && bottomEdge > otherTopEdge);
        if (_lasFrameColliding && isColliding)
            return false;
        if (isColliding)
        {
            bool res = !_lasFrameColliding && isColliding;
            _lasFrameColliding = true;
            return res;
        }
        _lasFrameColliding = false;
        return false;
    }
}