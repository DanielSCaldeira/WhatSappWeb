using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WhatSappRoboEnvio.Extensao;
using WhatSappWeb.DTO;

namespace WhatSappRoboEnvio.Service
{
    public static class AcaoVerifica
    {
        private static string _url = "https://web.whatsapp.com/";

        private static string _iconesHeader = "PVMjB";
        private static string _campoDePesquisa = "_3qx7_";
        private static string _headerMenuOpcaoLista = "I4jbF";
        private static string _qrCode = "_1QMFu";


        public static bool SeAPaginaFoiCarregada(this IWebDriver driver)
        {
            try
            {
                if (driver.ExecuteAsyncScript("return document.readyState", 60).Equals("complete") == false)
                {
                    Console.WriteLine("Não foi carregado a pagina verifique a conexão!");
                }
                return true;
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Método SeAPaginaFoiCarregada Erro: " + ex);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Método SeAPaginaFoiCarregada Erro: " + ex);
                return false;
            }
        }


        public static void ValidacaoQrCode(this IWebDriver driver)
        {
            try
            {
                driver.FindElement(By.ClassName(_campoDePesquisa), 60);
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Por favor leia o qrCode iniciando movamente..." + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Método ValidacaoQrCode Erro: " + ex);
            }
        }

        public static WebDriverWait IniciarChamadaDaPagina(this IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(_url);
                return wait;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi ler o QrCode tempo limite ultrapassado!", ex);
            }
        }

        //////

        public static void GerarQrCode(this IWebDriver driver)
        {
            try
            {
                string text;
                string textVelha = null;
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = 400, Height = 400, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                do
                {
                    text = driver.FindElement(By.ClassName(_qrCode), 5)?.GetAttribute("data-ref");
                    if (string.IsNullOrEmpty(text) || textVelha == text)
                    {
                        continue;
                    }

                    Bitmap resultado = new Bitmap(bw.Write(text));

                    var path = "D://QrCode";
                    if (Directory.Exists(path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }

                    resultado.Save(path + "/t.jpg");
                    textVelha = text;
                    System.Threading.Thread.Sleep(8000);

                } while (!driver.ExisteDisplayed(By.ClassName(_campoDePesquisa)));
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine("Método GerarQrCode Erro: " + ex.Message);
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Método GerarQrCode Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Método GerarQrCode Erro: " + ex);
            }
        }

        public static MensagemDTO VerificaSeTemMensagemParaEnviar()
        {
            try
            {
                var t = new MensagemDTO()
                {
                    Mensagem = "Teste",
                    Numero = "5561995757864"
                };

                return t;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Método VerificaSeTemMensagemParaEnviar Erro: " + ex);
                return null;
            }
        }

        public static void RedirecionarParaEnviarMensagem(this IWebDriver driver, MensagemDTO resposta)
        {
            try
            {
                resposta.Numero = "5561995757864";
                resposta.Mensagem = "Teste";

                var url = $"https://api.whatsapp.com/send?phone=" + resposta.Numero + "&text=" + resposta.Mensagem;

                driver.Navigate().GoToUrl(url);


                if (VerificaSePaginaFoiCarregada(driver))
                {
                    if (driver.Existe(By.ClassName("_whatsapp_www__block_action"), 30) == true)
                    {
                        var botao1 = driver.FindElement(By.ClassName("_whatsapp_www__block_action"), 30).FindElement(By.TagName("a"), 30);
                        botao1.Click();
                        Console.WriteLine("1");
                    }

                    if (driver.ExisteDisplayed(By.CssSelector("._8ibw>a"), 30) == true)
                    {
                        var texto = driver.FindElement(By.CssSelector("._8ibw>a"), 30).Text;
                        var botao2 = driver.FindElement(By.CssSelector("._8ibw>a"), 30);
                        botao2.Click();
                        Console.WriteLine("2");
                    }

                    if (driver.Existe(By.ClassName("_1U1xa"), 30) == true)
                    {
                        var botao3 = driver.FindElement(By.ClassName("_1U1xa"), 30);
                        botao3.Click();
                        Console.WriteLine("3");
                    }
                }
                else
                {
                    Console.WriteLine("Não foi possivel abrir a pagina para enviar a mensagem");
                }
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Método EnviarMensagem Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Método EnviarMensagem Erro: " + ex);
            }
        }


        public static bool VerificaSePaginaFoiCarregada(this IWebDriver driver)
        {
            try
            {
                if (driver.ExecuteAsyncScript("return document.readyState", 60).Equals("complete") == true)
                {
                    Console.WriteLine("Pagina Carregada");
                    return true;
                }
                return false;
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Método VerificaSePaginaFoiCarregada Erro: " + ex);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Método VerificaSePaginaFoiCarregada Erro: " + ex);
                return false;
            }
        }




        //public static void EnviarMensagem(this IWebDriver driver)
        //{
        //    try
        //    {

        //        var campo = driver.FindElement(By.ClassName(_campoTextoParaResponderMensagem), 30);
        //        System.Threading.Thread.Sleep(2000);
        //        campo.SendKeys(Keys.Enter);
        //    }
        //    catch (NoSuchElementException ex)
        //    {
        //        Console.WriteLine("Método EnviarMensagem Erro: " + ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Método EnviarMensagem Erro: " + ex);
        //    }
        //}

        public static void SairDoWhatsapp(this IWebDriver driver)
        {
            try
            {
                var r = driver.FindElements(By.ClassName(_iconesHeader), 15).LastOrDefault();
                if (r != null)
                    r.Click();
                else
                    new Exception("Não foi encontrado o elemento do menu para sair do Whatsapp");

                var t = driver.FindElement(By.ClassName(_headerMenuOpcaoLista), 15).FindElements(By.TagName("li")).LastOrDefault();
                if (t != null)
                    t.Click();
                else
                    new Exception("Não foi encontrado o elemento da lista de sair do menu Whatsapp");
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Método SairDoWhatsapp Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Método SairDoWhatsapp Erro: " + ex);
            }
        }




        public static bool DesligarRobo()
        {
            return false;
        }
    }
}



