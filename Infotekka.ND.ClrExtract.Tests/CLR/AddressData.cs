using System;
using System.Collections.Generic;
using System.Text;

namespace Infotekka.ND.ClrExtract.Tests.CLR
{
    class AddressData : IAddressData
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }
    }
}
