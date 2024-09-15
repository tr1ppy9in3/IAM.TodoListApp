using Microsoft.AspNetCore.Mvc;

namespace IAD.TodoListApp.Packages;

/// <summary>
/// Расширения для <see cref="Result{T}"/>
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Создать <see cref="IActionResult"/> на основе <see cref="Result{T}"/>
    /// </summary>
    /// <typeparam name="TResponseValue"></typeparam>
    /// <param name="result">Результат выполнения команды</param>
    /// <returns><see cref="IActionResult"/></returns>
    /// <exception cref="NotSupportedException">Неизвестный результат</exception>
    public static IActionResult ToActionResult<TResponseValue>(this in Result<TResponseValue> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => new OkObjectResult(result.GetValue()),
            ResultStatus.Created => new ObjectResult(result.GetValue()) { StatusCode = (int)result.Status },
            ResultStatus.NoContent => new NoContentResult(),
            ResultStatus.Invalid => new ObjectResult(result.Errors) { StatusCode = (int)result.Status },
            ResultStatus.Forbidden => new ForbidResult(),
            ResultStatus.Conflict => new ConflictObjectResult(result.Errors),
            ResultStatus.Error => new ObjectResult(result.Errors) { StatusCode = (int)result.Status },
            _ => throw new NotSupportedException()
        };
    }
}