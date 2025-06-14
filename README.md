# SharpForge.Results

A modern, fluent, and robust Result type for C# and .NET, inspired by functional programming.  
SharpForge.Results helps you write expressive, safe, and composable code by modeling success and failure as first-class citizens.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [API Highlights](#api-highlights)
- [Planned Features](#planned-features)
- [Design Philosophy](#design-philosophy)
- [License](#license)

## Features

- **Result and Result&lt;T&gt;**: Represent success or failure, with or without a value.
- **Async-First**: All major methods have async variants.
- **Fluent API**: Chain operations with `Then`, `Map`, `Otherwise`, and more.
- **Side-Effect Methods**: Use `OnSuccess`, `OnFailure`, `OnBoth`, `Tap`, and `TapError` for clean, imperative side effects and logging.
- **Pattern Matching**: Handle success/failure branches with `Match` and `MatchAsync`.
- **Error Mapping**: Transform error messages with `MapError`/`Otherwise`.
- **Conversion Helpers**: Convert from nullable types and booleans to results.
- **Deconstruction**: Use C# deconstruction syntax for ergonomic result handling.

## Installation

Add the package to your project: `dotnet add package SharpForge.Results`

## Quick Start

```csharp
using SharpForge.Results;
using SharpForge.Results.Extensions;

// Basic usage
Result r1 = Result.Success();
Result r2 = Result.Failure("User not found. You do not exist.");
Result<int> r3 = Result.Success(42);
Result<int> r4 = Result.Failure<int>("Math ain't mathing correctly.");

// Chaining operations
var result = Result.Success()
	.Then(() => DoSomething())
	.Then(() => DoSomethingElse())
	.Otherwise(() => HandleFailureSomehow());

// Mapping values
var mapped = r3.Map(value => value * 2); // Result<int> with value 84

// Pattern matching
var matchResult = r3.Match(
	onSuccess: value => {
		DoSomeStuff(value);
		return Result.Success();
		},
	onFailure: error => {
		RememberError(error);
		return Result.Failure();
		}
);

// Side effects / Logging
r3.OnSuccess(value => Console.WriteLine($"Success: {value}"))
  .OnFailure(error => Console.WriteLine($"Error: {error}"))
  .OnBoth(result => Console.WriteLine($"Result completed with status: {result.IsSuccess}"));

// Async operations
var asyncResult = await Result.Success()
	.ThenAsync(async () => await DoSomethingAsync())
	.ThenAsync(async () => await DoSomethingElseAsync())
	.OtherwiseAsync(async () => await HandleFailureSomehowAsync());

// Conversions
string? name = GetNameOrNull(); 
var nameResult = name.ToResult("Name is missing");

// Deconstruction
var (isSuccess, error) = result; 
var (ok, err, value) = mapped;

```


## API Highlights

- **Result**: Represents success/failure with an optional error message.
- **Result&lt;T&gt;**: Represents success/failure with a value of type `T`.
- **Then / ThenAsync**: Chain operations that return results.
- **Map / MapAsync**: Transform the value of a successful result.
- **Otherwise / MapError**: Transform error messages.
- **OnSuccess / OnFailure / OnBoth**: Perform side effects.
- **Tap / TapError**: Aliases for OnSuccess/OnFailure, for functional style.
- **Match / MatchAsync**: Pattern match on result state.
- **ToResult**: Convert nullable or boolean to a result.
- **Deconstruct**: Use C# deconstruction for ergonomic handling.

## Planned Features

*The following features are under consideration and may be added in future releases, depending on community feedback and project needs.*

- **Generic Error Types**:  
  Support for `Result<T, TError>` to allow custom error types (e.g., error codes, exceptions, or domain-specific error objects), not just strings.
- **Implicit Conversions**:  
  Ergonomic implicit conversions between values and results for cleaner code.
- **Exception Handling Helpers**:  
  Methods to convert exceptions to results.

Have a feature request? [Open an issue](https://github.com/your-repo/issues) or contribute!

## Design Philosophy

- **Clarity**: Make success and failure explicit.
- **Composability**: Chain operations fluently.
- **Safety**: Avoid exceptions for control flow.
- **Minimalism**: No redundant overloads or bloat.

## License

MIT

---

**SharpForge.Results** – Write safer, more expressive C#.