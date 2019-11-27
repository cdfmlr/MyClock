﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyClock
{
    public partial class ClockPage : ContentPage
    {
        // Structure for storing information about the three hands.
        struct HandParams
        {
            public HandParams(double width, double height, double offset) : this()
            {
                Width = width;
                Height = height;
                Offset = offset;
            }

            public double Width { private set; get; }   // fraction of radius
            public double Height { private set; get; }  // ditto
            public double Offset { private set; get; }  // relative to center pivot
        }


        static readonly HandParams secondParams = new HandParams(0.02, 1.1, 0.85);
        static readonly HandParams minuteParams = new HandParams(0.05, 0.8, 0.9);
        static readonly HandParams hourParams = new HandParams(0.125, 0.65, 0.9);

        BoxView[] tickMarks = new BoxView[60];

        public ClockPage()
        {
            InitializeComponent();

            // Create the tick marks (to be sized and positioned later).
            for (int i = 0; i < tickMarks.Length; i++)
            {
                tickMarks[i] = new BoxView { Color = Color.Black };
                absoluteLayout.Children.Add(tickMarks[i]);
            }

            Device.StartTimer(TimeSpan.FromSeconds(1.0 / 60), OnTimerTick);// Create the tick marks (to be sized and positioned later).
            for (int i = 0; i < tickMarks.Length; i++)
            {
                tickMarks[i] = new BoxView { Color = Color.Black };
                absoluteLayout.Children.Add(tickMarks[i]);
            }

            Device.StartTimer(TimeSpan.FromSeconds(1.0 / 60), OnTimerTick);
        }

        void OnAbsoluteLayoutSizeChanged(object sender, EventArgs args)
        {
            // Get the center and radius of the AbsoluteLayout.
            Point center = new Point(absoluteLayout.Width / 2, absoluteLayout.Height / 2);
            double radius = 0.45 * Math.Min(absoluteLayout.Width, absoluteLayout.Height);

            // Position, size, and rotate the 60 tick marks.
            for (int index = 0; index < tickMarks.Length; index++)
            {
                double size = radius / (index % 5 == 0 ? 15 : 30);
                double radians = index * 2 * Math.PI / tickMarks.Length;
                double x = center.X + radius * Math.Sin(radians) - size / 2;
                double y = center.Y - radius * Math.Cos(radians) - size / 2;
                AbsoluteLayout.SetLayoutBounds(tickMarks[index], new Rectangle(x, y, size, size));
                tickMarks[index].Rotation = 180 * radians / Math.PI;
            }

            // Position and size the three hands.
            LayoutHand(secondHand, secondParams, center, radius);
            LayoutHand(minuteHand, minuteParams, center, radius);
            LayoutHand(hourHand, hourParams, center, radius);
        }

        void LayoutHand(BoxView boxView, HandParams handParams, Point center, double radius)
        {
            double width = handParams.Width * radius;
            double height = handParams.Height * radius;
            double offset = handParams.Offset;

            AbsoluteLayout.SetLayoutBounds(boxView,
                new Rectangle(center.X - 0.5 * width,
                              center.Y - offset * height,
                              width, height));

            // Set the AnchorY property for rotations.
            boxView.AnchorY = handParams.Offset;
        }

        bool OnTimerTick()
        {
            // Set rotation angles for hour and minute hands.
            DateTime dateTime = DateTime.Now;
            hourHand.Rotation = 30 * (dateTime.Hour % 12) + 0.5 * dateTime.Minute;
            minuteHand.Rotation = 6 * dateTime.Minute + 0.1 * dateTime.Second;

            // Do an animation for the second hand.
            double t = dateTime.Millisecond / 1000.0;

            if (t < 0.5)
            {
                t = 0.5 * Easing.SpringIn.Ease(t / 0.5);
            }
            else
            {
                t = 0.5 * (1 + Easing.SpringOut.Ease((t - 0.5) / 0.5));
            }

            secondHand.Rotation = 6 * (dateTime.Second + t);

            // Do a check of alarms.
            if (dateTime.Second % 30 == 0 && dateTime.Millisecond % 100 < 5)  // To avoid the  high cpu occupying
            {
                Console.WriteLine("Check");
                List<AlarmItem> alarms = App.Database.GetAlarmItems();
                foreach (var a in alarms)
                {
                    if (a.Work)
                    {
                        if (isOnTime(a.Time, dateTime))
                        {
                            Console.WriteLine("TODO: Alarm!");
                            a.Work = false;
                            App.Database.SaveAlarmItemAsync(a);
                        }
                    }

                }
            }

            // For displaying the time in text.
            BindingContext = dateTime;
            return true;
        }

        bool isOnTime(TimeSpan t, DateTime d)
        {
            return t.Hours == d.Hour && t.Minutes == d.Minute;
        }
    }
}
