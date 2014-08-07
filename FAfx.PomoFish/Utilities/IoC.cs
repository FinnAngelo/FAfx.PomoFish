using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FAfx.Utilities
{
    internal enum ResolveFunc
    {
        AtRegister,
        AtFirstCall,
        EveryTime
    }

    [DebuggerStepThrough()]
    internal static class IoC
    {
        [DebuggerStepThrough()]
        //http://www.codeproject.com/Articles/23610/Dictionary-with-a-Custom-Key
        internal class IocKey
        {
            private string _Name;
            private Type _Type;
            
            [DebuggerHidden()]
            [DebuggerStepThrough()]
            internal IocKey(string name, Type type)
            {
                _Name = name ?? type.FullName;
                _Type = type;
            }
            
            [DebuggerStepThrough()]
            public class EqualityComparer : IEqualityComparer<IocKey>
            {
                [DebuggerHidden()]
                [DebuggerStepThrough()]
                public bool Equals(IocKey x, IocKey y)
                {
                    return x._Name == y._Name && x._Type == y._Type;
                }
                
                [DebuggerHidden()]
                [DebuggerStepThrough()]
                public int GetHashCode(IocKey x)
                {
                    return x._Type.GetHashCode() ^ x._Name.GetHashCode();
                }
            }
        }

        [DebuggerStepThrough()]
        internal class IocValue
        {
            internal object IocObject { get; private set; }
            internal ResolveFunc ResolveFunc { get; private set; }
            internal IocValue(object iocObject, ResolveFunc resolveFunc = ResolveFunc.AtFirstCall)
            {
                this.IocObject = iocObject;
                this.ResolveFunc = resolveFunc;
            }
        }

        internal static Dictionary<IocKey, IocValue> _registered = new Dictionary<IocKey, IocValue>(new IocKey.EqualityComparer());

        [DebuggerHidden()]
        [DebuggerStepThrough()]
        internal static void Clear()
        {
            _registered.Clear();
        }
        
        [DebuggerHidden()]
        [DebuggerStepThrough()]
        internal static void Register<T>(T toRegister)
        {
            Register<T>(null, toRegister);
        }
        
        [DebuggerHidden()]
        [DebuggerStepThrough()]
        internal static void Register<T>(string name, T toRegister)
        {
            var key = new IocKey(name, typeof(T));
            if (_registered.ContainsKey(key)) _registered.Remove(key);
            _registered.Add(key, new IocValue(toRegister, ResolveFunc.AtFirstCall));
        }

        [DebuggerHidden()]
        [DebuggerStepThrough()]
        internal static void Register<T>(Func<T> toRegister = default(Func<T>), ResolveFunc resolveFunc = ResolveFunc.AtFirstCall)
        {
            Register<T>(null, toRegister, resolveFunc);
        }

        [DebuggerHidden()]
        [DebuggerStepThrough()]
        internal static void Register<T>(string name, Func<T> toRegister, ResolveFunc resolveFunc = ResolveFunc.AtFirstCall)
        {
            var key = new IocKey(name, typeof(T));
            if (_registered.ContainsKey(key)) _registered.Remove(key);

            IocValue val = null;
            if(resolveFunc == ResolveFunc.AtRegister)
            {
                val = new IocValue(toRegister.Invoke(), resolveFunc);
            } 
            else
            {
                val = new IocValue(toRegister, resolveFunc);
            };

            _registered.Add(key, val);
        }

        [DebuggerHidden()]
        [DebuggerStepThrough()]
        internal static T Resolve<T>(string name = null)
        {
            var key = new IocKey(name, typeof(T));
            var regValue = _registered[key];

            if (regValue.IocObject is T)
            {
                return (T)regValue.IocObject;
            }
            else if (regValue.IocObject is Func<T>)
            {
                var newResult = ((Func<T>)regValue.IocObject).Invoke();
                if (regValue.ResolveFunc == ResolveFunc.AtFirstCall)
                {
                    Register<T>(name, newResult);
                }
                return newResult;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}