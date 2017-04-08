package WebDBInterface.DB_DI_Providers;

import WebDBInterface.DBEntities.Producer;
import WebDBInterface.DBEntities.Product;
import WebDBInterface.HibernateConfig.HibernateUtil;
import org.apache.commons.beanutils.BeanUtils;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.stereotype.Component;

import java.lang.reflect.InvocationTargetException;
import java.util.List;

/**
 * Created by admin on 01.04.2017.
 */
@Component
public class SuperDBWorker implements IDataBaseWorker {
    public void SaveNewProductToDB(Product newProduct, int ProducerId) {
        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();
        session.beginTransaction();

        Producer FoundProducer = (Producer) session.get(Producer.class, ProducerId);
        newProduct.setProducer(FoundProducer);
        FoundProducer.getSetOfProducts().add(newProduct);
        session.save(newProduct);
        session.getTransaction().commit();

        session.close();
    }

    public void SaveNewProducerToDB(Producer newProducer) {
        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();
        session.beginTransaction();

        session.save(newProducer);
        session.close();
    }

    public void UpdateProduct(Product productToUpdate) {
        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();
        session.beginTransaction();

        Product FoundProduct = (Product) session.get(Product.class, productToUpdate.getId());
        try {
            BeanUtils.copyProperties(FoundProduct, productToUpdate);
            session.update(FoundProduct);
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        }

        session.close();
    }

    public void UpdateProducer(Producer producerToUpdate) {

    }

    public List<Product> GetAllProducts() {
        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();
        session.beginTransaction();

        List<Product> ListOfAllProductsInDB = session.createQuery("from Product").list();

        session.close();
        return ListOfAllProductsInDB;
    }

    public List<Producer> GetAllProducers() {
        return null;
    }

    public void RemoveProductFromDB(Product currentProduct) {

    }

    public void RemoveProducerFromDB(Producer currentProduct) {

    }
}
