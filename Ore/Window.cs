using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Ore
{
    /// <summary>
    /// A window object to render to.
    /// </summary>
    public class Window
    {
        #region Private Variables
        // the internal SFML window object
        private RenderWindow window;

        // the internal Buffer objects
        private Buffer buffer;
        #endregion

        #region Getters/Setters
        /// <summary>
        /// Returns the SFML window used internally.
        /// </summary>
        public RenderWindow InternalWindow
        {
            get { return window; }
        }
        #endregion

        #region Event Delegates
        delegate void ClosingDelegate(object sender, EventArgs e);
        delegate void ResizingDelegate(object sender, SizeEventArgs e);
        #endregion

        #region Public Variables
        public event ClosingDelegate Closing;
        public event ResizingDelegate Resizing;
        #endregion

        /// <summary>
        /// Create a window.
        /// </summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="mode">The mode of the window.</param>
        public Window(string title, uint width, uint height, WindowMode mode = WindowMode.Normal)
        {
            window = new RenderWindow(
                new VideoMode(width, height),
                title,
                mode == WindowMode.Fullscreen ? Styles.Fullscreen : Styles.Default
                );
            this.window.Size = new Vector2u(width, height);
            buffer = new Buffer(width, height);
            // Events
            window.Closed += window_Closed;
            window.Resized += window_Resized;
        }

        /// <summary>
        /// Check whether the window is open.
        /// </summary>
        /// <returns>Returns true if the window is open, else it is not.</returns>
        public bool IsOpen()
        {
            return window.IsOpen();
        }

        #region Window Events
        void window_Closed(object sender, EventArgs e)
        {
            Closing(sender, e);
            window.Close();
        }

        void window_Resized(object sender, SizeEventArgs e)
        {
            Resizing(sender, e);
        }
        #endregion
    }
}
