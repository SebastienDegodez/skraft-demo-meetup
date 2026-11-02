namespace MonAssurance.Domain.Eligibility;

internal sealed class DrivingLicense
{
    private readonly int years;
    private readonly bool isSuspended;

    public DrivingLicense(int years, bool isSuspended)
    {
        this.years = years;
        this.isSuspended = isSuspended;
    }

    public bool IsSuspended() => isSuspended;

    public bool IsOldEnough(int minimumYears) => years >= minimumYears;
}
