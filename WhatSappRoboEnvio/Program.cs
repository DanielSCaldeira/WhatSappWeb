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
                AcaoVerifica.IniciarChamadaDaPagina(driver);
                AcaoVerifica.SeAPaginaFoiCarregada(driver);
                AcaoVerifica.GerarQrCode(driver);
                AcaoVerifica.ValidacaoQrCode(driver);
                //var usuario = AcaoVerifica.ValidadarDadosUsuario(driver);
                do
                {
                    //Proxima Mensagem
                    //VerificaSeTemMensagemParaEnviar
                    var mensagensFila = AcaoVerifica.VerificaSeTemMensagemParaEnviar("5561992946818");

                    //EnviarMensagem
                    AcaoVerifica.RedirecionarParaEnviarMensagem(driver, mensagensFila);

                    //Tempo necessario para poder realizar uma nova tarefa 
                    System.Threading.Thread.Sleep(2300);

                } while (!AcaoVerifica.DesligarRobo());

                AcaoVerifica.SairDoWhatsapp(driver);

            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                driver.Close();
                driver.Dispose();
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
                Console.WriteLine("Robo sendo reiniciado.... Quantidade de vezes que o robo caiu"+ QtdDeVezesQueORoboFoiIniciado);
                new Program();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Tentativa sem sucesso de reiniciar o Robo! Error->", ex);
            }
        }
    }
}
