package WebDBInterface.WebPart.Controllers;

import WebDBInterface.DBEntities.Producer;
import WebDBInterface.DBEntities.Product;
import WebDBInterface.DB_DI_Providers.IDataBaseWorker;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.SerializationFeature;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Created by admin on 01.04.2017.
 */
@Controller
public class DBInterfaceController {

    @Autowired
    IDataBaseWorker currentDBWorker;

    @RequestMapping(value = "/addNewProducerToDb", method = RequestMethod.POST)
    @ResponseStatus(value = HttpStatus.OK)
    public void AddNewProducerToDb(@RequestParam(value = "Name") String newName,
                                   @RequestParam(value = "Country") String newCountry,
                                   @RequestParam(value = "AnnualProfit") int newProfit) {
        /*AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DBDIConfiguration.class);
        UniversalDBWorker CurrentDBWorker = context.getBean(UniversalDBWorker.class);*/
        currentDBWorker.SaveNewProducerToDB(new Producer(newName, newCountry, newProfit));
    }

    @RequestMapping(value = "/addNewProductToDb", method = RequestMethod.POST)
    @ResponseStatus(value = HttpStatus.OK)
    public void AddNewProductToDb(@RequestParam(value = "Name") String newName,
                                  @RequestParam(value = "Price") int newPrice,
                                  @RequestParam(value = "Quantity") int newQuantity,
                                  @RequestParam(value = "Color") String newColor,
                                  @RequestParam(value = "ProducerId") int currentProducerId) {
        /*AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DBDIConfiguration.class);
        UniversalDBWorker CurrentDBWorker = context.getBean(UniversalDBWorker.class);*/
        currentDBWorker.SaveNewProductToDB(new Product(newPrice, newName, newQuantity, newColor), currentProducerId);
    }

    @RequestMapping(value = "/getAllProducts", method = RequestMethod.GET)
    @ResponseBody
    public String GetAllProducts() {
        /*AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DBDIConfiguration.class);
        UniversalDBWorker CurrentDBWorker = context.getBean(UniversalDBWorker.class);*/
        ObjectMapper JsonMapper = new ObjectMapper();
        try {
            List<Product> ListOfAllProductsInDB = currentDBWorker.GetAllProducts();
            System.out.println("**********************");
            System.out.println(ListOfAllProductsInDB.get(0).getProducer().getName());
            System.out.println("**********************");
            return JsonMapper.writeValueAsString(ListOfAllProductsInDB);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
        return "error during json mapping";
    }


    /*@RequestMapping(value = "/CreatePage", method = RequestMethod.GET)
    public String ReturnBasePage() {
        return "AddingNewEntitiesPage";
    }*/
}
