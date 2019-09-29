using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public GameObject wall;
    public int size;
    public Sprite[] trees;

    public Vector3 startLocation;
    public Vector3 endLocation;

    private int pathLength;

    public struct Node
    {
        public bool left;
        public bool right;
        public bool up;
        public bool down;
        public int distance;

        public Node(bool start, int dist)
        {
            left = start;
            right = start;
            up = start;
            down = start;

            distance = dist;
        }

        public override string ToString()
        {
            return "Dist: " + distance + ". Walls to the: left? " + left + " right? " + right + " up? " + up + " down? " + down;
        }

        public static Node operator + (Node n1, Node n2)
        {
            Node node = new Node(false, n1.distance);

            node.left = n1.left || n2.left;
            node.right = n1.right || n2.right;
            node.up = n1.up || n2.up;
            node.down = n1.down || n2.down;

            return node;
        }
    }

    private struct twoInts
    {
        int a;
        int b;

        public twoInts (int x, int y)
        {
            a = x;
            b = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is twoInts)) return false;

            twoInts ints = (twoInts)obj;

            return this.a == ints.a && this.b == ints.b;
        }

        public override int GetHashCode()
        {
            return elegantSigned(a, b);
        }

        //https://stackoverflow.com/questions/11742593/what-is-the-hashcode-for-a-custom-class-having-just-two-int-properties
        // x,y must be non-negative
        private int elegant(int x, int y)
        {
            return x < y ? y * y + x : x * x + x + y;
        }

        // returns a unique number for every x,y pair
        private int elegantSigned(int x, int y)
        {
            if (x < 0)
            {
                if (y < 0)
                    return 3 + 4 * elegant(-x - 1, -y - 1);
                return 2 + 4 * elegant(-x - 1, y);
            }
            if (y < 0)
                return 1 + 4 * elegant(x, -y - 1);
            return 4 * elegant(x, y);
        }
    }

    private Dictionary<twoInts, Node> maze;

    // Start is called before the first frame update
    void Start()
    {
        maze = new Dictionary<twoInts, Node>();

        startLocation = new Vector3(Random.Range(0, size + 1), Random.Range(0, size + 1), 0);

        Generator();

        GameObject Zul = GameObject.Find("Zul");
        Zul.transform.position = startLocation * 2;
        GameObject Ros = GameObject.Find("Ros");
        Ros.transform.position = endLocation * 2;
    }

    private void Generator()
    {
        Node startNode = new Node(false, 0);

        pathLength = 0;

        int distance = 0;

        endLocation = startLocation;

        CheckNode(startLocation, distance, startNode, out startNode);

        //foreach (Node finishedNode in maze.Values)
        //{
        //    Debug.Log(finishedNode);
        //}

        //Node node;

        //for (int a = 0; a < size + 1; a++)
        //{
        //    for (int b = 0; b < size + 1; b++)
        //    {
        //        if (maze.TryGetValue(new twoInts(a, b), out node))
        //        {
        //            if (!node.left)
        //            {
        //                GameObject obj = Instantiate(wall, new Vector3(a - 0.5f, b, 0), Quaternion.identity, transform);
        //            }
        //            if (!node.right)
        //            {
        //                GameObject obj = Instantiate(wall, new Vector3(a + 0.5f, b, 0), Quaternion.identity, transform);
        //            }
        //            if (!node.up)
        //            {
        //                GameObject obj = Instantiate(wall, new Vector3(a, b + 0.5f, 0), Quaternion.Euler(0, 0, 90), transform);
        //            }
        //            if (!node.down)
        //            {
        //                GameObject obj = Instantiate(wall, new Vector3(a, b - 0.5f, 0), Quaternion.Euler(0, 0, 90), transform);
        //            }
        //        }

        //    }
        //}
    }

    private bool CheckNode(Vector3 location, int distance, Node node, out Node nodeOut)
    {
        if (location.x < 0 || location.x > size || location.y < 0 || location.y > size)
        {
            nodeOut = new Node(false, -1);
            return false;
        }
        else if (maze.TryGetValue(new twoInts((int)location.x, (int)location.y), out nodeOut))
        {
            int rand = Random.Range(0, 8);
            if (rand == 0)
            {
                nodeOut = nodeOut + node;

                maze.Remove(new twoInts((int)location.x, (int)location.y));
                maze.Add(new twoInts((int)location.x, (int)location.y), nodeOut);
                return true;
            }
            return false;
        }

        //Debug.Log(node);

        maze.Add(new twoInts((int)location.x, (int)location.y), node);

        if (distance > pathLength)
        {
            pathLength = distance;
            endLocation = location;
        }

        int start = Random.Range(0, 4);
        bool direction = Random.Range(0, 2) == 1 ? true : false;

        for (int i = 0; i < 4; i ++)
        {
            Node newNode = new Node(false, node.distance + 1);

            //Debug.Log(newNode);

            switch (start)
            {
                case 0:
                    newNode.left = true;
                    node.right = node.right || CheckNode(location + Vector3.right, distance + 1, newNode, out newNode);
                    if(!node.right && (newNode.distance == -1 || node.distance > newNode.distance))
                    {
                        GameObject obj = Instantiate(wall, new Vector3(((int)location.x + 0.5f) * 2, (int)location.y * 2, 0), Quaternion.identity, transform);
                        obj.GetComponent<SpriteRenderer>().sprite = trees[Random.Range(0, trees.Length)];
                    }
                    break;
                case 1:
                    newNode.right = true;
                    node.left = node.left || CheckNode(location + Vector3.left, distance + 1, newNode, out newNode);
                    if (!node.left && (newNode.distance == -1 || node.distance > newNode.distance))
                    {
                        GameObject obj = Instantiate(wall, new Vector3(((int)location.x - 0.5f) * 2, (int)location.y * 2, 0), Quaternion.identity, transform);
                        obj.GetComponent<SpriteRenderer>().sprite = trees[Random.Range(0, trees.Length)];
                    }
                    break;
                case 2:
                    newNode.down = true;
                    node.up = node.up || CheckNode(location + Vector3.up, distance + 1, newNode, out newNode);
                    if (!node.up && (newNode.distance == -1 || node.distance > newNode.distance))
                    {
                        GameObject obj = Instantiate(wall, new Vector3((int)location.x * 2, ((int)location.y + 0.5f) * 2, 0), Quaternion.Euler(0, 0, 90), transform);
                        obj.GetComponent<SpriteRenderer>().sprite = trees[Random.Range(0, trees.Length)];
                    }
                    break;
                case 3:
                    newNode.up = true;
                    node.down = node.down || CheckNode(location + Vector3.down, distance + 1, newNode, out newNode);
                    if (!node.down && (newNode.distance == -1 || node.distance > newNode.distance))
                    {
                        GameObject obj = Instantiate(wall, new Vector3((int)location.x * 2, ((int)location.y - 0.5f) * 2, 0), Quaternion.Euler(0, 0, 90), transform);
                        obj.GetComponent<SpriteRenderer>().sprite = trees[Random.Range(0, trees.Length)];
                    }
                    break;
            }

            if (direction)
            {
                start = (start + 1) % 4; 
            }
            else
            {
                if (start <= 0)
                {
                    start = 3;
                }
                else
                {
                    start = (start - 1) % 4;
                }
            }
        }

        maze.Remove(new twoInts((int)location.x, (int)location.y));
        maze.Add(new twoInts((int)location.x, (int)location.y), node);

        nodeOut = node;

        return true;
    }
}
