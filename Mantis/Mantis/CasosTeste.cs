using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Mantis.PageObjects;
using System;

namespace Mantis
{
    public class CasosTeste
    {
        private IWebDriver driver;
        private LoginPage logar;

        [Theory]
        [InlineData("patrick.sorrentino", "errada", "Your account may be disabled or blocked or the username/password you entered is incorrect.", "font[color='red']")]
        [InlineData("errado", "senha123", "Your account may be disabled or blocked or the username/password you entered is incorrect.", "font[color='red']")]
        [InlineData("patrick.sorrentino", "senha123", "Logged in as: patrick.sorrentino (Patrick Sorrentino - manager)", "[class='login-info-left']")]
        public void RealizaLogin(string usuario, string senha, string msg, string elemento)
        {
            AbreNagevador();
            logar = new LoginPage(driver);
            logar.logaMantis(usuario, senha);
            Thread.Sleep(2000);
            logar.ValidaMsg(elemento, msg);
            Fechar();
        }

        public void AbreNagevador()
        {
            driver = new ChromeDriver();//@"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2017\");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://mantis-prova.base2.com.br/login_page.php");
            driver.Manage().Window.Maximize();
        }
        public void Fechar()
        {
            driver.Close();
            driver.Quit();
        }

        
    }
}
