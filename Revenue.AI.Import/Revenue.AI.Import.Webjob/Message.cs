using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revenue.AI.Import.Webjob
{
    public class ImportMessage
    {
        public string Product { get; }
        public long Price { get; }
        public int Quantity { get; }
    }
}
