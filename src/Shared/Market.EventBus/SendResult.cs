using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.EventBus;
public enum SendResult
{
    None,
    Acknowledged,
    RecoverableFailure,
    NoneRecoverableFailure
}
