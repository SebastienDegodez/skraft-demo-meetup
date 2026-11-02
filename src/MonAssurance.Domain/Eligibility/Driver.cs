namespace MonAssurance.Domain.Eligibility;

public sealed class Driver
{
    private readonly DateOnly dateOfBirth;
    private readonly int licenseYears;
    private readonly bool isLicenseSuspended;

    public Driver(DateOnly dateOfBirth, int licenseYears, bool isLicenseSuspended = false)
    {
        this.dateOfBirth = dateOfBirth;
        this.licenseYears = licenseYears;
        this.isLicenseSuspended = isLicenseSuspended;
    }

    public int Age(DateOnly today)
    {
        var age = today.Year - dateOfBirth.Year;
        if (today < dateOfBirth.AddYears(age))
        {
            age--;
        }

        return age;
    }

    public bool HasSuspendedLicense() => isLicenseSuspended;

    public bool HasEnoughExperience(int minimumYears) => licenseYears >= minimumYears;
}
