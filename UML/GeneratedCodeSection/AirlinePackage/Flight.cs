
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class Flight {

    /**
     * 
     */
    public Flight() {
    }

    /**
     * 
     */
    public string From;

    /**
     * 
     */
    public string To;

    /**
     * 
     */
    public Airplane CurrentAirplane;

    /**
     * 
     */
    public TimeSpan DepartureTime;

    /**
     * 
     */
    public TimeSpan ArrivalTime;

    /**
     * 
     */
    public char[] FlightID;

    /**
     * @param NewID 
     * @param NewSourceTown 
     * @param NewDestinationTown 
     * @param CurrentAirplane
     */
    protected void Flight(char[] NewID, string NewSourceTown, string NewDestinationTown, Airplane CurrentAirplane) {
        // TODO implement here
    }

}