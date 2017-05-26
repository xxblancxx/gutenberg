/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package projectGutenbergTest;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;
import static org.hamcrest.Matchers.contains;
import static org.hamcrest.Matchers.containsInAnyOrder;
import static org.hamcrest.Matchers.hasItem;
import static org.hamcrest.Matchers.hasProperty;
import static org.hamcrest.Matchers.instanceOf;
import static org.hamcrest.Matchers.is;
import static org.hamcrest.Matchers.isA;
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
import static org.hamcrest.Matchers.not;
import static org.hamcrest.text.IsEmptyString.isEmptyString;
import org.junit.FixMethodOrder;
import org.junit.runners.MethodSorters;
import org.openqa.selenium.Keys;
import org.openqa.selenium.chrome.ChromeDriver;
//import static projectGutenbergSelenium.Gutenberg.Compare;
/**
 *
 * @author Manse
 */
@FixMethodOrder(MethodSorters.NAME_ASCENDING)
public class GutenbergTest {
    static WebDriver driver;
    int maxTimer = 10;
    static File image;
    List<ExpectedBook> expectedBooks = null;
    List<ExpectedBook> foundBooks = null;
    
    //Parameterized input
    String input; 
    ArrayList<String> output;
    public static ArrayList<ExpectedBookList> cityParameters;
    public static ArrayList<ExpectedBookList> coordinateParameters;
    
    @BeforeClass
    public static void start() throws IOException {
        System.setProperty("webdriver.gecko.driver", "src\\geckodriver.exe");
        System.setProperty("webdriver.chrome.driver","src\\chromedriver.exe");
        
        driver = new ChromeDriver();
        driver.get("http://localhost:49944");
        cityParameters = generateDataCity("testCitiesExpectedBooks.txt");
        coordinateParameters = generateDataCoordinates("testCitiesExpectedWithParametersBooks.txt");
    }
    
