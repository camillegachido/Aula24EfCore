using Aula24EfCore.Models;

public interface IUserRepository
{
    public List<TbUser> SelectUsers();

    public List<TbUser> SelectUsersWithOrders();
}