package WebPackage.JustClassesForWeb;

/**
 * Created by admin on 25.02.2017.
 */
public class Student {
    public int Age;
    public String Name;
    public String SecondName;

    public String toString()
    {
        return  "Age: " + Age + " Name: " +  Name + " Second name: " + SecondName;
    }


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
