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

        private OrthoCamera camera;
        public OrthoCamera Camera
        {
            get => camera;
            set
            {
                if (value == null)
                {
                    BeginDrawMethod = BeginDrawNoCamera;
                }

                else
                {
                    BeginDrawMethod = BeginDrawWithCameraTransform;
                }
                camera = value;
                
            }
        }
        public ContentManager Content { get; set; }
        public RenderTarget2D RenderTarget { get; set; }
        public Viewport Viewport;
        public BoundingRect Bounds { get; private set; }
        public SpriteBatch SpriteBatch { get; set; }
        public BlendState BlendState { get; set; } = BlendState.AlphaBlend;
        private SamplerState samplerState = SamplerState.PointWrap;
        public SamplerState SamplerState
        {
            get
            {
                //if (Camera.ZoomIsInt && Camera.RotationIsNinetyDegreeInterval)
                //{
                //    return SamplerState.PointWrap;
                //}

                //else
                //{
                //    return SamplerState.PointClamp;
                //}

                return samplerState;
            }

            set
            {
                samplerState = value;
            }
        }
        public Vector2 RenderPosition { get; set; } = Vector2.Zero;
        public SpriteSortMode SpriteSortMode { get; set; } = SpriteSortMode.FrontToBack;

        private RasterizerState rasterizerState = RasterizerState.CullNone;
        public RasterizerState RasterizerState
        {
            get
            {
                return rasterizerState;
            }

            set
            {
                rasterizerState = value;
            }
        }

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
        private DrawSpriteBatchMethod BeginDrawMethod;

       
        
        private void SimpleDraw()
        {
            BeginDrawMethod.Invoke();
            SceneController.CurrentScene.Draw(SpriteBatch);
            SpriteBatch.End();
        }

        private void DrawEffect()
        {
            BeginDrawMethod.Invoke();
            foreach(var effect in SceneEffects)
            {
                effect.CurrentTechnique.Passes[0].Apply();
            }
            SceneController.CurrentScene.Draw(SpriteBatch);
            SpriteBatch.End();

        }

        private void BeginDrawWithCameraTransform()
        {
            SpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, null, null, null, Camera.Transform);
        }

        private void BeginDrawNoCamera()
        {
            SpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, null, null, null, null);
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


        private List<BlendState> blendStates = new List<BlendState>()
        {
            BlendState.AlphaBlend, BlendState.NonPremultiplied, BlendState.NonPremultiplied, BlendState.Opaque
        };

        private List<SamplerState> samplerStates = new List<SamplerState>()
        {
            SamplerState.PointClamp, SamplerState.PointWrap, SamplerState.AnisotropicClamp, SamplerState.LinearClamp, SamplerState.LinearWrap
        };

        private List<RasterizerState> rasterizerStates = new List<RasterizerState>()
        {
            RasterizerState.CullNone, RasterizerState.CullCounterClockwise, RasterizerState.CullClockwise
        };

        public void ToggleBlendState(bool toggleBackwards = false)
        {
            int index = blendStates.IndexOf(BlendState);
            int increment = 1;
            if (toggleBackwards)
            {
                increment *= -1;
            }

            int next = ValueRange.Enforce(index + increment, new ValueRange(0, blendStates.Count - 1), true);
            BlendState = blendStates[next];
        }

        public void ToggleSamplerState(bool toggleBackwards = false)
        {
            int index = samplerStates.IndexOf(SamplerState);
            int increment = 1;
            if (toggleBackwards)
            {
                increment *= -1;
            }

            int next = ValueRange.Enforce(index + increment, new ValueRange(0, samplerStates.Count - 1), true);
            SamplerState = samplerStates[next];
        }

        public void ToggleRasterizerState(bool toggleBackwards = false)
        {
            int index = rasterizerStates.IndexOf(RasterizerState);
            int increment = 1;
            if (toggleBackwards)
            {
                increment *= -1;
            }

            int next = ValueRange.Enforce(index + increment, new ValueRange(0, rasterizerStates.Count - 1), true);
            RasterizerState = rasterizerStates[next];
        }

        public void ToggleSpriteSortMode(bool toggleBackwards = false)
        {
            int index = (int)SpriteSortMode;
            int increment = 1;
            if (toggleBackwards)
            {
                increment *= -1;
            }
            int next = ValueRange.Enforce(index + increment, new ValueRange(0, 4), true);
            SpriteSortMode = (SpriteSortMode)next;
        }

        public override string ToString()
        {
            return $"{nameof(SceneRenderer)}: {this.GetString()}";
        }

    }
}
