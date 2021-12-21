using System;
using System.Collections.Generic;
using CSharpEngine.Components;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Sprite = CSharpEngine.Components.Sprite;
using Transform = CSharpEngine.Components.Transform;

namespace CSharpEngine
{
    public class Renderer : IRenderer
    {
        private RenderWindow _renderWindow;
        private Dictionary<string, Texture> _textures = new Dictionary<string, Texture>();
        private Dictionary<string, Font> _fonts = new Dictionary<string, Font>();
        public Renderer()
        {
            _renderWindow = new RenderWindow(new VideoMode(1280, 720), "CSharp Engine");
            Input.Instance = new Input();
            _renderWindow.Closed += RenderWindowOnClosed;
            _renderWindow.KeyPressed += RenderWindowOnKeyPressed;
        }

        private static void  RenderWindowOnKeyPressed(object? sender, KeyEventArgs e)
        {
            Input.Instance.PressedActions.Add((KeyCode)e.Code);
        }

        private static void  RenderWindowOnClosed(object? sender, EventArgs e)
        {
            if (sender == null)
                return;
            ((RenderWindow) sender).Close();
        }

        public void Draw(List<GameObject> gameObjects)
        {
            foreach(var gameObject in gameObjects)
            {
                var transform = gameObject.GetComponent<Transform>();
                var sprite = gameObject.GetComponent<Sprite>();
                var text = gameObject.GetComponent<Components.Text>();
                if (transform == null)
                    continue;
                if (sprite != null) 
                    DrawSprite(sprite.Path, transform);
                if (text != null) 
                    DrawText(text, transform);
            }
        }

        public void PollInputs()
        {
            _renderWindow.DispatchEvents();
        }

        public void Clear()
        {
            _renderWindow.Clear();
        }

        public void Display()
        {
            _renderWindow.Display();
        }

        public bool IsOpen()
        {
            return _renderWindow.IsOpen;
        }

        private void DrawSprite(string path, Components.Transform transform)
        {
            if (!_textures.ContainsKey(path)) 
                _textures[path] = new Texture(path);
            SFML.Graphics.Sprite sprite = new(_textures[path]);
            sprite.Position = new Vector2f(transform.Position.X, transform.Position.Y);
            sprite.Rotation = transform.Rotation;
            sprite.Scale = new Vector2f(transform.Scale.X, transform.Scale.Y);
            _renderWindow.Draw(sprite);
        }

        private void DrawText(Components.Text text, Transform transform)
        {
            if (!_fonts.ContainsKey(text.Font))
                _fonts[text.Font] = new Font(text.Font);
            SFML.Graphics.Text txt = new (text.Txt, _fonts[text.Font]);
            txt.Position = new Vector2f(transform.Position.X, transform.Position.Y);
            txt.Scale = new Vector2f(transform.Scale.X, transform.Scale.Y);
            txt.Rotation = transform.Rotation;
            _renderWindow.Draw(txt);
        }
    }
}