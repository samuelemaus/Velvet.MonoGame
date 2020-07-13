using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.GameSystems
{
    public abstract class CameraEffect : IUpdate
    {
        public OrthoCamera Camera { get; protected set; }
        public abstract void Update(GameTime gameTime);

        private bool effectActive;
        public bool EffectActive
        {
            get
            {
                return effectActive;
            }

            set
            {
                effectActive = value;

                if(value == true && !Camera.ActiveEffects.ContainsType(this.GetType()))
                {
                    Camera.ActiveEffects.Add(this);
                }

            }
        }



    }
}
