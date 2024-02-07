namespace Domain.Users
{
	public interface IUserRepository : IRepository<Guid, User>
	{
		Task<IList<User>> GetAllAsync();

		Task<User> GetByFullName(string fullName);

	}
}
