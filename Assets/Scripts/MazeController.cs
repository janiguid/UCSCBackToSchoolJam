using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public Sprite wall;
    public int size;

    public Vector3 startLocation;
    public Vector3 endLocation;

    private int pathLength;

    public struct Node
    {
        public bool left;
        public bool right;
        public bool up;
        public bool down;

        public Node(bool start)
        {
            left = start;
            right = start;
            up = start;
            down = start;
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
    }

    private void Generator()
    {
        Node startNode = new Node(false);

        pathLength = 0;

        int distance = 0;

        endLocation = startLocation;

        CheckNode(startLocation, distance);
    }

    private bool CheckNode(Vector3 location, int distance)
    {
        if (location.x < 0 || location.x > size || location.y < 0 || location.y > size)
        {
            return false;
        }
        else if (maze.ContainsKey(new twoInts((int)location.x, (int)location.y)))
        {
            return false;
        }

        Node node = new Node(false);

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
            switch(start)
            {
                case 0: node.right = CheckNode(location + Vector3.right, distance + 1);
                    break;
                case 1: node.left = CheckNode(location + Vector3.left, distance + 1);
                    break;
                case 2: node.up = CheckNode(location + Vector3.up, distance + 1);
                    break;
                case 3: node.down = CheckNode(location + Vector3.down, distance + 1);
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

        return true;
    }
}
