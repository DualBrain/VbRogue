Public Class EnumsAndConsts

  Public Const MapLevelGridMax As Integer = 3 ' The highest number of level grid sections height and width
  Public Const MapLevelsMax As Integer = 99 'The maximum number of levels in the dungeon
  Public Const MapHeight As Integer = 24 'The maximum number of rows on the CRT display
  Public Const MapWidth As Integer = 80 'The maximum number of columns on the CRT display
  Public Const MapGridCellHeight As Integer = 7 'The maximum number of rows in a level map grid cell
  Public Const MapGridCellWidth As Integer = 26 'The maximum number of columns in a level map grid cell
  Public Const MinHasRoomPercentage As Integer = 70 'Any random number out of 100 less or equal will have room

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
