using Newtonsoft.Json;

namespace ApiTemplate.Commons
{
    public abstract class BaseEntity
    {

    }

    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        [JsonProperty(Order = -2)]
        public virtual T Id { get; set; }
    }
}
