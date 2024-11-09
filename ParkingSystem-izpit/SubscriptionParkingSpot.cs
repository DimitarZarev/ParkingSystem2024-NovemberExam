using System;
using System.Collections.Generic;
using System.Text;

internal class SubscriptionParkingSpot : ParkingSpot
{
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

    public SubscriptionParkingSpot(int id, bool occupied, double price, string registrationPlate) : base(id, occupied, "subscription", price)
    {
        this.RegistrationPlate = registrationPlate;
    }

    public override bool ParkVehicle(string registrationPlate, int hoursParked, string type)
    {
        if (this.Occupied || this.RegistrationPlate != registrationPlate)
        {
            return false;
        }
        this.parkingIntervals.Add(new ParkingInterval(this, registrationPlate, hoursParked));
        this.Occupied = true;
        return true;
    }


    public override double CalculateTotal()
    {
        return 0;
    }
}

