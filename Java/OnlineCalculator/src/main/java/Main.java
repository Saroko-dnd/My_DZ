import WebPackage.Logic.Calculator;
import bsh.EvalError;

/**
 * Created by master on 2017/03/02.
 */
public class Main {
    public static void main(String[] args) throws EvalError {
        String TestString = Calculator.EvalExpression("10+7");
        System.out.println(TestString);
        System.exit(0);
    };
}
