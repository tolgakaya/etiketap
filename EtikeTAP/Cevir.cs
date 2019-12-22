using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtikeTAP
{
    public static class Cevir
    {
        public static DataTable ToDataTable(this IEnumerable<dynamic> items)
        {
            if (!items.Any()) return null;

            var table = new DataTable();
            bool isFirst = true;

            items.Cast<IDictionary<string, object>>().ToList().ForEach(x =>
            {
                if (isFirst)
                {
                    x.Keys.Select(y => table.Columns.Add(y)).ToList();
                    isFirst = false;
                }
                table.Rows.Add(x.Values.ToArray());
            });

            return table;
        }
    }
}
