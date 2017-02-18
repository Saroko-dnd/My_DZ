package DIUsers;

import java.util.Random;

/**
 * Created by admin on 18.02.2017.
 */
public class TrueDice implements IGameDice {

    Random randomGenerator = new Random();

    public int ThrowDice() {
        return randomGenerator.nextInt(6);
    }
}
