using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotekka.ND.ClrExtract
{
    public interface IAwardData
    {
        string Name { get; }

        string Description { get; }

        string Image { get; }

        DateTime Awarded { get; }
    }
}
