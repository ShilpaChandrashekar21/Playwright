using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PWPOM.PWTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWPOM.PWTests.Tests
{
    public class LoginPageTests : PageTest
    {
        [SetUp] 
        public async Task SetUp() 
        {
            Console.WriteLine("Browser Opened");
            
            await Page.GotoAsync("http://eaapp.somee.com/", new PageGotoOptions { Timeout = 10000, 
                    WaitUntil = WaitUntilState.DOMContentLoaded });
            
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
