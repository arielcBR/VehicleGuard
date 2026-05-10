namespace VehicleGuard.Shared.Domain.Enums;

public enum Command
{
    None = 0,
    LockDoors = 1,
    UnlockDoors = 2,
    FuelPumpOn = 3,
    FuelPumpOff = 4,
    EmergencyLightsOn = 5,
    EmergencyLightsOff = 6,
    BuzzOn = 7,
    BuzzOff = 8,
}