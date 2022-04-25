using System;
using System.Collections.Generic;

namespace Z3
{
    public class Airplane
    {

    }

    public class Airport
    {
        public Airport(int _capacity)
        {
            if (_capacity <= 0)
                throw new ArgumentOutOfRangeException("Wrong capacity");

            capacity  = _capacity;
            available = new List<Airplane> { };
            inUse     = new List<Airplane> { };
        }
        public Airplane GetAirplane()
        {
            if (inUse.Count == capacity)
                throw new Exception("All airplanes are in use");
            if(available.Count != 0)
            {
                Airplane airplane = available[0];
                inUse.Add(airplane);
                available.RemoveAt(0);
                return airplane;
            }

            Airplane newAirplane = new Airplane();
            inUse.Add(newAirplane);
            return newAirplane;
        }

        public void ReleaseAirplane(Airplane airplane)
        {
            if(!inUse.Contains(airplane))
            {
                throw new Exception("Plane cannot be released");
            }
            inUse.Remove(airplane);
            available.Add(airplane);
        }

        private int capacity;
        private List<Airplane> available;
        private List<Airplane> inUse;

        public static void Main(string[] param)
        {

        }
    }
}