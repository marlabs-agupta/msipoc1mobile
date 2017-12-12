using System;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;

namespace MSIPoC1Mobile
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new UserCredentials
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var response = await AreCredentialsCorrect(user);
            if (response != null && response.IsSuccess && response.StatusCode == (int) HttpStatusCode.OK)
            {
                Bootstrapper.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(usernameEntry.Text), this);
                await Navigation.PopAsync();
            }
            else if (response != null && !response.IsSuccess)
            {
                messageLabel.Text = response.Message;
                passwordEntry.Text = string.Empty;
            }
            else 
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }

        async Task<Response> AreCredentialsCorrect(UserCredentials user)
        {
            using (var client = new HttpClient())
                   {
                client.MaxResponseContentBufferSize = 256000;
                var uri = new Uri(string.Format(ConfigSettings.LoginAPI, string.Empty));
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
                        return new Response() { IsSuccess = false, StatusCode = (int) response.StatusCode, Message = response.ReasonPhrase };
                    }
                }
                return null;
            }
                //return false;
            //return user.Username == Constants.Username && user.Password == Constants.Password;
        }
    }
}
