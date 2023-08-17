using bot_zap1.Models;

namespace bot_zap1
{
    class Program
    {
       static void Main(string[] args)
        {
            // Crie a instância do bot
            var web = new ZapSendMensage();

            // Aguarde um tempo para escanear o código QR manualmente no navegador
            Console.WriteLine("Por favor, prepare o celular para escanear o código QR quando estiver pronto aperte alguma tecla para continuar.");
            Console.ReadLine();

            // Continue com o bot
            web.Start(); // Utilize a codificação Unicode do caractere especial
        }
    }
}
