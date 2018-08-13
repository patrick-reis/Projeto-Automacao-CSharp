using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;
using Xunit;

namespace Mantis.PageObjects
{
    class ManageProjectsPage
    {
        private IWebDriver driver;
        public ManageProjectsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[href='/manage_overview_page.php']")]
        private IWebElement manage;

        [FindsBy(How = How.CssSelector, Using = "[href='/manage_proj_page.php']")]
        private IWebElement manegaProjects;

        [FindsBy(How = How.CssSelector, Using = "[name='name']")]
        private IWebElement category;

        [FindsBy(How = How.CssSelector, Using = "[value='Add Category']")]
        private IWebElement Addcategory;

        public void irManageProjects()
        {
            manage.Click();
            manegaProjects.Click();
            Thread.Sleep(2000);
        }

        public void AdicionarCategory(string valor)
        {
            category.SendKeys(valor);
            Addcategory.Click();
            Thread.Sleep(2000);
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
