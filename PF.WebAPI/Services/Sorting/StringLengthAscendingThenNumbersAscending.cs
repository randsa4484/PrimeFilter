using System;

namespace PF.WebAPI.Services.Sorting
{
    public class StringLengthAscendingThenNumbersAscending : IWordSorter
    {
        public string Description => "Non numeric strings by length (ascending), then numbers in ascending order";

        public int Compare(string x, string y)
        {
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
                throw new ArgumentException();

            var xIsDouble = double.TryParse(x, out var valX);
            var yIsDouble = double.TryParse(y, out var valY);

            if (xIsDouble && yIsDouble)
            {
                if (valX > valY)
                    return 1;
                if (valX < valY)
                    return -1;
                return 0;
            }


            if (xIsDouble)
                return 1;
            if (yIsDouble)
                return -1;

            return x.Length.CompareTo(y.Length);
        }
    }
}
