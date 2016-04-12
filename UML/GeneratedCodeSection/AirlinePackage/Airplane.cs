
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class Airplane : IAirplane {

    /**
     * 
     */
    public Airplane() {
    }

    /**
     * 
     */
    public string Name;

    /**
     * 
     */
    public int FuelPerKilometer;

    /**
     * 
     */
    public int FlightDistance;

    /**
     * 
     */
    public long Cost;

}