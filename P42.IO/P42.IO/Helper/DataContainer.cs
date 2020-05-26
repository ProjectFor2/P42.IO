using System;
using System.Collections.Generic;
using System.Linq;

namespace P42.IO
{
    public class DataContainer : IDataContainer
    {
        private readonly Dictionary<Type, object> _objectHolder;

        public DataContainer(Dictionary<Type, object> objectHolder) => _objectHolder = objectHolder;

        public DataContainer() => new Dictionary<Type, object>();

        public virtual void Add<T>(T objectToAdd) where T : class => _objectHolder[typeof(T)] = objectToAdd;

        public virtual void Clear() => _objectHolder.Clear();

        public virtual bool Contains<T>() where T : class => _objectHolder.ContainsKey(typeof(T));

        public virtual T Get<T>() where T : class => (T)_objectHolder[typeof(T)];

        public virtual void Remove<T>() where T : class => _objectHolder.Remove(typeof(T));

        public virtual IEnumerable<object> Where(Func<object, bool> filter) => _objectHolder.Values.Where(filter);
    }
}
