using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IAD.TodoListApp.Packages;

/// <summary>
/// Расширения DI для добавления валидации
/// </summary>
public static class ServiceCollectionValidatorExtension
{
    /// <summary>
    /// Добавить валидацию в пайплайн обработки команд.
    /// </summary>
    /// <param name="services"> Коллекция сервисов </param>
    /// <param name="assemblies"> Сборки с командами или валидаторами </param>
    /// <returns> Коллекция сервисов </returns>
    public static IServiceCollection AddValidationPipelines(
        this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies);
        var types = assemblies.SelectMany(assembly => assembly.GetTypes());
        AddValidationPipelines(services, types);
        return services;
    }

    private static void AddValidationPipelines(IServiceCollection services, IEnumerable<Type> commandTypes)
    {
        foreach (var commandType in commandTypes)
        {
            if (commandType.IsClass && !commandType.IsAbstract && !commandType.ContainsGenericParameters)
            {
                var targetInterface = commandType
                                        .GetInterfaces()
                                        .FirstOrDefault(i => i.IsGenericType &&
                                                             i.GetGenericTypeDefinition() == typeof(IValidatableCommand<>));

                if (targetInterface != null)
                {
                    var valueType = targetInterface.GenericTypeArguments[0];
                    AddScoped(services, commandType, valueType);
                }
            }
        }
    }

    private static void AddScoped(IServiceCollection services, Type commandType, Type valueType)
    {
        var pipe = typeof(IPipelineBehavior<,>).MakeGenericType(commandType, typeof(Result<>).MakeGenericType(valueType));
        services.AddScoped(pipe, typeof(ValidationBehavior<,>).MakeGenericType(commandType, valueType));
    }
}