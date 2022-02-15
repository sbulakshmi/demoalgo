using static System.Linq.Enumerable;
using EdgeList = System.Collections.Generic.List<(int node, double weight)>;
sealed class Graph
{
    private readonly List<EdgeList> adjacency;

    public Graph(int vertexCount) => adjacency = Range(0, vertexCount).Select(v => new EdgeList()).ToList();

    public int Count => adjacency.Count;
    public bool HasEdge(int s, int e) => adjacency[s].Any(p => p.node == e);
    public bool RemoveEdge(int s, int e) => adjacency[s].RemoveAll(p => p.node == e) > 0;

    public bool AddEdge(int s, int e, double weight)
    {
        if (HasEdge(s, e)) return false;
        adjacency[s].Add((e, weight));
        return true;
    }

    public (double distance, int prev)[] FindPath(int start)
    {
        var info = Range(0, adjacency.Count).Select(i => (distance: double.PositiveInfinity, prev: i)).ToArray();
        info[start].distance = 0;
        var visited = new System.Collections.BitArray(adjacency.Count);

        var heap = new Heap<(int node, double distance)>((a, b) => a.distance.CompareTo(b.distance));
        heap.Push((start, 0));
        while (heap.Count > 0)
        {
            var current = heap.Pop();
            if (visited[current.node]) continue;
            var edges = adjacency[current.node];
            for (int n = 0; n < edges.Count; n++)
            {
                int v = edges[n].node;
                if (visited[v]) continue;
                double alt = info[current.node].distance + edges[n].weight;
                if (alt < info[v].distance)
                {
                    info[v] = (alt, current.node);
                    heap.Push((v, alt));
                }
            }
            visited[current.node] = true;
        }
        return info;
    }

}