using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumQualys
{
    public class QualysWrapper
    {
        private readonly IWebDriver _driver;

        private string _qualysTab;
        private string _previousTab;

        public QualysWrapper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void OpenExtension()
        {
            _driver.Url = "chrome-extension://abnnemjpaacaimkkepphpkaiomnafldi/panel/index.html";
        }

        private void OpenNewWindow()
        {
            (_driver as IJavaScriptExecutor).ExecuteScript("window.open()");
            _qualysTab = _driver.WindowHandles.Last();
            _driver.SwitchTo().Window(_qualysTab);
        }

        private void SwitchToQualysTab()
        {
            _previousTab = _driver.CurrentWindowHandle;
            _driver.SwitchTo().Window(_qualysTab);
        }

        private void SwitchToPreviousTab()
        {
            _driver.SwitchTo().Window(_previousTab);
        }

        public void StartCapture(string testCaseName)
        {
            var untitledTestCaseElement = _driver.FindElement(By.XPath("//*[@id='testCase-grid']//*[text()='Untitled Test Case']"));
            new Actions(_driver)
                .ContextClick(untitledTestCaseElement)
                .Click(_driver.FindElement(By.XPath("//a[text()='Rename Test Case']")))
                .Build()
                .Perform();
            var alert = _driver.SwitchTo().Alert();
            alert.SendKeys(testCaseName);
            alert.Accept();
            ClickRecord();
        }

        public void AddWait()
        {
            // you could provide some logic to insert waiting between steps statically,
            // or alternatively use driver events to insert these commands or overwrite the edited ones instead to make the script less brittle
            // obviously if the tests have to take into account that the driver could have qualys attached or not is not a good idea
            // so it would be up to you to figure it out,
            // if you have many loaders you could just use a post click event to insert waiting for the loader to appear and disappear, same with navigation
        }

        public void StopCapture(string fileName)
        {
            ClickRecord();
            _driver.FindElement(By.Id("download")).Click();
            var alert = _driver.SwitchTo().Alert();
            alert.SendKeys(fileName);
            alert.Accept();
        }

        private void ClickRecord() =>
            _driver.FindElement(By.Id("record")).Click();
    }
}
