using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore
{
    /// <summary>
    /// The core engine of Ore.
    /// </summary>
    public class Engine
    {
        private Window window;

        /// <summary>
        /// The current Window the engine is using.
        /// </summary>
        public Window Window
        {
            get { return window; }
            set { window = value; }
        }

        /// <summary>
        /// Create a new Engine object.
        /// </summary>
        public Engine()
        {

        }

        /// <summary>
        /// Poll events and render to the window.
        /// </summary>
        public void Render()
        {
            window.InternalWindow.DispatchEvents();
            window.InternalWindow.Clear();

            window.InternalWindow.Display();
        }
    }
}
