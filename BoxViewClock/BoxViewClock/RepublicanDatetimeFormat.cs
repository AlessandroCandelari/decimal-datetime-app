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
            's', //short
            'T', //long
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
            // This is a flag to indicate if we are formating hour/minute/second only. 
            bool bTimeOnly = true;

            int i = 0;
            int tokenLen, hour12;

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
                        bTimeOnly = false;
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
                        bTimeOnly = false;
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
                        bTimeOnly = false;
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
    }
}
