namespace MonAssurance.Domain.Eligibility;

public sealed class Driver
{
    private readonly DateOnly dateOfBirth;
    private readonly DrivingLicense license;

    public Driver(DateOnly dateOfBirth, int licenseYears, bool isLicenseSuspended = false)
    {
        this.dateOfBirth = dateOfBirth;
        license = new DrivingLicense(licenseYears, isLicenseSuspended);
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

    public bool HasSuspendedLicense() => license.IsSuspended();

    public bool HasEnoughExperience(int minimumYears) => license.IsOldEnough(minimumYears);
}
