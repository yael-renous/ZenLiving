using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FengShuiProject
{
   
    public partial class Form1 : Form
	{
        /**Pair first is the picture box and second is dropbox tag "under" it*/
        private Pair<PictureBox,int>[] _windows;
        private Button[] _windowButtons;

        private PictureBox[] _doors;
        private Button[] _doorButtons;

        /**Pair first is the picture box and second is furniture tag in the box*/
        public Pair<PictureBox,int>[] _dropboxes;
        private Pair<PictureBox,Label>[] _furnitureIcons;

        private Room _room;
  

        private PictureBox selectedDropbox;
        private Furniture selectedFurniture;
        

        public Form1()
		{
			InitializeComponent();
            this.AllowDrop = true;
            _windows = new Pair<PictureBox, int>[10];
            _windowButtons = new Button[10];
            _doors = new PictureBox[8];
            _doorButtons = new Button[8];

            /** list of all available dropboxees for funiture, Item1 = the picturebox, Item2= id of furniture placed ,-1 if empty*/
            _dropboxes = new Pair<PictureBox, int>[9];

            _furnitureIcons = new Pair<PictureBox,Label>[4];

            selectedDropbox = null;
            selectedFurniture = null;

            _room = new Room();
            
            init();
		}
        

        //=============================== INITIALIZATIONS ===========================================
        private void init()
        {
            initWindowImages();
            initWindowButtons();
            //initWalls();
            initDoorImages();
            initDoorButtons();
            initDropBoxes();
            initFurnitureIcons();
            
        }

        private void initFurnitureIcons()
        {
            for (int i = 0; i < _furnitureIcons.Length; i++)
                _furnitureIcons[i] = new Pair<PictureBox, Label>();
            _furnitureIcons[0].First = bedIcon;
            _furnitureIcons[0].Second = bedLabel;
            _furnitureIcons[1].First = deskIcon;
            _furnitureIcons[1].Second = deskLabel;
            _furnitureIcons[2].First = bedsidetableIcon;
            _furnitureIcons[2].Second = bedsideLabel;
            _furnitureIcons[3].First = closetIcon;
            _furnitureIcons[3].Second = cloetLabel;

            for (int i = 0; i < _furnitureIcons.Length; i++)
            {
                _furnitureIcons[i].First.Visible = false;
                _furnitureIcons[i].Second.Visible = false;
            }
        }

        private void initDropBoxes()
        {
            for (int i = 0; i < _dropboxes.Length; i++)
            {
                _dropboxes[i] = new Pair<PictureBox, int>();
            }
            _dropboxes[0].First = dropboxTopLeft;
            _dropboxes[1].First = dropboxTopMiddle;
            _dropboxes[2].First = dropboxTopRight;
            _dropboxes[3].First = dropboxMiddleRight;
            _dropboxes[4].First = dropboxBottomRight;
            _dropboxes[5].First = dropboxBottomMiddle;
            _dropboxes[6].First = dropboxBottomLeft;
            _dropboxes[7].First = dropboxMiddleLeft;
            _dropboxes[8].First = dropboxMiddleMiddle;

            for(int i = 0; i < _dropboxes.Length; i++)
            {
                _dropboxes[i].First.Visible = false;
                _dropboxes[i].First.AllowDrop = true;
            }
        }

        private void initDoorButtons()
        {
            _doorButtons[0] = buttonDoorTopLeft;
            _doorButtons[1] = buttonDoorTopMiddle;
            _doorButtons[2] = buttonDoorTopRight;
            _doorButtons[3] = buttonDoorRightMiddle;
            _doorButtons[4] = buttonDoorBottomRight;
            _doorButtons[5] = buttonDoorBottomMiddle;
            _doorButtons[6] = buttonDoorBottomLeft;
            _doorButtons[7] = buttonDoorLeftMiddle;
            for (int i = 0; i < _doorButtons.Length; i++)
                _doorButtons[i].Visible = false;
        }

        private void initDoorImages()
        {
            _doors[0] = doorTopLeft;
            _doors[1] = doorTopMiddle;
            _doors[2] = doorTopRight;
            _doors[3] = doorRightMiddle;
            _doors[4] = doorBottomRight;
            _doors[5] = doorBottomMiddle;
            _doors[6] = doorBottomLeft;
            _doors[7] = doorLeftMiddle;
            for (int i = 0; i < _doors.Length; i++)
                _doors[i].Visible = false;
        }

        private void initWindowButtons()
        {
            _windowButtons[0] = buttonWindowTopLeft;
            _windowButtons[1] = buttonWindowTopMiddle;
            _windowButtons[2] = buttonWindowTopRight;
            _windowButtons[3] = buttonWindowRightTop;
            _windowButtons[4] = buttonWindowRightBottom;
            _windowButtons[5] = buttonWindowBottomRight;
            _windowButtons[6] = buttonWindowBottomMiddle;
            _windowButtons[7] = buttonWindowBottomLeft;
            _windowButtons[8] = buttonWindowLeftBottom;
            _windowButtons[9] = buttonWindowLeftTop;
            for (int i = 0; i < _windowButtons.Length; i++)
                _windowButtons[i].Visible = false;
        }

        private void initWindowImages()
        {
            for (int i = 0; i < _windows.Length; i++)
            {
                _windows[i] = new Pair<PictureBox, int>();
            }
            _windows[0].First = windowTopLeft;
            _windows[0].Second = 0;
            _windows[1].First = windowTopMiddle;
            _windows[1].Second = 1;
            _windows[2].First = windowTopRight;
            _windows[2].Second = 2;
            _windows[3].First = windowRightTop; // right wall left window
            _windows[3].Second = 2;
            _windows[4].First = windowRightBottom; // right wall right window
            _windows[4].Second = 4;
            _windows[5].First = windowBottomRight;
            _windows[5].Second = 4;
            _windows[6].First = windowBottomMiddle;
            _windows[6].Second = 5;
            _windows[7].First = windowBottomLeft;
            _windows[7].Second = 6;
            _windows[8].First = windowLeftBottom;//left wall right window
            _windows[8].Second = 6;
            _windows[9].First = windowLeftTop; //left wall left window
            _windows[9].Second = 0;
            for (int i = 0; i < _windows.Length; i++)
                _windows[i].First.Visible = false;
        }



        //=========================== HELPER FUNCTIONS =====================================

        /*private Placement getWindowPlacement(int i)
        {
            switch (i)
            {
                //case 0 | 3 | 7 | 9
                case 0:
                case 3:
                case 7:
                case 9:
                    return Placement.left;
                //case 2 | 4 | 5 | 8
                case 2:
                case 4:
                case 5:
                case 8:
                    return Placement.right;
                //case 1 | 6
                case 1:
                case 6:
                    return Placement.middle;
            }
            return Placement.none;
        }
        
        private int getWindowWallIndex(int i)
        {
            switch (i)
            {
                case 0:
                case 1:
                case 2:
                    return Room.TOP_WALL_INDEX;
                case 3:
                case 4:
                    return Room.RIGHT_WALL_INDEX;
                case 5:
                case 6:
                case 7:
                    return Room.BOTTOM_WALL_INDEX;
                case 8:
                case 9:
                    return Room.LEFT_WALL_INDEX;
            }
            return -1;
        }


        /*private Placement getDoorPlacement(int i)
        {
            switch (i)
            {
                //case 0 | 3 | 7 | 9
                case 0:
                case 6:
                    return Placement.left;
                //case 2 | 4 | 5 | 8
                case 2:
                case 4:
                    return Placement.right;
                //case 1 | 6
                case 1:
                case 3:
                case 5:
                case 7:
                    return Placement.middle;
            }
            return Placement.none;
        }

        private int getDoorWallIndex(int i)
        {
            switch (i)
            {
                case 0:
                case 1:
                case 2:
                    return Room.TOP_WALL_INDEX;
                case 3:
                    return Room.RIGHT_WALL_INDEX;
                case 4:
                case 5:
                case 6:
                    return Room.BOTTOM_WALL_INDEX;
                case 7:
                    return Room.LEFT_WALL_INDEX;
            }
            return -1;
        }
        */

        private int getDoorPictureIndex()
        {
            for (int i = 0; i < _doors.Length; i++)
                if (_doors[i].Visible == true)
                    return i;
            return -1;
        }

        private void resetHelperButtons()
        {
            deleteButton.Visible = false;
            rotateButton.Visible = false;
            selectedDropbox = null;
            hideDropboxes();
        }

        private void hideDropboxes()
        {
            for (int i = 0; i < _dropboxes.Length; i++)
                if (_dropboxes[i].First.Image == null)
                    _dropboxes[i].First.Visible = false;
        }


        //==========================DOOR BUTTON CLICKS====================================

        private void buttonDoorClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int i;
            Int32.TryParse((string)button.Tag, out i);
            _doors[i].Visible = true;
            _room.doorPlacement=i;

           /* //set door
            int wallIndex = getDoorWallIndex(i);
            Placement place = getDoorPlacement(i);
            _room._walls[wallIndex].SetDoor(place);
            */

            for (int j = 0; j < _doorButtons.Length; j++)
            {
                _doorButtons[j].Visible = false;
            }
            
        }

        private void picboxDoorClick(object sender, EventArgs e)
        {

            PictureBox pic = (PictureBox)sender;
            int i;
            Int32.TryParse((string)pic.Tag, out i);
            _doors[i].Visible = false;
            _room.doorPlacement = 0;

            /*/remove window from wall
            int wallIndex = getDoorWallIndex(i);
            Placement place = getDoorPlacement(i);
            _room._walls[wallIndex].RemoveDoor();*/

            //set door
            for (int j = 0; j < _doorButtons.Length; j++)
            {
                   _doorButtons[j].Visible = true;
            }

        }

        private void DoorSelectedButton_Click(object sender, EventArgs e)
        {
            //check that a door has been selected
            bool doorExsists = false;
            for (int i = 0; i < _doors.Length; i++)
                if (_doors[i].Visible == true)
                    doorExsists = true;
            if (!doorExsists)
            {
                MessageBox.Show("must select door");
                return;
            }

            doorText.Visible = false;
            doorDoneButton.Visible = false;
            windowsDoneButton.Visible = true;
            windowsText.Visible = true;
            for (int i = 0; i < _windowButtons.Length; i++)
                _windowButtons[i].Visible = true;

        }
        //==========================WINDOW BUTTON CLICKS====================================
        private void buttonWindowClick(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            int i;
            Int32.TryParse((string)button.Tag, out i);
            _windows[i].First.Visible = true;
            _windowButtons[i].Visible = false;

            _room.addWindow(_windows[i].Second);

            /*/add window to wall
            int wallIndex = getWindowWallIndex(i);
            Placement place = getWindowPlacement(i);
            _room._walls[wallIndex].AddWindow(place);*/
        }

        private void picboxWindowClick(object sender, EventArgs e)
        {

            PictureBox pic = (PictureBox)sender;
            int i;
            Int32.TryParse((string)pic.Tag, out i);
            _windows[i].First.Visible = false;
            _windowButtons[i].Visible = true;

            _room.removeWindow(_windows[i].Second);

            /*remove window from wall
            int wallIndex = getWindowWallIndex(i);
            Placement place = getWindowPlacement(i);
            _room._walls[wallIndex].RemoveWindow(place);*/
        }



        private void WindowsDoneButton_Click(object sender, EventArgs e)
        {
            windowsText.Visible = false;
            drapdropText.Visible = true;
            fenShuiButton.Visible = true;

            windowsDoneButton.Visible = false;
            for (int i = 0; i < _windowButtons.Length; i++)
                _windowButtons[i].Visible = false;

            furnitureBG.Visible = true;
            for (int i = 0; i < _furnitureIcons.Length; i++)
            {
                _furnitureIcons[i].First.Visible = true;
                _furnitureIcons[i].Second.Visible = true;
            }
            furnitureBG.SendToBack();
            logo.SendToBack();

        }

        //================================= DRAG AND DROP ==================================

        private void dropbox_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("drag drop");
            
            //set image
            PictureBox dropbox = (PictureBox)sender;
            dropbox.Image = (Image)e.Data.GetData(DataFormats.Bitmap);

            int index;
            Int32.TryParse((string)dropbox.Tag, out index);
            _dropboxes[index].Second = selectedFurniture.tagId;

            //if bed - remove from options
            if (selectedFurniture.tagId == Bed.BED_TAG)
            {
                _furnitureIcons[0].First.Visible = false;
                _furnitureIcons[0].Second.Visible = false;
            }

            //add placement to furniture
            selectedFurniture.placeIndex=index;

            //add furniture to room
            _room.addFurniture(selectedFurniture);

            hideDropboxes();
        }

        private void dropbox_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("drag enter");
            PictureBox dropbox = (PictureBox)sender;
            dropbox.Visible = true;
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void furniture_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mouse down");
            resetHelperButtons();

            //get furniture type
            PictureBox furnitureIcon = (PictureBox)sender;
            int furnitureType;
            Int32.TryParse((string)furnitureIcon.Tag, out furnitureType);
            selectedFurniture = Furniture.getFurniture(furnitureType);

            //show available spots
            int j = getDoorPictureIndex();
            for (int i = 0; i < _dropboxes.Length; i++)
                if (i != j)
                    _dropboxes[i].First.Visible = true;
            //send furniture

            furnitureIcon.DoDragDrop(furnitureIcon.Image, DragDropEffects.Copy);
        }

        private void dropbox_Click(object sender, EventArgs e)
        {
            Point location = ((PictureBox)sender).Location;

            deleteButton.Location = location;
            Point rotateLocation = new Point(location.X + 75, location.Y);
            rotateButton.Location = rotateLocation;

            deleteButton.BringToFront();
            rotateButton.BringToFront();

            deleteButton.Visible = true;
            rotateButton.Visible = true;
            selectedDropbox = (PictureBox)sender;


        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            selectedDropbox.Image = null;
            int i;
            string tag = selectedDropbox.Tag.ToString();
            int furnitureTag;
            Int32.TryParse(tag, out i);
            furnitureTag=_dropboxes[i].Second;

            _room.deleteFurniture(furnitureTag, i);

            if (!_room.hasBed())
            {
                _furnitureIcons[0].First.Visible = true;
                _furnitureIcons[0].Second.Visible = true;
            }
            resetHelperButtons();
        }
        private void RotateButton_Click(object sender, EventArgs e)
        {
            Image img = selectedDropbox.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            selectedDropbox.Image = img;
            resetHelperButtons();
        }

        private void FenShuiButton_Click(object sender, EventArgs e)
        {
            if (!_room.hasBed())
            {
                MessageBox.Show("must insert bed");
                return;
            }

            Astar algorithm = new Astar();
            Room output = algorithm.a_star(_room);

            endlabel2.Visible = true;

            closeForum();
            printRoom(output);
            
        }

        private void closeForum()
        {
            //remove feng shui button + header
            fenShuiButton.Visible = false;
            drapdropText.Visible = false;

            //remove all furniture icons
            for (int i = 0; i < _furnitureIcons.Length; i++)
            {
                _furnitureIcons[i].First.Visible = false;
                _furnitureIcons[i].Second.Visible = false;
            }

            furnitureBG.Visible = false;
            //add text
        }

        private void printRoom(Room output)
        {

            for (int i = 0; i < _dropboxes.Length; i++)
            {
                _dropboxes[i].First.Image = null;
                _dropboxes[i].First.Visible = false;
            }
            ArrayList furniture = output._furniture;
            for(int i = 0; i < furniture.Count; i++)
            {
                Furniture temp = ((Furniture)furniture[i]);
                int placeIndex = temp.placeIndex;
                Image picture = new Bitmap(temp.icon);

                //roatate image
                rotateImage(picture, temp.tagId, placeIndex);

                PictureBox dropbox = _dropboxes[placeIndex].First;
                dropbox.Image = picture;
                dropbox.Visible = true;
            }
        }

        private void rotateImage(Image picture, int tagId, int placeIndex)
        {
            if (tagId == Bed.BED_TAG || tagId == Closet.CLOSET_TAG)
                rotateForwardFacing(picture, placeIndex);

            if (tagId == Desk.DESK_TAG)
                rotateDesk(picture, placeIndex);
        }

        private void rotateDesk(Image picture, int placeIndex)
        {
            switch (placeIndex)
            {
                case 0:
                case 7:
                case 6:
                    picture.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 1:
                    picture.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 2:
                case 3:
                case 4:
                    picture.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
        }


        private void rotateForwardFacing(Image picture, int placeIndex)
        {
            switch (placeIndex)
            {
                case 0:
                case 7:
                case 6:
                    picture.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case 5:
                    picture.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 2:
                case 3:
                case 4:
                    picture.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;

            }
        }

        //====================== FORM EVENTS =====================================

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.lotus;

        }

        // Drops item on background
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            hideDropboxes();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            e.Effect = DragDropEffects.Copy;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            instructions.Visible = false;
            instructions2.Visible = false;
            startButton.Visible = false;

            for (int i = 0; i < _doorButtons.Length; i++)
                _doorButtons[i].Visible = true;
            blueprintBG.Visible = true;
            flowLayoutPanel1.Visible = true;
           
            blueprintBG.SendToBack();
            flowLayoutPanel1.SendToBack();
             logo.SendToBack();
            doorText.Visible = true;
            doorDoneButton.Visible = true;
           
        }
    }
}
