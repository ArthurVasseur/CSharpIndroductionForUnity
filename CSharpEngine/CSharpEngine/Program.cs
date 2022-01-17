using CSharpEngine.Components;
using CSharpEngine.Physics;
using System;
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