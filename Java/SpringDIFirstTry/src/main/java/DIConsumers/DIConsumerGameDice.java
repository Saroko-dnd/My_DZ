package DIConsumers;

import DIUsers.ICalculator;
import DIUsers.IGameDice;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

/**
 * Created by admin on 18.02.2017.
 */
@Component
@Scope("prototype")
public class DIConsumerGameDice {
    private IGameDice _gameDiceService;
    @Autowired
    public void setService(IGameDice gd){
        this._gameDiceService = gd;
    }

    public int ThrowDice(){
        //some magic like validation, logging etc
        return this._gameDiceService.ThrowDice();
    }
}
