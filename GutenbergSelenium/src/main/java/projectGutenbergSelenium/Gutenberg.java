package projectGutenbergSelenium;
/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

import java.util.ArrayList;
import java.util.List;
import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.ExpectedCondition;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

/**
 *
 * @author Manse
 */
public class Gutenberg {
    
    static List<ExpectedOutput> expectedBooks = new ArrayList<ExpectedOutput>();
    static int maxTimer = 5;
        
    static WebDriver  driver = new FirefoxDriver();
    
    public ExpectedOutput validateGetBooksContainingCityMysql(int index) {
//        System.setProperty("webdriver.gecko.driver", "src\\geckodriver.exe");
//        System.setProperty("webdriver.chrome.driver","src\\chromedriver.exe");
//        driver.get("http://localhost:49944");
        
        String title = driver.findElement(By.xpath("//tbody/tr["+index+"]/td[1]")).getText();
        String author = driver.findElement(By.xpath("//tbody/tr["+index+"]/td[2]")).getText();
        ExpectedOutput res = new ExpectedOutput(title, author);
        return res;
   }
//    
//    public static void main(String[] args) throws InterruptedException {
//        
//        System.setProperty("webdriver.gecko.driver", "C:\\Program Files\\SeleniumDrivers\\geckodriver.exe");
//        System.setProperty("webdriver.chrome.driver","C:\\Program Files\\SeleniumDrivers\\chromedriver.exe");
//        driver.get("http://localhost:49944");
//        //verify page is loaded
//        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
//            public Boolean apply(WebDriver d) {
//                String title = d.findElement(By.xpath("//body//h1")).getText();
//                System.out.println("Text is: " + title);
//                return "Gutenberg project".equals(title);
//            }
//        });
//        
//        //Test GetBooksContainingCityMysql
//        WebElement element = driver.findElement(By.id("titleAuthorWithCityTextBox"));
//        element.sendKeys("Esbjerg");
//        WebElement element2 = driver.findElement(By.name("ctl02"));
//        element2.click();
//        
//        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
//            public Boolean apply(WebDriver d) {
//                int amount = d.findElements(By.xpath("//tbody/tr")).size();
//                return amount == 6;
//            }
//        });
//        
//        expectedBooks.add(new ExpectedOutput("Denmark", "M. Pearson Thomson,"));
//        expectedBooks.add(new ExpectedOutput("The 1990 CIA World Factbook", "M. United States. Central Intelligence Agency,"));
//        expectedBooks.add(new ExpectedOutput("The 1994 CIA World Factbook", "United States Central Intelligence Agency,"));
//        expectedBooks.add(new ExpectedOutput("The 1997 CIA World Factbook", "United States. Central Intelligence Agency.,"));
//        expectedBooks.add(new ExpectedOutput("The 1998 CIA World Factbook", "United States. Central Intelligence Agency.,"));
//        
//        System.out.println(Compare(driver, 2));
//        System.out.println(Compare(driver, 3));
//        System.out.println(Compare(driver, 4));
//        System.out.println(Compare(driver, 5));
//        System.out.println(Compare(driver, 6));
//        
//        //Test GetBooksContainingCityMongoDB
//        element2 = driver.findElement(By.name("ctl03"));
//        element2.click();
//        
//        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
//            public Boolean apply(WebDriver d) {
//                int amount = d.findElements(By.xpath("//tbody/tr")).size();
//                System.out.println("List amount is: " + amount);
//                return amount == 6;
//            }
//        });
//        
//        expectedBooks.clear();
//        expectedBooks.add(new ExpectedOutput("The 1994 CIA World Factbook", "United States Central Intelligence Agency,"));
//        expectedBooks.add(new ExpectedOutput("The 1997 CIA World Factbook", "United States. Central Intelligence Agency.,"));
//        expectedBooks.add(new ExpectedOutput("Denmark", "M. Pearson Thomson,"));
//        expectedBooks.add(new ExpectedOutput("The 1990 CIA World Factbook", "M. United States. Central Intelligence Agency,"));
//        expectedBooks.add(new ExpectedOutput("The 1998 CIA World Factbook", "United States. Central Intelligence Agency.,"));
//        
//        System.out.println(Compare(driver, 2));
//        System.out.println(Compare(driver, 3));
//        System.out.println(Compare(driver, 4));
//        System.out.println(Compare(driver, 5));
//        System.out.println(Compare(driver, 6));
//        
//        //Test GetCitiesInTitleMysql
//        element = driver.findElement(By.id("mentionedInBookTextbox"));
//        element.sendKeys("Jingle Bells");
//        element2 = driver.findElement(By.name("ctl04"));
//        element2.click();
//        
//        //TESTING
//        
//        //Test GetCitiesInTitleMongoDB
//        element2 = driver.findElement(By.name("ctl05"));
//        element2.click();
//        
//        //Test GetCitiesWithAuthorMysql
//        element = driver.findElement(By.id("citiesWithAuthorTextBox"));
//        element.sendKeys("Helen Bannerman");
//        element2 = driver.findElement(By.name("ctl06"));
//        element2.click();
//        
//        //Test GetCitiesWithAuthorMongoDB
//        element2 = driver.findElement(By.name("ctl07"));
//        element2.click();
//        
//        //Test GetBooksMentionedInAreaMysql
//        element = driver.findElement(By.id("mentionedInAreaLatitudeBox"));
//        element.sendKeys("36");
//        element2 = driver.findElement(By.id("mentionedInAreaLongitudeBox"));
//        element2.sendKeys("43");
//        WebElement element3 = driver.findElement(By.name("ctl08"));
//        element3.click();
//        
//        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
//            public Boolean apply(WebDriver d) {
//                int amount = d.findElements(By.xpath("//tbody/tr")).size();
//                System.out.println("List amount is: " + amount);
//                return amount == 6;
//            }
//        });
//        
//        
//        //Test GetBooksMentionedInAreaMongoDB
//    }
//    
//    public static String Compare(WebDriver driver,int index){
//        String title = driver.findElement(By.xpath("//tbody/tr["+index+"]/td[1]")).getText();
//        String author = driver.findElement(By.xpath("//tbody/tr["+index+"]/td[2]")).getText();
//        return title + " = " + expectedBooks.get(index-2).title + ", " + author + " = " + expectedBooks.get(index-2).author;
//    }
}

class ExpectedOutput{
    String title;
    String author;
    public ExpectedOutput(String Title, String Author){
        title = Title;
        author = Author;
    }
}