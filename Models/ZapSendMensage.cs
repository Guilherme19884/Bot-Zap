using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;


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

         private List<string> motivationalMessages = new List<string>
            {
                "O sucesso é ir de fracasso em fracasso sem perder entusiasmo. - Winston Churchill",
                "Acredite em si mesmo e todo o resto se encaixará. Tenha fé em seus próprios poderes, incluindo o poder de superar qualquer obstáculo. - Norman Vincent Peale",
                "O único limite para nossas realizações de amanhã serão as nossas dúvidas de hoje. - Franklin D. Roosevelt",
                "A jornada de mil milhas começa com um único passo. - Lao Tsé",
                "A maneira de começar é parar de falar e começar a fazer. - Walt Disney",
                "O maior prazer na vida é fazer aquilo que as pessoas dizem que você não pode fazer. - Walter Bagehot",
                "O pessimista reclama do vento; o otimista espera que ele mude; o realista ajusta as velas. - William Arthur Ward",
                "Não importa o quão devagar você vá, desde que não pare. - Confúcio",
                "O único lugar onde o sucesso vem antes do trabalho é no dicionário. - Vidal Sassoon",
                "O segredo de ficar à frente é começar. - Mark Twain",
                "Oportunidades não acontecem. Você as cria. - Chris Grosser",
                "O sucesso não é a chave para a felicidade. A felicidade é a chave para o sucesso. Se você ama o que faz, você será bem-sucedido. - Albert Schweitzer",
                "O que você obtém alcançando seus objetivos não é tão importante quanto o que você se torna alcançando seus objetivos. - Zig Ziglar",
                "O que você faz hoje pode melhorar todos os seus amanhãs. - Ralph Marston",
                "A única maneira de fazer um excelente trabalho é amar o que você faz. - Steve Jobs",
                "Acredite que você pode e você já está no meio do caminho. - Theodore Roosevelt",
                "Nossa maior fraqueza está em desistir. A maneira mais certa de ter sucesso é sempre tentar apenas mais uma vez. - Thomas Edison",
                "Se você não está disposto a arriscar, você não pode crescer. Se você não arriscar, você fica parado para sempre. - Brian Tracy",
                "Não é o mais forte que sobrevive, nem o mais inteligente. É o que melhor se adapta à mudança. - Charles Darwin",
                "A vitória é reservada para aqueles que estão dispostos a pagar o preço. - Sun Tzu",
                "O sucesso não é definitivo, o fracasso não é fatal: é a coragem de continuar que conta. - Winston Churchill",
                "A melhor maneira de prever o futuro é criá-lo. - Peter Drucker",
                "Aquele que tem um porquê para viver pode suportar quase qualquer como. - Friedrich Nietzsche",
                "A paixão é a energia. Sinta o poder que vem de se concentrar no que te excita. - Oprah Winfrey",
                "Acredite em suas esperanças, não em seus medos. - Nelson Mandela",
                "Se você não gosta de algo, mude. Se você não pode mudar, mude sua atitude. - Maya Angelou",
                "Você não falha até parar de tentar. - Albert Einstein",
                "A vida é 10% do que acontece comigo e 90% de como reajo a isso. - Charles R. Swindoll",
                
                // Adicione suas mensagens motivacionais aqui
            };

          private string GetRandomMessage()
        {
            Random random = new Random();
            int index = random.Next(motivationalMessages.Count);
            return motivationalMessages[index];
        }
        public void Start()
        {
            NavigateWhatsApp();
            // O login será realizado automaticamente.

              while (true)
            {
                string randomMessage = GetRandomMessage();
                SendMessageToGroup("Cravo & Canela", randomMessage);

                // Aguarde 24 horas para enviar a próxima mensagem
                TimeSpan waitTime = TimeSpan.FromHours(24);
                Task.Delay(waitTime).Wait();
            }
        }

        public void SendMessageToGroup(string groupName, string message)
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
            // Esse foi o exemplo usado na v1 string message = "7. A preguiça é a mãe do progresso. Se o homem não tivesse preguiça de caminhar, não teria inventado a roda." + "\n" + "– Mario Quintana";
           
            // Digita a mensagem no campo de mensagem.
            messageField.SendKeys(message);

            // Envia a mensagem pressionando a tecla Enter.
            messageField.SendKeys(Keys.Enter);

            // Aguarda um tempo após o envio da mensagem.
            Thread.Sleep(2000);
        }
    }
}
