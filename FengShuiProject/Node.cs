using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiProject
{
    class Node
    {
        public Room _room { get; set; }
        private int _hueristicCost;
        private int _baseCost;
        private int _finalCost;

        public Node(Room room)
        {
            this._room = room;
            _baseCost = 1;
            _hueristicCost = 0;
            _finalCost = 0;
        }

        public Node (Node other)
        {
            _room = new Room(other._room);
            _baseCost = 1;
            _hueristicCost = 0;
            _finalCost = 0;
        }

        public override bool Equals(object obj)
        {
            Node other = (Node) obj;

            if (other == null)
            {
                return false;
            }
            return _room._furniture.Equals(other._room._furniture);                
        }


        private Bed getBed()
        {
            ArrayList furnitureArrangment = _room._furniture;
            for (int i = 0; i < furnitureArrangment.Count; i++)
            {
                if (furnitureArrangment[i].GetType() == typeof(Bed))
                {
                    return (Bed)furnitureArrangment[i];
                }
            }
            return null;
        }

        public int numOfFurniture()
        {
            return _room._furniture.Count;
        }

        public int getFinal()
        {
            return _finalCost;
        }

        public void changePosition(int furnitureIndex, int spotIndex)
        {
            ArrayList furnitureArrangment = _room._furniture;
            int switchedFurnitureIndex = -1;
            for (int i = 0; i < furnitureArrangment.Count; i++)
            {
                if (((Furniture)furnitureArrangment[i]).placeIndex == spotIndex)
                    switchedFurnitureIndex = i;
            }

            //if there is furniture in the spot
            if (switchedFurnitureIndex != -1)
            {
                _baseCost=2;
                int oldSpot = ((Furniture)furnitureArrangment[furnitureIndex]).placeIndex;
                ((Furniture)furnitureArrangment[switchedFurnitureIndex]).placeIndex = oldSpot;
            }

            //set the new placement 
            ((Furniture)furnitureArrangment[furnitureIndex]).placeIndex = spotIndex;
            updateCosts();
        }

        internal float getBase()
        {
            return _baseCost;
        }

        public void updateCosts()
        {
            _hueristicCost = calculateHueristic();
            _finalCost = _baseCost + _hueristicCost;
        }

        private int calculateHueristic()
        {
            int bedPlace = getBed().placeIndex;
            int bedHueristics = calculateBedHeuristics(bedPlace);
            int tableHueristics = calculateDeskHeuristics(bedPlace);
            int bedSideHueristics = calculateBedSideTableHeuristics(bedPlace);
            int closetHueristics = calculateClosetHueristics(bedPlace);

            return bedHueristics + tableHueristics + bedSideHueristics + closetHueristics;
        }



        //============================== HEURISTICS CALC ==============================
        private int calculateBedHeuristics(int bedPlace)
        {
            int counter = 0;
            if (bedPlace == 8)
                counter += 4;
            if (bedUnderWindow(bedPlace))
                counter += 3;
            if (bedNotMiddle(bedPlace))
                counter += 6;
            if (bedDiagnoalDoor(bedPlace))
                counter += 2;
            return counter;
        }

        private int calculateDeskHeuristics(int bedPlace)
        {
            ArrayList furnitureArrangment = _room._furniture;
            int counter = 0;
            for (int i = 0; i < furnitureArrangment.Count; i++)
            {
                if (furnitureArrangment[i].GetType() == typeof(Desk))
                {
                    Desk desk = (Desk)furnitureArrangment[i];
                    if (desk.placeIndex == 8)
                        counter += 4;
                    if (deskByBed(desk,bedPlace))
                        counter += 2;
                    if (deskInFrontDoor(desk))
                        counter += 2;
                    if (deskUnderWindow(desk))
                        counter += 2;
                }
            }
            return counter;
        }

        private int calculateClosetHueristics(int bedPlace)
        {
            ArrayList furnitureArrangment = _room._furniture;
            int counter = 0;
            for (int i = 0; i < furnitureArrangment.Count; i++)
            {
                if (furnitureArrangment[i].GetType() == typeof(Closet))
                {
                    Closet closet = (Closet)furnitureArrangment[i];
                    if (closet.placeIndex == 8)
                        counter += 6;
                    if (closetDiagnoalFromBed(closet,bedPlace))
                        counter += 2;
                    if (furnitureUnderWindow(closet.placeIndex))
                        counter += 2;
                }
            }
            return counter;
        }

        private int calculateBedSideTableHeuristics(int bedPlace)
        {
            int counter = 0;
            ArrayList furnitureArrangment = _room._furniture;
            for (int i = 0; i < furnitureArrangment.Count; i++)
            {
                if (furnitureArrangment[i].GetType() == typeof(BedSideTable))
                {
                    BedSideTable table = (BedSideTable)furnitureArrangment[i];
                    if (table.placeIndex == 8)
                        counter += 6;
                    if (bedsideNotByBed(table, bedPlace))
                        counter += 3;
                    else
                        counter -= 2;
                }
            }
            return counter;
        }

        /// <summary>
        /// checks if the bedside table is by the bed
        /// </summary>
        private bool bedsideNotByBed(BedSideTable table, int bedPlace)
        {
            int tablePlace = table.placeIndex;
            return !furnitureByBed(tablePlace, bedPlace);
        }

        private bool furnitureByBed(int furniturePlace, int bedPlace)
        {
            bool output = false;
            switch (bedPlace)
            {
                case 0:
                    output = furniturePlace == 1 | furniturePlace == 7;
                    break;
                case 2:
                case 4:
                case 6:
                    bool option1 = furniturePlace == bedPlace + 1;
                    bool option2 = furniturePlace == bedPlace - 1;
                    output = option1 | option2;
                    break;
                case 1:
                case 7:
                case 3:
                case 5:
                    option1 = furniturePlace == (bedPlace + 1) % 8;
                    option2 = furniturePlace == bedPlace - 1;
                    output = option1 | option2 | furniturePlace == 8;
                    break;
            }
            return output;
        }

        /// <summary>
        /// checks that the closet isn't digonal from the bed
        /// </summary>
        private bool closetDiagnoalFromBed(Closet closet, int bedPlace)
        {
            return firstDiagonalSecond(closet.placeIndex, bedPlace);
        }

        private bool firstDiagonalSecond(int firstPlace, int secondPlace)
        {
            switch (secondPlace)
            {
                case 0:
                    return firstPlace == 4 | firstPlace == 8;
                case 1:
                case 5:
                    return firstPlace == 3 | firstPlace == 7;
                case 2:
                    return firstPlace == 8 | firstPlace == 6;
                case 3:
                case 7:
                    return firstPlace == 1 | firstPlace == 5;
                case 8:
                    return firstPlace == 0 | firstPlace == 2 | firstPlace == 6 | firstPlace == 4;
                case 6:
                    return firstPlace == 8 | firstPlace == 2;
                case 4:
                    return firstPlace == 8 | firstPlace == 0;
            }
            return false;
        }

        private bool deskUnderWindow(Desk desk)
        {
            return furnitureUnderWindow(desk.placeIndex);
        }

        private bool deskInFrontDoor(Desk desk)
        {
            int doorPlace = _room.doorPlacement;
            int deskPlace = desk.placeIndex;
            bool option1, option2, option3, option4;
            bool option5 = false;

            if (deskPlace == 8)
                return doorPlace == 1 | doorPlace == 5 | doorPlace == 3 | doorPlace == 7;

            option1 = doorPlace == deskPlace + 1;
            option2 = doorPlace == deskPlace + 2;
            option3 = doorPlace == deskPlace - 1;
            option4 = doorPlace == deskPlace - 2;
            if (deskPlace == 0) {
                option3 = doorPlace == 7;
                option4 = doorPlace == 6;
            }
            if (deskPlace == 6)
                option4 = doorPlace == 0;
            if (isMiddlePlace(deskPlace))
                option5 = doorPlace == 8;

            return option1 | option2 | option3 | option4 | option5;
        }

        private bool isMiddlePlace(int place)
        {
            return place == 1 | place == 5 | place == 7 | place == 3;
        }

        private bool deskByBed(Desk desk, int bedPlace)
        {
            return furnitureByBed(desk.placeIndex, bedPlace);
        }

        private bool bedDiagnoalDoor(int bedPlace)
        {
            return firstDiagonalSecond(bedPlace, _room.doorPlacement);
        }

        private bool bedNotMiddle(int bedPlace)
        {
            return !isMiddlePlace(bedPlace);
        }

        private bool bedUnderWindow(int bedPlace)
        {
            return furnitureUnderWindow(bedPlace);
        }

        private bool furnitureUnderWindow(int place)
        {
            for(int i = 0; i < _room._windows.Count; i++)
            {
                if ((int)_room._windows[i] == place)
                    return true;
            }
            return false;
        }

     
    }
}

