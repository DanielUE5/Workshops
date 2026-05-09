using DI.Attributes;
using DI.Modules;
using System.Reflection;

namespace DI.Injectors
{
    public class Injector
    {
        private IModule module;

        public Injector(IModule module)
        {
            this.module = module;
        }

        public TClass? Inject<TClass>()
        {
            bool hasConstructorAttribute = CheckForConstructorInjection<TClass>();
            bool hasFieldAttribute = CheckForFieldInjection<TClass>();

            if (hasConstructorAttribute && hasFieldAttribute)
            {
                throw new ArgumentException("There must be only field or constructor annotated with Inject attribute");
            }

            if (hasConstructorAttribute)
            {
                return CreateConstructorInjection<TClass>();
            }
            else if (hasFieldAttribute)
            {
                return CreateFieldInjection<TClass>();
            }

            return default;
        }

        private TClass? CreateConstructorInjection<TClass>()
        {
            Type desireClass = typeof(TClass);
            if (desireClass == null) return default;
            ConstructorInfo[] constructors = desireClass.GetConstructors();
            foreach (ConstructorInfo constructor in constructors)
            {
                if (!constructor.GetCustomAttributes(typeof(Inject), true).Any()) continue;

                Inject? inject = constructor
                    .GetCustomAttributes(typeof(Inject), true)
                    .FirstOrDefault() as Inject;
                ParameterInfo[] parameterTypes = constructor.GetParameters();
                object[] objArr = new object[parameterTypes.Length];
                int i = 0;

                foreach (ParameterInfo parameterType in parameterTypes)
                {
                    Attribute? qualifier = parameterType.GetCustomAttribute(typeof(Named));
                    Type? dependency;

                    if (qualifier == null)
                    {
                        dependency = module.GetMapping(parameterType.ParameterType, inject);
                    }
                    else
                    {
                        dependency = module.GetMapping(parameterType.ParameterType, qualifier);
                    }



                    GetType().GetMethod(nameof(Inject))
                        .MakeGenericMethod(dependency)
                        .Invoke(this, null);

                    if (parameterType.ParameterType.IsAssignableFrom(dependency))
                    {

                        object instance = module.GetInstance(dependency);
                        if (instance != null)
                        {
                            objArr[i++] = instance;
                        }
                        else
                        {
                            instance = Activator.CreateInstance(dependency);
                            objArr[i++] = instance;
                            module.SetInstance(parameterType.ParameterType, instance);
                        }
                    }
                }

                return (TClass)Activator.CreateInstance(desireClass, objArr);
            }

            return default;
        }

        private TClass CreateFieldInjection<TClass>()
        {
            Type? desireClass = typeof(TClass);
            object? desireClassInstance = module.GetInstance(desireClass);

            if (desireClassInstance == null)
            {
                desireClassInstance = Activator.CreateInstance(desireClass);
                module.SetInstance(desireClass, desireClassInstance);
            }

            foreach (FieldInfo field in desireClass.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                if (field.GetCustomAttributes(typeof(Inject), true).Any())
                {
                    Inject? injection = field.GetCustomAttributes(typeof(Inject), true).FirstOrDefault() as Inject;
                    Type? dependency;

                    Attribute? qualifier = field.GetCustomAttribute(typeof(Named), true);
                    Type? type = field.FieldType;
                    if (qualifier == null)
                    {
                        dependency = module.GetMapping(type, injection);
                    }
                    else
                    {
                        dependency = module.GetMapping(type, qualifier);
                    }
                    if (type.IsAssignableFrom(dependency))
                    {
                        object instance = module.GetInstance(dependency);
                        if (instance == null)
                        {
                            instance = Activator.CreateInstance(dependency);
                            module.SetInstance(dependency, instance);
                        }

                        field.SetValue(desireClassInstance, instance);
                    }



                }
            }

            return (TClass)desireClassInstance;
        }

        private bool CheckForFieldInjection<TClass>()
        {
            return typeof(TClass)
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Any(field => field.GetCustomAttributes(typeof(Inject), true).Any());
        }

        private bool CheckForConstructorInjection<TClass>()
        {
            return typeof(TClass).GetConstructors().Any(
                constructor => constructor.GetCustomAttributes(typeof(Inject), true).Any());
        }
    }
}
