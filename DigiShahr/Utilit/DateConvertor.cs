﻿using DigiShahr.Classes;
using PersianTools.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigiShahr
{
    public static class DateConvertor
    {
        public static bool CheckDateIsHolyday(this PersianDateTime persianDate) =>
           persianDate.IsHoliDay;

        public static bool CheckDateIsValid(this PersianDateTime date)
        {
            var dateNow = new PersianDateTime(DateTime.Now);
            if (dateNow >= date)
            {
                return true;
            }
            return false;

        }


        public static bool CheckDateShamsi(this string date)
        {
            try
            {
                var persianDate2 = new PersianDateTime(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool PersianNumberToEnglishNumber(this string persianStr, out string date)
        {
            try
            {
                Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
                {
                    ['۰'] = '0',
                    ['۱'] = '1',
                    ['۲'] = '2',
                    ['۳'] = '3',
                    ['۴'] = '4',
                    ['۵'] = '5',
                    ['۶'] = '6',
                    ['۷'] = '7',
                    ['۸'] = '8',
                    ['۹'] = '9',
                    ['/'] = '/'
                };
                foreach (var item in persianStr)
                {
                    persianStr = persianStr.Replace(item, LettersDictionary[item]);
                }
                date = persianStr;
                return true;
            }
            catch
            {
                date = "";
                return false;
            }
          
        }


        public static DateTime ShamsiToMiladi(this string date,out bool isValid,out string showErroe)
        {
            if (!date.PersianNumberToEnglishNumber(out date))
            {
                isValid=false;
                showErroe = "لطفا تاریخ را به درستی انتخاب کنید";
                return new DateTime();
            }
            if (!date.CheckDateShamsi())
            {
                isValid = false;
                showErroe = "لطفا تاریخ را به درستی انتخاب کنید";
                return new DateTime();
            }
            var dateTimeShamsi = new PersianDateTime(date);
            if (dateTimeShamsi.CheckDateIsHolyday())
            {
                isValid = false;
                showErroe = "لطفا روزهای کاری را انتخاب کنید (روزهای تعطیل امکان رزرو نیست)";
                return new DateTime();
            }
            if (dateTimeShamsi.CheckDateIsValid())
            {
                isValid = false;
                showErroe = "امروز و روزهای قبل قابل انتخاب نیست";
                return new DateTime();
            }
            isValid = true;
            showErroe = "";
            DateTime dateTimeMilady = date.DateShamsiToMiladi();
            return dateTimeMilady;
        }

    }
}
