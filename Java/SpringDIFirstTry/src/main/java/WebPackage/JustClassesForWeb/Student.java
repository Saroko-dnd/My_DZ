package WebPackage.JustClassesForWeb;

import org.codehaus.jackson.annotate.JsonAutoDetect;
import org.codehaus.jackson.annotate.JsonAutoDetect.Visibility;
/**
 * Created by admin on 25.02.2017.
 */
/*@JsonAutoDetect(fieldVisibility = JsonAutoDetect.Visibility.ANY, getterVisibility = Visibility.NONE,
        setterVisibility = Visibility.NONE)*/
public class Student {
    private int Age;
    private String Name;
    private String SecondName;

   /* public String toString()
    {
        return  "Age: " + Age + " Name: " +  Name + " Second name: " + SecondName;
    }*/

    public int getAge() {
        return Age;
    }

    public void setAge(int age) {
        Age = age;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getSecondName() {
        return SecondName;
    }

    public void setSecondName(String secondName) {
        SecondName = secondName;
    }
}
