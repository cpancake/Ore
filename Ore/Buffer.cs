﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Ore
{
    /// <summary>
    /// Represents a buffer to be rendered.
    /// </summary>
    public class Buffer
    {
        // the internal Image objects of the front and back buffer
        private Image frontImage;
        private Image backImage;

        // the internal width and height of this buffer
        private int width;
        private int height;

        // SFML texture
        private Texture texture;

        // SFML sprite
        private Sprite sprite;

        // true = front, false = back
        private bool currentBuffer = false;

        /// <summary>
        /// The current width of the buffer.
        /// </summary>
        public int Width { get { return width; } }
        /// <summary>
        /// The current height of the buffer.
        /// </summary>
        public int Height { get { return height; } }

        /// <summary>
        /// The Image object representing the contents of this buffer.
        /// </summary>
        public Image Image
        {
            get { return currentBuffer ? frontImage : backImage; }
        }

        /// <summary>
        /// The Texture object representing the contents of this buffer.
        /// </summary>
        public Texture Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Get the Sprite object representing the contents of this buffer.
        /// </summary>
        public Sprite Sprite
        {
            get { return sprite; }
        }

        /// <summary>
        /// Create a new buffer.
        /// </summary>
        /// <param name="width">The width of the buffer.</param>
        /// <param name="height">The height of the buffer.</param>
        public Buffer(uint width, uint height)
        {
            frontImage = new Image(width, height);
            backImage = new Image(width, height);
            this.width = (int)width;
            this.height = (int)height;
            sprite = new Sprite();
            texture = new Texture(frontImage);
        }

        /// <summary>
        /// Swap the buffers.
        /// </summary>
        public void Swap()
        {
            // set the texture to the current buffer and switch the current buffer, then update the sprite
            texture.Update(currentBuffer ? frontImage : backImage);
            currentBuffer = !currentBuffer;
            sprite.Texture = texture;
        }

        /// <summary>
        /// Return the color at the specified position.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <returns>The color at that position.</returns>
        public Color this[uint x, uint y]
        {
            get
            {
                if (x < 0 || x >= width || y < 0 || y >= height) return Color.Black;
                return (currentBuffer ? frontImage.GetPixel(x, y) : backImage.GetPixel(x, y));
            }
            set
            {
                if (x < 0 || x >= width || y < 0 || y >= height) return;
                // draw to the opposite buffer
                if (!currentBuffer)
                    frontImage.SetPixel(x, y, value);
                else
                    backImage.SetPixel(x, y, value);
            }
        }
    }
}
