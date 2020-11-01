using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WhatSappRoboEnvio.Extensao
{
    public static class WebDriverExtensao
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, uint timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, uint timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElements(by));
            }
            return driver.FindElements(by);
        }


        public static object ExecuteAsyncScript(this IWebDriver driver, string script, uint timeoutInSeconds = 0, params object[] args)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript(script, args));
            }
            return ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
        }

        /// <summary>
        ///     Method that finds an element based on the search parameters within a specified timeout.
        /// </summary>
        /// <param name="context">The context where this is searched. Required for extension methods</param>
        /// <param name="by">The search parameters that are used to identify the element</param>
        /// <param name="timeOutInSeconds">The time that the tool should wait before throwing an exception</param>
        /// <returns> The first element found that matches the condition specified</returns>
        public static IWebElement FindElement(this ISearchContext context, By by, uint timeOutInSeconds)
        {
            if (timeOutInSeconds > 0)
            {
                var wait = new DefaultWait<ISearchContext>(context);
                wait.Timeout = TimeSpan.FromSeconds(timeOutInSeconds);
                return wait.Until<IWebElement>(ctx => ctx.FindElement(by));
            }
            return context.FindElement(by);
        }

        /// <summary>
        ///     Method that finds a list of elements based on the search parameters within a specified timeout.
        /// </summary>
        /// <param name="context">The context where this is searched. Required for extension methods</param>
        /// <param name="by">The search parameters that are used to identify the element</param>
        /// <param name="timeoutInSeconds">The time that the tool should wait before throwing an exception</param>
        /// <returns>A list of all the web elements that match the condition specified</returns>
        public static IReadOnlyCollection<IWebElement> FindElements(this ISearchContext context, By by, uint timeoutInSeconds)
        {

            if (timeoutInSeconds > 0)
            {
                var wait = new DefaultWait<ISearchContext>(context);
                wait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
                return wait.Until<IReadOnlyCollection<IWebElement>>(ctx => ctx.FindElements(by));
            }
            return context.FindElements(by);
        }

        /// <summary>
        ///     Method that finds a list of elements with the minimum amount specified based on the search parameters within a specified timeout.<br/>
        /// </summary>
        /// <param name="context">The context where this is searched. Required for extension methods</param>
        /// <param name="by">The search parameters that are used to identify the element</param>
        /// <param name="timeoutInSeconds">The time that the tool should wait before throwing an exception</param>
        /// <param name="minNumberOfElements">
        ///     The minimum number of elements that should meet the criteria before returning the list <para/>
        ///     If this number is not met, an exception will be thrown and no elements will be returned
        ///     even if some did meet the criteria
        /// </param>
        /// <returns>A list of all the web elements that match the condition specified</returns>
        public static IReadOnlyCollection<IWebElement> FindElements(this ISearchContext context, By by, uint timeoutInSeconds, int minNumberOfElements)
        {
            var wait = new DefaultWait<ISearchContext>(context);
            if (timeoutInSeconds > 0)
            {
                wait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            }

            // Wait until the current context found the minimum number of elements. If not found after timeout, an exception is thrown
            wait.Until<bool>(ctx => ctx.FindElements(by).Count >= minNumberOfElements);

            //If the elements were successfuly found, just return the list
            return context.FindElements(by);
        }

        public static IWebElement WaitUntilVisible(this IWebDriver driver, By by, int secondsTimeout = 10)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, secondsTimeout));
            var element = wait.Until(driver =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(by);
                    if (elementToBeDisplayed.Displayed)
                    {
                        return elementToBeDisplayed;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }

            });
            return element;
        }

        public static IReadOnlyCollection<IWebElement> WaitUntilVisibles(this IWebDriver driver, By by, int secondsTimeout = 10)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, secondsTimeout));
            var elements = wait.Until(driver =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElements(by);
                    if (elementToBeDisplayed.All(x => x.Displayed == true))
                    {
                        return elementToBeDisplayed;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }

            });
            return elements;
        }

        public static IWebElement WaitUntilVisible(this IWebElement elemento, By by, int secondsTimeout = 10)
        {
            var driver = ((IWrapsDriver)elemento).WrappedDriver;
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, secondsTimeout));
            var element = wait.Until(elemento =>
            {
                try
                {
                    var elementToBeDisplayed = elemento.FindElement(by);
                    if (elementToBeDisplayed.Displayed)
                    {
                        return elementToBeDisplayed;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }

            });
            return element;
        }

        public static IReadOnlyCollection<IWebElement> WaitUntilVisibles(this IWebElement elemento, By by, int secondsTimeout = 10)
        {
            var driver = ((IWrapsDriver)elemento).WrappedDriver;
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, secondsTimeout));
            var elements = wait.Until(elemento =>
            {
                try
                {
                    var elementToBeDisplayed = elemento.FindElements(by);
                    if (elementToBeDisplayed != null && elementToBeDisplayed.All(x => x.Displayed == true))
                    {
                        return elementToBeDisplayed;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }

            });
            return elements;
        }

        public static bool Existe(this IWebElement elemento, By by)
        {
            try
            {
                elemento.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool ExisteDisplayed(this IWebElement elemento, By by, uint secondsTimeout = 10)
        {
            try
            {
                var driver = ((IWrapsDriver)elemento).WrappedDriver;
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsTimeout));

                if (secondsTimeout > 0)
                {
                    return wait.Until(drv => drv.FindElement(by).Displayed == true);
                }

                return wait.Until(drv => drv.FindElement(by).Displayed == true);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool ExisteDisplayed(this IWebDriver driver, By by, uint secondsTimeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsTimeout));
                if (secondsTimeout > 0)
                {
                    return wait.Until(drv => drv.FindElement(by).Displayed == true);
                }

                return wait.Until(drv => drv.FindElement(by).Displayed == true);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }   
        
        public static bool Existe(this IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }    
        public static bool Existe(this IWebDriver driver, By by, uint secondsTimeout = 10)
        {
            try
            {
                driver.FindElement(by, secondsTimeout);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ElementoTemAClasse(this IWebElement elemento, string classe)
        {
            try
            {
                return elemento.GetAttribute("class").Split(" ").ToList().Contains(classe);
            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine("Erro no metodo -> ElementoTemAClasse" + ex);
                return false;
            }
        }

        public static bool ElementoTemUmaDasClasses(this IWebElement elemento, string[] classes)
        {
            try
            {
                return elemento.GetAttribute("class").Split(" ").ToList().Any(x => classes.Contains(x));
            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine("Erro no metodo -> ElementoTemAClasse" + ex);
                return false;
            }
        }

        public static bool ElementoTemStyle(this IWebElement elemento, string style)
        {
            try
            {
                style.Replace(" ", "");
                var teste = elemento.GetAttribute("style").Replace(" ", "").Split(";").ToList().Contains(style);
                return teste;
            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine("Erro no metodo -> ElementoTemAClasse" + ex);
                return false;
            }

        }

        public static IEnumerable<IWebElement> ElementosTemStyle(this IEnumerable<IWebElement> elementos, string style)
        {
            try
            {
                style = style.Replace(" ", "");
                return elementos.Where(x => x.GetAttribute("style").Replace(" ", "").Split(";").Contains(style));
            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine("Erro no metodo -> ElementoTemAClasse" + ex);
                return null;
            }
        }

        public static bool ElementosTemUmaDasClasse(this IEnumerable<IWebElement> elementos, string[] classes)
        {
            try
            {
                return elementos.Select(x => x.GetAttribute("class").Split(" ")).SelectMany(x => x).Any(x => classes.Contains(x));
            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine("Erro no metodo -> ElementoTemAClasse" + ex);
                return false;
            }
        }

        public static bool ElementosTemAClasse(this IEnumerable<IWebElement> elementos, string classe)
        {
            try
            {
                return elementos.Select(x => x.GetAttribute("class").Split(" ")).SelectMany(x => x).Contains(classe);
            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine("Erro no metodo -> ElementoTemAClasse" + ex);
                return false;
            }
        }

        public static void scrollUp(this IWebDriver driver, By by, string style)
        {
            try
            {
                while (driver.FindElements(by).ElementosTemStyle(style).FirstOrDefault() == null)
                {
                    driver.FindElement(By.TagName("body")).SendKeys(Keys.PageUp);
                }
            }
            catch (WebDriverException ex)
            {
                Console.WriteLine("Erro no metodo -> scrollUp" + ex);
            }
        }

    }
}
