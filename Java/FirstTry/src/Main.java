import java.awt.*;
import java.util.Scanner;
import java.util.*;

public class Main {

    public static void main(String[] args) {
        System.out.println("Hello World!");
        System.err.println("Test error");
        int FirstInt;
        int SecondInt;
        /*Scanner TestScanner = new Scanner(System.in);

        System.out.println("Enter first int:");
        FirstInt = TestScanner.nextInt();
        System.out.println("Enter second int:");
        SecondInt = TestScanner.nextInt();

        System.out.println(FirstInt + " + " + SecondInt + " = " + (FirstInt + SecondInt));*/

        Student TestStudent = new Student(20,"Alex", Color.black);

        System.out.println(TestStudent);

        Integer[] IntArray = new Integer[100];
        System.out.println("Length of test array: " + IntArray.length);

        java.util.List<Student> ListOfStudents = new ArrayList<Student>();
        ListOfStudents.add(new Student(20,"Alex", Color.black));
        ListOfStudents.add(new Student(15,"Gleb", Color.BLUE));
        ListOfStudents.add(new Student(25,"Flex", Color.GREEN));

        for (int Counter = 0; Counter < ListOfStudents.size(); ++Counter)
        {
            System.out.println(ListOfStudents.get(Counter));
        }

        TreeMap<Integer,String> TestMap = new TreeMap<Integer,String>();
        TestMap.put(10,"first number");
        TestMap.put(15,"second number");
        TestMap.put(20,"third number");

        TestMap.forEach((key, value) -> {
            System.out.println("Key : " + key + " Value : " + value);
        });

        //Prints integer as binary
        System.out.println(Integer.toString(-2147483647,2));

    }
}
