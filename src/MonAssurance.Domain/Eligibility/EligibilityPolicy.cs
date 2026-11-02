namespace MonAssurance.Domain.Eligibility;

public sealed class EligibilityPolicy
{
    private static readonly LicenseYears HighPowerMotorcycleMinimumLicenseYears = new(5);

    public EligibilityResult Evaluate(Driver driver, Vehicle vehicle, DateOnly today)
    {
        if (!driver.CanDrive())
        {
            return EligibilityResult.Refused("Permis suspendu");
        }

        if (driver.Age(today) < vehicle.MinimumAge())
        {
            return EligibilityResult.Refused("Conducteur trop jeune pour ce véhicule");
        }

        if (vehicle.IsHighPowerMotorcycle() && !driver.HasHeldLicenseFor(HighPowerMotorcycleMinimumLicenseYears))
        {
            return EligibilityResult.Refused("Expérience insuffisante pour la puissance");
        }

        return EligibilityResult.Accepted();
    }
}
