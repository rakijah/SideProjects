using Crosswalk.Components;
using Crosswalk.Entities;
using Crosswalk.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Crosswalk
{
    public partial class Crossing : Form
    {
        public static Crossing Instance { get; private set; }

        /// <summary>
        /// The buffer that is used to draw to the screen.
        /// </summary>
        private Buffer Buffer;

        /// <summary>
        /// The active scene.
        /// </summary>
        private Scene Scene;

        /// <summary>
        /// Measures the time since program start.
        /// </summary>
        private Stopwatch gameTime;

        //Used to allow dragging the window.
        private bool Dragging = false;
        private Point StartOffset;

        //Timestamp of previous update.
        TimeSpan previousTime;
        TimeSpan deltaTime;

#if INFO
        float previousTicks = 0;
        Font DebugFont = SystemFonts.StatusFont;
        Brush DebugBrush = Brushes.WhiteSmoke;
#endif
        
        public bool Paused { get; set; }

        public Crossing()
        {
            InitializeComponent();
            
            Instance = this;
            CenterToScreen();
            //Loads all needed sprites.
            Assets.Load();

            Buffer = new Buffer(this, 2.0001f); //2.0001f, because simply using 2f shows white borders for some reason.
            Scene = new MainScene();

            SetUpKeys();

            gameTime = Stopwatch.StartNew();
            Application.Idle += new EventHandler(GameLoop);
        }

        private void SetUpKeys()
        {
            Scene.InputManager.AddKeyDownHandler(Keys.Up, (o, e) => Train.Add(Scene, Train.Direction.Up));
            Scene.InputManager.AddKeyDownHandler(Keys.Down, (o, e) => Train.Add(Scene, Train.Direction.Down));
            Scene.InputManager.AddKeyDownHandler(Keys.Left, (o, e) => Train.Add(Scene, Train.Direction.Left));
            Scene.InputManager.AddKeyDownHandler(Keys.Right, (o, e) => Train.Add(Scene, Train.Direction.Right));
            Scene.InputManager.AddKeyDownHandler(Keys.S, (o, e) => Scene.TimeScale = 0.2f);
        }
        
        private void GameLoop(object o, EventArgs ea)
        {
            while(Program.IsApplicationIdle)
            {
                TimeSpan currentTime = gameTime.Elapsed;
                deltaTime = currentTime - previousTime;
                previousTime = currentTime;
#if INFO
                previousTicks = (float)deltaTime.Ticks;
#endif
                UpdateGame(deltaTime.Milliseconds);
                Draw();
            }
        }

        private void UpdateGame(float DeltaTime)
        {
            if (Paused)
                return;

            Scene.Update(DeltaTime);
        }

        private void Draw()
        {
            Graphics g = Buffer.Begin(Color.White);
            Scene.Draw(g);
#if INFO
            int particleCount = Scene.GetEntitiesList<Particle>().Count;
            string fps = "-";
            if (previousTicks != 0)
            {
                fps = (10000000f / previousTicks).ToString("#");
            }
            g.DrawString(string.Format("{0} FPS ({1}ns)", fps, (previousTicks * 100).ToString("#.###")), DebugFont, DebugBrush, 10, 10);
            g.DrawString(string.Format("{0} Entities ({1} Particles)", (Scene.EntityCount - particleCount), particleCount), DebugFont, DebugBrush, new PointF(10, 20));
            g.DrawString(string.Format("{0} Colliders", Scene.CollisionManager.ColliderCount), DebugFont, DebugBrush, 10, 30);
            g.DrawString(string.Format("Timescale: {0}", Scene.TimeScale), DebugFont, DebugBrush, 10, 40);
#endif
            Buffer.End();
        }

        private void Crossing_KeyDown(object sender, KeyEventArgs e)
        {
            //Exit application when escape is pressed.
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            else if (e.KeyCode == Keys.Space) //Press space to pause.
            {
                Paused = !Paused;
                if (Paused)
                    gameTime.Stop();
                else
                    gameTime.Start();
            }
            else if (e.KeyCode == Keys.Delete) //Kill a random human.
            {
                var humans = Scene.GetEntitiesList<Human>();
                if (humans.Count > 0)
                {
                    humans.GetRandom().Die();
                }
            }
            else if (e.KeyCode == Keys.Back) //Destroy a random car.
            {
                var cars = Scene.GetEntitiesList<Car>();
                if (cars.Count > 0)
                {
                    cars.GetRandom().Explode();
                }
            }
        }

        private void Crossing_FormClosed(object sender, FormClosedEventArgs e)
        {
            Buffer.Dispose();
        }
           
        private void Crossing_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            StartOffset = e.Location;
        }

        private void Crossing_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void Crossing_MouseMove(object sender, MouseEventArgs e)
        {
            if(Dragging)
            {
                Point p = PointToScreen(e.Location);
                SetDesktopLocation(p.X - StartOffset.X, p.Y - StartOffset.Y);
            }
        }
    }
}
