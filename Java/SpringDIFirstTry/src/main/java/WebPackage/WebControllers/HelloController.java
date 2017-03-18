package WebPackage.WebControllers;

import DBPackage.DBWorker;
import DBPackage.DB_DI_Providers.DBDIConfiguration;
import DBPackage.HibernateUtil;
import DBPackage.IDBWorker;
import DBPackage.UniversalDBWorker;
import DIProviders.DIConfiguration;
import WebPackage.JustClassesForWeb.Student;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import org.springframework.http.HttpRequest;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;
import org.springframework.ui.ModelMap;

import org.springframework.web.servlet.ModelAndView;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by admin on 25.02.2017.
 */
@Controller
//@RequestMapping("/HelloPage")
public class HelloController {

    static List<Student> Students = new ArrayList<Student>();

    @Autowired
    IDBWorker idbWorker;


    @RequestMapping(value = "/addNewStudentToDb", method = RequestMethod.POST)
    @ResponseStatus(value = HttpStatus.OK)
    public void AddNewStudentToDb(@RequestParam(value = "Age") int newAge,
                                  @RequestParam(value = "Name") String newName,
                                  @RequestParam(value = "SecondName") String newSecondName) {
        /*AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DBDIConfiguration.class);
        UniversalDBWorker CurrentDBWorker = context.getBean(UniversalDBWorker.class);*/
        idbWorker.SaveNewStudentToDB(new Student(newAge, newName, newSecondName));

    }

    @RequestMapping(value = "/studentsInfo", method = RequestMethod.GET)
    @ResponseBody
    public String AddNewStudentToDb() {
        ObjectMapper mapper = new ObjectMapper();
        try {
            /*AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DBDIConfiguration.class);
            UniversalDBWorker CurrentDBWorker = context.getBean(UniversalDBWorker.class);*/
            return mapper.writeValueAsString(idbWorker.GetAllStudents());
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return "";
    }

    @RequestMapping(value = "/students", method = RequestMethod.GET)
    public String ReturnBasePage() {
        return "StudentsPage";
    }

    /*@RequestMapping(value = "/temperature", method = RequestMethod.GET)
    @ResponseBody
    public Student returnTemperature(ModelMap model) {
        Student TestStudent = new Student();
        TestStudent.setAge(40);
        TestStudent.setName("fffff");
        TestStudent.setSecondName("GGGGG");
        return TestStudent;
    }

    @RequestMapping(value = "/students", method = RequestMethod.GET)
    public String printWelcome(ModelMap model) {
        StringBuffer BufferForStudents = new StringBuffer();
        for (Student FoundStudent : Students) {
            BufferForStudents.append("<p>");
            BufferForStudents.append(FoundStudent.toString());
            BufferForStudents.append("</p>");
        }
        model.addAttribute("Students", BufferForStudents.toString());
        return "StudentsPage";
    }

    @RequestMapping(value = "/createNewObject", method = RequestMethod.POST)
    public ModelAndView printHurry(@ModelAttribute Student newStudent) {
        Students.add(newStudent);
        return new ModelAndView("redirect:/students");
        //return "StudentsPage";
    }

    @RequestMapping(value = "/getObjectAsJson", method = RequestMethod.POST)
    @ResponseBody
    public String ReturnStudent(@RequestBody String SomeDataFromUser) {
        Student TestStudent = new Student();
        TestStudent.setAge(40);
        TestStudent.setName("First Name");
        TestStudent.setSecondName("Second Name");
        ObjectMapper mapper = new ObjectMapper();
        try {
            return mapper.writeValueAsString(TestStudent);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return "";
    }*/
}
