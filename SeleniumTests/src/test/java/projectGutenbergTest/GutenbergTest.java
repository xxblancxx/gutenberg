/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package projectGutenbergTest;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.ExpectedCondition;
import org.openqa.selenium.support.ui.WebDriverWait;
import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.Matchers.greaterThan;
import org.junit.FixMethodOrder;
import org.junit.runners.MethodSorters;
import org.openqa.selenium.Keys;
import org.openqa.selenium.chrome.ChromeDriver;
import static projectGutenbergSelenium.Gutenberg.Compare;
/**
 *
 * @author Manse
 */
@FixMethodOrder(MethodSorters.NAME_ASCENDING)
public class GutenbergTest {
    static WebDriver driver;
    int maxTimer = 5;
    List<ExpectedOutput> expectedBooks = null;
    List<ExpectedOutput> foundBooks = null;
    
    @BeforeClass
    public static void start() {
        System.setProperty("webdriver.gecko.driver", "C:\\Program Files\\SeleniumDrivers\\geckodriver.exe");
        System.setProperty("webdriver.chrome.driver","C:\\Program Files\\SeleniumDrivers\\chromedriver.exe");
        
        driver = new ChromeDriver();
        driver.get("http://localhost:49944");
    }
    
    @AfterClass
    public static void end(){
        driver.quit();
    }
    
