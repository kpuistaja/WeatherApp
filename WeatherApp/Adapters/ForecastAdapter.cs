using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using WeatherApp.Models;

namespace WeatherApp.Adapters
{
    public class ForecastAdapter : BaseAdapter<List>
    {
        List<List> _items;
        Activity _context;

        public ForecastAdapter(Activity context, List<List> items)
        {
            _items = items;
            _context = context;
        }

        public override List this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.forecast_layout, null);
            view.FindViewById<TextView>(Resource.Id.forecastTimeTextView).Text = _items[position].dt_txt; 
            view.FindViewById<TextView>(Resource.Id.forecastTempTextView).Text = Math.Round(_items[position].main.temp).ToString() + " °C";
            view.FindViewById<TextView>(Resource.Id.forecastWindTextView).Text = Math.Round(_items[position].wind.speed).ToString() + " m/s";
            view.FindViewById<TextView>(Resource.Id.forecastDescriptTextView).Text = _items[position].weather[0].main.ToString();

            return view;
        }
    }
}
