using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace FengShuiProject
{ 
    class Astar
    {
        private readonly int NUM_OF_SPOTS = 9;
        private readonly int GRADE_THRESHOLD = 3;
        private SimplePriorityQueue<Node> priorityQueue = new SimplePriorityQueue<Node>();


      public Room a_star(Room  room)
        {
            Node current = new Node(room);
            current.updateCosts();
            Node next = null;

            //main loop
            while (current.getFinal() > GRADE_THRESHOLD)
            {
                //create all nodes for search in a priority queue.
                createNeighbourNodes(current,room.doorPlacement);

                //retrieve "closest node"
                next = priorityQueue.Dequeue();
                current = next;

            }
            return current._room;
        }

        /// <summary>
        /// adds to the priority queue all of the neighbouring nodes
        /// </summary>
        /// <param name="currentNode">the node to retrieve it's neighbours</param>
        public void createNeighbourNodes(Node currentNode, int doorPlacement)
        {
            for (int furnitureIndex = 0; furnitureIndex < currentNode.numOfFurniture(); furnitureIndex++)
            {
                for (int spotIndex = 0; spotIndex < NUM_OF_SPOTS; spotIndex++)
                {
                    if (spotIndex == doorPlacement)
                        continue;
                    Node newNode = new Node(currentNode);
                    newNode.changePosition(furnitureIndex, spotIndex);

                    //makes sure we don't already have that node
                    bool nodeExists = priorityQueue.Contains(newNode);
                    if (!nodeExists)
                        priorityQueue.Enqueue(newNode, newNode.getFinal());


                }
            }
        }

    }
}
