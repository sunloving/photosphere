﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.Exceptions;

namespace Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects
{
    internal class Registry : IRegistry
    {
        private readonly IDictionary<Type, IRegistration> _dictionary;

        public Registry()
        {
            _dictionary = new ConcurrentDictionary<Type, IRegistration>();
        }

        public void Add(IEnumerable<IRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                _dictionary.Add(registration.ServiceType, registration);
            }
        }

        public bool Contains(Type serviceType)
        {
            return _dictionary.ContainsKey(serviceType);
        }

        public IRegistration this[Type serviceType]
        {
            get
            {
                IRegistration result;
                if (_dictionary.TryGetValue(serviceType, out result))
                {
                    return result;
                }
                throw new TypeNotRegisteredException(serviceType);
            }
        }

        public int Count => _dictionary.Count;

        public IEnumerator<IRegistration> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}