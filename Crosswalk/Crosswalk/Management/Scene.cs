using Crosswalk.Components;
using Crosswalk.Entities;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crosswalk.Management
{
    public class Scene
    {
        private SafeList<Entity> Entities = new SafeList<Entity>();
        private float timeScale = 1f;
        private float timeScaleCooldown = 0f;
        
        public CollisionManager CollisionManager { get; private set; }
        public InputManager InputManager { get; private set; }
        
        public float TimeScale
        { 
            get 
            { 
                return timeScale; 
            } 
            set 
            { 
                timeScale = value;
                timeScaleCooldown = 250f;
            } 
        }

        public int EntityCount { get { return Entities.Count; } }
        
        /// <summary>
        /// A scene that manages entities, input and collisions
        /// </summary>
        public Scene()
        {
            CollisionManager = new CollisionManager();
            InputManager = new InputManager();
            Initialize();
        }

        public virtual void Initialize()
        {
        }
        
        public virtual void Update(float deltaTime)
        {
            if (TimeScale != 1f)
            {
                if (timeScaleCooldown > 0)
                {
                    timeScaleCooldown -= deltaTime;
                }
                else
                {
                    TimeScale = Util.Approach(TimeScale, 1, 0.1f);
                }
                deltaTime = deltaTime * TimeScale;
            }
            
            foreach (Entity entity in Entities)
            {
                entity.Update(deltaTime);
            }
            
            Entities.Update();
            CollisionManager.Update();
        }

        /// <summary>
        /// Add an entity to the scene.
        /// </summary>
        public T Add<T>(T entity) where T: Entity
        {
            entity.Scene = this;
            Entities.Add(entity);
            entity.Initialize();
            return entity;
        }

        /// <summary>
        /// Remove an entity from the scene.
        /// </summary>
        /// <param name="entity">Die Entity, die entfernt werden soll.</param>
        public void Remove(Entity entity)
        {
            foreach (Component c in entity.GetComponents())
            {
                c.OnRemove();
            }
            Entities.Remove(entity);
        }

        /// <summary>
        /// Returns a list of entities within the given radius around the given position.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <returns></returns>
        public List<T> GetEntities<T>(Vector2 Position, float Radius) where T : Entity
        {
            return Entities.Where(x => x.GetType() == typeof(T) &&
                                       Vector2.Distance(x.Position, Position) < Radius).Cast<T>().ToList();
        }

        /// <summary>
        /// Returns a list of entities of a given type.
        /// </summary>
        public List<T> GetEntitiesList<T>() where T : Entity
        {
            return Entities.Where(x => x is T).Cast<T>().ToList();
        }

        /// <summary>
        /// Returns an IEnumerable of entities of a given type.
        /// </summary>
        public IEnumerable<T> GetEntities<T>() where T : Entity
        {
            foreach(var entity in Entities)
            {
                if(entity is T)
                {
                    yield return entity as T;
                }
            }
        }

        public virtual void Draw(Graphics g)
        {
            foreach (Entity entity in Entities)
            {
                entity.Draw(g);
            }
        }   
    }
}
