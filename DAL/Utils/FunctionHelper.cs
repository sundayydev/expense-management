using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Utils
{
    public class FunctionHelper
    {
        /// <summary>
        /// Lấy tháng trước và năm tương ứng từ tháng hiện tại và năm hiện tại.
        /// </summary>
        /// <param name="currentMonth">Tháng hiện tại (1-12).</param>
        /// <param name="currentYear">Năm hiện tại.</param>
        /// <returns>Tuple chứa tháng trước và năm tương ứng.</returns>
        public static (int previousMonth, int previousYear) GetPreviousMonth(int currentMonth, int currentYear)
        {
            // Kiểm tra nếu tháng hiện tại là tháng 1
            if (currentMonth == 1)
            {
                return (12, currentYear - 1); // Tháng trước là tháng 12 của năm trước
            }
            else
            {
                return (currentMonth - 1, currentYear); // Các tháng còn lại
            }
        }

        /// <summary>
        /// Chuyển đổi một số nguyên đại diện cho tháng (từ 1 đến 12) thành tên tháng bằng tiếng Việt.
        /// </summary>
        /// <param name="month">Tháng (1-12)</param>
        /// <returns>Chứa tháng kiểu chữ bằng Tiếng Việt</returns>
        public string ConvertMonthToVietnamese(int month)
        {
            string[] vietnameseMonths = 
                {
                    "Tháng Một", "Tháng Hai", "Tháng Ba", "Tháng Tư", "Tháng Năm",
                    "Tháng Sáu", "Tháng Bảy", "Tháng Tám", "Tháng Chín", "Tháng Mười",
                    "Tháng Mười Một", "Tháng Mười Hai"
                };

            if (month >= 1 && month <= 12)
            {
                return vietnameseMonths[month - 1];
            }
            else
            {
                return "Invalid month";
            }
        }

    }
}
