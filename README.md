Winnie
======

Winnie is a strategy video-game inspired from the board game "Small World".
It's build using WPF and MonoGame technologies, with a C++ library.

This is an educational project for Computer Science Departement of [INSA Rennes](http://www.insa-rennes.fr/en.html), built by:
- [Quentin Dufour (Superboum)](https://github.com/superboum)
- [Loïck Bonniot (Lesterpig)](https://github.com/lesterpig)

The user manual is available [in this repository](USER_MANUAL.pdf).

Build from source
-----------------

1. Install MonoGame tools if not present on your system
2. Compile assets present in “gameRessources” folder with MonoGame Pipeline tool
3. Open “Winnie/WinnieLin.sln” or “Winnie/WinnieWin.sln” depending on your current operating system
4. Build “AlgoWin” or “AlgoLin” alone (C++ classes)
5. Build the whole solution
6. Run!

Known issues
------------

This project is not known to run smoothly on win7 platform.
This should be fixed by the installation of the VC++ redistributable package, but not garanteed.
