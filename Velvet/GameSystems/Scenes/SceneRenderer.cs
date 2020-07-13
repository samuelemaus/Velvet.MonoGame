using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet.GameSystems
{
    public class SceneRenderer : Renderer2D
    {
        public SceneRenderer(ContentManager content, RenderTarget2D renderTarget, Vector2 position = default)
        {
            Content = content;
            RenderTarget = renderTarget;
            RenderPosition = position;
            UpdateViewport();
            DrawMethod = SimpleDraw;
            RendererInitialized = true;

            GameRenderer.Instance.AddRenderer(this);
        }


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

        private Viewport viewport;
        public Viewport Viewport => viewport;

        protected void UpdateViewport()
        {
            viewport = new Viewport(GetBounds);
        }

        public override Vector2 RenderPosition { get => base.RenderPosition + (GameRenderer.Instance.RenderPosition); set => base.RenderPosition = value; }

        protected virtual Rectangle GetBounds => new Rectangle((int)RenderPosition.X, (int)RenderPosition.Y, RenderTarget.Bounds.Width, RenderTarget.Bounds.Height);

        public List<Effect> SceneEffects { get; private set; } = new List<Effect>();



        private void SimpleDraw()
        {
            BeginDrawMethod.Invoke();
            SceneController.Instance.CurrentScene.Draw(SpriteBatch);
            SpriteBatch.End();
        }

        private void DrawEffect()
        {
            BeginDrawMethod.Invoke();
            foreach(var effect in SceneEffects)
            {
                effect.CurrentTechnique.Passes[0].Apply();
            }
            SceneController.Instance.CurrentScene.Draw(SpriteBatch);
            SpriteBatch.End();

        }

        private void BeginDrawWithCameraTransform()
        {
            SpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, null, RasterizerState, null, Camera.Transform);
        }

        private void BeginDrawNoCamera()
        {
            SpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, null, RasterizerState, null, null);
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

        public override void OnInternalResolutionChanged(object sender, EventArgs e)
        {
            base.OnInternalResolutionChanged(sender, e);
            UpdateViewport();
            Camera.Viewport = Viewport;
        }

        #region//DebugSettings
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

        #endregion

    }
}
