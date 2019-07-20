Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public Enum TileType
    Void
    Floor
    Tunnel
    Hero ' On level start, replace with Floor.
    Hole
    Door
    SecretVertical
    SecretHorizontal
    WallHorizontal
    WallVertical
    WallTopLeft
    WallTopRight
    WallBottomLeft
    WallBottomRight
  End Enum

End Namespace