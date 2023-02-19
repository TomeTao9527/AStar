// See https://aka.ms/new-console-template for more information
using AStar_program;
using System.Drawing;
using System.Numerics;

Console.WriteLine("Hello, AStar!");
Map map = new Map(10, 10);

AStarInterface AStar = new AStarInterface(map);

var Path= AStar.GetPath(new MapNode(new Vector2(1,1),10), new MapNode(new Vector2(8, 8), 10));
for(int i=0;i<Path.Count;i++)
{
    var item = Path[i];
    if(item.father!=null)
    {
        item.father.ContainInPath = true;
    }
}
for(int i=0;i<10;i++)
{
    for(int j=0;j<10;j++)
    {
        var item = map.Nodes[i*10+j];
        Console.Write((item.ContainInPath?"1":"0")+" ");
    }
    Console.WriteLine();
}
Console.ReadLine();

