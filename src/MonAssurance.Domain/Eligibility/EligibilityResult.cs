namespace MonAssurance.Domain.Eligibility;

public sealed class EligibilityResult
{
    private readonly bool isEligible;
    private readonly string? reason;

    private EligibilityResult(bool isEligible, string? reason)
    {
        this.isEligible = isEligible;
        this.reason = reason;
    }

    public static EligibilityResult Accepted() => new(true, null);
    public static EligibilityResult Refused(string reason) => new(false, reason);

    public T Match<T>(Func<T> onAccepted, Func<string, T> onRefused) =>
        isEligible ? onAccepted() : onRefused(reason!);
}
