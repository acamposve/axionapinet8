using Moq;
using Core.Abstractions.Services;

namespace Infrastructure.Tests.ServicesTests;


public class UserServiceTests
{
    private readonly Mock<ISecretUserQueriesRepository> _secretUserQueriesRepositoryMock;
    private readonly Mock<ISecretUserCommandsRepository> _secretUserCommandsRepositoryMock;
    private readonly Mock<ISecretRoleQueriesRepository> _secretRoleQueriesRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _secretUserQueriesRepositoryMock = new Mock<ISecretUserQueriesRepository>();
        _secretUserCommandsRepositoryMock = new Mock<ISecretUserCommandsRepository>();
        _secretRoleQueriesRepositoryMock = new Mock<ISecretRoleQueriesRepository>();
        _userService = new UserService(
            _secretUserQueriesRepositoryMock.Object,
            _secretUserCommandsRepositoryMock.Object,
            _secretRoleQueriesRepositoryMock.Object
        );
    }

    [Fact]
    public async Task ChangeUserRole_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var changeRoleCommand = new ChangeRoleCommand { UserName = "testuser", RoleName = "admin" };
        _secretUserQueriesRepositoryMock
            .Setup(x => x.FindUser(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("User not found"));

        // Act
        var result = await _userService.ChangeUserRole(changeRoleCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Message == "User not found");
    }

    [Fact]
    public async Task ChangeUserRole_ShouldReturnFailure_WhenRoleNotFound()
    {
        // Arrange
        var changeRoleCommand = new ChangeRoleCommand { UserName = "testuser", RoleName = "admin" };
        _secretUserQueriesRepositoryMock
            .Setup(x => x.FindUser(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(new User()));
        _secretRoleQueriesRepositoryMock
            .Setup(x => x.GetRoleEntity(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("Role not found"));

        // Act
        var result = await _userService.ChangeUserRole(changeRoleCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains(result.Errors, e => e.Message == "Role not found");
    }

    [Fact]
    public async Task ChangeUserRole_ShouldReturnSuccess_WhenUserAndRoleFound()
    {
        // Arrange
        var changeRoleCommand = new ChangeRoleCommand { UserName = "testuser", RoleName = "admin" };
        var user = new User();
        var role = new Role();
        _secretUserQueriesRepositoryMock
            .Setup(x => x.FindUser(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(user));
        _secretRoleQueriesRepositoryMock
            .Setup(x => x.GetRoleEntity(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(role));
        _secretUserCommandsRepositoryMock
            .Setup(x => x.ChangeUserRole(It.IsAny<User>(), It.IsAny<Role>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(true));

        // Act
        var result = await _userService.ChangeUserRole(changeRoleCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
}
