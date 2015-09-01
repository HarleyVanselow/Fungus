Fungus
Harley Vanselow 2015

While the main program loop takes place in Fungus/Form1.cs, the Fungus class that powers the simulation can be
found at Fungus/Class1.cs

The Fungus class powers a simulation of a fungus growing. As a fungus grows, it tracks where it has grown and
de-prioritizes those areas to emulate nutrients being consumed from that spot. If a fungus must revisit a spot, that spot
is colored more vividly each time it is revisited. Each Fungus object, when spore is called from it, generates its own thread.
This allows several fungus objects to work with a single canvas concurrently.

The GDIDrawer library used was developed by engineers at NAIT and can be viewed here: 
https://github.com/NAIT-CNT/GDIDrawer
