public interface IUserService
{
    /// <summary>
    /// Metodo para retornar os usuarios do banco, porem o retorno é um DTO, ou seja, um tipo diferente do que esta no banco, isso é feito para não expor dados sensiveis como senha e tambem para ter um controle melhor sobre o que é retornado para o cliente
    /// </summary>
    /// <returns></returns>
    public List<UserResponseDTO> GetUsers();
    public List<UserOrdersResponseDTO> GetUsersWithOrders();
     public string UpdateUserPassword(Guid id, string password);
    public Task<UserResponseDTO> CreateUserAsync(CreateUserRequestDTO request);
}