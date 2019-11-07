using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeuristicStrategy { EuclideanDistance, ManhatanDistance };
public class Graph2
{
    HeuristicStrategy strategy;
    public Graph2(HeuristicStrategy strategy)
    {
        this.strategy = strategy;
    }
    Dictionary<char, Dictionary<char, float>> vertices = new Dictionary<char, Dictionary<char, float>>();
    Dictionary<char, Vector3> verticesData = new Dictionary<char, Vector3>();

    public void add_vertex_AStar(char vertex, Vector3 pos, Dictionary<char, float> edges)
    {
        vertices[vertex] = edges;
        verticesData[vertex] = pos;
    }
    public float EuclideanDistance(Vector3 v1, Vector3 v2)
    {
        return Vector3.Distance(v1, v2);
    }
    public float ManhatanDistance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Abs(v1.x - v2.x) + Mathf.Abs(v1.y - v2.y) + Mathf.Abs(v1.z - v2.z);
    }
    public float GoalDistanceEstimate(char start, char finish)
    {
        float res = 0f;
        switch (strategy)
        {
            case HeuristicStrategy.EuclideanDistance:
                //res = Vector3.Distance(verticesData[start], verticesData[finish]);
                res = EuclideanDistance(verticesData[start], verticesData[finish]);
                break;
            case HeuristicStrategy.ManhatanDistance:
                res = ManhatanDistance(verticesData[start], verticesData[finish]);
                break;
            default:
                break;


        }
        return res;
    }

    public List<char> shortest_path_via_AStar_algo(char start, char finish)
    {
        //throw new NotImplementedException();
        List<char> path = null;
        var previous = new Dictionary<char, char>();
        var distances = new Dictionary<char, float>(); //try to put fScore (= gScore+hScore)
        var gScore = new Dictionary<char, float>();

        var Pending = new List<char>(); //Open priority queue
        var Closed = new List<char>();  //Closed list
        //Step 0
        gScore[start] = 0;
        float hScore = GoalDistanceEstimate(start, finish);
        distances[start] = gScore[start] + hScore;
        previous[start] = '-';
        Pending.Add(start);



        //foreach (var vertex in vertices)
        //{
        //  if (vertex.Key == start)
        //{
        //}
        //else
        //{
        //    gScore[vertex.Key] = int.MaxValue;
        //    distances[vertex.Key] = int.MaxValue;
        //}
        //}
        //General Step
        while (Pending.Count != 0)
        {
            Pending.Sort((x, y) => (int)(distances[x].CompareTo(distances[y])));//Instead of a priority queue

            var smallest = Pending[0];
            Pending.Remove(smallest);

            //did we catch the finish vertex?
            if (smallest == finish)
            {
                path = new List<char>();
                while (previous.ContainsKey(smallest))
                {
                    path.Add(smallest);
                    smallest = previous[smallest];
                }
                break;
            }
            /*
            //is smallest infinity? (no path is found - return null)
            if (distances[smallest] == int.MaxValue)
            {
                break;
            }
            */
            //relax distances
            foreach (var neighbor in vertices[smallest])
            {
                var new_gScore = gScore[smallest] + neighbor.Value;

                if (Closed.Contains(neighbor.Key) && gScore[neighbor.Key] <= new_gScore)
                    continue;

                previous[neighbor.Key] = smallest;
                gScore[neighbor.Key] = new_gScore;
                var new_hScore = GoalDistanceEstimate(neighbor.Key, finish);
                distances[neighbor.Key] = new_gScore + new_hScore;

                if (Closed.Contains(neighbor.Key))
                {
                    Closed.Remove(neighbor.Key);
                }
                if (!Pending.Contains(neighbor.Key))
                {
                    Pending.Add(neighbor.Key);
                }
            }
            Closed.Add(smallest); //

        }
        return path;
    }
}
public class AStarAlgo : MonoBehaviour
{
    void PrintPath(List<char> shortest_path)
    {
        print("shortest_path:" + shortest_path);
        if (shortest_path == null)
        {
            print("No Path found!");
        }
        else
        {
            shortest_path.Reverse();

            string p = "";// "F";
            for (int i = 0; i < shortest_path.Count; i++)
            {
                p += shortest_path[i];

            }
            print(p);
        }
    }
    void GivenExample()
    {
        Graph2 g = new Graph2(HeuristicStrategy.EuclideanDistance);
        g.add_vertex_AStar('A', new Vector3(4, 4, 0), new Dictionary<char, float>() { { 'B', 4 }, { 'X', 20 } }); ;
        g.add_vertex_AStar('B', new Vector3(8, 4, 0), new Dictionary<char, float>() { { 'A', 4 }, { 'C', 4 } }); ;
        g.add_vertex_AStar('C', new Vector3(12, 4, 0), new Dictionary<char, float>() { { 'B', 4 }, { 'Z', 4 } }); ;
        g.add_vertex_AStar('Z', new Vector3(12, 0, 0), new Dictionary<char, float>() { { 'C', 4 }, { 'Y', 4 } }); ;
        g.add_vertex_AStar('Y', new Vector3(8, 0, 0), new Dictionary<char, float>() { { 'X', 4 }, { 'Z', 4 } }); ;
        g.add_vertex_AStar('X', new Vector3(4, 0, 0), new Dictionary<char, float>() { { 'A', 20 }, { 'W', 4 }, { 'Y', 4 } }); ;
        g.add_vertex_AStar('W', new Vector3(0, 0, 0), new Dictionary<char, float>() { { 'X', 4 } }); ;

        //print("g:" + g);
        char start = 'C', finish = 'X';

        List<char> shortest_path = g.shortest_path_via_AStar_algo(start, finish);
        PrintPath(shortest_path);

    }

    void WikipediaExample()
    {
        Graph2 g = new Graph2(HeuristicStrategy.EuclideanDistance);
        g.add_vertex_AStar('S', new Vector3(0, 0, 4), new Dictionary<char, float>() { { 'D', 2 }, { 'A', 1.5f } });
        g.add_vertex_AStar('A', new Vector3(1, 0, 2), new Dictionary<char, float>() { { 'B', 2 }, { 'S', 1.5f } });
        g.add_vertex_AStar('B', new Vector3(3, 0, 1.5f), new Dictionary<char, float>() { { 'C', 3 }, { 'A', 2f } });
        g.add_vertex_AStar('C', new Vector3(0, 0, 0), new Dictionary<char, float>() { { 'F', 4 }, { 'B', 3f } });
        g.add_vertex_AStar('D', new Vector3(2, 0, 4), new Dictionary<char, float>() { { 'S', 2 }, { 'E', 3f } });
        g.add_vertex_AStar('E', new Vector3(3.5f, 0, 2), new Dictionary<char, float>() { { 'D', 3 }, { 'F', 2f } });
        g.add_vertex_AStar('F', new Vector3(4, 0, 0), new Dictionary<char, float>() { { 'C', 4 }, { 'E', 2f } });

        char start = 'S', finish = 'F';
        List<char> shortest_path = g.shortest_path_via_AStar_algo(start, finish);
        PrintPath(shortest_path);
    }
    // Use this for initialization
    void Start()
    {
        //GivenExample();
        WikipediaExample();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
