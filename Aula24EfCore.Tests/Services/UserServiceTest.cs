
using Aula24EfCore.Models;
using Moq;

[TestFixture]
public class UserServiceTest
{
    private Mock<IUserRepository> _repositoryMock;
    private UserService _service;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IUserRepository>();
        _service = new UserService(_repositoryMock.Object);
    }

    [Test]
    public void GetUsers_ShouldReturnEmtpyList_WhenNoUserIsAvailable()
    {
        ///AAA
        _repositoryMock.Setup(x => x.SelectUsers()).Returns(new List<TbUser>());

        //ACT
        var result = _service.GetUsers();

        //ASSERT
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetUsers_ShouldReturnDTO_WhenUsersAreAvailable()
    {
        ///AAA
        var list = new List<TbUser>
        {
            new TbUser
            {
                Name = "Camille",
                Id = new Guid(),
                Email = "camille@ponei.com"
            },
            new TbUser
            {
                Name = "João",
                Id = new Guid(),
                Email = "joao@ponei.com"
            }
        };

        _repositoryMock.Setup(x => x.SelectUsers()).Returns(list);

        //ACT
        var result = _service.GetUsers();

        //ASSERT
        Assert.That(result.Count, Is.EqualTo(2));
        
        // validar se o primeiro usuario retornou corretamente
        Assert.That(result[0].Id, Is.EqualTo(list[0].Id));
        Assert.That(result[0].Nome, Is.EqualTo(list[0].Name));
        Assert.That(result[0].Email, Is.EqualTo(list[0].Email));

        // validar se o segundo usuario retornou corretamente
        Assert.That(result[1].Id, Is.EqualTo(list[1].Id));
        Assert.That(result[1].Nome, Is.EqualTo(list[1].Name));
        Assert.That(result[1].Email, Is.EqualTo(list[1].Email));
    }
}