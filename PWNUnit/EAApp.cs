using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWNUnit
{
    [TestFixture]
    internal class EAApp : PageTest
    {

        [Test]
        public async Task LoginTest()
        {
            Console.WriteLine("Browser Opened");
            await Page.GotoAsync("http://eaapp.somee.com/");
            Console.WriteLine("Page Loaded");

            //await Page.GetByText("Login").ClickAsync();
            //var loginBtn = Page.Locator(selector: "text=Login");
            //await loginBtn.ClickAsync();
            //Console.WriteLine("Clicked on Login Button");

            await Page.ClickAsync(selector: "text=Login");
            await Console.Out.WriteLineAsync("Clicked on Login Button");
            await Expect(Page).ToHaveURLAsync("http://eaapp.somee.com/Account/Login");

            //await Page.GetByLabel("UserName").FillAsync("admin");
            //await Page.Locator("#UserName").FillAsync("admin");

            await Page.FillAsync(selector: "#UserName", "admin");
            Console.WriteLine("Entered User name");

            // await Page.GetByLabel("Password").FillAsync("password");
            // await Page.Locator("#Password").FillAsync("password");
            await Page.FillAsync(selector: "#Password", "password");

            Console.WriteLine("Entered password");

            //await Page.Locator("//input[@value ='Log in']").ClickAsync();
            var btnLogin = Page.Locator(selector: "input", new PageLocatorOptions { HasTextString = "Log in" });
            await btnLogin.ClickAsync();
            Console.WriteLine("Clicked");

            //await Expect(Page).ToHaveTitleAsync("Home - Execute Automation Employee App");
            await Expect(Page.Locator(selector: "text='Hello admin!'")).ToBeVisibleAsync();
        }
    }
}
