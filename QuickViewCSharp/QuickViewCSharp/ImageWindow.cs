using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace QuickViewCSharp
{
    public partial class ImageWindow : Form
    {
        private string fileName;
        private Image currentImage;
        private Boolean isMoving = false, shiftKeyDown = false, fKeyDown = false, opacityToggle = false, lockPos = false;
        private Point curPos, mousePos;
        private static string[] files;
        private static int currentImageIndex = 0;

        public ImageWindow(string[] args)
        {
            InitializeComponent();
            fileName = args[0];
            files = getFiles(getPath(fileName));
            imgBox.SizeMode = PictureBoxSizeMode.StretchImage;

            for (int i = 0; i < files.Length; i++)
                if (files[i].Equals(fileName))
                {
                    currentImageIndex = i;
                    break;
                }

            loadImage();

            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
        }

        /// <summary>
        /// Cuts away the file name from an absolute path.
        /// </summary>
        /// <param name="input">The path to a file.</param>
        /// <returns>The method returns a string without the file name at the end.</returns>

        private string getPath(string input)
        {
            string[] pathArrTmp = input.Split('\\');
            string path = "";

            for (int i = 0; i < pathArrTmp.Length - 1; i++)
                path += pathArrTmp[i] + '\\';

            return path;
        }

        /// <summary>
        /// Searches an absolute path for image files.
        /// </summary>
        /// <param name="path">The absolute path to be searched.</param>
        /// <returns>The method returns a string array containing paths to all image files in the given directory.</returns>

        private string[] getFiles(string path)
        {
            if(!path.EndsWith("\\"))
                return null;

            string[] types = { ".jpg", ".jpeg", ".gif", ".png", ".bmp", ".tif" };
            string[] files = Directory.GetFiles(path).Where(x => types.Contains(Path.GetExtension(x).ToLower())).ToArray();
            return files;
        }

        /// <summary>
        /// Rescales a Size while preserving the aspect ratio.
        /// </summary>
        /// <param name="input">The initial Size that gets rescaled.</param>
        /// <param name="width">The new width to be applied to the Size.</param>
        /// <param name="height">The new height to be applied to the Size.</param>
        /// <returns>The method returns the rescaled Size.</returns>

        private Size Rescale(Size input, int width, int height)
        {
            Size newSize = new Size(width, height);

            double ratioX = (double)newSize.Width / (double)input.Width;
            double ratioY = (double)newSize.Height / (double)input.Height;

            double ratio = ratioX < ratioY ? ratioX : ratioY;

            int newWidth = Convert.ToInt32(input.Width * ratio);
            int newHeight = Convert.ToInt32(input.Height * ratio);


            return new Size(newWidth, newHeight);
        }

        /// <summary>
        /// Loads an Image from the 'files' string array.
        /// </summary>

        private void loadImage()
        {
            currentImage = Image.FromFile(files[currentImageIndex]);
            int sWidth = Screen.PrimaryScreen.Bounds.Width, sHeight = Screen.PrimaryScreen.Bounds.Height;

            if (currentImage.Width > sWidth || currentImage.Height > sHeight)
                imgBox.Size = Rescale(currentImage.Size, sWidth, sHeight);
            else
                imgBox.Size = currentImage.Size;

            this.Height = imgBox.Height;
            this.Width = imgBox.Width;
            imgBox.Image = currentImage;
            imgBox.Enabled = true;

            if (!lockPos)
                this.CenterToScreen();
        }

        /// <summary>
        /// Used to retrieve the color of a pixel of the current image at a specified point.
        /// </summary>
        /// <param name="input">The point to extract the color from</param>
        /// <returns>The method returns the color of the pixel at the given point. This Color may equal null.</returns>

        private Color? getPixelColorAt(Point input)
        {
            Color? color = null;

            PropertyInfo imageRectangleProperty = typeof(PictureBox).GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);

            Rectangle rectangle = (Rectangle)imageRectangleProperty.GetValue(imgBox, null);
            if (rectangle.Contains(new Point(input.X, input.Y)))
            {
                using (Bitmap copy = new Bitmap(imgBox.ClientSize.Width, imgBox.ClientSize.Height))
                {
                    using (Graphics g = Graphics.FromImage(copy))
                    {
                        g.DrawImage(imgBox.Image, rectangle);

                        color = copy.GetPixel(input.X, input.Y);
                    }
                }
            }

            return color;
        }

        ///////////////////////// Event handling methods start here /////////////////////////

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (shiftKeyDown)
            {
                if (this.Opacity <= 0.05)
                {
                    if (e.Delta > 0)
                        this.Opacity += 0.05;
                    return;
                }
                this.Opacity += e.Delta > 0 ? 0.05 : -0.05;
                return;
            }

            int newWidth = imgBox.Size.Width + ((e.Delta / 120) * (imgBox.Size.Width / 25));
            int newHeight = imgBox.Size.Height + ((e.Delta / 120) * (imgBox.Size.Height / 25));

            if ((e.Delta < 0 && (newHeight < 50 || newWidth < 50)) || (e.Delta > 0 && (newHeight > Screen.PrimaryScreen.Bounds.Height || newWidth > Screen.PrimaryScreen.Bounds.Width)))
                return;

            Size newSize = new Size(newWidth, newHeight);
            this.Location = new Point(this.Location.X + (this.Size.Width - newSize.Width) / 2, this.Location.Y + (this.Size.Height - newSize.Height) / 2);
            this.Size = newSize;
            imgBox.Size = newSize;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:                     //Left Arrow Key
                    if (e.Control)
                    {
                        if (!(this.Location.X > 5))
                            return;

                        Point tmp = this.Location;
                        tmp.X -= (e.Alt ? 1 : 10);
                        this.Location = tmp;
                        return;
                    }

                    if (currentImageIndex > 0)
                        currentImageIndex--;
                    else currentImageIndex = files.Length - 1;
                    loadImage();
                    break;
                case Keys.Right:                    //Right Arrow Key
                    if (e.Control)
                    {
                        if (!(this.Location.X < Screen.PrimaryScreen.Bounds.Width - this.Width))
                            return;

                        Point tmp = this.Location;
                        tmp.X += (e.Alt ? 1 : 10);
                        this.Location = tmp;
                        return;
                    }

                    if (currentImageIndex < files.Length - 1)
                        currentImageIndex++;
                    else currentImageIndex = 0;
                    loadImage();
                    break;
                case Keys.Up:                       //Up Arrow Key
                    if (e.Control)
                    {
                        if (!(this.Location.Y > 5))
                            return;

                        Point tmp = this.Location;
                        tmp.Y -= (e.Alt ? 1 : 10);
                        this.Location = tmp;
                        return;
                    }
                    break;
                case Keys.Down:                     //Down Arrow Key
                    if (e.Control)
                    {
                        if (!(this.Location.Y < Screen.PrimaryScreen.Bounds.Height - this.Height))
                            return;

                        Point tmp = this.Location;
                        tmp.Y += (e.Alt ? 1 : 10);
                        this.Location = tmp;
                        return;
                    }
                    break;
                case Keys.C:                        //C Key
                    if (!lockPos)
                        this.CenterToScreen();
                    break;
                case Keys.F:                        //F Key
                    fKeyDown = true;
                    break;
                case Keys.H:                        //H Key
                    if (fKeyDown)
                    {
                        if (files[currentImageIndex].EndsWith(".gif"))
                            break;

                        Image tmp = imgBox.Image;
                        tmp.RotateFlip(RotateFlipType.RotateNoneFlipX);

                        imgBox.Image = tmp;
                        return;
                    }

                    if (!e.Control)
                        break;
                    System.Windows.Forms.MessageBox.Show(
                    "CTRL + H - Display hotkey list\n" + 
                    "LMB - Drag window\n" + 
                    "RMB - Close window\n" + 
                    "Middle Mouse - Toogle always on top mode\n" + 
                    "Left/Right Arrow - Cycle images\n" + 
                    "CTRL + Arrow Keys - Nudge window\n" + 
                    "CTRL + Alt + Arrow Keys - Precision nudge\n" +
                    "Scroll wheel - Scale image\n" + 
                    "Shift + Scroll wheel - Change window opacity\n" + 
                    "C - Center window\n" + 
                    "O - Toggle opacity\n" +
                    "Shift + LMB - Set new transparency color" +
                    "Shift + R - Reset transparency color" + 
                    "R - Reset size\n" + 
                    "F + H - Flip horizontally\n" + 
                    "F + V - Flip vertically\n" + 
                    "L - Lock position\n",
                    "Hotkey List"
                    );
                    break;
                case Keys.L:                        //L Key
                    lockPos = !lockPos;
                    break;
                case Keys.O:                        //O Key
                    opacityToggle = !opacityToggle;
                    this.TransparencyKey = (opacityToggle ? BackColor : Color.Empty);
                    break;
                case Keys.P:                        //P Key
                    if (files[currentImageIndex].EndsWith(".gif"))
                        imgBox.Enabled = !imgBox.Enabled;
                    break;
                case Keys.R:                        //R Key
                    if (shiftKeyDown)
                    {
                        opacityToggle = false;
                        this.BackColor = SystemColors.Control;
                        this.TransparencyKey = Color.Empty;
                        break;
                    }
                    imgBox.Size = currentImage.Size;
                    this.Size = currentImage.Size;
                    break;
                case Keys.V:                        //V Key
                    if (fKeyDown)
                    {
                        if (files[currentImageIndex].EndsWith(".gif"))
                            break;

                        Image tmp = imgBox.Image;
                        tmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                        imgBox.Image = tmp;
                        return;
                    }
                    break;
                default:
                    break;
            };

            if (e.Shift)
                shiftKeyDown = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F:                        //F Key
                    fKeyDown = false;
                    break;
                default:
                    break;
            };

            if (e.Shift == false)
                shiftKeyDown = false;
        }

        private void imgBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (shiftKeyDown)
                {
                    Color? tmp = getPixelColorAt(e.Location);
                    if (tmp == null)
                        return;

                    if (((Color)tmp).A == 0)
                        return;

                    opacityToggle = true;
                    this.BackColor = (Color)tmp;
                    this.TransparencyKey = BackColor;
                    return;
                }
                
                isMoving = true;
                curPos = this.Location;
                mousePos = Cursor.Position;
            }
            else if (e.Button == MouseButtons.Middle)
                this.TopMost = !this.TopMost;
        }

        private void imgBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving && !lockPos)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(mousePos));
                this.Location = Point.Add(curPos, new Size(dif));
            }
        }

        private void imgBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isMoving = false;
            else if (e.Button == MouseButtons.Right)
                this.Close();
        }
    }

}