using System;
using System.Diagnostics.CodeAnalysis;

namespace ToDo.Api.Persistence
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
