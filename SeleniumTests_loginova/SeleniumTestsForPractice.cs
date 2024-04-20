using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests_loginova;

public class SeleniumTestsForPractice
{
    public ChromeDriver driver;
    
    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--window-size=1280,800", "--start-maximized" , "--disable-extensions");
        
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2); 
        
        Authorization();
    }
    
    [Test] 
    public void AuthorizationTest() 
    {
        // Проверить, что находимся на нужной странице
        var news = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        driver.Url.Should().Be("https://staff-testing.testkontur.ru/news");
    }

    [Test]
    public void NavigationTest()
    {
        //1. Открыть боковое меню
        driver.FindElement(By.CssSelector("[data-tid='SidebarMenuButton']")).Click();
        
        //2. Кликнуть на раздел "Сообщества"
        driver.FindElements(By.CssSelector("[data-tid='Community']")).First(element => element.Displayed).Click();
        
        //3. Проверить, что находимся на странице Сообщества
        driver.Url.Should().Be("https://staff-testing.testkontur.ru/communities");

    }

    [Test]
    public void CommunityTabTest()
    {
        //1. Перейти в раздел "Сообщества"
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
        
        //2. Дождаться появления вкладок
        var waitTitle = new WebDriverWait(driver, TimeSpan.FromSeconds(2)); 
        waitTitle.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-tid='PageHeader']")));
        
        //3. Кликнуть по вкладке "Я модератор"
        driver.FindElements(By.CssSelector("[data-tid='Item']"))[2].Click();
        
        //4. Проверить, что находимся на вкладке "Я модератор"
        driver.Url.Should().Be("https://staff-testing.testkontur.ru/communities?activeTab=isAdministrator");
    }

    [Test]
    public void NewYearThemeTest()
    {
        //1. Кликнуть по аватару профиля
        driver.FindElement((By.CssSelector("[data-tid='Avatar']"))).Click();
        
        //2. Кликнуть "Настройки"
        driver.FindElement(By.CssSelector("[data-tid='Settings']")).Click();
        
        //3. Кликнуть на "Новогодняя тема"
        driver.FindElement(By.XPath("//*[contains(text(),'Новогодняя тема')]")).Click();
        
        //4. Нажать кнопку "Сохранить"
        driver.FindElement(By.XPath("//*[contains(text(),'Сохранить')]")).Click();
        
        //5. Проверить, что новогодняя тема появилась 
        var newYearTheme = driver.FindElement(By.XPath("//*[@id='root']/div[2]/div/div"));
        newYearTheme.Should().NotBeNull();
    }
    
    [Test]
    public void SearchBarTest()
    {
        //1. Кликнуть по поиску
        driver.FindElement(By.CssSelector("[data-tid='SearchBar']")).Click();
        
        //2. Ввести корректную фамилию в поле поиска
        driver.FindElement(By.XPath("//*[@id='root']/div/header/div/div[2]/div/span/" +
                                                "label/span[2]/input")).SendKeys("Агапова");
        
        //3. Выбрать из выпадающего списка второго сотрудника
        driver.FindElements(By.CssSelector("[data-tid='ComboBoxMenu__item']"))[1].Click();
        
        //4. Проверить, что перешли в профиль выбранного сотрудника
        driver.Url.Should().Be("https://staff-testing.testkontur.ru/profile/c1ff468d-ff31-4cd4-843a-44d0ad7d895e");
    }

    public void Authorization()
    {
        //1. Перейти по Url https://staff-testing.testkontur.ru
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        
        //2. Ввести логин
        driver.FindElement(By.Id("Username")).SendKeys("elena-log95@yandex.ru");
    
        //3. Ввести пароль
        driver.FindElement(By.Name("Password")).SendKeys("aaxkL96@HC");
        
        //4. Нажать кнопку "Войти"
        driver.FindElement(By.Name("button")).Click();
        
        // Дождаться появления заголовка Новостей
        var waitTitle = new WebDriverWait(driver, TimeSpan.FromSeconds(2)); 
        waitTitle.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-tid='Title']")));
    }
    
    
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
