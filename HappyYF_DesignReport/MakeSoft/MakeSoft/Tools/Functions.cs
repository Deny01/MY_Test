namespace MakeSoft.Tools
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;
    using System.Text;
    using System.Xml;

    public class Functions
    {
        private static string[] aCent = new string[] { "cento", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos", "setecentos", "oitocentos", "novecentos" };
        private static string[] aDeci = new string[] { "", " centavo", " centavos" };
        private static string[] aDez0 = new string[] { "onze", "doze", "treze", "quatorze", "quinze", "dezesseis", "dezessete", "dezoito", "dezenove" };
        private static string[] aDez1 = new string[] { "dez", "vinte", "trinta", "quarenta", "cinq\x00fcenta", "sessenta", "setenta", "oitenta", "noventa" };
        private static string[] aE = new string[] { "", " e " };
        private static string[] aEspe = new string[] { "", " real", " reais" };
        private static string[] aFlex = new string[] { "", "\x00e3o", "\x00f5es" };
        private static string[] aMilh = new string[] { "", " mil", " milh", " bilh", " trilh" };
        private static string[] aUnid = new string[] { "um", "dois", "tr\x00eas", "quatro", "cinco", "seis", "sete", "oito", "nove" };
        private static string[] aVirg = new string[] { " ", ", " };

        public static byte[] Compress(byte[] data)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                Stream stream2 = new GZipStream(stream, CompressionMode.Compress, true);
                stream2.Write(data, 0, data.Length);
                stream2.Close();
                stream.Position = 0L;
                byte[] buffer = new byte[stream.Length - 1L];
                stream.Read(buffer, 0, ((short) stream.Length) - 1);
                return buffer;
            }
            catch
            {
                return null;
            }
        }

        public static void CreateColumns(DataTable table, object obj)
        {
            foreach (MemberInfo info2 in obj.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (info2.MemberType == MemberTypes.Property)
                {
                    PropertyInfo info = info2 as PropertyInfo;
                    if ((((info.PropertyType == typeof(long)) || (info.PropertyType == typeof(string))) || ((info.PropertyType == typeof(double)) || (info.PropertyType == typeof(bool)))) || (info.PropertyType == typeof(DateTime)))
                    {
                        table.Columns.Add(info.Name);
                    }
                }
            }
        }

        private static DataTable CreateTable(object source)
        {
            return CreateTable(source.GetType().Name, source);
        }

        private static DataTable CreateTable(DataSet ds, object source)
        {
            DataTable table = CreateTable(source);
            ds.Tables.Add(table);
            return table;
        }

        private static DataTable CreateTable(string tablename, object source)
        {
            DataTable table = new DataTable(tablename);
            CreateColumns(table, source);
            return table;
        }

        public static string DataReaderToXML(SqlDataReader datareader, string tablename)
        {
            string str = string.Empty;
            string str3 = tablename.Trim();
            try
            {
                int fieldCount = datareader.FieldCount;
                if (string.IsNullOrEmpty(str3))
                {
                    return str;
                }
                str = str + "<" + str3 + ">";
                while (datareader.Read())
                {
                    for (int i = 0; i <= (fieldCount - 1); i++)
                    {
                        if (!datareader.IsDBNull(i))
                        {
                            string str2 = datareader[i].ToString();
                            if (!string.IsNullOrEmpty(str2.Trim()))
                            {
                                string str5 = str;
                                str = str5 + "<" + datareader.GetName(i) + ">" + str2 + "</" + datareader.GetName(i) + ">";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(str3))
                    {
                        str = str + "</" + str3 + ">";
                    }
                    str = "";
                }
            }
            catch
            {
            }
            return str;
        }

        public static string Decompress(byte[] data)
        {
            string str2;
            try
            {
                int num;
                bool flag;
                MemoryStream stream = new MemoryStream(data);
                Stream stream2 = new GZipStream(stream, CompressionMode.Decompress, true);
                string str = string.Empty;
                data = new byte[0x1000];
                goto Label_0057;
            Label_0026:
                num = stream2.Read(data, 0, data.Length);
                if (num == 0)
                {
                    goto Label_005C;
                }
                str = str + Encoding.UTF8.GetString(data, 0, num);
            Label_0057:
                flag = true;
                goto Label_0026;
            Label_005C:
                str2 = str;
            }
            catch
            {
                str2 = null;
            }
            return str2;
        }

        public static string Extenso(double Num)
        {
            long num2;
            string str = string.Empty;
            if (Math.Round((double) (Num * 100.0)) == 0.0)
            {
                return "zero reais";
            }
            str = "";
            for (int i = 4; i >= 0; i--)
            {
                num2 = Convert.ToInt64(Math.Truncate((double) ((Math.Round(Num, 0) % ((double) Convert.ToInt64(Math.Round(Math.Pow(10.0, (double) ((i + 1) * 3)), 0)))) / Math.Round(Math.Pow(10.0, (double) (i * 3)), 0))));
                if (num2 != 0L)
                {
                    str = str + MakeExtenso(num2) + aMilh[i] + aFlex[(Convert.ToInt32(((Math.Truncate(Num) % Math.Truncate(Math.Pow(10.0, (double) ((i + 1) * 3)))) / Math.Truncate(Math.Pow(10.0, (double) (i * 3)))) > 1.0) + 1) * Convert.ToInt16(i > 1)] + aVirg[Convert.ToInt32((double) (Math.Truncate(Num) % ((double) Convert.ToInt16(Math.Pow(10.0, (double) (i * 3)) > 0.0))))];
                }
            }
            str = str + aEspe[(Convert.ToInt16(Num > 1.0) + 1) * Convert.ToInt16(Math.Truncate(Num) > 0.0)];
            num2 = Convert.ToInt16((double) (Math.Round((double) (Num * 100.0)) % 100.0));
            if (num2 > 0L)
            {
                str = str + aE[Convert.ToInt16(Math.Truncate(Num) > 0.0)] + MakeExtenso(num2) + aDeci[Convert.ToInt16(num2 > 1L) + 1];
            }
            return str;
        }

        public static DataTable FillObjectToDataTable(object source)
        {
            DataTable table;
            if (source is IList)
            {
                table = CreateTable((source as IList)[0]);
                foreach (object obj2 in source as IList)
                {
                    FillTable(table, obj2);
                }
                return table;
            }
            table = CreateTable(source);
            FillTable(table, source);
            return table;
        }

        public static DataSet FillObjetToDataSet(object source)
        {
            DataSet ds = new DataSet();
            FillObjetToDataSet(ds, source);
            return ds;
        }

        public static void FillObjetToDataSet(DataSet ds, object source)
        {
            if (ds != null)
            {
                FillTable(CreateTable(ds, source), source);
            }
        }

        protected static void FillTable(DataTable table, object obj)
        {
            DataRow row = table.NewRow();
            if (obj != null)
            {
                foreach (MemberInfo info2 in obj.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                {
                    if (info2.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo info = info2 as PropertyInfo;
                        if ((((info.PropertyType == typeof(long)) || (info.PropertyType == typeof(string))) || ((info.PropertyType == typeof(double)) || (info.PropertyType == typeof(bool)))) || (info.PropertyType == typeof(DateTime)))
                        {
                            row[info.Name] = info.GetValue(obj, null).ToString();
                        }
                    }
                }
                table.Rows.Add(row);
            }
        }

        public static string FormatNumber(double valor, int digitos)
        {
            return string.Format("{0:N" + digitos.ToString() + "}", valor);
        }

        public static int GetCountMonth(DateTime d1, DateTime d2)
        {
            int num = 0;
            if (DateTime.Compare(d1, d2) == 1)
            {
                int num2 = (d1.Year - d2.Year) * 12;
                int num3 = d1.Month - d2.Month;
                num = num2 + num3;
            }
            if (DateTime.Compare(d1, d2) == 0)
            {
                num = 1;
            }
            if (DateTime.Compare(d1, d2) == -1)
            {
                num = 0;
            }
            return num;
        }

        public static byte[] GetRecords(DataSet ds)
        {
            XmlDataDocument document = new XmlDataDocument(ds);
            string innerXml = document.InnerXml;
            return Compress(Encoding.UTF8.GetBytes(innerXml));
        }

        private static string MakeExtenso(long N)
        {
            string str = string.Empty;
            if (N == 100L)
            {
                return "cem";
            }
            if (N > 100L)
            {
                str = str + aCent[(int) ((IntPtr) ((N / 100L) - 1L))] + aE[Convert.ToInt16((N % 100L) > 0L)];
            }
            N = N % 100L;
            if (N > 9L)
            {
                if ((N > 10L) && (N < 20L))
                {
                    str = str + aDez0[(int) ((IntPtr) ((N % 10L) - 1L))];
                }
                else
                {
                    str = str + aDez1[(int) ((IntPtr) ((N / 10L) - 1L))] + aE[Convert.ToInt16((N % 10L) > 0L)];
                }
            }
            N = (N % 10L) * Convert.ToInt16((N < 10L) || (N > 20L));
            if (N > 0L)
            {
                str = str + aUnid[(int) ((IntPtr) (N - 1L))];
            }
            return str;
        }

        public static string MakeSqlIn(object[] obj)
        {
            string str = "(";
            for (int i = 0; i <= (obj.Length - 1); i++)
            {
                str = str + obj[0].ToString();
                if (i < (obj.Length - 1))
                {
                    str = str + ",";
                }
            }
            return (str = str + ")");
        }
    }
}

