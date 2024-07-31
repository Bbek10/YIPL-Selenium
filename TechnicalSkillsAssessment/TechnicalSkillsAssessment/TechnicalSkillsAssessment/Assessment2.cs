using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TechnicalSkillsAssessment
{
    [TestFixture]
    public class Assessment2
    {   private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            //instance of the driver
            driver = new ChromeDriver();
            //maximize the window
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void BasicAuth()
        {
         
            string username = "admin";
            string password = "admin";
            string expectedTitle = "The Internet";
            string expectedHeader = "Basic Auth";
            

            //passing user information
            string url = $"https://{username}:{password}@the-internet.herokuapp.com/basic_auth";
            //Navigate to the site URL
            driver.Navigate().GoToUrl(url);
           
            Assert.That(expectedTitle, Is.EqualTo(driver.Title));
            var header = driver.FindElement(By.TagName("h3")).Text;
            Assert.That(expectedHeader, Is.EqualTo(header));

        }
        [Test]
        public void ValidatingCheckBoxes()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/checkboxes");

            //select checkbox1
            IWebElement checkbox1 = driver.FindElement(By.XPath("//*[@id=\"checkboxes\"]/input[1]"));
            checkbox1.Click();
            Assert.IsTrue(checkbox1.Selected,"checkbox2");
            
            //select checkbox2
            IWebElement checkbox2 = driver.FindElement(By.XPath("//*[@id=\"checkboxes\"]/input[2]"));
            checkbox2.Click();
            Assert.IsFalse(checkbox2.Selected,"checkbox1");

        }
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

    }
}