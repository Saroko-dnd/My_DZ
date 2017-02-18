package DIUsers;

import DIConsumers.DIConsumerGameDice;
import com.sun.org.apache.xpath.internal.operations.Bool;

/**
 * Created by admin on 18.02.2017.
 */
public class SuperCasino implements ICasino {
    private DIConsumerGameDice _firstDice;
    private DIConsumerGameDice _secondDice;

    public boolean TryIt() {
        if ((_firstDice.ThrowDice() + _secondDice.ThrowDice()) >= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public SuperCasino(DIConsumerGameDice FirstDice, DIConsumerGameDice SecondDice)
    {
        _firstDice = FirstDice;
        _secondDice = SecondDice;
    }
}
