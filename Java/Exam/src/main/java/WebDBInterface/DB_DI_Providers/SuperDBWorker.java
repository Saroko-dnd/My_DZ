package WebDBInterface.DB_DI_Providers;

import WebDBInterface.DBEntities.Producer;
import WebDBInterface.DBEntities.Product;
import WebDBInterface.HibernateConfig.HibernateUtil;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.springframework.stereotype.Component;

import java.util.List;

/**
 * Created by admin on 01.04.2017.
 */
@Component
public class SuperDBWorker implements IDataBaseWorker {
    public void SaveNewProductToDB(Product newProduct) {

    }

    public void SaveNewProducerToDB(Producer newProducer) {
        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();
        session.beginTransaction();

        session.save(newProducer);
        session.close();
    }

    public void UpdateProduct(Product productToUpdate) {

    }

    public void UpdateProducук(Producer producerToUpdate) {

    }

    public List<Product> GetAllProducts() {
        return null;
    }

    public List<Producer> GetAllProducers() {
        return null;
    }

    public void RemoveProductFromDB(Product currentProduct) {

    }

    public void RemoveProducerFromDB(Producer currentProduct) {

    }
}
