using Crosswalk.Entities;
using Crosswalk.Management;
using System.Drawing;

namespace Crosswalk.Components
{
    public abstract class Component
    {
        /// <summary>
        /// The entity that owns this component.
        /// </summary>
        public Entity Entity { get; set; }
        
        public Scene Scene { get { return Entity.Scene; } }
        public virtual void Update(float deltaTime) { }
        public virtual void Draw(Graphics g) { }

        /// <summary>
        /// Called before the owner is removed from the scene.
        /// </summary>
        public virtual void OnRemove() { }

        /// <summary>
        /// Called after this was added to an entity within a scene.
        /// </summary>
        public virtual void Initialize() { }
    }
}
