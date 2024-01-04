using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{

    [TestFixture]
    internal class NaaptolTests : PageTest
    {
        [Test]
        public async Task NHPTests()
        {
            Console.WriteLine("Browser Loaded");
            await Page.GotoAsync("https://www.naaptol.com/");
            Console.WriteLine("Page Loaded");

            await Page.GetByPlaceholder("Find your favourite brand, " +
                "product, model and many more").Last.FillAsync("eyewear");
            await Page.PressAsync("body","Enter");
           // await Page.Locator("//div[@class ='search']/a").Last.ClickAsync();

            Console.WriteLine("Performed Search");

            await Expect(Page).ToHaveURLAsync("https://www.naaptol.com/search.html?type=srch_catlg&kw=eyewear");


        }

    }
}
