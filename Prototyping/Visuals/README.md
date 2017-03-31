# VbRogue\Prototyping\Visuals

Purpose: To experiment with attempting to recreate the visual elements of Rogue (Epyx) circa mid-1980's.

# Challenges

* The colors in Windows 10 console applications doesn't match exactly with those in MS-DOS. Specifically, the color orange is actually orange in Windows 10 where the color orange is  more of a brown color in MS-DOS.  This color is used for the walls in the original Rogue.
* .NET is unicode based; where as original Rogue used ASCII and (possibly) ANSI.  This means that drawing of the walls and other special characters needs to be handled using the unicode table and there isn't (AFAIK) a 1:1 mapping between ASCII and unicode.