using Crosswalk.Components;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Crosswalk.Entities
{
    class Particle : Entity
    {
        /// <summary>
        /// Initial rotation speed.
        /// </summary>
        public float RotationSpeed = 0f;

        /// <summary>
        /// Alpha at the end of lifetime.
        /// </summary>
        public float FinalAlpha = 0f;

        /// <summary>
        /// Scale at the end of lifetime.
        /// </summary>
        public float FinalScale = 1f;

        /// <summary>
        /// Speed at the end of lifetime.
        /// </summary>
        public float FinalSpeed = 1f;

        /// <summary>
        /// Current speed.
        /// </summary>
        public float Speed = 1f;
        
        private Graphic Graphic;
        private float StartSpeed = 1f;
        private float StartScale = 1f;
        private float StartAlpha = 1f;
           
        public float Scale { get { return Graphic.Scale; } set { Graphic.Scale = value; } }
        public float Alpha { get { return Graphic.Alpha; } set { Graphic.Alpha = value; } }
        
        public Particle(Vector2 Position, Image Image)
            :base(new Vector2(Position.X, Position.Y))
        {
            Graphic = AddComponent(new Graphic(Image, true));
            LifeTime = 500;
        }
        
        public override void Added()
        {
            StartSpeed = Speed;
            StartAlpha = Alpha;
            StartScale = Scale;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            float deltaTimeScaled = (deltaTime / 40f);
            Position += Velocity * (Speed * deltaTimeScaled);
            Rotation += RotationSpeed * deltaTimeScaled;

            //Calculate life time percentage.
            float percentAlive = (TimeAlive / LifeTime);
            
            //Adjust properties accordingly.
            Speed = Util.Lerp(StartSpeed, FinalSpeed, percentAlive);
            Scale = Util.Lerp(StartScale, FinalScale, percentAlive);
            Alpha = Util.Lerp(StartAlpha, FinalAlpha, percentAlive);
        }
    }
}
