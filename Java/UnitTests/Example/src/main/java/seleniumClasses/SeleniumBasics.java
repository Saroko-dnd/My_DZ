package seleniumClasses;

import org.openqa.selenium.*;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.ExpectedCondition;
import org.openqa.selenium.support.ui.WebDriverWait;

/**
 * Created by pst on 27.05.2017.
 */
public class SeleniumBasics {
    public static void TestSelenium()
    {
        System.setProperty("webdriver.chrome.driver", "D:\\Users\\SarokoIV\\chromedriver_win32\\chromedriver.exe");
        WebDriver testWebDriver = new ChromeDriver();
        testWebDriver.get("https://stackoverflow.com/");
        WebElement foundElement = testWebDriver.findElement(By.cssSelector("#search > input"));
        foundElement.sendKeys("vim issues");
        foundElement.submit();
        WebDriverWait testWait = new WebDriverWait( testWebDriver, 15);
        testWait.until((ExpectedCondition<Boolean>) wd -> ((JavascriptExecutor) wd).executeScript("return document.readyState").equals("complete"));
        foundElement = testWebDriver.findElement(By.cssSelector("#tabs > a:nth-of-type(3)"));
        foundElement.click();
        System.out.println("Votes for first question: " + foundElement.getText());

        //waiting and trying to get votes for first question
        WebDriverWait testWait_2 = new WebDriverWait( testWebDriver, 15);
        testWait.until((ExpectedCondition<Boolean>) wd -> ((JavascriptExecutor) wd).executeScript("return document.querySelector(\"div[data-position=\"1\"] vote-count-post strong\")").equals("complete"));
        foundElement = testWebDriver.findElement(By.cssSelector("div[data-position=\"1\"] vote-count-post strong"));
    }
}
