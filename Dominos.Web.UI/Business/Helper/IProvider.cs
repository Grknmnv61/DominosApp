using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominos.Web.UI.Business.Helper
{
    public interface IProvider<T>
    {
        void Execute(T model);
    }
}