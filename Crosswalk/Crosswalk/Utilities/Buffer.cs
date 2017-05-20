using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Crosswalk
{
    public class Buffer : IDisposable
    {  
        private Control m_Owner;
        private Image m_BufferImage;
        private Graphics m_BufferGraphics;
        private Graphics m_OwnerGraphics;
        private Matrix ScaleMat;
        
        public bool Scaled { get; private set; }

        /// <summary>
        /// A doublebuffer used to draw to a Form Control.
        /// </summary>
        /// <param name="Owner">The Control that this Buffer draws to.</param>
        public Buffer(Control Owner, float ScaleFactor = 1f)
        {
            m_Owner = Owner;
            m_BufferImage = new Bitmap(Owner.Width, Owner.Height);
            m_BufferGraphics = Graphics.FromImage(m_BufferImage);
            m_OwnerGraphics = m_Owner.CreateGraphics();
            m_OwnerGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            m_BufferGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            m_BufferGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            m_BufferGraphics.PixelOffsetMode = PixelOffsetMode.Half;

            if (ScaleFactor != 1)
            {
                Scaled = true;
                ScaleMat = new Matrix();
                ScaleMat.Scale(ScaleFactor, ScaleFactor);
            }
        }
        
        /// <summary>
        /// Clear the screen to white and initiate drawing process.
        /// Call End() to draw to screen.
        /// </summary>
        public Graphics Begin()
        {
            return Begin(Color.White);
        }

        /// <summary>
        /// Clear the screen to the specified color and initiate drawing process.
        /// </summary>
        public Graphics Begin(Color ClearColor)
        {
            m_BufferGraphics.Clear(ClearColor);
            return m_BufferGraphics;
        }

        /// <summary>
        /// Ends the drawing process and draws to the screen.
        /// </summary>
        public void End()
        {
            if (Scaled)
            {
                m_OwnerGraphics.Transform = ScaleMat;
            }

            m_OwnerGraphics.DrawImage(m_BufferImage, Point.Empty);
        }

        public void Dispose()
        {
            m_BufferGraphics.Dispose();
            m_OwnerGraphics.Dispose();
            m_BufferImage.Dispose();
        }
    }
}
