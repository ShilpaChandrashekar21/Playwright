using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazomPOM.PWTests.Pages
{
    internal class HomePage
    {
        private IPage _page;
        //private ILocator _searchbar => _page.GetByPlaceholder("Search Amazon");
        private ILocator _searchbar => _page.Locator(selector: "id='twotabsearchtextbox'");
        
        private ILocator _searchbutton => _page.Locator("//input[contains(@id,'nav')]");

        private ILocator _titleCheck => _page.GetByText("Amazon.com : mobiles");

        public HomePage(IPage page) => _page = page;
        public async Task GotoAsync(string url)
        {
            await _page.GotoAsync(url);
        }

        public async Task Search(string pname)
        {
            await _searchbar.FillAsync(pname);
            await _searchbutton.ClickAsync();
        }

        public async Task<bool> TitleCheck()
        {
           bool IsVisi = await _titleCheck.IsVisibleAsync();
            return IsVisi;
        }
    }
}
