import WebPackage.Logic.Calculator;
import bsh.EvalError;

/**
 * Created by master on 2017/03/02.
 */
public class Main {
    public static void main(String[] args) throws EvalError {
        System.out.println(Calculator.EvalExpression("10 + 7 - 5"));
        System.exit(0);
    };
}
