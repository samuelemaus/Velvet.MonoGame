using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.DataIO
{
    public interface ILoadableCollection<T> where T : class
    {
        T[] Collection { get; }




    }
}
