using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Mantis.PageObjects;
using System;
using System.Linq;
using System.IO;

namespace Mantis
{
    public class CasosTeste
    {
        private IWebDriver driver;
        private LoginPage logar;
        private ReportIssuePage reIssue;
        private ManageProjectsPage maProjects;
        private static int cont = 0;

        /*
         * Testes:
         * 1- Senha incorreta
         * 2- E-mail incorreto
         * 3- Logando com sucesso
         */
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
            Screenshot(driver, "RealizaLogin");
            logar.ValidaMsg(elemento, msg);
            Fechar();
        }

        /*
         * Testes:
         * 1- Testando obrigatoriedade do campo Categoria
         * 2- Testando obrigatoriedade do campo Assunto
         * 3- Testando obrigatoriedade do campo Descrição
         * 4- Reportando erro com sucesso
         */
        [Theory]
        [InlineData("", "Erros encontrados", "Erro ao logar","Category", "p[style='color:red']")]
        [InlineData("[All Projects] General", "", "Erro ao logar", "Summary", "p[style='color:red']")]
        [InlineData("[All Projects] General", "Erros encontrados", "", "Description", "p[style='color:red']")]
        [InlineData("[All Projects] General", "Erros encontrados", "Erro ao logar", "Viewing Issues", "span[class='floatleft']")]
        public void ReportIssues(string categoria, string assunto, string descricao, string mensagem, string elementoMgs)
        {
            AbreNagevador();
            logar = new LoginPage(driver);
            logar.logaMantis("patrick.sorrentino", "senha123");
            Thread.Sleep(2000);
            reIssue = new ReportIssuePage(driver);
            reIssue.irReportIssue();
            reIssue.ReportBug(categoria, assunto, descricao);
            Screenshot(driver, "ReportIssues");
            reIssue.ValidaMsg(elementoMgs, mensagem);
            Fechar();
        }

        /*
         * Testes:
         * 1- Categoria já existente
         * 2- Cadastrando uma nova categoria
         */
        [Theory]
        [InlineData("Apptest", "A category with that name already exists.", "p[style='color:red']")]
        [InlineData("Aleatorio", "Global Categories", "td[colspan='3']")]
        public void criarCategoria(string categoria, string Msg, string elementoMsg)
        {
            AbreNagevador();
            logar = new LoginPage(driver);
            logar.logaMantis("patrick.sorrentino", "senha123");
            Thread.Sleep(2000);
            maProjects = new ManageProjectsPage(driver);
            maProjects.irManageProjects();
            if (categoria == "Aleatorio") categoria = alfanumericoAleatorio();
            maProjects.AdicionarCategory(categoria);
            Screenshot(driver, "criarCategoria");
            maProjects.ValidaMsg(elementoMsg, Msg);
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

        public string alfanumericoAleatorio()
        {
            int tamanho = 10;
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public void Screenshot(IWebDriver driver, string nome)
        {
            string screenshotsPasta = @"C:\Users\Patrick Reis\Documents\Git\Base2\Mantis\Mantis\Mantis\Screenshot\" + nome + "_" + cont++ +".png";
            ITakesScreenshot camera = driver as ITakesScreenshot;
            Screenshot foto = camera.GetScreenshot();
            foto.SaveAsFile(screenshotsPasta, ScreenshotImageFormat.Png);


        }

    }
}
