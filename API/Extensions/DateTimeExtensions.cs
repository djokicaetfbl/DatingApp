using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        /* OVO sa this su EXTENSIONS Metode, odlicna stvar! */
        public static int CalculateAge(this DateTime dob)  // dob - date of birth
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
