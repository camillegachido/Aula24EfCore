using Aula24EfCore.Models;

public interface IUserRepository
{
    public List<TbUser> SelectUsers();

    public List<TbUser> SelectUsersWithOrders();
    public void UpdateUser(Guid id, string newPassword);
    public TbUser? GetUserById(Guid id);

    public Task<TbUser?> GetUserByEmailAsync(string email);

    public Task<TbUser> CreateUserAsync(TbUser user);
}