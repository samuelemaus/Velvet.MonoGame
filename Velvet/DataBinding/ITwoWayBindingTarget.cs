using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public interface ITwoWayBindingTarget : IBindingTarget
    {
        void SendValueUpdateToDataSource(object _value);
        event SendValuePropertyChangedEventHandler SourceValueUpdateSent;



    }
}
