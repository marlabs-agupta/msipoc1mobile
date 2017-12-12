using System;
using System.Linq;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace MSIPoC1Mobile
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var user = new UserCredentials()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var response = await RegisterCredentials(user);
            if (response != null && response.IsSuccess && response.StatusCode == (int)HttpStatusCode.OK)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    Bootstrapper.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(usernameEntry.Text), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else if (response != null && !response.IsSuccess)
            {
                messageLabel.Text = response.Message;
                passwordEntry.Text = string.Empty;
            }
            else
            {
                messageLabel.Text = "Registration failed";
                passwordEntry.Text = string.Empty;
            }


        }

        async Task<Response> RegisterCredentials(UserCredentials user)
        {
            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;
                var uri = new Uri(string.Format(ConfigSettings.RegisterAPI, string.Empty));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return new Response() { IsSuccess = true, StatusCode = (int)HttpStatusCode.OK };
                    }
                    else
                    {
                        return new Response() { IsSuccess = false, StatusCode = (int)response.StatusCode, Message = response.ReasonPhrase };
                    }
                }
                return null;
            }
        }
    }
}
