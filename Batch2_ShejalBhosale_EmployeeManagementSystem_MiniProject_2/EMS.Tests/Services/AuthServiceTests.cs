using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using EMS.API.Services;
using EMS.API.Data;
using EMS.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EMS.Tests.Services;

public class AuthServiceTests
{
    private AppDbContext _db;
    private IConfiguration _config;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("AuthTestDb")
            .Options;

        _db = new AppDbContext(options);

       var settings = new Dictionary<string, string>
       {
        { "Jwt:Key", "THIS_IS_A_SUPER_SECRET_KEY_32_CHARS_LONG!!" },
        { "Jwt:Issuer", "EMS.API" },
        { "Jwt:Audience", "EMS.Client" },
        { "Jwt:ExpiryHours", "8" }
        };

        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(settings!)
            .Build();
    }

    [Test]
    public async Task Register_NewUser_ReturnsSuccess()
    {
        var service = new AuthService(_db, _config);

        var result = await service.RegisterAsync(new RegisterRequestDto
        {
            Username = "test",
            Password = "123456",
            Role = "Admin"
        });

        Assert.That(result.Success, Is.True);
        Assert.That(result.Role, Is.EqualTo("Admin"));
    }

    [Test]
    public async Task Login_ValidCredentials_ReturnsToken()
    {
        var service = new AuthService(_db, _config);

        await service.RegisterAsync(new RegisterRequestDto
        {
            Username = "login",
            Password = "123456",
            Role = "Admin"
        });

        var result = await service.LoginAsync(new LoginRequestDto
        {
            Username = "login",
            Password = "123456"
        });

        Assert.That(result.Success, Is.True);
        Assert.That(result.Token, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task Login_InvalidPassword_ReturnsFailure()
    {
        var service = new AuthService(_db, _config);

        await service.RegisterAsync(new RegisterRequestDto
        {
            Username = "user1",
            Password = "123456"
        });

        var result = await service.LoginAsync(new LoginRequestDto
        {
            Username = "user1",
            Password = "wrong"
        });

        Assert.That(result.Success, Is.False);
    }
}