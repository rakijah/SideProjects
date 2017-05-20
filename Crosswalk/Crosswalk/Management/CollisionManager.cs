using Crosswalk.Components;
using Crosswalk.Entities;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Crosswalk.Management
{
    public class CollisionManager
    {
        private SafeList<CircleCollider> Colliders = new SafeList<CircleCollider>();

        public int ColliderCount { get { return Colliders.Count; } }
        
        public CollisionManager()
        {
        }
        
        public void Add(CircleCollider Collider)
        {
            Colliders.Add(Collider);
        }
        
        public void Remove(CircleCollider Collider)
        {
            Colliders.Remove(Collider);
        }

        /// <summary>
        /// Returns a list of entities that collide with the given Collider and posess the Tag.
        /// </summary>
        public IEnumerable<Entity> Collide(CircleCollider Collider, CollisionTag Tag)
        {
            foreach (var possibleCollider in Colliders)
            {
                if (possibleCollider.Tag == Tag && possibleCollider.Entity != Collider.Entity && possibleCollider.CollidesWith(Collider))
                    yield return possibleCollider.Entity;
            }
        }

        public void Update()
        {
            Colliders.Update();
        }
    }
}
