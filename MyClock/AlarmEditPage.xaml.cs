using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyClock
{
    public partial class AlarmEditPage : ContentPage
    {
        public AlarmEditPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var alarmItem = (AlarmItem)BindingContext;
            alarmItem.TimeString = alarmItem.Time.ToString();

            await App.Database.SaveAlarmItemAsync(alarmItem);
            await Navigation.PopAsync();
            Console.WriteLine("Save alarmItem: " + alarmItem);
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var alarmItem = (AlarmItem)BindingContext;
            await App.Database.DeleteAlarmItemAsync(alarmItem);
            await Navigation.PopAsync();
        }
    }
}
