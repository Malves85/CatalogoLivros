namespace CatalogoLivros.Helpers
{
    public class Search
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public string Searching { get; set; } = "";
        public string Sorting { get; set; } = "";
    }
}
