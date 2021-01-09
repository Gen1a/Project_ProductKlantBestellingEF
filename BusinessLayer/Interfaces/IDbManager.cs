using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IDbManager<T>
    {
        IReadOnlyList<T> HaalOp();
        IReadOnlyList<T> HaalOp(Func<T, bool> predicate);
        T HaalOp(long id);
        void VoegToe(T item);
        void Verwijder(T item);
        
    }
}
