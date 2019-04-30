using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiProject
{
    class Room
    {
        public ArrayList _windows { get; set; }
        public ArrayList _furniture { get; set; }
        public int doorPlacement { get; set; }

        public Room()
        {
            _furniture = new ArrayList();
            _windows = new ArrayList();
        }

        public Room(Room other)
        {
            _windows = new ArrayList(other._windows);
            _furniture = new ArrayList();
            for (int i = 0; i < other._furniture.Count; i++)
            {
                _furniture.Add(Furniture.deepClone((Furniture)other._furniture[i]));

            }
            doorPlacement = other.doorPlacement;
        }
        internal void addFurniture(Furniture draggedFurniture)
        {
            _furniture.Add(draggedFurniture);
        }

        internal bool hasBed()
        {
            for (int i = 0; i < _furniture.Count; i++)
                if (((Furniture)_furniture[i]).tagId == Bed.BED_TAG)
                    return true;
            return false;
        }

        internal void addWindow(int overHeadIndex)
        {
            _windows.Add(overHeadIndex);
        }

        internal void removeWindow(int overHeadIndex)
        {
            _windows.Remove(overHeadIndex);
        }

        internal void deleteFurniture(int tag, int placment)
        {
            for (int i = 0; i < _furniture.Count; i++)
            {
                Furniture piece = ((Furniture)_furniture[i]);
                if (piece.tagId == tag && piece.placeIndex==placment)
                    _furniture.Remove(_furniture[i]);
            }
        }
    }
}
