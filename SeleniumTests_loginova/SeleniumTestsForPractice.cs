using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests_loginova;

public class SeleniumTestsForPractice
{
    [Test]
    public void Authorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");

        // - зайти в хром (с помощью вебдрайвера)
        var driver = new ChromeDriver();
            
        // - перейти по урлу https://staff-testing.testkontur.ru/
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");
        Thread.Sleep(5000);
            
        // - ввести логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("elena-log95@yandex.ru");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("aaxkL96@HC");
            
        Thread.Sleep(5000);
            
        // - нажать на кнопку "Войти"
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
            
        Thread.Sleep(3000);
            
        // - проверяем, что мы находимся на нужной странице
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news");
            
        // - закрываем браузер и убиваем процесс драйвера
        driver.Quit(); 
    }
}
