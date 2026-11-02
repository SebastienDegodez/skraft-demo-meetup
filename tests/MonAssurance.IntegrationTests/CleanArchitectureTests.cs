using System.Reflection;
using NetArchTest.Rules;
using Xunit;

namespace MonAssurance.IntegrationTests;

public sealed class CleanArchitectureTests
{
    private const string DomainNamespace = "MonAssurance.Domain";
    private const string ApplicationNamespace = "MonAssurance.Application";
    private const string InfrastructureNamespace = "MonAssurance.Infrastructure";
    private const string ApiNamespace = "MonAssurance.Api";

    private static readonly Assembly domainAssembly = typeof(MonAssurance.Domain.IDomainMarker).Assembly;
    private static readonly Assembly applicationAssembly = typeof(MonAssurance.Application.IApplicationMarker).Assembly;
    private static readonly Assembly infrastructureAssembly = typeof(MonAssurance.Infrastructure.DependencyInjection).Assembly;
    private static readonly Assembly apiAssembly = typeof(Program).Assembly;

    [Fact]
    public void Domain_ShouldNotHaveDependencyOn_OtherLayers()
    {
        // Act
        var result = Types.InAssembly(domainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationNamespace)
            .And()
            .NotHaveDependencyOn(InfrastructureNamespace)
            .And()
            .NotHaveDependencyOn(ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Domain layer should not depend on other layers. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Application_ShouldOnlyDependOn_Domain()
    {
        // Act
        var result = Types.InAssembly(applicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureNamespace)
            .And()
            .NotHaveDependencyOn(ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Application layer should only depend on Domain. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Infrastructure_ShouldNotHaveDependencyOn_Api()
    {
        // Act
        var result = Types.InAssembly(infrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"Infrastructure should not depend on API layer. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Api_ShouldNotHaveDependencyOn_Domain_Directly()
    {
        // API must go through Application layer — direct Domain references bypass the application boundary.
        // API -> Application -> Domain is correct. API -> Domain directly is not allowed.
        // Note: API is allowed to depend on Application (CQS without bus: handlers injected into endpoints).
        var result = Types.InAssembly(apiAssembly)
            .Should()
            .NotHaveDependencyOn(DomainNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, $"API layer should not depend on Domain directly. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }
}
