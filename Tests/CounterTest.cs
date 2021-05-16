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
    public class CounterTest
    {
        IWebDriver driver;
        public static string INDEX_URL = "http://localhost:3000/";
        public static string ADDRESEARCH_URL = "http://localhost:3000/addResearch";
        public static string COUNTER_URL = "http://localhost:3000/counter";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(COUNTER_URL);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void CounterButtonTest()
        {
            //Initial counter value 
            string expectedInitialValue = "0";
            string expectedModifiedValue = "5";
            //get the initial value 
            string initialValue = driver.FindElement(By.ClassName("counter")).Text;
            //Assert the value
            Assert.AreEqual(expectedInitialValue, initialValue);
            //Let's press the button a few times
            for (int i = 0; i < 5; i++)
            {
                driver.FindElement(By.ClassName("btn-counter")).Click();
            }
            //get the modified value 
            string modifiedValue = driver.FindElement(By.ClassName("counter")).Text;
            //Assert the value
            Assert.AreEqual(expectedModifiedValue, modifiedValue);
        }

        [Test]
        public void StylingTest()
        {
            //Declare expected values
            string paddingExcpected = "16px 32px";
            string borderRadiusExpected = "5px";
            string fontWeightExpected = "700";
            string backgroundColorExpected = "rgba(123, 104, 238, 1)";

            //Retrieve element
            IWebElement button = driver.FindElement(By.ClassName("btn-counter"));
            //Assert the element css properties
            Assert.AreEqual(paddingExcpected, button.GetCssValue("padding"));
            Assert.AreEqual(borderRadiusExpected, button.GetCssValue("border-radius"));
            Assert.AreEqual(fontWeightExpected, button.GetCssValue("font-weight"));
            Assert.AreEqual(backgroundColorExpected, button.GetCssValue("background-color"));
        }
    }
}
