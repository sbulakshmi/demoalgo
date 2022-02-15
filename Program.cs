using static System.Linq.Enumerable;
using static System.String;
using static System.Console;
using System.Collections;
// See https://aka.ms/new-console-template for more information
// https://rosettacode.org/wiki/Dijkstra%27s_algorithm#C.23
Graph graph = new Graph(9);
var lookupDictionary = new Dictionary<string, int>();
Hashtable lookupHash = new Hashtable();

// Func<char, int> id = c => c - 'a';
// Func<int, char> name = i => (char)(i + 'a');

int AddKeyAndGetIntCode(string name)
{
    // int hCode = lookupHash.Count;//lookupDictionary.Count();// name.GetHashCode();
    // lookupHash.ContainsKey(name)?(hCode =lookupHash.g):(lookupHash.Add(hCode, name));
    int hCode = lookupDictionary.Count();// name.GetHashCode();
    if (lookupDictionary.ContainsKey(name))
    {
        hCode = lookupDictionary[name];
    }
    else
    {
        lookupDictionary.Add(name, hCode);
    }

    return hCode;
    //return (name[0] - 'a');
    // return name[0] + 'a';
}

string GetNameForIntCode(int id)
{
    return lookupDictionary.FirstOrDefault(x => x.Value == id).Key;
    //return ((char)(id + 'a')).ToString();
    //return (id - 'a').ToString();
}

Func<string, int> id = AddKeyAndGetIntCode;
Func<int, string> name = GetNameForIntCode;
//TODO: debug the flow
//TODO: Add directed edges, undirected edges
foreach (var (start, end, cost) in new[] {
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
    graph.AddEdge(id(start), id(end), cost);
}

//TODO: Display the matrix and get the start, destination
Console.WriteLine("********Available Node Names********");
WriteLine(Join(" , ", (lookupDictionary.Keys.Select(p => $"{p}"))));

Console.WriteLine();
Console.WriteLine("Enter the From Node Name");
//string fromNodeName = "a";
string fromNodeName = Console.ReadLine();
if (!lookupDictionary.ContainsKey(fromNodeName))
{
    Console.WriteLine();
    Console.WriteLine("Invalid From Node Name");
    return;
}

Console.WriteLine();
Console.WriteLine("Enter the To Node Name");
string toNodeName = Console.ReadLine();
if (!lookupDictionary.ContainsKey(toNodeName))
{
    Console.WriteLine();
    Console.WriteLine("Invalid To Node Name");
    return;
}
//string toNodeName = "i";//Console.ReadKey().KeyChar;
//char t = Console.ReadKey().KeyChar;
Console.WriteLine();
//TODO: check if valid char is entered

// var path = graph.FindPath(id("a"));
var path = graph.FindPath(id(fromNodeName));
//TODO: Display the path for the destination

WriteLine(Join(" , ", Path(id(fromNodeName), id(toNodeName)).Select(p => $"{name(p.node)}({p.distance})").Reverse()));
//TODO: Input, output as per use case description
//TODO: Parallel operations
//TODO: Stack to pop?
//TODO: why custom heap class?
//TODO: Unit test cases
//TODO: SOLID
IEnumerable<(double distance, int node)> Path(int start, int destination)
{
    yield return (path[destination].distance, destination);
    for (int i = destination; i != start; i = path[i].prev)
    {
        yield return (path[path[i].prev].distance, path[i].prev);
    }
}

// ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Nodes> graphNode)
// {

// }

// class ShortestPathData
// {
//     List<string> NodeNames { get; set; }
//     int Distance { get; }
// }