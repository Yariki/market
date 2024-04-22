using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Shared.CorrelationId
{
    public interface ICorrelationIdService
    {
        string Get();

        void Set(string correlationId);

    }
}
