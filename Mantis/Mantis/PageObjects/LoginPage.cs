using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using Xunit;

namespace Mantis.PageObjects
{
    class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[name='username']")]
        public IWebElement user; 

        [FindsBy(How = How.CssSelector, Using = "[name='password']")]
        public IWebElement password;

        [FindsBy(How = How.CssSelector, Using = "[value='Login']")]
        public IWebElement entrar;

        [FindsBy(How = How.CssSelector, Using = "[class='login-info-left']")]
        public IWebElement msgInicial;

        public void logaMantis(string usuario, string senha)
        {
            user.SendKeys(usuario);
            password.SendKeys(senha);
            entrar.Click();

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
