using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonPlaceholder
{
    internal class PWJsonPlaceholder
    {
        IAPIRequestContext reqRescontext; [SetUp]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            reqRescontext = await playwright.APIRequest.NewContextAsync(
                new APIRequestNewContextOptions
                {
                    BaseURL = "https://jsonplaceholder.typicode.com/",
                    IgnoreHTTPSErrors = true,
                });
        }

        [Test]
        public async Task GetAllUser()
        {
            var getresponse = await reqRescontext.GetAsync(url: "posts");

            await Console.Out.WriteLineAsync("Res: \n" + getresponse.ToString());
            await Console.Out.WriteLineAsync("Code:\n" + getresponse.Status);
            await Console.Out.WriteLineAsync("Text:\n" + getresponse.StatusText);

            Assert.That(getresponse.Status.Equals(200));
            Assert.That(getresponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body :\n" + responseBody.ToString());


        }
        [Test]
        public async Task PostUser()
        {
            var postData = new
            {
                userId = 123,
                title = "Lead ust"
            };
            var jsonData = System.Text.Json.JsonSerializer.Serialize(postData);

            var postresponse = await reqRescontext.PostAsync(url: "posts",
                new APIRequestContextOptions
                {
                    Data = jsonData
                });

            await Console.Out.WriteLineAsync("Res: \n" + postresponse.ToString());
            await Console.Out.WriteLineAsync("Code:\n" + postresponse.Status);
            await Console.Out.WriteLineAsync("Text:\n" + postresponse.StatusText);

            Assert.That(postresponse.Status.Equals(201));
            Assert.That(postresponse, Is.Not.Null);

        }
        [Test]
        [TestCase(129, "Lead Ust", 1)]
        public async Task PutUser(int uid, string title, int id)
        {
            var postData = new
            {
                userId = uid,
                title = title
            };
            var jsonData = System.Text.Json.JsonSerializer.Serialize(postData);

            var putresponse = await reqRescontext.PutAsync(url: "posts/" + id,
                new APIRequestContextOptions
                {
                    Data = jsonData
                });

            await Console.Out.WriteLineAsync("Res: \n" + putresponse.ToString());
            await Console.Out.WriteLineAsync("Code:\n" + putresponse.Status);
            await Console.Out.WriteLineAsync("Text:\n" + putresponse.StatusText);

            Assert.That(putresponse.Status.Equals(200));
            Assert.That(putresponse, Is.Not.Null);

        }
        [Test]
        [TestCase(2)]
        public async Task DeleteUser(int id)

        {

            var delresponse = await reqRescontext.DeleteAsync(url: "posts/" + id);

            await Console.Out.WriteLineAsync("Res: \n" + delresponse.ToString());
            await Console.Out.WriteLineAsync("Code:\n" + delresponse.Status);
            await Console.Out.WriteLineAsync("Text:\n" + delresponse.StatusText);

            Assert.That(delresponse.Status.Equals(200));
            Assert.That(delresponse, Is.Not.Null);

        }
    }
}
