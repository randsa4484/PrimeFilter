# PrimeFilter

This is a .Net Core 2.2 Solution, developed initially in VS Code & then in Visual Studio 2017. It has not been opened in VS Code since Sunday, so it is possible it does not compile/run in that environment.

With the filtering, I have taken an approach whereby I use brute force testing of candidate doubles, using a precomputed collection of primes.

This means that there is a limit, beyond which I cannot assertain if an integer is prime or not. I have assumed that this approach is acceptable as I have not had time to get to understand other techniques/algorithms such as Miller-Rabin primality test.

There is a setting in appsettings.json which controls the number of primes generated, using Sieve of Eratosthenes:

PrimesTesting":"LimitForPrimeGeneration"

The idea is that if say, the limit is set to 12, it will find primes 2, 3, 5, 7, 11

I can test if 143 is prime, by testing its divisibility by these primes, as I know the highest factor needing testing is (int)SqrRoot(143) which is 11.

The limit that applies can be seen by means of the https://localhost:5001/TechTest/Health GET 

This returns information about the filter & sort that have been loaded via DI:

{
  "wordFilter": "Word filter in use is Remove Prime Number Filter",
  "maxSupportedNumber": 871897231,
  "sortCriteria": "Non numeric strings by length (ascending), then numbers in ascending order",
  "applicationVersion": "1.0.0.0"
}

If a number exceeding the limit is presented, the json response hints at the problem:

400: '7.77777777777778E+32 was too large for the applied primality test algorithm's limit: 871897231'

I have used Swagger as a means of documenting the WebApi and to aid discovery of the key endpoints

https://localhost:5001/TechTest/FilterAndOrder/SpaceDelimited
https://localhost:5001/TechTest/FilterAndOrder/UserDefinedDelimiter

