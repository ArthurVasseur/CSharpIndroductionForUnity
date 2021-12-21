using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CSharpEngine.Components;
using CSharpEngine.Physics;
using CSharpEngine.Scripts;

namespace CSharpEngine
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Update();
        }
        private static void Update()
        {
            IRenderer renderer = new Renderer();
            Scene scene = new Scene();
            {
                GameObject gameObject = new GameObject();
                gameObject.AddComponent(new Transform(new Vector2D(100, 300), 0, new Vector2D(1, 1)));
                gameObject.AddComponent(new Sprite("/home/arthur/Downloads/index.png"));
                gameObject.AddComponent(new Move());
                gameObject.AddComponent(new Collider(new Vector2D(10, 10)));
                gameObject.Tag = "objectA";
                scene.AddGameObject(gameObject);
            }
            {
                GameObject gameObject = new GameObject();
                gameObject.AddComponent(new Transform(new Vector2D(100, 300), 0, new Vector2D(1, 1)));
                gameObject.AddComponent(new Sprite("/home/arthur/Downloads/index.png"));
                gameObject.AddComponent(new Move());
                gameObject.AddComponent(new Collider(new Vector2D(10, 10)));
                gameObject.Tag = "objectB";
                scene.AddGameObject(gameObject);
            }
            scene.OnConstruct();
            const float timeUpdate = 1f / 60f;
            float updateRemainingTime = 0f;
            DateTime lastFrameTime = DateTime.UtcNow;
            while (renderer.IsOpen())
            {
                DateTime beginTime = DateTime.UtcNow;
                float deltaTime =  beginTime.Ticks / TimeSpan.TicksPerMillisecond - lastFrameTime.Ticks / TimeSpan.TicksPerMillisecond;
                lastFrameTime = DateTime.UtcNow;
                updateRemainingTime += deltaTime;
                while (updateRemainingTime >= timeUpdate && renderer.IsOpen())
                {
                    renderer.Clear();
                    renderer.PollInputs();
                    scene.StepUpdate();
                    scene.Update(timeUpdate);
                    renderer.Draw(scene.GameObjects);
                    Input.Instance.ClearActions();
                    renderer.Display();
                    updateRemainingTime -= timeUpdate;
                }
            }
        }
    }
}