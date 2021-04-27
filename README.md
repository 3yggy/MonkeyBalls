![:(](http://raw.githubusercontent.com/3yggy/MonkeyBalls/main/balls.ico)
# MonkeyBalls
How can you trust a hash function or the cold ConditionalWeakTables of a compiler?

```cs
//Example
Tree tree = new Tree();
tree.m.DemonstrateLiving();
Console.WriteLine("Setting Value ...");
tree.m = MB.SetMonkeyPoop(tree.m, new Intestines());
tree.m.verb = "I feel well with Intestines!";
tree.m.DemonstrateLiving();
Console.WriteLine($"Monkey has Intestines with {MB.GetMonkeyPoop<Intestines>(tree.m).bits} bits.");
Console.WriteLine("Setting Value ...");
MB.SetMonkeyPoop(tree, "m", new SuperIntestines());
tree.m.verb = "I feel well with SuperIntestines!";
tree.m.DemonstrateLiving();
Console.WriteLine($"Monkey has Intestines with {MB.GetMonkeyPoop<Intestines>(tree.m).bits} bits.");
tree.m.verb = "I am never harmed!";
tree.m.DemonstrateLiving();
Console.ReadKey();
/* 
Output:
      Monkey: I am alive and well!
      Setting Value ...
      Monkey: I feel well with Intestines!
      Monkey has Intestines with 3 bits.
      Setting Value ...
      Monkey: I feel well with SuperIntestines!
      Monkey has Intestines with 31991 bits.
      Monkey: I am never harmed!
*/
public class Tree
{
  public Monkey m;
  public Tree() {
      m = new Monkey();
  }
}
public class Monkey
{
  public string verb = "I am alive and well!";
  public void DemonstrateLiving() {
      Console.WriteLine("Monkey: " + verb);
  }
}
public class Intestines
{
  public short bits = 3;
}
public class SuperIntestines : Intestines
{
  public SuperIntestines() {
      base.bits = 31991;
  }
}
```
This was made by the will of Mr Pardyk, saying I would be unbanned if I made monkey balls.
