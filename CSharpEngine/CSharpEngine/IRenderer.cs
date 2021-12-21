using System.Collections.Generic;
using CSharpEngine.Components;

namespace CSharpEngine
{
    public interface IRenderer
    {
        public void Draw(List<GameObject> gameObjects);
        public void Clear();
        public void Display();
        public bool IsOpen();
        public void PollInputs();
    }
}