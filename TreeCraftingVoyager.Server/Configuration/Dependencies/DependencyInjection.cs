using TreeCraftingVoyager.Server.Attributes;
using TreeCraftingVoyager.Server.Configuration.Dependencies.DependencyLifecycleInterfaces;
using System.Reflection;
using TreeCraftingVoyager.Server.Exceptions;
using TreeCraftingVoyager.Server.Logging;

namespace TreeCraftingVoyager.Server.Configuration.Dependencies;

public static class DependencyInjection
{
    private static readonly Type TransientType = typeof(ITransientDependency);
    private static readonly Type ScopedType = typeof(IPerWebRequestDependency);
    private static readonly Type SingletonType = typeof(ISingletonDependency);

    public static WebApplicationBuilder AddDependenciesByConvention(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var assembly = Assembly.GetExecutingAssembly();

        Register(services, assembly, TransientType);
        Register(services, assembly, ScopedType);
        Register(services, assembly, SingletonType);

        services.AddAutoMapper(typeof(MappingProfileConfiguration));
        
        LogRegisteredServicesByConvention(services);

        return builder;
    }

    private static void Register(IServiceCollection services, Assembly assembly, Type dependencyType)
    {
        var types = assembly.GetTypes()
            .Where(type =>
                !type.IsInterface &&
                !type.IsAbstract &&
                ImplementsDependency(type, dependencyType));

        foreach (var type in types)
            RegisterImplementations(services, type, dependencyType);
    }

    private static bool ImplementsDependency(Type type, Type dependencyType) => type.GetInterfaces().Any(i => i == dependencyType);

    private static void RegisterImplementations(IServiceCollection services, Type type, Type dependencyType)
    {
        var currType = GetOpenGenericClassTypeIfExist(type, out var isSameType);
        var allInterfacesInHierarchy = currType.GetInterfaces();

        var classInterfaces = allInterfacesInHierarchy.Where(interfaceType => !allInterfacesInHierarchy.Any(parentInterfaceType => parentInterfaceType != interfaceType && interfaceType.IsAssignableFrom(parentInterfaceType))).ToList();
        var classInterfacesWithRegistration = allInterfacesInHierarchy.Where(i => i != dependencyType && classInterfaces.Any(item => item == i) && ImplementsDependency(i, dependencyType)).ToList();
        var registerInterfaceIsOnClass = classInterfaces.FirstOrDefault(i => i == dependencyType) != null;

        // jeśli jest typem generycznym i nie jest TransientType to sprawdź ilość interfejsów dziedziczących od interfejsów IPerWebRequestDependency, ISingletonDependency
        // jeśli typ generyczny nie jest TransientType to nie ma możliwości rejestrowania wielu interfejsów bo np. singleton będzie osobnym obiektem dla każdego interfejsu
        if (!isSameType && dependencyType != TransientType && classInterfacesWithRegistration.Count > 1)
            throw new MultipleInterfacesForGenericTypeException($"Type {currType.FullName} has multiple registering interfaces. This is not allowed for IPerWebRequestDependency and ISingletonDependency.");

        // jeśli interfejs rejestracyjny jest bezpośrednio na klasie to dodajemy samą klasę
        if (registerInterfaceIsOnClass)
        {
            if (dependencyType == TransientType)
                services.AddTransient(currType);

            if (dependencyType == ScopedType)
                services.AddScoped(currType);

            if (dependencyType == SingletonType)
                services.AddSingleton(currType);
        }
        else
        {
            // jeśli typ nie jest generyczny dodajemy jego implementację klasy
            if (dependencyType == ScopedType && isSameType)
                services.AddScoped(currType);

            if (dependencyType == SingletonType && isSameType)
                services.AddSingleton(currType);
        }
        
        foreach (var interfaceType in classInterfacesWithRegistration)
        {
            var currInterfaceType = GetOpenGenericInterfaceTypeIfExist(interfaceType);
            
            if (dependencyType == TransientType)
                services.AddTransient(currInterfaceType, currType);

            // jeśli typ nie jest generyczny dodajemy interfejs do wcześniej dodanej implementacji klasy
            if (dependencyType == ScopedType)
                if (isSameType)
                    services.AddScoped(currInterfaceType, provider => provider.GetRequiredService(currType));
                else
                    services.AddScoped(currInterfaceType, currType);

            if (dependencyType == SingletonType)
                if (isSameType)
                    services.AddSingleton(currInterfaceType, provider => provider.GetRequiredService(currType));
                else
                    services.AddSingleton(currInterfaceType, currType);
        }
    }

    private static Type GetOpenGenericClassTypeIfExist(Type type, out bool isSameType)
    {
        var genericClassRegistrationAttribute = typeof(RegisterOpenGenericClassInDiAttribute);
        if (type.IsDefined(genericClassRegistrationAttribute, false))
        {
            var attribute = (RegisterOpenGenericClassInDiAttribute)type.GetCustomAttributes(genericClassRegistrationAttribute, false).First();
            isSameType = false;
            return attribute.ImplementationType;
        }
        else
        {
            isSameType = true;
            return type;
        }
    }

    private static Type GetOpenGenericInterfaceTypeIfExist(Type type)
    {
        var genericInterfaceRegistrationAttribute = typeof(RegisterOpenGenericInterfaceInDiAttribute);
        if (type.IsDefined(genericInterfaceRegistrationAttribute, false))
        {
            var attribute = (RegisterOpenGenericInterfaceInDiAttribute) type.GetCustomAttributes(genericInterfaceRegistrationAttribute, false).First();
            return attribute.InterfaceType;
        }
        else
            return type;
    }

    private static void LogRegisteredServicesByConvention(IServiceCollection services)
    {
        var logger = FileLoggerFactory.GetLogger();

        foreach (var service in services)
        {
            var associatedWithInjectionConvention = ImplementsDependency(service.ServiceType, TransientType)
                                                    || ImplementsDependency(service.ServiceType, ScopedType)
                                                    || ImplementsDependency(service.ServiceType, SingletonType);

            if (associatedWithInjectionConvention)
                logger.LogDebug($"Registered the service in a DI container according to convention: {service.Lifetime} => {service.ServiceType} => {service.ImplementationType}");
        }
    }
}
