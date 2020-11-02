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
using System.Threading.Tasks;
using WhatSappRoboEnvio.Extensao;
using WhatSappWeb.DTO;
using WhatSappWeb.Model;
using WhatSappWeb.Service;

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

        public static void IniciarChamadaDaPagina(this IWebDriver driver)
        {
            try
            {
                driver.Navigate().GoToUrl(_url);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel IniciarChamadaDaPagina Error:!", ex);
            }
        }

        //////

        public static void GerarQrCode(this IWebDriver driver)
        {
            try
            {
                using (QrCodeService servise = new QrCodeService())
                {
                    string text;
                    string textVelha = null;
                    do
                    {
                        text = driver.FindElement(By.ClassName(_qrCode), 30)?.GetAttribute("data-ref");
                        if (textVelha == text || string.IsNullOrEmpty(text))
                        {
                            Console.WriteLine("continue..");
                            continue;
                        }

                        Console.WriteLine("Salva..");
                        servise.Salvar(new QrCode()
                        {
                            Src = text,
                            TelefoneOrigem = "61995757864",
                            DataExpiracao = DateTime.Now.AddSeconds(20)
                        });

                        textVelha = text;

                        //tempo ideal para esperar
                        System.Threading.Thread.Sleep(19000);
                    } while (!driver.Existe(By.ClassName(_campoDePesquisa)));
                }

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

        public static List<FilaEnvio> VerificaSeTemMensagemParaEnviar(string telefoneOrigem)
        {
            try
            {
                using (FilaEnvioService service = new FilaEnvioService())
                {
                    return service
                         .Listar(x => x.TelefoneOrigem == telefoneOrigem && x.Enviado == false)?
                         .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Método VerificaSeTemMensagemParaEnviar Erro: " + ex);
                return null;
            }
        }

        public static void RedirecionarParaEnviarMensagem(this IWebDriver driver, List<FilaEnvio> filaEnvio)
        {
            try
            {
                using (FilaEnvioService service = new FilaEnvioService())
                {
                    foreach (var item in filaEnvio)
                    {
                        var url = $"https://web.whatsapp.com/send?phone=" + item.TelefoneDestino + "&text=" + item.Mensagem;

                        Console.WriteLine("Redirecionando....");
                        driver.Navigate().GoToUrl(url);


                        if (VerificaSePaginaFoiCarregada(driver))
                        {
                            if (driver.Existe(By.ClassName("_1U1xa"), 30) == true)
                            {
                                Console.WriteLine("enviando....");
                                driver.FindElement(By.ClassName("_1U1xa"), 30).Click();
                                Console.WriteLine("Atualizando item na fila....");
                                item.Enviado = true;
                                service.Gravar(item);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Não foi possivel abrir a pagina para enviar a mensagem");
                        }
                    }
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



