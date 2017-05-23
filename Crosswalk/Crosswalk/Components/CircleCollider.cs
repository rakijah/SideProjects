using System;
using System.Drawing;
namespace Crosswalk.Components
{
    public class CircleCollider : Component
    {
        public float Radius { get; set; }
        public CollisionTag Tag { get; set; }

        /// <summary>
        /// Offset relative to the entity position.
        /// </summary>
        public Vector2 Offset { get; set; }
   
        public Vector2 Position = Vector2.Zero;
        
        public CircleCollider(Vector2 Offset, float Radius, CollisionTag Tag)
        {
            this.Radius = Radius;
            this.Tag = Tag;
            this.Offset = Offset;
        }
        
        public override void Update(float deltaTime)
        {
            CalculatePosition();
        }

        private void CalculatePosition()
        {
            Vector2 Dir = Entity.Position + Offset;
            Dir.RotateAround(Entity.Position, Util.ToRadians(Entity.Rotation));
            Position = Dir;
        }
        
        public bool CollidesWith(CircleCollider Collider)
        {
            return Util.PowF(Position.X - Collider.Position.X, 2) + Util.PowF(Position.Y - Collider.Position.Y, 2) <= Util.PowF(Radius + Collider.Radius, 2);
        }

        public override void Initialize()
        {
            Scene.CollisionManager.Add(this);
            CalculatePosition();
        }

        public override void OnRemove()
        {
            Scene.CollisionManager.Remove(this);
        }

#if TEST
        Pen debugPen = new Pen(Color.FromArgb(160, Color.Yellow), 2f);
        public override void Draw(System.Drawing.Graphics g)
        {
            
            g.DrawEllipse(debugPen, Util.ToEllipseRect(Position, Radius));   
        }
#endif
        
    }
    
    public enum CollisionTag
    {
        Vehicle,
        Human
    }
}
