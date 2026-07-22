namespace ERMS.SharedKernel.Pagination;


public sealed record PageRequest(int Page = 1,int PageSize = 20)
{
    public int Skip => (Page - 1) * PageSize;
}