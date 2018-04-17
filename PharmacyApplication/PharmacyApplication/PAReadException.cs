using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApplication
{
    class PAReadException : Exception
    {
        public PAReadException(string message) : base(message)
        {

        }
    }
}
