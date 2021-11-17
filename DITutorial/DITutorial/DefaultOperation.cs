using DITutorial.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Guid;

namespace DITutorial
{
    class DefaultOperation : ITransientOperation, IScopedOperation, ISingletonOperation
    {
        public string OperationId { get; } = NewGuid().ToString()[^4..];
    }
}
