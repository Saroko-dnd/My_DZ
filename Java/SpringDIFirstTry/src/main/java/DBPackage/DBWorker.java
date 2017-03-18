package DBPackage;

import WebPackage.JustClassesForWeb.Student;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.stereotype.Component;

import java.util.List;

/**
 * Created by admin on 11.03.2017.
 */
@Component
public class DBWorker implements IDBWorker {
    public void  SaveNewStudentToDB(Student NewStudent)
    {
        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();
        session.beginTransaction();

        session.save(NewStudent);
        session.close();
    }

    public List<Student> GetAllStudents(){
        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();
        session.beginTransaction();

        List<Student> ListOfAllProductsInDB = session.createQuery("from Student").list();

        session.close();
        return ListOfAllProductsInDB;
    }
}
