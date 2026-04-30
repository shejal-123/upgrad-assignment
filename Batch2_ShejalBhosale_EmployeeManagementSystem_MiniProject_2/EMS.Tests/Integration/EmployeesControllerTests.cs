using NUnit.Framework;
using Moq;
using EMS.API.Controllers;
using EMS.API.Services;
using EMS.API.Repositories;
using EMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EMS.Tests.Controllers;

public class EmployeesControllerTests
{
    private Mock<IEmployeeRepository> _repoMock;
    private EmployeeService _service;
    private EmployeesController _controller;

    [SetUp]
    public void Setup()
    {
        _repoMock = new Mock<IEmployeeRepository>();
        _service = new EmployeeService(_repoMock.Object);
        _controller = new EmployeesController(_service);
    }

    [Test]
    public async Task GetById_ValidId_ReturnsOk()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(new Employee { Id = 1, FirstName = "Test" });

        var result = await _controller.GetById(1);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
    }

    [Test]
    public async Task GetById_InvalidId_ReturnsNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(2))
            .ReturnsAsync((Employee?)null);

        var result = await _controller.GetById(2);

        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }
}