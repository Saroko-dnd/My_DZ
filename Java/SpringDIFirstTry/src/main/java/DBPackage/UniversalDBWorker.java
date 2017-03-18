package DBPackage;

import DBPackage.IDBWorker;
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

public class UniversalDBWorker {
    private IDBWorker DBWorkerService;

    @Autowired
    public void setService(IDBWorker svc){
        this.DBWorkerService = svc;
    }

    public void SaveNewStudentToDB(Student NewStudent) {
        this.DBWorkerService.SaveNewStudentToDB(NewStudent);
    }

    public List<Student> GetAllStudents(){
        //some magic like validation, logging etc
        return this.DBWorkerService.GetAllStudents();
    }
}
