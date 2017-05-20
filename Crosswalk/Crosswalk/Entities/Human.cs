using Crosswalk.Components;
using Crosswalk.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Crosswalk.Entities
{
    class Human : Entity
    {
        CircleCollider Collider;
        TrafficLightDetector TrafficLightDetector;
        
        /// <summary>
        /// A list of all possible paths.
        /// </summary>
        static List<List<Vector2>> PossiblePaths = new List<List<Vector2>>()
        {
            new List<Vector2>()
            {
                new Vector2(100, -10),
                new Vector2(100, 310)
            },
            new List<Vector2>()
            {
                new Vector2(310, 105),
                new Vector2(-10, 105)
            },
            new List<Vector2>()
            {
                new Vector2(200, 310),
                new Vector2(200, -10)
            },
            new List<Vector2>()
            {
                new Vector2(-10, 200),
                new Vector2(310, 200)
            }
        };
           
        public float Speed { get; set; }
        public bool Fleeing { get; set; }
        
        /// <summary>
        /// A human that walks along a given path.
        /// </summary>
        private Human(List<Vector2> Path)
            : base(Path[0])
        {
            AddComponent(new Path(Path));
            AddComponent(new Graphic(Assets.Humans.GetRandom(), true));
            Collider = AddComponent(new CircleCollider(Vector2.Zero, 5, CollisionTag.Human)) as CircleCollider;

            //A random offset is added to the traffic light detector to avoid humans bunching up on top of each other at a red light.
            float RandOffset = Util.Rand.Next(-10, 10);
            TrafficLightDetector = AddComponent(new TrafficLightDetector() { OffsetAmount = 20 + RandOffset, OffsetAngle = 0, Radius = 10 });

            //Slightly vary the walking speed.
            Speed = (Util.Float() + 1f) * 0.5f;
            Fleeing = false;
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            if (Fleeing || !TrafficLightDetector.RedTrafficLightDetected)
            {
                Position += Velocity * (Speed * (deltaTime / 20f));
                float Rot = (float)Math.Sin(TimeAlive / 50f);
                Rotation += Rot * 20f;
            }

            //Die on collision with a car.
            var cars = Scene.CollisionManager.Collide(Collider, CollisionTag.Vehicle);
            foreach(var auto in cars)
            {
                Die();
                return;
            }
        }

        /// <summary>
        /// Removes a human from the scene and displays a particle effect.
        /// All other humans flee from the point of death.
        /// </summary>
        public void Die()
        {
            Scene.Remove(this);
            
            int amount = Util.Rand.Next(3, 7);
            for (int i = 0; i < amount; ++i)
            {
                Scene.Add(new Particle(Position, Assets.Blood.GetRandom())
                {
                    LifeTime = 2000f,
                    Velocity = Util.RandomDirection() * Util.Float(4),
                    FinalSpeed = 0f,
                    Rotation = Util.RandomAngle(),
                    RotationSpeed = Util.Float(10)
                });
            }

            var humans = Scene.GetEntities<Human>();
            foreach (Human m in humans)
            {
                if (m == this)
                    continue;

                //Remove path component to disable pathing.
                m.RemoveComponent<Path>();
                m.RemoveComponent<TrafficLightDetector>();
                
                //Flee from the point of death.
                m.Velocity = Vector2.Normalize(m.Position - Position);
                m.Rotation = m.VelocityAngle;
                m.LifeTime = m.TimeAlive + 3000f;
                m.Speed = 4f;
                m.Fleeing = true;
            }
        }
        
        /// <summary>
        /// Add a new human to the scene that follows a random path.
        /// </summary>
        public static void Add(Scene scene)
        {
            List<Vector2> Path = PossiblePaths.GetRandom();
            scene.Add(new Human(Path));
        }
    }
}
