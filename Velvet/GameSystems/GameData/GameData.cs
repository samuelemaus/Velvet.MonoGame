using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using Velvet;

namespace Velvet.GameSystems
{
    //SetField method derived from top answer found here: https://stackoverflow.com/questions/1315621/implementing-inotifypropertychanged-does-a-better-way-exist

    
    /// <summary>
    /// 
    /// </summary>
    public abstract class GameData : IBindingSource
    {
        #region//Property Data
        public PropertyInfo IdentifyProperty(string propertyName)
        {
            PropertyInfo property = null;

            foreach (PropertyInfo info in this.GetProperties())
            {
                if (propertyName == info.Name)
                {
                    property = info;
                }

            }

            return property;
        }
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }

        //public PropertyInfo this[string propertyName]
        //{
        //    get
        //    {
        //        PropertyInfo property = null;

        //        foreach (PropertyInfo info in this.GetProperties())
        //        {
        //            if (propertyName == info.Name)
        //            {
        //                property = info;
        //            }

        //        }

        //        return property;
        //    }
        //}

        #endregion

        #region//Property Changed Notification Logic


        #region//Sending Property Changes to Data
        public event SendValuePropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object value, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, value, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetFieldAndNotify<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            else
            {
                field = value;
                OnPropertyChanged(value, propertyName);
                return true;
            }

        }

        #endregion

        #region//Receiving PropertyChanges from Data
        public void OnPropertySetByControl(object sender, object value, PropertyChangedEventArgs e)
        {
            if (PropertyUpdateValidated(sender))
            {
                PropertyInfo property = IdentifyProperty(e.PropertyName);

                if (property != null)
                {
                    property.SetValue(this, value);
                }
            }

        }
        private bool PropertyUpdateValidated(object _sender)
        {
            bool value = false;

            bool isUIData = _sender is IControlValueSender;


            IControlValueSender sender = _sender as IControlValueSender;

            if (SenderValidated(sender))
            {
                value = true;
            }

            else if (!isUIData)
            {
                value = false;
            }

            else if (SubscriptionValidated(sender))
            {
                value = true;
            }


            return value;
        }
        private bool SenderValidated(IControlValueSender sender)
        {
            bool value = false;

            foreach(var data in ValidatedBindings)
            {
                if (data == sender)
                {
                    value = true;
                }
            }

            return value;
        }
        private bool SubscriptionValidated(IControlValueSender sender)
        {
            bool value = false;

            //SendValuePropertyChangedEventHandler e = typeof(UIData).GetField(nameof(UIData.SourceValueUpdateSent)).GetValue(sender) as SendValuePropertyChangedEventHandler;


            if (typeof(IControlValueSender).GetField(nameof(IControlValueSender.SourceValueUpdateSent), BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sender) is SendValuePropertyChangedEventHandler e)
            {
                Delegate[] subscribers = e.GetInvocationList();

                foreach (var subscriber in subscribers)
                {
                    if (subscriber.Target == this)
                    {
                        value = true;
                    }
                }

            }

            return value;
        }
        private List<IControlValueSender> ValidatedBindings { get; set; } = new List<IControlValueSender>();
        #endregion

        //__Sample Property__//

        //private string name;
        //public string Name
        //{
        //    get => name;
        //    set => SetField(ref name, value);
        //}

        private void blapo()
        {
            PropertyInfo prop = null;

            MethodInfo setMethod = prop.GetSetMethod();

            


        }

        #endregion



    }
}
