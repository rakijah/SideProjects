using Crosswalk.Components;
using Crosswalk.Management;
using System.Collections.Generic;

namespace Crosswalk.Entities
{
    class Car : Entity
    {
        public float Speed = 1f;
           
        /// <summary>
        /// A list of all possible paths.
        /// </summary>
        static List<List<Vector2>> PossiblePaths = new List<List<Vector2>>()
        {
            new List<Vector2>()
            {
                new Vector2(150, -30),
                new Vector2(150, 330)
            },
            new List<Vector2>()
            {
                new Vector2(150, 330),
                new Vector2(150, -30)
            },
            new List<Vector2>()
            {
                new Vector2(-30, 150),
                new Vector2(330, 150)
            },
            new List<Vector2>()
            {
                new Vector2(330, 150),
                new Vector2(-30, 150)
            },
            new List<Vector2>() //drives around the corner, slightly rounded turn
            {
                new Vector2(150, 330),
                new Vector2(150, 170),
                new Vector2(150.5f, 165),
                new Vector2(151.25f, 162),
                new Vector2(152, 160),
                new Vector2(153.5f, 157),
                new Vector2(155, 155),
                new Vector2(156.5f, 153.5f),
                new Vector2(159, 152),
                new Vector2(170, 150),
                new Vector2(330, 150)
            }
        };
        
        /// <summary>
        /// A car that drives along a given path.
        /// </summary>
        private Car(List<Vector2> Path)
            :base(Path[0])
        {
            AddComponent(new Graphic(Assets.Cars.GetRandom(), true));

            //Each car has two colliders (one in the front, one in the back)
            AddComponent(new CircleCollider(new Vector2(10, 0), 15f, CollisionTag.Vehicle));
            AddComponent(new CircleCollider(new Vector2(-10, 0), 15f, CollisionTag.Vehicle));
            AddComponent(new TrafficLightDetector());
            AddComponent(new Path(Path));

            //Slightly vary the speed.
            Speed = 1.0f + Util.Float();
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            var ap = GetComponent<TrafficLightDetector>();
            if (ap == null || !ap.RedTrafficLightDetected)
            {
                Position += (Velocity * Speed) * (deltaTime / 30f);
            }
            
            //Add smoke to exhaust.
            if ((int)(TimeAlive / 10) % 10 == 0)
            {
                float RotInvert = Util.InvertAngle(Rotation);
                float InvInRadians = Util.ToRadians(RotInvert);
                Vector2 RotVec = Vector2.FromAngle(InvInRadians, 30);
                Vector2 ParticlePosition = Position + RotVec;
                
                Scene.Add(new Particle(ParticlePosition, Assets.Smoke)
                    { 
                        Rotation = Util.Float(360),
                        RotationSpeed = Util.RandFloat(40, 60), 
                        FinalScale = 0.2f, 
                        Velocity = Util.RandomDirection()
                    });
            }
        }

        /// <summary>
        /// Removes this car from the scene and adds an explosion particle effect.
        /// </summary>
        public void Explode()
        {
            Scene.Remove(this);
            
            for (int i = 0; i < 4; ++i)
            {
                Scene.Add(
                    new Particle(Position + Util.RandomDirection() * 20f, Assets.Debris.GetRandom())
                        {
                             LifeTime = 2000f,
                             Scale = 2f,
                             RotationSpeed = Util.Float(5f),
                             Velocity = Util.RandomDirection()
                        }
                    );
            }
            
            int amount = Util.Rand.Next(7, 12);
            for (int i = 0; i < amount; ++i)
            {
                Scene.Add(
                    new Particle(Position + Util.RandomDirection() * Util.Float(5), Assets.Smoke)
                    {
                        LifeTime = 2000f,
                        Scale = 3f,
                        RotationSpeed = Util.Float() * 4f,
                        FinalScale = 0.2f,
                        Velocity = Util.RandomDirection(),
                        FinalSpeed = 0.2f
                    });
            }
            
            for (int i = 0; i < 3; ++i)
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
        }

        /// <summary>
        /// Add a new car to the scene that follows a random path.
        /// </summary>
        public static void Add(Scene scene)
        {
            scene.Add(new Car(PossiblePaths.GetRandom()));
        }
    }
}
