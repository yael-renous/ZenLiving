using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiProject
{
    class Closet:Furniture
    {
        public static readonly int CLOSET_TAG = 4;
        private static readonly Image closeticon = Properties.Resources.closet;

        public Closet() : base(CLOSET_TAG, closeticon) { }
    }
}
