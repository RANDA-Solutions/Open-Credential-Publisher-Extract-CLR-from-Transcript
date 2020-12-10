using System;
using System.Collections.Generic;
using System.Text;

namespace Infotekka.ND.ClrExtract
{
    public interface IIdentityData
    {
        string Identifier { get; }

        string IdentificationSystem { get; }
    }
}
