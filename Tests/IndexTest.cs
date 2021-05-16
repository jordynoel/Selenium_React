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
    public class IndexTest
    {
        IWebDriver driver;
        public static string INDEX_URL = "http://localhost:3000/";
        public static string ADDRESEARCH_URL = "http://localhost:3000/addResearch";
        public static string COUNTER_URL = "http://localhost:3000/counter";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3000/");
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void CheckElementsTest()
        {
            //Check elements on the page. We do not have to  make an asserts as the test will fail if the elements are not found.
            //Check title
            IWebElement title = driver.FindElement(By.XPath("//*[contains(text(),'Research library!!')]"));
            //Check nav-elements
            IWebElement nav1 = driver.FindElement(By.XPath("//*[contains(text(),'Research library')]"));
            IWebElement nav2 = driver.FindElement(By.XPath("//*[contains(text(),'Add research')]"));
            IWebElement nav3 = driver.FindElement(By.XPath("//*[contains(text(),'Dropdown')]"));
        }

        [Test]
        public void NavigationTest()
        {
            //We are going to test if the Add research nav redirects us to the page addResearch
            //We can copy the web element over from the other test. If the file would contain many navigational tests it could be
            //useful to declare some global variables.
            driver.FindElement(By.XPath("//*[contains(text(),'Add research')]")).Click();
            //Since the driver is asserting the url before the page has loaded we will have to implement some waits
            //Assert the url.
            Assert.AreEqual(ADDRESEARCH_URL, driver.Url);
            //We are going to test if the Dropdown nav redirects us to the Counter page
            driver.FindElement(By.XPath("//*[contains(text(),'Dropdown')]")).Click();
            //Since the driver is asserting the url before the page has loaded we will have to implement some waits
            //Assert the url.
            Assert.AreEqual(COUNTER_URL, driver.Url);
            //We are going to test if the Research library nav redirects us to the index page
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/header/div/ul/li[1]/a")).Click();
            //Since the driver is asserting the url before the page has loaded we will have to implement some waits
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //Assert the url
            Assert.AreEqual(INDEX_URL, driver.Url);
        }
        [Test]
        public void FilterTest()
        {
            string filterValue = "Gebuisd";
            //Fill in a value in the filter field.
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div/div/div[2]/div[1]/input")).SendKeys(filterValue);
            //Click the filter button
            driver.FindElement(By.ClassName("btn-filter")).Click();
            //get the list of items
            var elements =driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            //Assert the number of elements
            foreach(var e in elements)
            {
                Assert.IsTrue(e.Text.Contains(filterValue));
            }           
        }

        [Test]
        public void ProgressBarTest()
        {
            string initialValue = "5";
            string modifiedValue = "4";
            //Fetch the progress bar element
            IWebElement progressBar = driver.FindElement(By.TagName("progress"));
            //Assert the initial value 
            Assert.AreEqual(initialValue, progressBar.GetAttribute("value"));
            //Delete an item
            driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).First().FindElement(By.Id("delete-item")).Click();
            //Click yes on the alert
            driver.SwitchTo().Alert().Accept();
            //Assert the initial value 
            Assert.AreEqual(modifiedValue, progressBar.GetAttribute("value"));
        }

       




    }
}
