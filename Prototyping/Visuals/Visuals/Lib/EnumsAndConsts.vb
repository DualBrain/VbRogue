Public Class EnumsAndConsts

  Public Const MapLevelsMax As Integer = 99
  Public Const MapHeight As Integer = 24
  Public Const MapWidth As Integer = 80

  Public Enum CellType As Integer
    StructureSolidStone = 0
    StructureTunnel = 1
    StructureWallTopBottom = 2
    StructureWallSide = 3
    StructureWallTopLeftCorner = 4
    StructureWallTopRightCorner = 5
    StructureWallBottomLeftCorner = 6
    StructureWallBottomRightCorner = 7
    StructureDoorTopBottom = 8
    StructureDoorSide = 9
    StructureFloor = 10
    StructureStairsDown = 11
    StructureStairsUp = 12

    UserSelf = 100
    ItemWeapon = 201
    ItemArmor = 202
    ItemPotion = 203
    ItemGold = 204
    ItemRing = 205

    MonsterKestrel = 301
    MonsterInvisibleStalker = 302

  End Enum

End Class
