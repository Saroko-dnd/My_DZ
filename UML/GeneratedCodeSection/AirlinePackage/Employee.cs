
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class Employee : Person , IEmployee {

    /**
     * 
     */
    public Employee() {
    }

    /**
     * 
     */
    public int Salary;

    /**
     * 
     */
    public int Experience;

    /**
     * 
     */
    public ProfessionalismLevel LevelOfProfessionalism;

    /**
     * @param NewSalary 
     * @param NewExperience 
     * @param NewProfessionalismLevel
     */
    protected void Employee(int NewSalary, int NewExperience, ProfessionalismLevel NewProfessionalismLevel) {
        // TODO implement here
    }

}