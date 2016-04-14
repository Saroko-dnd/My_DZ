
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class CrewMember : Employee {

    /**
     * 
     */
    public CrewMember() {
    }

    /**
     * 
     */
    public Rank CurrentRank;

    /**
     * 
     */
    public int NumberOfFlyingHours;


    /**
     * @param NewLevelOfProfessionalism 
     * @param NewPremium 
     * @param NewSalary 
     * @param NewRank 
     * @param NewFlyingHours
     */
    protected void CrewMember(ProfessionalismLevel NewLevelOfProfessionalism, int NewPremium, int NewSalary, Rank NewRank, int NewFlyingHours) {
        // TODO implement here
    }

}