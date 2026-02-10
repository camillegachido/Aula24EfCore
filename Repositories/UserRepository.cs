using Aula24EfCore.Models;
using Microsoft.EntityFrameworkCore;

// Responsavel pela conexao com o banco de dados
public class UserRepository : IUserRepository
{
    private readonly PedidosContext dbContext;

    public UserRepository(PedidosContext context)
    {
        dbContext = context;
    }

    public List<TbUser> SelectUsers()
    {
         List<TbUser> tbUsers = dbContext.TbUsers.ToList();
         return tbUsers;
    }

    public List<TbUser> SelectUsersWithOrders()
    {
        List<TbUser> tbUsers = dbContext.TbUsers.Include(u => u.TbOrders).ToList();
        return tbUsers;
    }

    public TbUser? GetUserById(Guid id){
        TbUser? findedUser = dbContext.TbUsers.Find(id);
        return findedUser;
    }

    public void UpdateUser(Guid id, string newPassword){
        TbUser? findedUser = dbContext.TbUsers.Find(id);
        findedUser.Password = newPassword;
        dbContext.SaveChanges();
    }
    public async Task<TbUser?> GetUserByEmailAsync(string email)
    {
        return await dbContext.TbUsers.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<TbUser> CreateUserAsync(TbUser user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        
        dbContext.TbUsers.Add(user);
        await dbContext.SaveChangesAsync();
        
        return user;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public TbUser PostUser(TbUser user)
    {
        dbContext.TbUsers.Add(user);
        dbContext.SaveChanges();
        return user;
    }
}