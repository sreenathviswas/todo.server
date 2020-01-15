using System;
namespace ToDo.Api.Persistence
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
