namespace MonAssurance.Domain.Eligibility;

internal readonly record struct LicenseYears
{
    private readonly int value;

    public LicenseYears(int value)
    {
        this.value = value;
    }

    public bool AtLeast(LicenseYears minimum) => value >= minimum.value;
}
