using System;
using System.Collections.Generic;
using System.Threading;

namespace EGAZT.Helper
{
    public class StateManager 
    {

        private Dictionary<string, object> _stateProvider = new Dictionary<string, object>();

        public object GetItem(string key)
        {
            _stateProvider.TryGetValue(key, out object data);
            return data;
        }

        public void SetItem(string key, object data)
        {
            if (_stateProvider.ContainsKey(key))
                _stateProvider.Remove(key);
            _stateProvider.Add(key, data);
        }

        public void DeleteItem(string key)
        {
            _stateProvider.Remove(key);
        }

        public void DeleteAll()
        {
            _stateProvider.Clear();
        }

    }

    ///// <summary>
    /////     Class that can be derived from to create a singleton.
    ///// </summary>
    ///// <typeparam name="T">Any type inherited from Singleton&lt;T&gt;></typeparam>
    //public abstract class Singleton<T> where T : Singleton<T>
    //{
    //    private static readonly Lazy<T> Lazy = new Lazy<T>(() => (T)Activator.CreateInstance(typeof(T)),
    //        LazyThreadSafetyMode.ExecutionAndPublication);

    //    /// <summary>
    //    ///     Lazily created instance property.
    //    /// </summary>
    //    public static T Instance => Lazy.Value;
    //}
}

