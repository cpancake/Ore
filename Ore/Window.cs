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
        // the internal SFML window object
        private RenderWindow window;

        // the internal Buffer object
        private Buffer buffer;

        /// <summary>
        /// Create a window.
        /// </summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="mode">The mode of the window.</param>
        public Window(string title, int width, int height, WindowMode mode = WindowMode.Normal)
        {
            window = new RenderWindow(
                mode == WindowMode.Fullscreen ? VideoMode.FullscreenModes[0] : VideoMode.DesktopMode,
                title,
                mode == WindowMode.Fullscreen ? Styles.Fullscreen : Styles.Default
                );
            this.window.Size = new Vector2u((uint)width, (uint)height);
            buffer = new Buffer();
        }

        /// <summary>
        /// Create a window.
        /// </summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="buffer">The buffer to use internally.</param>
        /// <param name="mode">The mode of the window.</param>
        public Window(string title, int width, int height, ref Buffer buffer, WindowMode mode = WindowMode.Normal)
        {
            this.window = new RenderWindow(
                mode == WindowMode.Fullscreen ? VideoMode.FullscreenModes[0] : VideoMode.DesktopMode,
                title,
                mode == WindowMode.Fullscreen ? Styles.Fullscreen : Styles.Default
                );
            this.window.Size = new Vector2u((uint)width, (uint)height);
            this.buffer = buffer;
        }

        /// <summary>
        /// Open the window.
        /// </summary>
        public void Show()
        {
            window.Display();
        }
    }
}
