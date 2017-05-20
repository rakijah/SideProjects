using Crosswalk.Components;
using Crosswalk.Management;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Crosswalk.Entities
{
    public class Entity
    {
        /// <summary>
        /// The position of the entity.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The speed of the entity.
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// The angle (in degrees) of the entity.
        /// </summary>
        public float Rotation;

        /// <summary>
        /// If not -1, remove entity when this counts to 0
        /// </summary>
        public float LifeTime = -1;

        /// <summary>
        /// Time spent alive (in ms).
        /// </summary>
        public float TimeAlive = 0;
           
        private List<Component> Components = new List<Component>();
        
        /// <summary>
        /// Angle of the velocity.
        /// </summary>
        public float VelocityAngle { get
            {
                return Vector2.Angle(Velocity);
            }
        }

        /// <summary>
        /// The scene that controls this entity.
        /// </summary>
        public Scene Scene { get; set; }
        
        public Entity(Vector2 Position)
        {
            this.Position = Position;
            Velocity = Vector2.Zero;
        }
        
        public virtual void Update(float deltaTime)
        {
            foreach (Component c in Components)
            {
                c.Update(deltaTime);
            }

            TimeAlive += deltaTime;

            if (LifeTime != -1)
            {
                if(TimeAlive >= LifeTime)
                {
                    Scene.Remove(this);
                }
            }
        }
        
        public virtual void Draw(Graphics g)
        {
            foreach (Component c in Components)
            {
                c.Draw(g);
            }
        }

        /// <summary>
        /// Gets called when the entity is added to a scene.
        /// </summary>
        public virtual void Added() {}
        
        public T AddComponent<T>(T component) where T:Component
        {
            if (!Components.Contains(component))
            {
                component.Entity = this;
                Components.Add(component);
                if(Scene != null)
                    component.Initialize();
            }
            return component;
        }
        
        public void RemoveComponent<T>() where T : Component
        {
            T component = GetComponent<T>();
            if(component != null)
            {
                Components.Remove(component);
            }
        }

        /// <summary>
        /// Returns the first component of type T
        /// If none are found, returns null.
        /// </summary>
        public T GetComponent<T>() where T : Component
        {
            return Components.OfType<T>().FirstOrDefault();
        }
        
        public IEnumerable<Component> GetComponents()
        {
            foreach (Component c in Components)
            {
                yield return c;
            }
        }
        
        public void Initialize()
        {
            foreach (Component c in Components)
            {
                c.Initialize();
            }
        }
    }
}
