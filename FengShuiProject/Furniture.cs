using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiProject
{
    abstract class Furniture
    {
        protected static int numOfPieces = 0;
       
        public int personalId { get; set; }
        public int tagId { get; set; }
        public int placeIndex { get; set; }
        public Image icon { get; set; }

        public Furniture(int tag, Image iconIm)
        {
            this.personalId = numOfPieces;
            this.tagId = tag;
            icon = iconIm;
            numOfPieces++;
        }

        public Furniture( Furniture other)
        {
            personalId = other.personalId;
            tagId = other.tagId;
            placeIndex = other.placeIndex;
            icon = other.icon;
        }
        public static Furniture getFurniture(int tag)
        {
            if (tag == Bed.BED_TAG)
                return new Bed();
            if (tag == Closet.CLOSET_TAG)
                return new Closet();
            if (tag == BedSideTable.BEDSIDETABLE_TAG)
                return new BedSideTable();
            if (tag == Desk.DESK_TAG)
                return new Desk();
            return null;
        }
        public static Furniture deepClone(Furniture other)
        {
            Furniture newPiece = getFurniture(other.tagId);
            newPiece.personalId = other.personalId;
            newPiece.placeIndex = other.placeIndex;
            return newPiece;
        }
        public override bool Equals(object obj)
        {
            Furniture other = (Furniture)obj;

            if (other == null)
            {
                return false;
            }
            return personalId==other.personalId;
        }
    }
}
