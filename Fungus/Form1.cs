using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;

namespace Fungus
{
  public enum EColor { Red, Green, Blue };
  public partial class Form1 : Form
  {
    public static CDrawer canvas = new CDrawer(1000, 1000);
    public EColor c = EColor.Red;
    public static Random rand = new Random();
    public Form1()
    {
      InitializeComponent();
      Fungus f1 = new Fungus(new Point(rand.Next(canvas.ScaledWidth), rand.Next(canvas.ScaledHeight)), canvas, EColor.Red);
      Fungus f2 = new Fungus(new Point(rand.Next(canvas.ScaledWidth), rand.Next(canvas.ScaledHeight)), canvas, EColor.Blue);
      Fungus f3 = new Fungus(new Point(rand.Next(canvas.ScaledWidth), rand.Next(canvas.ScaledHeight)), canvas, EColor.Green);
      Fungus[] fungi = { f1, f2, f3 };
      foreach (Fungus f in fungi)
      {
        f.spore();
      }
    }
  }
}
