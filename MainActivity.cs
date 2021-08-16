using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Globalization;
using Java.Util;
using Android.Util;
using AndroidX.Core.Content;
using AndroidX.Core.App;

namespace DatePickerDialog_Fragment_Xamarin_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DateTime StDate, EndDate;
        TextView txtEventStartDate;
        TextView txtEventEndDate;
        TextView txtEventDescription;
        Button StartDateSelectButton;
        Button EndDateSelectButton;
        Button btnCalendarAdd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            txtEventStartDate = FindViewById<TextView>(Resource.Id.date_display);
            txtEventEndDate = FindViewById<TextView>(Resource.Id.txtDateEnd);
            txtEventDescription = FindViewById<TextView>(Resource.Id.editText1);
            StartDateSelectButton = FindViewById<Button>(Resource.Id.date_select_button);
            EndDateSelectButton = FindViewById<Button>(Resource.Id.date_select_button2);
            btnCalendarAdd = FindViewById<Button>(Resource.Id.btnCalendar);
             StartDateSelectButton.Click += StartDateSelectButton_Click; 
            EndDateSelectButton.Click += EndDateSelectButton_Click;
            btnCalendarAdd.Click += BtnCalendarAdd_Click;
        }
      
        void StartDateSelectButton_Click(object sender, EventArgs eventArgs)
        {
            // Create the dialog
            DatePickerDialog datePickerDialog = new DatePickerDialog(this);

            // Handle the date selected event
            datePickerDialog.DateSet += (object sender, DatePickerDialog.DateSetEventArgs e) => {
                StDate = e.Date;
                txtEventStartDate.Text = e.Date.ToString();
            };

            //Show the dialog
            datePickerDialog.Show();
       
        }
        private void EndDateSelectButton_Click(object sender, EventArgs e)
        {
            // Create the dialog
            DatePickerDialog datePickerDialog = new DatePickerDialog(this);

            // Handle the date selected event
            datePickerDialog.DateSet += (object sender, DatePickerDialog.DateSetEventArgs e) => {
                EndDate = e.Date;
                txtEventEndDate.Text = e.Date.ToString();
            };

            //Show the dialog
            datePickerDialog.Show();
        }
        private void BtnCalendarAdd_Click(object sender, EventArgs e)
        {
            ContentValues eventValues = new ContentValues();

            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, "Testing Calendar");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, txtEventDescription.Text);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetStDateTimeMS(StDate));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetEndDateTimeMS(EndDate));

            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

            var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
            Console.WriteLine("Uri for new event: {0}", uri);
        }
        long GetStDateTimeMS(DateTime date)
        {
            var c = Java.Util.Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("US/Eastern"));

            c.Set(CalendarField.DayOfMonth, date.Day);
            c.Set(CalendarField.HourOfDay, date.Hour);
            c.Set(CalendarField.Minute, date.Minute);
            c.Set(CalendarField.Month, date.Month);
            c.Set(CalendarField.Year, date.Year);

            return c.TimeInMillis;
        }

        long GetEndDateTimeMS(DateTime date)
        {
            var c = Java.Util.Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("US/Eastern"));

            c.Set(CalendarField.DayOfMonth, date.Day);
            c.Set(CalendarField.HourOfDay, date.Hour);
            c.Set(CalendarField.Minute, date.Minute);
            c.Set(CalendarField.Month, date.Month);
            c.Set(CalendarField.Year, date.Year);

            return c.TimeInMillis;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}