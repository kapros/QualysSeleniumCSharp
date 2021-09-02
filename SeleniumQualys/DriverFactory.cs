using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumQualys
{
    public class DriverFactory
    {
        public static IWebDriver CreateChromeWithQualys(ChromeOptions opts)
        {
            opts.AddExtension("qualys.crx");
            return new ChromeDriver(opts);
        }

        public static ChromeOptions DefaultChromeOptions
        {
            get
            {
                return new ChromeOptions();
            }
        }
    }

    public static class DriverExtensions
    {
        public static ChromeOptions AddDownloadPath(this ChromeOptions opts, string path)
        {
            opts.AddUserProfilePreference("download.default_directory", path);
            return opts;
        }
    }
}