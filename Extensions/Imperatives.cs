namespace SharpForge.Results.Extensions
{
    public static class Imperatives
    {
        #region OnSuccess

        /// <summary>
        /// Executes the specified action if the result is successful, and returns the original result.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The action to execute if the result is successful.</param>
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action.Invoke();
            }
            return result;
        }

        /// <summary>
        /// Executes the specified action if the result is successful, passing the value, and returns the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The action to execute if the result is successful.</param>
        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action.Invoke(result.Value);
            }
            return result;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is successful, and returns the original result.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is successful.</param>
        public static async Task<Result> OnSuccessAsync(this Result result, Func<Task> action)
        {
            if (result.IsSuccess)
            {
                await action.Invoke();
            }
            return result;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is successful, passing the value, and returns the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is successful.</param>
        public static async Task<Result<T>> OnSuccessAsync<T>(this Result<T> result, Func<T, Task> action)
        {
            if (result.IsSuccess)
            {
                await action.Invoke(result.Value);
            }
            return result;
        }

        #endregion

        #region OnFailure

        /// <summary>
        /// Executes the specified action if the result is a failure, passing the error message, and returns the original result.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The action to execute if the result is a failure.</param>
        public static Result OnFailure(this Result result, Action<string?> action)
        {
            if (result.IsFailure)
            {
                action.Invoke(result.Error);
            }
            return result;
        }

        /// <summary>
        /// Executes the specified action if the result is a failure, passing the error message, and returns the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The action to execute if the result is a failure.</param>
        public static Result<T> OnFailure<T>(this Result<T> result, Action<string?> action)
        {
            if (result.IsFailure)
            {
                action.Invoke(result.Error);
            }
            return result;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is a failure, passing the error message, and returns the original result.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is a failure.</param>
        public static async Task<Result> OnFailureAsync(this Result result, Func<string?, Task> action)
        {
            if (result.IsFailure)
            {
                await action.Invoke(result.Error);
            }
            return result;
        }

        /// <summary>
        /// Executes the specified asynchronous action if the result is a failure, passing the error message, and returns the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is a failure.</param>
        public static async Task<Result<T>> OnFailureAsync<T>(this Result<T> result, Func<string?, Task> action)
        {
            if (result.IsFailure)
            {
                await action.Invoke(result.Error);
            }
            return result;
        }

        #endregion

        #region OnBoth

        /// <summary>
        /// Executes the specified action regardless of the result's success or failure, and returns the original result.
        /// </summary>
        /// <param name="result">The result to apply the action to.</param>
        /// <param name="action">The action to execute, receiving the result.</param>
        public static Result OnBoth(this Result result, Action<Result> action)
        {
            action.Invoke(result);
            return result;
        }

        /// <summary>
        /// Executes the specified action regardless of the result's success or failure, and returns the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to apply the action to.</param>
        /// <param name="action">The action to execute, receiving the result.</param>
        public static Result<T> OnBoth<T>(this Result<T> result, Action<Result<T>> action)
        {
            action.Invoke(result);
            return result;
        }

        /// <summary>
        /// Executes the specified asynchronous action regardless of the result's success or failure, and returns the original result.
        /// </summary>
        /// <param name="result">The result to apply the action to.</param>
        /// <param name="action">The asynchronous action to execute, receiving the result.</param>
        public static async Task<Result> OnBothAsync(this Result result, Func<Result, Task> action)
        {
            await action.Invoke(result);
            return result;
        }

        /// <summary>
        /// Executes the specified asynchronous action regardless of the result's success or failure, and returns the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to apply the action to.</param>
        /// <param name="action">The asynchronous action to execute, receiving the result.</param>
        public static async Task<Result<T>> OnBothAsync<T>(this Result<T> result, Func<Result<T>, Task> action)
        {
            await action.Invoke(result);
            return result;

        }

        #endregion

        #region Tap

        /// <summary>
        /// Executes the specified action if the result is successful, returning the original result.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The action to execute if the result is successful.</param>
        public static Result Tap(this Result result, Action action) => result.OnSuccess(action);

        /// <summary>
        /// Executes the specified action if the result is successful, returning the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The action to execute if the result is successful.</param>
        public static Result<T> Tap<T>(this Result<T> result, Action<T> action) => result.OnSuccess(action);

        /// <summary>
        /// Executes an asynchronous action if the result is successful, returning the original result.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is successful.</param>
        public static async Task<Result> TapAsync(this Result result, Func<Task> action) => await result.OnSuccessAsync(action);

        /// <summary>
        /// Executes an asynchronous action if the result is successful, returning the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is successful.</param>
        public static async Task<Result<T>> TapAsync<T>(this Result<T> result, Func<T, Task> action) => await result.OnSuccessAsync(action);

        #endregion

        #region TapError

        /// <summary>
        /// Executes the specified action if the result is a failure, passing the result's error message.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The action to execute if the result is a failure, receiving the error message.</param>
        public static Result TapError(this Result result, Action<string?> action) => result.OnFailure(action);

        /// <summary>
        /// Executes the specified action if the result is a failure, passing the result's error message.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The action to execute if the result is a failure, receiving the error message.</param>
        public static Result<T> TapError<T>(this Result<T> result, Action<string?> action) => result.OnFailure(action);

        /// <summary>
        /// Executes an asynchronous action if the result is a failure, passing the result's error message.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is a failure.</param>
        public static async Task<Result> TapErrorAsync(this Result result, Func<string?, Task> action) => await result.OnFailureAsync(action);

        /// <summary>
        /// Executes an asynchronous action if the result is a failure, returning the original result.
        /// </summary>
        /// <typeparam name="T">Type of the value contained in the result.</typeparam>
        /// <param name="result">The result to check.</param>
        /// <param name="action">The asynchronous action to execute if the result is a failure.</param>
        public static async Task<Result<T>> TapErrorAsync<T>(this Result<T> result, Func<string?, Task> action) => await result.OnFailureAsync(action);

        #endregion
    }
}
