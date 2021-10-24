using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.DI
{
    public class ServiceCollectionWrapper : IServiceCollection
    {
        protected ServiceCollection services;

        public ServiceCollectionWrapper()
        {
            services = new();
        }

        public virtual void InitDefault()
        {
            services = new();
            services.AddSingleton(GetType(), this);
        }

        public int Count => services.Count;

        public bool IsReadOnly => services.IsReadOnly;

        public ServiceDescriptor this[int index]
        {
            get => services[index];
            set => services[index] = value;
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return services.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            services.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            services.RemoveAt(index);
        }

        public void Add(ServiceDescriptor item)
        {
            ((ICollection<ServiceDescriptor>)services).Add(item);
        }

        public void Clear()
        {
            services.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return services.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            services.CopyTo(array, arrayIndex);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return services.Remove(item);
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return services.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return services.GetEnumerator();
        }
    }
}
