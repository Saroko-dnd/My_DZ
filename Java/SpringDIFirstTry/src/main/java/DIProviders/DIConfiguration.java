package DIProviders;

import DIUsers.*;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.beans.factory.annotation.Qualifier;

/**
 * Created by admin on 18.02.2017.
 */
@Configuration
@ComponentScan(value={"DIConsumers"})
public class DIConfiguration {


    @Bean
    @Qualifier("Simple")
    public ICalculator getCalculatorService(){
        return new SimpleCalculator();
    }

    @Bean
    @Qualifier("Advanced")
    public ICalculator getCalculatorService_2(){
        return new AdvancedCalculator();
    }

    @Bean
    public IGameDice getGameDiceService(){
        return new FalseDice();
    }
}
