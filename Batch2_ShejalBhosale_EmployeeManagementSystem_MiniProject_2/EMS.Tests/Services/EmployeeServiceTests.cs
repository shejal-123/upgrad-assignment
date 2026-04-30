using NUnit.Framework;
using Moq;
using EMS.API.Services;
using EMS.API.Repositories;
using EMS.API.Models;
using EMS.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EMS.Tests.Services;

public class EmployeeServiceTests
{
    private Mock<IEmployeeRepository> _repoMock;
    private EmployeeService _service;

    [SetUp]
    public void Setup()
    {
        _repoMock = new Mock<IEmployeeRepository>();
        _service = new EmployeeService(_repoMock.Object);
    }

    [Test]
    public async Task GetById_ValidId_ReturnsEmployee()
    {
        var emp = new Employee { Id = 1, FirstName = "Priya", LastName = "Sharma", Email = "p@test.com", Status = "Active" };

        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(emp);

        var result = await _service.GetById(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.FirstName, Is.EqualTo("Priya"));
        _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Test]
    public async Task GetById_InvalidId_ReturnsNull()
    {
        _repoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync((Employee?)null);

        var result = await _service.GetById(10);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task Create_ValidEmployee_CallsAddAsync()
    {
        var dto = new EmployeeRequestDto
        {
            FirstName = "A",
            LastName = "B",
            Email = "a@test.com",
            Phone = "9999999999",
            Department = "IT",
            Designation = "Dev",
            Salary = 1000,
            JoinDate = DateTime.UtcNow,
            Status = "Active"
        };

        _repoMock.Setup(r => r.EmailExistsAsync(dto.Email, null)).ReturnsAsync(false);

        _repoMock.Setup(r => r.AddAsync(It.IsAny<Employee>()))
                 .Callback<Employee>(e => e.Id = 1)
                 .Returns(Task.CompletedTask);

        _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        _repoMock.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync(new Employee { Id = 1, FirstName = "A", LastName = "B", Email = "a@test.com" });

        var result = await _service.Create(dto);

        Assert.That(result, Is.Not.Null);
        _repoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
    }
}