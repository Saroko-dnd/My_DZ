package DIConsumers;

import DIUsers.ICalculator;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;
import org.springframework.beans.factory.annotation.Qualifier;

/**
 * Created by admin on 18.02.2017.
 */
@Component
@Scope("prototype")
public class DIConsumer {
    private ICalculator CalculatorService;

    @Autowired
    @Qualifier("Simple")
    public void setService(ICalculator svc){
        this.CalculatorService = svc;
    }

    public int calculateSum(int FirstNumber, int Secondnumber){
        //some magic like validation, logging etc
        return this.CalculatorService.Sum(FirstNumber, Secondnumber);
    }
}

