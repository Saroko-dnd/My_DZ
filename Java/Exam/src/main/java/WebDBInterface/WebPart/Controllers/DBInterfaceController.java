package WebDBInterface.WebPart.Controllers;

import WebDBInterface.DBEntities.Producer;
import WebDBInterface.DB_DI_Providers.IDataBaseWorker;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseStatus;

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
}
