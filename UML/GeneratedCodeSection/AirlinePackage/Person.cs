
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class Person {

    /**
     * 
     */
    public Person() {
    }

    /**
     * 
     */
    public string FirstName;

    /**
     * 
     */
    public string SecondName;

    /**
     * 
     */
    public byte Age;

    /**
     * @param NewPersonFirstName 
     * @param NewPersonSecondName 
     * @param NewPersonAge
     */
    protected void Person(string NewPersonFirstName, string NewPersonSecondName, int NewPersonAge) {
        // TODO implement here
    }

}