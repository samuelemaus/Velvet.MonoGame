using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Velvet.EntityComponentSystem;

namespace Velvet.GameSystems
{
    public abstract class CameraEffect : UpdateableComponent
    {
        public OrthoCamera Camera { get; protected set; }

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
