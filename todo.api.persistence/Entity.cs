using System;
namespace todo.api.persistence
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
