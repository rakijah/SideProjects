using Crosswalk.Entities;
using System.Drawing;
using System.Linq;

namespace Crosswalk.Components
{
    class TrafficLightDetector : Component
    {    
        /// <summary>
        /// Radius of the collider.
        /// </summary>
        public float Radius = 20f;
        
        /// <summary>
        /// Offset amount to the front.
        /// </summary>
        public float OffsetAmount = 50;

        /// <summary>
        /// Angle offset (in radius) in relation to the entity.
        /// </summary>
        public float OffsetAngle = 45;
        
        private Vector2 Position = Vector2.Zero;
        
        public bool RedTrafficLightDetected { get; private set; }
        
        /// <summary>
        /// Detects red traffic lights.
        /// </summary>
        public TrafficLightDetector()
        {
            this.RedTrafficLightDetected = false;
        }
        
        public override void Update(float deltaTime)
        {
            Position = Entity.Position + Vector2.FromAngle(Util.ToRadians(Entity.Rotation + OffsetAngle), OffsetAmount);
            var entities = Scene.GetEntities<TrafficLight>(Position, Radius);
            RedTrafficLightDetected = (entities.Any(x => x.State == TrafficLight.TrafficLightState.Red));
        }

#if TEST
        Pen debugPen = new Pen(Color.FromArgb(160, Color.Red), 2f);
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(debugPen, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
        }
#endif
    }
}
