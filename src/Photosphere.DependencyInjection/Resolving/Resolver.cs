﻿using System;
using System.Linq;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Resolving
{
    internal class Resolver : IResolver
    {
        private readonly IRegistry _registry;
        private readonly IScopeKeeper _scopeKeeper;

        public Resolver(IRegistry registry, IScopeKeeper scopeKeeper)
        {
            _registry = registry;
            _scopeKeeper = scopeKeeper;
        }

        public TService GetInstance<TService>()
        {
            var registration = _registry[typeof(TService)];
            var instantiateFunction = (Func<object[], TService>) registration.InstantiateFunction;
            var instances = _scopeKeeper.PerContainerScope.AvailableInstances.ToArray();
            return instantiateFunction.Invoke(instances);
        }
    }
}