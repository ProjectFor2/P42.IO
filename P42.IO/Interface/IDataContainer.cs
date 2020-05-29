using System;
using System.Collections.Generic;

namespace P42.IO
{
    public interface IDataContainer
    {
        void Add<T>(T objectToAdd) where T : class;

        T Get<T>() where T : class;

        IEnumerable<object> Where(Func<object, bool> filter);

        void Remove<T>() where T : class;

        bool Contains<T>() where T : class;

        void Clear();
    }
}