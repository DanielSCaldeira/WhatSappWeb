using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using WhatSappRoboEnvio.Service;

namespace WhatSappRoboEnvio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando robo...");
            IWebDriver driver = new ChromeDriver("C:\\Selenium\\");
            try
            {
                WebDriverWait wait = AcaoVerifica.IniciarChamadaDaPagina(driver);
                AcaoVerifica.SeAPaginaFoiCarregada(driver);
                AcaoVerifica.GerarQrCode(driver);
                AcaoVerifica.ValidacaoQrCode(driver);

                do
                {
                    //Proxima Mensagem
                    //VerificaSeTemMensagemParaEnviar
                    var mensagem = AcaoVerifica.VerificaSeTemMensagemParaEnviar();
                    //EnviarMensagem
                    AcaoVerifica.RedirecionarParaEnviarMensagem(driver, mensagem);
                    //Tempo para poder recarregar novamente a tarefa 

                    System.Threading.Thread.Sleep(2300);

                } while (!AcaoVerifica.DesligarRobo());

                AcaoVerifica.SairDoWhatsapp(driver);

            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine(ex);
                driver.Close();
                driver.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                driver.Close();
                driver.Dispose();
            }
            finally
            {
                Iniciar.IniciarRoboNovamente();
            }
        }
    }

    class Iniciar
    {
        public static int QtdDeVezesQueORoboFoiIniciado = 0;
        public static void IniciarRoboNovamente()
        {
            try
            {
                QtdDeVezesQueORoboFoiIniciado++;
                new Program();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Tentativa sem sucesso de reiniciar o Robo! Error->", ex);
            }
        }
    }
}
