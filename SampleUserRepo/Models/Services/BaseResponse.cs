using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleUserRepo.Models.Services
{
    public abstract class BaseResponse<T>
    {
        public bool Success { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int Count { get; private set; }
        public List<ErrorMessage> Message { get; private set; } = new List<ErrorMessage>();
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public T Resource { get; private set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<T> Resources { get; private set; }

        public void SetCount(int count)
        {
            Count = count;
        }
        protected BaseResponse(T resource)
        {
            Success = true;
            Resource = resource;
        }

        protected BaseResponse(List<T> resource, int count = 0)
        {
            Success = true;
            Resources = resource;
            Count = count;
        }

        protected BaseResponse(ErrorMessage message)
        {
            Success = false;
            Message.Add(message);
            Resource = default;
        }

        protected BaseResponse(List<ErrorMessage> message)
        {
            Success = false;
            Message = message;
            Resource = default;
        }
    }
}
