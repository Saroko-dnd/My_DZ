import org.hibernate.Session;
import org.hibernate.SessionFactory;

import javax.persistence.Query;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by master on 2017/01/28.
 */
//SET GLOBAL time_zone = '+8:00'; way of setting timezone for mysql
public class Main {
    public static void main(String[] args) {
        System.out.println("Start");

        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();

        session.beginTransaction();
        //ONE TO MANY EXAMPLE
        Producer FoundProducer = (Producer) session.get(Producer.class, 1);
        /*Producer TestProducer = new Producer(1980, "TestProducerName",
                "www.SuperProducer.com" );
        session.save(TestProducer);*/
        System.out.println(FoundProducer.get_setOfProducts().size());
        /*Product TestProduct_1 = new Product(1500, "TestProductName_2" , FoundProducer);
        Product TestProduct_2 = new Product(1250, "TestProductName_3" , FoundProducer);
        FoundProducer.get_setOfProducts().add(TestProduct_1);
        FoundProducer.get_setOfProducts().add(TestProduct_2);

        session.save(TestProduct_1);
        session.save(TestProduct_2);*/

        session.getTransaction().commit();
        //-----------------------------------------------------------
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

        //Criteria example (new version) select all products with _cost == 500
        /*CriteriaBuilder TestCriteriaBuilder = session.getCriteriaBuilder();
        CriteriaQuery<Product> TestCriteriaQuery = TestCriteriaBuilder.createQuery( Product.class );
        Root<Product> TestRoot = TestCriteriaQuery.from( Product.class );
        TestCriteriaQuery.select( TestRoot );
        TestCriteriaQuery.where( TestCriteriaBuilder.equal( TestRoot.get( "_cost" ), 500 ) );
        List<Product> ListOfCheapProductsInDB = session.createQuery(TestCriteriaQuery).getResultList();

        for (Product FoundProduct : ListOfCheapProductsInDB) {
            System.out.println(FoundProduct);
        }*/

        /*session.beginTransaction();

        Product FoundProduct = (Product) session.get(Product.class, 2);*/
        //Updating entity selected by Id
        /*FoundProduct.set_name("_UpdatedName");
        session.update(FoundProduct);*/

        //Deleting entity selected by Id from db
        //session.delete(FoundProduct);
        //session.getTransaction().commit();

        session.close();

        System.out.println("End");

            System.exit(0);
    }
}
