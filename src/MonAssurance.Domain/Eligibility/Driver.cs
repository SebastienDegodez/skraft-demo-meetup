namespace MonAssurance.Domain.Eligibility;

public sealed class Driver
{
    private readonly DateOnly _dateOfBirth;
    private readonly int _licenseYears;
    private readonly bool _isLicenseSuspended;

    public Driver(DateOnly dateOfBirth, int licenseYears, bool isLicenseSuspended = false)
    {
        _dateOfBirth = dateOfBirth;
        _licenseYears = licenseYears;
        _isLicenseSuspended = isLicenseSuspended;
    }

    public int Age(DateOnly today)
    {
        var age = today.Year - _dateOfBirth.Year;
        if (today < _dateOfBirth.AddYears(age)) age--;
        return age;
    }

    public bool HasSuspendedLicense() => _isLicenseSuspended;

    public bool HasEnoughExperience(int minimumYears) => _licenseYears >= minimumYears;
}
