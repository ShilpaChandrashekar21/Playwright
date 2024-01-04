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
        
        public async Task LoginTest() 
        {
            NewLoginPage loginPage = new (Page);
            string? excelFilePath = currdir + "/Test Data/Google Data.xlsx";
            string? sheetName = "Search Data";

            List<EAText> excelDataList = LoginCredDataRead.ReadEAText(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? username = excelData.Username;
                string? password = excelData.Password;
                await loginPage.ClickLoginLink();

                await loginPage.Login(username, password);
                await Page.ScreenshotAsync(new()
                {
                    Path = currdir + "/Screenshots/ss.png",
                    FullPage = true
                });

                Assert.IsTrue(await loginPage.CheckWelcomeMsg());

            }

        }
    }
}
