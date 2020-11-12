using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public  class SystemSet
    {
        public bool AutoStore { get; set; }

        public int StoreInterval { get; set; }

        public bool AutoLock { get; set; }

        public int LockInterval { get; set; }

        public List<string> TrendList { get; set; } = new List<string>();
    }
}
