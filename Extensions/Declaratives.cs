namespace SharpForge.Results.Extensions
{
    public static class Declaratives
    {
        #region Then

        /// <summary>
        /// After a successful result, executes the next function and returns its result.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="next">The function to execute if the result is successful.</param>
        public static Result Then(this Result result, Func<Result> next)
        {
            if (result.IsSuccess)
            {
                return next.Invoke();
            }
            return Result.Failure(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// After a successful result, executes the next asynchronous function and returns its result.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="next">The asynchronous function to execute if the result is successful.</param>
        public static async Task<Result> ThenAsync(this Result result, Func<Task<Result>> next)
        {
            if (result.IsSuccess)
            {
                return await next.Invoke();
            }
            return Result.Failure(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// After a successful result, executes the next function and returns its result as a new result of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to be returned.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="next">The function to execute if the result is successful, returning a result of type <typeparamref name="T"/>.</param>
        public static Result<T> Then<T>(this Result result, Func<Result<T>> next)
        {
            if (result.IsSuccess)
            {
                return next.Invoke();
            }
            return Result.Failure<T>(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// After a successful result, executes the next asynchronous function and returns its result as a new result of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to be returned.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="next">The asynchronous function to execute if the result is successful, returning a result of type <typeparamref name="T"/>.</param>
        public static async Task<Result<T>> ThenAsync<T>(this Result result, Func<Task<Result<T>>> next)
        {
            if (result.IsSuccess)
            {
                return await next.Invoke();
            }
            return Result.Failure<T>(result.Error ?? "Operation failed with unknown error.");
        }

        #endregion

        #region Otherwise

        /// <summary>
        /// After a failed result, maps the error to a new error message using the provided function.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="errorMapper">The function to map the error message.</param>
        public static Result Otherwise(this Result result, Func<string?, string> errorMapper)
        {
            if (result.IsFailure)
            {
                return Result.Failure(errorMapper.Invoke(result.Error));
            }
            return result;
        }

        /// <summary>
        /// After a failed result, maps the error to a new error message using the provided asynchronous function.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="errorMapper">The asynchronous function to map the error message.</param>
        public static async Task<Result> OtherwiseAsync(this Result result, Func<string?, Task<string>> errorMapper)
        {
            if (result.IsFailure)
            {
                var error = await errorMapper.Invoke(result.Error);
                return Result.Failure(error);
            }
            return result;
        }

        /// <summary>
        /// After a failed result, maps the error to a new error message using the provided function.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="errorMapper">The function to map the error message.</param>
        public static Result<T> Otherwise<T>(this Result<T> result, Func<string?, string> errorMapper)
        {
            if (result.IsFailure)
            {
                return Result.Failure<T>(errorMapper.Invoke(result.Error));
            }
            return result;
        }

        /// <summary>
        /// After a failed result, maps the error to a new error message using the provided asynchronous function.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="errorMapper">The asynchronous function to map the error message.</param>
        public static async Task<Result<T>> OtherwiseAsync<T>(this Result<T> result, Func<string?, Task<string>> errorMapper)
        {
            if (result.IsFailure)
            {
                var error = await errorMapper.Invoke(result.Error);
                return Result.Failure<T>(error);
            }
            return result;
        }

        #endregion

        #region Match

        /// <summary>
        /// Matches the result, executing the success or failure function based on the result's state.
        /// </summary>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The function to execute if the result is successful.</param>
        /// <param name="onFailure">The function to execute if the result is a failure, receiving the error message.</param>
        public static Result Match(this Result result, Func<Result> onSuccess, Func<string, Result> onFailure)
        {
            if (result.IsSuccess)
            {
                return onSuccess.Invoke();
            }
            return onFailure.Invoke(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Matches the result, executing the success or failure asynchronous function based on the result's state.
        /// </summary>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The asynchronous function to execute if the result is successful.</param>
        /// <param name="onFailure">The asynchronous function to execute if the result is a failure, receiving the error message.</param>
        public static async Task<Result> MatchAsync(this Result result, Func<Task<Result>> onSuccess, Func<string, Task<Result>> onFailure)
        {
            if (result.IsSuccess)
            {
                return await onSuccess.Invoke();
            }
            return await onFailure.Invoke(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Matches the result, executing the success or failure function based on the result's state.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The function to execute if the result is successful.</param>
        /// <param name="onFailure">The function to execute if the result is a failure, receiving the error message.</param>
        public static Result<T> Match<T>(this Result result, Func<Result<T>> onSuccess, Func<string, Result<T>> onFailure)
        {
            if (result.IsSuccess)
            {
                return onSuccess.Invoke();
            }
            return onFailure.Invoke(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Matches the result, executing the success or failure asynchronous function based on the result's state.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The asynchronous function to execute if the result is successful.</param>
        /// <param name="onFailure">The asynchronous function to execute if the result is a failure, receiving the error message.</param>
        public static async Task<Result<T>> MatchAsync<T>(this Result result, Func<Task<Result<T>>> onSuccess, Func<string, Task<Result<T>>> onFailure)
        {
            if (result.IsSuccess)
            {
                return await onSuccess.Invoke();
            }
            return await onFailure.Invoke(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Matches the result, executing the success or failure function based on the result's state.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The function to execute if the result is successful, receiving the value.</param>
        /// <param name="onFailure">The function to execute if the result is a failure, receiving the error message.</param>
        public static Result Match<T>(this Result<T> result, Func<T, Result> onSuccess, Func<string, Result> onFailure)
        {
            if (result.IsSuccess)
                return onSuccess(result.Value);
            return onFailure(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Matches the result, executing the success or failure asynchronous function based on the result's state.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The asynchronous function to execute if the result is successful, receiving the value.</param>
        /// <param name="onFailure">The asynchronous function to execute if the result is a failure, receiving the error message.</param>
        public static async Task<Result> MatchAsync<T>(this Result<T> result, Func<T, Task<Result>> onSuccess, Func<string, Task<Result>> onFailure)
        {
            if (result.IsSuccess)
                return await onSuccess(result.Value);
            return await onFailure(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Matches the result, executing the success or failure function based on the result's state, returning a new Result of type TOut.
        /// </summary>
        /// <typeparam name="TIn">Type of the value contained in the result.</typeparam>
        /// <typeparam name="TOut">Type of the value to be returned.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The function to execute if the result is successful, receiving the value and returning a Result of type TOut.</param>
        /// <param name="onFailure">The function to execute if the result is a failure, receiving the error message and returning a Result of type TOut.</param>
        public static Result<TOut> Match<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> onSuccess, Func<string, Result<TOut>> onFailure)
        {
            if (result.IsSuccess)
                return onSuccess(result.Value);
            return onFailure(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Matches the result, executing the success or failure asynchronous function based on the result's state, returning a new Result of type TOut.
        /// </summary>
        /// <typeparam name="TIn">Type of the value contained in the result.</typeparam>
        /// <typeparam name="TOut">Type of the value to be returned.</typeparam>
        /// <param name="result">The result to match.</param>
        /// <param name="onSuccess">The asynchronous function to execute if the result is successful, receiving the value and returning a Result of type TOut.</param>
        /// <param name="onFailure">The asynchronous function to execute if the result is a failure, receiving the error message and returning a Result of type TOut.</param>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> onSuccess, Func<string, Task<Result<TOut>>> onFailure)
        {
            if (result.IsSuccess)
                return await onSuccess(result.Value);
            return await onFailure(result.Error ?? "Operation failed with unknown error.");
        }

        #endregion

        #region Map

        /// <summary>
        /// Maps the result to a new Result of type T using the provided mapper function.
        /// </summary>
        /// <typeparam name="T">Type of the value to be returned.</typeparam>
        /// <param name="result">The result to map.</param>
        /// <param name="mapper">The function to map the result to a new Result of type T.</param>
        public static Result<T> Map<T>(this Result result, Func<Result<T>> mapper)
        {
            if (result.IsSuccess)
            {
                return mapper.Invoke();
            }
            return Result.Failure<T>(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Maps the result to a new Result of type T using the provided asynchronous mapper function.
        /// </summary>
        /// <typeparam name="T">Type of the value to be returned.</typeparam>
        /// <param name="result">The result to map.</param>
        /// <param name="mapper">The asynchronous function to map the result to a new Result of type T.</param>
        public static async Task<Result<T>> MapAsync<T>(this Result result, Func<Task<Result<T>>> mapper)
        {
            if (result.IsSuccess)
            {
                return await mapper.Invoke();
            }
            return Result.Failure<T>(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Maps the value of a successful result to a new type using the provided mapper function.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <typeparam name="TNew">Type of the new value to be returned.</typeparam>
        /// <param name="result">The result to map.</param>
        /// <param name="mapper">The function to map the value to a new type.</param>
        public static Result<TNew> Map<T, TNew>(this Result<T> result, Func<T, TNew> mapper)
        {
            if (result.IsSuccess)
            {
                return Result.Success(mapper.Invoke(result.Value));
            }
            return Result.Failure<TNew>(result.Error ?? "Operation failed with unknown error.");
        }

        /// <summary>
        /// Maps the value of a successful result to a new type using the provided asynchronous mapper function.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <typeparam name="TNew">Type of the new value to be returned.</typeparam>
        /// <param name="result">The result to map.</param>
        /// <param name="mapper">The asynchronous function to map the value to a new type.</param>
        public static async Task<Result<TNew>> MapAsync<T, TNew>(this Result<T> result, Func<T, Task<TNew>> mapper)
        {
            if (result.IsSuccess)
            {
                var newValue = await mapper.Invoke(result.Value);
                return Result.Success(newValue);
            }
            return Result.Failure<TNew>(result.Error ?? "Operation failed with unknown error.");
        }

        #endregion

        #region MapError

        /// <summary>
        /// After a failed result, maps the error to a new error message using the provided function.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="errorMapper">The function to map the error message.</param>
        public static Result MapError(this Result result, Func<string?, string> errorMapper) => result.Otherwise(errorMapper);

        /// <summary>
        /// After a failed result, maps the error to a new error message using the provided asynchronous function.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="errorMapper">The asynchronous function to map the error message.</param>
        public static async Task<Result> MapErrorAsync(this Result result, Func<string?, Task<string>> errorMapper) => await result.OtherwiseAsync(errorMapper);

        /// <summary>
        /// After a failed result, maps the error to a new error message using the provided function.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="errorMapper">The function to map the error message.</param>
        public static Result<T> MapError<T>(this Result<T> result, Func<string?, string> errorMapper) => result.Otherwise(errorMapper);
        /// <summary>
        /// After a failed result, maps the error to a new error message using the provided asynchronous function.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="errorMapper">The asynchronous function to map the error message.</param>
        public static async Task<Result<T>> MapErrorAsync<T>(this Result<T> result, Func<string?, Task<string>> errorMapper) => await result.OtherwiseAsync(errorMapper);

        #endregion

        #region Convert

        /// <summary>
        /// Converts a nullable value to a Result, returning a failure if the value is null.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The nullable value to convert.</param>
        /// <param name="errorMessage">The optional error message to use if the value is null.</param>
        public static Result<T> ToResult<T>(this T? value, string errorMessage = "Value is null.") where T : class
        {
            if (value is not null)
            {
                return Result.Success(value);
            }
            else
            {
                return Result.Failure<T>(errorMessage);
            }
        }

        /// <summary>
        /// Converts a nullable value to a Result, returning a failure if the value is null.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The nullable value to convert.</param>
        /// <param name="errorMessage">The optional error message to use if the value is null.</param>
        public static Result<T> ToResult<T>(this T? value, string errorMessage = "Value is null.") where T : struct
        {
            if (value.HasValue)
            {
                return Result.Success(value.Value);
            }
            else
            {
                return Result.Failure<T>(errorMessage);
            }
        }

        /// <summary>
        /// Converts a boolean condition to a Result, returning success if true and failure if false.
        /// </summary>
        /// <param name="condition">The boolean condition to evaluate.</param>
        /// <param name="errorMessage">The optional error message to use if the condition is false.</param>
        public static Result ToResult(this bool condition, string errorMessage = "Condition is false.")
        {
            if (condition)
            {
                return Result.Success();
            }
            else
            {
                return Result.Failure(errorMessage);
            }
        }

        #endregion

        #region Deconstruct

        /// <summary>
        /// Returns the success state and error message of the result.
        /// </summary>
        /// <param name="result">The result to deconstruct.</param>
        /// <param name="isSuccess">Output parameter indicating whether the result is successful.</param>
        /// <param name="error">Output parameter containing the error message if the result is a failure, or null if successful.</param>
        public static void Deconstruct(this Result result, out bool isSuccess, out string? error)
        {
            isSuccess = result.IsSuccess;
            error = result.Error;
        }

        /// <summary>
        /// Returns the success state and error message of the result.
        /// </summary>
        /// <param name="result">The result to deconstruct.</param>
        /// <param name="isSuccess">Output parameter indicating whether the result is successful.</param>
        /// <param name="error">Output parameter containing the error message if the result is a failure, or null if successful.</param>
        public static void Deconstruct<T>(this Result<T> result, out bool isSuccess, out string? error, out T value)
        {
            isSuccess = result.IsSuccess;
            value = result.Value;
            error = result.Error;
        }

        #endregion
    }

}
