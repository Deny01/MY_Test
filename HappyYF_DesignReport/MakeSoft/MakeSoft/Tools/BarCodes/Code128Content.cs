namespace MakeSoft.Tools.BarCodes
{
    using System;
    using System.Collections;
    using System.Text;

    public class Code128Content
    {
        private int[] mCodeList;

        public Code128Content(string AsciiData)
        {
            this.mCodeList = this.StringToCode128(AsciiData);
        }

        private CodeSet GetBestStartSet(Code128Code.CodeSetAllowed csa1, Code128Code.CodeSetAllowed csa2)
        {
            int num = 0;
            num += (csa1 == Code128Code.CodeSetAllowed.CodeA) ? 1 : 0;
            num += (csa1 == Code128Code.CodeSetAllowed.CodeB) ? -1 : 0;
            num += (csa2 == Code128Code.CodeSetAllowed.CodeA) ? 1 : 0;
            num += (csa2 == Code128Code.CodeSetAllowed.CodeB) ? -1 : 0;
            return ((num > 0) ? CodeSet.CodeA : CodeSet.CodeB);
        }

        private int[] StringToCode128(string AsciiData)
        {
            int num;
            byte[] bytes = Encoding.ASCII.GetBytes(AsciiData);
            Code128Code.CodeSetAllowed allowed = (bytes.Length > 0) ? Code128Code.CodesetAllowedForChar(bytes[0]) : Code128Code.CodeSetAllowed.CodeAorB;
            Code128Code.CodeSetAllowed allowed2 = (bytes.Length > 0) ? Code128Code.CodesetAllowedForChar(bytes[1]) : Code128Code.CodeSetAllowed.CodeAorB;
            CodeSet bestStartSet = this.GetBestStartSet(allowed, allowed2);
            ArrayList list = new ArrayList(bytes.Length + 3);
            list.Add(Code128Code.StartCodeForCodeSet(bestStartSet));
            for (num = 0; num < bytes.Length; num++)
            {
                int charAscii = bytes[num];
                int lookAheadAscii = (bytes.Length > (num + 1)) ? bytes[num + 1] : -1;
                list.AddRange(Code128Code.CodesForChar(charAscii, lookAheadAscii, ref bestStartSet));
            }
            int num4 = (int) list[0];
            for (num = 1; num < list.Count; num++)
            {
                num4 += num * ((int) list[num]);
            }
            list.Add(num4 % 0x67);
            list.Add(Code128Code.StopCode());
            return (list.ToArray(typeof(int)) as int[]);
        }

        public int[] Codes
        {
            get
            {
                return this.mCodeList;
            }
        }
    }
}

