using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using WeatherApp.Adapters;
using WeatherApp.Services;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var cityEditText = FindViewById<EditText>(Resource.Id.cityEditText);
            var searchButton = FindViewById<Button>(Resource.Id.searchButton);
            var temperatureTextView = FindViewById<TextView>(Resource.Id.temperatureTextView);
            var windTextView = FindViewById<TextView>(Resource.Id.windTextView);
            var weatherImageView = FindViewById<ImageView>(Resource.Id.weatherImageView);
            var forecastListView = FindViewById<ListView>(Resource.Id.forecastListView);

            var weatherService = new WeatherService();

            searchButton.Click += async delegate
            {
                var data = await weatherService.GetCityWeather(cityEditText.Text);
                temperatureTextView.Text = Math.Round(data.main.temp).ToString() + " °C";
                windTextView.Text = Math.Round(data.wind.speed).ToString() + " m/s";

                var imageBytes = await weatherService.GetImageFromUrl($"https://openweathermap.org/img/wn/{data.weather[0].icon}@2x.png");
                var bitmap = await BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length);
                weatherImageView.SetImageBitmap(bitmap);

                var weatherForecast = await weatherService.GetCityWeatherForecast(cityEditText.Text);
                forecastListView.Adapter = new ForecastAdapter(this, weatherForecast.list);
            };

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
