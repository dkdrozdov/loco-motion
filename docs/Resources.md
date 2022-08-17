# Resources

## Classification

Resource pack kinds:
- common
- scene-specific

Resource item kinds:
-  animatable
-  texture (identifiable picture)

_Animatable_ resource item consist of:
- sprites (identifiable png-alike picture)
- animations (identifiable sprite sequence)

## Resource structure example

* item 1
* item 2
  * item 2.1
* item 3

Common resources
* agent (animatable item)
  * standing (sprite)
  * falling (sprite)
  * running 1 (sprite)
  * running 2 (sprite)
  * shooting 1 (sprite)
  * shooting 2 (sprite)
  * standing (animation) (standing)
  * falling (animation) (falling)
  * running (animation) (running 1, running 2)
  * shooting (animation) (shooting 1, shooting 2)
*  default texture (texture item)

Scene specific resources
* fan (animatable item)
  * rotation 1 (sprite)
  * rotation 2 (sprite)
  * rotating (animation) (rotation 1, rotation 2)
* barrel (animatable item)
  * standing (sprite)
  * standing (animation) (standing)
* ground texture (texture item)
* wall texture (texture item)

Scene definition
* resource pack
  * ground texture
  * wall texture
  * fan (animatable item)
    * rotation 1 (sprite)
    * rotation 2 (sprite)
    * rotating (animation) (rotation 1, rotation 2)
* objects
  * platform 1 (ground texture, position, size)
  * platform 2 (ground texture, position, size)
  * wall 1 (wall texture, position, size)
  * wall 2 (wall texture, position, size)
  * wall 3 (wall texture, position, size)