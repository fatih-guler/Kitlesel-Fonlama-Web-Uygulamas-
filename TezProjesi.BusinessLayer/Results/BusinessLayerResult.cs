using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TezProjesi.Entities.Messages;

namespace TezProjesi.BusinessLayer.Results
{
    public class BusinessLayerResult<T> where T : class
    {
        
        public T  Result { get; set; }
        public List<MessageObj> Errors { get; set; }
        public BusinessLayerResult()
        {
            Errors = new List<MessageObj>();
        }
        public void AddError(MessageErrorCode code , string ErrorMessage)
        {
            Errors.Add(new MessageObj() { Description = ErrorMessage, MessageCode = code });
        }
    }
}
