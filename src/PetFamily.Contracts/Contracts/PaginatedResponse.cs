namespace PetFamily.Contracts.Contracts;

public class PaginatedResponse<TItem>
{
    public List<TItem> Items { get; set; } = [];
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public bool HasPrevious => CurrentPage > 0;
    public bool HasNext => CurrentPage < TotalPages;
}