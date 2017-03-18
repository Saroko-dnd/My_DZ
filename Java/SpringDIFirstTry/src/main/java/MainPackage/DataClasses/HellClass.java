package MainPackage.DataClasses;

import MainPackage.Exeptions.TestAnnotation;
import MainPackage.Exeptions.TestAnnotationForType;

/**
 * Created by admin on 18.03.2017.
 */

@TestAnnotationForType(
        priority = TestAnnotationForType.Priority.HIGH,
        testInfo = "My test info",
        listOfTests = {"First test","Second test","Third test"},
        score = 30,
        testTime = 1000)
public class HellClass {
    @TestAnnotation(enabled = true, title = "takeOneTest function")
    void takeOneTest(int testIndex)
    {

    }
    @TestAnnotation(enabled = false, title = "takeAllTests function")
    void takeAllTests()
    {

    }
    @TestAnnotation(enabled = true, title = "getCurrentScore function")
    int getCurrentScore()
    {
        return 50;
    }
}
