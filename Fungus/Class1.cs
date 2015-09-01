using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;
using System.Threading;

namespace Fungus
{

  public class Fungus
  {
    private Point start;
    private CDrawer canvas;
    private Color color;
    private Dictionary<Point, int> pointDict = new Dictionary<Point, int>();
    private int color_strength;
    private EColor e;

    public static bool growing = true;
    public Fungus(Point s, CDrawer c, EColor e)
    {
      this.canvas = c;
      this.start = s;
      this.e = e;
      this.color_strength = 64;
    }
    private void updateColor()
    {
      if (e == EColor.Blue)
      {
        this.color = Color.FromArgb(0, 0, color_strength);
      }
      if (e == EColor.Red)
      {
        this.color = Color.FromArgb(color_strength, 0, 0);
      }
      if (e == EColor.Green)
      {
        this.color = Color.FromArgb(0, color_strength, 0);
      }

    }
    public void spore()
    {
      Thread t = new Thread(this.grow);
      t.IsBackground = true;
      t.Start();
    }
    private List<Point> getAdjacent(Point p)
    {
      List<Point> adjacent = new List<Point>();
      for (int i = 0; i < 3; ++i)
      {
        for (int j = 0; j < 3; ++j)
        {
          adjacent.Add(new Point(p.X + j - 1, p.Y + i - 1));
        }
      }
      adjacent.RemoveAll((Point r) => r.X >= canvas.ScaledWidth || r.Y >= canvas.ScaledHeight || r.X < 0 || r.Y < 0 || r == p);
      return adjacent;
    }
    private void grow()
    {
      canvas.SetBBScaledPixel(start.X, start.Y, color);
      pointDict.Add(this.start, 64);
      Point cur_point = start;
      while (growing)
      {
        List<Point> adjacent = this.getAdjacent(cur_point);
        adjacent.ShuffleList();
        var paired_list = adjacent.ToDictionary(x => x, x => pointDict.ContainsKey(x) ? pointDict[x] : 0).ToList();
        paired_list.Sort((x, y) => x.Value.CompareTo(y.Value));
        cur_point = paired_list[0].Key;
        if (pointDict.ContainsKey(cur_point))
        {
          pointDict[cur_point] += (pointDict[cur_point] <= 191) ? 64 : 255 - pointDict[cur_point];
          color_strength = pointDict[cur_point];
        }
        else
        {
          pointDict.Add(cur_point, 64);
          color_strength = 64;
        }
        updateColor();
        canvas.SetBBScaledPixel(cur_point.X, cur_point.Y, color);
        canvas.Render();
        Thread.Sleep(1);
      }
    }
  }


}
public static class Extension
{
  private static Random rand = new Random();
  public static List<Point> ShuffleList(this List<Point> l)
  {
    int swap;
    for (int i = 0; i < l.Count; ++i)
    {

      lock (rand)
      {
        swap = rand.Next(i, l.Count);
      }
      Point temp;
      temp = l[swap];
      l[swap] = l[i];
      l[i] = temp;
    }
    return l;
  }
}


