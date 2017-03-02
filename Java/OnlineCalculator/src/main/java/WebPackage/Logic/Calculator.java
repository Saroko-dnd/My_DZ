package WebPackage.Logic;

import bsh.EvalError;
import bsh.Interpreter;

import java.lang.String;

/**
 * Created by master on 2017/03/02.
 */
public class Calculator {

    private static Interpreter ExpressionInterpreter = new Interpreter();

    public static String  EvalExpression(String expression) throws EvalError {
        String Result;
        try {
            ExpressionInterpreter.eval("result = " + expression);
        } catch (EvalError evalError) {
            return  "Evaluation fail!";
        }
        Result = ExpressionInterpreter.get("result").toString();
        return Result;
    }
}
