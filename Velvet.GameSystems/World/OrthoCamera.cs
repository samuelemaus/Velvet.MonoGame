using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velvet.Rendering;

namespace Velvet.GameSystems
{
    public class OrthoCamera : IOrthoCamera
    {
        public OrthoCamera(Viewport view)
        {
            Viewport = view;
        }

        public Matrix Transform { get; set; }
        public Viewport Viewport { get; set; }
        public Vector2 Center { get; protected set; }
        public BoundingRect BoundingRect => new BoundingRect(Viewport.Bounds);



        public float Rotation { get; set; }

        public Vector2 Scale { get; set; }

        private float zoom;
        public float Zoom { get { return zoom; }
            set
            {
                zoom = ValueRange.Enforce(value, ZoomRange);
            }
        }
        public ValueRange ZoomRange { get; set; }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
