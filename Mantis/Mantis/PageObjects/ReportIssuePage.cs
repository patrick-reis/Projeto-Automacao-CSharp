using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;
using Xunit;

namespace Mantis.PageObjects
{
    class ReportIssuePage
    {
        private IWebDriver driver;
        public ReportIssuePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[href='/bug_report_page.php']")]
        public IWebElement PageReport;

        [FindsBy(How = How.CssSelector, Using = "[name='category_id']")]
        public IWebElement category;

        [FindsBy(How = How.CssSelector, Using = "[name='summary']")]
        public IWebElement summary;

        [FindsBy(How = How.CssSelector, Using = "[name='description']")]
        public IWebElement description;

        [FindsBy(How = How.CssSelector, Using = "[value='Submit Report']")]
        public IWebElement submit;

        public void irReportIssue()
        {
            PageReport.Click();
            Thread.Sleep(2000);
        }

        public void ReportBug(string Categoria, string Assunto, string Descrip)
        {
            category.SendKeys(Categoria);
            summary.SendKeys(Assunto);
            description.SendKeys(Descrip);
            submit.Click();
            Thread.Sleep(5000);
        }

        public void ValidaMsg(string elemento, string msg)
        {

            IWebElement msgTela = driver.FindElement(By.CssSelector(elemento));
            string valor = msgTela.Text;
            try
            {
                bool mnsg = (valor + "").Contains(msg);
                Assert.True(mnsg);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

    }
}
