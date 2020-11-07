namespace MakeSoft.Tools
{
    using System;

    public abstract class FunctionsStrings
    {
        protected FunctionsStrings()
        {
        }

        public static bool CheckCGC(string cgc)
        {
            string str = "";
            try
            {
                for (int i = 0; i < cgc.Length; i++)
                {
                    char ch = cgc[i];
                    if ((ch.CompareTo('0') >= 0) && ((ch = cgc[i]).CompareTo('9') <= 0))
                    {
                        str = str + cgc[i];
                    }
                }
                cgc = str;
                str = str.Substring(0, str.Length - 2);
                return ((str.Length == 12) && (cgc == GetCGCDig(str)));
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckCPF(string cpf)
        {
            string str = "";
            try
            {
                for (int i = 0; i < cpf.Length; i++)
                {
                    char ch = cpf[i];
                    if ((ch.CompareTo('0') >= 0) && ((ch = cpf[i]).CompareTo('9') <= 0))
                    {
                        str = str + cpf[i];
                    }
                }
                cpf = str;
                str = cpf.Substring(0, cpf.Length - 2);
                return ((str.Length == 9) && (cpf == GetCPFDig(str)));
            }
            catch
            {
                return false;
            }
        }

        public static string GetCGCDig(string cgc)
        {
            string num = cgc + Mod11(cgc, 9);
            return (num + Mod11(num, 9));
        }

        public static string GetCPFDig(string cpf)
        {
            string num = cpf + Mod11(cpf, 0);
            return (num + Mod11(num, 0));
        }

        public static string Mod10(string num)
        {
            int num2 = 0;
            int num4 = 2;
            for (int i = num.Length; i >= 0; i++)
            {
                int num5 = (Convert.ToInt16(num[i]) % 10) * num4;
                num2 = (num2 + (num5 % 10)) + (num5 % 10);
                if (num4 == 2)
                {
                    num4 = 1;
                }
                else
                {
                    num4 = 2;
                }
            }
            num2 = 10 - (num2 % 10);
            if (num2 == 10)
            {
                num2 = 0;
            }
            return num2.ToString();
        }

        public static string Mod11(string num, int loop)
        {
            int num2 = 0;
            int num4 = 2;
            for (int i = num.Length - 1; i >= 0; i--)
            {
                char ch = num[i];
                num2 += Convert.ToInt16(ch.ToString()) * num4;
                num4++;
                if ((loop > 0) && (num4 > loop))
                {
                    num4 = 2;
                }
            }
            num2 = 11 - (num2 % 11);
            if (num2 >= 10)
            {
                num2 = 0;
            }
            return num2.ToString();
        }
    }
}

