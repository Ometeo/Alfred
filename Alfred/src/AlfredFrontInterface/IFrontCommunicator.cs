using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfredFrontInterface
{
    public interface IFrontCommunicator<T>
    {
        void Send(T message);
    }
}
