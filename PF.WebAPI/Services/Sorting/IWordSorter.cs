namespace PF.WebAPI.Services.Sorting
{
    public interface IWordSorter
    {
        string Description { get; }

        int Compare(string x, string y);
    }
}