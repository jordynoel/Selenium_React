using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_React.Tests
{
    public class FormTest
    {
        IWebDriver driver;
        public static string INDEX_URL = "http://localhost:3000/";
        public static string ADDRESEARCH_URL = "http://localhost:3000/addResearch";
        public static string COUNTER_URL = "http://localhost:3000/counter";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(ADDRESEARCH_URL);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void DataTypeTest()
        {
            //Define incorrect data
            string incorrectDataType = "ThisIsNotaNumber";
            //initial field value 
            string initialValue = "0";
            //Fill in the incorrect data
            driver.FindElement(By.Id("inputPages")).SendKeys(incorrectDataType);
            //retrieve the value
            var elementValue = driver.FindElement(By.Id("inputPages")).GetProperty("value");
            //Assert the value
            Assert.AreNotEqual(incorrectDataType, elementValue);
        }

        [Test]
        public void DataRequiredError()
        {
            int amountOfErrors = 4;
            //We will submit an empty form, this should give some errors
            driver.FindElement(By.ClassName("submit")).Click();
            //Let's verify we have errors
            var elements = driver.FindElements(By.ClassName("error-text"));
            foreach(var e in elements)
            {
                Assert.IsTrue(e.Text.Contains("is required"));
            }
            //Let's verify the amount of errors 
            Assert.AreEqual(amountOfErrors, elements.Count);
        }

        [Test]
        public void FieldWidthTest()
        {
            //The Title field can't be longer than 30 characters.
            var stringLong = "ThisStringIs30CharactersLong!!ThisPartIsToLong";
            var stringGood = "ThisStringIs30CharactersLong!!";
            //Fill in a title that is to long. Only the first 30 characters should be accepted
            driver.FindElement(By.Id("inputTitle")).SendKeys(stringLong);
            //Retrieve the value
            string titleValue = driver.FindElement(By.Id("inputTitle")).GetAttribute("value");
            //Assert the value
            Assert.AreEqual(stringGood, titleValue);
        }

        [Test]
        public void FillFormTest()
        {
            string title = "New title";
            string author = "Jordy Noel";
            string url = "http://localhost:3000";
            string pages = "456";
            driver.FindElement(By.Id("inputTitle")).SendKeys(title);
            driver.FindElement(By.Id("inputAuthor")).SendKeys(author);
            driver.FindElement(By.Id("inputUrl")).SendKeys(url);
            driver.FindElement(By.Id("inputPages")).SendKeys(pages);

        }






    }
}
