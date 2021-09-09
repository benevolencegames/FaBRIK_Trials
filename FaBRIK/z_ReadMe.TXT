Here you go, LegacyOfwax, this is the demonstration files for Forward and Backward Reaching Inverse Kinematics. Just follow the steps to get it up and running.

1. Import the FaBRIK folder into your project

2. Create an empty GameObject named "Renderer" and reset it's transform.

3. Attach the IKGFX.cs to this "Renderer" object.

4. Create 3 empty GameObjects to hold the transform of the points. You can name them like P1, P2, etc or whatever.

5. Set the X values of their positions to be 0, 2, 4, and 6 respectively, and the Y and Z of all to be 0.

6. Set the array length in the IKGFX component on the "Renderer" to 4, then assign each of the points to the array. The first point in the array is the base, the part that cant move, only rotate.

7. Create an empty GameObject named "Target" and reset it's transform.

8. Assign the "Target" GameObject to the Target field in the IKGFX component on the "Renderer"

9. Set the Radius field in the IKGFX component to 0.18 amd the Line Thickness 

Enjoy!