    public static ArrayList<ExpectedBookList> generateDataCity(String filename) throws FileNotFoundException, IOException {
        ArrayList<ExpectedBookList> param = new ArrayList<>();
        ExpectedBookList expectedList = new ExpectedBookList("", null);
        ArrayList<ExpectedBook> output = new ArrayList<>();
        ExpectedBook expOutput = new ExpectedBook("","");
        boolean isInput = false;
        boolean isTitle = true;
        
        BufferedReader br = new BufferedReader(new FileReader("src/" + filename));
        try {
            StringBuilder sb = new StringBuilder();
            String line;

            //Read lines, checks if input, afterwards takes values as output, on end it closes and resets for next values
            while ((line = br.readLine() ) != null ) {
                if(!line.contains("##") && !"".equals(line)){
                    if(isInput == true){
                        expectedList = new ExpectedBookList(line, null);
                        isInput = false;
                    }
                    else if("start".equals(line)){
                        isInput = true;
                    }
                    else if("end".equals(line)){
                        expectedList.output = output;
                        param.add(expectedList);
                        output = new ArrayList<>();
                    }else{
                        if(isTitle){
                            expOutput = new ExpectedBook(line, "");
                            isTitle = false;
                        }else{
                            expOutput.author = line;
                            output.add(expOutput);
                            isTitle = true;
                        }
                    }
                }
            }
        }finally {
            br.close();
        }
        return param;
    }
    public static ArrayList<ExpectedBookList> generateDataCoordinates(String filename) throws FileNotFoundException, IOException {
        ArrayList<ExpectedBookList> param = new ArrayList<>();
        ExpectedBookList expectedList = new ExpectedBookList("", null);
        ArrayList<ExpectedBook> output = new ArrayList<>();
        ExpectedBook expOutput = new ExpectedBook("","");
        boolean isInput = false;
        
        BufferedReader br = new BufferedReader(new FileReader("src/" + filename));
        try {
            StringBuilder sb = new StringBuilder();
            String line;

            //Read lines, checks if input, afterwards takes values as output, on end it closes and resets for next values
            while ((line = br.readLine() ) != null ) {
                if(!line.contains("##") && !"".equals(line)){
                    if(isInput == true){
                        expectedList = new ExpectedBookList(line, null);
                        isInput = false;
                    }
                    else if("start".equals(line)){
                        isInput = true;
                    }
                    else if("end".equals(line)){
                        expectedList.output = output;
                        param.add(expectedList);
                        output = new ArrayList<>();
                    }else{
                        expOutput = new ExpectedBook(line, "");
                        output.add(expOutput);
                    }
                }
            }
        }finally {
            br.close();
        }
        return param;
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
    
//    TEST GetBooksContainingCityMysql
    @Test
    public void Test2() {
        for(int i = 0; i < cityParameters.size(); i++){
            
            //ADD 1 because of header
            int expectedOutputSize = cityParameters.get(i).output.size();
            WebElement element = driver.findElement(By.id("titleAuthorWithCityTextBox"));
            element.sendKeys(cityParameters.get(i).input);
            WebElement element2 = driver.findElement(By.name("ctl02"));
            element2.click();

            //Check if tbody size is the same as expectedBook amount
            (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
                public Boolean apply(WebDriver d) {
                    int amount = d.findElements(By.xpath("//tbody/tr")).size();
                    return amount == expectedOutputSize + 1;
                }
            });

            //Iterate through and check if a book exists and the author fits
            for(int i2 = 0; i2 < expectedOutputSize; i2++){
                ExpectedBook file  = cityParameters.get(i).output.get(i2);
                for(int i3 = 1; i3 < driver.findElements(By.xpath("//tbody/tr")).size(); i3++){
                    String title = driver.findElement(By.xpath("//tbody/tr["+(i3+1)+"]/td[1]")).getText();
                    if(file.title == null ? title == null : file.title.equals(title)){
                        String author = driver.findElement(By.xpath("//tbody/tr["+(i3+1)+"]/td[2]")).getText();
                        assertTrue((file.author == null ? author == null : file.author.equals(author)));
                    }
                }
            }
            
            //Remove text from textbox
            for(int n = 1; n <= cityParameters.get(i).input.length(); n++){
                driver.findElement(By.id("titleAuthorWithCityTextBox")).sendKeys(Keys.BACK_SPACE);
            }
        }
    }
    
//    TEST GetBooksContainingCityMongoDB
    @Test
    public void Test3() {
        for(int i = 0; i < cityParameters.size(); i++){
            
            //ADD 1 because of header
            int expectedOutputSize = cityParameters.get(i).output.size();
            WebElement element = driver.findElement(By.id("titleAuthorWithCityTextBox"));
            element.sendKeys(cityParameters.get(i).input);
            WebElement element2 = driver.findElement(By.name("ctl03"));
            element2.click();

            //Check if tbody size is the same as expectedBook amount
            (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
                public Boolean apply(WebDriver d) {
                    int amount = d.findElements(By.xpath("//tbody/tr")).size();
                    return amount == expectedOutputSize + 1;
                }
            });

            //Iterate through and check if a book exists and the author fits
            for(int i2 = 0; i2 < expectedOutputSize; i2++){
                ExpectedBook file  = cityParameters.get(i).output.get(i2);
                for(int i3 = 1; i3 < driver.findElements(By.xpath("//tbody/tr")).size(); i3++){
                    String title = driver.findElement(By.xpath("//tbody/tr["+(i3+1)+"]/td[1]")).getText();
                    if(file.title == null ? title == null : file.title.equals(title)){
                        String author = driver.findElement(By.xpath("//tbody/tr["+(i3+1)+"]/td[2]")).getText();
                        assertTrue((file.author == null ? author == null : file.author.equals(author)));
                    }
                }
            }
            
            //Remove text from textbox
            for(int n = 1; n <= cityParameters.get(i).input.length(); n++){
                driver.findElement(By.id("titleAuthorWithCityTextBox")).sendKeys(Keys.BACK_SPACE);
            }
        }
    }
    
    //Test GetCitiesInTitleMysql - NEEDS WAIT
    @Test
    public void Test4() {
        WebElement element = driver.findElement(By.id("mentionedInBookTextbox"));
        element.sendKeys("Jingle Bells");
        WebElement element2 = driver.findElement(By.name("ctl04"));
        element2.click();
        String imgSource = driver.findElement(By.id("img")).getAttribute("src");
        assertThat(imgSource, not(isEmptyString()));
        
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
        String imgSource = driver.findElement(By.id("img")).getAttribute("src");
        assertThat(imgSource, not(isEmptyString()));
    }
    
    //Test GetCitiesWithAuthorMysql - NEEDS WAIT
    @Test
    public void Test6() {
        WebElement element = driver.findElement(By.id("citiesWithAuthorTextBox"));
        element.sendKeys("Helen Bannerman");
        WebElement element2 = driver.findElement(By.name("ctl06"));
        element2.click();
        String imgSource = driver.findElement(By.id("img")).getAttribute("src");
        assertThat(imgSource, not(isEmptyString()));
        
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
        String imgSource = driver.findElement(By.id("img")).getAttribute("src");
        assertThat(imgSource, not(isEmptyString()));
    }
    
    //Test GetBooksMentionedInAreaMysql
    @Test
    public void Test8() {
        for(int i = 0; i < coordinateParameters.size(); i++){
            String[] coordinate = coordinateParameters.get(i).input.split(",");
            String latitude = coordinate[0];
            String longitude = coordinate[1];
            
            //ADD 1 because of header
            int expectedOutputSize = coordinateParameters.get(i).output.size();
            WebElement element = driver.findElement(By.id("mentionedInAreaLatitudeBox"));
            element.sendKeys(latitude);
            WebElement element2 = driver.findElement(By.id("mentionedInAreaLongitudeBox"));
            element2.sendKeys(longitude);
            WebElement element3 = driver.findElement(By.name("ctl08"));
            element3.click();

            //Check if tbody size is the same as expectedBook amount
            (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
                public Boolean apply(WebDriver d) {
                    int amount = d.findElements(By.xpath("//tbody/tr")).size();
                    return amount == expectedOutputSize + 1;
                }
            });

            //Iterate through and check if a book exists and the author fits
            for(int i2 = 0; i2 < expectedOutputSize; i2++){
                ExpectedBook file  = coordinateParameters.get(i).output.get(i2);
                for(int i3 = 1; i3 < driver.findElements(By.xpath("//tbody/tr")).size(); i3++){
                    String title = driver.findElement(By.xpath("//tbody/tr["+(i3+1)+"]/td[1]")).getText();
                    if(title == null ? file.title == null : title.equals(file.title)){
                        assertTrue((file.title == null ? title == null : file.title.equals(title)));
                        break;
                    }
                }
            }
            
            //Remove text from textbox
            for(int n = 1; n <= coordinateParameters.get(i).input.length(); n++){
                driver.findElement(By.id("mentionedInAreaLatitudeBox")).sendKeys(Keys.BACK_SPACE);
                driver.findElement(By.id("mentionedInAreaLongitudeBox")).sendKeys(Keys.BACK_SPACE);
            }
        }
    }
    
    //Test GetBooksMentionedInAreaMysql
    @Test
    public void Test9() {
        for(int i = 0; i < coordinateParameters.size(); i++){
            String[] coordinate = coordinateParameters.get(i).input.split(",");
            String latitude = coordinate[0];
            String longitude = coordinate[1];

            //ADD 1 because of header
            int expectedOutputSize = coordinateParameters.get(i).output.size();
            WebElement element = driver.findElement(By.id("mentionedInAreaLatitudeBox"));
            element.sendKeys(latitude);
            WebElement element2 = driver.findElement(By.id("mentionedInAreaLongitudeBox"));
            element2.sendKeys(longitude);
            WebElement element3 = driver.findElement(By.name("ctl09"));
            element3.click();

            //Check if tbody size is the same as expectedBook amount
            (new WebDriverWait(driver, maxTimer)).until(new ExpectedCondition<Boolean>() {
                public Boolean apply(WebDriver d) {
                    int amount = d.findElements(By.xpath("//tbody/tr")).size();
                    return amount == expectedOutputSize + 1;
                }
            });

            //Iterate through and check if a book exists and the author fits
            for(int i2 = 0; i2 < expectedOutputSize; i2++){
                ExpectedBook file  = coordinateParameters.get(i).output.get(i2);
                for(int i3 = 1; i3 < driver.findElements(By.xpath("//tbody/tr")).size(); i3++){
                    String title = driver.findElement(By.xpath("//tbody/tr["+(i3+1)+"]/td[1]")).getText();
                    if(title == null ? file.title == null : title.equals(file.title)){
                        assertTrue((file.title == null ? title == null : file.title.equals(title)));
                        break;
                    }
                }
            }
            
            //Remove text from textbox
            for(int n = 1; n <= coordinateParameters.get(i).input.length(); n++){
                driver.findElement(By.id("mentionedInAreaLatitudeBox")).sendKeys(Keys.BACK_SPACE);
                driver.findElement(By.id("mentionedInAreaLongitudeBox")).sendKeys(Keys.BACK_SPACE);
            }
        }
    }
    
    public ExpectedBook Compare(WebDriver driver,int index){
        index += 2;
        String title = driver.findElement(By.xpath("//tbody/tr["+index+"]/td[1]")).getText();
        String author = driver.findElement(By.xpath("//tbody/tr["+index+"]/td[2]")).getText();
        return new ExpectedBook(title, author);
    }
}
