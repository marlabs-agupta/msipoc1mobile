using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net;
using System.Net.Http;

namespace MSIPoC1Mobile
{
    public partial class MainPage : ContentPage
    {
        public string LoggedInUserName { get; set; }
        public MainPage (string UserName)
        {
            LoggedInUserName = UserName;
            BindingContext = this;
            InitializeComponent();
            welcomeUser.Text = "Hello " + LoggedInUserName;
            IList<Movie> usermovies = new List<Movie>();
            usermovies.Add(new Movie() { Name = "Star Wars", Genre = "Scifi" });
            usermovies.Add(new Movie() { Name = "Scream", Genre = "Horror" });
            usermovies.Add(new Movie() { Name = "Abyss", Genre = "Scifi" });
            usermovies.Add(new Movie() { Name = "Godfather", Genre = "Drama" });
            MoviesListView.ItemsSource = usermovies;
        }

        public MainPage()
        {
            InitializeComponent();
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Bootstrapper.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

        //async Task<IList<Movie>> GetMovies(string username)
        //{

        //}
    }
}
