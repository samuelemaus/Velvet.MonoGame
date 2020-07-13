using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public class Renderer2D : IRenderer2D
    {
        public virtual RenderTarget2D RenderTarget { get; set; }
        public virtual SpriteBatch SpriteBatch { get; set; }
        public virtual BlendState BlendState { get; set; } = BlendState.AlphaBlend;
        public virtual SamplerState SamplerState { get; set; } = SamplerState.PointWrap;
        public virtual RasterizerState RasterizerState { get; set; }
        public virtual SpriteSortMode SpriteSortMode { get; set; }

        public float RenderTargetAspectRatio => RenderTarget.Width / RenderTarget.Height;

        public virtual Vector2 RenderPosition { get; set; }

        public Dimensions2D TargetDimensions => new Dimensions2D(RenderTarget.Width, RenderTarget.Height);
        public BoundingRect Bounds => new BoundingRect(RenderTarget.Bounds);
        public float TargetScale => GameRenderer.Instance.OutputResolution.Width / TargetDimensions.Width;

        public bool RendererInitialized { get; protected set; }


        protected delegate void DrawSpriteBatchMethod();
        protected DrawSpriteBatchMethod DrawMethod { get; set; }
        protected DrawSpriteBatchMethod BeginDrawMethod { get; set; }

        public void DrawSpriteBatch()
        {
            DrawMethod.Invoke();
        }

        public void DrawToRenderTarget(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(RenderTarget);
            graphicsDevice.Clear(Color.Transparent);
            DrawSpriteBatch();
            graphicsDevice.SetRenderTarget(null);

        }

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
            
        }

        public virtual void OnInternalResolutionChanged(object sender, EventArgs e)
        {
            this.RenderTarget = new RenderTarget2D(GameRenderer.Instance.GraphicsDevice, (int)GameRenderer.Instance.InternalResolution.Width, (int)GameRenderer.Instance.InternalResolution.Height);
        }

    }
}
