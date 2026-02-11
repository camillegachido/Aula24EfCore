using Aula24EfCore.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

[TestFixture]
public class UserControllerTest
{
    private Mock<IUserService> _service;
    private UserController _controller;

    [SetUp]
    public void SetUp()
    {
        _service = new Mock<IUserService>();
        _controller = new UserController(_service.Object);
    }

    [Test]
    public void Get_ShouldReturnUsers_WhenUsersAreAvailable()
    {
        List<UserResponseDTO> list = new List<UserResponseDTO>
        {
            new UserResponseDTO
            {
                Id = new Guid(),
                Nome = "Camille",
                Email = "camille@ponei.com"
            }
        };

        _service.Setup(x=> x.GetUsers()).Returns(list);

        //ACT
        var result = _controller.Get();

        //Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());

        var requestResult = result.Result as OkObjectResult;
        Assert.That(requestResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));

        var requestBody = requestResult.Value as List<UserResponseDTO>;

        Assert.That(requestBody.Count, Is.EqualTo(1));

        Assert.That(requestBody[0].Id, Is.EqualTo(list[0].Id));
        Assert.That(requestBody[0].Nome, Is.EqualTo(list[0].Nome));
        Assert.That(requestBody[0].Email, Is.EqualTo(list[0].Email));
    }

    [Test]
    public void UpdateUser_ServiceRetornaSucesso_DeveRetornarOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new UpdateUserRequest { Senha = "NovaSenha123" };

        _service
            .Setup(s => s.UpdateUserPassword(userId, request.Senha))
            .Returns("");

        // Act
        var result = _controller.UpdateUser(userId, request);

        // Assert
        Assert.That(result, Is.TypeOf<OkResult>());
        _service.Verify(s => s.UpdateUserPassword(userId, request.Senha), Times.Once);
    }

    [Test]
    public void UpdateUser_ServiceRetornaError_DeveRetornarBadRequestComProblemDetails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new UpdateUserRequest { Senha = "NovaSenha123" };

        _service
            .Setup(s => s.UpdateUserPassword(userId, request.Senha))
            .Returns("error");

        // Act
        var result = _controller.UpdateUser(userId, request);

        // Assert
        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());

        var badRequest = result as BadRequestObjectResult;
        var problemDetails = badRequest.Value as ProblemDetails;

        Assert.That(problemDetails.Status, Is.EqualTo(StatusCodes.Status400BadRequest));
        Assert.That(problemDetails.Title, Is.EqualTo("Usuário não existe na base de dados"));
        Assert.That(problemDetails.Detail, Is.EqualTo("Usuário não foi encontrado para o id informado"));

        _service.Verify(s => s.UpdateUserPassword(userId, request.Senha), Times.Once);
    }

    [Test]
    public void UpdateUser_ServiceLancaExcecao_DevePropagarExcecao()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new UpdateUserRequest { Senha = "NovaSenha123" };

        _service
            .Setup(s => s.UpdateUserPassword(userId, request.Senha))
            .Throws(new Exception("Erro inesperado"));

        // Act & Assert
        Assert.Throws<Exception>(() => _controller.UpdateUser(userId, request));
        _service.Verify(s => s.UpdateUserPassword(userId, request.Senha), Times.Once);
    }
}