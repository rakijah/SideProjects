using System.Drawing;

namespace Crosswalk.Entities
{
    class TrafficLight : Entity
    {
        public float Counter = 0;
           
        /// <summary>
        /// Size of the traffic light, not changeable.
        /// </summary>
        private static Point Size = new Point(16, 40);

        /// <summary>
        /// The length of each phase in this order: green, yellow, red.
        /// If you want the traffic lights to synchronize: red = (green + yellow)
        /// </summary>
        public readonly float[] PhaseLength = new float[] { 4000f, 1000f, 5000f };
        
        public TrafficLightState State { get; set; }
        
        /// <summary>
        /// A traffic light.
        /// </summary>
        /// <param name="Status">Initial state of the traffic light.</param>
        public TrafficLight(Vector2 Position, TrafficLightState Status = TrafficLightState.Green)
            :base(Position)
        {
            Velocity = Vector2.Zero;
            this.State = Status;
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Counter += deltaTime;
            if (Counter >= PhaseLength[(int)State])
            {
                State = (TrafficLightState)(((int)State + 1) % 3);
                Counter = 0;
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            //Calculate center of traffic light.
            float dx = Position.X - Size.X / 2f;
            float dy = Position.Y - Size.Y / 2f;
            //Draw a black background.
            g.FillRectangle(Brushes.Black,  dx, dy, Size.X, Size.Y);
            
            float cWidth = (Size.X - 2f);
            float cHeight = (Size.Y - 2f) / 3f;

            //Draw circles according to state.
            g.FillEllipse(
                (State == TrafficLightState.Red ? Brushes.Red : Brushes.White)
                , dx + 1, dy + 1 + (cHeight * 0), cWidth, cHeight);

            g.FillEllipse(
                (State == TrafficLightState.Yellow ? Brushes.Yellow : Brushes.White)
                , dx + 1, dy + 1 + (cHeight * 1), cWidth, cHeight);

            g.FillEllipse(
               (State == TrafficLightState.Green ? Brushes.Green : Brushes.White)
                , dx + 1, dy + 1 + (cHeight * 2), cWidth, cHeight);
#if TEST
            g.FillEllipse(
                (Status == AmpelStatus.Red ? Brushes.Red : Brushes.Blue)
                , Position.X - 2, Position.Y - 2, 4, 4);
#endif
        }
        
        public enum TrafficLightState
        {
            Green = 0,
            Yellow = 1,
            Red = 2
        }
    }
}
