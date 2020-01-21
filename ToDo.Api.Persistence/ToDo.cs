using System;
using System.Diagnostics.CodeAnalysis;

namespace ToDo.Api.Persistence
{
    [ExcludeFromCodeCoverage]
    public class ToDo : Entity
    {
        public string Content { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
