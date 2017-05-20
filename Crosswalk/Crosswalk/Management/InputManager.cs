using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crosswalk.Management
{
    public class InputManager
    {
        private Dictionary<Keys, EventHandler> KeyDownHandlers = new Dictionary<Keys, EventHandler>();
        private Dictionary<Keys, EventHandler> KeyUpHandlers = new Dictionary<Keys, EventHandler>();

        public InputManager()
        {
            Crossing.Instance.KeyDown += OnKeyDown;
            Crossing.Instance.KeyUp += OnKeyUp;
        }

        public void AddKeyDownHandler(Keys key, EventHandler handler)
        {
            if (!KeyDownHandlers.ContainsKey(key))
            {
                KeyDownHandlers.Add(key, delegate { });
            }
            KeyDownHandlers[key] += handler;
        }

        public void AddKeyUpHandler(Keys key, EventHandler handler)
        {
            if (!KeyUpHandlers.ContainsKey(key))
            {
                KeyUpHandlers.Add(key, delegate { });
            }
            KeyUpHandlers[key] += handler;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(KeyDownHandlers.ContainsKey(e.KeyCode))
                KeyDownHandlers[e.KeyCode](null, e);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUpHandlers.ContainsKey(e.KeyCode))
                KeyUpHandlers[e.KeyCode](null, e);
        }
    }
}
