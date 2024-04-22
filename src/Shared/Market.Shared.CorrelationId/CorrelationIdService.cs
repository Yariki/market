using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Shared.CorrelationId;
public class CorrelationIdService : ICorrelationIdService
{
    private string _correlationId = Guid.NewGuid().ToString("D");

    public string Get() => _correlationId;

    public void Set(string correlationId) => _correlationId = correlationId;
}
