using static System.Linq.Enumerable;
using static System.String;
using static System.Console;
using System.Collections;


var lookupDictionary = new Dictionary<string, int>();
Func<string, int> id = AddKeyAndGetIntCode;
Func<int, string> name = GetNameForIntCode;

int AddKeyAndGetIntCode(string name)
{
    int hCode = lookupDictionary.Count();
    if (lookupDictionary.ContainsKey(name))
    {
        hCode = lookupDictionary[name];
    }
    else
    {
        lookupDictionary.Add(name, hCode);
    }
    return hCode;
}

string GetNameForIntCode(int id)
{
    return lookupDictionary.FirstOrDefault(x => x.Value == id).Key;
}

//Initialise the graph
Graph graph = new Graph(9);
foreach (var (start, end, weight) in new[] {
            ("a", "b", 4),
            ("b", "a", 4),
            ("a", "c", 6),
            ("c", "a", 6),
            ("b", "f", 2),
            ("f", "b", 2),
            ("c", "d", 8),
            ("d", "c", 8),
            ("d", "e", 4),
            ("e", "d", 4),
            ("d", "g", 1),
            ("g", "d", 1),
            ("e", "b", 2),
            ("e", "f", 3),
            ("f", "e", 3),
            ("e", "i", 8),
            ("i", "e", 8),
            ("f", "h", 6),
            ("h", "f", 6),
            ("f", "g", 4),
            ("g", "f", 4),
            ("h", "g", 5),
            ("g", "h", 5),
        })
{
    graph.AddEdge(id(start), id(end), weight);
}
Console.WriteLine("********Available Node Names********");
WriteLine(Join(" , ", (lookupDictionary.Keys.Select(p => $"{p}"))));

Console.WriteLine();
Console.WriteLine("Enter the From Node Name");
string fromNodeName = "a";
//string fromNodeName = Console.ReadLine();
if (!lookupDictionary.ContainsKey(fromNodeName))
{
    Console.WriteLine();
    Console.WriteLine("Invalid From Node Name");
    return;
}

Console.WriteLine();
Console.WriteLine("Enter the To Node Name");
string toNodeName = "d";
//string toNodeName = Console.ReadLine();
if (!lookupDictionary.ContainsKey(toNodeName))
{
    Console.WriteLine();
    Console.WriteLine("Invalid To Node Name");
    return;
}
Console.WriteLine();

ShortestPathData result = ShortestPath(fromNodeName, toNodeName, graph);

Console.WriteLine();
Console.WriteLine("*********Suggested Path*********");
//WriteLine(Join(" , ", result.NodeNames.Select(p => $"{p}").Reverse()));
WriteLine(Join(" , ", result.NodeNames.Select(p => $"{p}")));
Console.WriteLine();
Console.WriteLine("*********Distance covered by path*********");
WriteLine(result.Distance);

ShortestPathData ShortestPath(string fromNodeName, string toNodeName, Graph graphNode)
{
    ShortestPathData result = new ShortestPathData();
    var path = graph.FindPath(id(fromNodeName));
    // result.NodeNames.AddRange(Path(id(fromNodeName), id(toNodeName)).Select(p => ((name(p.node)))));
    // result.Distance = path[id(toNodeName)].distance;

    TraversePath(id(fromNodeName), id(toNodeName), id(fromNodeName), id(toNodeName));
    result.Distance = path[id(toNodeName)].distance;
    return result;

    // IEnumerable<(double distance, int node)> Path(int start, int destination)
    // {
    //     yield return (path[destination].distance, destination);
    //     for (int i = destination; i != start; i = path[i].prev)
    //     {
    //         yield return (path[path[i].prev].distance, path[i].prev);
    //     }
    // }
    void TraversePath(int start, int destination, int orgStart, int orgDestination)
    {
        if (start != destination)
            TraversePath(start, path[destination].prev, orgStart, orgDestination);
        if (destination != orgStart)
            result.NodeNames.Add(name(path[destination].prev));
        if (destination == orgDestination)
            result.NodeNames.Add(name(destination));
    }
}
