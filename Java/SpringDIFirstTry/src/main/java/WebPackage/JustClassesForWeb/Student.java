package WebPackage.JustClassesForWeb;

import org.codehaus.jackson.annotate.JsonAutoDetect;
import org.codehaus.jackson.annotate.JsonAutoDetect.Visibility;

import javax.persistence.*;

/**
 * Created by admin on 25.02.2017.
 */
/*@JsonAutoDetect(fieldVisibility = JsonAutoDetect.Visibility.ANY, getterVisibility = Visibility.NONE,
        setterVisibility = Visibility.NONE)*/
@Entity
@Table(name = "Students")
public class Student {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Student_Id")
    private int StudentId;
    @Column(name = "Age")
    private int Age;
    @Column(name = "Name")
    private String Name;
    @Column(name = "SecondName")
    private String SecondName;

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

    public Student(int NewAge, String NewName, String NewSecondName){
        Age = NewAge;
        Name = NewName;
        SecondName = NewSecondName;
    }

    public Student(){

    }
}
