using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PWPOM.PWTests.Pages;
using PWPOM.Test_Data_Classes;
using PWPOM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWPOM.PWTests.Tests
{
    public class LoginPageTests : PageTest
    {
        Dictionary<string, string> Properties = new Dictionary<string, string>();
        string? currdir = Directory.GetParent(@"../../../")?.FullName;

        private void ReadConfigSettings()
        {
            string fileName = currdir + "/Config Settings/config.properties";
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains('='))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    Properties[key] = value;
                }
            }

        }

        [SetUp] 
        public async Task SetUp() 
        {
            ReadConfigSettings();
            Console.WriteLine("Browser Opened");
            
            await Page.GotoAsync(Properties["baseUrl"]);
            
            Console.WriteLine("Page Loaded");

        }

        [Test]
        [TestCase("admin", "password")]
        [TestCase("admin", "xxx")]
        public async Task LoginTest(string uname, string pwd) 
        {
            LoginPage loginPage = new LoginPage(Page);

            await loginPage.ClickLoginLink();
           
            await loginPage.Login(uname, pwd);
           
            Assert.IsTrue(await loginPage.CheckWelcomeMsg());

        }
    }
}
