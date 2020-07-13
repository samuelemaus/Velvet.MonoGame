using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Velvet
{
    public interface IRenderer2D
    {        
        RenderTarget2D RenderTarget { get; set; }
        SpriteBatch SpriteBatch { get; set; }
        BlendState BlendState { get; set; }
        SamplerState SamplerState { get; set; }
        RasterizerState RasterizerState { get; set; }
        SpriteSortMode SpriteSortMode { get; set; }
        Vector2 RenderPosition { get; }
        Dimensions2D TargetDimensions { get; }
        float TargetScale { get; }
        bool RendererInitialized { get; }
        void LoadContent();
        void UnloadContent();
        void DrawSpriteBatch();
        void DrawToRenderTarget(GraphicsDevice graphicsDevice);

        void OnInternalResolutionChanged(object sender, EventArgs e);

    }
}
