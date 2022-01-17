using System.Collections.Generic;
using CSharpEngine.Components;
using CSharpEngine.Physics;

namespace CSharpEngine
{
    public class Scene
    {
        private static Scene _instance;
        public List<GameObject> GameObjects { get; set; }
        private List<GameObject> _pendingDestroyedGameObjects = new List<GameObject>();
        private List<GameObject> _pendingAddedGameObjects = new List<GameObject>();
        public Scene()
        {
            _instance = this;
            GameObjects = new List<GameObject>();
        }

        public void Update(float deltaTime)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(deltaTime);
            }
            foreach (var gameObject in _pendingDestroyedGameObjects)
            {
                GameObjects.RemoveAll(go => go == gameObject);
            }
            _pendingDestroyedGameObjects.Clear();
            foreach (var go in _pendingAddedGameObjects)
            {
                AddGameObject(go);
            }
            _pendingAddedGameObjects.Clear();
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

            foreach (var gameObject in GameObjects)
            {
                var transform = gameObject.GetComponent<Transform>();
                var position = transform.Position;

                if (position.X < -10 || position.X > 1300 || position.Y < -10 || position.Y > 730)
                {
                    foreach (var component in gameObject.Components)
                    {
                        component.OneBecameInvisible();
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

        public static void Instantiate(GameObject go)
        {
            _instance._pendingAddedGameObjects.Add(go);
        }

        public static void Destroy(GameObject gameObject)
        {
            _instance._pendingDestroyedGameObjects.Add(gameObject);
        }
    }
}