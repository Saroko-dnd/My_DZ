package WebDBInterface.DB_DI_Providers;

import WebDBInterface.DBEntities.Producer;
import WebDBInterface.DBEntities.Product;

import java.util.List;

/**
 * Created by admin on 01.04.2017.
 */
public interface IDataBaseWorker {
    void SaveNewProductToDB(Product newProduct);
    void SaveNewProducerToDB(Producer newProducer);
    void UpdateProduct(Product productToUpdate);
    void UpdateProducук(Producer producerToUpdate);
    List<Product> GetAllProducts();
    List<Producer> GetAllProducers();
    void RemoveProductFromDB(Product currentProduct);
    void RemoveProducerFromDB(Producer currentProduct);
}
