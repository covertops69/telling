using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Telling.Droid.Views.Fragments
{
    public class DatePickerFragment : DialogFragment,
                                  DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string of your choice.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = delegate { };
        static DateTime _startDate;

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected, DateTime? startDate)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;

            _startDate = startDate.HasValue ? startDate.Value : DateTime.Now.AddYears(-30);

            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DatePickerDialog dialog = new DatePickerDialog
                (Activity,
                this,
                _startDate.Year,
                _startDate.Month - 1,
                _startDate.Day);

            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
}