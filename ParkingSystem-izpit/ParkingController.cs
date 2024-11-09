using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class ParkingController
{
    private List<ParkingSpot> parkingSpots;

    public ParkingController()
    {
        parkingSpots = new List<ParkingSpot>();
    }

    public string CreateParkingSpot(List<string> args)
    {
        int id = int.Parse(args[0]);
        bool occupied = bool.Parse(args[1]);
        string type = args[2];
        double price = double.Parse(args[3]);
        if (type == "car")
        {
            ParkingSpot spot = new CarParkingSpot(id, occupied, price);
            if (parkingSpots.Contains(spot))
            {
                return $"Parking spot {id} is already registered!";
            }
            parkingSpots.Add(spot);
            return $"Parking spot {spot.Id} was successfully registered in the system!";
        }
        else if (type == "bus")
        {
            ParkingSpot spot = new BusParkingSpot(id, occupied, price);
            if (parkingSpots.Contains(spot))
            {
                return $"Parking spot {id} is already registered!";
            }
            parkingSpots.Add(spot);
            return $"Parking spot {spot.Id} was successfully registered in the system!";
        }
        else if (type == "subscription")
        {
            string registrationPlate = args[4];
            ParkingSpot spot = new SubscriptionParkingSpot(id, occupied, price, registrationPlate);
            if (parkingSpots.Contains(spot))
            {
                return $"Parking spot {id} is already registered!";
            }
            parkingSpots.Add(spot);
            return $"Parking spot {spot.Id} was successfully registered in the system!";
        }
        return $"Unable to create parking spot!";
    }

    public string ParkVehicle(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        string registrationPlate = args[1];
        int hoursParked = int.Parse(args[2]);
        string type = args[3];

        var searchPark = parkingSpots.FirstOrDefault(x => x.Id == parkingSpotId);
        if (searchPark == null)
        {
            return $"Parking spot {parkingSpotId} not found!";
        }

        if (searchPark.Occupied || searchPark.Type != type)
        {
            return $"Vehicle {registrationPlate} can't park at {parkingSpotId}.";
        }

        searchPark.ParkVehicle(registrationPlate, hoursParked, type);
        return $"Vehicle {registrationPlate} parked at {parkingSpotId} for {hoursParked} hours.";
    }


    public string FreeParkingSpot(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        var spot = parkingSpots.FirstOrDefault(x => x.Id == parkingSpotId);
        if (spot != null)
        {
            if (spot.Occupied == true)
            {
                spot.Occupied = false;
                return $"Parking spot {parkingSpotId} is now free!";
            }
            return $"Parking spot {parkingSpotId} is not occupied.";
        }
        return $"Parking spot {parkingSpotId} not found!";
    }

    public string GetParkingSpotById(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        var spot = parkingSpots.FirstOrDefault(x => x.Id == parkingSpotId);
        if (spot != null)
        {
            return spot.ToString();
        }
        return $"Parking spot {parkingSpotId} not found!";
    }

    public string GetParkingIntervalsByParkingSpotIdAndRegistrationPlate(List<string> args)
    {
        int parkingSpotId = int.Parse(args[0]);
        string registrationPlate = args[1];
        var spot = parkingSpots.FirstOrDefault(x => x.Id == parkingSpotId);
        if (spot != null)
        {
            List<ParkingInterval> parkInterval = spot.GetAllParkingIntervalsByRegistrationPlate(registrationPlate);
            StringBuilder builder = new StringBuilder();
            foreach (var park in parkInterval)
            {
                builder.AppendLine(park.ToString());
            }
            return builder.ToString().Trim();
        }
        return $"Parking spot {parkingSpotId} not found!";
    }

    public string CalculateTotal(List<string> args)
    {
        // Проверка дали са подадени аргументи
        if (args.Count == 2)
        {
            // Изчисляване на приходите само за конкретно място и регистрационен номер
            int parkingSpotId = int.Parse(args[0]);
            string registrationPlate = args[1];

            var spot = parkingSpots.FirstOrDefault(x => x.Id == parkingSpotId);
            if (spot == null)
            {
                return $"Parking spot {parkingSpotId} not found!";
            }

            var parkingIntervals = spot.GetAllParkingIntervalsByRegistrationPlate(registrationPlate);
            double total = parkingIntervals.Sum(x => x.Revenue);

            return $"Total revenue from parking spot {parkingSpotId} for vehicle {registrationPlate}: {total:F2} BGN";
        }
        else
        {
            // Общото изчисляване на приходите за всички места
            double total = parkingSpots
                .Where(spot => spot.GetType() != typeof(SubscriptionParkingSpot))
                .Sum(spot => spot.CalculateTotal());

            return $"Total revenue from the parking: {total:F2} BGN";
        }
    }




}


