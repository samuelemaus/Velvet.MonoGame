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

namespace Velvet
{
    public delegate void SendValuePropertyChangedEventHandler(object sender, object _value, PropertyChangedEventArgs e);

    /// <summary>
    /// Base Class for all Game Data; necessary for anything which needs to interact with UI.
    /// </summary>
    public abstract class GameData
    {
        #region//Property Data
        private PropertyInfo IdentifyProperty(string propertyName)
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
            Type t = this.GetType();

            return t.GetProperties();

        }

        #endregion

        #region//Property Changed Notification Logic


        #region//Sending Property Changes to Data
        public event SendValuePropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object _value, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, _value, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
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
        public void OnPropertyUpdatedExternally(object sender, object _value, PropertyChangedEventArgs e)
        {
            if (PropertyUpdateValidated(sender))
            {
                PropertyInfo property = IdentifyProperty(e.PropertyName);

                if (property != null)
                {
                    property.SetValue(this, _value);
                }
            }

        }
        private bool PropertyUpdateValidated(object _sender)
        {
            bool value = false;

            bool isUIData = _sender is UIData;


            UIData sender = _sender as UIData;

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
        private bool SenderValidated(UIData sender)
        {
            bool value = false;

            foreach(var data in ValidatedUIBindings)
            {
                if (data == sender)
                {
                    value = true;
                }
            }

            return value;
        }
        private bool SubscriptionValidated(UIData sender)
        {
            bool value = false;

            //SendValuePropertyChangedEventHandler e = typeof(UIData).GetField(nameof(UIData.SourceValueUpdateSent)).GetValue(sender) as SendValuePropertyChangedEventHandler;


            if (typeof(UIData).GetField(nameof(UIData.SourceValueUpdateSent), BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sender) is SendValuePropertyChangedEventHandler e)
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
        private List<UIData> ValidatedUIBindings { get; set; } = new List<UIData>();
        #endregion


        //__Sample Property__//

        //private string name;
        //public string Name
        //{
        //    get => name;
        //    set => SetField(ref name, value);
        //}

        #endregion
    }

}
