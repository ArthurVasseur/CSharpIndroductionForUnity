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
                GameObject background = new GameObject();
                background.AddComponent(new Transform(new Vector2D(0,0),0, new Vector2D(1,1)));
                background.AddComponent(new Sprite("../../../Assets/background.png"));
                scene.AddGameObject(background);
            }
            {
                GameObject go = new GameObject();
                go.Tag = "Player";
                go.AddComponent(new Transform(new Vector2D(100, 100), 90, new Vector2D(1,1)));
                go.AddComponent(new Sprite("../../../Assets/player.png"));
                go.AddComponent(new Player());
                scene.AddGameObject(go);
            }
            {
                GameObject txt = new GameObject();
                txt.AddComponent(new Transform(new Vector2D(1200, 15), 0, new Vector2D(1,1)));
                txt.AddComponent(new Text("../../../Assets/pixelated.ttf", "TXT", new Color(255,255,255), 20));
                txt.AddComponent(new HealthText());
                scene.AddGameObject(txt);
            }

            {
                GameObject enemy = new GameObject();
                enemy.Tag = "enemy";
                enemy.AddComponent(new Transform(new Vector2D(700, 200), 0, new Vector2D(1,1)));
                enemy.AddComponent(new Sprite("../../../Assets/enemy.png"));
                enemy.AddComponent(new Enemy());
                enemy.AddComponent(new Collider(new Vector2D(10,10), true));
                scene.AddGameObject(enemy);
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