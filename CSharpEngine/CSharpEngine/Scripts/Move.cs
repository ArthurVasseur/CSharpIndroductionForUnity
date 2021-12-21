using CSharpEngine.Components;

namespace CSharpEngine.Scripts;

public class Move : Component
{
    public override void OnConstruct()
    {
        base.OnConstruct(); // call the parent method
        Input.RegisterAction("Left", KeyCode.Q);
        Input.RegisterAction("Right", KeyCode.D);
        Input.RegisterAction("Up", KeyCode.Z);
        Input.RegisterAction("Down", KeyCode.S);
        Input.RegisterAction("Fire", KeyCode.Space);
    }

    public override void Update(float deltaTime)
    {
        if(Input.GetKeyDown("Left"))
            Console.WriteLine("Left action pressed");
    }

    public override void OnCollisionEnter(GameObject go)
    {
        Console.WriteLine("collision");
    }
}