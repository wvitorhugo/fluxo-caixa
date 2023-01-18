using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Shared.MessageBus
{
    public interface IMessageProducer
    {
        Task EnviaMensagem<T>(T message);
    }
}
