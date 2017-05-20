using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Crosswalk.Components
{
    class Graphic : Component
    {
        /// <summary>
        /// Origin. (0, 0) is in the upper left corner.
        /// </summary>
        public Vector2 Origin = Vector2.Zero;
        
        /// <summary>
        /// Handles translation/rotation and scaling.
        /// </summary>
        public Matrix Transformation = new Matrix();
        
        public float Scale = 1f;
        private Image Image;
        private ImageAttributes Attributes = new ImageAttributes();
        private ColorMatrix ImageColorMatrix = new ColorMatrix();
        
        public float Alpha
        {
            get
            {
                return ImageColorMatrix.Matrix33;
            }
            set
            {
                if (ImageColorMatrix.Matrix33 != value)
                {
                    ImageColorMatrix.Matrix33 = value;
                    Attributes.SetColorMatrix(ImageColorMatrix);
                }
            }
        }

        public int Width { get { return Image.Width; } }
        public int Height { get { return Image.Height; } }
        
        public Graphic(Image Image, bool CenterOrigin = false)
        {
            this.Image = Image;
            if (CenterOrigin)
            {
                Origin = new Vector2(Image.Width / 2f, Image.Height / 2f);
            }
            Attributes.SetColorMatrix(ImageColorMatrix);
        }
        
        public Graphic(Image Image, Vector2 Origin)
        {
            this.Image = Image;
            this.Origin = Origin;
        }
        
        public override void Draw(Graphics g)
        {
            Transformation.Reset();
            if (Scale != 1 || Entity.Rotation != 0)
            {
                Transformation.Translate(Entity.Position.X, Entity.Position.Y);
                Transformation.Scale(Scale, Scale);
                Transformation.Rotate(Entity.Rotation);
                Transformation.Translate(-Entity.Position.X, -Entity.Position.Y);
            }
            g.Transform = Transformation;
            if (Alpha == 1f)
            {
                g.DrawImage(Image, new PointF(Entity.Position.X - Origin.X, Entity.Position.Y - Origin.Y));
            }
            else
            {
                Rectangle dest = new Rectangle((int)(Entity.Position.X - Origin.X), (int)(Entity.Position.Y - Origin.Y), (int)Image.Width, (int)Image.Height);
                g.DrawImage(Image, dest, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, Attributes);
            }
            g.ResetTransform();
        }
    }
}
