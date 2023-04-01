namespace ServerTest;
using Server;

[TestClass]
public class DynamicAgainstStaticCollisionDetectionTest
{
  private readonly ICollisionDetector cd = new CollisionDetector();

  private bool AreEqualWithCdPrecision(float lhs, float rhs)
  {
    return Math.Abs(lhs - rhs) < IPhysics.COLLISION_EPS;
  }

  [TestMethod]
  public void FreeFall()
  {
    var source = new Vector2D(5, 5);
    var destination = new Vector2D(5, 2);

    var upperPlatform = new Platform(5, 1);
    upperPlatform.Position = new Vector2D(5, 8);

    Assert.AreEqual(null, cd.FindCollisionPointStatic(source, destination, upperPlatform));

    var lowerPlatform = new Platform(5, 1);
    lowerPlatform.Position = new Vector2D(5, 0);
    Assert.AreEqual(null, cd.FindCollisionPointStatic(source, destination, lowerPlatform));

    var leftPlatform = new Platform(2, 1);
    leftPlatform.Position = new Vector2D(1, 3);
    Assert.AreEqual(null, cd.FindCollisionPointStatic(source, destination, leftPlatform));
  }

  [TestMethod]
  public void CollidingFall()
  {
    var source = new Vector2D(5, 5);
    var destination = new Vector2D(5, 1);
    var centeredPlatform = new Platform(3, 1);
    centeredPlatform.Position = new Vector2D(4, 3);
    Assert.AreNotEqual(null, cd.FindCollisionPointStatic(source, destination, centeredPlatform));
  }
}