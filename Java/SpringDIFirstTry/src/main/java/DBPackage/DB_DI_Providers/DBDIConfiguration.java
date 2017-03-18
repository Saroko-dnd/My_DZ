package DBPackage.DB_DI_Providers;

import DBPackage.DBWorker;
import DBPackage.IDBWorker;
import DIUsers.*;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

/**
 * Created by admin on 11.03.2017.
 */
@Configuration
@ComponentScan(value = {"DBPackage"})
public class DBDIConfiguration {
    
}






