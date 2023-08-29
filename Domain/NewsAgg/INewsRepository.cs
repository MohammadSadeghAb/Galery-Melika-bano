namespace Domain.NewsAgg
{
    public interface INewsRepository : IRepository<Guid?, News>
    {
        Task<IList<News>> GetAllAsync();

        Task<News> GetByTitle(string Title);
    }
}
