using System.Collections.Generic;
using System.Drawing;

namespace Crosswalk.Components
{
    class Path : Component
    {
        public int CurrentIndex = 0;
        public Vector2 CurrentWayPoint;
           
        private List<Vector2> WayPoints = new List<Vector2>();

#if TEST
        Point[] DebugPoints;
        Pen DebugPen;
#endif    
        /// <summary>
        /// Set to true if last waypoint reached.
        /// </summary>
        public bool TargetReached { get; set; }

        /// <summary>
        /// Movement speed of the entity.
        /// </summary>
        public float Speed { get; set; }
          
        /// <summary>
        /// Move an entity along a given path and remove it after reaching the target location.
        /// </summary>
        public Path(List<Vector2> WayPoints, float Speed = 2f)
        {
            this.WayPoints = WayPoints;
            this.Speed = Speed;
            this.TargetReached = false;
            CurrentWayPoint = WayPoints[0];
#if TEST
            DebugPen = new Pen(Util.RandomColor(128));
            DebugPen.Width = 3;
            DebugPoints = new Point[WayPoints.Count];
            for(int i = 0; i < WayPoints.Count; ++i)
            {
                DebugPoints[i] = WayPoints[i].ToPoint();
            }
#endif
        }
#if TEST
        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawLines(DebugPen, DebugPoints);
        }
#endif

        public override void Update(float deltaTime)
        {
            if (TargetReached)
                return;
            
            if (Vector2.Distance(Entity.Position, CurrentWayPoint) < 2)
            {
                Entity.Position = CurrentWayPoint;
                if (++CurrentIndex < WayPoints.Count)
                {
                    CurrentWayPoint = WayPoints[CurrentIndex];
                }
                else
                {
                    TargetReached = true;
                    Scene.Remove(Entity);
                }
            }
            else
            {
                Vector2 Towards = Vector2.Normalize(CurrentWayPoint - Entity.Position);
                Entity.Velocity = Towards * Speed;
                Entity.Rotation = Util.ToDegrees(Entity.VelocityAngle);
            }
        }
    }
}
