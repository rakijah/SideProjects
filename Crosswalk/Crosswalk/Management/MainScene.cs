using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosswalk.Entities;
using Crosswalk.Components;

namespace Crosswalk.Management
{
    class MainScene : Scene
    {
        private float CarCooldown = 100;
        private float HumanCooldown = 100;

        public override void Initialize()
        {
            //Add background image
            Entity hintergrund = new Entity(Vector2.Zero);
            hintergrund.AddComponent(new Graphic(Assets.Street));
            Add(hintergrund);

            //Add traffic lights
            Add(new TrafficLight(new Vector2(105, 60), TrafficLight.TrafficLightState.Green));
            Add(new TrafficLight(new Vector2(225, 100), TrafficLight.TrafficLightState.Red));
            Add(new TrafficLight(new Vector2(60, 200), TrafficLight.TrafficLightState.Red));
            Add(new TrafficLight(new Vector2(196, 227), TrafficLight.TrafficLightState.Green));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //Add humans and cars in random intervals.
            HumanCooldown -= deltaTime;
            if (HumanCooldown <= 0)
            {
                Human.Add(this);
                HumanCooldown = Util.RandFloat(800, 1500);
            }

            CarCooldown -= deltaTime;
            if (CarCooldown <= 0)
            {
                Car.Add(this);
                CarCooldown = Util.RandFloat(1600, 2000);
            }
        }

    }
}
