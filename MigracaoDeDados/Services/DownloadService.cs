using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace MigracaoDeDados.Services
{
    class DownloadService
    {
        public FileInfo Download(string path, string url)
        {
            bool fileExists = false;
            string finalPath = Path.Combine(path, "DADOS_ABERTOS_CNPJ_01.zip");
            
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddUserProfilePreference("download.default_directory", path);
                options.AddArguments("--headless");

                IWebDriver driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();
                driver.Url = url;
                driver.FindElement(By.Id("uc-download-link")).Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(360));
                wait.Until<bool>(x => fileExists = File.Exists(finalPath));
            }
            catch (Exception)
            {
                return null;
            }

            FileInfo fileInfo = new FileInfo(finalPath);

            return fileInfo;
        }
    }
}
