using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWNUnit
{
    internal class GHPTests :PageTest
    {
        [SetUp]
        public void Setup()
        {
        }
        /*
        //PW Manual instance
            [Test]
            public async Task GHPTest()
            {
                using var playwright = await Playwright.CreateAsync();

                //launch browser
                await using var browser = await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = false
                    });

                //page instance
                var context = await browser.NewContextAsync();

                  var page = await context.NewPageAsync();

                Console.WriteLine("Opened Browser");
                await page.GotoAsync("https://www.google.com/");
                Console.WriteLine("Page Loaded");

                string title = await page.TitleAsync(); // gets title of that page
                Console.WriteLine(title);

                await page.GetByTitle("Search").FillAsync("hp laptop");
                Console.WriteLine("Typed");

                await page.GetByText("Google Search").Last.ClickAsync();
                Console.WriteLine("Clicked");

                title = await page.TitleAsync(); // gets title of that page
                Console.WriteLine(title);

            }
        */
        
    
        //PW managed Instance
       [Test]
        public async Task Test2()
        {


            Console.WriteLine("Opened Browser");
            await Page.GotoAsync("https://www.google.com/");
            Console.WriteLine("Page Loaded");

            string title = await Page.TitleAsync(); // gets title of that page
            Console.WriteLine(title);
           await Expect(Page).ToHaveTitleAsync("Google");

            await Page.GetByTitle("Search").FillAsync("hp laptop");
            Console.WriteLine("Typed");

            await Page.GetByText("Google Search").Last.ClickAsync();
            Console.WriteLine("Clicked");

          //  await Page.Locator(".btnK").ClickAsync();
            /*title = await Page.TitleAsync(); // gets title of that page
            Console.WriteLine(title);*/

            //Assert.That(title.Contains("hp"));
           await Expect(Page).ToHaveTitleAsync("hp laptop - Google Search");
        }
    }
}


