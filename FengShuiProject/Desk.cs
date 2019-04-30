using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiProject
{
    class Desk:Furniture
    {
        public static readonly int DESK_TAG =2;
        private static readonly Image deskicon = Properties.Resources.desk;

        public Desk() : base(DESK_TAG, deskicon) { }
    }
}
