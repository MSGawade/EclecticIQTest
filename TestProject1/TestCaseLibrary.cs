using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Collections;

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

        //get the row count for impact score
        IList<IWebElement> impactScoreRows => driver.FindElements(By.XPath("//div[@class='table-data data-averageImpact']"));

        //get the row count for number of cases
        IList<IWebElement> noOfCasesRows => driver.FindElements(By.XPath("//div[@class='table-data data-cases']"));

        //get the row count for name column
        IList<IWebElement> noOfNameRows => driver.FindElements(By.XPath("//div[@class='table-data data-name']"));

        //below scripts TestMethod1 to TestMethod4 validates the table data dynamically based on user input
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

                    //Verify name, cases, impact score and complexity
                    if (dataToBeVerified.Contains(name[i]) && dataToBeVerified.Contains(numberOfCases[i]) && dataToBeVerified.Contains(averageImpactScore[i]) && dataToBeVerified.Contains(complexity[i]))
                    {
                        Assert.IsTrue(true, "displayed");
                    }
                    else
                    {
                        //failure stack trace
                        Assert.IsTrue(false);
                    }
                }

                Thread.Sleep(2000);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
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
                        //get the name
                        IWebElement names = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']"));
                        string actualName = names.Text;

                        //get the number of cases associated with the name
                        IWebElement cases = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']//following-sibling::div[text()='" + numberOfCases[i] + "']"));
                        string actualCases = cases.Text;

                        //get the average impact score associated with name
                        IWebElement impactScore = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']//following-sibling::div[text()='" + averageImpactScore[i] + "']"));
                        string actualImpactScore = impactScore.Text;

                        //validate name, cases and impact score is present in the table
                        if(actualName==name[i] && actualCases== numberOfCases[i] && actualImpactScore== averageImpactScore[i])
                        {
                            Assert.IsTrue(true, "Data validated");
                        }
                        else
                        {
                            Assert.IsTrue(false, "Data Mismatch");
                        }

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
                //failure stack trace
                Assert.Fail(e.Message);
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
                        //get the name
                        IWebElement names = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']"));
                        string actualName = names.Text;

                        //get the number of cases associated with the name
                        IWebElement cases = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']//following-sibling::div[text()='" + numberOfCases[i] + "']"));
                        string actualCases = cases.Text;

                        //get the average impact score associated with name
                        IWebElement impactScore = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']//following-sibling::div[text()='" + averageImpactScore[i] + "']"));
                        string actualImpactScore = impactScore.Text;

                        //validate name, cases and impact score is present in the table
                        if (actualName == name[i] && actualCases == numberOfCases[i] && actualImpactScore == averageImpactScore[i])
                        {
                            Assert.IsTrue(true, "Data validated");
                        }
                        else
                        {
                            Assert.IsTrue(false, "Data Mismatch");
                        }

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
                //failure stack trace
                Assert.Fail(e.Message);
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
                        //get the name
                        IWebElement names = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']"));
                        string actualName = names.Text;

                        //get the number of cases associated with the name
                        IWebElement cases = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']//following-sibling::div[text()='" + numberOfCases[i] + "']"));
                        string actualCases = cases.Text;

                        //get the average impact score associated with name
                        IWebElement impactScore = driver.FindElement(By.XPath("//div[text()='" + name[i] + "']//following-sibling::div[text()='" + averageImpactScore[i] + "']"));
                        string actualImpactScore = impactScore.Text;

                        //validate name, cases and impact score is present in the table
                        if (actualName == name[i] && actualCases == numberOfCases[i] && actualImpactScore == averageImpactScore[i])
                        {
                            Assert.IsTrue(true, "Data validated");
                        }
                        else
                        {
                            Assert.IsTrue(false, "Data Mismatch");
                        }

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
                //failure stack trace
                Assert.Fail(e.Message);
            }

        }

        //below scripts TestMethod5 to TestMethod7 validate the data if it's appearing in ascending order or not
        [TestMethod]
        [Description("Verify ascending order when filter data by complexity ='low' and sort data ='Impact score'")]
        public void TestMethod5()
        {
            string complexity = "low";
            string sortDataValue = "Impact score";
            try
            {
                //enter filter data
                filterData.Clear();
                filterData.SendKeys(complexity);
                Thread.Sleep(1000);

                //select sort data as Name
                sortData.SelectByText(sortDataValue);
                
                IList<float> obtainedList = new List<float>();
                
                //add values to list
                foreach (IWebElement we in impactScoreRows)
                {
                    float i = (float)(Double.Parse(we.Text.ToString()));

                    obtainedList.Add(i);
                }

                //Validate whether values are displayed in ascending order for impact score column
                Assert.IsTrue(obtainedList.OrderBy(c => c).SequenceEqual(obtainedList));

                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                //failure stack trace
                Assert.Fail(e.Message);
            }

        }

        [TestMethod]
        [Description("Verify ascending order when filter data is 'a' and sort data ='number of cases'")]
        public void TestMethod6()
        {
            string filterData1 = "a";
            string sortDataValue = "Number of cases";
            try
            {
                //enter filter data
                filterData.Clear();
                filterData.SendKeys(filterData1);
                Thread.Sleep(1000);

                //select sort data as Name
                sortData.SelectByText(sortDataValue);

                IList<int> obtainedList = new List<int>();
                
                foreach (IWebElement we in noOfCasesRows)
                {
                    //replace k=1000,M=100000,B=10000000

                    string value = we.Text;
                    int i = 0;
                    if (!value.Contains("k") && !value.Contains("M") && !value.Contains("B"))
                    {
                        i = int.Parse(value);
                    }
                    else if (value.Contains("k"))
                    {
                        i = (int)(Double.Parse(value.Substring(0, value.Length - 1)) * 1000);
                    }
                    else if (value.Contains("M"))
                    {
                        i = (int)(Double.Parse(value.Substring(0, value.Length - 1)) * 100000);
                    }
                    else if (value.Contains("B"))
                    {
                        i = (int)(Double.Parse(value.Substring(0, value.Length - 1)) * 10000000);
                    }

                    obtainedList.Add(i);
                }

                //validate ILIST<int> is in sequence or not
                Assert.IsTrue(obtainedList.OrderBy(c => c).SequenceEqual(obtainedList));

                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                //failure stack trace
                Assert.Fail(e.Message);
            }

        }

        [TestMethod]
        [Description("Verify ascending order when filter data is not entered and sort data ='Name'")]
        public void TestMethod7()
        {
            string filterData1 = "";
            string sortDataValue = "Name";
            try
            {
                //enter filter data
                //filterData.Clear();
                //filterData.SendKeys(filterData1);
                //Thread.Sleep(1000);

                //select sort data as Name
                sortData.SelectByText(sortDataValue);

                IList<string> obtainedList = new List<string>();

                //add values to list
                foreach (IWebElement we in noOfNameRows)
                {
                    obtainedList.Add(we.Text);
                }

                //validate ILIST<int> is in sequence or not
                Assert.IsTrue(obtainedList.OrderBy(c => c).SequenceEqual(obtainedList));

                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                //failure stack trace
                Assert.Fail(e.Message);
            }

        }
    }
}
