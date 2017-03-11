package DBPackage;

import WebPackage.JustClassesForWeb.Student;
import org.hibernate.Session;
import org.hibernate.SessionFactory;

import java.util.List;

/**
 * Created by admin on 11.03.2017.
 */
public interface IDBWorker {

    public void  SaveNewStudentToDB(Student NewStudent);

    public List<Student> GetAllStudents();
}
