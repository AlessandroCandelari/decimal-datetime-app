using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxViewClock
{
    internal static class RepublicanDatetimeFormat
    {
        internal static char[] allStandardFormats =
        {
            'd', 'D',
            'f', 'F', 
            /*'g', 'G',*/ //era
            'm', 'M',
            'y'
        };

        private static int ParseRepeatPattern(String format, int pos, char patternChar)
        {
            int len = format.Length;
            int index = pos + 1;
            while ((index < len) && (format[index] == patternChar))
            {
                index++;
            }
            return (index - pos);
        }
        
        internal static String Format(RepublicanDatetime dateTime, String format)
        {
            StringBuilder result = new StringBuilder();

            int i = 0;
            int tokenLen;

            while (i < format.Length)
            {
                char ch = format[i];
                int nextChar;
                switch (ch)
                {
                    case 'h':
                    case 'H':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        FormatDigits(result, dateTime.RepublicanHours, tokenLen);
                        break;
                    case 'm':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        FormatDigits(result, dateTime.RepublicanMinutes, tokenLen);
                        break;
                    case 's':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        FormatDigits(result, dateTime.RepublicanSeconds, tokenLen);
                        break;
                    /*case 'f':
                    case 'F':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        if (tokenLen <= MaxSecondsFractionDigits)
                        {
                            long fraction = (dateTime.Ticks % Calendar.TicksPerSecond);
                            fraction = fraction / (long)Math.Pow(10, 7 - tokenLen);
                            if (ch == 'f')
                            {
                                result.Append(((int)fraction).ToString(fixedNumberFormats[tokenLen - 1], CultureInfo.InvariantCulture));
                            }
                            else
                            {
                                int effectiveDigits = tokenLen;
                                while (effectiveDigits > 0)
                                {
                                    if (fraction % 10 == 0)
                                    {
                                        fraction = fraction / 10;
                                        effectiveDigits--;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (effectiveDigits > 0)
                                {
                                    result.Append(((int)fraction).ToString(fixedNumberFormats[effectiveDigits - 1], CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    // No fraction to emit, so see if we should remove decimal also.
                                    if (result.Length > 0 && result[result.Length - 1] == '.')
                                    {
                                        result.Remove(result.Length - 1, 1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
                        }
                        break;*/
                    case 'd':
                        // tokenLen == 1 : Day of month as digits with no leading zero. 
                        // tokenLen == 2 : Day of month as digits with leading zero for single-digit months.
                        // tokenLen >= 3 : Day of year as its full name. 
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        if (tokenLen <= 2)
                        {
                            int day = dateTime.RepublicanDay;
                            FormatDigits(result, day, tokenLen);
                        }
                        else
                        {
                            result.Append(dateTime.DayName);
                        }
                        break;
                    case 'M':
                        // tokenLen == 1 : Month as digits with no leading zero.
                        // tokenLen == 2 : Month as digits with leading zero for single-digit months.
                        // tokenLen == 3 : Month as a three-letter abbreviation. 
                        // tokenLen >= 4 : Month as its full name.
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        int month = dateTime.RepublicanMonth;
                        if (tokenLen <= 2)
                        {
                            FormatDigits(result, month, tokenLen);
                        }
                        else
                        {
                            if (tokenLen >= 4)
                            {
                                result.Append(dateTime.MonthName);
                            }
                            else
                            {
                                var abbreviateMonth = dateTime.MonthName.Substring(0, 3);
                                result.Append(abbreviateMonth);
                            }
                        }
                        break;
                    case 'y':
                        int year = dateTime.RepublicanYear;
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        if (tokenLen <= 2)
                        {
                            FormatDigits(result, year % 100, tokenLen);
                        }
                        else
                        {
                            String fmtPattern = "D" + tokenLen;
                            result.Append(year.ToString(fmtPattern, CultureInfo.InvariantCulture));
                        }
                        break;
                    case 'Y':
                        tokenLen = ParseRepeatPattern(format, i, ch);
                        result.Append(ToRoman(dateTime.RepublicanYear));
                        break;
                    case '\\':
                        // Escaped character.  Can be used to insert character into the format string. 
                        nextChar = ParseNextChar(format, i);
                        if (nextChar >= 0)
                        {
                            result.Append(((char)nextChar));
                            tokenLen = 2;
                        }
                        else
                        {
                            // This means that '\' is at the end of the formatting string.
                            throw new FormatException("Format Invalid String");
                        }
                        break;
                    default:
                        result.Append(ch);
                        tokenLen = 1;
                        break;
                }
                i += tokenLen;
            }
            return (result.ToString());
        }

        private static int ParseNextChar(String format, int pos)
        {
            if (pos >= format.Length - 1)
            {
                return (-1);
            }
            return ((int)format[pos + 1]);
        }

        private static void FormatDigits(StringBuilder result, int value, int tokenLen)
        {
            var format = String.Empty;
            for(int i=0; i < tokenLen; i++)
            {
                format += "0";
            }
            result.Append(value.ToString(format));
        }
        private static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException();
        }
    }
}
