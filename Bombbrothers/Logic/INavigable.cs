using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombbrothers.Logic
{
    /// <summary>
    /// Interfejs dla klas które mogą służyć jako strony zmieniane dynamicznie (zawartość okna)
    /// </summary>
    public interface INavigable
    {
        void UtilizeState(object state);
    }
}
