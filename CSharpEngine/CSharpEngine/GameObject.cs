using System.Collections.Generic;
using CSharpEngine.Components;

namespace CSharpEngine
{
    public class GameObject
    {
        public List<Component> Components = new List<Component>();
        public string Tag = "Untagged";
        private List<Component> _queue = new List<Component>();
        public T? GetComponent<T>() where T : Component
        {
            foreach (var component in Components)
            {
                if (component.GetType() == typeof(T))
                {
                    return (T)component;
                }
            }
            return null;
        }

        public void AddComponent(Component component)
        {
            Components.Add(component);
            if (component.Initialized) 
                return;
            component.GameObject = this;
            component.OnConstruct();
        }

        public void OnConstruct()
        {
            foreach (var component in Components)
            {
                if(component.Initialized == false)
                    component.OnConstruct();
            }
        }
        public void Update(float deltaTime)
        {
            foreach (var component in Components)
            {
                component.Update(deltaTime);
            }
        }

        public void OnCollisionEnter(GameObject go)
        {
            foreach (Component component in Components)
            {
                component.OnCollisionEnter(go);
            }
        }
    }
}