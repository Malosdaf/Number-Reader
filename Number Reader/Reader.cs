using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_Reader
{
    public class EnglishReader
    {
        private readonly string[] number = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private readonly string[] teen_num = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private readonly string[] ten = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private readonly string[] level = { string.Empty, "thousand", "million", "billion", "trillion", "quadrillion", "quintillions" };

        private string Hundred(char s)
        {
            if (s == '0')
            {
                return string.Empty;
            }
            return $"{number[s - '0']}-hundred";
        }

        public string LastTwoNumbers(string s)
        {
            if (s[0] == '0')
            {
                if (s[1] == '0')
                {
                    return string.Empty;
                }
                return number[s[1] - '0'];
            }
            else if (s[0] == '1')
            {
                return (teen_num[s[1] - '0']);
            }
            else
            {
                return $"{ten[s[0] - '2']} {number[s[1] - '0']}";
            }
        }

        public string ReadThreeNumbers(string s)
        {
            string hun = Hundred(s[0]);
            string t = string.Empty;
            t += s[1];
            t += s[2];
            string lt = LastTwoNumbers(t);
            if (hun == string.Empty && lt == string.Empty)
            {
                return string.Empty;
            }
            else if (hun == string.Empty)
            {
                return lt;
            }
            else if (lt == string.Empty)
            {
                return hun;
            }
            return $"{hun} and {lt}";
        }

        public string Read(string s)
        {
            int l = 0;
            string res = string.Empty;
            for (int i = s.Length - 1; i >= 0; i -= 3)
            {
                char a, b, c;
                a = s[i];
                if (i - 1 < 0)
                {
                    b = '0';
                }
                else
                {
                    b = s[i - 1];
                }
                if (i - 2 < 0)
                {
                    c = '0';
                }
                else
                {
                    c = s[i - 2];
                }
                string u = string.Empty;
                u += c; u += b; u += a;
                res =
                    (u == "001" || l == 0) ?
                    $"{ReadThreeNumbers(u)} {level[l]} {res}" :
                    $"{ReadThreeNumbers(u)} {level[l]} {res}";
                l++;
            }
            return res;
        }
    };

    public class VietnameseReader
    {
        private readonly string[] number = { string.Empty, "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        private readonly string[] level = { string.Empty, "nghìn", "triệu", "tỉ", "nghìn tỉ", "triệu tỉ" };

        private string Hundred(char s)
        {
            if (s == '0')
            {
                return "không trăm";
            }
            return $"{number[s - '0']} trăm";
        }

        public string LastTwoNumbers(string s)
        {
            if (s[0] == '0')
            {
                if (s[1] == '0')
                {
                    return string.Empty;
                }
                return number[s[1] - '0'];
            }
            else if (s[0] == '1')
            {
                if (s[1] == '0')
                {
                    return "mười";
                }
                return $"mười {number[s[1] - '0']}";
            }
            else
            {
                string res = $"{number[s[0] - '0']} mươi ";
                if (s[1] == 0)
                {
                    return res;
                }
                res += number[s[1] - '0'];
                return res;
            }
        }

        public string ReadThreeNumbers(string s)
        {
            string hun = Hundred(s[0]);
            string t = $"{s[1]}{s[2]}";
            string lt = LastTwoNumbers(t);
            if (s[1] == '0')
            {
                if (s[2] == '0')
                {
                    return hun;
                }
                return $"{hun} lẻ {lt}";
            }
            return $"{hun} {lt}";
        }

        public string Read(string s)
        {
            int l = 0;
            string res = string.Empty;
            for (int i = s.Length - 1; i >= 0; i -= 3)
            {
                char a, b, c;
                int cnt = 1;
                a = s[i];
                if (i - 1 < 0)
                {
                    b = '0';

                }
                else
                {
                    b = s[i - 1];
                    cnt++;
                }
                if (i - 2 < 0)
                {
                    c = '0';

                }
                else
                {
                    c = s[i - 2];
                    cnt++;
                }
                string u = $"{c}{b}{a}";

                if (cnt < 3)
                {
                    string p = $"{b}{a}";
                    res = $"{LastTwoNumbers(p)} {level[l]} {res}";
                }
                else if (u != "000")
                {
                    res = $"{ReadThreeNumbers(u)} {level[l]} {res}";
                }
                l++;
            }
            return res;
        }
    };

}
