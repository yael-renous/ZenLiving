using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FengShuiProject
{
    class Bed:Furniture
    {
        public static readonly int BED_TAG = 1;
        private static readonly Image bedicon = Properties.Resources.bed;
        
        public Bed():base(BED_TAG, bedicon)
        { }

    }
}
