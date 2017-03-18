package MainPackage.Exeptions;

/**
 * Created by admin on 18.03.2017.
 */
import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface TestAnnotationForType {
    public enum Priority {
        LOW, MEDIUM, HIGH
    }

    Priority priority() default Priority.MEDIUM;

    String testInfo() default "test info";

    String[] listOfTests() default "";

    int score() default 10;

    int testTime() default 600;
}
