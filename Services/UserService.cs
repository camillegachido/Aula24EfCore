using Aula24EfCore.Models;

// responsavel por toda logica do EndPoint
// Conecta ao repositorio
public class UserService : IUserService
{
    private IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public List<UserResponseDTO> GetUsers()
    {
        List<TbUser> tbUsers = _repository.SelectUsers();

        // crio uma lista com o tipo que quero do retorno
        List<UserResponseDTO> usersDTO = new List<UserResponseDTO>();

        // percorre a tabela de usuarios
        foreach(TbUser tbUser in tbUsers)
        {
            // cria um usuario com tipo do meu retorno
            UserResponseDTO usuarioRetorno = new UserResponseDTO();
            usuarioRetorno.Id = tbUser.Id;
            usuarioRetorno.Nome = tbUser.Name;
            usuarioRetorno.Email = tbUser.Email;

            // adiciona o usuario a minha lista de retorno
            usersDTO.Add(usuarioRetorno);
            // UserResponseDTO userDTO = new UserResponseDTO()
            // {
            //     Id = tbUser.Id,
            //     Nome = tbUser.Name,
            //     Email = tbUser.Email
            // };
            // usersDTO.Add(userDTO);
        }

        // retorna a lista que eu criei
        return usersDTO;
    }

    public List<UserOrdersResponseDTO> GetUsersWithOrders()
    {
       List<TbUser> tbUsers = _repository.SelectUsersWithOrders();

       List<UserOrdersResponseDTO> users = new List<UserOrdersResponseDTO>();

       foreach(TbUser tbUser in tbUsers)
        {
            UserOrdersResponseDTO user = new UserOrdersResponseDTO();
            user.Id = tbUser.Id;
            user.Nome = tbUser.Name;
            user.Email = tbUser.Email;
            // user.Pedidos = tbUser.TbOrders.Select(order => new OrderResponseDTO
            // {
            //     Id = order.Id,
            //     CreatedAt = order.CreatedAt,
            //     PaymentType = order.PaymentType,
            //     Total = order.Total
            // }).ToList();

            List<OrderResponseDTO> orders = new List<OrderResponseDTO>();
            foreach(TbOrder tbOrder in tbUser.TbOrders){
                OrderResponseDTO order = new OrderResponseDTO();
                order.Id = tbOrder.Id;
                order.PaymentType = tbOrder.PaymentType;
                order.Total = tbOrder.Total;

                orders.Add(order);
            }
            user.Pedidos = orders;


            users.Add(user);
        }

        return users;
    }

    public string UpdateUserPassword(Guid id, string password){
        TbUser? user = _repository.GetUserById(id);
        if(user == null){
            return "error";
        }

        _repository.UpdateUser(id, password);
        return "";
    }
}