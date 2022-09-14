using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace KobeTaiGuidance_Client
{
    public class Client_Page
    {
        public IPage Page;
        public string? Server;
    }
    public class Browser_Client
    {
        private IBrowser browser;
        private IBrowserContext context;
        private bool loggedIn;

        public Browser_Client()
        {
            CheckInstallation();
            InitiateBrowser().Wait();
        }
        private void CheckInstallation()
        {
            var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
            if (exitCode != 0)
            {
                throw new Exception($"Playwright exited with code {exitCode}");
            }
        }
        private async Task InitiateBrowser()
        {
            var playwright = await Playwright.CreateAsync();
            browser = await playwright.Firefox.LaunchAsync();
            context = await browser.NewContextAsync();
        }

        public async Task<Client_Page> OpenNewPage()
        {
            var page = await context.NewPageAsync();
            Client_Page clientPage = new()
            {
                Page = page
            };

            return clientPage;
        }
        public async Task<bool> LogIn(Client_Page clientPage)
        {
            var config = new ConfigurationBuilder()
                            .AddUserSecrets<Browser_Client>()
                            .Build();

            await clientPage.Page.FillAsync("#ctl00_cphRightColumn_ucLogin_txtUsername", config["Username"]);
            await clientPage.Page.FillAsync("#ctl00_cphRightColumn_ucLogin_txtPassword", config["Password"]);
            await clientPage.Page.ClickAsync("#ctl00_cphRightColumn_ucLogin_btnLogin");

            var taskWaitingLoginSuccess = clientPage.Page.WaitForSelectorAsync("#ctl00_ctl08_btnLogout");
            var taskWaitingForErrorNotification = clientPage.Page.WaitForSelectorAsync(".notification-error");

            Task taskCompleted = await Task.WhenAny(new Task[] { taskWaitingLoginSuccess, taskWaitingForErrorNotification });
            if (taskCompleted == taskWaitingLoginSuccess)
            {
                loggedIn = true;
                await GetServerAsync(clientPage);
                return true;
            }

            return false;
        }

        private static async Task GetServerAsync(Client_Page clientPage)
        {
            await clientPage.Page.WaitForLoadStateAsync();
            clientPage.Server = clientPage.Page.Url.Split('/')[2];
        }

        public bool IsLoggedIn()
        {
            return loggedIn;
        }
    }
}

