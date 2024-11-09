using System;
using System.Collections.Generic;
using System.Text;

public class ParkingInterval
{
    private ParkingSpot parkingSpot;
    public ParkingSpot ParkingSpot
    {
        get
        {
            return this.parkingSpot;
        }
        set
        {
            this.parkingSpot = value;
        }
    }
    private string registrationPlate;
    public string RegistrationPlate
    {
        get
        {
            return this.registrationPlate;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"Registration plate can’t be null or empty!");
            }
            this.registrationPlate = value;
        }
    }
    private int hoursParked;
    public int HoursParked
    {
        get
        {
            return this.hoursParked;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException($"Hours parked can’t be zero or negative!");
            }
            this.hoursParked = value;
        }
    }
    public double Revenue
    {
        get
        {
            if (parkingSpot.Type == "subscription")
            {
                return 0;
            }
            return this.hoursParked * parkingSpot.Price;
        }
    }
    public ParkingInterval(ParkingSpot parkingSpot, string registrationPlate, int hoursParked)
    {
        this.ParkingSpot = parkingSpot;
        this.RegistrationPlate = registrationPlate;
        this.HoursParked = hoursParked;
    }
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"> Parking Spot #{parkingSpot.Id}");
        builder.AppendLine($"> RegistrationPlate: {RegistrationPlate}");
        builder.AppendLine($"> HoursParked: {HoursParked}");
        builder.AppendLine($"> Revenue: {Revenue:f2} BGN");
        return builder.ToString();
    }
}
