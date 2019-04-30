using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiProject
{
    class BedSideTable:Furniture
    {
        public static readonly int BEDSIDETABLE_TAG = 3;
        private static readonly Image bedsideicon = Properties.Resources.sideTable;
        public BedSideTable() : base(BEDSIDETABLE_TAG, bedsideicon) { }
    }
}
