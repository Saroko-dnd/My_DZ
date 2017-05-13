package Tests;

import codeForTesting.CustomMath;
import org.junit.Test;

import static org.junit.Assert.*;

/**
 * Created by pst on 13.05.2017.
 */
public class CustomMathTest {
    @Test
    public void subtract() throws Exception {
        int result = CustomMath.subtract(10,5);
        assertEquals(5, result);
    }

    @Test
    public void division() throws Exception {
        int result = CustomMath.division(20,5);
        assertEquals(4, result);
    }

    @Test
    public void sum() throws Exception {
        int sum = CustomMath.sum(5,4);
        assertEquals(9, sum);
    }

    @Test
    public void multiply() throws Exception {
        int result = CustomMath.multiply(5,4);
        assertEquals(20, result);
    }

}