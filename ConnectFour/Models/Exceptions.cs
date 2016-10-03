using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class DataCorruptException : Exception
    {
        public DataCorruptException()
        {
        }

        public DataCorruptException(string message) : base(message)
        {
        }

        public DataCorruptException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
