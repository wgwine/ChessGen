using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class StalemateException :Exception
    {
        public StalemateException():base()
        {

        }
    }
    public class CheckmateException : Exception
    {
        public CheckmateException():base()
        {

        }
    }
}
