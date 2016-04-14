
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Passenger : Person {

    /**
     * 
     */
    public Passenger() {
    }

    /**
     * 
     */
    public Ticket CurrentTicket = null;




    /**
     * @param NewFirstName 
     * @param NewSecondName 
     * @param NewAge
     */
    public void Passenger(string NewFirstName, string NewSecondName, int NewAge) {
        // TODO implement here
    }

    /**
     * @param CurrentFlight 
     * @param CurrentAirline 
     * @return
     */
    public bool BuyTicket(PassengerFlight CurrentFlight, Airline CurrentAirline) {
        // TODO implement here
        return False;
    }

}