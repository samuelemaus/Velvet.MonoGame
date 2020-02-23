using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet.GameSystems
{
    public class SceneRenderer : IRenderer2D
    {
        public SceneRenderer(ContentManager content, RenderTarget2D renderTarget, Vector2 position = default)
        {
            Content = content;
            RenderTarget = renderTarget;
            RenderPosition = position;

            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, renderTarget.Bounds.Width, renderTarget.Bounds.Height);
            Bounds = new BoundingRect(bounds);
            Viewport = new Viewport(bounds);

            //Camera = new OrthoCamera(Viewport);
            DrawMethod = SimpleDraw;

            RendererInitialized = true;
        }



        public bool RendererInitialized { get; private set; }

        public OrthoCamera Camera { get; set; }
        public ContentManager Content { get; set; }
        public RenderTarget2D RenderTarget { get; set; }
        public Viewport Viewport;
        public BoundingRect Bounds { get; private set; }
        public SpriteBatch SpriteBatch { get; set; }
        public BlendState BlendState { get; set; } = BlendState.AlphaBlend;
        public Vector2 RenderPosition { get; set; } = Vector2.Zero;

        public float TargetScale => GameRenderer.ScreenResolution.Width / TargetDimensions.Width;

        public Dimensions2D TargetDimensions => new Dimensions2D(RenderTarget.Width, RenderTarget.Height);

        public List<Effect> SceneEffects { get; private set; } = new List<Effect>();

        public void LoadContent()
        {
            
        }

        public void UnloadContent()
        {
            
        }

        public void DrawSpriteBatch()
        {
            DrawMethod.Invoke();
        }

        private delegate void DrawSpriteBatchMethod();
        private DrawSpriteBatchMethod DrawMethod;

        private SamplerState SamplerState
        {
            get
            {
                if (Camera.ZoomIsInt && Camera.RotationIsNinetyDegreeInterval)
                {
                    return SamplerState.PointWrap;
                }

                else
                {
                    return SamplerState.PointClamp;
                }
            }
        }
        
        private void SimpleDraw()
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState, SamplerState, null, null, null, Camera.Transform);
            SceneController.CurrentScene.Draw(SpriteBatch);
            SpriteBatch.End();
        }

        private void DrawEffect()
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState, SamplerState, null, null, null, Camera.Transform);
            foreach(var effect in SceneEffects)
            {
                effect.CurrentTechnique.Passes[0].Apply();
            }
            SceneController.CurrentScene.Draw(SpriteBatch);
            SpriteBatch.End();

        }

        public void AddSceneEffect(Effect effect, bool activateOnAddition = true)
        {
            if (!SceneEffects.Contains(effect))
            {
                SceneEffects.Add(effect);

                if (activateOnAddition)
                {
                    DrawMethod = DrawEffect;
                }
            }

        }

        public void ActivateSceneEffects()
        {
            DrawMethod = DrawEffect;
        }

        public void ClearEffects()
        {
            SceneEffects.Clear();

            DrawMethod = SimpleDraw;
        }

        public override string ToString()
        {
            return $"{nameof(SceneRenderer)}: {this.GetString()}";
        }

    }
}
