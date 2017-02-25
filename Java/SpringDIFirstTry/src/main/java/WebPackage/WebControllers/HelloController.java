package WebPackage.WebControllers;

import WebPackage.JustClassesForWeb.Student;
import org.springframework.http.HttpRequest;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.ui.ModelMap;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.ResponseBody;
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

    @RequestMapping(value="/temperature",method = RequestMethod.GET)
    @ResponseBody
    public Student returnTemperature(ModelMap model) {
        Student TestStudent = new Student();
        TestStudent.Age = 40;
        TestStudent.Name = "fffff";
        TestStudent.SecondName = "GGGGG";
        return TestStudent;
    }

    @RequestMapping(value="/students",method = RequestMethod.GET)
    public String printWelcome(ModelMap model) {
        StringBuffer BufferForStudents = new StringBuffer();
        for(Student FoundStudent : Students)
        {
            BufferForStudents.append("<p>");
            BufferForStudents.append(FoundStudent.toString());
            BufferForStudents.append("</p>");
        }
        model.addAttribute("Students", BufferForStudents.toString());
        return "StudentsPage";
    }

    @RequestMapping(value="/createNewObject",method = RequestMethod.POST)
    public ModelAndView printHurry(@ModelAttribute Student newStudent) {
        Students.add(newStudent);
        return new ModelAndView("redirect:/students");
        //return "StudentsPage";
    }
}
