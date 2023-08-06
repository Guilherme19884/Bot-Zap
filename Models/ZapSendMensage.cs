using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace bot_zap1.Models
{
    public class ZapSendMensage
    {
        public IWebDriver driver;

        private const string MessageFieldLocator = "//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[1]";
        public ZapSendMensage()
        {
            driver = new ChromeDriver();
        }

        public void SiteMensage()
        {
            string url = "https://mundointerpessoal.com/2016/01/as-50-frases-mais-bonitas-da-literatura-brasileira.html";
            driver.Navigate().GoToUrl(url);
        }
        public string CopyText()
        {
            // Obter o link da imagem do elemento na página (se possível).
            IWebElement selectText = driver.FindElement(By.XPath("//*[@id='post-5']/div/div/article/div[2]/blockquote[2]"));

            return selectText.Text;
        }


        public void NavigateWhatsApp()
        {
            string url = "https://web.whatsapp.com/";
            driver.Navigate().GoToUrl(url);

            // Aguarde até que o elemento com a classe "copyable-text selectable-text" esteja presente na página.
            // O tempo máximo de espera é de 60 segundos.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(80));
            IWebElement searchField = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("copyable-text")));

            // Localiza o campo de pesquisa para grupos.
            // IWebElement searchField = driver.FindElement(By.XPath("//*[@id='side']/div[1]/div/div/div[2]/div/div[1]/p"));
        }

        public void Start()
        {
            NavigateWhatsApp();
            // O login será realizado automaticamente.
            // SendMessageToGroup("Família \U0001F495 Lima"); // Utilize a codificação Unicode do caractere especial
            // Task.Delay(2000).Wait();
            SendMessageToGroup("Cravo & Canela");

        }

        public void SendMessageToGroup(string groupName)
        {
            // Localiza o campo de pesquisa para grupos.
            IWebElement searchField = driver.FindElement(By.XPath("//*[@id='side']/div[1]/div/div/div[2]/div/div[1]/p"));

            // Digita o nome do grupo para pesquisar.
            searchField.SendKeys(groupName);

            // Espera até que o grupo correto seja exibido na lista de resultados da pesquisa.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            IWebElement groupElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//span[@title='{groupName}']")));

            // Clique no grupo para abri-lo.
            groupElement.Click();

            // Aguarde até que o campo de mensagem esteja disponível para enviar a mensagem.
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(MessageFieldLocator)));

            // Localiza o campo de mensagem.
            IWebElement messageField = driver.FindElement(By.XPath(MessageFieldLocator));

            // Obter o texto a ser enviado como mensagem.
            string message = "7. A preguiça é a mãe do progresso. Se o homem não tivesse preguiça de caminhar, não teria inventado a roda." + "\n" + "– Mario Quintana";
            messageField.SendKeys(message);


            // Digita a mensagem no campo de mensagem.
            messageField.SendKeys(message);

            // Envia a mensagem pressionando a tecla Enter.
            messageField.SendKeys(Keys.Enter);

            // Aguarda um tempo após o envio da mensagem.
            Thread.Sleep(2000);
        }
    }
}
