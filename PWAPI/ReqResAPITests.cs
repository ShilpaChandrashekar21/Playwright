using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PWAPI
{
    public class ReqResAPITests
    {
        IAPIRequestContext requestContext;
        
        [SetUp]
        public async Task SetUp()
        {
            var playwright = await Playwright.CreateAsync();//creating playwright
            requestContext = await playwright.APIRequest.NewContextAsync(//creating api request for context
                new APIRequestNewContextOptions
                {
                    BaseURL = "https://reqres.in/api/",
                    IgnoreHTTPSErrors = true
                });
        }

        [Test]
        [TestCase(2)]
        public async Task GetAllUsers(int pageNum)
        {
            var getresponse = await requestContext.GetAsync(url: "users?page="+pageNum);
            await Console.Out.WriteLineAsync("Res: \n " + getresponse.ToString());
            await Console.Out.WriteLineAsync("Code: \n " + getresponse.Status);
            await Console.Out.WriteLineAsync("Text: \n " + getresponse.StatusText);

            Assert.That(getresponse.Status.Equals(200));
            Assert.That(getresponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body: \n "+responseBody.ToString());
            
            var response = JObject.Parse(responseBody.ToString());
            await Console.Out.WriteLineAsync("Parsed: \n " + response);
        }

        [Test]
        [TestCase(2)]
        public async Task GetSingleUser(int id)
        {
            var getresponse = await requestContext.GetAsync(url: "users/" + id);
            await Console.Out.WriteLineAsync("Res: \n " + getresponse.ToString());
            await Console.Out.WriteLineAsync("Code: \n " + getresponse.Status);
            await Console.Out.WriteLineAsync("Text: \n " + getresponse.StatusText);

            Assert.That(getresponse.Status.Equals(200));
            Assert.That(getresponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body: \n " + responseBody.ToString());
            
            var response = JObject.Parse(responseBody.ToString());
            await Console.Out.WriteLineAsync("Parsed: \n " + response);

        }

        [Test]
        [TestCase(22)]
        public async Task GetSingleUserNotFound( int id)
        {
            var getresponse = await requestContext.GetAsync(url: "users/"+id);
            await Console.Out.WriteLineAsync("Res: \n " + getresponse.ToString());
            await Console.Out.WriteLineAsync("Code: \n " + getresponse.Status);
            await Console.Out.WriteLineAsync("Text: \n " + getresponse.StatusText);

            Assert.That(getresponse.Status.Equals(404));
            Assert.That(getresponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body: \n " + responseBody.ToString());

            Assert.IsNotEmpty(responseBody.ToString());

        }

        [Test]
        [TestCase("John", "Engineer")]
        public async Task CreateUser(string nm, string jb)
        {
            var requestBody = new
            {
                name = nm,
                job = jb
            };

            var jsonString = System.Text.Json.JsonSerializer.Serialize(requestBody);

            var postresponse = await requestContext.PostAsync(url: "users",
                new APIRequestContextOptions
                {
                    Data = jsonString //sending json data with url for post request
                });

            await Console.Out.WriteLineAsync("Res: \n " + postresponse.ToString());
            await Console.Out.WriteLineAsync("Code: \n " + postresponse.Status);
            await Console.Out.WriteLineAsync("Text: \n " + postresponse.StatusText);

            Assert.That(postresponse.Status.Equals(201));
            Assert.That(postresponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await postresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body: \n " + responseBody.ToString());

            var response = JObject.Parse(responseBody.ToString());
            await Console.Out.WriteLineAsync("Parsed: \n " + response);

        }

        [Test]
        [TestCase("John","Engineer",2)]
        public async Task UpdateUser(string nm, string jb,int id)
        {
            var requestBody = new
            {
                name = nm,
                job = jb
            };

            var jsonString = System.Text.Json.JsonSerializer.Serialize(requestBody);

            var putresponse = await requestContext.PutAsync(url: "users/"+id,
                new APIRequestContextOptions
                {
                    Data = jsonString 
                });

            await Console.Out.WriteLineAsync("Res: \n " + putresponse.ToString());
            await Console.Out.WriteLineAsync("Code: \n " + putresponse.Status);
            await Console.Out.WriteLineAsync("Text: \n " + putresponse.StatusText);

            Assert.That(putresponse.Status.Equals(200));
            Assert.That(putresponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await putresponse.JsonAsync();
            await Console.Out.WriteLineAsync("Res Body: \n " + responseBody.ToString());

            var response = JObject.Parse(responseBody.ToString());
            await Console.Out.WriteLineAsync("Parsed: \n " + response);

        }

        [Test]
        [TestCase(2)]
        public async Task DeleteUser(int id)
        {

            var deleteresponse = await requestContext.DeleteAsync(url: "users/"+id);
                

            await Console.Out.WriteLineAsync("Res: \n " + deleteresponse.ToString());
            await Console.Out.WriteLineAsync("Code: \n " + deleteresponse.Status);
            await Console.Out.WriteLineAsync("Text: \n " + deleteresponse.StatusText);

            Assert.That(deleteresponse.Status.Equals(204));
            Assert.That(deleteresponse, Is.Not.Null);

        }


    }
}
