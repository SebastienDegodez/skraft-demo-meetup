namespace MonAssurance.Domain.Eligibility;

internal sealed class DrivingLicense
{
    private readonly LicenseYears years;
    private readonly bool isSuspended;

    public DrivingLicense(LicenseYears years, bool isSuspended)
    {
        this.years = years;
        this.isSuspended = isSuspended;
    }

    public bool AllowsDriving() => !isSuspended;

    public bool HasBeenHeldFor(LicenseYears minimum) => years.AtLeast(minimum);
}
