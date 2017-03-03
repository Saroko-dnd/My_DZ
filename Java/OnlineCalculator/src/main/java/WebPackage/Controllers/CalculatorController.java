package WebPackage.Controllers;

import WebPackage.Logic.Calculator;
import com.udojava.evalex.Expression;
import org.springframework.http.HttpRequest;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;
import org.springframework.ui.ModelMap;
import org.springframework.web.servlet.ModelAndView;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by master on 2017/03/02.
 */
@Controller
public class CalculatorController {
    @RequestMapping(value="/EvaluateExpression",method = RequestMethod.POST)
    @ResponseBody
    public String printHurry(@RequestBody String newExpression) {
        try
        {
            return Calculator.EvalExpression(newExpression);
        }
        catch(Exception evalException)
        {
            return evalException.getMessage();
        }
    }
}
