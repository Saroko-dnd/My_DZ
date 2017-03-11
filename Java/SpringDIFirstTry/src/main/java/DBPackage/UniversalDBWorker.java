package DBPackage;

import DIUsers.ICalculator;
import WebPackage.JustClassesForWeb.Student;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.List;

/**
 * Created by admin on 11.03.2017.
 */
@Component
@Scope("singleton")
public class UniversalDBWorker {
    private IDBWorker DBWorkerService;

    @Autowired
    @Qualifier("Simple")
    public void setService(IDBWorker svc){
        this.DBWorkerService = svc;
    }

    public void AddNewStudentToDB(Student newStudent){
        //some magic like validation, logging etc
        this.DBWorkerService.SaveNewStudentToDB(newStudent);
    }

    public List<Student> GetAllStudents(){
        //some magic like validation, logging etc
        return this.DBWorkerService.GetAllStudents();
    }
}
