public interface IUserService
{
    public List<UserResponseDTO> GetUsers();
    public List<UserOrdersResponseDTO> GetUsersWithOrders();
}