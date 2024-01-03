
using Microsoft.Playwright;

//playwright setup
//using - automatically gc cleans if the instance is not in use
//await - async operations
 using var playwright = await Playwright.CreateAsync();

//launch browser
await using var browser = await playwright.Chromium.LaunchAsync();

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

