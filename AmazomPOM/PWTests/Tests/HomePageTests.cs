using AmazomPOM.PWTests.Pages;
using AmazomPOM.TestDataHelperClass;
using AmazomPOM.Utils;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazomPOM.PWTests.Tests
{
    internal class HomePageTests : PageTest
    {
        Dictionary<string, string> Properties = new Dictionary<string, string>();
        string? currdir = Directory.GetParent(@"../../../")?.FullName;

        private void ReadConfigSettings()
        {
            string fileName = currdir + "/ConfigSettings/config.properties";
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

        public async Task SearchTest()
        {
            HomePage homePage = new HomePage(Page);

            string? excelFilePath = currdir + "/TestData/Amazon TestData.xlsx";
            string? sheetName = "Search Data";

            List<SearchData> excelDataList = SearchDataUtil.ReadSearchText(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? searchtext = excelData.SearchText;
                await homePage.Search(searchtext);
                await Console.Out.WriteLineAsync("Searched");
                await Page.ScreenshotAsync(new()
                {
                    Path = currdir + "/Screenshots/ss.png",
                    FullPage = true
                });

                Assert.IsTrue(await homePage.TitleCheck());
            }
        }
    }
}
