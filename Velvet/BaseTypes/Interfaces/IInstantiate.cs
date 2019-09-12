using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public interface IInstantiate<T> 
    {
        T Instantiate(params object[] args);



    }
}
