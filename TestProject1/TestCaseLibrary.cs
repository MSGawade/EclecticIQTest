using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TestProject1
{
    [TestClass]
    public class TestCaseLibrary
    {
        public static TestContext _testcontext;
        IWebDriver driver = new ChromeDriver();

        [ClassInitialize]
        public static void TestClassIntialize(TestContext context)

        {
            _testcontext = context;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl(_testcontext.Properties["ApiUrl"].ToString());
            driver.Manage().Window.Maximize();
            Thread.Sleep(5000);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            driver.Quit();
        }

        //Filter data
        IWebElement filterData => driver.FindElement(By.XPath("//input[@placeholder='filter data']"));

        //Sort data
        SelectElement sortData => new SelectElement(driver.FindElement(By.Id("sort-select")));

        //table
        IWebElement VerifyData => driver.FindElement(By.XPath("//div[@class='table-row']"));

        IList<IWebElement> noOfRows => driver.FindElements(By.XPath("//div[@class='table-row']"));

        [TestMethod]
        [Description("Combination filter data by names and sort data ='Name'")]
        public void TestMethod1()
        {
            string[] name = new string[] { "Man in the Middle", "Password attack","Phishing","Session hijack", "SQL Injection", "XSS" };
            string[] numberOfCases = new string[]{"95k","32.85M", "25.12M", "9024", "1.25M", "29850"};
            string[] averageImpactScore = new string[] { "8.12", "5", "7.18", "5.79", "10.21", "2.19" };
            string[] complexity = new string[] { "high", "low", "low", "high", "medium", "low" };

            string sortDataValue = "Name";
            try
            {

                string dataToBeVerified = "";

                for (int i = 0; i < name.Length; i++)
                {
                    //enter filter data
                    filterData.Clear();
                    filterData.SendKeys(name[i]);
                    Thread.Sleep(1000);

                    //select sort data as Name
                    sortData.SelectByText(sortDataValue);

                    //no of rows by filter
                    int count = noOfRows.Count;
                
                    IWebElement row = driver.FindElement(By.XPath("//div[@class='table-row']"));

                    dataToBeVerified = row.Text;
                    //Verify the values
                    if (dataToBeVerified.Contains(name[i]) && dataToBeVerified.Contains(numberOfCases[i]) && dataToBeVerified.Contains(averageImpactScore[i]) && dataToBeVerified.Contains(complexity[i]))
                    {
                        Assert.IsTrue(true, "displayed");
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

                Thread.Sleep(2000);
            }
            catch(Exception e)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        [Description("Combination filter data by complexity ='low' and sort data ='Number of cases'")]
        public void TestMethod2()
        {
            string[] name = new string[] { "Password attack", "Phishing", "XSS" };
            string[] numberOfCases = new string[] { "32.85M", "25.12M", "29850" };
            string[] averageImpactScore = new string[] { "5", "7.18" , "2.19" };
            string complexity = "low";

            string sortDataValue = "Number of cases";
            try
            {
                //enter filter data
                filterData.Clear();
                filterData.SendKeys(complexity);
                Thread.Sleep(1000);

                //select sort data as Name
                sortData.SelectByText(sortDataValue);

                //find the no of rows using complexity value
                IList<IWebElement> complexityCount = driver.FindElements(By.XPath("//div[text()='" + complexity + "']"));

                if (complexityCount.Count > 0)
                {
                    for (int i = 0; i < complexityCount.Count; i++)
                    {
                        IWebElement names = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']"));
                        IWebElement cases = driver.FindElement(By.XPath("//div[text()='" + numberOfCases[i] + "']"));
                        IWebElement impactScore = driver.FindElement(By.XPath("//div[text()='" + averageImpactScore[i] + "']"));

                        //validate name, case and impact score is present in table
                        Assert.IsTrue(names.Displayed && cases.Displayed && impactScore.Displayed);

                    }
                }
                else
                {
                    Assert.IsTrue(false, "No rows found with the complexity value");
                }

                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        [Description("Combination filter data by complexity ='Medium' and sort data ='Number of cases'")]
        public void TestMethod3()
        {
            string[] name = new string[] { "SQL Injection" };
            string[] numberOfCases = new string[] { "1.25M" };
            string[] averageImpactScore = new string[] { "10.21" };
            string complexity = "medium";

            string sortDataValue = "Number of cases";
            try
            {
                //enter filter data
                filterData.Clear();
                filterData.SendKeys(complexity);
                Thread.Sleep(1000);

                //select sort data as Name
                sortData.SelectByText(sortDataValue);

                //find the no of rows using complexity value
                IList<IWebElement> complexityCount = driver.FindElements(By.XPath("//div[text()='" + complexity + "']"));

                if (complexityCount.Count > 0)
                {
                    for (int i = 0; i < complexityCount.Count; i++)
                    {
                        IWebElement names = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']"));
                        IWebElement cases = driver.FindElement(By.XPath("//div[text()='" + numberOfCases[i] + "']"));
                        IWebElement impactScore = driver.FindElement(By.XPath("//div[text()='" + averageImpactScore[i] + "']"));

                        //validate name, case and impact score is present in table
                        Assert.IsTrue(names.Displayed && cases.Displayed && impactScore.Displayed);

                    }
                }
                else
                {
                    Assert.IsTrue(false, "No rows found with the complexity value");
                }

                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        [Description("Combination filter data by complexity ='high' and sort data ='Number of cases'")]
        public void TestMethod4()
        {
            string[] name = new string[] { "Man in the Middle","Session hijack",  };
            string[] numberOfCases = new string[] { "95k","9024" };
            string[] averageImpactScore = new string[] { "8.12","5.79"};
            string complexity = "high";
            string sortDataValue = "Number of cases";
            try
            {
                //enter filter data
                filterData.Clear();
                filterData.SendKeys(complexity);
                Thread.Sleep(1000);

                //select sort data as Name
                sortData.SelectByText(sortDataValue);

                //find the no of rows using complexity value
                IList<IWebElement> complexityCount = driver.FindElements(By.XPath("//div[text()='"+ complexity + "']"));

                if (complexityCount.Count > 0)
                {
                    for (int i = 0; i < complexityCount.Count; i++)
                    {
                        IWebElement names = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']"));
                        IWebElement cases = driver.FindElement(By.XPath("//div[text()='" + numberOfCases[i] + "']"));
                        IWebElement impactScore = driver.FindElement(By.XPath("//div[text()='" + averageImpactScore[i] + "']"));

                        //validate name, case and impact score is present in table
                        Assert.IsTrue(names.Displayed && cases.Displayed && impactScore.Displayed);

                    }
                }
                else
                {
                    Assert.IsTrue(false, "No rows found with the complexity value");
                }

                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

        }
    }
}
