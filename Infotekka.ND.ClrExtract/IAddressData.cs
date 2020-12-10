using System;
using System.Collections.Generic;
using System.Text;

namespace Infotekka.ND.ClrExtract
{
    public interface IAddressData
    {
        string Address1 { get; }

        string Address2 { get; }

        string State { get; }

        string City { get; }

        string Zip { get; }

        string Country { get; }
    }
}
