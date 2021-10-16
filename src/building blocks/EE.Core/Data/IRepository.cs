using System;
using EE.Core.DomainObjects;

namespace EE.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        
    }
}