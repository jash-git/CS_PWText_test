using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS_PWText_test
{
    /*
    出處: https://social.msdn.microsoft.com/Forums/zh-TW/1d69e2d2-13b9-430d-b036-306a274b229e/c-exlanlan123lanlan321lanaaalanaaalan111lanabc?forum=233
    問題:
        1.採英數字混和使用，至少含一個大寫和一個小寫英文字母(英文字母大小視為不同)
        2.不得訂為4個以上相同英文或數字
           例如:aaaa或AAAA或1111
        3.不得訂為4個以上連續英文或數字(含升冪與降冪排列)
           例如:abcd或dcba或ABCD或DCBA或1234或4321
    */
    class Program
    {

        static bool JLCheckVerify(string s)
        {
            //是否都通過了檢測？
            bool isPass = true;

            Regex NumberPattern = new Regex("[0-9]");
            var NumberPattern01 = NumberPattern.Matches(s);
            if (NumberPattern01.Count == 0)
            {
                return false;
            }

            Regex a_zPattern = new Regex("[a-z]");
            var a_zPattern01 = a_zPattern.Matches(s);
            if (a_zPattern01.Count == 0)
            {
                return false;
            }

            Regex A_ZPattern = new Regex("[A-Z]");
            var A_ZPattern01 = A_ZPattern.Matches(s);
            if (A_ZPattern01.Count == 0)
            {
                return false;
            }

            Regex reg01 = new Regex("([0-9])+|([a-zA-Z])+");//切出所有連續英文或連續數字的子字串，連續長度=1~n
            var matches01 = reg01.Matches(s);

            foreach (Match item01 in matches01)
            {
                if (item01.Value.Length >= 4)//連續英文或連續數字的子字串長度大於四，才要判斷是否為相同字元或者為連續字元
                {
                    int index = 0;//陣列指標
                    int count = 0;//連續比對計數器[四個數，要三次]
                    char chrbuf = '\0';//前一個字元暫存
                    int intvar0 = 0;//變化量
                    int sum = 0;
                    foreach (char c in item01.Value)
                    {
                        if (index > 0)
                        {
                            intvar0 = Math.Abs(Convert.ToInt32(chrbuf) - Convert.ToInt32(c));//計算遞增方向

                            if (intvar0 > 1)//字串各字元之間是否為依序排列
                            {
                                sum = 0;
                                intvar0 = 0;
                                count = 0;
                                continue;
                            }
                            else
                            {
                                sum += intvar0;
                                count++;
                            }
                        }
                        chrbuf = c;
                        index++;

                        if ((count == 3) && ((sum == -3) || (sum == 3) || (sum == 0)))//找到 四個連續的字元 或 四個相同字元
                        {
                            isPass = false;
                            break;
                        }
                    }
                }

                if (!isPass)
                {
                    break;
                }
            }
            return isPass;
        }

        static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

        static void Main(string[] args)
        {
            //測試1：滿足條件
            string a = "1ab2h4Ef5g6h";
            //測試2：連續四個小英文字母
            string b = "1abcd23ef4G";
            //測試3：沒有小寫字母
            string c = "1AB2CD3EF4G";
            //測試4：連續四個數字
            string d = "A8765ab2h4Ef5h";
            //測試5：連續四個大英文字母
            string e = "DCBAab2h4Ef5h";
            //測試6：滿足條件
            string f = "pppab2h4Ef5h";
            //測試7：全部小寫字母
            string g = "qazxswedcvfr";
            //測試8：少大寫
            string h = "a12212";
            //測試9：滿足條件
            string i = "aB12212";
            //輸出結果：
            Console.WriteLine("a字串結果：" + JLCheckVerify(a));
            Console.WriteLine("b字串結果：" + JLCheckVerify(b));
            Console.WriteLine("c字串結果：" + JLCheckVerify(c));
            Console.WriteLine("d字串結果：" + JLCheckVerify(d));
            Console.WriteLine("e字串結果：" + JLCheckVerify(e));
            Console.WriteLine("f字串結果：" + JLCheckVerify(f));
            Console.WriteLine("g字串結果：" + JLCheckVerify(g));
            Console.WriteLine("h字串結果：" + JLCheckVerify(h));
            Console.WriteLine("i字串結果：" + JLCheckVerify(i));
            Pause();
        }
    }
}
