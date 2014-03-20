using System;
using System.Collections.Generic;
using Microsoft.AspNet.DependencyInjection;
using Microsoft.AspNet.DependencyInjection.Fallback;

namespace Microsoft.AspNet.Identity.Test
{
    public static class TestServices
    {
        public static IServiceProvider DefaultServiceProvider<TUser, TKey>()
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            var serviceCollection = new ServiceCollection {DefaultServices<TUser, TKey>()};
            return serviceCollection.BuildServiceProvider();
        }

        public static IEnumerable<IServiceDescriptor> DefaultServices<TUser, TKey>()
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return new IServiceDescriptor[]
            {
                new ServiceDescriptor<IPasswordValidator, PasswordValidator>(),
                new ServiceDescriptor<IUserValidator<TUser, TKey>, UserValidator<TUser, TKey>>(),
                new ServiceDescriptor<IPasswordHasher, PasswordHasher>(),
                new ServiceDescriptor<IClaimsIdentityFactory<TUser, TKey>, ClaimsIdentityFactory<TUser, TKey>>(),
                new ServiceDescriptor<IUserStore<TUser, TKey>, NoopUserStore>()
            };
        }

        public class ServiceDescriptor<TService, TImplementation> : IServiceDescriptor
        {
            public ServiceDescriptor(LifecycleKind lifecycle = LifecycleKind.Transient)
            {
                Lifecycle = lifecycle;
            }

            public LifecycleKind Lifecycle { get; private set; }

            public Type ServiceType
            {
                get { return typeof (TService); }
            }

            public Type ImplementationType
            {
                get { return typeof (TImplementation); }
            }

            public object ImplementationInstance
            {
                get { return null; }
            }
        }
    }
}