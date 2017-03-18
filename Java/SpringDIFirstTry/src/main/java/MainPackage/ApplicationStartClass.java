package MainPackage;

import DIConsumers.DIConsumerGameDice;
import DIProviders.DIConfiguration;
import DIConsumers.DIConsumer;
import DIUsers.SuperCasino;
import MainPackage.DataClasses.HellClass;
import MainPackage.Exeptions.TestAnnotation;
import MainPackage.Exeptions.TestAnnotationForType;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

import java.lang.annotation.Annotation;
import java.lang.reflect.Method;

/**
 * Created by admin on 18.02.2017.
 */
public class ApplicationStartClass {
    public static void main(String[] args) {
        /*AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DIConfiguration.class);
        DIConsumerGameDice CurrentGameDice = context.getBean(DIConsumerGameDice.class);
        SuperCasino CurrentCasino = new SuperCasino(context.getBean(DIConsumerGameDice.class),
                context.getBean(DIConsumerGameDice.class));

        System.out.println(CurrentCasino.TryIt());
        System.out.println(CurrentCasino.TryIt());
        System.out.println(CurrentCasino.TryIt());
        System.out.println(CurrentCasino.TryIt());
        System.out.println(CurrentCasino.TryIt());

        //close the context and stop application
        context.close();*/
        Class<HellClass> reflectionTestObject = HellClass.class;

        if (reflectionTestObject.isAnnotationPresent(TestAnnotationForType.class))
        {
            Annotation annotation = reflectionTestObject.getAnnotation(TestAnnotationForType.class);
            TestAnnotationForType annotationInfo = (TestAnnotationForType) annotation;

            System.out.println("Priority :" + annotationInfo.priority().toString());
            System.out.println("Score :" + annotationInfo.testInfo());
        }

        Method[] listOfMethods = reflectionTestObject.getDeclaredMethods();

        for (int Index = 0; Index < listOfMethods.length; ++Index)
        {
            if (listOfMethods[Index].isAnnotationPresent(TestAnnotation.class))
            {
                System.out.println(listOfMethods[Index].getName());
            }
        }

        System.exit(0);
    };
}
