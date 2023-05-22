using ChatAggregator.Core;
using ChatAggregator.Core.UseCase.InterfaceAdapters;
using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;
using ChatAggregator.Web.Controllers;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace ChatHistoryAggregator.Architecture.Tests.ArchitectureTests;

public class CleanArchitectureTests
{
    [Fact]
    public void Web_Should_Be_Dependent_On_Core()
    {
        var result = Types.InAssembly(typeof(ChatRoomController).Assembly)
            .That()
            .ResideInNamespace(typeof(ChatRoomController).ToString()) 
            .Should()
            .HaveDependencyOn("ChatAggregator.Core")
            .GetResult()
            .IsSuccessful;

        result.Should().Be(true);
    }
    
    [Fact]
    public void Web_Should_Not_Be_Dependent_On_Domain()
    {
        var result = Types.InAssembly(typeof(ChatRoomController).Assembly)
            .That()
            .ResideInNamespace("ChatAggregator.Web")
            .ShouldNot()
            .HaveDependencyOn("ChatAggregator.Domain")
            .GetResult()
            .IsSuccessful;
    
        result.Should().Be(true);
    }

    [Fact]
    public void Core_Should_Be_Dependent_On_Domain()
    {
        var result = Types.InAssembly(typeof(EventService).Assembly)
            .That()
            .ResideInNamespace(typeof(EventService).ToString()) 
            .Should()
            .HaveDependencyOn("ChatAggregator.Domain")
            .GetResult()
            .IsSuccessful;

        result.Should().Be(true);
    }
    
    [Fact]
    public void Core_Should_Not_Be_Dependent_On_Web()
    {
        var result = Types.InAssembly(typeof(EventService).Assembly)
            .That()
            .ResideInNamespace(typeof(EventService).ToString()) 
            .ShouldNot()
            .HaveDependencyOn("ChatAggregator.Web")
            .GetResult()
            .IsSuccessful;

        result.Should().Be(true);
    }
    
    [Fact]
    public void Domain_Should_Not_Be_Dependent_On_Web()
    {
        var result = Types.InAssembly(typeof(ChatEvent).Assembly)
            .That()
            .ResideInNamespace(typeof(ChatEvent).ToString()) 
            .ShouldNot()
            .HaveDependencyOn("ChatAggregator.Web")
            .GetResult()
            .IsSuccessful;

        result.Should().Be(true);
    }
    
    [Fact]
    public void Domain_Should_Not_Be_Dependent_On_Core()
    {
        var result = Types.InAssembly(typeof(ChatEvent).Assembly)
            .That()
            .ResideInNamespace(typeof(ChatEvent).ToString()) 
            .ShouldNot()
            .HaveDependencyOn("ChatAggregator.Core")
            .GetResult()
            .IsSuccessful;

        result.Should().Be(true);
    }
    
    [Fact]
    public void Interfaces_Should_Have_Name_Starting_With_I()
    {
        var result = Types.InAssembly(typeof(IDataService).Assembly)
            .That().ResideInNamespace(("ChatAggregator.Core.UseCase.Interfaces"))
            .And().AreInterfaces()
            .Should().HaveNameStartingWith("I")
            .GetResult();
        Assert.True(result.IsSuccessful);
    }   
    
    
}