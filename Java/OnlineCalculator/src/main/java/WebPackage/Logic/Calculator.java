package WebPackage.Logic;

import java.lang.String;
import com.udojava.evalex.Expression;

/**
 * Created by master on 2017/03/02.
 */
public class Calculator {

    public static String  EvalExpression(String expression) {
        Expression mathExpression = new Expression(expression);
        return mathExpression.eval().toString();
    }
}
