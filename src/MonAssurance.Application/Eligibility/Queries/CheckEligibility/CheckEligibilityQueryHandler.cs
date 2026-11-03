using MonAssurance.Domain.Eligibility;

namespace MonAssurance.Application.Eligibility.Queries.CheckEligibility;

public sealed class CheckEligibilityQueryHandler
{
    private readonly EligibilityPolicy policy;
    private readonly TimeProvider timeProvider;

    public CheckEligibilityQueryHandler(EligibilityPolicy policy, TimeProvider timeProvider)
    {
        this.policy = policy;
        this.timeProvider = timeProvider;
    }

    public EligibilityViewModel Handle(CheckEligibilityQuery query)
    {
        var today = DateOnly.FromDateTime(timeProvider.GetUtcNow().UtcDateTime);
        var driver = new Driver(query.DateOfBirth, query.LicenseYears);
        var vehicle = new Vehicle(query.VehicleType, query.Power);

        return policy
            .Evaluate(driver, vehicle, today)
            .Match(
                onAccepted: () => EligibilityViewModel.Accepted(),
                onRefused: reason => EligibilityViewModel.Refused(reason)
            );
    }
}
