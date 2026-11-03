namespace MonAssurance.Domain.Eligibility;

public sealed class Driver
{
    private readonly DateOnly dateOfBirth;
    private readonly int licenseYears;

    public Driver(DateOnly dateOfBirth, int licenseYears)
    {
        this.dateOfBirth = dateOfBirth;
        this.licenseYears = licenseYears;
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

    public bool HasEnoughExperience(int minimumYears) => licenseYears >= minimumYears;
}
