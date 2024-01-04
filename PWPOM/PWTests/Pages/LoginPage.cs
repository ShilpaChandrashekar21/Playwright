using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWPOM.PWTests.Pages
{
    internal class LoginPage
    {
        private IPage _page;
        private ILocator _loginLink;
        private ILocator _inputUsername;
        private ILocator _inputPassword;
        private ILocator _btnLogin;
        private ILocator _linkWelcomeMsg;

        public LoginPage(IPage page) 
        {
            _page = page;
            _loginLink = _page.Locator(selector: "text=Login");
            _inputUsername = _page.GetByLabel("UserName");
            _inputPassword = _page.GetByLabel("Password");
            _btnLogin = _page.Locator(selector: "input", new PageLocatorOptions { HasTextString = "Log in" });
            _linkWelcomeMsg = _page.Locator(selector: "text='Hello admin!'");
        }

        public async Task GotoAsync()
        {
            await _page.GotoAsync("http://eaapp.somee.com/");
        }

        public async Task ClickLoginLink()
        {
            await _loginLink.ClickAsync();
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
