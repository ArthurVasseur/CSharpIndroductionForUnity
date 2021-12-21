using System;

namespace CSharpEngine.Components
{
    public class Component
    {
        public GameObject GameObject;
        public Transform? Transform;
        public virtual void OnConstruct()
        {
            Transform = GameObject.GetComponent<Transform>();
        }

        public virtual void Update(float deltaTime)
        {
        }

        public virtual void OnCollisionEnter(GameObject go)
        {
            
        }
    }
}