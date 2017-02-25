import DIConsumers.DIConsumerGameDice;
import DIProviders.DIConfiguration;
import DIConsumers.DIConsumer;
import DIUsers.SuperCasino;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

/**
 * Created by admin on 18.02.2017.
 */
public class ApplicationStartClass {
    public static void main(String[] args) {
        AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DIConfiguration.class);
        DIConsumerGameDice CurrentGameDice = context.getBean(DIConsumerGameDice.class);
        SuperCasino CurrentCasino = new SuperCasino(context.getBean(DIConsumerGameDice.class),
                context.getBean(DIConsumerGameDice.class));

        System.out.println(CurrentCasino.TryIt());
        System.out.println(CurrentCasino.TryIt());
        System.out.println(CurrentCasino.TryIt());
        System.out.println(CurrentCasino.TryIt());
        System.out.println(CurrentCasino.TryIt());

        //close the context and stop application
        context.close();
        System.exit(0);
    };
}
