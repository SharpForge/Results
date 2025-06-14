namespace SharpForge.Results
{
    /// <summary>
    /// Represents the outcome of an operation, with success/failure status and optional error message.
    /// </summary>
    public class Result : IEquatable<Result>
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Indicates whether the operation failed.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Contains the error message when the operation fails; otherwise null.
        /// </summary>
        public string? Error { get; }

        protected Result(bool isSuccess, string? error = null)
        {
            if (isSuccess && error is not null)
            {
                throw new ArgumentException("Successful result must not have an error message.", nameof(error));
            }

            if (!isSuccess && string.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentException("Failed result must have an error message.", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Determines whether the current instance is equal to another instance of <see cref="Result"/>.
        /// </summary>
        public bool Equals(Result? other)
        {
            if (other is null) return false;
            return IsSuccess == other.IsSuccess && Error == other.Error;
        }
        /// <summary>
        /// Gets the hash code for this instance.
        /// </summary>
        public override int GetHashCode() => HashCode.Combine(IsSuccess, Error);

        /// <summary>
        /// Creates a successful result with no value.
        /// </summary>
        public static Result Success() => new (true);

        /// <summary>
        /// Creates a failure result with an error message.
        /// </summary>
        /// <param name="error">The error message describing the failure.</param>
        public static Result Failure(string error) => new (false, error);

        /// <summary>
        /// Creates a successful result with a value of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to be returned.</typeparam>
        /// <param name="value">The value to be returned.</param>
        public static Result<T> Success<T>(T value) => new (value);

        /// <summary>
        /// Creates a failure result with an error message, for a result of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result that would have been returned.</typeparam>
        /// <param name="error">The error message describing the failure.</param>
        public static Result<T> Failure<T>(string error) => new (error);

    }

    /// <summary>
    /// Represents the outcome of an operation that returns a value.
    /// </summary>
    /// <typeparam name="T">Type of the returned value.</typeparam>
    public sealed class Result<T> : Result, IEquatable<Result<T>>
    {
        private readonly T? value;

        /// <summary>
        /// Gets the value of the result. Should only be accessed if <see cref="IsSuccess"/> is true.
        /// May be null if <typeparamref name="T"/> is a nullable type.
        /// </summary>
        public T Value 
        { 
            get {
                if (IsFailure)
                {
                    throw new InvalidOperationException("Cannot access Value on a failed result.");
                }
                return value;
            }
        }

        /// <summary>
        /// Indicates whether the result has a non-null value (only true for successful results).
        /// </summary>
        public bool HasValue => IsSuccess && Value is not null;

        /// <summary>
        /// Returns the value if the result is successful, otherwise returns default.
        /// </summary>
        public T? ValueOrDefault => IsSuccess ? value : default;

        /// <summary>
        /// Constructs a successful result with a value.
        /// </summary>
        /// <param name="value">The value to be returned.</param>
        internal Result(T value) : base(true)
        {
            this.value = value;
        }

        /// <summary>
        /// Constructs a failure result.
        /// </summary>
        /// <param name="error">The error message describing the failure.</param>
        internal Result(string error) : base(false, error)
        {
            value = default; // It is a Failure, so Value should not be used further.
        }


        /// <summary>
        /// Tries to get the value if the result is successful.
        /// </summary>
        /// <param name="value">The value to be set if the operation is successful.</param>
        public bool TryGetValue(out T? value)
        {
            if (IsSuccess)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Returns the value if the result is successful, or throws an exception with the error message.
        /// </summary>
        public T ValueOrThrow()
        {
            if (IsFailure)
                throw new InvalidOperationException(Error ?? "An unknown error occurred.");
            return value!;
        }

        /// <summary>
        /// Returns a string representation of the result.
        /// </summary>
        public override string ToString() =>
            IsSuccess ? $"Success({(value is null ? "null" : value.ToString())})" : $"Failure: {Error}";

        public bool Equals(Result<T>? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (IsSuccess != other.IsSuccess)
                return false;

            if (!IsSuccess)
                return Error == other.Error;

            // Both are success, compare values (handle nulls)
            return EqualityComparer<T?>.Default.Equals(ValueOrDefault, other.ValueOrDefault);
        }

        /// <summary>
        /// Determines whether the current instance is equal to another instance of <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        public override bool Equals(object? obj)
        {
            if (obj is Result<T> other)
                return Equals(other);
            return false;
        }

        /// <summary>
        /// Gets the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(IsSuccess, Error, ValueOrDefault);
        }
    }
}
