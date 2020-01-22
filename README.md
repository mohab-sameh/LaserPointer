# LaserPointer
LaserPointer tracks a laser pointer objected on a surface and enables intuitive controls using a simple laser pointer.


LaserPointer has 2 versions, one written in C# and another written in Python. Both implement OpenCV.
Run the desired version and wait for the camera window to popup.

Point a laser beam to a surface and see the camera detecting the laser beam and the laser beam shall be followed by a 
circle or a contour on the monitor.

C# version uses a red laser beam by default and implements masking using an intensity algorithm, while
the python version uses a green laser beam by default and implements HSV masking. However, laser color beams can be changed 
according to the users' needs and scenario.
