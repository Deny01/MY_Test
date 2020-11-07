namespace MakeSoft.Tools.BarCodes
{
    using System;

    public static class Code128Code
    {
        private const int cCODEA = 0x65;
        private const int cCODEB = 100;
        private const int cSHIFT = 0x62;
        private const int cSTARTA = 0x67;
        private const int cSTARTB = 0x68;
        private const int cSTOP = 0x6a;

        public static bool CharCompatibleWithCodeset(int CharAscii, CodeSet currcs)
        {
            CodeSetAllowed allowed = CodesetAllowedForChar(CharAscii);
            return (((allowed == CodeSetAllowed.CodeAorB) || ((allowed == CodeSetAllowed.CodeA) && (currcs == CodeSet.CodeA))) || ((allowed == CodeSetAllowed.CodeB) && (currcs == CodeSet.CodeB)));
        }

        public static CodeSetAllowed CodesetAllowedForChar(int CharAscii)
        {
            if ((CharAscii >= 0x20) && (CharAscii <= 0x5f))
            {
                return CodeSetAllowed.CodeAorB;
            }
            return ((CharAscii < 0x20) ? CodeSetAllowed.CodeA : CodeSetAllowed.CodeB);
        }

        public static int[] CodesForChar(int CharAscii, int LookAheadAscii, ref CodeSet CurrCodeSet)
        {
            int num = -1;
            if (!CharCompatibleWithCodeset(CharAscii, CurrCodeSet))
            {
                if ((LookAheadAscii != -1) && !CharCompatibleWithCodeset(LookAheadAscii, CurrCodeSet))
                {
                    switch (CurrCodeSet)
                    {
                        case CodeSet.CodeA:
                            num = 100;
                            CurrCodeSet = CodeSet.CodeB;
                            goto Label_0052;

                        case CodeSet.CodeB:
                            num = 0x65;
                            CurrCodeSet = CodeSet.CodeA;
                            goto Label_0052;
                    }
                }
                else
                {
                    num = 0x62;
                }
            }
        Label_0052:
            if (num != -1)
            {
                return new int[] { num, CodeValueForChar(CharAscii) };
            }
            return new int[] { CodeValueForChar(CharAscii) };
        }

        public static int CodeValueForChar(int CharAscii)
        {
            return ((CharAscii >= 0x20) ? (CharAscii - 0x20) : (CharAscii + 0x40));
        }

        public static int StartCodeForCodeSet(CodeSet cs)
        {
            return ((cs == CodeSet.CodeA) ? 0x67 : 0x68);
        }

        public static int StopCode()
        {
            return 0x6a;
        }

        public enum CodeSetAllowed
        {
            CodeA,
            CodeB,
            CodeAorB
        }
    }
}

