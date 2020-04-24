using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velvet.Input;

namespace Velvet.UI
{
    public abstract class Menu : IContentManager
    {
        #region//Content
        public IMenuObject FocusedObject { get; protected set; }
        public List<IMenuObject> MenuObjects { get; protected set; } = new List<IMenuObject>();
        public List<IMenuObject> ActiveMenuObjects { get
            {
                //var returnList = new List<IMenuObject>();

                //foreach(var obj in MenuObjects)
                //{
                //    if (obj.IsActive)
                //    {
                //        returnList.Add(obj);
                //    }
                //}

                var returnList = MenuObjects;

                foreach(var obj in returnList)
                {
                    if (!obj.IsActive)
                    {
                        returnList.Remove(obj);
                    }
                }

                return returnList;
            }
        }
        //TODO: pre-processor directive for VelvetInput
        public InputManager Input { get; set; } = InputManager.CreateInputManager();
        public ContentManager Content { get; set; }
        public string RootDirectory { get; set; }

        protected void InitializeContent()
        {
            Content = new ContentManager(UIController.Content.ServiceProvider, RootDirectory);
        }

        //protected void ArrangeAsList(IBoundingRect[] boundingRects, IBoundingRect target, Alignment alignment, TextAlignment alignment = TextAlignment.Left)
        //{
        //    float offset = 5;

        //    boundingRects[0].AnchorTo(target, alignment, RectRelativity.Inside, offset);

        //    for (int i = 1; i < boundingRects.Count(); i++)
        //    {
        //        //if (boundingRects[i] is TextImage t)
        //        //{
        //        //    t.Alignment = alignment;
        //        //}

        //        boundingRects[i].AnchorTo(boundingRects[i - 1], Alignment.BottomCentered, RectRelativity.Outside, offset);
        //    }

            
        //}

        //protected void ArrangeAsList(IBoundingRect[] boundingRects, BoundingRect target, Alignment alignment, TextAlignment alignment = TextAlignment.Left)
        //{
        //    float offset = 5;

        //    boundingRects[0].AnchorTo(target, alignment, RectRelativity.Inside, offset);

        //    for (int i = 1; i < boundingRects.Count(); i++)
        //    {
        //        //if(boundingRects[i] is TextImage t)
        //        //{
        //        //    t.Alignment = alignment;
        //        //} 
        //        boundingRects[i].AnchorTo(boundingRects[i - 1], Alignment.BottomCentered, RectRelativity.Outside, offset);
        //    }
        //}

        #endregion

        #region//XNA Methods

        public virtual void LoadContent()
        {
            
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {   
            Input.Update(gameTime);

            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        #endregion

        #region//Base Menu Methods

        public virtual void InitializeMenu()
        {
            
        }

        public virtual void ActivateMenuControls(GameTime gameTime)
        {

        }

        #endregion

    }
}
