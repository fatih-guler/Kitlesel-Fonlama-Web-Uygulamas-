using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Entities.Messages
{
    public enum MessageErrorCode
    {
        UserIsNotFound= 101,
        UserAlreadyExists = 102,
        EmailIsAlreadyExits = 103,
        InvalidRequest=104,
        AnErrorOccur = 105,
        RewardCouldntAdd = 106
    }
}
