using System;
using System.Text;

namespace QuestPark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Park park = new Park();
            bool isShow = true;
            while (isShow)
            {
                isShow = Menu(park);
            }
        }
        static bool Menu(Park park)
        {
            //mapping available command
            string[] command = Console.ReadLine().Split(' ');
            switch (command[0])
            {
                case "create_parking_lot":
                    park.create_parking_lot(command, park);
                    return true;
                case "park":
                    park.park(command, park);
                    return true;
                case "status":
                    park.status(command,park);
                    return true;
                case "type_of_vehicles":
                    park.type_of_vehicles(command, park);
                    return true;
                case "leave":
                    park.Slotleave(command, park);
                    return true;
                case "registration_numbers_for_vehicles_with_ood_plate":
                    park.RegNumberVehicleWithOodPlate(command, park);
                    return true;
                case "registration_numbers_for_vehicles_with_event_plate":
                    park.RegNumberVehicleWithEvenPlate(command, park);
                    return true;
                case "registration_numbers_for_vehicles_with_colour":
                    park.RegNumberVehicleWithColor(command, park);
                    return true;
                case "slot_numbers_for_vehicles_with_colour":
                    park.SlotNumberVehicleWithColor(command, park);
                    return true;
                case "slot_number_for_registration_number":
                    park.SlotNumberVehicle(command, park);
                    return true;
                case "exit":
                    Environment.Exit(1);
                    return false;
                default:
                    Console.WriteLine("Command Not Found\n");
                    return true;
            }
        }
        
        public class Park
        {
            public ParkSlot[] ParkSlotList { get; set; }
            public void create_parking_lot(string[] command, Park park)
            {
                try
                {
                    park.ParkSlotList = new ParkSlot[Convert.ToInt32(command[1])];
                    Console.Write("Created a parking lot with {0} slots\n", park.ParkSlotList.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void park(string[] command, Park park)
            {
                try
                {

                    if (park.ParkSlotList == null)
                        return;
                    ParkSlot parkSlot = new ParkSlot()
                    {
                        NumberVehicle = command[1],
                        Color = command[2],
                        Type = command[3],
                        Hour = 1
                    };
                    //check available lot
                    int findFirstIndex = Array.FindIndex(park.ParkSlotList, x => x == null);
                    if (findFirstIndex == -1)
                    {
                        Console.Write("Sorry, parking lot is full\n");
                    }
                    else
                    {
                        parkSlot.Slot = findFirstIndex + 1;
                        //assign to lot
                        park.ParkSlotList[findFirstIndex] = parkSlot;
                        Console.Write("Allocated slot number: {0}\n", parkSlot.Slot);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void status(string[] command, Park park)
            {
                try
                {
                    Console.Write("Slot\t\tNo.\tType\tRegistration No Colour\n");
                    if (park.ParkSlotList == null)
                        return;
                    var data = Array.FindAll(park.ParkSlotList, x => x != null);
                    foreach (var item in data)
                    {
                        Console.Write("{0}\t {1}\t {2}\t {3} \n", item.Slot, item.NumberVehicle, item.Type, item.Color);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void type_of_vehicles(string[] command, Park park)
            {
                try
                {
                    if (park.ParkSlotList == null)
                        return;
                    var typeVehcileCouter = Array.FindAll(park.ParkSlotList, x => x != null && x.Type == command[1]).Length;
                    Console.Write("{0}\n", typeVehcileCouter);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void Slotleave(string[] command, Park park)
            {
                try
                {

                    if (park.ParkSlotList == null)
                        return;
                    int slot = Convert.ToInt32(command[1]);
                    if (slot > park.ParkSlotList.Length)
                    {
                        Console.Write("Slot Not Available\n");
                        return;
                    }
                    park.ParkSlotList[slot - 1] = null;
                    Console.Write("Slot number {0} is free\n", slot);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void RegNumberVehicleWithOodPlate(string[] command, Park park)
            {
                try
                {

                    if (park.ParkSlotList == null)
                        return;
                    var ood = Array.FindAll(park.ParkSlotList, x => x != null && Convert.ToInt32(x.NumberVehicle.Split('-')[1]) % 2 != 0);
                    if (ood.Length == 0)
                    {
                        Console.Write("0");
                        return;
                    }
                    StringBuilder oodStr = new StringBuilder();
                    foreach (var item in ood)
                    {
                        oodStr.Append(string.Format("{0},", item.NumberVehicle));
                    }
                    Console.Write("{0}\n", oodStr.Remove(oodStr.Length - 1, 1));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void RegNumberVehicleWithEvenPlate(string[] command, Park park)
            {
                try
                {

                    if (park.ParkSlotList == null)
                        return;
                    var even = Array.FindAll(park.ParkSlotList, x => x != null && Convert.ToInt32(x.NumberVehicle.Split('-')[1]) % 2 == 0);
                    if (even.Length == 0)
                    {
                        Console.Write("0");
                        return;
                    }
                    StringBuilder evenStr = new StringBuilder();
                    foreach (var item in even)
                    {
                        evenStr.Append(string.Format("{0},", item.NumberVehicle));
                    }
                    Console.Write("{0}\n", evenStr.Remove(evenStr.Length - 1, 1));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void RegNumberVehicleWithColor(string[] command, Park park)
            {
                try
                {
                    if (park.ParkSlotList == null)
                        return;
                    var colorList = Array.FindAll(park.ParkSlotList, x => x != null && x.Color == command[1]);
                    StringBuilder colorListStr = new StringBuilder();
                    foreach (var item in colorList)
                    {
                        colorListStr.Append(string.Format("{0},", item.NumberVehicle));
                    }
                    Console.Write("{0}\n", colorListStr.Remove(colorListStr.Length - 1, 1));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void SlotNumberVehicleWithColor(string[] command,Park park)
            {
                try
                {
                    if (park.ParkSlotList == null)
                        return;
                    var slotColorList = Array.FindAll(park.ParkSlotList, x => x != null && x.Color == command[1]);
                    StringBuilder slotColorListStr = new StringBuilder();
                    foreach (var item in slotColorList)
                    {
                        slotColorListStr.Append(string.Format("{0},", item.Slot));
                    }
                    Console.Write("{0}\n", slotColorListStr.Remove(slotColorListStr.Length - 1, 1));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void SlotNumberVehicle(string[] command, Park park)
            {
                try
                {
                    if (park.ParkSlotList == null)
                        return;
                    var slotNumberVehicle = Array.FindAll(park.ParkSlotList, x => x != null && x.NumberVehicle == command[1]);
                    if (slotNumberVehicle.Length > 0)
                    {
                        StringBuilder slotNumberVehicleStr = new StringBuilder();
                        foreach (var item in slotNumberVehicle)
                        {
                            slotNumberVehicleStr.Append(string.Format("{0},", item.Slot));
                        }
                        Console.Write("{0}\n", slotNumberVehicleStr.Remove(slotNumberVehicleStr.Length - 1, 1));
                    }
                    else
                    {
                        Console.Write("Not Found\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public class ParkSlot
        {
            public int Slot { get; set; }
            public string NumberVehicle { get; set; }
            public string Type { get; set; }
            public string Color { get; set; }
            public int Hour { get; set; }
        }

    }
}
