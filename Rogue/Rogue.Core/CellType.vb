Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public Enum CellType As Integer

    Void = 0
    StructureTunnel = 1
    StructureWallTopBottom = 2
    StructureWallSide = 3
    StructureWallTopLeftCorner = 4
    StructureWallTopRightCorner = 5
    StructureWallBottomLeftCorner = 6
    StructureWallBottomRightCorner = 7
    'StructureDoorTopBottom = 8
    'StructureDoorSide = 9
    StructureDoor = 8
    StructureFloor = 10
    StructureStairsDown = 11
    'StructureStairsUp = 12
    SecretHorizontal = 13
    SecretVertical = 14

    Hero = 100
    ItemWeapon = 201
    ItemArmor = 202
    ItemPotion = 203
    ItemGold = 204
    ItemRing = 205

    MonsterKestrel = 301
    MonsterInvisibleStalker = 302

    Unknown = 99999

  End Enum

End Namespace