using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH2HM
{
    class Program
    {
        static void Main(string[] args)
        {
            string yyyymm = "2017-07";
            for (int day = 1; day <= 31; day++)
            {

                var ds = MatterLib.Utils.GetLoadedDataSet("SELECT TOP 1000000 [lines] FROM [KostiaHH].[dbo].[HH] where date='" + yyyymm + "-" + day.ToString("00")+"' and tableMax=6 and bad is null");

                var sb = new StringBuilder();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    sb.Append(row[0].ToString());
                    sb.AppendLine("");
                    sb.AppendLine("");
                }

                File.WriteAllText("c:\\@\\hands\\"+yyyymm+"-" + day.ToString("00") + ".txt", sb.ToString());
                Console.WriteLine("c:\\@\\hands\\" + yyyymm + "-" + day.ToString("00") + ".txt", sb.ToString());
            }

        }
    }
}
