import org.hibernate.Session;
import org.hibernate.SessionFactory;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by master on 2017/01/28.
 */
public class Main {
    public static void main(String[] args) {
        System.out.println("Start");

        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();

        //Adding new entities to db
        /*session.beginTransaction();

        Product product_1 = new Product(500, "p","manufacturer_");
        Product product_2 = new Product(500, "pr","manufacturer");
        Product product_3 = new Product(1000, "rrrrrtt","tytytyt");

        session.save(product_1);
        session.save(product_2);
        session.save(product_3);

        session.getTransaction().commit();*/

        //Obtaining all entities from db
        /*List<Product> ListOfAllProductsInDB = session.createQuery("from Product").list();

        for (Product FoundProduct : ListOfAllProductsInDB) {
            System.out.println(FoundProduct);
        }*/

        session.beginTransaction();

        Product FoundProduct = (Product) session.get(Product.class, 2);
        //Updating entity selected by Id
        FoundProduct.set_name("_UpdatedName");
        session.update(FoundProduct);

        //Deleting entity selected by Id from db
        //session.delete(FoundProduct);
        session.getTransaction().commit();

        session.close();

        System.out.println("End");

        System.exit(0);
    }
}
