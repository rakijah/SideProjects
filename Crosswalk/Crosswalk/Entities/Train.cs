using Crosswalk.Components;
using Crosswalk.Management;
using System.Collections.Generic;

namespace Crosswalk.Entities
{
    class Train : Entity
    {
        public float Speed = 3f;
           
        private CircleCollider Collider;

        private static List<Vector2> PossibleStartPositions = new List<Vector2>
        {
            new Vector2(-100, 150), //from the left
            new Vector2(150, -100), //from above
            new Vector2(400, 150), //from the right
            new Vector2(150, 400)  //from below
        };
        
        /// <summary>
        /// A train that drives along a given path.
        /// Does not react to traffic lights.
        /// </summary>
        private Train(List<Vector2> Path)
            :base(Path[0])
        {
            AddComponent(new Path(Path));
            AddComponent(new Graphic(Assets.Train.GetRandom(), true));
            Collider = AddComponent(new CircleCollider(new Vector2(60, 0), 10, CollisionTag.Vehicle)) as CircleCollider;
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Position += (Velocity * Speed) * (deltaTime / 30f);

            //Destroy cars on collision.
            var vehicles = Scene.CollisionManager.Collide(Collider, CollisionTag.Vehicle);
            foreach (Entity e in vehicles)
            {
                if (e is Car)
                {
                    (e as Car).Explode();
                    Scene.TimeScale = 0.1f;
                }
            }
        }
        
        /// <summary>
        /// Adds a train to the scene that follows a given path.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="direction"></param>
        public static void Add(Scene scene, Direction direction)
        {
            int StartIndex = (int)direction;
            Vector2 Start = PossibleStartPositions[StartIndex];
            Vector2 End = PossibleStartPositions[(StartIndex + 2) % PossibleStartPositions.Count];
            List<Vector2> Path = new List<Vector2>() { Start, End };
            scene.Add(new Train(Path));
        }
        
        public enum Direction
        {
            Right = 0,
            Down = 1,
            Left = 2,
            Up = 3
        }
    }
}
