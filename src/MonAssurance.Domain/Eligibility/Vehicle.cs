namespace MonAssurance.Domain.Eligibility;

public sealed class Vehicle
{
    private readonly VehicleType type;
    private readonly int? power;

    public Vehicle(VehicleType type, int? power)
    {
        this.type = type;
        this.power = power;
    }

    public int MinimumAge() => type == VehicleType.ElectricScooter ? 16 : 18;

    // Convention: null power treated as < 100hp — no experience rule triggered.
    public bool IsHighPowerMotorcycle() => type == VehicleType.Motorcycle && power > 100;
}
