using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IDbManager<T>
    {
        IReadOnlyList<T> HaalOp();
        IReadOnlyList<T> HaalOp(Func<T, bool> predicate);
        void VoegToe(T item);
        void Verwijder(T item);
        T HaalOp(long id);
    }
}
