using System;

namespace CSharpEngine.Components
{
    public abstract class Component : Object
    {
        public GameObject GameObject;
        public Transform? Transform;
        public bool Initialized { get; private set; }
        public virtual void OnConstruct()
        {
            Transform = GameObject.GetComponent<Transform>();
            Initialized = true;
        }

        public virtual void Update(float deltaTime)
        {
        }

        public virtual void OnCollisionEnter(GameObject go)
        {
            
        }

        public virtual void OneBecameInvisible()
        {
            
        }

        public void Instantiate(GameObject gameObject)
        {
            Scene.Instantiate(gameObject);
        }

        public void Destroy(GameObject gameObject)
        {
            Scene.Destroy(gameObject);
        }
    }
}