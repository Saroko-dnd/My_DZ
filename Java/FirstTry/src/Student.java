/**
 * Created by admin on 14.01.2017.
 */
import java.awt.*;

public class Student {
    public int Age;
    public String Name;
    public Color HairColor;

    public Student()
    {

    }

    public Student(int NewAge, String NewName, Color NewHairColor)
    {
        this.Age = NewAge;
        this.Name = NewName;
        this.HairColor = NewHairColor;
    }

    public String toString()
    {
        return "Name: " + Name + " Age: " + Age + " Hair color: " + HairColor.toString();
    }
}
