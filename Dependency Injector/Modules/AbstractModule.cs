using DI.Attributes;

namespace DI.Modules
{
    public abstract class AbstractModule : IModule
    {
        private IDictionary<Type, Dictionary<string, Type>> implementations;

        private IDictionary<Type, object> instances;


        protected AbstractModule()
        {
            implementations = new Dictionary<Type, Dictionary<string, Type>>();
            instances = new Dictionary<Type, object>();
        }

        public abstract void Configure();

        public Type GetMapping(Type someClass, object attribute)
        {
            IDictionary<string, Type> impl = implementations[someClass];

            Type? type = null;

            if (attribute is Inject)
            {
                if (impl.Count == 1)
                {
                    type = impl.Values.First();
                }
                else
                {
                    throw new ArgumentException("No available mapping for class: " + someClass.FullName);
                }
            }
            else if (attribute is Named)
            {
                Named? qualifier = attribute as Named;

                string dependencyName = qualifier?.Name ?? throw new ArgumentException("Named attribute must have a name");
                return impl[dependencyName];
            }



            return type;
        }

        public object GetInstance(Type parameter)
        {
            instances.TryGetValue(parameter, out object? value);
            return value;
        }

        public void SetInstance(Type implementation, object instance)
        {
            if (!instances.ContainsKey(implementation))
            {
                instances.Add(implementation, instance);
            }
        }

        protected void CreateMapping<TInter, TImpl>()
        {
            if (!implementations.ContainsKey(typeof(TInter)))
            {
                implementations[typeof(TInter)] = new Dictionary<string, Type>();
            }

            implementations[typeof(TInter)].Add(typeof(TImpl).Name, typeof(TImpl));
        }
    }
}