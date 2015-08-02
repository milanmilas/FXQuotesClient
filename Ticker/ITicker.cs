using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ticker
{
    public interface ITicker
    {
        IObservable<Quote> Quotes();
        void Connect();
        void Disconnect();
    }
}
