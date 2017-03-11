package DBPackage.DB_DI_Providers;

import DIProviders.DIConfiguration;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

/**
 * Created by admin on 11.03.2017.
 */
public class DIResolver {

    private static AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DBDIConfiguration.class);

    public static AnnotationConfigApplicationContext GetDiContext(){
        return null;
    }
}