    //VERIFY DATA IS LOADED
    @Test
    public void Test1() {
        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
            public Boolean apply(WebDriver d) {
                String title = d.findElement(By.xpath("//body//h1")).getText();
                System.out.println("Text is: " + title);
                return "Gutenberg project".equals(title);
            }
        });
    }
    
    //TEST GetBooksContainingCityMysql
    @Test
    public void Test2() {
        WebElement element = driver.findElement(By.id("titleAuthorWithCityTextBox"));
        element.sendKeys("Esbjerg");
        WebElement element2 = driver.findElement(By.name("ctl02"));
        element2.click();
        
        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
            public Boolean apply(WebDriver d) {
                int amount = d.findElements(By.xpath("//tbody/tr")).size();
                return amount == 6;
            }
        });
        
        //Get from file later
        expectedBooks = new ArrayList<ExpectedOutput>();
        expectedBooks.add(new ExpectedOutput("Denmark", "M. Pearson Thomson,"));
        expectedBooks.add(new ExpectedOutput("The 1990 CIA World Factbook", "M. United States. Central Intelligence Agency,"));
        expectedBooks.add(new ExpectedOutput("The 1994 CIA World Factbook", "United States Central Intelligence Agency,"));
        expectedBooks.add(new ExpectedOutput("The 1997 CIA World Factbook", "United States. Central Intelligence Agency.,"));
        expectedBooks.add(new ExpectedOutput("The 1998 CIA World Factbook", "United States. Central Intelligence Agency.,"));
        
        expectedBooks.get(0).title.equals(Compare(driver, 2).title);
        expectedBooks.get(1).title.equals(Compare(driver, 3).title);
        expectedBooks.get(2).title.equals(Compare(driver, 4).title);
        expectedBooks.get(3).title.equals(Compare(driver, 5).title);
        expectedBooks.get(4).title.equals(Compare(driver, 6).title);
        
        for(int i = 1; i < 8; i++){
            driver.findElement(By.id("titleAuthorWithCityTextBox")).sendKeys(Keys.BACK_SPACE);
        }
    }
    
    //TEST GetBooksContainingCityMongoDB
    @Test
    public void Test3() {
        WebElement element = driver.findElement(By.id("titleAuthorWithCityTextBox"));
        element.sendKeys("Esbjerg");
        WebElement element2 = driver.findElement(By.name("ctl03"));
        element2.click();
        
        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
            public Boolean apply(WebDriver d) {
                int amount = d.findElements(By.xpath("//tbody/tr")).size();
                System.out.println("List amount is: " + amount);
                return amount == 6;
            }
        });
        
        //Get from file later
        expectedBooks = new ArrayList<ExpectedOutput>();
        expectedBooks.add(new ExpectedOutput("The 1994 CIA World Factbook", "United States Central Intelligence Agency,"));
        expectedBooks.add(new ExpectedOutput("The 1997 CIA World Factbook", "United States. Central Intelligence Agency.,"));
        expectedBooks.add(new ExpectedOutput("Denmark", "M. Pearson Thomson,"));
        expectedBooks.add(new ExpectedOutput("The 1990 CIA World Factbook", "M. United States. Central Intelligence Agency,"));
        expectedBooks.add(new ExpectedOutput("The 1998 CIA World Factbook", "United States. Central Intelligence Agency.,"));
        
        expectedBooks.get(0).title.equals(Compare(driver, 2).title);
        expectedBooks.get(1).title.equals(Compare(driver, 3).title);
        expectedBooks.get(2).title.equals(Compare(driver, 4).title);
        expectedBooks.get(3).title.equals(Compare(driver, 5).title);
        expectedBooks.get(4).title.equals(Compare(driver, 6).title);
    }
    
    //Test GetCitiesInTitleMysql - NEEDS WAIT
    @Test
    public void Test4() {
        WebElement element = driver.findElement(By.id("mentionedInBookTextbox"));
        element.sendKeys("Jingle Bells");
        WebElement element2 = driver.findElement(By.name("ctl04"));
        element2.click();
        
        for(int i = 1; i < 13; i++){
            driver.findElement(By.id("mentionedInBookTextbox")).sendKeys(Keys.BACK_SPACE);
        }
    }
    
    //Test GetCitiesInTitleMongoDB - NEEDS WAIT
    @Test
    public void Test5() {
        WebElement element = driver.findElement(By.id("mentionedInBookTextbox"));
        element.sendKeys("Jingle Bells");
        WebElement element2 = driver.findElement(By.name("ctl04"));
        element2.click();
    }
    
    //Test GetCitiesWithAuthorMysql - NEEDS WAIT
    @Test
    public void Test6() {
        WebElement element = driver.findElement(By.id("citiesWithAuthorTextBox"));
        element.sendKeys("Helen Bannerman");
        WebElement element2 = driver.findElement(By.name("ctl06"));
        element2.click();
        
        for(int i = 1; i < 16; i++){
            driver.findElement(By.id("citiesWithAuthorTextBox")).sendKeys(Keys.BACK_SPACE);
        }
    }
    
    //Test GetCitiesWithAuthorMongoDB - NEEDS WAIT
    @Test
    public void Test7() {
        WebElement element = driver.findElement(By.id("citiesWithAuthorTextBox"));
        element.sendKeys("Helen Bannerman");
        WebElement element2 = driver.findElement(By.name("ctl07"));
        element2.click();
    }
    
    //Test GetBooksMentionedInAreaMysql
    @Test
    public void Test8() {
        WebElement element = driver.findElement(By.id("mentionedInAreaLatitudeBox"));
        element.sendKeys("15");
        WebElement element2 = driver.findElement(By.id("mentionedInAreaLongitudeBox"));
        element2.sendKeys("43");
        WebElement element3 = driver.findElement(By.name("ctl08"));
        element3.click();
        
        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
            public Boolean apply(WebDriver d) {
                int amount = d.findElements(By.xpath("//tbody/tr")).size();
                System.out.println("List amount is: " + amount);
                return amount == 5;
            }
        });
        
        //Get from file later
        List<String> Books = new ArrayList<String>();
        Books.add("The 1994 CIA World Factbook");
        Books.add("The 1997 CIA World Factbook");
        Books.add("The 1990 CIA World Factbook");
        Books.add("The 1998 CIA World Factbook");
        
        Books.get(0).equals(driver.findElement(By.xpath("//tbody/tr[2]/td[1]")).getText());
        Books.get(1).equals(driver.findElement(By.xpath("//tbody/tr[3]/td[1]")).getText());
        Books.get(2).equals(driver.findElement(By.xpath("//tbody/tr[4]/td[1]")).getText());
        Books.get(3).equals(driver.findElement(By.xpath("//tbody/tr[5]/td[1]")).getText());
        
        for(int i = 1; i < 3; i++){
            driver.findElement(By.id("mentionedInAreaLatitudeBox")).sendKeys(Keys.BACK_SPACE);
            driver.findElement(By.id("mentionedInAreaLongitudeBox")).sendKeys(Keys.BACK_SPACE);
        }
    }
    
    //Test GetBooksMentionedInAreaMysql
    @Test
    public void Test9() {
        WebElement element = driver.findElement(By.id("mentionedInAreaLatitudeBox"));
        element.sendKeys("15");
        WebElement element2 = driver.findElement(By.id("mentionedInAreaLongitudeBox"));
        element2.sendKeys("43");
        WebElement element3 = driver.findElement(By.name("ctl09"));
        element3.click();
        
        (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
            public Boolean apply(WebDriver d) {
                int amount = d.findElements(By.xpath("//tbody/tr")).size();
                System.out.println("List amount is: " + amount);
                return amount == 5;
            }
        });
        
        //Get from file later
        List<String> Books = new ArrayList<String>();
        Books.add("The 1994 CIA World Factbook");
        Books.add("The 1990 CIA World Factbook");
        Books.add("The 1997 CIA World Factbook");
        Books.add("The 1998 CIA World Factbook");
        
        Books.get(0).equals(driver.findElement(By.xpath("//tbody/tr[2]/td[1]")).getText());
        Books.get(1).equals(driver.findElement(By.xpath("//tbody/tr[3]/td[1]")).getText());
        Books.get(2).equals(driver.findElement(By.xpath("//tbody/tr[4]/td[1]")).getText());
        Books.get(3).equals(driver.findElement(By.xpath("//tbody/tr[5]/td[1]")).getText());
    }
    
    public ExpectedOutput Compare(WebDriver driver,int index){
        String title = driver.findElement(By.xpath("//tbody/tr["+index+"]/td[1]")).getText();
        String author = driver.findElement(By.xpath("//tbody/tr["+index+"]/td[2]")).getText();
        return new ExpectedOutput(title, author);
    }
}
