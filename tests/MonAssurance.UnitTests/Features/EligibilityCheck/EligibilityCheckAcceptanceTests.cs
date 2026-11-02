using Microsoft.Extensions.Time.Testing;
using MonAssurance.Application.Eligibility.Queries.CheckEligibility;
using MonAssurance.Domain.Eligibility;

namespace MonAssurance.UnitTests.Features.EligibilityCheck;

public class EligibilityCheckAcceptanceTests
{
    private static readonly DateOnly Today = new(2026, 1, 1);

    private static CheckEligibilityQueryHandler BuildHandler(DateOnly today)
    {
        var fakeTime = new FakeTimeProvider();
        fakeTime.SetUtcNow(today.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
        return new CheckEligibilityQueryHandler(new EligibilityPolicy(), fakeTime);
    }

    [Fact]
    public void Handle_WhenDriverWithSuspendedLicenseRequestsCar_ReturnsRefusedWithPermisSuspendu()
    {
        // Un assureur refuse un conducteur au permis suspendu
        var handler = BuildHandler(Today);
        var query = new CheckEligibilityQuery(
            DateOfBirth: Today.AddYears(-18),
            VehicleType: VehicleType.Car,
            Power: null,
            LicenseYears: 2,
            IsLicenseSuspended: true);

        var result = handler.Handle(query);

        Assert.False(result.IsEligible);
        Assert.Equal("Permis suspendu", result.RejectionReason);
    }
}
