using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumFormTest.Test
{
    [TestFixture]
    public class SeleniumTests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3000/form.php");

        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();

        }
        //function to upload file with a file addresss as a param
        private void FileUpload(string filename) {
            //for no file selected
            if(filename != " ")
            {
                driver.FindElement(By.Name("document")).SendKeys(filename);    //send keys value pathauna


            }
            driver.FindElement(By.Name("submit")).Click();
        }

        [Test]
        public void FileUploadTest()
        {

            //2. file move/upload test 
            FileUpload("F:\\uploads\\LAB.pdf");
            var messsage = driver.FindElement(By.ClassName("success")).Text;
            Assert.That(messsage, Is.EqualTo("Thank you. Your file is uploaded."));
        }
        [Test]
        public void NoFileUpload()
        {
            FileUpload(" ");
            var messsage = driver.FindElement(By.ClassName("no-file-upload")).Text;
            Assert.That(messsage, Is.EqualTo("No file was uploaded. Please select a file"));
        }
        [Test]
        //greater than 5MB
        public void FileSizeGreaterThan5MB()
        {
            FileUpload("F:\\uploads\\6mb.pdf");
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));
            //wait.Until(EC driver.FindElement(By.ClassName("sizeok")).Displayed);
            var message = driver.FindElement(By.ClassName("sizeok")).Text;
            Assert.That(message, Is.EqualTo("Too large file. Limit to 5MB."));
        }

        //less than 5MB
        [Test]
        public void FileSizeLessThan5MB()

        {
            FileUpload("F:\\uploads\\LAB.pdf");
            var messsage = driver.FindElement(By.ClassName("success")).Text;
            Assert.That(messsage, Is.EqualTo("Thank you. Your file is uploaded."));
        }
        //equalto5MB
        [Test]
        public void FileSizeEqualto5MB()

        {
            FileUpload("F:\\uploads\\LAB.pdf");
            var messsage = driver.FindElement(By.ClassName("success")).Text;
            Assert.That(messsage, Is.EqualTo("Thank you. Your file is uploaded."));
        }


        [Test]
        public void FileTypeValidation()
        {
            FileUpload("F:\\uploads\\Bibek.jpg");
            var messsage = driver.FindElement(By.ClassName("not-pdf")).Text;
            Assert.That(messsage, Is.EqualTo("Sorry! Choose a pdf file."));



        }
    }
}
