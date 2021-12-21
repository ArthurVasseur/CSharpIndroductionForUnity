using System.Collections.Generic;
using CSharpEngine.Physics;

namespace CSharpEngine
{
    public class Scene
    {
        public List<GameObject> GameObjects { get; set; }
        public Scene()
        {
            GameObjects = new List<GameObject>();
        }

        public void Update(float deltaTime)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(deltaTime);
            }
        }

        public void StepUpdate()
        {
            foreach (var gameObject in GameObjects)
            {
                var collider = gameObject.GetComponent<Collider>();
                if (collider == null)
                    continue;
                foreach (var otherGameObject in GameObjects)
                {
                    var otherCollider = otherGameObject.GetComponent<Collider>();
                    if (otherCollider == null || gameObject == otherGameObject || !collider.IsTrigger)
                        continue;
                    if (collider.CheckCollisionEnter(otherCollider))
                    {
                        collider.GameObject.OnCollisionEnter(otherGameObject);
                    }
                }
            }
        }

        public void OnConstruct()
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.OnConstruct();
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
            foreach (var gameObjectComponent in gameObject.Components)
            {
                gameObjectComponent.GameObject = gameObject;
            }
        }
    }
}