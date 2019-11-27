using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyClock
{
    public partial class AlarmPage : ContentPage
    {

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            alarmListView.ItemsSource = await App.Database.GetAllAlarmItemsAsync();
        }

        public AlarmPage()
        {
            InitializeComponent();
        }

        async void OnAlarmItemAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlarmEditPage
            {
                BindingContext = new AlarmItem()
            });
        }

        async void OnAlarmItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AlarmEditPage
                {
                    BindingContext = e.SelectedItem as AlarmItem
                });
            }
        }
    }
}
