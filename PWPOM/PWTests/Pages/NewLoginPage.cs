using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PWPOM.PWTests.Pages
{
    internal class NewLoginPage
    {
        private IPage _page;
        private ILocator _loginLink => _page.Locator(selector: "text=Login");
       private ILocator _inputUsername => _page.GetByLabel("UserName");
        private ILocator _inputPassword => _page.GetByLabel("Password");
        private ILocator _btnLogin => _page.Locator(selector: "input", new PageLocatorOptions { HasTextString = "Log in" });
        private ILocator _linkWelcomeMsg => _page.Locator(selector: "text='Hello admin!'");

        public NewLoginPage(IPage page) => _page = page;


        public async Task GotoAsync(string url)
        {
            await _page.GotoAsync(url);
        }

        public async Task ClickLoginLink()
        {
            await _linkWelcomeMsg.ClickAsync();
        }

        public async Task Login(string uname, string pwd)
        {
            await _inputUsername.FillAsync(uname);
            await _inputPassword.FillAsync(pwd);
            await _btnLogin.ClickAsync();
        }

        public async Task<bool> CheckWelcomeMsg()
        {
            bool IsWelMsgVisible = await _linkWelcomeMsg.IsVisibleAsync();
            return IsWelMsgVisible;
        }


    }
}
