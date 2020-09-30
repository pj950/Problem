using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class WeekAndDate
    {
        public int Week { get; set; }
        public string BTEDate { get; set; }

        public List<DateTime> DateList { get; set; }
    }

    public class DateCommon
    {
        /// <summary>
        /// 获取指定日期，在为一年中为第几周
        /// </summary>
        /// <param name="dt">指定时间</param>
        /// <reutrn>返回第几周</reutrn>
        public static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }

        /// <summary>
        /// 获取指定日期为当月的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="weekStart"></param>
        /// <returns></returns>
        public static int GetWeekOfMonth(DateTime dt)
        {
            int weekStart = 1;

            //WeekStart
            //1表示 周一至周日 为一周
            //2表示 周日至周六 为一周
            DateTime FirstofMonth;
            FirstofMonth = Convert.ToDateTime(dt.Date.Year + "-" + dt.Date.Month + "-" + 1);

            int i = (int)FirstofMonth.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }

            if (weekStart == 1)
            {
                return (dt.Date.Day + i - 2) / 7 + 1;
            }
            if (weekStart == 2)
            {
                return (dt.Date.Day + i - 1) / 7;

            }
            return 0;
            //错误返回值0
        }

        /// <summary>
        /// 计算本周起始日期（礼拜一的日期）
        /// </summary>
        /// <param name="someDate">该周中任意一天</param>
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns>
        public static DateTime CalculateFirstDateOfWeek(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }

        /// <summary>
        /// 计算本周结束日期（礼拜日的日期）
        /// </summary>
        /// <param name="someDate">该周中任意一天</param>
        /// <returns>返回礼拜日日期，后面的具体时、分、秒和传入值相等</returns>
        public static DateTime CalculateLastDateOfWeek(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;// 因为枚举原因，Sunday排在最前，相减间隔要被7减。
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Add(ts);
        }

        /// <summary>
        /// 根据一年中的第几周获取该周的开始日期与结束日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekNumber"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static Tuple<DateTime, DateTime> GetFirstEndDayOfWeek(int year, int weekNumber, System.Globalization.CultureInfo culture)
        {
            System.Globalization.Calendar calendar = culture.Calendar;
            DateTime firstOfYear = new DateTime(year, 1, 1, calendar);
            DateTime targetDay = calendar.AddWeeks(firstOfYear, weekNumber - 1);
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            while (targetDay.DayOfWeek != firstDayOfWeek)
            {
                targetDay = targetDay.AddDays(-1);
            }

            return Tuple.Create<DateTime, DateTime>(targetDay, targetDay.AddDays(6));
        }

        public static WeekAndDate GetFirstEndDayOfWeekStr(int weekNumber, System.Globalization.CultureInfo culture)
        {
            System.Globalization.Calendar calendar = culture.Calendar;
            DateTime firstOfYear = new DateTime(DateTime.Now.Year, 1, 1, calendar);

            var index = (int)firstOfYear.DayOfWeek;


            DateTime targetDay = calendar.AddWeeks(firstOfYear, weekNumber - 1);
            if(index > 1)
            {
                index = index - 1;
                targetDay = targetDay.AddDays(-index);
            }
            //DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            //while (targetDay.DayOfWeek != firstDayOfWeek)
            //{
            //    targetDay = targetDay.AddDays(-1);
            //}
            string str = targetDay.ToShortDateString() + "~" + targetDay.AddDays(6).ToShortDateString();
            WeekAndDate wad = new WeekAndDate();
            wad.Week = weekNumber;
            wad.BTEDate = str;
            wad.DateList = new List<DateTime>();
            wad.DateList.Add(targetDay);
            wad.DateList.Add(targetDay.AddDays(1));
            wad.DateList.Add(targetDay.AddDays(2));
            wad.DateList.Add(targetDay.AddDays(3));
            wad.DateList.Add(targetDay.AddDays(4));


            return wad;
        }


    }
}
