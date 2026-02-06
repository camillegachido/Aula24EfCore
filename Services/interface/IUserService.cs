public interface IUserService
{
    public List<UserResponseDTO> GetUsers();
    public List<UserOrdersResponseDTO> GetUsersWithOrders();
     public string UpdateUserPassword(Guid id, string password);
}