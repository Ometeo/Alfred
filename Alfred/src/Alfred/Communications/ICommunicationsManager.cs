using System;
using System.Collections.Generic;
using System.Text;

namespace SuperBack.Communications
{
    public interface ICommunicationsManager
    {
        List<ICommunicationChannel> CommunicationChannels { get; set; }
    }
}
