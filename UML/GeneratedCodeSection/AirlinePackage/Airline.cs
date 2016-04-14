
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Airline {

    /**
     * 
     */
    public Airline() {
    }

    /**
     * 
     */
    public HashSet<List<Airliner>> ListOfAirliners;

    /**
     * 
     */
    public HashSet<List<CargoAirplane>> ListOfCargoAirplanes;

    /**
     * 
     */
    public HashSet<List<CargoFlight>> ListOfCargoFlights;

    /**
     * 
     */
    public HashSet<List<PassengerFlight>> ListOfPassengerFlights;

    /**
     * 
     */
    public string CompanyName;

    /**
     * 
     */
    public long Budget;

    /**
     * 
     */
    public HashSet<List<Ticket>> Tickets;

    /**
     * 
     */
    public HashSet<List<Loader>> ListOfLoaders;

    /**
     * 
     */
    public HashSet<List<Airplane>> ListOfAirplanes;

    /**
     * 
     */
    public HashSet<List<Flight>> ListOfFlights;

    /**
     * 
     */
    public HashSet<List<IEmployee>> ListOfEmployees;







    /**
     * @param Name
     */
    public void Airline(string Name) {
        // TODO implement here
    }

    /**
     * @param Customer 
     * @param AirlinerForSelling 
     * @return
     */
    public void SellAirliner(Airline Customer, Airliner AirlinerForSelling) {
        // TODO implement here
        return null;
    }

    /**
     * @param Dealer 
     * @param DesiredAirliner 
     * @return
     */
    public void BuyAirliner(Airline Dealer, Airliner DesiredAirliner) {
        // TODO implement here
        return null;
    }

    /**
     * @param Dealer 
     * @param DesiredCargoAirplane 
     * @return
     */
    public void BuyCargoAirplane(Airline Dealer, CargoAirplane DesiredCargoAirplane) {
        // TODO implement here
        return null;
    }

    /**
     * @param Customer 
     * @param CargoAirplaneForSelling 
     * @return
     */
    public void SellCargoAirplane(Airline Customer, CargoAirplane CargoAirplaneForSelling) {
        // TODO implement here
        return null;
    }

    /**
     * @param NewFlight 
     * @return
     */
    public void AddNewFlight(Flight NewFlight) {
        // TODO implement here
        return null;
    }

    /**
     * @param CurrentFlight 
     * @return
     */
    public void RemoveFlight(Flight CurrentFlight) {
        // TODO implement here
        return null;
    }

    /**
     * @param NewEmployee 
     * @return
     */
    public void HireNewEmployee(IEmployee NewEmployee) {
        // TODO implement here
        return null;
    }

    /**
     * @param CurrentEmployee
     */
    public void FireEmployee(IEmployee CurrentEmployee) {
        // TODO implement here
    }

}