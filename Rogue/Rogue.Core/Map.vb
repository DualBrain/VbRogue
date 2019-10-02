Option Explicit On
Option Strict On
Option Infer On
Imports System.IO

Namespace Global.Rogue.Core

  Public NotInheritable Class Map

    Private ReadOnly ModifierUp As New Coordinate(0, -1)
    Private ReadOnly ModifierDown As New Coordinate(0, +1)
    Private ReadOnly ModifierLeft As New Coordinate(-1, 0)
    Private ReadOnly ModifierRight As New Coordinate(+1, 0)
    Private m_VerifiedCount As Integer = 0

    ' The cell type value of the square the character is currently location at...
    'Private m_CellTypeBeforeCharacterEntered As Integer = -1

    'Private ReadOnly m_IsFogOfWarActive As Boolean = True

    'Private m_EntryStairGrid As Coordinate
    'Private m_ExitStairGrid As Coordinate

    Public Sub New() 'depth%)
      'Me.m_TestMode = generate
      'Me.CurrentMapLevel = depth
      Initialize() 'generate)
    End Sub

    '''' <summary>
    '''' Hold a pointer to the level of the current map
    '''' Bigger numbers means further down into the dungeon.
    '''' 0=not any level, 1=first or beginning level.
    '''' </summary>
    '''' <returns></returns>
    'Public Property CurrentMapLevel() As Integer

    'Private Property Row As Integer = 0
    'Private Property Column As Integer = 0

    '''' <summary>
    '''' Hold a pointer to which grid cell the EntryStairLocation points to
    '''' Number is YX 
    '''' Y being the row 
    '''' X being the column 
    '''' </summary>
    '''' <returns></returns>
    'Public Property EntryStairGrid() As Coordinate
    '  Get
    '    If Me.EntryStairLocation IsNot Nothing Then
    '      Me.Row = Me.EntryStairLocation.Y
    '      Me.Column = Me.EntryStairLocation.X

    '      If Me.Row <= 6 Then
    '        If Me.Column < 26 Then
    '          Me.m_EntryStairGrid = New Coordinate(1, 1)
    '        End If
    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
    '          Me.m_EntryStairGrid = New Coordinate(2, 1)
    '        End If
    '        If Me.Column >= 53 Then
    '          Me.m_EntryStairGrid = New Coordinate(3, 1)
    '        End If
    '      End If
    '      If Me.Row >= 7 AndAlso Me.Row <= 13 Then
    '        If Me.Column < 26 Then
    '          Me.m_EntryStairGrid = New Coordinate(1, 2)
    '        End If
    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
    '          Me.m_EntryStairGrid = New Coordinate(2, 2)
    '        End If
    '        If Me.Column >= 53 Then
    '          Me.m_EntryStairGrid = New Coordinate(3, 2)
    '        End If
    '      End If
    '      If Me.Row >= 14 Then
    '        If Me.Column < 26 Then
    '          Me.m_EntryStairGrid = New Coordinate(1, 3)
    '        End If
    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
    '          Me.m_EntryStairGrid = New Coordinate(2, 3)
    '        End If
    '        If Me.Column >= 53 Then
    '          Me.m_EntryStairGrid = New Coordinate(3, 3)
    '        End If
    '      End If

    '    End If
    '    Return Me.m_EntryStairGrid
    '  End Get
    '  Set(value As Coordinate)
    '    Me.m_EntryStairGrid = value
    '  End Set
    'End Property

    '''' <summary>
    '''' Hold a pointer to which grid cell the ExitStairLocation points to
    '''' Number is YX 
    '''' Y being the row 
    '''' X being the column 
    '''' </summary>
    '''' <returns></returns>
    'Public Property ExitStairGrid() As Coordinate
    '  Get
    '    If Me.ExitStairLocation IsNot Nothing Then
    '      Me.Row = Me.ExitStairLocation.Y
    '      Me.Column = Me.ExitStairLocation.X

    '      If Me.Row <= 6 Then
    '        If Me.Column < 26 Then
    '          Me.m_ExitStairGrid = New Coordinate(1, 1)
    '        End If
    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
    '          Me.m_ExitStairGrid = New Coordinate(2, 1)
    '        End If
    '        If Me.Column >= 53 Then
    '          Me.m_ExitStairGrid = New Coordinate(3, 1)
    '        End If
    '      End If
    '      If Me.Row >= 7 AndAlso Me.Row <= 13 Then
    '        If Me.Column < 26 Then
    '          Me.m_ExitStairGrid = New Coordinate(1, 2)
    '        End If
    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
    '          Me.m_ExitStairGrid = New Coordinate(2, 2)
    '        End If
    '        If Me.Column >= 53 Then
    '          Me.m_ExitStairGrid = New Coordinate(3, 2)
    '        End If
    '      End If
    '      If Me.Row >= 14 Then
    '        If Me.Column < 26 Then
    '          Me.m_ExitStairGrid = New Coordinate(1, 3)
    '        End If
    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
    '          Me.m_ExitStairGrid = New Coordinate(2, 3)
    '        End If
    '        If Me.Column >= 53 Then
    '          Me.m_ExitStairGrid = New Coordinate(3, 3)
    '        End If
    '      End If

    '    End If
    '    Return Me.m_ExitStairGrid
    '  End Get
    '  Set(value As Coordinate)
    '    Me.m_ExitStairGrid = value
    '  End Set
    'End Property

    ''' <summary>
    ''' Hold a pointer to where on the level the player will come down the stairs
    ''' There must be a room here when level is generated
    ''' Number is YYXX 
    ''' YY being the row as two digits which may be zero padded
    ''' XX being the column as two digits which may be zero padded
    ''' </summary>
    ''' <returns></returns>
    Public Property EntryStairLocation() As Coordinate 'String = ""

    ''' <summary>
    ''' Hold a pointer to where on the level the player will go down to the next level
    ''' There must be a room here when level is generated
    ''' Number is YYXX 
    ''' YY being the row as two digits which may be zero padded
    ''' XX being the column as two digits which may be zero padded
    ''' </summary>
    ''' <returns></returns>
    Public Property ExitStairLocation() As Coordinate 'String = ""

    ''' <summary>
    ''' Each cell of the array contains a indicator which defines what is in that location of the map defined in enum CellType
    ''' 0 - 99 are reserved for structural elements
    ''' 100 indicates the user
    ''' 101 - 199 are reserved for future multiplayer aspects
    ''' 201 - 999 are reserved for items that can be picked up or manipulated
    ''' 1001 - x are reserved for creatures
    ''' </summary>
    Public MapCellData(Param.MapHeight - 1, Param.MapWidth - 1) As CellType

    '''' <summary>
    '''' Used to determine if a particular cell of the map has been made visible to the user.
    '''' </summary>
    'Public MapCellVisibility(Param.MapHeight - 1, Param.MapWidth - 1) As Boolean

    '''' <summary>
    '''' Used to store if a particular cell of the map has a room 
    '''' Used in tunnel creation.
    '''' </summary>
    'Public Property MapCellHasRoom(Param.GridRowCount + 1, Param.GridColumnCount + 1) As String

    Public Property Rooms() As New List(Of Room)

    Public Property RoomConnections() As New List(Of Coordinate) ' keep track of which rooms this room is connected to

    Public Sub Initialize()

      'Me.Row = 0
      'Me.Column = 0

Start:

      Rooms = New List(Of Room)
      RoomConnections = New List(Of Coordinate)

      ' Generate 1 or more random rooms in random locations (1-9)

      ClearMapData()

      ' Create a list of possible grid locations (1-9)...
      Dim grid = {New Coordinate(1, 1),
                              New Coordinate(2, 1),
                              New Coordinate(3, 1),
                              New Coordinate(1, 2),
                              New Coordinate(2, 2),
                              New Coordinate(3, 2),
                              New Coordinate(1, 3),
                              New Coordinate(2, 3),
                              New Coordinate(3, 3)}.ToList

      ' Now we will loop through a maximum of 9 times...
      For r = 1 To 9

        ' Pick a random entry from the grid list...

        Dim number = Param.Randomizer.Next(0, grid.Count)

        ' Now we will determine if we are going to actually create...

        ' First we need to determine if this is the first room being
        ' placed... if so, then we will place it as we will always
        ' have at least one room.

        Dim place = Rooms.Count = 0

        If Not place Then

          ' If not the first room being placed, then we will determine
          ' whether or not this room will be placed by rolling a d100.

          place = Param.Randomizer.Next(1, 101) <= Param.MinHasRoomPercentage

        End If

        'must have at least 6 rooms
        Dim RoomsLeftToCreate As Integer = Core.Param.MinRoomsPerLevel - Rooms.Count
        Dim CellsLeftToCheck As Integer = (Core.Param.GridRowCount * Core.Param.GridColumnCount) - r
        If CellsLeftToCheck - RoomsLeftToCreate >= 0 Then
          'use random place
          place = place
        Else
          'must create room here
          place = True
        End If

        If place Then

          ' We've determined that we are placing this room...
          Dim c = grid(number)
          ' So we will actually generate the room parameters (and doors).
          CreateRoom(c.Y, c.X)
          ' And remove it from the list of possible locations before we
          ' try for the next pass.  Yes, this means that it is possible
          ' that the same room (at random) could try again in another
          ' pass if it didn't place during a previous pass.  Removing it
          ' from the available list prevents a useless second attempt.
          grid.RemoveAt(number)
        End If

      Next

      ' In each room, generate 1 or more random doors;
      '  however, limit to a single door per wall;
      '  so a maximum of 4 doors.
      '  With that said, need to restrict creating a door on a
      '  wall that would be an invalid tunnel location.

      If True Then
        For Each rm In Rooms
          CreateDoors(rm)
        Next
      End If

      ' Now that we have rooms, need we need to genrate connections between each room:
      '
      '   If there is a room in the center, start with it.
      '   If not a room in the center, pick another room at random.
      '   Now that we have a room to start with, pick a door (source) at random in the selected room.
      '   Find the doormat (source) for the source door.
      '   Determine the nearest (target) door (in another room) to the selected door.
      '   Find the doormat for the target door.
      '   If target doormat is already a tunnel, follow path from the target door to
      '     toward the source door until we move in a direction that would place us in "the void".
      '     We will then use this as the target location.
      '   We will then "dig" a path between the source location and target location.
      '     Attempt to move in the direction of the target location.
      '     If blocked, move toward the left/right of the target location.
      '     If blocked, move in the other direction (left/right).
      '     After moving, attempt (again) to move in the direction of the target.
      '     If blocked, repeat...

      For Each rm In Rooms
        UpdateMap(rm)
      Next

      ' Generate the random start location within one of the rooms at random.

      Dim heroRow As Integer
      Dim heroColumn As Integer

      If True Then

        Dim n = Param.Randomizer.Next(0, Rooms.Count)

        Dim rm = Rooms(n)

        Dim x = Param.Randomizer.Next(1, rm.Width - 1)
        Dim y = Param.Randomizer.Next(1, rm.Height - 1)

        heroRow = rm.ActualMapTopLocation + y
        heroColumn = rm.ActualMapLeftLocation + x

        MapCellData(heroRow, heroColumn) = CellType.Hero

      End If

      ' Generate the random exit location within one of the rooms at random.
      '  Yes, the exit can be in the same room as the hero start; however,
      '  the exit should not (cannot) be in the same position as the hero start.

      If True Then

        Dim n = Param.Randomizer.Next(0, Rooms.Count)

        Dim rm = Rooms(n)

        Do

          Dim x = Param.Randomizer.Next(1, rm.Width - 1)
          Dim y = Param.Randomizer.Next(1, rm.Height - 1)

          Dim r = rm.ActualMapTopLocation + y
          Dim c = rm.ActualMapLeftLocation + x

          If r = heroRow AndAlso c = heroColumn Then
            ' Try again...
          Else
            MapCellData(r, c) = CellType.StructureStairsDown
            Exit Do
          End If

        Loop

      End If

      'show the rooms before we draw tunnels to see what we think should happen
      If Core.Param.IsDebugMode Then
        TryToDisplayCurrentMap()


      End If

      If True Then
        ' Create the tunnels between rooms...
        ' Expects that there are at least two rooms (entry and exit)...
        CreateRandomTunnels()
      End If

      If True Then

        ' Now that we have the rooms... go through each room/region and connect it with another room/region.
        ' 

        ' 2,2 -> 1,2
        ' 2,2 -> 2,3
        ' 2,2 -> 3,2
        ' 2,2 -> 2,1

        ' If room, create random door on facing walls between two rooms.
        '


      End If
      If Core.Param.IsDebugMode Then
        TryToDisplayCurrentMap()

      End If

      If False Then

        'If Me.EntryStairLocation Is Nothing AndAlso
        '   Me.ExitStairLocation Is Nothing Then

        ' These locations can be created by calling NEW with the correct parameters
        ' If not, then randomly select them...
        ' First select a random grid cell...
        ' Then select a random spot within the cell which is not on the edge so it can be within walls...

        Dim seekX = 0
        Dim seekY = Param.Randomizer.Next(0, Param.GridColumnCount * Param.GridRowCount) + 1

        Dim currentX = 0
        Do While currentX < 10 AndAlso seekX = 0
          seekX = Param.Randomizer.Next(0, Param.GridColumnCount * Param.GridRowCount) + 1
          If seekX = seekY Then
            seekX = 0 ' Can not be in the same grid as entry...
          End If
          currentX += 1
        Loop

        If seekX = 0 Then
          ' If could not create random then set to next grid...
          seekX = seekY + 1
          If seekX > Param.GridColumnCount * Param.GridRowCount Then
            seekX = 1
          End If
        End If

        ' Now we have the entry grid in aseekYptr and the exit grid in aseekXptr...
        ' Pick a random location within the grid for the actual stairs making sure 
        ' not to hit an edge so it will not conflict with walls

        'Dim currentY = Param.Randomizer.Next(0, Param.MapGridCellHeight - 2) + 2
        'currentX = Param.Randomizer.Next(0, Param.MapGridCellWidth - 2) + 2

        ' Need to convert local grid coordinates into global map coordinates...

        Dim gridY = 1
        Dim aYPtr = seekY
        Do While aYPtr > Param.GridColumnCount
          If aYPtr > Param.GridColumnCount Then
            gridY += 1
            aYPtr -= Param.GridColumnCount
          End If
        Loop

        If gridY = 0 Then
          gridY = 1
        End If

        Dim gridX = aYPtr ' What is left is the x axis...

        Dim currentY = Param.Randomizer.Next(1, Param.CellHeight(gridY))
        currentX = Param.Randomizer.Next(1, Param.CellWidth(gridX))

        'currentY = ((gridY - 1) * Param.MapGridCellHeight) + currentY
        'currentX = ((gridX - 1) * Param.MapGridCellWidth) + currentX
        currentY = Param.CellStartY(gridY) + currentY
        currentX = Param.CellStartX(gridX) + currentX

        EntryStairLocation = New Coordinate(currentX, currentY) ' Me.GetCoordinateString(currentY, currentX)
        'Me.EntryStairGrid = New Coordinate(gridX, gridY) ' gridY.ToString & gridX.ToString

        ' Pick a random location within the grid for the actual stairs making 
        ' sure not to hit an edge so it will not conflict with walls...

        'currentY = Param.Randomizer.Next(0, Param.MapGridCellHeight - 2) + 2
        'currentX = Param.Randomizer.Next(0, Param.MapGridCellWidth - 2) + 2
        'currentY = Param.Randomizer.Next(0, Param.MapGridCellHeight - 2) + 2
        'currentX = Param.Randomizer.Next(0, Param.MapGridCellWidth - 2) + 2
        currentY = Param.Randomizer.Next(1, Param.CellHeight(gridY))
        currentX = Param.Randomizer.Next(1, Param.CellWidth(gridX))

        ' Need to convert local grid coordinates into global map coordinates...

        gridY = 1
        gridX = 0
        aYPtr = seekX
        Do While aYPtr > Param.GridColumnCount
          If aYPtr > Param.GridColumnCount Then
            gridY += 1
            aYPtr -= Param.GridColumnCount
          End If
        Loop

        If gridY = 0 Then
          gridY = 1
        End If

        gridX = aYPtr ' What is left is the x axis...

        'currentY = ((gridY - 1) * Param.MapGridCellHeight) + currentY
        'currentX = ((gridX - 1) * Param.MapGridCellWidth) + currentX
        currentY = Param.CellStartY(gridY) + currentY
        currentX = Param.CellStartX(gridX) + currentX

        ExitStairLocation = New Coordinate(currentX, currentY) ' Me.GetCoordinateString(currentY, currentX)
        'Me.ExitStairGrid = New Coordinate(gridX, gridY) ' gridY.ToString & gridX.ToString

        'End If

        CreateRandomLevel()

      End If

    End Sub

    'Public Function IsEntryCell(x%, y%) As Boolean
    '  Return Me.MapCellData(y, x) = CellType.Hero 'StructureStairsUp
    'End Function

    'Public Function IsExitCell(x%, y%) As Boolean
    '  Return Me.MapCellData(y, x) = CellType.StructureStairsDown
    'End Function

    Private Shared Function MapCellTypeToChar(type As CellType) As Char
      Select Case type
        Case CellType.Void : Return " "c
        Case CellType.StructureFloor : Return "."c
        Case CellType.StructureTunnel : Return "#"c
        Case CellType.StructureWallTopLeftCorner : Return "["c
        Case CellType.StructureWallTopRightCorner : Return "]"c
        Case CellType.StructureWallTopBottom : Return "-"c
        Case CellType.StructureWallSide : Return "|"c
        Case CellType.StructureWallBottomLeftCorner : Return "{"c
        Case CellType.StructureWallBottomRightCorner : Return "}"c
        'Case CellType.StructureDoorTopBottom : Return "+"c
        'Case CellType.StructureDoorSide : Return "+"c
        Case CellType.StructureDoor : Return "+"c
        Case CellType.StructureStairsDown : Return "="c
        'Case CellType.StructureStairsUp : Return "="c
        Case CellType.SecretHorizontal : Return "/"c
        Case CellType.SecretVertical : Return "\"c
        Case CellType.Hero
          Return "@"c
        Case Else
          Throw New Exception("Unknown tile type.")
      End Select

    End Function

    Public Function ToDungeonLevel() As Level

      Dim result As New Level()

      For light = 0 To 8
        'TESTING:
        Dim isLit = True 'False
        For room = 0 To Rooms.Count - 1
          Dim grid = ((Rooms(room).MapGridRowLocation - 1) * 3) + Rooms(room).MapGridColumnLocation
          If grid = light + 1 Then
            ' This room is at the currently pointed at location in the map grid...
            isLit = Rooms(room).IsLit
            Exit For
          End If
        Next
        result.Lights(light) = isLit
      Next

      For r = 0 To Param.MapHeight - 1
        For c = 0 To Param.MapWidth - 1
          Dim ch = MapCellTypeToChar(MapCellData(r, c))
          result.Map(r, c) = New Tile(ch) With {.Explored = True}
        Next
      Next

      Return result

    End Function

    Private Function CheckIfRandomRoomExists(gridRow%, gridColumn%) As String

      Dim entryRow As Integer '= 0
      Dim entryColumn As Integer '= 0
      Dim entryRowPointer As Integer '= 0
      Dim entryColumnPointer As Integer '= 0
      Dim exitRow As Integer '= 0
      Dim exitColumn As Integer '= 0
      Dim exitRowPointer As Integer '= 0
      Dim exitColumnPointer As Integer '= 0
      'Dim roomRowOffset As Integer '= 0 'offset from top side of cell
      'Dim roomColumnOffset As Integer '= 0 'offset from left side of cell
      Dim isOK As Boolean = False
      'Dim isEntryCell As Boolean '= False ' Default to false and set true if found
      'Dim isExitCell As Boolean '= False ' Default to false and set true if found
      'Dim fromX As Integer '= 0 'These will hold the allowable random range that the top left corner of the room can be created in
      'Dim toX As Integer '= 0
      'Dim fromY As Integer '= 0
      'Dim toY As Integer '= 0
      'Dim aString As String '= ""

      'NOTE; MUST MAKE SURE TO INITIALIZE EntryStairGrid

      entryRow = EntryStairLocation.Y
      entryColumn = EntryStairLocation.X
      exitRow = ExitStairLocation.Y
      exitColumn = ExitStairLocation.X
      entryRowPointer = EntryStairLocation.Y
      entryColumnPointer = EntryStairLocation.X
      exitRowPointer = ExitStairLocation.Y
      exitColumnPointer = ExitStairLocation.X

      ' If a room has an entry or exit point, then the room must surround that point
      ' else it can be anywhere in the grid cell...

      ' Minimum room size if 4x4 so can have border with 2x2 inside...

      'Dim roomWidth = Param.Randomizer.Next(0, Param.MapGridCellWidth) + 2 ' Make sure there is room on the side for a corridor...
      'Dim roomHeight = Param.Randomizer.Next(0, Param.MapGridCellHeight - 2) + 1 ' Top and bottom can only be 6 vice 7 to make room for corridors...
      'If roomHeight < 4 Then
      '  roomHeight = 4 ' Room must have minimum floor space of 2 with wall on either side...
      'End If
      'If roomWidth < 4 Then
      '  roomWidth = 4
      'End If
      'If roomHeight > Param.MapGridCellHeight - 2 Then
      '  roomHeight = Param.MapGridCellHeight - 2
      'End If
      'If roomWidth > Param.MapGridCellWidth - 2 Then
      '  roomWidth = Param.MapGridCellWidth - 2 ' Make sure there is room on the side for a corridor...
      'End If

      Dim result = "" '$"{aString}{roomHeight}-{roomWidth}|"

      If entryRow = gridRow AndAlso entryColumn = gridColumn Then
        result &= CreateRandomEntryRoom(gridRow, gridColumn) ', entryRowPointer, entryColumnPointer)
        isOK = True ' This is the cell for the entry into the level...
      End If

      If exitRow = gridRow AndAlso exitColumn = gridColumn Then
        result &= CreateRandomExitRoom(gridRow, gridColumn) ', exitRowPointer, exitColumnPointer)
        isOK = True ' This is the cell for the exit from the level...
      End If

      If Not isOK Then

        ' If not an entry or exit cell, then randomly choose if room exists...

        Dim randomNumber = Param.Randomizer.Next(0, 100) + 1
        If randomNumber <= Param.MinHasRoomPercentage Then
          result &= CreateRandomRoom(gridRow, gridColumn)
          isOK = True
        End If

      End If

      If Not isOK Then
        result = ""
      End If

      Return result

    End Function

    Private Sub ClearMapData()

      For y = 0 To Param.MapHeight - 1
        For x = 0 To Param.MapWidth - 1
          MapCellData(y, x) = 0
          'Me.MapCellVisibility(y, x) = False
        Next
      Next

    End Sub

    Private Function CreateRandomEntryRoom$(gridRow%, gridColumn%) ', entryRow%, entryColumn%)

      'Dim result As String = ""

      Dim rm = Room.CreateRandomRoom(gridRow, gridColumn)

      If rm.Height > 0 Then

        Rooms.Add(rm)
        'result = rm.ToString

        UpdateMap(rm)

        Dim x = Param.Randomizer.Next(1, rm.Width - 1)
        Dim y = Param.Randomizer.Next(1, rm.Height - 1)

        Dim r = rm.ActualMapTopLocation + y
        Dim c = rm.ActualMapLeftLocation + x

        MapCellData(r, c) = CellType.Hero 'CellType.StructureStairsUp
        'Me.MapCellVisibility(r, c) = Not Me.m_IsFogOfWarActive

      End If

      Return rm.ToString

      ''Dim rm = Room.CreateRandomEntryRoom(gridRow, gridColumn, entryRow, entryColumn)

      'Dim entryX = entryColumn - ((gridColumn - 1) * 26)
      'Dim entryY = entryRow - ((gridRow - 1) * 7)
      'Dim xOff = (entryX - rm.Width) + 1
      'Dim yOff = (entryY - rm.Height) + 1

      'If rm.Height > 0 Then
      '  Me.Rooms.Add(rm)
      '  result = rm.ToString
      'End If

      'Me.UpdateMap(rm)

      'Me.MapCellData(entryRow, entryColumn) = CellType.Hero 'CellType.StructureStairsUp
      'Me.MapCellVisibility(entryRow, entryColumn) = Not Me.m_IsFogOfWarActive

      ''If Me.CurrentMapLevel = 1 Then
      ''  ' On first level place player character initialially beside the stair leading up...
      ''If Me.MapCellData(entryRow, entryColumn - 1) = CellType.StructureFloor Then
      ''  Me.MapCellData(entryRow, entryColumn - 1) = CellType.Hero
      ''Else
      ''  Me.MapCellData(entryRow, entryColumn + 1) = CellType.Hero
      ''End If
      ''End If

      'Return result

    End Function

    Private Function CreateRandomExitRoom$(gridRow%, gridColumn%) ', exitRow%, exitColumn%)

      Dim rm = Room.CreateRandomRoom(gridRow, gridColumn)

      If rm.Height > 0 Then

        Rooms.Add(rm)

        UpdateMap(rm)

        Dim x = Param.Randomizer.Next(1, rm.Width - 1)
        Dim y = Param.Randomizer.Next(1, rm.Height - 1)

        Dim r = rm.ActualMapTopLocation + y
        Dim c = rm.ActualMapLeftLocation + x

        MapCellData(r, c) = CellType.StructureStairsDown
        'Me.MapCellVisibility(r, c) = Not Me.m_IsFogOfWarActive

      End If

      Return rm.ToString

      'Dim rm = Room.CreateRandomExitRoom(gridRow, mapColumn, exitRow, exitColumn)

      'Dim aReturnValue As String = ""
      'Dim X As Integer = 0 ' left to right
      'Dim Y As Integer = 0 ' top to bottom
      'Dim X1 As Integer = 0 ' left to right
      'Dim Y1 As Integer = 0 ' top to bottom
      'Dim exitX As Integer = exitColumn - ((mapColumn - 1) * 26)
      'Dim exitY As Integer = exitRow - ((gridRow - 1) * 7)
      'Dim xOFF As Integer = (exitX - rm.Width) + 1
      'Dim yOFF As Integer = (exitY - rm.Height) + 1

      'If rm.Height > 0 Then
      '  Me.Rooms.Add(rm)
      '  aReturnValue = rm.ToString
      'End If
      'Me.UpdateMap(rm)
      'Me.MapCellData(exitRow, exitColumn) = CellType.StructureStairsDown
      'Me.MapCellVisibility(exitRow, exitColumn) = Not Me.m_IsFogOfWarActive 'TODO testing m_TestMode 'testing true makes all visible

      'Return aReturnValue
    End Function

    ''' <summary>
    ''' Create a new random level
    ''' Replace current level information in properties with randomly generated level.
    ''' 
    ''' 
    ''' Levels are divided into a 3x3 grid, each of which can have or not have a room.
    ''' Rooms, if the exist, must be at least 2x2
    ''' Each wall may have only one door
    ''' Generation controlled by properties:
    ''' CurrentMapLevel = How many levels down the current level is. 
    ''' EntryStairLocation - There must be a room where the stairs come down into this level
    ''' ExitStairLocation - There must be a room where the stairs go down to the next level
    ''' </summary>
    Private Sub CreateRandomLevel()

      Dim m_entryRow As Integer '= 0
      Dim m_entryColumn As Integer '= 0
      Dim m_entryRowPointer As Integer '= 0
      Dim m_entryColumnPointer As Integer ' = 0
      Dim m_exitRow As Integer '= 0
      Dim m_exitColumn As Integer '= 0
      Dim m_exitRowPointer As Integer '= 0
      Dim m_exitColumnPointer As Integer '= 0
      'Dim m_roomHeight As Integer '= 0
      'Dim m_roomWidth As Integer '= 0
      'Dim m_roomRowOffset As Integer '= 0 ' Offset from top side of cell
      'Dim m_roomColumnOffset As Integer '= 0 ' Offset from left side of cell
      Dim m_randomNumber As Integer = Param.Randomizer.Next()
      'Dim m_mapCharacterValue As Integer '= 0
      'Dim m_ISOK As Boolean '= False
      'Dim m_ISEntryCell As Boolean '= False
      'Dim m_ISExitCell As Boolean '= False
      'Dim m_FromX As Integer '= 0 ' These will hold the allowable random range that the top left corner of the room can be created in
      'Dim m_ToX As Integer '= 0
      'Dim m_FromY As Integer '= 0
      'Dim m_ToY As Integer '= 0
      Dim aString As String '= ""

      ClearMapData()

      m_entryRow = EntryStairLocation.Y
      m_entryColumn = EntryStairLocation.X
      m_exitRow = ExitStairLocation.Y
      m_exitColumn = ExitStairLocation.X

      m_entryRowPointer = EntryStairLocation.Y
      m_entryColumnPointer = EntryStairLocation.X
      m_exitRowPointer = ExitStairLocation.Y
      m_exitColumnPointer = ExitStairLocation.X

      For gridRow = 1 To Param.GridRowCount
        For gridColumn = 1 To Param.GridColumnCount
          aString = CheckIfRandomRoomExists(gridRow, gridColumn)
          ' Keep track of which grid cells have rooms for tunnel creation...
          'astring will in the form of height-width|top-left or BLANK if no room exists
          'Me.MapCellHasRoom(gridRow, gridColumn) = aString
        Next
      Next

      Console.ResetColor()

      For y = 0 To Param.MapHeight - 1
        For x = 0 To Param.MapWidth - 1
          Console.SetCursorPosition(x, y + 1)
          Select Case Tile(x, y)
            Case CellType.Void : Console.Write(" "c)
            Case CellType.StructureDoor : Console.Write("+")
            Case CellType.StructureFloor : Console.Write(".")
            Case Else
              Console.Write("=")
          End Select
        Next
      Next

      ' Create the tunnels between rooms...
      ' Expects that there are at least two rooms (entry and exit)...
      CreateRandomTunnels()

    End Sub

    Private Sub CreateRoom(gridRow%, gridColumn%)

      Dim rm = Room.CreateRoom(gridRow, gridColumn)

      If rm.Height > 0 Then
        Rooms.Add(rm)
      Else
        Stop
      End If

    End Sub

    ''' <summary>
    ''' Each room must have at least one door.
    ''' Each room can only have a door facing into the level, no door facing edges.
    ''' There is a percentage chance that any door may be secret
    ''' </summary>
    ''' <param name="rm"></param>
    Private Sub CreateDoors(rm As Room)

      Dim gridRow = rm.MapGridRowLocation
      Dim gridColumn = rm.MapGridColumnLocation

      Dim sx = If(gridColumn = 1, 0, If(gridColumn = 2, 27, 54))
      Dim sy = If(gridRow = 1, 0, If(gridRow = 2, 7, 15))

      Dim y = rm.MapTopLocation
      Dim x = rm.MapLeftLocation

      Dim allowTop As Boolean = False '= gridRow > 1 OrElse (gridRow = 1 AndAlso y > 0)
      Dim allowBottom As Boolean = False '= gridRow < 3 OrElse (gridRow = 3 AndAlso y + (rm.Height) < Param.CellHeight(gridRow))
      Dim allowLeft As Boolean = False '= gridColumn > 1 OrElse (gridColumn = 1 AndAlso x > 0)
      Dim allowRight As Boolean = False ' = gridColumn < 3 OrElse (gridColumn = 3 AndAlso x + (rm.Width) < Param.CellWidth(gridColumn))

      'no doors allowed facing edge of map
      If gridRow = 1 Then
        allowTop = False
        allowBottom = True
      End If

      If gridRow = 2 Then
        allowTop = True
        allowBottom = True
      End If

      If gridRow = 3 Then
        allowTop = True
        allowBottom = False
      End If

      If gridColumn = 1 Then
        allowLeft = False
        allowRight = True
      End If

      If gridColumn = 2 Then
        allowLeft = True
        allowRight = True
      End If

      If gridColumn = 3 Then
        allowLeft = True
        allowRight = False
      End If

      Dim possible As New List(Of Integer)
      If allowTop Then possible.Add(1)
      If allowBottom Then possible.Add(2)
      If allowLeft Then possible.Add(3)
      If allowRight Then possible.Add(4)

      ' Determine how many doors we will create...

      Dim count = Param.Randomizer.Next(1, possible.Count + 1)

      ' Now work to create "count" number of doors...

      For i = 1 To count

        ' Select a random (available) entry from the possible list.

        Dim entry = Param.Randomizer.Next(0, possible.Count)

        ' Determine which location this door will be (top, bottom, left, right)...

        Dim door = possible(entry)

        Dim xoffset = 0
        Dim yoffset = 0

        Dim face As Core.Face

        Select Case door
          Case 1, 2 ' Top, bottom
            face = Core.Face.Top
            xoffset = Param.Randomizer.Next(1, rm.Width - 1)
          Case 3, 4 ' Left, right
            face = Core.Face.Left
            yoffset = Param.Randomizer.Next(1, rm.Height - 1)
          Case Else
            Stop
        End Select

        Select Case door
          Case 2 ' Bottom
            face = Core.Face.Bottom
            yoffset = rm.Height - 1
          Case 4 ' Right
            face = Core.Face.Right
            xoffset = rm.Width - 1
          Case Else
        End Select


        rm.Doors.Add(New Door(sx + x + xoffset, sy + y + yoffset, face))

        possible.RemoveAt(entry)

      Next

    End Sub

    Private Function CreateRandomRoom$(gridRow%, gridColumn%)

      Dim result As String = ""

      Dim rm = Room.CreateRandomRoom(gridRow, gridColumn)

      If rm.Height > 0 Then
        Rooms.Add(rm)
        result = rm.ToString
      End If

      UpdateMap(rm)

      Return result

    End Function

    ''' <summary>
    ''' Make sure each door of each room connects to either another door (preferably 
    ''' in another room) or intersects a tunnel.
    ''' Make sure each room is connected to a least one other room and 
    ''' that every room on the level is accessible.
    ''' HOW TO CHOOSE WHICH DOOR TO GO TO

    '''If there Is a door in the room In the grid Next To the door, Then go To it.
    '''If Not Then
    '''    Try To go To the center room
    '''                If this room Is center room Or already connects To center via another door Then
    '''                                Try To go To Next closest room that Is Not center And Not already connected
    '''    If there is no center room try to go to the next closest room not already connected  
    '''    
    '''HOW TO DIG A TUNNEL
    '''ALWAYS stop if digging And hit tunnel Or door
    '''Dig one To x spaces straight out from all doors initially.
    '''If target door On same row Or column As door coming from Then go straight there.
    '''Else
    '''    If target door Is <5 cells away just dig To it.
    '''                Else choose random distance between 3 spaces And 2/3 way To target room And dig that far
    '''                                turn to door if get to same row Or column
    '''                If past door In direction Of travel Then turn To door
    '''                Else choose random direction between Continue Or turn To door
    '''                If target door Is <5 cells away just dig To it.
    '''                Else choose random distance up To 2/3 way To target room And dig that far
    '''                                turn to door if get to same row Or column
    ''' </summary>
    ''' <returns>
    ''' For each row
    '''   For each column
    '''     look for a door
    '''       if found then extend the tunnel out one or two points then look for door the closets to it to tunnel toward
    '''       (determine differential distance up/down and left/right from current door)
    '''       Until reach other door
    '''         Set random value to determine if tunnel should go left/right or up/down toward destination door 
    '''         (65% toward greatest differential, 30% toward lesser differential and 4% away from those two and the way coming from, 1% stop tunnel
    '''         Tunnel for random distance (3 blocks to 1/2 differential)
    '''       end until
    '''       endif
    '''   next
    ''' next
    ''' When all doors have tunnels, make sure a path is possible to every room from the entry room.
    ''' </returns>
    Private Sub CreateRandomTunnels()

      Dim isRoomFound As Boolean = False
      Dim aFromRoom As New Room
      Dim aConnectionCount As Integer = 0

      'First connect all the doors that lead to adjacent cells
      'then connect remaining doors
      For gridPtr As Integer = 1 To 9
        isRoomFound = False
        aFromRoom = New Room
        aConnectionCount = 0
        For roomPtr As Integer = 0 To Rooms.Count - 1
          If Rooms(roomPtr).GridPosition = gridPtr Then
            aFromRoom = Rooms(roomPtr)
            isRoomFound = True
            Exit For
          End If
        Next
        If isRoomFound = True Then
          aConnectionCount = CreateTunnelsFromRoom(aFromRoom, True)

        End If
      Next

      If Core.Param.IsDebugMode = True Then
        TryToDisplayCurrentMap()
      End If

      For position = 0 To Rooms.Count - 1

        Dim rm = Rooms(position)
        aConnectionCount = CreateTunnelsFromRoom(rm, False)

        ''For d = 0 To rm.Doors.Count - 1

        ''  Dim door = rm.Doors(d)

        ''  Dim mat = FindDoormat(door.X, door.Y)
        ''  If Me.Tile(mat.X, mat.Y) = CellType.StructureTunnel Then
        ''    ' Already dug..
        ''    Continue For
        ''  End If

        ''  Dim startDoorY = door.Y
        ''  Dim startDoorX = door.X

        ''  Dim coord = FindNearestDoor(rm, door, False)

        ''  ' If coord Is Nothing Then Stop

        ''  If coord IsNot Nothing Then

        ''    Dim targetDoorY = coord.Y
        ''    Dim targetDoorX = coord.X

        ''    ' Set to tunnel to start just outside of the from door and 
        ''    ' to end just outside of the target door...

        ''    coord = FindDoormat(startDoorX, startDoorY)

        ''    If coord Is Nothing Then Stop

        ''    If coord IsNot Nothing Then

        ''      Dim startY = coord.Y
        ''      Dim startX = coord.X

        ''      Dig(startX, startY, 0)

        ''      ' Set tunnel to end just outside target door...

        ''      coord = FindDoormat(targetDoorX, targetDoorY)

        ''      If coord Is Nothing Then Stop

        ''      If coord IsNot Nothing Then

        ''        Dim targetY = coord.Y
        ''        Dim targetX = coord.X

        ''        Dim createdStepCount = CreateUtilityTunnel(startX, startY, targetX, targetY)
        ''        If createdStepCount > 0 Then
        ''          ' Add tunnel to room connections...
        ''          coord = rm.GetConnectionNumber(startDoorY, startDoorX, targetDoorY, targetDoorX)
        ''          If coord Is Nothing Then Stop
        ''          RoomConnections.Add(coord)
        ''        Else
        ''          Stop
        ''        End If

        ''      End If

        ''    End If

        ''  End If

        ''Next
      Next

      'now that all tunnels that will be randomly created are in place
      'make sure that each room is accessible somehow.

      If True Then
        VerifyAllRoomsAccessible()
      End If

    End Sub

    Private Function CreateTunnelsFromRoom(ByRef whatRoom As Room, ByVal whatFacingDoorsOnlyFlag As Boolean) As Integer
      Dim aReturnValue As Integer = 0

      For d = 0 To whatRoom.Doors.Count - 1

        Dim door = whatRoom.Doors(d)

        Dim mat = FindDoormat(door.X, door.Y)
        If Me.Tile(mat.X, mat.Y) = CellType.StructureTunnel Then
          ' Already dug..
          Continue For
        End If

        Dim startDoorY = door.Y
        Dim startDoorX = door.X

        Dim coord = FindNearestDoor(whatRoom, door, whatFacingDoorsOnlyFlag)

        ' If coord Is Nothing Then Stop

        If coord IsNot Nothing Then

          Dim targetDoorY = coord.Y
          Dim targetDoorX = coord.X

          ' Set to tunnel to start just outside of the from door and 
          ' to end just outside of the target door...

          coord = FindDoormat(startDoorX, startDoorY)

          If coord Is Nothing Then Stop

          If coord IsNot Nothing Then

            Dim startY = coord.Y
            Dim startX = coord.X

            Dig(startX, startY, 0)

            ' Set tunnel to end just outside target door...

            coord = FindDoormat(targetDoorX, targetDoorY)

            If coord Is Nothing Then Stop

            If coord IsNot Nothing Then

              Dim targetY = coord.Y
              Dim targetX = coord.X

              Dim createdStepCount = CreateUtilityTunnel(startX, startY, targetX, targetY, True)
              If createdStepCount > 0 Then
                ' Add tunnel to room connections...
                coord = whatRoom.GetConnectionNumber(startDoorY, startDoorX, targetDoorY, targetDoorX)
                If coord Is Nothing Then Stop
                RoomConnections.Add(coord)
                aReturnValue += 1
                door.HasConnections = True
              Else
                Stop

              End If

            End If

          End If

        End If
      Next

      Return aReturnValue
    End Function

    Private Enum Direction
      None
      Up
      Down
      Left
      Right
    End Enum

    Private Function OppositeDirection(d As Direction) As Direction
      Select Case d
        Case Direction.Up : Return Direction.Down
        Case Direction.Left : Return Direction.Right
        Case Direction.Right : Return Direction.Left
        Case Direction.Down : Return Direction.Up
        Case Else
          Return Direction.None
      End Select
    End Function

    ''' <summary>
    '''HOW TO DIG A TUNNEL
    '''ALWAYS stop if digging And hit tunnel Or door
    '''Dig one To x spaces straight out from all doors initially.
    '''If target door On same row Or column As door coming from Then go straight there.
    '''Else
    '''    If target door Is <5 cells away just dig To it.
    '''                Else choose random distance between 3 spaces And 2/3 way To target room And dig that far
    '''                                turn to door if get to same row Or column
    '''                If past door In direction Of travel Then turn To door
    '''                Else choose random direction between Continue Or turn To door
    '''                If target door Is <5 cells away just dig To it.
    '''                Else choose random distance up To 2/3 way To target room And dig that far
    '''                                turn to door if get to same row Or column
    ''' </summary>
    ''' <param name="startX%"></param>
    ''' <param name="startY%"></param>
    ''' <param name="targetX%"></param>
    ''' <param name="targetY%"></param>
    ''' <returns></returns>
    Private Function CreateUtilityTunnel(startX%, startY%, targetX%, targetY%, whatStopOnTunnelFlag As Boolean) As Integer
      Dim aReturnValue As Integer = 0
      Dim aStepsInDirectionCtr As Integer = 0
      Dim aRandomTurnPotential As Integer = 0
      Dim aHoldDirection As Direction
      Dim tempX As Integer = 0
      Dim tempY As Integer = 0
      Dim tunnelFound As Boolean = False

      If Core.Param.IsDebugMode = True Then
        Console.SetCursorPosition(startX, startY + 1)
        Console.ForegroundColor = ConsoleColor.Green
        Console.Write($"*")

        Console.SetCursorPosition(targetX, targetY + 1)
        Console.ForegroundColor = ConsoleColor.Red
        Console.Write($"*")

      End If

      Dim cx = startX
      Dim cy = startY

      ' Dim count = 1
      aReturnValue = 1

      Dim previous As Direction = Direction.None
      Dim diffX = cx - targetX 'negative means target is to right and positive means target is to left
      Dim diffY = cy - targetY 'negative means target is to bottom and positive means target is to top
      Dim primary As Direction
      Dim secondary As Direction

      Do
        If (cx = targetX AndAlso cy = targetY) OrElse (tunnelFound = True AndAlso whatStopOnTunnelFlag = True) Then Exit Do
        diffX = cx - targetX 'negative means target is to right and positive means target is to left
        diffY = cy - targetY 'negative means target is to bottom and positive means target is to top

        If diffX = 0 Then
          primary = If(diffY > 0, Direction.Up, Direction.Down)
          secondary = If(diffX > 0, Direction.Left, Direction.Right)
        ElseIf diffY = 0 Then
          primary = If(diffX > 0, Direction.Left, Direction.Right)
          secondary = If(diffY > 0, Direction.Up, Direction.Down)
        ElseIf Math.Abs(diffY) > Math.Abs(diffX) Then
          primary = If(diffY > 0, Direction.Up, Direction.Down)
          secondary = If(diffX > 0, Direction.Left, Direction.Right)
        Else
          primary = If(diffX > 0, Direction.Left, Direction.Right)
          secondary = If(diffY > 0, Direction.Up, Direction.Down)
        End If

        aStepsInDirectionCtr = 1
        'set the maximum distance to travel
        If primary = Direction.Up OrElse primary = Direction.Down Then
          aStepsInDirectionCtr = Math.Abs(diffY)
        Else
          aStepsInDirectionCtr = Math.Abs(diffX)
        End If
        aRandomTurnPotential = Core.Param.Randomizer.Next(1, 100)
        If aRandomTurnPotential < 25 AndAlso aStepsInDirectionCtr > 3 Then
          'if random turn then
          'change primary and secondary and distance to travel
          aHoldDirection = primary
          primary = secondary
          secondary = aHoldDirection
          If aHoldDirection = Direction.Up OrElse aHoldDirection = Direction.Down Then
            aStepsInDirectionCtr = Math.Abs(diffX)
          Else
            aStepsInDirectionCtr = Math.Abs(diffY)
          End If
        End If
        If aStepsInDirectionCtr > 0 Then
          aStepsInDirectionCtr = Core.Param.Randomizer.Next(1, aStepsInDirectionCtr)
        End If

        ''Select Case primary
        ''  Case Direction.Down
        ''    aStepsInDirectionCtr = Core.Param.Randomizer.Next(1, aStepsInDirectionCtr)
        ''  Case Direction.Left
        ''    aStepsInDirectionCtr = Core.Param.Randomizer.Next(1, aStepsInDirectionCtr)
        ''  Case Direction.Right
        ''    aStepsInDirectionCtr = Core.Param.Randomizer.Next(1, aStepsInDirectionCtr)
        ''  Case Direction.Up
        ''    aStepsInDirectionCtr = Core.Param.Randomizer.Next(1, aStepsInDirectionCtr)
        ''End Select

        Do
          If aStepsInDirectionCtr < 1 Then Exit Do
          If (cx = targetX AndAlso cy = targetY) OrElse (tunnelFound = True AndAlso whatStopOnTunnelFlag = True) Then Exit Do

          Dim possible = DigDirections(cx, cy)

          For index = possible.Count - 1 To 0 Step -1
            If possible(index) = OppositeDirection(previous) Then
              possible.RemoveAt(index)
              Exit For
            End If
          Next

          If possible.Contains(primary) Then
            Select Case primary
              Case Direction.Up : cy -= 1
              Case Direction.Down : cy += 1
              Case Direction.Left : cx -= 1
              Case Direction.Right : cx += 1
            End Select
            previous = primary
          ElseIf possible.Contains(secondary) Then
            Select Case secondary
              Case Direction.Up : cy -= 1
              Case Direction.Down : cy += 1
              Case Direction.Left : cx -= 1
              Case Direction.Right : cx += 1
            End Select
            previous = secondary
          ElseIf possible.Contains(previous) Then
            Select Case previous
              Case Direction.Up : cy -= 1
              Case Direction.Down : cy += 1
              Case Direction.Left : cx -= 1
              Case Direction.Right : cx += 1
            End Select
          Else
            '  Stop
            'Exit Do
            tempX = cx
            tempY = cy
            'If Me.Tile(cx, cy) = CellType.Unknown Then

            'End If
            Select Case primary
              Case Direction.Up : tempY -= 1
              Case Direction.Down : tempY += 1
              Case Direction.Left : tempX -= 1
              Case Direction.Right : tempX += 1
            End Select
            If (Me.Tile(tempX, tempY) = CellType.StructureTunnel AndAlso whatStopOnTunnelFlag = True) OrElse Me.Tile(tempX, tempY) = CellType.StructureFloor OrElse Me.Tile(tempX, tempY) = CellType.Unknown Then
              aStepsInDirectionCtr = 0
              tunnelFound = True
            Else
              tempX = cx
              tempY = cy
              Select Case secondary
                Case Direction.Up : tempY -= 1
                Case Direction.Down : tempY += 1
                Case Direction.Left : tempX -= 1
                Case Direction.Right : tempX += 1
              End Select
              If tempX >= 0 AndAlso tempX < Core.Param.MapWidth AndAlso tempY >= 0 AndAlso tempY < Core.Param.MapHeight Then

                If (Me.Tile(tempX, tempY) = CellType.StructureTunnel AndAlso whatStopOnTunnelFlag = True) OrElse Me.Tile(tempX, tempY) = CellType.StructureFloor OrElse Me.Tile(tempX, tempY) = CellType.Unknown Then
                  aStepsInDirectionCtr = 0
                  tunnelFound = True
                  Exit Do
                Else
                  aReturnValue = aReturnValue
                End If
              Else
                aStepsInDirectionCtr = 0
                tunnelFound = True
                Exit Do
              End If
            End If
          End If

          If (Me.Tile(cx, cy) = CellType.StructureTunnel AndAlso whatStopOnTunnelFlag = True) OrElse Me.Tile(tempX, tempY) = CellType.Unknown Then
            aStepsInDirectionCtr = 0
            tunnelFound = True
            Exit Do
          Else
            aReturnValue = aReturnValue
          End If

          Dig(cx, cy, CellType.StructureTunnel)
          'If Core.Param.isDebugMode = True Then
          '  Console.ReadLine()
          'End If
          'look to see if next to a door so stop
          tempX = cx - 1 'left
          tempY = cy
          If tempX >= 0 AndAlso tempX < Core.Param.MapWidth - 1 Then
            If tempY >= 0 AndAlso tempY < Core.Param.MapHeight - 1 Then
              If Me.Tile(tempX, tempY) = CellType.StructureDoor OrElse Me.Tile(tempX, tempY) = CellType.SecretHorizontal OrElse Me.Tile(tempX, tempY) = CellType.SecretVertical Then
                aStepsInDirectionCtr = 0
                tunnelFound = True
                cx = targetX
                cy = targetY
                Exit Do
              End If
            End If
          End If
          tempX = cx + 1 'right
          tempY = cy
          If tempX >= 0 AndAlso tempX < Core.Param.MapWidth - 1 Then
            If tempY >= 0 AndAlso tempY < Core.Param.MapHeight - 1 Then
              If Me.Tile(tempX, tempY) = CellType.StructureDoor OrElse Me.Tile(tempX, tempY) = CellType.SecretHorizontal OrElse Me.Tile(tempX, tempY) = CellType.SecretVertical Then
                aStepsInDirectionCtr = 0
                tunnelFound = True
                cx = targetX
                cy = targetY
                Exit Do
              End If
            End If
          End If
          tempX = cx
          tempY = cy - 1 'up
          If tempX >= 0 AndAlso tempX < Core.Param.MapWidth - 1 Then
            If tempY >= 0 AndAlso tempY < Core.Param.MapHeight - 1 Then
              If Me.Tile(tempX, tempY) = CellType.StructureDoor OrElse Me.Tile(tempX, tempY) = CellType.SecretHorizontal OrElse Me.Tile(tempX, tempY) = CellType.SecretVertical Then
                aStepsInDirectionCtr = 0
                tunnelFound = True
                cx = targetX
                cy = targetY
                Exit Do
              End If
            End If
          End If
          tempX = cx
          tempY = cy + 1 'down
          If tempX >= 0 AndAlso tempX < Core.Param.MapWidth - 1 Then
            If tempY >= 0 AndAlso tempY < Core.Param.MapHeight - 1 Then
              If Me.Tile(tempX, tempY) = CellType.StructureDoor OrElse Me.Tile(tempX, tempY) = CellType.SecretHorizontal OrElse Me.Tile(tempX, tempY) = CellType.SecretVertical Then
                aStepsInDirectionCtr = 0
                tunnelFound = True
                cx = targetX
                cy = targetY
                Exit Do
              End If
            End If
          End If
          aStepsInDirectionCtr -= 1
          aReturnValue += 1

        Loop
      Loop
      If Core.Param.IsDebugMode = True Then
        Console.ResetColor()

        Console.SetCursorPosition(startX, startY + 1)
        Console.Write($"#")

        Console.SetCursorPosition(targetX, targetY + 1)
        Console.Write($"#")
      End If


      Return aReturnValue


      ''Dim result As Integer = 0

      '''Dim aRoom As New Room

      ''Dim aStepsInDirectionCtr As Integer = 0
      ''Dim aSeekColumn As Integer = 0
      ''Dim aSeekRow As Integer = 0

      ''Dim horizontalDifference = startX - targetX
      ''Dim verticalDifference = startY - targetY

      ''Dim tunnelDirection As Direction
      ''Dim alternateDirection As Direction

      ''If Math.Abs(verticalDifference) > Math.Abs(horizontalDifference) Then
      ''  If horizontalDifference > 0 Then
      ''    tunnelDirection = Direction.Left
      ''  Else
      ''    tunnelDirection = Direction.Right
      ''  End If
      ''  If verticalDifference > 0 Then
      ''    alternateDirection = Direction.Up
      ''  Else
      ''    alternateDirection = Direction.Down
      ''  End If
      ''Else
      ''  If verticalDifference > 0 Then
      ''    tunnelDirection = Direction.Up
      ''  Else
      ''    tunnelDirection = Direction.Down
      ''  End If
      ''  If horizontalDifference > 0 Then
      ''    alternateDirection = Direction.Left
      ''  Else
      ''    alternateDirection = Direction.Right
      ''  End If
      ''End If

      ''' We need to tunnel in a particular (primary) direction.
      ''' If we hit an obsticle, we need to tunnel into a different (secondary) direction.
      ''' Each dig into the alternate direction, need to try to tunnel into the primary direction.

      ''Dim aCurrentRow = startY
      ''Dim aCurrentColumn = startX

      ''For result = 1 To (Param.MapHeight * Param.MapWidth)

      ''  Dim currentData = DigDirections(aCurrentColumn, aCurrentRow)

      ''  If aCurrentRow = targetY Then
      ''    If aCurrentColumn - targetX > 0 Then
      ''      tunnelDirection = Direction.Left
      ''    Else
      ''      tunnelDirection = Direction.Right
      ''    End If
      ''  Else
      ''    If aCurrentColumn = targetX Then
      ''      If aCurrentRow - targetY > 0 Then
      ''        tunnelDirection = Direction.Up
      ''      Else
      ''        tunnelDirection = Direction.Down
      ''      End If
      ''    End If
      ''  End If
      ''  If currentData.Contains(tunnelDirection) Then
      ''    Select Case tunnelDirection
      ''      Case Direction.Up
      ''        aSeekRow = aCurrentRow - 1
      ''        aSeekColumn = aCurrentColumn
      ''      Case Direction.Down
      ''        aSeekRow = aCurrentRow + 1
      ''        aSeekColumn = aCurrentColumn
      ''      Case Direction.Left
      ''        aSeekRow = aCurrentRow
      ''        aSeekColumn = aCurrentColumn - 1
      ''      Case Direction.Right
      ''        aSeekRow = aCurrentRow
      ''        aSeekColumn = aCurrentColumn + 1
      ''    End Select
      ''  Else
      ''    If tunnelDirection = alternateDirection Then
      ''      'If 1 = 0 Then
      ''      'need to tunnel around object
      ''      Dim coord = TunnelAroundObstacle(aCurrentRow, aCurrentColumn, tunnelDirection)
      ''      If coord IsNot Nothing Then
      ''        aSeekRow = coord.Y
      ''        aSeekColumn = coord.X
      ''      End If
      ''      aCurrentColumn = aSeekColumn
      ''      aCurrentRow = aSeekRow
      ''      'End If
      ''    Else
      ''      'TODO: Getting stuck here when it hits a wall where the 
      ''      ' door is literally just around the corner...
      ''      If currentData.Contains(alternateDirection) Then
      ''        Select Case alternateDirection
      ''          Case Direction.Up
      ''            aSeekRow = aCurrentRow - 1
      ''            aSeekColumn = aCurrentColumn
      ''          Case Direction.Down
      ''            aSeekRow = aCurrentRow + 1
      ''            aSeekColumn = aCurrentColumn
      ''          Case Direction.Left
      ''            aSeekRow = aCurrentRow
      ''            aSeekColumn = aCurrentColumn - 1
      ''          Case Direction.Right
      ''            aSeekRow = aCurrentRow
      ''            aSeekColumn = aCurrentColumn + 1
      ''        End Select
      ''      Else
      ''        'stuck
      ''        Exit For
      ''      End If
      ''    End If
      ''  End If

      ''  Dig(aSeekColumn, aSeekRow, 1)

      ''  aCurrentColumn = aSeekColumn
      ''  aCurrentRow = aSeekRow

      ''  If aCurrentRow = targetY AndAlso aCurrentColumn = targetX Then
      ''    Exit For
      ''  End If
      ''  If aCurrentRow = targetY OrElse aCurrentColumn = targetX Then
      ''    aStepsInDirectionCtr = 5
      ''  End If
      ''  'every X steps can change direction
      ''  aStepsInDirectionCtr += 1
      ''  If aStepsInDirectionCtr > 5 Then
      ''    aStepsInDirectionCtr = 0
      ''    horizontalDifference = aCurrentColumn - targetX
      ''    verticalDifference = aCurrentRow - targetY
      ''    If horizontalDifference = 0 Then
      ''      If verticalDifference > 0 Then
      ''        tunnelDirection = Direction.Up
      ''      Else
      ''        tunnelDirection = Direction.Down
      ''      End If
      ''    Else
      ''      If verticalDifference = 0 Then
      ''        If horizontalDifference > 0 Then
      ''          tunnelDirection = Direction.Left
      ''        Else
      ''          tunnelDirection = Direction.Right
      ''        End If
      ''      Else
      ''        If Math.Abs(verticalDifference) > Math.Abs(horizontalDifference) Then
      ''          'if up-down farther then start left right
      ''          If horizontalDifference > 0 Then
      ''            tunnelDirection = Direction.Left
      ''          Else
      ''            tunnelDirection = Direction.Right
      ''          End If
      ''          If verticalDifference > 0 Then
      ''            alternateDirection = Direction.Up
      ''          Else
      ''            alternateDirection = Direction.Down
      ''          End If
      ''        Else
      ''          If verticalDifference > 0 Then
      ''            tunnelDirection = Direction.Up
      ''          Else
      ''            tunnelDirection = Direction.Down
      ''          End If
      ''          If horizontalDifference > 0 Then
      ''            alternateDirection = Direction.Left
      ''          Else
      ''            alternateDirection = Direction.Right
      ''          End If
      ''        End If
      ''      End If
      ''    End If
      ''  End If
      ''Next

      ''Return result

    End Function

    Private Sub TryToDisplayCurrentMap()

      ' The following code will draw what we've done thus far.
      'TODO this needs to be moved out of here to display program

      Console.ResetColor()

      For y = 0 To Param.MapHeight - 1
        For x = 0 To Param.MapWidth - 1

          Dim ch As Char '= "?"c
          Dim fg = ConsoleColor.Gray

          Select Case Tile(x, y)
            Case CellType.Void : ch = " "c
            Case CellType.Hero : ch = "@"c : fg = ConsoleColor.Yellow
            Case CellType.StructureDoor : ch = "+"c : fg = ConsoleColor.White
            Case CellType.StructureFloor : ch = "."c : fg = ConsoleColor.DarkGreen
            Case CellType.StructureWallTopLeftCorner : ch = "["c : fg = ConsoleColor.DarkYellow
            Case CellType.StructureWallTopRightCorner : ch = "]"c : fg = ConsoleColor.DarkYellow
            Case CellType.StructureWallTopBottom : ch = "-"c : fg = ConsoleColor.DarkYellow
            Case CellType.StructureWallSide : ch = "|"c : fg = ConsoleColor.DarkYellow
            Case CellType.StructureWallBottomLeftCorner : ch = "{"c : fg = ConsoleColor.DarkYellow
            Case CellType.StructureWallBottomRightCorner : ch = "}"c : fg = ConsoleColor.DarkYellow
            Case CellType.StructureTunnel : ch = "#"c
            Case CellType.StructureStairsDown : ch = "="c : fg = ConsoleColor.Green
            Case Else
              ch = "?"c : fg = ConsoleColor.Red
          End Select

          Console.SetCursorPosition(x, y + 1)
          Console.ForegroundColor = fg
          Console.Write(ch)

        Next
      Next


      Console.ReadLine()

    End Sub
    Private Property Tile(x%, y%) As CellType
      Get

        If Not x.Between(0, Param.MapWidth - 1) Then
          Throw New ArgumentOutOfRangeException("x")
        End If

        If Not y.Between(0, Param.MapHeight - 1) Then
          Throw New ArgumentOutOfRangeException("y")
        End If

        Return MapCellData(y, x)

      End Get
      Set(value As CellType)

        If Not x.Between(0, Param.MapWidth - 1) Then
          Throw New ArgumentOutOfRangeException("x")
        End If

        If Not y.Between(0, Param.MapHeight - 1) Then
          Throw New ArgumentOutOfRangeException("y")
        End If

        MapCellData(y, x) = value

      End Set
    End Property

    ''' <summary>
    ''' Determine which directions (0 or more) are available for digging a tunnel.
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns>A list containing one or more up, down, left and right directions.</returns>
    Private Function DigDirections(x%, y%) As List(Of Direction)

      If Not x.Between(0, Param.MapWidth - 1) Then
        Throw New ArgumentOutOfRangeException("x")
      End If

      If Not y.Between(0, Param.MapHeight - 1) Then
        Throw New ArgumentOutOfRangeException("y")
      End If

      Dim result = New List(Of Direction)

      Dim modifiers = {ModifierUp,
                                   ModifierDown,
                                   ModifierLeft,
                                   ModifierRight}

      For Each modifier In modifiers
        Dim seekX = x + modifier.X
        Dim seekY = y + modifier.Y
        If seekX.Between(0, Param.MapWidth - 1) AndAlso
                           seekY.Between(0, Param.MapHeight - 1) Then
          Select Case Tile(seekX, seekY)
            Case CellType.Void, CellType.StructureTunnel, CellType.Unknown
              Select Case modifier
                Case ModifierUp : result.Add(Direction.Up)
                Case ModifierDown : result.Add(Direction.Down)
                Case ModifierLeft : result.Add(Direction.Left)
                Case ModifierRight : result.Add(Direction.Right)
                Case Else
                  Stop
              End Select
            Case Else
          End Select
        End If
      Next

      Return result

    End Function

    Private Sub Dig(x%, y%, type%)

      If Not x.Between(0, Param.MapWidth - 1) Then
        Throw New ArgumentOutOfRangeException("x")
      End If

      If Not y.Between(0, Param.MapHeight - 1) Then
        Throw New ArgumentOutOfRangeException("y")
      End If

      Tile(x, y) = CellType.StructureTunnel

      If Debugger.IsAttached Then

        Console.ResetColor()

        Select Case type
          Case 0 : Console.ForegroundColor = ConsoleColor.DarkGreen
          Case 1 : Console.ForegroundColor = ConsoleColor.Gray
          Case 2 : Console.ForegroundColor = ConsoleColor.DarkRed
          Case Else
            Stop
        End Select

        Console.SetCursorPosition(x, y + 1)

        SpinnerIndex += 1 : If SpinnerIndex > SpinnerChar.Length - 1 Then SpinnerIndex = 0
        Console.Write(SpinnerChar(SpinnerIndex))

        Threading.Thread.Sleep(100)
        Console.SetCursorPosition(x, y + 1)
        Console.Write("#"c)

      End If

    End Sub

    'Private SpinnerChar As String = "\|/-"
    Private ReadOnly SpinnerChar As String = "|+|-+-"
    Private SpinnerIndex As Integer = 0

    Private Function FindDoormat(x%, y%) As Coordinate

      If Not x.Between(0, Param.MapWidth - 1) Then
        Throw New ArgumentOutOfRangeException("x")
      End If

      If Not y.Between(0, Param.MapHeight - 1) Then
        Throw New ArgumentOutOfRangeException("y")
      End If

      Dim modifiers = {ModifierLeft,
                                   ModifierUp,
                                   ModifierRight,
                                   ModifierDown}

      For Each modifier In modifiers
        Dim seekX = x + modifier.X
        Dim seekY = y + modifier.Y
        If seekX.Between(0, Param.MapWidth - 1) AndAlso
                           seekY.Between(0, Param.MapHeight - 1) Then
          Select Case Tile(seekX, seekY)
            Case CellType.Void, CellType.StructureTunnel
              Return New Coordinate(seekX, seekY)
            Case Else
          End Select
        End If
      Next

      Return Nothing

    End Function

    ''' <summary>
    ''' Try to find a door to tunnel to from the room and door provided
    ''' Try to find a next in one of the grid cells next to this one
    ''' else just find one
    ''' HOW TO CHOOSE WHICH DOOR TO GO TO

    '''If there Is a door in the room In the grid Next To the door, Then go To it.
    '''If Not Then
    '''    Try To go To the center room
    '''                If this room Is center room Or already connects To center via another door Then
    '''                                Try To go To Next closest room that Is Not center And Not already connected
    '''    If there is no center room try to go to the next closest room not already connected  
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <param name="coord"></param>
    ''' <returns></returns>
    Private Function FindNearestDoor(whatRoom As Room, whatDoor As Door, ByVal whatFacingDoorsOnlyFlag As Boolean) As Coordinate
      Dim aReturnValue As New Coordinate(0, 0)
      Dim aReturnString As String = ""
      Dim aTargetRoom As New Room 'the room with the door to go to
      Dim aTargetDoor As New Door(0, 0, 0) 'the door to go to
      Dim aTargetDoorNumber As Integer = 0 ' the door number within the target room to go to
      Dim aTargetGridCell As Integer = 0 'the cell where the door to go to will be found
      Dim isDoorFound As Boolean = False
      Dim aConnection As String = ""
      Dim isFound As Boolean = False

      ' Given an x/y find the nearest door not part of the same room.

      'Only look for new connection from this door if it is not already connected
      If whatDoor.HasConnections = False Then
        'Find out which grid cell the door is pointing to
        ' so we can see if there Is a room in that grid with a door on our side
        If whatDoor.Face = Face.Top AndAlso whatRoom.MapGridRowLocation > 1 Then
          aTargetGridCell = whatRoom.GridPosition - Core.Param.GridColumnCount
        End If
        If whatDoor.Face = Face.Bottom AndAlso whatRoom.MapGridRowLocation < Core.Param.GridRowCount Then
          aTargetGridCell = whatRoom.GridPosition + Core.Param.GridColumnCount
        End If
        If whatDoor.Face = Face.Left AndAlso whatRoom.MapGridColumnLocation > 1 Then
          aTargetGridCell = whatRoom.GridPosition - 1
        End If
        If whatDoor.Face = Face.Right AndAlso whatRoom.MapGridColumnLocation < Core.Param.GridColumnCount Then
          aTargetGridCell = whatRoom.GridPosition + 1
        End If

        'STEP 1
        'now see if there is a room in the targetGridCell
        For Each foundRoom As Room In Rooms
          If foundRoom.GridPosition = aTargetGridCell Then
            'see if there is a door pointing at us to go to
            aTargetRoom = foundRoom
            isDoorFound = False
            Select Case whatDoor.Face
              Case Face.Top
                For Each foundDoor As Door In aTargetRoom.Doors
                  If foundDoor.Face = Face.Bottom Then
                    aTargetDoor = foundDoor
                    isDoorFound = True
                    Exit For
                  End If
                Next
              Case Face.Bottom
                For Each foundDoor As Door In aTargetRoom.Doors
                  If foundDoor.Face = Face.Top Then
                    aTargetDoor = foundDoor
                    isDoorFound = True
                    Exit For
                  End If
                Next
              Case Face.Left
                For Each foundDoor As Door In aTargetRoom.Doors
                  If foundDoor.Face = Face.Right Then
                    aTargetDoor = foundDoor
                    isDoorFound = True
                    Exit For
                  End If
                Next
              Case Face.Right
                For Each foundDoor As Door In aTargetRoom.Doors
                  If foundDoor.Face = Face.Left Then
                    aTargetDoor = foundDoor
                    isDoorFound = True
                    Exit For
                  End If
                Next
            End Select
            Exit For
          End If
        Next
        'now if isDoorFound=true then aTargetRoom is the room to go to
        'else try to go to center room
        If isDoorFound = True Then
          aReturnValue = New Coordinate(aTargetDoor.X, aTargetDoor.Y)
          whatDoor.HasConnections = True
          aTargetDoor.HasConnections = True
          Dim connectionCoord As New Coordinate(whatRoom.GridPosition, aTargetRoom.GridPosition)
          RoomConnections.Add(connectionCoord)

          whatRoom.Connections.Add(whatRoom.GridPosition.ToString & "|" & aTargetRoom.GridPosition.ToString)
        Else

          If isDoorFound = False AndAlso whatFacingDoorsOnlyFlag = False Then
            'continue to look for connection
            'STEP 2
            'if whatroom is not center room then try to go to center room
            If Not whatRoom.GridPosition = 5 Then
              aTargetGridCell = 5
              aTargetRoom = New Room()

              For Each foundRoom As Room In Rooms
                If foundRoom.GridPosition = aTargetGridCell Then
                  'see if there is a door pointing at us to go 
                  aConnection = whatRoom.GridPosition.ToString & "|" & foundRoom.GridPosition.ToString
                  isFound = whatRoom.IsRoomConnected(foundRoom)
                  If isFound = False Then
                    aTargetRoom = foundRoom
                  End If
                  Exit For
                End If
              Next
              'now if atargetroom.gridposition=5 then one was found
              'else there is no center room or we are already connected to it
              If aTargetRoom.GridPosition = 5 Then
                'go to center room
                'We know that targetRoom does not have a door facing the From Door
                '  because it would have been found in the step1 above
                'So target room is diagonally away from the From Door
                '  or the From Door needs to go to a door on either a side or the wall facing away from the From Room
                aReturnValue = New Coordinate(aTargetRoom.Doors(0).X, aTargetRoom.Doors(0).Y)
                isDoorFound = True


              End If

              'If no door found with center room
              'then just connect to closest room
              'If isDoorFound = False Then

              'just fall through to original method
              'End If

            End If

          End If

          If isDoorFound = False Then
            aReturnValue = Nothing
          End If
        End If

      Else
        aReturnValue = Nothing

      End If


      'Return aReturnValue














      If aReturnValue Is Nothing AndAlso whatFacingDoorsOnlyFlag = False Then
        Dim possible As Coordinate = Nothing
        Dim isAlreadyConnected As Boolean = False
        Dim pdx = Integer.MaxValue \ 2
        Dim pdy = Integer.MaxValue \ 2
        Dim possibleList As New List(Of Coordinate)

        'try to connect to a room in the next gridcell over first
        For Each r In Rooms
          isAlreadyConnected = whatRoom.IsRoomConnected(r)
          If r IsNot whatRoom AndAlso Not isAlreadyConnected Then
            'see if r is only one grid cell away
            isFound = False
            If Math.Abs(whatRoom.MapGridRowLocation - r.MapGridRowLocation) <= 1 Then
              If Math.Abs(whatRoom.MapGridColumnLocation - r.MapGridColumnLocation) <= 1 Then
                isFound = True
              End If
            End If
            If isFound = True Then
              For Each d In r.Doors
                Dim dx = Math.Abs(whatDoor.X - d.X)
                Dim dy = Math.Abs(whatDoor.Y - d.Y)
                If dx + dy < pdx + pdy Then
                  pdx = dx
                  pdy = dy
                  Dim newpossible As Coordinate = Nothing
                  newpossible = New Coordinate(d.X, d.Y)
                  possibleList.Add(newpossible)
                End If
              Next

            End If
          End If
        Next

        'if no connections made then try all rooms
        If possibleList.Count = 0 Then
          For Each r In Rooms
            isAlreadyConnected = whatRoom.IsRoomConnected(r)
            If r IsNot whatRoom AndAlso Not isAlreadyConnected Then
              For Each d In r.Doors
                Dim dx = Math.Abs(whatDoor.X - d.X)
                Dim dy = Math.Abs(whatDoor.Y - d.Y)
                If dx + dy < pdx + pdy Then
                  pdx = dx
                  pdy = dy
                  Dim newpossible As Coordinate = Nothing
                  newpossible = New Coordinate(d.X, d.Y)
                  possibleList.Add(newpossible)
                End If
              Next
            End If
          Next

        End If

        ''If possibleList.Count > 0 Then
        ''  Dim returnPtr As Integer = 0
        ''  returnPtr = Core.Param.Randomizer.Next(1, possibleList.Count)
        ''  If returnPtr > 0 Then
        ''    possible = possibleList(returnPtr - 1)
        ''  End If
        ''  Return possible
        ''End If

        'look for closest door to go to
        'that is not in same room
        If possibleList.Count > 0 Then
          Dim returnPtr As Integer = 0
          possible = possibleList(returnPtr)
          For returnPtr = 0 To possibleList.Count - 1
            If returnPtr > 0 Then
              Dim dx = Math.Abs(whatDoor.X - possible.X)
              Dim dy = Math.Abs(whatDoor.Y - possible.Y)
              Dim px = Math.Abs(whatDoor.X - possibleList(returnPtr).X)
              Dim py = Math.Abs(whatDoor.Y - possibleList(returnPtr).Y)
              If (px + py) <= (dx + dy) Then
                Dim gPtr As Integer = GetRoomNumberFromXY(possibleList(returnPtr).X, possibleList(returnPtr).Y)
                If Not whatRoom.GridPosition = gPtr Then
                  possible = possibleList(returnPtr) 'not in same grid aka room
                  'could also check that another door from whatRoom is not already connected to this one
                  'as future enhancement
                End If
              End If
            End If
          Next
          Return possible
        End If

        Dim foundList As New List(Of Room)

        For gridRow = 1 To Param.GridRowCount

          For gridColumn = 1 To Param.GridColumnCount

            Dim fromPosition = Param.GridPositionToIndex(gridColumn, gridRow)

            If fromPosition = whatRoom.GridPosition Then

              If gridRow > 1 Then

                Dim toPosition = Param.GridPositionToIndex(gridColumn, gridRow - 1)

                For r = 0 To Rooms.Count - 1

                  If toPosition = Rooms(r).GridPosition Then

                    Dim connection = New Coordinate(whatRoom.GridPosition, Rooms(r).GridPosition)
                    Dim reverseConnection = New Coordinate(Rooms(r).GridPosition, whatRoom.GridPosition)
                    'Dim 
                    isFound = False

                    For c = 0 To RoomConnections.Count - 1
                      If connection = RoomConnections(c) OrElse
                                                                 reverseConnection = RoomConnections(c) Then
                        isFound = True
                        Exit For
                      End If
                    Next

                    If Not isFound Then
                      ' Do not use room if already has a connection...
                      foundList.Add(Rooms(r))
                    End If

                    Exit For

                  End If

                Next

              End If

              If gridRow < Param.GridRowCount Then

                Dim toPosition = Param.GridPositionToIndex(gridColumn, gridRow + 1)

                For r = 0 To Rooms.Count - 1

                  If toPosition = Rooms(r).GridPosition Then

                    Dim connection = New Coordinate(whatRoom.GridPosition, Rooms(r).GridPosition)
                    Dim reverseConnection = New Coordinate(Rooms(r).GridPosition, whatRoom.GridPosition)
                    'Dim 
                    isFound = False

                    For c = 0 To RoomConnections.Count - 1
                      If connection = RoomConnections(c) OrElse
                                                                 reverseConnection = RoomConnections(c) Then
                        isFound = True
                        Exit For
                      End If
                    Next

                    If Not isFound Then
                      ' Do not use room if already has a connection...
                      foundList.Add(Rooms(r))
                    End If

                    Exit For

                  End If

                Next

              End If

              If gridColumn > 1 Then

                Dim toPosition = Param.GridPositionToIndex(gridColumn - 1, gridRow)

                For r = 0 To Rooms.Count - 1

                  If toPosition = Rooms(r).GridPosition Then

                    Dim connection = New Coordinate(whatRoom.GridPosition, Rooms(r).GridPosition)
                    Dim reverseConnection = New Coordinate(Rooms(r).GridPosition, whatRoom.GridPosition)
                    'Dim 
                    isFound = False

                    For c As Integer = 0 To RoomConnections.Count - 1
                      If connection = RoomConnections(c) OrElse
                                                                 reverseConnection = RoomConnections(c) Then
                        isFound = True
                        Exit For
                      End If
                    Next

                    If Not isFound Then
                      ' Do not use room if already has a connection...
                      foundList.Add(Rooms(r))
                    End If

                    Exit For

                  End If

                Next

              End If

              If gridColumn < Param.GridColumnCount Then

                Dim toPosition = Param.GridPositionToIndex(gridColumn + 1, gridRow)

                For r = 0 To Rooms.Count - 1

                  If toPosition = Rooms(r).GridPosition Then

                    Dim connection = New Coordinate(whatRoom.GridPosition, Rooms(r).GridPosition)
                    Dim reverseConnection = New Coordinate(Rooms(r).GridPosition, whatRoom.GridPosition)
                    'Dim 
                    isFound = False

                    For connect = 0 To RoomConnections.Count - 1
                      If connection = RoomConnections(connect) OrElse
                                                                 reverseConnection = RoomConnections(connect) Then
                        isFound = True
                        Exit For
                      End If
                    Next

                    If Not isFound Then
                      ' Do not use room if already has a connection...
                      foundList.Add(Rooms(r))
                    End If

                    Exit For

                  End If

                Next

              End If

            End If

          Next

        Next

        ' Now we know what ways we can look...
        If foundList.Count > 0 Then
          Dim toPosition = Param.Randomizer.Next(foundList.Count) '+ 1 do not need +1 because list is zero based
          'Return
          aReturnValue = foundList(toPosition).Doors(0)
        Else
          ' Need to pick any random room...
          For pass = 0 To 10
            Dim toPosition = Param.Randomizer.Next(Rooms.Count)
            If Not Rooms(toPosition).GridPosition = whatRoom.GridPosition Then
              'Return
              aReturnValue = Rooms(toPosition).Doors(0)
            End If
          Next
        End If

        Return Nothing

      End If

      Return aReturnValue
    End Function

    'Private Function GetCoordinateString$(row%, column%)

    '  Dim currentData = $"0{row}"
    '  Dim aReturnValue = currentData.Substring(currentData.Length - 2)
    '  currentData = $"0{column}"
    '  aReturnValue &= currentData.Substring(currentData.Length - 2)
    '  Return aReturnValue

    'End Function

    'Private Function GetRoom(number%) As Room

    '  If Not number.Between(1, Param.GridRowCount * Param.GridColumnCount) Then
    '    Throw New ArgumentOutOfRangeException("number")
    '  End If

    '  For Each rm In Rooms
    '    If rm.GridPosition = number Then
    '      Return rm
    '    End If
    '  Next

    '  Return Nothing

    'End Function

    Private Function GetRoom(x%, y%) As Room

      If Not x.Between(0, Param.MapWidth - 1) Then
        Throw New ArgumentOutOfRangeException("x")
      End If

      If Not y.Between(0, Param.MapHeight - 1) Then
        Throw New ArgumentOutOfRangeException("y")
      End If

      Try

        Dim row = Param.GridRow(y)
        Dim col = Param.GridColumn(x)

        If row = 0 OrElse col = 0 Then
          ' Coords provided do not clearly identify a zone.
          Return Nothing
        End If

        Dim number = ((row - 1) * Param.GridColumnCount) + col

        For Each rm In Rooms
          If rm.GridPosition = number Then
            Return rm
          End If
        Next

        Return Nothing

      Catch ex As Exception
        ' We have a valid x, y coordinate; however,
        ' we must be "between" the rooms...
        Return Nothing
      End Try

    End Function

    ''' <summary>
    ''' Find out which grid cell a specific point is within 
    ''' There may or not be a room there
    ''' </summary>
    ''' <param name="x">The column of the specific point</param>
    ''' <param name="y">The row of the specific point</param>
    ''' <returns>
    ''' the number of the room at the point would be at the point requested if it exists
    ''' </returns>
    Private Function GetRoomNumberFromXY%(x%, y%)

      'Dim sx = If(gridColumn = 1, 0, If(gridColumn = 2, 27, 55))
      'Dim sy = If(gridRow = 1, 0, If(gridRow = 2, 7, 15))

      Select Case x
        Case 0 To 25
          Select Case y
            Case 0 To 5 : Return 1
            Case 6 To 13 : Return 4
            Case 14 To 20 : Return 7
            Case Else
              Stop
          End Select
        Case 26 To 53
          Select Case y
            Case 0 To 5 : Return 2
            Case 6 To 13 : Return 5
            Case 14 To 20 : Return 8
            Case Else
              Stop
          End Select
        Case 54 To 80
          Select Case y
            Case 0 To 5 : Return 3
            Case 6 To 13 : Return 6
            Case 14 To 20 : Return 9
            Case Else
              Stop
          End Select
        Case Else
          Stop
      End Select

      '  'Dim result = 0

      '  'For r = 0 To Param.MapLevelGridRowMax - 1
      '  '  For c = 0 To Param.MapLevelGridColumnMax - 1
      '  '    Dim top = r * Param.MapGridCellHeight
      '  '    Dim left = c * Param.MapGridCellWidth
      '  '    Dim bottom = top + Param.MapGridCellHeight
      '  '    Dim right = left + Param.MapGridCellWidth
      '  '    If top <= y AndAlso bottom >= y AndAlso left <= x AndAlso right >= x Then
      '  '      result = r * 3 + c + 1
      '  '      Exit For
      '  '    End If
      '  '  Next
      '  '  If result > 0 Then
      '  '    Exit For
      '  '  End If
      '  'Next

      'Return result

    End Function

    'Private Function IsBlockingCell(x%, y%) As Boolean

    '  If x >= 0 AndAlso x <= Param.MapWidth AndAlso
    '     y >= 0 AndAlso y < Param.MapHeight Then
    '    Select Case Me.MapCellData(y, x)
    '      Case CellType.Void : Return True
    '      Case CellType.StructureWallTopLeftCorner : Return True
    '      Case CellType.StructureWallTopRightCorner : Return True
    '      Case CellType.StructureWallSide : Return True
    '      Case CellType.StructureWallTopBottom : Return True
    '      Case CellType.StructureWallBottomLeftCorner : Return True
    '      Case CellType.StructureWallBottomRightCorner : Return True
    '      Case Else
    '    End Select
    '  End If

    '  Return False

    'End Function

    ''' <summary>
    ''' When a tunnel is being dug, if it hits an obstruction on the way to its goal, 
    ''' it needs to dig around the obstruction so that it can continue in the desired direction.
    ''' </summary>
    ''' <param name="currentRow"></param>
    ''' <param name="currentColumn"></param>
    ''' <param name="direction"></param>
    ''' 
    ''' 
    ''' <returns></returns>
    Public Shared Function SaveDungeon(ByRef whatDungeon As List(Of Level)) As String
      Dim aCurrentMethod As String = "SaveDungeon"
      Dim aCurrentData As String = ""
      Dim aReturnValue As String = ""
      Dim sw As StreamWriter
      Dim aMap As New Map
      Dim lineIndex = 0
      Dim level = 0
      Dim levelCount As Integer = 0
      Dim aLine As String = ""

      Try
        aReturnValue = Environment.CurrentDirectory & "/Dungeon " & Date.Today.Year.ToString & Date.Today.Month.ToString & Date.Today.Day.ToString & ".rogue"
        If (File.Exists(aReturnValue)) Then
          File.Delete(aReturnValue)
        End If
        sw = File.CreateText(aReturnValue)

        For Each foundLevel As Level In whatDungeon
          levelCount = levelCount + 1
          sw.WriteLine("level: " & levelCount.ToString)
          sw.WriteLine("name: " & levelCount.ToString & " level")
          aCurrentData = "lights: "
          For lPtr As Integer = 0 To foundLevel.Lights.Count - 1
            If foundLevel.Lights(lPtr) = True Then
              aCurrentData = aCurrentData & "1"
            Else
              aCurrentData = aCurrentData & "0"
            End If
            'do not put comma after last item
            If lPtr < foundLevel.Lights.Count - 1 Then
              aCurrentData = aCurrentData & ","
            End If
          Next
          sw.WriteLine(aCurrentData)

          sw.WriteLine("map: ")
          For i = 0 To 20
            aCurrentData = ""
            For c = 0 To 79
              ' If (i = 11 AndAlso c = 29) OrElse (i = 6 And c = 56) Then
              Dim aTile As New Tile(foundLevel.Map(i, c).Type)
              aCurrentData &= aMap.MapCellTypeToChar(Core.Tile.TileTypeToCellType(aTile.Type))

              ' End If
              '' aCurrentData = aCurrentData & foundLevel.Map(i, c).ToChar
            Next
            sw.WriteLine(aCurrentData)
          Next

        Next
        sw.Close()

      Catch ex As Exception
        ' Me.m_ErrorHandler.LogError(Me.m_CurrentObject, aCurrentMethod, ex.Message, Now, ex)
        aReturnValue = ex.Message
      Finally
        If Not sw Is Nothing Then
          sw.Close()
          sw = Nothing
        End If
      End Try

      Return aReturnValue

    End Function

    Private Function TunnelAroundObstacle(currentRow%, currentColumn%, direction As Direction) As Coordinate

      Dim aReturnValue As Coordinate = New Coordinate(0, 0)
      'Dim isFound As Boolean = True
      'Dim aStepCtr As Integer = 0
      'Dim aAvoidanceDirection As Direction
      'Dim aBlockingRoomNumber As Integer = 0
      'Dim aBlockingRoom As New Room
      Dim aDistanceOne As Integer '= 0
      Dim aDistanceTwo As Integer '= 0
      Dim aStatus As String = "B" ' B = going around block, C = continuing past block

      'Dim availableDirections = Me.DigDirections(currentColumn, currentRow)

      Dim aCurrentY = currentRow
      Dim aCurrentX = currentColumn

      ' Make sure current direction is blocked...

      Dim seekX = 0
      Dim seekY = 0

      Select Case direction
        Case Direction.Up
          seekY = aCurrentY - 1
          seekX = aCurrentX
        Case Direction.Down
          seekY = aCurrentY + 1
          seekX = aCurrentX
        Case Direction.Left
          seekY = aCurrentY
          seekX = aCurrentX - 1
        Case Direction.Right
          seekY = aCurrentY
          seekX = aCurrentX + 1
        Case Else
          Stop
      End Select

      Dim tile = Me.Tile(seekX, seekY)

      Dim isFound As Boolean '= True

      If tile = CellType.Void OrElse tile = CellType.StructureTunnel Then

        ' Original direction clear...
        Dig(seekX, seekY, 2)

        ' Take the step in that direction then...
        'isFound = False ' The way is clear so exit...
        aCurrentY = seekY
        aCurrentX = seekX
        aReturnValue = New Coordinate(aCurrentX, aCurrentY)

      Else

        Dim aBlockingRoom = GetRoom(seekX, seekY)

        Dim aAvoidanceDirection As Direction

        If aBlockingRoom IsNot Nothing Then
          ' Find out the shortest side of the blocking room and go that way
          If direction = Direction.Down OrElse direction = Direction.Up Then
            aDistanceOne = aCurrentX - If(aBlockingRoom?.ActualMapLeftLocation, 0)
            aDistanceTwo = (aBlockingRoom.ActualMapLeftLocation + aBlockingRoom.Width) - aCurrentX
            If aDistanceOne > aDistanceTwo Then
              aAvoidanceDirection = Direction.Right
            Else
              aAvoidanceDirection = Direction.Left
            End If
          Else
            aDistanceOne = aCurrentY - If(aBlockingRoom?.ActualMapTopLocation, 0)
            aDistanceTwo = (aBlockingRoom.ActualMapTopLocation + aBlockingRoom.Height) - aCurrentY
            If aDistanceOne > aDistanceTwo Then
              aAvoidanceDirection = Direction.Down
            Else
              aAvoidanceDirection = Direction.Up
            End If
          End If
        End If

        isFound = True

        Dim aStepCtr = 0

        Do While isFound AndAlso aStepCtr < Param.MapHeight * Param.MapWidth

          If Not aCurrentX.Between(0, Param.MapWidth - 1) OrElse
                                 Not aCurrentY.Between(0, Param.MapHeight - 1) Then
            Stop
          End If

          Dim availableDirections = DigDirections(aCurrentX, aCurrentY)

          If availableDirections.Count > 0 Then
            'there are available moves from here
            If availableDirections.Contains(direction) Then
              'we can continue on our way
              'do so until block we were going around is cleared
              Select Case aAvoidanceDirection
                Case Direction.Down
                  If availableDirections.Contains(Direction.Up) Then
                    aStatus = "C"
                  Else
                    aStatus = "D" 'don going around
                  End If
                Case Direction.Up
                  If availableDirections.Contains(Direction.Down) Then
                    aStatus = "C"
                  Else
                    aStatus = "D" 'don going around
                  End If
                Case Direction.Left
                  If availableDirections.Contains(Direction.Right) Then
                    aStatus = "C"
                  Else
                    aStatus = "D" 'don going around
                  End If
                Case Direction.Right
                  If availableDirections.Contains(Direction.Left) Then
                    aStatus = "C"
                  Else
                    aStatus = "D" 'don going around
                  End If
              End Select
              If aStatus = "D" Then
                'we are around block so exit 
                aReturnValue = New Coordinate(aCurrentX, aCurrentY)
                isFound = True 'found a door so exit
              Else
                'we should continue
                Select Case direction
                  Case Direction.Up
                    seekY = aCurrentY - 1
                    seekX = aCurrentX
                  Case Direction.Down
                    seekY = aCurrentY + 1
                    seekX = aCurrentX
                  Case Direction.Left
                    seekY = aCurrentY
                    seekX = aCurrentX - 1
                  Case Direction.Right
                    seekY = aCurrentY
                    seekX = aCurrentX + 1
                  Case Else
                    Stop
                End Select
                Dig(seekX, seekY, 2)
                aCurrentX = seekX
                aCurrentY = seekY
              End If
            Else

              ' *** The following code doesn't make any sense...
              '     It makes no sense to go in a direction that we are trying
              '     to avoid... right?
              '     We end up here because we can no longer move in the direction
              '     we were traveling...
              '     Wouldn't we change to a direction that we *can* go?

              'If 1 = 0 Then

              'if cannot go in current desired direction then continue in avoidance direction
              If availableDirections.Contains(aAvoidanceDirection) Then
                Select Case aAvoidanceDirection
                  Case Direction.Up
                    seekY = aCurrentY - 1
                    seekX = aCurrentX
                  Case Direction.Down
                    seekY = aCurrentY + 1
                    seekX = aCurrentX
                  Case Direction.Left
                    seekY = aCurrentY
                    seekX = aCurrentX - 1
                  Case Direction.Right
                    seekY = aCurrentY
                    seekX = aCurrentX + 1
                  Case Else
                    Stop
                End Select
                Dig(seekX, seekY, 2)
                aCurrentX = seekX
                aCurrentY = seekY
              Else
                ' Can't move in the avoidance direction either...
                ' What do we do????
                Dim entry = Param.Randomizer.Next(0, availableDirections.Count)
                direction = availableDirections(entry)
              End If

              'End If

            End If
          End If
          aStepCtr += 1
        Loop
      End If

      Return aReturnValue

    End Function

    ''' <summary>
    ''' The the data from a recently created random room and put it in the MapData() object used by all the level functions
    ''' </summary>
    ''' <param name="rm">The Room object to be placed within the MapData array.</param>
    ''' <returns>A string with the top and left point of the room encoded as T-L</returns>
    Private Function UpdateMap$(rm As Room)

      Dim aReturnValue As String '= ""
      'Dim isDoorTop As Boolean '= False
      'Dim isDoorLeft As Boolean '= False
      'Dim isDoorRight As Boolean '= False
      'Dim isDoorBottom As Boolean '= False
      'Dim isAnyDoor As Boolean '= False
      'Dim rNumber As Integer '= 0
      'Dim xNumber As Integer '= 0
      Dim aDataArray() As String = {""}
      'Dim aFirstPtr As Integer '= 0
      'Dim aSecondPtr As Integer '= 0
      Dim aTopPtr As Integer = Param.MapHeight
      Dim aLeftPtr As Integer = Param.MapWidth
      'Dim aDirectionPtr As String '= ""
      'Dim aDoorCharacter As Integer '= 0
      Dim x As Integer '= 0
      Dim y As Integer '= 0

      If 1 = 0 Then

        'now update the mapdata with the random room 
        'rptr will cycle from 1 to height
        'cptr will cycle from 1 to width
        For r As Integer = 1 To (rm.Height)
          For c As Integer = 1 To (rm.Width)

            Dim ch = CellType.Void

            If r = 1 AndAlso c = 1 Then
              ch = CellType.StructureWallTopLeftCorner
            End If
            If r = 1 AndAlso c > 1 AndAlso c < rm.Width Then
              ch = CellType.StructureWallTopBottom
            End If
            If r = 1 AndAlso c = rm.Width Then
              ch = CellType.StructureWallTopRightCorner
            End If

            If r > 1 AndAlso r < rm.Height AndAlso (c = 1) Then
              ch = CellType.StructureWallSide
            End If
            If r > 1 AndAlso r < rm.Height AndAlso (c = rm.Width) Then
              ch = CellType.StructureWallSide
            End If

            If r = rm.Height AndAlso c = 1 Then
              ch = CellType.StructureWallBottomLeftCorner
            End If
            If r = rm.Height AndAlso c > 1 AndAlso c < rm.Width Then
              ch = CellType.StructureWallTopBottom
            End If
            If r = rm.Height AndAlso c = (rm.Width) Then
              ch = CellType.StructureWallBottomRightCorner
            End If
            If r > 1 AndAlso r < rm.Height AndAlso c > 1 AndAlso c < rm.Width Then
              ch = CellType.StructureFloor
            End If

            ''ensure that pointer into map data takes into account which grid cell is being processed
            y = (r + rm.MapTopLocation + ((rm.MapGridRowLocation - 1) * 7)) - 1
            x = (c + rm.MapLeftLocation + ((rm.MapGridColumnLocation - 1) * 26)) - 1

            MapCellData(y, x) = CType(ch, CellType)
            'Me.MapCellVisibility(y, x) = Not Me.m_IsFogOfWarActive ' testing m_TestMode 'testing true makes all visible

            If y < aTopPtr Then
              aTopPtr = y
            End If
            If x < aLeftPtr Then
              aLeftPtr = x
            End If
          Next
        Next

      Else

        For r = 0 To rm.Height - 1
          For c = 0 To rm.Width - 1

            Dim cell As CellType

            If r = 0 Then
              If c = 0 Then
                cell = CellType.StructureWallTopLeftCorner
              ElseIf c = rm.Width - 1 Then
                cell = CellType.StructureWallTopRightCorner
              Else
                cell = CellType.StructureWallTopBottom
              End If
            ElseIf r = rm.Height - 1 Then
              If c = 0 Then
                cell = CellType.StructureWallBottomLeftCorner
              ElseIf c = rm.Width - 1 Then
                cell = CellType.StructureWallBottomRightCorner
              Else
                cell = CellType.StructureWallTopBottom
              End If
            ElseIf c = 0 Then
              cell = CellType.StructureWallSide
            ElseIf c = rm.Width - 1 Then
              cell = CellType.StructureWallSide
            Else
              cell = CellType.StructureFloor
            End If

            y = rm.ActualMapTopLocation + r
            x = rm.ActualMapLeftLocation + c

            ' ensure that pointer into map data takes into account which grid cell is being processed
            'yPtr = (r + rm.MapTopLocation + ((rm.MapGridRowLocation - 1) * 7)) - 1
            'xPtr = (c + rm.MapLeftLocation + ((rm.MapGridColumnLocation - 1) * 26)) - 1

            MapCellData(y, x) = cell
            'Me.MapCellVisibility(y, x) = Not Me.m_IsFogOfWarActive

            ' ?????
            If y < aTopPtr Then
              aTopPtr = y
            End If
            If x < aLeftPtr Then
              aLeftPtr = x
            End If

          Next
        Next

      End If

      If 1 = 1 Then

        'now show the doors
        For Each doorData As Coordinate In rm.Doors
          y = doorData.Y
          x = doorData.X
          'Dim ct = doorData.Type
          MapCellData(y, x) = CellType.StructureDoor ' ct
          'Me.MapCellVisibility(y, x) = Not Me.m_IsFogOfWarActive ' testing m_TestMode 'testing true makes all visible

        Next

      End If

      aReturnValue = aTopPtr.ToString & "-" & aLeftPtr.ToString

      Return aReturnValue

    End Function

    Private Sub VerifyAllRoomsAccessible()
      Dim aCurrentRoom As New Room
      Dim aRoomList As New Stack(Of Room)
      Dim aVisitedRoomList As New List(Of Room)
      Dim isFound As Boolean = False
      Dim allDone As Boolean = False

      m_VerifiedCount += 1 'count of how many times recursively tried to verify

      'Start with all room having no connections
      'When walk from each room through each door to see where each tunnel goes 
      'and set room connected when found.
      'At end all rooms should be connected 
      '   of if not, then connect any unconnected room to closest connected room
      For Each aRoom As Core.Room In Rooms
        aRoom.HasConnections = False
        If aRoomList.Count = 0 Then
          aRoomList.Push(aRoom)
          aVisitedRoomList.Add(aRoom)
        End If
      Next

      Do While aRoomList.Count > 0
        aCurrentRoom = aRoomList.Pop()
        'keep a running list of tiles that need to be visited
        'start with first tile outside of first door in first room
        'add tiles that extend from visited tile so can recursively visit all tiles extending from this door
        Dim aTileStack As New Stack(Of Core.Coordinate)
        Dim aCurrentTile As New Coordinate(0, 0)
        Dim aCount As Integer = 0
        Dim aMax As Integer = Core.Param.MapHeight * Core.Param.MapWidth
        ' For Each aRoom As Core.Room In Rooms
        For Each aDoor As Core.Door In aCurrentRoom.Doors
          'walk out each door and follow tunnel and all branches from it
          '  making note of which rooms get visited
          Select Case aDoor.Face
            Case Face.Bottom
              Dim newCoord As New Core.Coordinate(aDoor.X, aDoor.Y + 1)
              aTileStack.Push(newCoord)
            Case Face.Left
              Dim newCoord As New Core.Coordinate(aDoor.X - 1, aDoor.Y)
              aTileStack.Push(newCoord)
            Case Face.Right
              Dim newCoord As New Core.Coordinate(aDoor.X + 1, aDoor.Y)
              aTileStack.Push(newCoord)
            Case Face.Top
              Dim newCoord As New Core.Coordinate(aDoor.X, aDoor.Y - 1)
              aTileStack.Push(newCoord)
          End Select
          'now first tunnel cell outside of door is in stack
          'pull cell from stack set celltype to Unknown to show it has been visited
          'and add any adjacent tunnel cell which has not yet been visited to stack
          'keep pulling and adding until stack is empty at end of attemped add
          'make note of any door found along the way by setting that door's room Hasconnection=true
          aCount = 0
          Do While aCount < aMax AndAlso aTileStack.Count > 0
            aCurrentTile = aTileStack.Pop
            'mark this cell visited
            Me.Tile(aCurrentTile.X, aCurrentTile.Y) = CellType.Unknown
            If Core.Param.IsDebugMode = True Then
              Console.SetCursorPosition(aCurrentTile.X, aCurrentTile.Y + 1)
              Console.ForegroundColor = ConsoleColor.Yellow
              Console.Write($"*")
            End If
            'now add adjacent tunnels to stack and if adjacent is a door mark the room as connected
            'Left
            If aCurrentTile.X > 0 Then
              If Me.Tile(aCurrentTile.X - 1, aCurrentTile.Y) = CellType.StructureTunnel Then
                Dim newCoord As New Core.Coordinate(aCurrentTile.X - 1, aCurrentTile.Y)
                aTileStack.Push(newCoord)
              End If
              If Not (aDoor.X = aCurrentTile.X - 1 AndAlso aDoor.Y = aCurrentTile.Y) AndAlso (Me.Tile(aCurrentTile.X - 1, aCurrentTile.Y) = CellType.StructureDoor OrElse Me.Tile(aCurrentTile.X - 1, aCurrentTile.Y) = CellType.SecretHorizontal OrElse Me.Tile(aCurrentTile.X - 1, aCurrentTile.Y) = CellType.SecretVertical) Then
                aCurrentRoom.HasConnections = True
                Dim newGrid As Integer = 0
                'find the gridPointer from the map x,y
                '  and then mark the room in the grid as having connectiosn
                newGrid = GetRoomNumberFromXY%(aCurrentTile.X - 1, aCurrentTile.Y)
                If newGrid > 0 Then
                  For Each foundRoom As Core.Room In Rooms
                    If foundRoom.GridPosition = newGrid AndAlso foundRoom.HasConnections = False Then
                      foundRoom.HasConnections = True
                      isFound = False
                      For Each vRoom As Room In aVisitedRoomList
                        If vRoom.GridPosition = foundRoom.GridPosition Then
                          isFound = True
                          Exit For
                        End If
                      Next
                      If isFound = False Then
                        aRoomList.Push(foundRoom)
                      End If
                      Exit For
                    End If
                  Next
                End If
              End If

            End If
            'Right
            If aCurrentTile.X < Core.Param.MapWidth - 1 Then
              If Me.Tile(aCurrentTile.X + 1, aCurrentTile.Y) = CellType.StructureTunnel Then
                Dim newCoord As New Core.Coordinate(aCurrentTile.X + 1, aCurrentTile.Y)
                aTileStack.Push(newCoord)
              End If
              If Not (aDoor.X = aCurrentTile.X + 1 AndAlso aDoor.Y = aCurrentTile.Y) AndAlso (Me.Tile(aCurrentTile.X + 1, aCurrentTile.Y) = CellType.StructureDoor OrElse Me.Tile(aCurrentTile.X + 1, aCurrentTile.Y) = CellType.SecretHorizontal OrElse Me.Tile(aCurrentTile.X + 1, aCurrentTile.Y) = CellType.SecretVertical) Then
                aCurrentRoom.HasConnections = True
                Dim newGrid As Integer = 0
                'find the gridPointer from the map x,y
                '  and then mark the room in the grid as having connectiosn
                newGrid = GetRoomNumberFromXY%(aCurrentTile.X + 1, aCurrentTile.Y)
                If newGrid > 0 Then
                  For Each foundRoom As Core.Room In Rooms
                    If foundRoom.GridPosition = newGrid AndAlso foundRoom.HasConnections = False Then
                      foundRoom.HasConnections = True
                      isFound = False
                      For Each vRoom As Room In aVisitedRoomList
                        If vRoom.GridPosition = foundRoom.GridPosition Then
                          isFound = True
                          Exit For
                        End If
                      Next
                      If isFound = False Then
                        aRoomList.Push(foundRoom)
                      End If
                      Exit For
                    End If
                  Next
                End If
              End If

            End If
            'Up
            If aCurrentTile.Y > 0 Then
              If Me.Tile(aCurrentTile.X, aCurrentTile.Y - 1) = CellType.StructureTunnel Then
                Dim newCoord As New Core.Coordinate(aCurrentTile.X, aCurrentTile.Y - 1)
                aTileStack.Push(newCoord)
              End If
              If Not (aDoor.X = aCurrentTile.X AndAlso aDoor.Y = aCurrentTile.Y - 1) AndAlso (Me.Tile(aCurrentTile.X, aCurrentTile.Y - 1) = CellType.StructureDoor OrElse Me.Tile(aCurrentTile.X, aCurrentTile.Y - 1) = CellType.SecretHorizontal OrElse Me.Tile(aCurrentTile.X, aCurrentTile.Y - 1) = CellType.SecretVertical) Then
                aCurrentRoom.HasConnections = True
                Dim newGrid As Integer = 0
                'find the gridPointer from the map x,y
                '  and then mark the room in the grid as having connectiosn
                newGrid = GetRoomNumberFromXY%(aCurrentTile.X, aCurrentTile.Y - 1)
                If newGrid > 0 Then
                  For Each foundRoom As Core.Room In Rooms
                    If foundRoom.GridPosition = newGrid AndAlso foundRoom.HasConnections = False Then
                      foundRoom.HasConnections = True
                      isFound = False
                      For Each vRoom As Room In aVisitedRoomList
                        If vRoom.GridPosition = foundRoom.GridPosition Then
                          isFound = True
                          Exit For
                        End If
                      Next
                      If isFound = False Then
                        aRoomList.Push(foundRoom)
                      End If
                      Exit For
                    End If
                  Next
                End If
              End If

            End If
            'Down
            If aCurrentTile.Y < Core.Param.MapHeight - 1 Then
              If Me.Tile(aCurrentTile.X, aCurrentTile.Y + 1) = CellType.StructureTunnel Then
                Dim newCoord As New Core.Coordinate(aCurrentTile.X, aCurrentTile.Y + 1)
                aTileStack.Push(newCoord)
              End If
              If Not (aDoor.X = aCurrentTile.X AndAlso aDoor.Y = aCurrentTile.Y + 1) AndAlso (Me.Tile(aCurrentTile.X, aCurrentTile.Y + 1) = CellType.StructureDoor OrElse Me.Tile(aCurrentTile.X, aCurrentTile.Y + 1) = CellType.SecretHorizontal OrElse Me.Tile(aCurrentTile.X, aCurrentTile.Y + 1) = CellType.SecretVertical) Then
                aCurrentRoom.HasConnections = True
                Dim newGrid As Integer = 0
                'find the gridPointer from the map x,y
                '  and then mark the room in the grid as having connectiosn
                newGrid = GetRoomNumberFromXY%(aCurrentTile.X, aCurrentTile.Y + 1)
                If newGrid > 0 Then
                  For Each foundRoom As Core.Room In Rooms
                    If foundRoom.GridPosition = newGrid AndAlso foundRoom.HasConnections = False Then
                      foundRoom.HasConnections = True
                      isFound = False
                      For Each vRoom As Room In aVisitedRoomList
                        If vRoom.GridPosition = foundRoom.GridPosition Then
                          isFound = True
                          Exit For
                        End If
                      Next
                      If isFound = False Then
                        aRoomList.Push(foundRoom)
                      End If
                      Exit For
                    End If
                  Next
                End If
              End If

            End If


            'If Core.Param.isDebugMode = True Then
            '  Console.ReadLine()
            'End If

            aCount += 1
          Loop
          'If Core.Param.isDebugMode = True Then
          '  Console.ReadLine()
          'End If
        Next
        'Next
      Loop

      'now any room that does not have HasConnections set needs a new connection
      'see what rooms do not yet have a connection and get them one
      allDone = True
      For Each aRoom As Core.Room In Rooms
        If aRoom.HasConnections = False Then
          'TODO make a connection
          'then call verify again to see if this fixed it
          'make a connection between a room that has no connections and a room that does
          'may need to disable stopping digging when find a tunnel
          Dim startX As Integer = 0
          Dim startY As Integer = 0
          Dim targetX As Integer = 0
          Dim targetY As Integer = 0
          isFound = False
          For Each aDoor As Door In aRoom.Doors
            'find door facing a room that hasconnections
            'this code has a small logic error 
            'using gridposition +- does not take into account wrapping where 4-1=3 etc
            'but should still work with longer than necessary tunnels
            Select Case aDoor.Face
              Case Face.Bottom
                For Each foundRoom As Room In Rooms
                  If foundRoom.GridPosition = aRoom.GridPosition + 3 AndAlso foundRoom.HasConnections = True Then
                    startX = aDoor.X
                    startY = aDoor.Y + 1
                    For Each foundDoor As Door In foundRoom.Doors
                      'try to get a door not facing away from startdoor
                      If Not foundDoor.Face = Face.Bottom Then
                        isFound = True
                        Select Case foundDoor.Face
                          Case Face.Left
                            targetX = foundDoor.X - 1
                            targetY = foundDoor.Y
                          Case Face.Right
                            targetX = foundDoor.X + 1
                            targetY = foundDoor.Y
                          Case Face.Top
                            targetX = foundDoor.X
                            targetY = foundDoor.Y - 1
                          Case Face.Bottom
                            targetX = foundDoor.X
                            targetY = foundDoor.Y + 1
                        End Select
                        Exit For
                      End If
                    Next
                    If isFound = False Then
                      'just use first door
                      isFound = True
                      Select Case foundRoom.Doors(0).Face
                        Case Face.Left
                          targetX = foundRoom.Doors(0).X - 1
                          targetY = foundRoom.Doors(0).Y
                        Case Face.Right
                          targetX = foundRoom.Doors(0).X + 1
                          targetY = foundRoom.Doors(0).Y
                        Case Face.Top
                          targetX = foundRoom.Doors(0).X
                          targetY = foundRoom.Doors(0).Y - 1
                        Case Face.Bottom
                          targetX = foundRoom.Doors(0).X
                          targetY = foundRoom.Doors(0).Y + 1
                      End Select
                      Exit For
                    End If
                  End If
                Next

              Case Face.Left
                For Each foundRoom As Room In Rooms
                  If foundRoom.GridPosition = aRoom.GridPosition - 1 AndAlso foundRoom.HasConnections = True Then
                    startX = aDoor.X - 1
                    startY = aDoor.Y
                    For Each foundDoor As Door In foundRoom.Doors
                      'try to get a door not facing away from startdoor
                      If Not foundDoor.Face = Face.Left Then
                        isFound = True
                        Select Case foundDoor.Face
                          Case Face.Left
                            targetX = foundDoor.X - 1
                            targetY = foundDoor.Y
                          Case Face.Right
                            targetX = foundDoor.X + 1
                            targetY = foundDoor.Y
                          Case Face.Top
                            targetX = foundDoor.X
                            targetY = foundDoor.Y - 1
                          Case Face.Bottom
                            targetX = foundDoor.X
                            targetY = foundDoor.Y + 1
                        End Select
                        Exit For
                      End If
                    Next
                    If isFound = False Then
                      'just use first door
                      isFound = True
                      Select Case foundRoom.Doors(0).Face
                        Case Face.Left
                          targetX = foundRoom.Doors(0).X - 1
                          targetY = foundRoom.Doors(0).Y
                        Case Face.Right
                          targetX = foundRoom.Doors(0).X + 1
                          targetY = foundRoom.Doors(0).Y
                        Case Face.Top
                          targetX = foundRoom.Doors(0).X
                          targetY = foundRoom.Doors(0).Y - 1
                        Case Face.Bottom
                          targetX = foundRoom.Doors(0).X
                          targetY = foundRoom.Doors(0).Y + 1
                      End Select
                      Exit For
                    End If
                  End If
                Next
              Case Face.Right
                For Each foundRoom As Room In Rooms
                  If foundRoom.GridPosition = aRoom.GridPosition + 1 AndAlso foundRoom.HasConnections = True Then
                    startX = aDoor.X + 1
                    startY = aDoor.Y
                    For Each foundDoor As Door In foundRoom.Doors
                      'try to get a door not facing away from startdoor
                      If Not foundDoor.Face = Face.Right Then
                        isFound = True
                        Select Case foundDoor.Face
                          Case Face.Left
                            targetX = foundDoor.X - 1
                            targetY = foundDoor.Y
                          Case Face.Right
                            targetX = foundDoor.X + 1
                            targetY = foundDoor.Y
                          Case Face.Top
                            targetX = foundDoor.X
                            targetY = foundDoor.Y - 1
                          Case Face.Bottom
                            targetX = foundDoor.X
                            targetY = foundDoor.Y + 1
                        End Select
                        Exit For
                      End If
                    Next
                    If isFound = False Then
                      'just use first door
                      isFound = True
                      Select Case foundRoom.Doors(0).Face
                        Case Face.Left
                          targetX = foundRoom.Doors(0).X - 1
                          targetY = foundRoom.Doors(0).Y
                        Case Face.Right
                          targetX = foundRoom.Doors(0).X + 1
                          targetY = foundRoom.Doors(0).Y
                        Case Face.Top
                          targetX = foundRoom.Doors(0).X
                          targetY = foundRoom.Doors(0).Y - 1
                        Case Face.Bottom
                          targetX = foundRoom.Doors(0).X
                          targetY = foundRoom.Doors(0).Y + 1
                      End Select
                      Exit For
                    End If
                  End If
                Next
              Case Face.Top
                For Each foundRoom As Room In Rooms
                  If foundRoom.GridPosition = aRoom.GridPosition - 3 AndAlso foundRoom.HasConnections = True Then
                    startX = aDoor.X
                    startY = aDoor.Y - 1
                    For Each foundDoor As Door In foundRoom.Doors
                      'try to get a door not facing away from startdoor
                      If Not foundDoor.Face = Face.Top Then
                        isFound = True
                        Select Case foundDoor.Face
                          Case Face.Left
                            targetX = foundDoor.X - 1
                            targetY = foundDoor.Y
                          Case Face.Right
                            targetX = foundDoor.X + 1
                            targetY = foundDoor.Y
                          Case Face.Top
                            targetX = foundDoor.X
                            targetY = foundDoor.Y - 1
                          Case Face.Bottom
                            targetX = foundDoor.X
                            targetY = foundDoor.Y + 1
                        End Select
                        Exit For
                      End If
                    Next
                    If isFound = False Then
                      'just use first door
                      isFound = True
                      Select Case foundRoom.Doors(0).Face
                        Case Face.Left
                          targetX = foundRoom.Doors(0).X - 1
                          targetY = foundRoom.Doors(0).Y
                        Case Face.Right
                          targetX = foundRoom.Doors(0).X + 1
                          targetY = foundRoom.Doors(0).Y
                        Case Face.Top
                          targetX = foundRoom.Doors(0).X
                          targetY = foundRoom.Doors(0).Y - 1
                        Case Face.Bottom
                          targetX = foundRoom.Doors(0).X
                          targetY = foundRoom.Doors(0).Y + 1
                      End Select
                      Exit For
                    End If
                  End If
                Next
            End Select
          Next
          If startX = 0 OrElse startY = 0 Then
            'just pick any unconnected room
            For Each foundRoom As Room In Rooms
              If foundRoom.HasConnections = False Then
                'startX = foundRoom.Doors(0).X
                'startY = foundRoom.Doors(0).Y
                Select Case foundRoom.Doors(0).Face
                  Case Face.Left
                    startX = foundRoom.Doors(0).X - 1
                    startY = foundRoom.Doors(0).Y
                  Case Face.Right
                    startX = foundRoom.Doors(0).X + 1
                    startY = foundRoom.Doors(0).Y
                  Case Face.Top
                    startX = foundRoom.Doors(0).X
                    startY = foundRoom.Doors(0).Y - 1
                  Case Face.Bottom
                    startX = foundRoom.Doors(0).X
                    startY = foundRoom.Doors(0).Y + 1
                End Select
                Exit For
              End If
            Next

          End If
          If isFound = False Then
            'just pick any other room
            For Each foundRoom As Room In Rooms
              If foundRoom.HasConnections = True Then
                'targetX = foundRoom.Doors(0).X
                'targetY = foundRoom.Doors(0).Y
                Select Case foundRoom.Doors(0).Face
                  Case Face.Left
                    targetX = foundRoom.Doors(0).X - 1
                    targetY = foundRoom.Doors(0).Y
                  Case Face.Right
                    targetX = foundRoom.Doors(0).X + 1
                    targetY = foundRoom.Doors(0).Y
                  Case Face.Top
                    targetX = foundRoom.Doors(0).X
                    targetY = foundRoom.Doors(0).Y - 1
                  Case Face.Bottom
                    targetX = foundRoom.Doors(0).X
                    targetY = foundRoom.Doors(0).Y + 1
                End Select
                Exit For
              End If
            Next

          End If
          If startX > 0 AndAlso startY > 0 AndAlso targetX > 0 AndAlso targetY > 0 Then
            'set start and target to cell outside of door

            Dim createdStepCount = CreateUtilityTunnel(startX, startY, targetX, targetY, False)
          End If
          allDone = False
          Exit For
        End If
      Next
      'If Core.Param.isDebugMode = True Then
      '  Console.ReadLine()
      'End If

      'reset all tunnel tiles from unknown
      For rowPtr As Integer = 0 To Core.Param.MapHeight - 1
        For colPtr As Integer = 0 To Core.Param.MapWidth - 1
          If Me.Tile(colPtr, rowPtr) = CellType.Unknown Then
            Me.Tile(colPtr, rowPtr) = CellType.StructureTunnel
            If Core.Param.IsDebugMode = True Then
              Console.SetCursorPosition(colPtr, rowPtr + 1)
              Console.ForegroundColor = ConsoleColor.White
              Console.Write($"#")
            End If
          End If
        Next
      Next

      If allDone = False AndAlso m_VerifiedCount < 10 Then
        'if all rooms not connected try again
        VerifyAllRoomsAccessible()
      End If








      ' Now examine the doors in each room that is not already in the connections list.
      ' Follow the tunnel from each door to see if it connects to a room that is not 
      ' showing in the connection list and add if found.
      ' If no connection found, add a utility tunnel to room left or right. 
      ' Choosing left-right utility tunnel connections to help with connecting all rooms.
      '
      ' Choose room with least number of connections in list. Choose the door in that room 
      ' closest to the room coming from.
      ' Utility direct tunnels should start from the first point outside the their door and 
      ' head directly to their destination. 
      '
      ' If a tunnel is found before the target door, follow it to make sure that it goes 
      ' to a door, even though tunnels do not exist without a door.
      ' If obstructed by a wall, step around it in the way away from the door (because 
      ' that will get to a corner sooner) and follow wall until can turn toward door.

      'Build list of connections between rooms

      ' Dim connectionList As New List(Of Coordinate)
      ''For Each connection In RoomConnections
      ''  connectionList.Add(connection)
      ''  If Not connection.X = connection.Y Then
      ''    connectionList.Add(New Coordinate(connection.Y, connection.X))
      ''  End If
      ''Next

      ''Dim cycle = 0

      ''Do While cycle < Param.MapHeight * Param.MapWidth

      ''  ' Finally, walk through all the rooms to ensure that every room is accessible...

      ''  Dim stepList = New List(Of Integer)

      ''  For Each connection In connectionList
      ''    Dim isFound = False
      ''    For Each foundStep In stepList
      ''      If foundStep = connection.X Then
      ''        isFound = True
      ''        Exit For
      ''      End If
      ''    Next
      ''    If Not isFound Then
      ''      stepList.Add(connection.X)
      ''    End If
      ''  Next

      ''  'now aStepList contains a list of all the rooms that exist. 
      ''  'Take first room in asteplist and put in atunnelroomlist
      ''  '   remove the room from asteplist
      ''  'while tunnelrooms contains entries and stepctr<max
      ''  '  Take first room from tunnelrooms and make working room
      ''  '     remove from tunnelrooms
      ''  '     Add connections from working room to tunnelrooms if they are not already there and if they still exist in steplist
      ''  '         rooms no longer in steplist have already been worked
      ''  'loop until all rooms in tunnel rooms have been worked
      ''  'any rooms remaining in steplist now need to be connected to a room not in step list
      ''  'createutilitytunnel
      ''  'Then go through this process again until steplist is empty at the end of the run.

      ''  If stepList.Count > 0 Then

      ''    Dim tunnelStepList = New List(Of Integer) From {
      ''                stepList(0) 'TODO does this work the same as in prior version???
      ''              }

      ''    stepList.RemoveAt(0)

      ''    Dim currentStep = tunnelStepList(0)

      ''    tunnelStepList.RemoveAt(0)

      ''    Dim count = 0

      ''    Do While count < Param.MapHeight * Param.MapWidth

      ''      For Each connection In connectionList

      ''        Dim x = connection.X
      ''        Dim y = connection.Y

      ''        If x = currentStep Then

      ''          ' This is a connection from the working room.
      ''          ' See if it goes to a room still in stepList, but not yet in tunnelList...
      ''          ' If so, remove it from stepList and add it to tunnelList.

      ''          For i = 0 To stepList.Count - 1

      ''            If stepList(i) = y Then

      ''              ' Still in stepList so remove and add to tunnelList if not already there...

      ''              Dim isFound = False

      ''              For Each foundItem In tunnelStepList
      ''                If foundItem = y Then
      ''                  isFound = True ' Already in tunnelList...
      ''                  Exit For
      ''                End If
      ''              Next

      ''              If Not isFound Then
      ''                ' Not there, so add to tunnellist for further processing...
      ''                tunnelStepList.Add(stepList(i))
      ''              End If

      ''              stepList.RemoveAt(i)

      ''              Exit For

      ''            End If

      ''          Next

      ''        End If

      ''      Next

      ''      If tunnelStepList.Count = 0 Then
      ''        Exit Do
      ''      Else
      ''        currentStep = tunnelStepList(0)
      ''        tunnelStepList.RemoveAt(0)
      ''      End If

      ''      ' Try again until finished...

      ''      count += 1

      ''    Loop

      ''    ' If stepList has any rooms left after tunnelList emptied then we need to add a tunnel...
      ''    If stepList.Count = 0 Then
      ''      Exit Do
      ''    Else

      ''      ' Need to tunnel between first room left in stepList to some room not in stepList...

      ''      Dim fromDoorX = 0
      ''      Dim fromDoorY = 0
      ''      Dim toDoorX = 0
      ''      Dim toDoorY = 0
      ''      Dim fromRoom = stepList(0)
      ''      Dim toRoom = 0

      ''      For Each foundRoom In Rooms
      ''        If foundRoom.GridPosition = fromRoom Then
      ''          ' Get a door in this room to start from...
      ''          fromDoorY = foundRoom.Doors(0).Y
      ''          fromDoorX = foundRoom.Doors(0).X
      ''          Exit For
      ''        End If
      ''      Next

      ''      ' Now find a room not in stepList to connect to...
      ''      ' Try rooms next door first...

      ''      For Each foundRoom As Room In Rooms

      ''        Dim isFound = False

      ''        For Each foundString In stepList
      ''          If foundString = foundRoom.GridPosition Then
      ''            isFound = True
      ''            Exit For
      ''          End If
      ''        Next

      ''        If Not isFound Then
      ''          ' This may be it!
      ''          If (fromRoom + 1 = foundRoom.GridPosition) OrElse
      ''                             (fromRoom - 1 = foundRoom.GridPosition) OrElse
      ''                             (fromRoom - 3 = foundRoom.GridPosition) OrElse
      ''                             (fromRoom + 3 = foundRoom.GridPosition) Then
      ''            toRoom = foundRoom.GridPosition
      ''            toDoorY = foundRoom.Doors(0).Y
      ''            toDoorX = foundRoom.Doors(0).X
      ''            Exit For
      ''          End If
      ''        End If

      ''      Next

      ''      If toRoom = 0 Then

      ''        ' If could not find room next door then take any room...

      ''        For Each foundRoom In Rooms

      ''          Dim isFound = False

      ''          For Each position In stepList
      ''            If position = foundRoom.GridPosition Then
      ''              isFound = True
      ''              Exit For
      ''            End If
      ''          Next

      ''          If Not isFound Then
      ''            ' This may be it!
      ''            toRoom = foundRoom.GridPosition
      ''            toDoorY = foundRoom.Doors(0).Y
      ''            toDoorX = foundRoom.Doors(0).X
      ''            Exit For
      ''          End If

      ''        Next

      ''      End If

      ''      Dim createdStepCount = CreateUtilityTunnel(fromDoorX, fromDoorY, toDoorX, toDoorY)

      ''      If createdStepCount > 0 Then
      ''        ' Add new tunnel to connectionslist and continue..
      ''        connectionList.Add(New Coordinate(fromRoom, toRoom))
      ''        connectionList.Add(New Coordinate(toRoom, fromRoom))
      ''      End If

      ''    End If

      ''  Else

      ''    Exit Do

      ''  End If

      ''  Debug.WriteLine("ConnectionList")
      ''  For Each currentData In connectionList
      ''    Debug.WriteLine(currentData)
      ''  Next

      ''  cycle += 1

      ''Loop

    End Sub

  End Class


  '  Public NotInheritable Class Map

  '    Private ReadOnly ModifierUp As New Coordinate(0, -1)
  '    Private ReadOnly ModifierDown As New Coordinate(0, +1)
  '    Private ReadOnly ModifierLeft As New Coordinate(-1, 0)
  '    Private ReadOnly ModifierRight As New Coordinate(+1, 0)

  '    ' The cell type value of the square the character is currently location at...
  '    'Private m_CellTypeBeforeCharacterEntered As Integer = -1

  '    'Private ReadOnly m_IsFogOfWarActive As Boolean = True

  '    'Private m_EntryStairGrid As Coordinate
  '    'Private m_ExitStairGrid As Coordinate

  '    Public Sub New() 'depth%)
  '      'Me.m_TestMode = generate
  '      'Me.CurrentMapLevel = depth
  '      Initialize() 'generate)
  '    End Sub

  '    '''' <summary>
  '    '''' Hold a pointer to the level of the current map
  '    '''' Bigger numbers means further down into the dungeon.
  '    '''' 0=not any level, 1=first or beginning level.
  '    '''' </summary>
  '    '''' <returns></returns>
  '    'Public Property CurrentMapLevel() As Integer

  '    'Private Property Row As Integer = 0
  '    'Private Property Column As Integer = 0

  '    '''' <summary>
  '    '''' Hold a pointer to which grid cell the EntryStairLocation points to
  '    '''' Number is YX 
  '    '''' Y being the row 
  '    '''' X being the column 
  '    '''' </summary>
  '    '''' <returns></returns>
  '    'Public Property EntryStairGrid() As Coordinate
  '    '  Get
  '    '    If Me.EntryStairLocation IsNot Nothing Then
  '    '      Me.Row = Me.EntryStairLocation.Y
  '    '      Me.Column = Me.EntryStairLocation.X

  '    '      If Me.Row <= 6 Then
  '    '        If Me.Column < 26 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(1, 1)
  '    '        End If
  '    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(2, 1)
  '    '        End If
  '    '        If Me.Column >= 53 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(3, 1)
  '    '        End If
  '    '      End If
  '    '      If Me.Row >= 7 AndAlso Me.Row <= 13 Then
  '    '        If Me.Column < 26 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(1, 2)
  '    '        End If
  '    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(2, 2)
  '    '        End If
  '    '        If Me.Column >= 53 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(3, 2)
  '    '        End If
  '    '      End If
  '    '      If Me.Row >= 14 Then
  '    '        If Me.Column < 26 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(1, 3)
  '    '        End If
  '    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(2, 3)
  '    '        End If
  '    '        If Me.Column >= 53 Then
  '    '          Me.m_EntryStairGrid = New Coordinate(3, 3)
  '    '        End If
  '    '      End If

  '    '    End If
  '    '    Return Me.m_EntryStairGrid
  '    '  End Get
  '    '  Set(value As Coordinate)
  '    '    Me.m_EntryStairGrid = value
  '    '  End Set
  '    'End Property

  '    '''' <summary>
  '    '''' Hold a pointer to which grid cell the ExitStairLocation points to
  '    '''' Number is YX 
  '    '''' Y being the row 
  '    '''' X being the column 
  '    '''' </summary>
  '    '''' <returns></returns>
  '    'Public Property ExitStairGrid() As Coordinate
  '    '  Get
  '    '    If Me.ExitStairLocation IsNot Nothing Then
  '    '      Me.Row = Me.ExitStairLocation.Y
  '    '      Me.Column = Me.ExitStairLocation.X

  '    '      If Me.Row <= 6 Then
  '    '        If Me.Column < 26 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(1, 1)
  '    '        End If
  '    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(2, 1)
  '    '        End If
  '    '        If Me.Column >= 53 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(3, 1)
  '    '        End If
  '    '      End If
  '    '      If Me.Row >= 7 AndAlso Me.Row <= 13 Then
  '    '        If Me.Column < 26 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(1, 2)
  '    '        End If
  '    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(2, 2)
  '    '        End If
  '    '        If Me.Column >= 53 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(3, 2)
  '    '        End If
  '    '      End If
  '    '      If Me.Row >= 14 Then
  '    '        If Me.Column < 26 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(1, 3)
  '    '        End If
  '    '        If Me.Column >= 27 AndAlso Me.Column < 52 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(2, 3)
  '    '        End If
  '    '        If Me.Column >= 53 Then
  '    '          Me.m_ExitStairGrid = New Coordinate(3, 3)
  '    '        End If
  '    '      End If

  '    '    End If
  '    '    Return Me.m_ExitStairGrid
  '    '  End Get
  '    '  Set(value As Coordinate)
  '    '    Me.m_ExitStairGrid = value
  '    '  End Set
  '    'End Property

  '    ''' <summary>
  '    ''' Hold a pointer to where on the level the player will come down the stairs
  '    ''' There must be a room here when level is generated
  '    ''' Number is YYXX 
  '    ''' YY being the row as two digits which may be zero padded
  '    ''' XX being the column as two digits which may be zero padded
  '    ''' </summary>
  '    ''' <returns></returns>
  '    Public Property EntryStairLocation() As Coordinate 'String = ""

  '    ''' <summary>
  '    ''' Hold a pointer to where on the level the player will go down to the next level
  '    ''' There must be a room here when level is generated
  '    ''' Number is YYXX 
  '    ''' YY being the row as two digits which may be zero padded
  '    ''' XX being the column as two digits which may be zero padded
  '    ''' </summary>
  '    ''' <returns></returns>
  '    Public Property ExitStairLocation() As Coordinate 'String = ""

  '    ''' <summary>
  '    ''' Each cell of the array contains a indicator which defines what is in that location of the map defined in enum CellType
  '    ''' 0 - 99 are reserved for structural elements
  '    ''' 100 indicates the user
  '    ''' 101 - 199 are reserved for future multiplayer aspects
  '    ''' 201 - 999 are reserved for items that can be picked up or manipulated
  '    ''' 1001 - x are reserved for creatures
  '    ''' </summary>
  '    Public MapCellData(Param.MapHeight - 1, Param.MapWidth - 1) As CellType

  '    '''' <summary>
  '    '''' Used to determine if a particular cell of the map has been made visible to the user.
  '    '''' </summary>
  '    'Public MapCellVisibility(Param.MapHeight - 1, Param.MapWidth - 1) As Boolean

  '    '''' <summary>
  '    '''' Used to store if a particular cell of the map has a room 
  '    '''' Used in tunnel creation.
  '    '''' </summary>
  '    'Public Property MapCellHasRoom(Param.GridRowCount + 1, Param.GridColumnCount + 1) As String

  '    Public Property Rooms() As New List(Of Room)

  '    Public Property RoomConnections() As New List(Of Coordinate) ' keep track of which rooms this room is connected to

  '    Public Sub Initialize()

  '      'Me.Row = 0
  '      'Me.Column = 0

  'Start:

  '      Rooms = New List(Of Room)
  '      RoomConnections = New List(Of Coordinate)

  '      ' Generate 1 or more random rooms in random locations (1-9)

  '      ClearMapData()

  '      ' Create a list of possible grid locations (1-9)...
  '      Dim grid = {New Coordinate(1, 1),
  '                  New Coordinate(2, 1),
  '                  New Coordinate(3, 1),
  '                  New Coordinate(1, 2),
  '                  New Coordinate(2, 2),
  '                  New Coordinate(3, 2),
  '                  New Coordinate(1, 3),
  '                  New Coordinate(2, 3),
  '                  New Coordinate(3, 3)}.ToList

  '      ' Now we will loop through a maximum of 9 times...
  '      For r = 1 To 9

  '        ' Pick a random entry from the grid list...

  '        Dim number = Param.Randomizer.Next(0, grid.Count)

  '        ' Now we will determine if we are going to actually create...

  '        ' First we need to determine if this is the first room being
  '        ' placed... if so, then we will place it as we will always
  '        ' have at least one room.

  '        Dim place = Rooms.Count = 0

  '        If Not place Then

  '          ' If not the first room being placed, then we will determine
  '          ' whether or not this room will be placed by rolling a d100.

  '          place = Param.Randomizer.Next(1, 101) <= Param.MinHasRoomPercentage

  '        End If

  '        If place Then

  '          ' We've determined that we are placing this room...
  '          Dim c = grid(number)
  '          ' So we will actually generate the room parameters (and doors).
  '          CreateRoom(c.Y, c.X)
  '          ' And remove it from the list of possible locations before we
  '          ' try for the next pass.  Yes, this means that it is possible
  '          ' that the same room (at random) could try again in another
  '          ' pass if it didn't place during a previous pass.  Removing it
  '          ' from the available list prevents a useless second attempt.
  '          grid.RemoveAt(number)
  '        End If

  '      Next

  '      ' In each room, generate 1 or more random doors;
  '      '  however, limit to a single door per wall;
  '      '  so a maximum of 4 doors.
  '      '  With that said, need to restrict creating a door on a
  '      '  wall that would be an invalid tunnel location.

  '      If False Then
  '        For Each rm In Rooms
  '          CreateDoors(rm)
  '        Next
  '      End If

  '      ' Now that we have rooms, need we need to genrate connections between each room:
  '      '
  '      '   If there is a room in the center, start with it.
  '      '   If not a room in the center, pick another room at random.
  '      '   Now that we have a room to start with, pick a door (source) at random in the selected room.
  '      '   Find the doormat (source) for the source door.
  '      '   Determine the nearest (target) door (in another room) to the selected door.
  '      '   Find the doormat for the target door.
  '      '   If target doormat is already a tunnel, follow path from the target door to
  '      '     toward the source door until we move in a direction that would place us in "the void".
  '      '     We will then use this as the target location.
  '      '   We will then "dig" a path between the source location and target location.
  '      '     Attempt to move in the direction of the target location.
  '      '     If blocked, move toward the left/right of the target location.
  '      '     If blocked, move in the other direction (left/right).
  '      '     After moving, attempt (again) to move in the direction of the target.
  '      '     If blocked, repeat...

  '      For Each rm In Rooms
  '        UpdateMap(rm)
  '      Next

  '      ' Generate the random start location within one of the rooms at random.

  '      Dim heroRow As Integer
  '      Dim heroColumn As Integer

  '      If True Then

  '        Dim n = Param.Randomizer.Next(0, Rooms.Count)

  '        Dim rm = Rooms(n)

  '        Dim x = Param.Randomizer.Next(1, rm.Width - 1)
  '        Dim y = Param.Randomizer.Next(1, rm.Height - 1)

  '        heroRow = rm.ActualMapTopLocation + y
  '        heroColumn = rm.ActualMapLeftLocation + x

  '        MapCellData(heroRow, heroColumn) = CellType.Hero

  '      End If

  '      ' Generate the random exit location within one of the rooms at random.
  '      '  Yes, the exit can be in the same room as the hero start; however,
  '      '  the exit should not (cannot) be in the same position as the hero start.

  '      If True Then

  '        Dim n = Param.Randomizer.Next(0, Rooms.Count)

  '        Dim rm = Rooms(n)

  '        Do

  '          Dim x = Param.Randomizer.Next(1, rm.Width - 1)
  '          Dim y = Param.Randomizer.Next(1, rm.Height - 1)

  '          Dim r = rm.ActualMapTopLocation + y
  '          Dim c = rm.ActualMapLeftLocation + x

  '          If r = heroRow AndAlso c = heroColumn Then
  '            ' Try again...
  '          Else
  '            MapCellData(r, c) = CellType.StructureStairsDown
  '            Exit Do
  '          End If

  '        Loop

  '      End If

  '      If True Then

  '        ' The following code will draw what we've done thus far.

  '        Console.ResetColor()

  '        For y = 0 To Param.MapHeight - 1
  '          For x = 0 To Param.MapWidth - 1

  '            Dim ch As Char '= "?"c
  '            Dim fg = ConsoleColor.Gray

  '            Select Case Tile(x, y)
  '              Case CellType.Void : ch = " "c
  '              Case CellType.Hero : ch = "@"c : fg = ConsoleColor.Yellow
  '              Case CellType.StructureDoor : ch = "+"c : fg = ConsoleColor.White
  '              Case CellType.StructureFloor : ch = "."c : fg = ConsoleColor.DarkGreen
  '              Case CellType.StructureWallTopLeftCorner : ch = "["c : fg = ConsoleColor.DarkYellow
  '              Case CellType.StructureWallTopRightCorner : ch = "]"c : fg = ConsoleColor.DarkYellow
  '              Case CellType.StructureWallTopBottom : ch = "-"c : fg = ConsoleColor.DarkYellow
  '              Case CellType.StructureWallSide : ch = "|"c : fg = ConsoleColor.DarkYellow
  '              Case CellType.StructureWallBottomLeftCorner : ch = "{"c : fg = ConsoleColor.DarkYellow
  '              Case CellType.StructureWallBottomRightCorner : ch = "}"c : fg = ConsoleColor.DarkYellow
  '              Case CellType.StructureTunnel : ch = "#"c
  '              Case CellType.StructureStairsDown : ch = "="c : fg = ConsoleColor.Green
  '              Case Else
  '                ch = "?"c : fg = ConsoleColor.Red
  '            End Select

  '            Console.SetCursorPosition(x, y + 1)
  '            Console.ForegroundColor = fg
  '            Console.Write(ch)

  '          Next
  '        Next

  '        If False Then
  '          ' Create the tunnels between rooms...
  '          ' Expects that there are at least two rooms (entry and exit)...
  '          CreateRandomTunnels()
  '        End If

  '        If True Then

  '          ' Now that we have the rooms... go through each room/region and connect it with another room/region.
  '          ' 

  '          ' 2,2 -> 1,2
  '          ' 2,2 -> 2,3
  '          ' 2,2 -> 3,2
  '          ' 2,2 -> 2,1

  '          ' If room, create random door on facing walls between two rooms.
  '          '


  '        End If

  '        Console.ReadLine()

  '        GoTo Start

  '      End If

  '      If 1 = 0 Then

  '        'If Me.EntryStairLocation Is Nothing AndAlso
  '        '   Me.ExitStairLocation Is Nothing Then

  '        ' These locations can be created by calling NEW with the correct parameters
  '        ' If not, then randomly select them...
  '        ' First select a random grid cell...
  '        ' Then select a random spot within the cell which is not on the edge so it can be within walls...

  '        Dim seekX = 0
  '        Dim seekY = Param.Randomizer.Next(0, Param.GridColumnCount * Param.GridRowCount) + 1

  '        Dim currentX = 0
  '        Do While currentX < 10 AndAlso seekX = 0
  '          seekX = Param.Randomizer.Next(0, Param.GridColumnCount * Param.GridRowCount) + 1
  '          If seekX = seekY Then
  '            seekX = 0 ' Can not be in the same grid as entry...
  '          End If
  '          currentX += 1
  '        Loop

  '        If seekX = 0 Then
  '          ' If could not create random then set to next grid...
  '          seekX = seekY + 1
  '          If seekX > Param.GridColumnCount * Param.GridRowCount Then
  '            seekX = 1
  '          End If
  '        End If

  '        ' Now we have the entry grid in aseekYptr and the exit grid in aseekXptr...
  '        ' Pick a random location within the grid for the actual stairs making sure 
  '        ' not to hit an edge so it will not conflict with walls

  '        'Dim currentY = Param.Randomizer.Next(0, Param.MapGridCellHeight - 2) + 2
  '        'currentX = Param.Randomizer.Next(0, Param.MapGridCellWidth - 2) + 2

  '        ' Need to convert local grid coordinates into global map coordinates...

  '        Dim gridY = 1
  '        Dim aYPtr = seekY
  '        Do While aYPtr > Param.GridColumnCount
  '          If aYPtr > Param.GridColumnCount Then
  '            gridY += 1
  '            aYPtr -= Param.GridColumnCount
  '          End If
  '        Loop

  '        If gridY = 0 Then
  '          gridY = 1
  '        End If

  '        Dim gridX = aYPtr ' What is left is the x axis...

  '        Dim currentY = Param.Randomizer.Next(1, Param.CellHeight(gridY))
  '        currentX = Param.Randomizer.Next(1, Param.CellWidth(gridX))

  '        'currentY = ((gridY - 1) * Param.MapGridCellHeight) + currentY
  '        'currentX = ((gridX - 1) * Param.MapGridCellWidth) + currentX
  '        currentY = Param.CellStartY(gridY) + currentY
  '        currentX = Param.CellStartX(gridX) + currentX

  '        EntryStairLocation = New Coordinate(currentX, currentY) ' Me.GetCoordinateString(currentY, currentX)
  '        'Me.EntryStairGrid = New Coordinate(gridX, gridY) ' gridY.ToString & gridX.ToString

  '        ' Pick a random location within the grid for the actual stairs making 
  '        ' sure not to hit an edge so it will not conflict with walls...

  '        'currentY = Param.Randomizer.Next(0, Param.MapGridCellHeight - 2) + 2
  '        'currentX = Param.Randomizer.Next(0, Param.MapGridCellWidth - 2) + 2
  '        'currentY = Param.Randomizer.Next(0, Param.MapGridCellHeight - 2) + 2
  '        'currentX = Param.Randomizer.Next(0, Param.MapGridCellWidth - 2) + 2
  '        currentY = Param.Randomizer.Next(1, Param.CellHeight(gridY))
  '        currentX = Param.Randomizer.Next(1, Param.CellWidth(gridX))

  '        ' Need to convert local grid coordinates into global map coordinates...

  '        gridY = 1
  '        gridX = 0
  '        aYPtr = seekX
  '        Do While aYPtr > Param.GridColumnCount
  '          If aYPtr > Param.GridColumnCount Then
  '            gridY += 1
  '            aYPtr -= Param.GridColumnCount
  '          End If
  '        Loop

  '        If gridY = 0 Then
  '          gridY = 1
  '        End If

  '        gridX = aYPtr ' What is left is the x axis...

  '        'currentY = ((gridY - 1) * Param.MapGridCellHeight) + currentY
  '        'currentX = ((gridX - 1) * Param.MapGridCellWidth) + currentX
  '        currentY = Param.CellStartY(gridY) + currentY
  '        currentX = Param.CellStartX(gridX) + currentX

  '        ExitStairLocation = New Coordinate(currentX, currentY) ' Me.GetCoordinateString(currentY, currentX)
  '        'Me.ExitStairGrid = New Coordinate(gridX, gridY) ' gridY.ToString & gridX.ToString

  '        'End If

  '        CreateRandomLevel()

  '      End If

  '    End Sub

  '    'Public Function IsEntryCell(x%, y%) As Boolean
  '    '  Return Me.MapCellData(y, x) = CellType.Hero 'StructureStairsUp
  '    'End Function

  '    'Public Function IsExitCell(x%, y%) As Boolean
  '    '  Return Me.MapCellData(y, x) = CellType.StructureStairsDown
  '    'End Function

  '    Private Shared Function MapCellTypeToChar(type As CellType) As Char
  '      Select Case type
  '        Case CellType.Void : Return " "c
  '        Case CellType.StructureFloor : Return "."c
  '        Case CellType.StructureTunnel : Return "#"c
  '        Case CellType.StructureWallTopLeftCorner : Return "["c
  '        Case CellType.StructureWallTopRightCorner : Return "]"c
  '        Case CellType.StructureWallTopBottom : Return "-"c
  '        Case CellType.StructureWallSide : Return "|"c
  '        Case CellType.StructureWallBottomLeftCorner : Return "{"c
  '        Case CellType.StructureWallBottomRightCorner : Return "}"c
  '        'Case CellType.StructureDoorTopBottom : Return "+"c
  '        'Case CellType.StructureDoorSide : Return "+"c
  '        Case CellType.StructureDoor : Return "+"c
  '        Case CellType.StructureStairsDown : Return "="c
  '        'Case CellType.StructureStairsUp : Return "="c
  '        Case CellType.SecretHorizontal : Return "/"c
  '        Case CellType.SecretVertical : Return "\"c
  '        Case CellType.Hero
  '          Return "@"c
  '        Case Else
  '          Throw New Exception("Unknown tile type.")
  '      End Select

  '    End Function

  '    Public Function ToDungeonLevel() As Level

  '      Dim result As New Level()

  '      For light = 0 To 8
  '        'TESTING:
  '        Dim isLit = True 'False
  '        For room = 0 To Rooms.Count - 1
  '          Dim grid = ((Rooms(room).MapGridRowLocation - 1) * 3) + Rooms(room).MapGridColumnLocation
  '          If grid = light + 1 Then
  '            ' This room is at the currently pointed at location in the map grid...
  '            isLit = Rooms(room).IsLit
  '            Exit For
  '          End If
  '        Next
  '        result.Lights(light) = isLit
  '      Next

  '      For r = 0 To Param.MapHeight - 1
  '        For c = 0 To Param.MapWidth - 1
  '          Dim ch = MapCellTypeToChar(MapCellData(r, c))
  '          result.Map(r, c) = New Tile(ch) With {.Explored = True}
  '        Next
  '      Next

  '      Return result

  '    End Function

  '    Private Function CheckIfRandomRoomExists(gridRow%, gridColumn%) As String

  '      Dim entryRow As Integer '= 0
  '      Dim entryColumn As Integer '= 0
  '      Dim entryRowPointer As Integer '= 0
  '      Dim entryColumnPointer As Integer '= 0
  '      Dim exitRow As Integer '= 0
  '      Dim exitColumn As Integer '= 0
  '      Dim exitRowPointer As Integer '= 0
  '      Dim exitColumnPointer As Integer '= 0
  '      'Dim roomRowOffset As Integer '= 0 'offset from top side of cell
  '      'Dim roomColumnOffset As Integer '= 0 'offset from left side of cell
  '      Dim isOK As Boolean = False
  '      'Dim isEntryCell As Boolean '= False ' Default to false and set true if found
  '      'Dim isExitCell As Boolean '= False ' Default to false and set true if found
  '      'Dim fromX As Integer '= 0 'These will hold the allowable random range that the top left corner of the room can be created in
  '      'Dim toX As Integer '= 0
  '      'Dim fromY As Integer '= 0
  '      'Dim toY As Integer '= 0
  '      'Dim aString As String '= ""

  '      'NOTE; MUST MAKE SURE TO INITIALIZE EntryStairGrid

  '      entryRow = EntryStairLocation.Y
  '      entryColumn = EntryStairLocation.X
  '      exitRow = ExitStairLocation.Y
  '      exitColumn = ExitStairLocation.X
  '      entryRowPointer = EntryStairLocation.Y
  '      entryColumnPointer = EntryStairLocation.X
  '      exitRowPointer = ExitStairLocation.Y
  '      exitColumnPointer = ExitStairLocation.X

  '      ' If a room has an entry or exit point, then the room must surround that point
  '      ' else it can be anywhere in the grid cell...

  '      ' Minimum room size if 4x4 so can have border with 2x2 inside...

  '      'Dim roomWidth = Param.Randomizer.Next(0, Param.MapGridCellWidth) + 2 ' Make sure there is room on the side for a corridor...
  '      'Dim roomHeight = Param.Randomizer.Next(0, Param.MapGridCellHeight - 2) + 1 ' Top and bottom can only be 6 vice 7 to make room for corridors...
  '      'If roomHeight < 4 Then
  '      '  roomHeight = 4 ' Room must have minimum floor space of 2 with wall on either side...
  '      'End If
  '      'If roomWidth < 4 Then
  '      '  roomWidth = 4
  '      'End If
  '      'If roomHeight > Param.MapGridCellHeight - 2 Then
  '      '  roomHeight = Param.MapGridCellHeight - 2
  '      'End If
  '      'If roomWidth > Param.MapGridCellWidth - 2 Then
  '      '  roomWidth = Param.MapGridCellWidth - 2 ' Make sure there is room on the side for a corridor...
  '      'End If

  '      Dim result = "" '$"{aString}{roomHeight}-{roomWidth}|"

  '      If entryRow = gridRow AndAlso entryColumn = gridColumn Then
  '        result &= CreateRandomEntryRoom(gridRow, gridColumn) ', entryRowPointer, entryColumnPointer)
  '        isOK = True ' This is the cell for the entry into the level...
  '      End If

  '      If exitRow = gridRow AndAlso exitColumn = gridColumn Then
  '        result &= CreateRandomExitRoom(gridRow, gridColumn) ', exitRowPointer, exitColumnPointer)
  '        isOK = True ' This is the cell for the exit from the level...
  '      End If

  '      If Not isOK Then

  '        ' If not an entry or exit cell, then randomly choose if room exists...

  '        Dim randomNumber = Param.Randomizer.Next(0, 100) + 1
  '        If randomNumber <= Param.MinHasRoomPercentage Then
  '          result &= CreateRandomRoom(gridRow, gridColumn)
  '          isOK = True
  '        End If

  '      End If

  '      If Not isOK Then
  '        result = ""
  '      End If

  '      Return result

  '    End Function

  '    Private Sub ClearMapData()

  '      For y = 0 To Param.MapHeight - 1
  '        For x = 0 To Param.MapWidth - 1
  '          MapCellData(y, x) = 0
  '          'Me.MapCellVisibility(y, x) = False
  '        Next
  '      Next

  '    End Sub

  '    Private Function CreateRandomEntryRoom$(gridRow%, gridColumn%) ', entryRow%, entryColumn%)

  '      'Dim result As String = ""

  '      Dim rm = Room.CreateRandomRoom(gridRow, gridColumn)

  '      If rm.Height > 0 Then

  '        Rooms.Add(rm)
  '        'result = rm.ToString

  '        UpdateMap(rm)

  '        Dim x = Param.Randomizer.Next(1, rm.Width - 1)
  '        Dim y = Param.Randomizer.Next(1, rm.Height - 1)

  '        Dim r = rm.ActualMapTopLocation + y
  '        Dim c = rm.ActualMapLeftLocation + x

  '        MapCellData(r, c) = CellType.Hero 'CellType.StructureStairsUp
  '        'Me.MapCellVisibility(r, c) = Not Me.m_IsFogOfWarActive

  '      End If

  '      Return rm.ToString

  '      ''Dim rm = Room.CreateRandomEntryRoom(gridRow, gridColumn, entryRow, entryColumn)

  '      'Dim entryX = entryColumn - ((gridColumn - 1) * 26)
  '      'Dim entryY = entryRow - ((gridRow - 1) * 7)
  '      'Dim xOff = (entryX - rm.Width) + 1
  '      'Dim yOff = (entryY - rm.Height) + 1

  '      'If rm.Height > 0 Then
  '      '  Me.Rooms.Add(rm)
  '      '  result = rm.ToString
  '      'End If

  '      'Me.UpdateMap(rm)

  '      'Me.MapCellData(entryRow, entryColumn) = CellType.Hero 'CellType.StructureStairsUp
  '      'Me.MapCellVisibility(entryRow, entryColumn) = Not Me.m_IsFogOfWarActive

  '      ''If Me.CurrentMapLevel = 1 Then
  '      ''  ' On first level place player character initialially beside the stair leading up...
  '      ''If Me.MapCellData(entryRow, entryColumn - 1) = CellType.StructureFloor Then
  '      ''  Me.MapCellData(entryRow, entryColumn - 1) = CellType.Hero
  '      ''Else
  '      ''  Me.MapCellData(entryRow, entryColumn + 1) = CellType.Hero
  '      ''End If
  '      ''End If

  '      'Return result

  '    End Function

  '    Private Function CreateRandomExitRoom$(gridRow%, gridColumn%) ', exitRow%, exitColumn%)

  '      Dim rm = Room.CreateRandomRoom(gridRow, gridColumn)

  '      If rm.Height > 0 Then

  '        Rooms.Add(rm)

  '        UpdateMap(rm)

  '        Dim x = Param.Randomizer.Next(1, rm.Width - 1)
  '        Dim y = Param.Randomizer.Next(1, rm.Height - 1)

  '        Dim r = rm.ActualMapTopLocation + y
  '        Dim c = rm.ActualMapLeftLocation + x

  '        MapCellData(r, c) = CellType.StructureStairsDown
  '        'Me.MapCellVisibility(r, c) = Not Me.m_IsFogOfWarActive

  '      End If

  '      Return rm.ToString

  '      'Dim rm = Room.CreateRandomExitRoom(gridRow, mapColumn, exitRow, exitColumn)

  '      'Dim aReturnValue As String = ""
  '      'Dim X As Integer = 0 ' left to right
  '      'Dim Y As Integer = 0 ' top to bottom
  '      'Dim X1 As Integer = 0 ' left to right
  '      'Dim Y1 As Integer = 0 ' top to bottom
  '      'Dim exitX As Integer = exitColumn - ((mapColumn - 1) * 26)
  '      'Dim exitY As Integer = exitRow - ((gridRow - 1) * 7)
  '      'Dim xOFF As Integer = (exitX - rm.Width) + 1
  '      'Dim yOFF As Integer = (exitY - rm.Height) + 1

  '      'If rm.Height > 0 Then
  '      '  Me.Rooms.Add(rm)
  '      '  aReturnValue = rm.ToString
  '      'End If
  '      'Me.UpdateMap(rm)
  '      'Me.MapCellData(exitRow, exitColumn) = CellType.StructureStairsDown
  '      'Me.MapCellVisibility(exitRow, exitColumn) = Not Me.m_IsFogOfWarActive 'TODO testing m_TestMode 'testing true makes all visible

  '      'Return aReturnValue
  '    End Function

  '    ''' <summary>
  '    ''' Create a new random level
  '    ''' Replace current level information in properties with randomly generated level.
  '    ''' 
  '    ''' 
  '    ''' Levels are divided into a 3x3 grid, each of which can have or not have a room.
  '    ''' Rooms, if the exist, must be at least 2x2
  '    ''' Each wall may have only one door
  '    ''' Generation controlled by properties:
  '    ''' CurrentMapLevel = How many levels down the current level is. 
  '    ''' EntryStairLocation - There must be a room where the stairs come down into this level
  '    ''' ExitStairLocation - There must be a room where the stairs go down to the next level
  '    ''' </summary>
  '    Private Sub CreateRandomLevel()

  '      Dim m_entryRow As Integer '= 0
  '      Dim m_entryColumn As Integer '= 0
  '      Dim m_entryRowPointer As Integer '= 0
  '      Dim m_entryColumnPointer As Integer ' = 0
  '      Dim m_exitRow As Integer '= 0
  '      Dim m_exitColumn As Integer '= 0
  '      Dim m_exitRowPointer As Integer '= 0
  '      Dim m_exitColumnPointer As Integer '= 0
  '      'Dim m_roomHeight As Integer '= 0
  '      'Dim m_roomWidth As Integer '= 0
  '      'Dim m_roomRowOffset As Integer '= 0 ' Offset from top side of cell
  '      'Dim m_roomColumnOffset As Integer '= 0 ' Offset from left side of cell
  '      Dim m_randomNumber As Integer = Param.Randomizer.Next()
  '      'Dim m_mapCharacterValue As Integer '= 0
  '      'Dim m_ISOK As Boolean '= False
  '      'Dim m_ISEntryCell As Boolean '= False
  '      'Dim m_ISExitCell As Boolean '= False
  '      'Dim m_FromX As Integer '= 0 ' These will hold the allowable random range that the top left corner of the room can be created in
  '      'Dim m_ToX As Integer '= 0
  '      'Dim m_FromY As Integer '= 0
  '      'Dim m_ToY As Integer '= 0
  '      Dim aString As String '= ""

  '      ClearMapData()

  '      m_entryRow = EntryStairLocation.Y
  '      m_entryColumn = EntryStairLocation.X
  '      m_exitRow = ExitStairLocation.Y
  '      m_exitColumn = ExitStairLocation.X

  '      m_entryRowPointer = EntryStairLocation.Y
  '      m_entryColumnPointer = EntryStairLocation.X
  '      m_exitRowPointer = ExitStairLocation.Y
  '      m_exitColumnPointer = ExitStairLocation.X

  '      For gridRow = 1 To Param.GridRowCount
  '        For gridColumn = 1 To Param.GridColumnCount
  '          aString = CheckIfRandomRoomExists(gridRow, gridColumn)
  '          ' Keep track of which grid cells have rooms for tunnel creation...
  '          'astring will in the form of height-width|top-left or BLANK if no room exists
  '          'Me.MapCellHasRoom(gridRow, gridColumn) = aString
  '        Next
  '      Next

  '      Console.ResetColor()

  '      For y = 0 To Param.MapHeight - 1
  '        For x = 0 To Param.MapWidth - 1
  '          Console.SetCursorPosition(x, y + 1)
  '          Select Case Tile(x, y)
  '            Case CellType.Void : Console.Write(" "c)
  '            Case CellType.StructureDoor : Console.Write("+")
  '            Case CellType.StructureFloor : Console.Write(".")
  '            Case Else
  '              Console.Write("=")
  '          End Select
  '        Next
  '      Next

  '      ' Create the tunnels between rooms...
  '      ' Expects that there are at least two rooms (entry and exit)...
  '      CreateRandomTunnels()

  '    End Sub

  '    Private Sub CreateRoom(gridRow%, gridColumn%)

  '      Dim rm = Room.CreateRoom(gridRow, gridColumn)

  '      If rm.Height > 0 Then
  '        Rooms.Add(rm)
  '      Else
  '        Stop
  '      End If

  '    End Sub

  '    Private Shared Sub CreateDoors(rm As Room)

  '      Dim gridRow = rm.MapGridRowLocation
  '      Dim gridColumn = rm.MapGridColumnLocation

  '      Dim sx = If(gridColumn = 1, 0, If(gridColumn = 2, 27, 54))
  '      Dim sy = If(gridRow = 1, 0, If(gridRow = 2, 7, 15))

  '      Dim y = rm.MapTopLocation
  '      Dim x = rm.MapLeftLocation

  '      Dim allowTop = gridRow > 1 OrElse (gridRow = 1 AndAlso y > 0)
  '      Dim allowBottom = gridRow < 3 OrElse (gridRow = 3 AndAlso y + (rm.Height) < Param.CellHeight(gridRow))
  '      Dim allowLeft = gridColumn > 1 OrElse (gridColumn = 1 AndAlso x > 0)
  '      Dim allowRight = gridColumn < 3 OrElse (gridColumn = 3 AndAlso x + (rm.Width) < Param.CellWidth(gridColumn))

  '      Dim possible As New List(Of Integer)
  '      If allowTop Then possible.Add(1)
  '      If allowBottom Then possible.Add(2)
  '      If allowLeft Then possible.Add(3)
  '      If allowRight Then possible.Add(4)

  '      ' Determine how many doors we will create...

  '      Dim count = Param.Randomizer.Next(1, possible.Count + 1)

  '      ' Now work to create "count" number of doors...

  '      For i = 1 To count

  '        ' Select a random (available) entry from the possible list.

  '        Dim entry = Param.Randomizer.Next(0, possible.Count)

  '        ' Determine which location this door will be (top, bottom, left, right)...

  '        Dim door = possible(entry)

  '        Dim xoffset = 0
  '        Dim yoffset = 0

  '        Dim face As Core.Face

  '        Select Case door
  '          Case 1, 2 ' Top, bottom
  '            face = Core.Face.Top
  '            xoffset = Param.Randomizer.Next(1, rm.Width - 1)
  '          Case 3, 4 ' Left, right
  '            face = Core.Face.Left
  '            yoffset = Param.Randomizer.Next(1, rm.Height - 1)
  '          Case Else
  '            Stop
  '        End Select

  '        Select Case door
  '          Case 2 ' Bottom
  '            face = Core.Face.Bottom
  '            yoffset = rm.Height - 1
  '          Case 4 ' Right
  '            face = Core.Face.Right
  '            xoffset = rm.Width - 1
  '          Case Else
  '        End Select

  '        rm.Doors.Add(New Door(sx + x + xoffset, sy + y + yoffset, face))

  '        possible.RemoveAt(entry)

  '      Next

  '    End Sub

  '    Private Function CreateRandomRoom$(gridRow%, gridColumn%)

  '      Dim result As String = ""

  '      Dim rm = Room.CreateRandomRoom(gridRow, gridColumn)

  '      If rm.Height > 0 Then
  '        Rooms.Add(rm)
  '        result = rm.ToString
  '      End If

  '      UpdateMap(rm)

  '      Return result

  '    End Function

  '    ''' <summary>
  '    ''' Make sure each door of each room connects to either another door (preferably 
  '    ''' in another room) or intersects a tunnel.
  '    ''' Make sure each room is connected to a least one other room and 
  '    ''' that every room on the level is accessible.
  '    ''' </summary>
  '    ''' <returns>
  '    ''' For each row
  '    '''   For each column
  '    '''     look for a door
  '    '''       if found then extend the tunnel out one or two points then look for door the closets to it to tunnel toward
  '    '''       (determine differential distance up/down and left/right from current door)
  '    '''       Until reach other door
  '    '''         Set random value to determine if tunnel should go left/right or up/down toward destination door 
  '    '''         (65% toward greatest differential, 30% toward lesser differential and 4% away from those two and the way coming from, 1% stop tunnel
  '    '''         Tunnel for random distance (3 blocks to 1/2 differential)
  '    '''       end until
  '    '''       endif
  '    '''   next
  '    ''' next
  '    ''' When all doors have tunnels, make sure a path is possible to every room from the entry room.
  '    ''' </returns>
  '    Private Sub CreateRandomTunnels()

  '      For position = 0 To Rooms.Count - 1

  '        Dim rm = Rooms(position)

  '        For d = 0 To rm.Doors.Count - 1

  '          Dim door = rm.Doors(d)

  '          Dim mat = FindDoormat(door.X, door.Y)
  '          If Me.Tile(mat.X, mat.Y) = CellType.StructureTunnel Then
  '            ' Already dug..
  '            Continue For
  '          End If

  '          Dim startDoorY = door.Y
  '          Dim startDoorX = door.X

  '          Dim coord = FindNearestDoor(rm, door)

  '          If coord Is Nothing Then Stop

  '          If coord IsNot Nothing Then

  '            Dim targetDoorY = coord.Y
  '            Dim targetDoorX = coord.X

  '            ' Set to tunnel to start just outside of the from door and 
  '            ' to end just outside of the target door...

  '            coord = FindDoormat(startDoorX, startDoorY)

  '            If coord Is Nothing Then Stop

  '            If coord IsNot Nothing Then

  '              Dim startY = coord.Y
  '              Dim startX = coord.X

  '              Dig(startX, startY, 0)

  '              ' Set tunnel to end just outside target door...

  '              coord = FindDoormat(targetDoorX, targetDoorY)

  '              If coord Is Nothing Then Stop

  '              If coord IsNot Nothing Then

  '                Dim targetY = coord.Y
  '                Dim targetX = coord.X

  '                Dim createdStepCount = CreateUtilityTunnel(startX, startY, targetX, targetY)
  '                If createdStepCount > 0 Then
  '                  ' Add tunnel to room connections...
  '                  coord = Room.GetConnectionNumber(startDoorY, startDoorX, targetDoorY, targetDoorX)
  '                  If coord Is Nothing Then Stop
  '                  RoomConnections.Add(coord)
  '                Else
  '                  Stop
  '                End If

  '              End If

  '            End If

  '          End If

  '        Next
  '      Next

  '      'now that all tunnels that will be randomly created are in place
  '      'make sure that each room is accessible somehow.

  '      If 1 = 0 Then
  '        VerifyAllRoomsAccessible()
  '      End If

  '    End Sub

  '    Private Enum Direction
  '      None
  '      Up
  '      Down
  '      Left
  '      Right
  '    End Enum

  '    Private Shared Function OppositeDirection(d As Direction) As Direction
  '      Select Case d
  '        Case Direction.Up : Return Direction.Down
  '        Case Direction.Left : Return Direction.Right
  '        Case Direction.Right : Return Direction.Left
  '        Case Direction.Down : Return Direction.Up
  '        Case Else
  '          Return Direction.None
  '      End Select
  '    End Function

  '    Private Function CreateUtilityTunnel%(startX%, startY%, targetX%, targetY%)

  '      Console.SetCursorPosition(startX, startY + 1)
  '      Console.ForegroundColor = ConsoleColor.Green
  '      Console.Write($"*")

  '      Console.SetCursorPosition(targetX, targetY + 1)
  '      Console.ForegroundColor = ConsoleColor.Red
  '      Console.Write($"*")

  '      Dim cx = startX
  '      Dim cy = startY

  '      Dim count = 1

  '      Dim previous As Direction = Direction.None

  '      Do

  '        If cx = targetX AndAlso cy = targetY Then Exit Do

  '        Dim diffX = cx - targetX
  '        Dim diffY = cy - targetY

  '        Dim primary As Direction
  '        Dim secondary As Direction

  '        If diffX = 0 Then
  '          primary = If(diffY > 0, Direction.Up, Direction.Down)
  '          secondary = If(diffX > 0, Direction.Left, Direction.Right)
  '        ElseIf diffY = 0 Then
  '          primary = If(diffX > 0, Direction.Left, Direction.Right)
  '          secondary = If(diffY > 0, Direction.Up, Direction.Down)
  '        ElseIf Math.Abs(diffY) > Math.Abs(diffX) Then
  '          primary = If(diffX > 0, Direction.Left, Direction.Right)
  '          secondary = If(diffY > 0, Direction.Up, Direction.Down)
  '        Else
  '          primary = If(diffY > 0, Direction.Up, Direction.Down)
  '          secondary = If(diffX > 0, Direction.Left, Direction.Right)
  '        End If

  '        Dim possible = DigDirections(cx, cy)

  '        For index = possible.Count - 1 To 0 Step -1
  '          If possible(index) = OppositeDirection(previous) Then
  '            possible.RemoveAt(index)
  '            Exit For
  '          End If
  '        Next

  '        If possible.Contains(primary) Then
  '          Select Case primary
  '            Case Direction.Up : cy -= 1
  '            Case Direction.Down : cy += 1
  '            Case Direction.Left : cx -= 1
  '            Case Direction.Right : cx += 1
  '          End Select
  '          previous = primary
  '        ElseIf possible.Contains(secondary) Then
  '          Select Case secondary
  '            Case Direction.Up : cy -= 1
  '            Case Direction.Down : cy += 1
  '            Case Direction.Left : cx -= 1
  '            Case Direction.Right : cx += 1
  '          End Select
  '          previous = secondary
  '        ElseIf possible.Contains(previous) Then
  '          Select Case previous
  '            Case Direction.Up : cy -= 1
  '            Case Direction.Down : cy += 1
  '            Case Direction.Left : cx -= 1
  '            Case Direction.Right : cx += 1
  '          End Select
  '        Else
  '          Stop
  '        End If

  '        If Me.Tile(cx, cy) = CellType.StructureTunnel Then Exit Do

  '        Dig(cx, cy, 1)

  '        count += 1

  '      Loop

  '      Console.ResetColor()

  '      Console.SetCursorPosition(startX, startY + 1)
  '      Console.Write($"#")

  '      Console.SetCursorPosition(targetX, targetY + 1)
  '      Console.Write($"#")

  '      Return count

  '      Dim result As Integer = 0

  '      'Dim aRoom As New Room

  '      Dim aStepsInDirectionCtr As Integer = 0
  '      Dim aSeekColumn As Integer = 0
  '      Dim aSeekRow As Integer = 0

  '      Dim horizontalDifference = startX - targetX
  '      Dim verticalDifference = startY - targetY

  '      Dim tunnelDirection As Direction
  '      Dim alternateDirection As Direction

  '      If Math.Abs(verticalDifference) > Math.Abs(horizontalDifference) Then
  '        If horizontalDifference > 0 Then
  '          tunnelDirection = Direction.Left
  '        Else
  '          tunnelDirection = Direction.Right
  '        End If
  '        If verticalDifference > 0 Then
  '          alternateDirection = Direction.Up
  '        Else
  '          alternateDirection = Direction.Down
  '        End If
  '      Else
  '        If verticalDifference > 0 Then
  '          tunnelDirection = Direction.Up
  '        Else
  '          tunnelDirection = Direction.Down
  '        End If
  '        If horizontalDifference > 0 Then
  '          alternateDirection = Direction.Left
  '        Else
  '          alternateDirection = Direction.Right
  '        End If
  '      End If

  '      ' We need to tunnel in a particular (primary) direction.
  '      ' If we hit an obsticle, we need to tunnel into a different (secondary) direction.
  '      ' Each dig into the alternate direction, need to try to tunnel into the primary direction.

  '      Dim aCurrentRow = startY
  '      Dim aCurrentColumn = startX

  '      For result = 1 To (Param.MapHeight * Param.MapWidth)

  '        Dim currentData = DigDirections(aCurrentColumn, aCurrentRow)

  '        If aCurrentRow = targetY Then
  '          If aCurrentColumn - targetX > 0 Then
  '            tunnelDirection = Direction.Left
  '          Else
  '            tunnelDirection = Direction.Right
  '          End If
  '        Else
  '          If aCurrentColumn = targetX Then
  '            If aCurrentRow - targetY > 0 Then
  '              tunnelDirection = Direction.Up
  '            Else
  '              tunnelDirection = Direction.Down
  '            End If
  '          End If
  '        End If
  '        If currentData.Contains(tunnelDirection) Then
  '          Select Case tunnelDirection
  '            Case Direction.Up
  '              aSeekRow = aCurrentRow - 1
  '              aSeekColumn = aCurrentColumn
  '            Case Direction.Down
  '              aSeekRow = aCurrentRow + 1
  '              aSeekColumn = aCurrentColumn
  '            Case Direction.Left
  '              aSeekRow = aCurrentRow
  '              aSeekColumn = aCurrentColumn - 1
  '            Case Direction.Right
  '              aSeekRow = aCurrentRow
  '              aSeekColumn = aCurrentColumn + 1
  '          End Select
  '        Else
  '          If tunnelDirection = alternateDirection Then
  '            'If 1 = 0 Then
  '            'need to tunnel around object
  '            Dim coord = TunnelAroundObstacle(aCurrentRow, aCurrentColumn, tunnelDirection)
  '            If coord IsNot Nothing Then
  '              aSeekRow = coord.Y
  '              aSeekColumn = coord.X
  '            End If
  '            aCurrentColumn = aSeekColumn
  '            aCurrentRow = aSeekRow
  '            'End If
  '          Else
  '            'TODO: Getting stuck here when it hits a wall where the 
  '            ' door is literally just around the corner...
  '            If currentData.Contains(alternateDirection) Then
  '              Select Case alternateDirection
  '                Case Direction.Up
  '                  aSeekRow = aCurrentRow - 1
  '                  aSeekColumn = aCurrentColumn
  '                Case Direction.Down
  '                  aSeekRow = aCurrentRow + 1
  '                  aSeekColumn = aCurrentColumn
  '                Case Direction.Left
  '                  aSeekRow = aCurrentRow
  '                  aSeekColumn = aCurrentColumn - 1
  '                Case Direction.Right
  '                  aSeekRow = aCurrentRow
  '                  aSeekColumn = aCurrentColumn + 1
  '              End Select
  '            Else
  '              'stuck
  '              Exit For
  '            End If
  '          End If
  '        End If

  '        Dig(aSeekColumn, aSeekRow, 1)

  '        aCurrentColumn = aSeekColumn
  '        aCurrentRow = aSeekRow

  '        If aCurrentRow = targetY AndAlso aCurrentColumn = targetX Then
  '          Exit For
  '        End If
  '        If aCurrentRow = targetY OrElse aCurrentColumn = targetX Then
  '          aStepsInDirectionCtr = 5
  '        End If
  '        'every X steps can change direction
  '        aStepsInDirectionCtr += 1
  '        If aStepsInDirectionCtr > 5 Then
  '          aStepsInDirectionCtr = 0
  '          horizontalDifference = aCurrentColumn - targetX
  '          verticalDifference = aCurrentRow - targetY
  '          If horizontalDifference = 0 Then
  '            If verticalDifference > 0 Then
  '              tunnelDirection = Direction.Up
  '            Else
  '              tunnelDirection = Direction.Down
  '            End If
  '          Else
  '            If verticalDifference = 0 Then
  '              If horizontalDifference > 0 Then
  '                tunnelDirection = Direction.Left
  '              Else
  '                tunnelDirection = Direction.Right
  '              End If
  '            Else
  '              If Math.Abs(verticalDifference) > Math.Abs(horizontalDifference) Then
  '                'if up-down farther then start left right
  '                If horizontalDifference > 0 Then
  '                  tunnelDirection = Direction.Left
  '                Else
  '                  tunnelDirection = Direction.Right
  '                End If
  '                If verticalDifference > 0 Then
  '                  alternateDirection = Direction.Up
  '                Else
  '                  alternateDirection = Direction.Down
  '                End If
  '              Else
  '                If verticalDifference > 0 Then
  '                  tunnelDirection = Direction.Up
  '                Else
  '                  tunnelDirection = Direction.Down
  '                End If
  '                If horizontalDifference > 0 Then
  '                  alternateDirection = Direction.Left
  '                Else
  '                  alternateDirection = Direction.Right
  '                End If
  '              End If
  '            End If
  '          End If
  '        End If
  '      Next

  '      Return result

  '    End Function

  '    Private Property Tile(x%, y%) As CellType
  '      Get

  '        If Not x.Between(0, Param.MapWidth - 1) Then
  '          Throw New ArgumentOutOfRangeException(NameOf(x))
  '        End If

  '        If Not y.Between(0, Param.MapHeight - 1) Then
  '          Throw New ArgumentOutOfRangeException(NameOf(y))
  '        End If

  '        Return MapCellData(y, x)

  '      End Get
  '      Set(value As CellType)

  '        If Not x.Between(0, Param.MapWidth - 1) Then
  '          Throw New ArgumentOutOfRangeException(NameOf(x))
  '        End If

  '        If Not y.Between(0, Param.MapHeight - 1) Then
  '          Throw New ArgumentOutOfRangeException(NameOf(y))
  '        End If

  '        MapCellData(y, x) = value

  '      End Set
  '    End Property

  '    ''' <summary>
  '    ''' Determine which directions (0 or more) are available for digging a tunnel.
  '    ''' </summary>
  '    ''' <param name="x"></param>
  '    ''' <param name="y"></param>
  '    ''' <returns>A list containing one or more up, down, left and right directions.</returns>
  '    Private Function DigDirections(x%, y%) As List(Of Direction)

  '      If Not x.Between(0, Param.MapWidth - 1) Then
  '        Throw New ArgumentOutOfRangeException(NameOf(x))
  '      End If

  '      If Not y.Between(0, Param.MapHeight - 1) Then
  '        Throw New ArgumentOutOfRangeException(NameOf(y))
  '      End If

  '      Dim result = New List(Of Direction)

  '      Dim modifiers = {ModifierUp,
  '                       ModifierDown,
  '                       ModifierLeft,
  '                       ModifierRight}

  '      For Each modifier In modifiers
  '        Dim seekX = x + modifier.X
  '        Dim seekY = y + modifier.Y
  '        If seekX.Between(0, Param.MapWidth - 1) AndAlso
  '           seekY.Between(0, Param.MapHeight - 1) Then
  '          Select Case Tile(seekX, seekY)
  '            Case CellType.Void, CellType.StructureTunnel
  '              Select Case modifier
  '                Case ModifierUp : result.Add(Direction.Up)
  '                Case ModifierDown : result.Add(Direction.Down)
  '                Case ModifierLeft : result.Add(Direction.Left)
  '                Case ModifierRight : result.Add(Direction.Right)
  '                Case Else
  '                  Stop
  '              End Select
  '            Case Else
  '          End Select
  '        End If
  '      Next

  '      Return result

  '    End Function

  '    Private Sub Dig(x%, y%, type%)

  '      If Not x.Between(0, Param.MapWidth - 1) Then
  '        Throw New ArgumentOutOfRangeException(NameOf(x))
  '      End If

  '      If Not y.Between(0, Param.MapHeight - 1) Then
  '        Throw New ArgumentOutOfRangeException(NameOf(y))
  '      End If

  '      Tile(x, y) = CellType.StructureTunnel

  '      If Debugger.IsAttached Then

  '        Console.ResetColor()

  '        Select Case type
  '          Case 0 : Console.ForegroundColor = ConsoleColor.DarkGreen
  '          Case 1 : Console.ForegroundColor = ConsoleColor.Gray
  '          Case 2 : Console.ForegroundColor = ConsoleColor.DarkRed
  '          Case Else
  '            Stop
  '        End Select

  '        Console.SetCursorPosition(x, y + 1)

  '        SpinnerIndex += 1 : If SpinnerIndex > SpinnerChar.Length - 1 Then SpinnerIndex = 0
  '        Console.Write(SpinnerChar(SpinnerIndex))

  '        Threading.Thread.Sleep(100)
  '        Console.SetCursorPosition(x, y + 1)
  '        Console.Write("#"c)

  '      End If

  '    End Sub

  '    'Private SpinnerChar As String = "\|/-"
  '    Private ReadOnly SpinnerChar As String = "|+|-+-"
  '    Private SpinnerIndex As Integer = 0

  '    Private Function FindDoormat(x%, y%) As Coordinate

  '      If Not x.Between(0, Param.MapWidth - 1) Then
  '        Throw New ArgumentOutOfRangeException(NameOf(x))
  '      End If

  '      If Not y.Between(0, Param.MapHeight - 1) Then
  '        Throw New ArgumentOutOfRangeException(NameOf(y))
  '      End If

  '      Dim modifiers = {ModifierLeft,
  '                       ModifierUp,
  '                       ModifierRight,
  '                       ModifierDown}

  '      For Each modifier In modifiers
  '        Dim seekX = x + modifier.X
  '        Dim seekY = y + modifier.Y
  '        If seekX.Between(0, Param.MapWidth - 1) AndAlso
  '           seekY.Between(0, Param.MapHeight - 1) Then
  '          Select Case Tile(seekX, seekY)
  '            Case CellType.Void, CellType.StructureTunnel
  '              Return New Coordinate(seekX, seekY)
  '            Case Else
  '          End Select
  '        End If
  '      Next

  '      Return Nothing

  '    End Function

  '    ''' <summary>
  '    ''' Try to find a door to tunnel to from the room and door provided
  '    ''' Try to find a next in one of the grid cells next to this one
  '    ''' else just find one
  '    ''' </summary>
  '    ''' <param name="cell"></param>
  '    ''' <param name="coord"></param>
  '    ''' <returns></returns>
  '    Private Function FindNearestDoor(cell As Room, coord As Door) As Coordinate

  '      ' Given an x/y find the nearest door not part of the same room.

  '      Dim possible As Coordinate = Nothing

  '      Dim pdx = Integer.MaxValue \ 2
  '      Dim pdy = Integer.MaxValue \ 2
  '      For Each r In Rooms
  '        If r IsNot cell Then
  '          For Each d In r.Doors
  '            Dim dx = Math.Abs(coord.X - d.X)
  '            Dim dy = Math.Abs(coord.Y - d.Y)
  '            If dx + dy < pdx + pdy Then
  '              pdx = dx
  '              pdy = dy
  '              possible = New Coordinate(d.X, d.Y)
  '            End If
  '          Next
  '        End If
  '      Next

  '      If possible IsNot Nothing Then
  '        Return possible
  '      End If

  '      Dim foundList As New List(Of Room)

  '      For gridRow = 1 To Param.GridRowCount

  '        For gridColumn = 1 To Param.GridColumnCount

  '          Dim fromPosition = Param.GridPositionToIndex(gridColumn, gridRow)

  '          If fromPosition = cell.GridPosition Then

  '            If gridRow > 1 Then

  '              Dim toPosition = Param.GridPositionToIndex(gridColumn, gridRow - 1)

  '              For r = 0 To Rooms.Count - 1

  '                If toPosition = Rooms(r).GridPosition Then

  '                  Dim connection = New Coordinate(cell.GridPosition, Rooms(r).GridPosition)
  '                  Dim reverseConnection = New Coordinate(Rooms(r).GridPosition, cell.GridPosition)
  '                  Dim isFound = False

  '                  For c = 0 To RoomConnections.Count - 1
  '                    If connection = RoomConnections(c) OrElse
  '                       reverseConnection = RoomConnections(c) Then
  '                      isFound = True
  '                      Exit For
  '                    End If
  '                  Next

  '                  If Not isFound Then
  '                    ' Do not use room if already has a connection...
  '                    foundList.Add(Rooms(r))
  '                  End If

  '                  Exit For

  '                End If

  '              Next

  '            End If

  '            If gridRow < Param.GridRowCount Then

  '              Dim toPosition = Param.GridPositionToIndex(gridColumn, gridRow + 1)

  '              For r = 0 To Rooms.Count - 1

  '                If toPosition = Rooms(r).GridPosition Then

  '                  Dim connection = New Coordinate(cell.GridPosition, Rooms(r).GridPosition)
  '                  Dim reverseConnection = New Coordinate(Rooms(r).GridPosition, cell.GridPosition)
  '                  Dim isFound = False

  '                  For c = 0 To RoomConnections.Count - 1
  '                    If connection = RoomConnections(c) OrElse
  '                       reverseConnection = RoomConnections(c) Then
  '                      isFound = True
  '                      Exit For
  '                    End If
  '                  Next

  '                  If Not isFound Then
  '                    ' Do not use room if already has a connection...
  '                    foundList.Add(Rooms(r))
  '                  End If

  '                  Exit For

  '                End If

  '              Next

  '            End If

  '            If gridColumn > 1 Then

  '              Dim toPosition = Param.GridPositionToIndex(gridColumn - 1, gridRow)

  '              For r = 0 To Rooms.Count - 1

  '                If toPosition = Rooms(r).GridPosition Then

  '                  Dim connection = New Coordinate(cell.GridPosition, Rooms(r).GridPosition)
  '                  Dim reverseConnection = New Coordinate(Rooms(r).GridPosition, cell.GridPosition)
  '                  Dim isFound = False

  '                  For c As Integer = 0 To RoomConnections.Count - 1
  '                    If connection = RoomConnections(c) OrElse
  '                       reverseConnection = RoomConnections(c) Then
  '                      isFound = True
  '                      Exit For
  '                    End If
  '                  Next

  '                  If Not isFound Then
  '                    ' Do not use room if already has a connection...
  '                    foundList.Add(Rooms(r))
  '                  End If

  '                  Exit For

  '                End If

  '              Next

  '            End If

  '            If gridColumn < Param.GridColumnCount Then

  '              Dim toPosition = Param.GridPositionToIndex(gridColumn + 1, gridRow)

  '              For r = 0 To Rooms.Count - 1

  '                If toPosition = Rooms(r).GridPosition Then

  '                  Dim connection = New Coordinate(cell.GridPosition, Rooms(r).GridPosition)
  '                  Dim reverseConnection = New Coordinate(Rooms(r).GridPosition, cell.GridPosition)
  '                  Dim isFound = False

  '                  For connect = 0 To RoomConnections.Count - 1
  '                    If connection = RoomConnections(connect) OrElse
  '                       reverseConnection = RoomConnections(connect) Then
  '                      isFound = True
  '                      Exit For
  '                    End If
  '                  Next

  '                  If Not isFound Then
  '                    ' Do not use room if already has a connection...
  '                    foundList.Add(Rooms(r))
  '                  End If

  '                  Exit For

  '                End If

  '              Next

  '            End If

  '          End If

  '        Next

  '      Next

  '      ' Now we know what ways we can look...
  '      If foundList.Count > 0 Then
  '        Dim toPosition = Param.Randomizer.Next(foundList.Count) '+ 1 do not need +1 because list is zero based
  '        Return foundList(toPosition).Doors(0)
  '      Else
  '        ' Need to pick any random room...
  '        For pass = 0 To 10
  '          Dim toPosition = Param.Randomizer.Next(Rooms.Count)
  '          If Not Rooms(toPosition).GridPosition = cell.GridPosition Then
  '            Return Rooms(toPosition).Doors(0)
  '          End If
  '        Next
  '      End If

  '      Return Nothing

  '    End Function

  '    'Private Function GetCoordinateString$(row%, column%)

  '    '  Dim currentData = $"0{row}"
  '    '  Dim aReturnValue = currentData.Substring(currentData.Length - 2)
  '    '  currentData = $"0{column}"
  '    '  aReturnValue &= currentData.Substring(currentData.Length - 2)
  '    '  Return aReturnValue

  '    'End Function

  '    'Private Function GetRoom(number%) As Room

  '    '  If Not number.Between(1, Param.GridRowCount * Param.GridColumnCount) Then
  '    '    Throw New ArgumentOutOfRangeException("number")
  '    '  End If

  '    '  For Each rm In Rooms
  '    '    If rm.GridPosition = number Then
  '    '      Return rm
  '    '    End If
  '    '  Next

  '    '  Return Nothing

  '    'End Function

  '    Private Function GetRoom(x%, y%) As Room

  '      If Not x.Between(0, Param.MapWidth - 1) Then
  '        Throw New ArgumentOutOfRangeException(NameOf(x))
  '      End If

  '      If Not y.Between(0, Param.MapHeight - 1) Then
  '        Throw New ArgumentOutOfRangeException(NameOf(y))
  '      End If

  '      Try

  '        Dim row = Param.GridRow(y)
  '        Dim col = Param.GridColumn(x)

  '        If row = 0 OrElse col = 0 Then
  '          ' Coords provided do not clearly identify a zone.
  '          Return Nothing
  '        End If

  '        Dim number = ((row - 1) * Param.GridColumnCount) + col

  '        For Each rm In Rooms
  '          If rm.GridPosition = number Then
  '            Return rm
  '          End If
  '        Next

  '        Return Nothing

  '        'Catch ex As Exception
  '        '  ' We have a valid x, y coordinate; however,
  '        '  ' we must be "between" the rooms...
  '        '  Return Nothing
  '      Finally

  '      End Try

  '    End Function

  '    '''' <summary>
  '    '''' Find out which grid cell a specific point is within 
  '    '''' There may or not be a room there
  '    '''' </summary>
  '    '''' <param name="x">The column of the specific point</param>
  '    '''' <param name="y">The row of the specific point</param>
  '    '''' <returns>
  '    '''' the number of the room at the point would be at the point requested if it exists
  '    '''' </returns>
  '    'Private Function GetRoomNumberFromXY%(x%, y%)

  '    '  'Dim sx = If(gridColumn = 1, 0, If(gridColumn = 2, 27, 55))
  '    '  'Dim sy = If(gridRow = 1, 0, If(gridRow = 2, 7, 15))

  '    '  Select Case x
  '    '    Case 0 To 25
  '    '      Select Case y
  '    '        Case 0 To 5 : Return 1
  '    '        Case 7 To 13 : Return 2
  '    '        Case 15 To 20 : Return 3
  '    '        Case Else
  '    '          Stop
  '    '      End Select
  '    '    Case 27 To 53
  '    '      Select Case y
  '    '        Case 0 To 5 : Return 4
  '    '        Case 7 To 13 : Return 5
  '    '        Case 15 To 20 : Return 6
  '    '        Case Else
  '    '          Stop
  '    '      End Select
  '    '    Case 55 To 80
  '    '      Select Case y
  '    '        Case 0 To 5 : Return 7
  '    '        Case 7 To 13 : Return 8
  '    '        Case 15 To 20 : Return 9
  '    '        Case Else
  '    '          Stop
  '    '      End Select
  '    '    Case Else
  '    '      Stop
  '    '  End Select

  '    '  'Dim result = 0

  '    '  'For r = 0 To Param.MapLevelGridRowMax - 1
  '    '  '  For c = 0 To Param.MapLevelGridColumnMax - 1
  '    '  '    Dim top = r * Param.MapGridCellHeight
  '    '  '    Dim left = c * Param.MapGridCellWidth
  '    '  '    Dim bottom = top + Param.MapGridCellHeight
  '    '  '    Dim right = left + Param.MapGridCellWidth
  '    '  '    If top <= y AndAlso bottom >= y AndAlso left <= x AndAlso right >= x Then
  '    '  '      result = r * 3 + c + 1
  '    '  '      Exit For
  '    '  '    End If
  '    '  '  Next
  '    '  '  If result > 0 Then
  '    '  '    Exit For
  '    '  '  End If
  '    '  'Next

  '    '  'Return result

  '    'End Function

  '    'Private Function IsBlockingCell(x%, y%) As Boolean

  '    '  If x >= 0 AndAlso x <= Param.MapWidth AndAlso
  '    '     y >= 0 AndAlso y < Param.MapHeight Then
  '    '    Select Case Me.MapCellData(y, x)
  '    '      Case CellType.Void : Return True
  '    '      Case CellType.StructureWallTopLeftCorner : Return True
  '    '      Case CellType.StructureWallTopRightCorner : Return True
  '    '      Case CellType.StructureWallSide : Return True
  '    '      Case CellType.StructureWallTopBottom : Return True
  '    '      Case CellType.StructureWallBottomLeftCorner : Return True
  '    '      Case CellType.StructureWallBottomRightCorner : Return True
  '    '      Case Else
  '    '    End Select
  '    '  End If

  '    '  Return False

  '    'End Function

  '    ''' <summary>
  '    ''' When a tunnel is being dug, if it hits an obstruction on the way to its goal, 
  '    ''' it needs to dig around the obstruction so that it can continue in the desired direction.
  '    ''' </summary>
  '    ''' <param name="currentRow"></param>
  '    ''' <param name="currentColumn"></param>
  '    ''' <param name="direction"></param>
  '    ''' 
  '    ''' 
  '    ''' <returns></returns>
  '    Private Function TunnelAroundObstacle(currentRow%, currentColumn%, direction As Direction) As Coordinate

  '      Dim aReturnValue As Coordinate = New Coordinate(0, 0)
  '      'Dim isFound As Boolean = True
  '      'Dim aStepCtr As Integer = 0
  '      'Dim aAvoidanceDirection As Direction
  '      'Dim aBlockingRoomNumber As Integer = 0
  '      'Dim aBlockingRoom As New Room
  '      Dim aDistanceOne As Integer '= 0
  '      Dim aDistanceTwo As Integer '= 0
  '      Dim aStatus As String = "B" ' B = going around block, C = continuing past block

  '      'Dim availableDirections = Me.DigDirections(currentColumn, currentRow)

  '      Dim aCurrentY = currentRow
  '      Dim aCurrentX = currentColumn

  '      ' Make sure current direction is blocked...

  '      Dim seekX = 0
  '      Dim seekY = 0

  '      Select Case direction
  '        Case Direction.Up
  '          seekY = aCurrentY - 1
  '          seekX = aCurrentX
  '        Case Direction.Down
  '          seekY = aCurrentY + 1
  '          seekX = aCurrentX
  '        Case Direction.Left
  '          seekY = aCurrentY
  '          seekX = aCurrentX - 1
  '        Case Direction.Right
  '          seekY = aCurrentY
  '          seekX = aCurrentX + 1
  '        Case Else
  '          Stop
  '      End Select

  '      Dim tile = Me.Tile(seekX, seekY)

  '      Dim isFound As Boolean '= True

  '      If tile = CellType.Void OrElse tile = CellType.StructureTunnel Then

  '        ' Original direction clear...
  '        Dig(seekX, seekY, 2)

  '        ' Take the step in that direction then...
  '        'isFound = False ' The way is clear so exit...
  '        aCurrentY = seekY
  '        aCurrentX = seekX
  '        aReturnValue = New Coordinate(aCurrentX, aCurrentY)

  '      Else

  '        Dim aBlockingRoom = GetRoom(seekX, seekY)

  '        Dim aAvoidanceDirection As Direction

  '        If aBlockingRoom IsNot Nothing Then
  '          ' Find out the shortest side of the blocking room and go that way
  '          If direction = Direction.Down OrElse direction = Direction.Up Then
  '            aDistanceOne = aCurrentX - If(aBlockingRoom?.ActualMapLeftLocation, 0)
  '            aDistanceTwo = (aBlockingRoom.ActualMapLeftLocation + aBlockingRoom.Width) - aCurrentX
  '            If aDistanceOne > aDistanceTwo Then
  '              aAvoidanceDirection = Direction.Right
  '            Else
  '              aAvoidanceDirection = Direction.Left
  '            End If
  '          Else
  '            aDistanceOne = aCurrentY - If(aBlockingRoom?.ActualMapTopLocation, 0)
  '            aDistanceTwo = (aBlockingRoom.ActualMapTopLocation + aBlockingRoom.Height) - aCurrentY
  '            If aDistanceOne > aDistanceTwo Then
  '              aAvoidanceDirection = Direction.Down
  '            Else
  '              aAvoidanceDirection = Direction.Up
  '            End If
  '          End If
  '        End If

  '        isFound = True

  '        Dim aStepCtr = 0

  '        Do While isFound AndAlso aStepCtr < Param.MapHeight * Param.MapWidth

  '          If Not aCurrentX.Between(0, Param.MapWidth - 1) OrElse
  '             Not aCurrentY.Between(0, Param.MapHeight - 1) Then
  '            Stop
  '          End If

  '          Dim availableDirections = DigDirections(aCurrentX, aCurrentY)

  '          If availableDirections.Count > 0 Then
  '            'there are available moves from here
  '            If availableDirections.Contains(direction) Then
  '              'we can continue on our way
  '              'do so until block we were going around is cleared
  '              Select Case aAvoidanceDirection
  '                Case Direction.Down
  '                  If availableDirections.Contains(Direction.Up) Then
  '                    aStatus = "C"
  '                  Else
  '                    aStatus = "D" 'don going around
  '                  End If
  '                Case Direction.Up
  '                  If availableDirections.Contains(Direction.Down) Then
  '                    aStatus = "C"
  '                  Else
  '                    aStatus = "D" 'don going around
  '                  End If
  '                Case Direction.Left
  '                  If availableDirections.Contains(Direction.Right) Then
  '                    aStatus = "C"
  '                  Else
  '                    aStatus = "D" 'don going around
  '                  End If
  '                Case Direction.Right
  '                  If availableDirections.Contains(Direction.Left) Then
  '                    aStatus = "C"
  '                  Else
  '                    aStatus = "D" 'don going around
  '                  End If
  '              End Select
  '              If aStatus = "D" Then
  '                'we are around block so exit 
  '                aReturnValue = New Coordinate(aCurrentX, aCurrentY)
  '                isFound = True 'found a door so exit
  '              Else
  '                'we should continue
  '                Select Case direction
  '                  Case Direction.Up
  '                    seekY = aCurrentY - 1
  '                    seekX = aCurrentX
  '                  Case Direction.Down
  '                    seekY = aCurrentY + 1
  '                    seekX = aCurrentX
  '                  Case Direction.Left
  '                    seekY = aCurrentY
  '                    seekX = aCurrentX - 1
  '                  Case Direction.Right
  '                    seekY = aCurrentY
  '                    seekX = aCurrentX + 1
  '                  Case Else
  '                    Stop
  '                End Select
  '                Dig(seekX, seekY, 2)
  '                aCurrentX = seekX
  '                aCurrentY = seekY
  '              End If
  '            Else

  '              ' *** The following code doesn't make any sense...
  '              '     It makes no sense to go in a direction that we are trying
  '              '     to avoid... right?
  '              '     We end up here because we can no longer move in the direction
  '              '     we were traveling...
  '              '     Wouldn't we change to a direction that we *can* go?

  '              'If 1 = 0 Then

  '              'if cannot go in current desired direction then continue in avoidance direction
  '              If availableDirections.Contains(aAvoidanceDirection) Then
  '                Select Case aAvoidanceDirection
  '                  Case Direction.Up
  '                    seekY = aCurrentY - 1
  '                    seekX = aCurrentX
  '                  Case Direction.Down
  '                    seekY = aCurrentY + 1
  '                    seekX = aCurrentX
  '                  Case Direction.Left
  '                    seekY = aCurrentY
  '                    seekX = aCurrentX - 1
  '                  Case Direction.Right
  '                    seekY = aCurrentY
  '                    seekX = aCurrentX + 1
  '                  Case Else
  '                    Stop
  '                End Select
  '                Dig(seekX, seekY, 2)
  '                aCurrentX = seekX
  '                aCurrentY = seekY
  '              Else
  '                ' Can't move in the avoidance direction either...
  '                ' What do we do????
  '                Dim entry = Param.Randomizer.Next(0, availableDirections.Count)
  '                direction = availableDirections(entry)
  '              End If

  '              'End If

  '            End If
  '          End If
  '          aStepCtr += 1
  '        Loop
  '      End If

  '      Return aReturnValue

  '    End Function

  '    ''' <summary>
  '    ''' The the data from a recently created random room and put it in the MapData() object used by all the level functions
  '    ''' </summary>
  '    ''' <param name="rm">The Room object to be placed within the MapData array.</param>
  '    ''' <returns>A string with the top and left point of the room encoded as T-L</returns>
  '    Private Function UpdateMap$(rm As Room)

  '      Dim aReturnValue As String '= ""
  '      'Dim isDoorTop As Boolean '= False
  '      'Dim isDoorLeft As Boolean '= False
  '      'Dim isDoorRight As Boolean '= False
  '      'Dim isDoorBottom As Boolean '= False
  '      'Dim isAnyDoor As Boolean '= False
  '      'Dim rNumber As Integer '= 0
  '      'Dim xNumber As Integer '= 0
  '      Dim aDataArray() As String = {""}
  '      'Dim aFirstPtr As Integer '= 0
  '      'Dim aSecondPtr As Integer '= 0
  '      Dim aTopPtr As Integer = Param.MapHeight
  '      Dim aLeftPtr As Integer = Param.MapWidth
  '      'Dim aDirectionPtr As String '= ""
  '      'Dim aDoorCharacter As Integer '= 0
  '      Dim x As Integer '= 0
  '      Dim y As Integer '= 0

  '      If 1 = 0 Then

  '        'now update the mapdata with the random room 
  '        'rptr will cycle from 1 to height
  '        'cptr will cycle from 1 to width
  '        For r As Integer = 1 To (rm.Height)
  '          For c As Integer = 1 To (rm.Width)

  '            Dim ch = CellType.Void

  '            If r = 1 AndAlso c = 1 Then
  '              ch = CellType.StructureWallTopLeftCorner
  '            End If
  '            If r = 1 AndAlso c > 1 AndAlso c < rm.Width Then
  '              ch = CellType.StructureWallTopBottom
  '            End If
  '            If r = 1 AndAlso c = rm.Width Then
  '              ch = CellType.StructureWallTopRightCorner
  '            End If

  '            If r > 1 AndAlso r < rm.Height AndAlso (c = 1) Then
  '              ch = CellType.StructureWallSide
  '            End If
  '            If r > 1 AndAlso r < rm.Height AndAlso (c = rm.Width) Then
  '              ch = CellType.StructureWallSide
  '            End If

  '            If r = rm.Height AndAlso c = 1 Then
  '              ch = CellType.StructureWallBottomLeftCorner
  '            End If
  '            If r = rm.Height AndAlso c > 1 AndAlso c < rm.Width Then
  '              ch = CellType.StructureWallTopBottom
  '            End If
  '            If r = rm.Height AndAlso c = (rm.Width) Then
  '              ch = CellType.StructureWallBottomRightCorner
  '            End If
  '            If r > 1 AndAlso r < rm.Height AndAlso c > 1 AndAlso c < rm.Width Then
  '              ch = CellType.StructureFloor
  '            End If

  '            ''ensure that pointer into map data takes into account which grid cell is being processed
  '            y = (r + rm.MapTopLocation + ((rm.MapGridRowLocation - 1) * 7)) - 1
  '            x = (c + rm.MapLeftLocation + ((rm.MapGridColumnLocation - 1) * 26)) - 1

  '            MapCellData(y, x) = CType(ch, CellType)
  '            'Me.MapCellVisibility(y, x) = Not Me.m_IsFogOfWarActive ' testing m_TestMode 'testing true makes all visible

  '            If y < aTopPtr Then
  '              aTopPtr = y
  '            End If
  '            If x < aLeftPtr Then
  '              aLeftPtr = x
  '            End If
  '          Next
  '        Next

  '      Else

  '        For r = 0 To rm.Height - 1
  '          For c = 0 To rm.Width - 1

  '            Dim cell As CellType

  '            If r = 0 Then
  '              If c = 0 Then
  '                cell = CellType.StructureWallTopLeftCorner
  '              ElseIf c = rm.Width - 1 Then
  '                cell = CellType.StructureWallTopRightCorner
  '              Else
  '                cell = CellType.StructureWallTopBottom
  '              End If
  '            ElseIf r = rm.Height - 1 Then
  '              If c = 0 Then
  '                cell = CellType.StructureWallBottomLeftCorner
  '              ElseIf c = rm.Width - 1 Then
  '                cell = CellType.StructureWallBottomRightCorner
  '              Else
  '                cell = CellType.StructureWallTopBottom
  '              End If
  '            ElseIf c = 0 Then
  '              cell = CellType.StructureWallSide
  '            ElseIf c = rm.Width - 1 Then
  '              cell = CellType.StructureWallSide
  '            Else
  '              cell = CellType.StructureFloor
  '            End If

  '            y = rm.ActualMapTopLocation + r
  '            x = rm.ActualMapLeftLocation + c

  '            ' ensure that pointer into map data takes into account which grid cell is being processed
  '            'yPtr = (r + rm.MapTopLocation + ((rm.MapGridRowLocation - 1) * 7)) - 1
  '            'xPtr = (c + rm.MapLeftLocation + ((rm.MapGridColumnLocation - 1) * 26)) - 1

  '            MapCellData(y, x) = cell
  '            'Me.MapCellVisibility(y, x) = Not Me.m_IsFogOfWarActive

  '            ' ?????
  '            If y < aTopPtr Then
  '              aTopPtr = y
  '            End If
  '            If x < aLeftPtr Then
  '              aLeftPtr = x
  '            End If

  '          Next
  '        Next

  '      End If

  '      If 1 = 1 Then

  '        'now show the doors
  '        For Each doorData As Coordinate In rm.Doors
  '          y = doorData.Y
  '          x = doorData.X
  '          'Dim ct = doorData.Type
  '          MapCellData(y, x) = CellType.StructureDoor ' ct
  '          'Me.MapCellVisibility(y, x) = Not Me.m_IsFogOfWarActive ' testing m_TestMode 'testing true makes all visible

  '        Next

  '      End If

  '      aReturnValue = aTopPtr.ToString & "-" & aLeftPtr.ToString

  '      Return aReturnValue

  '    End Function

  '    Private Sub VerifyAllRoomsAccessible()

  '      ' Now examine the doors in each room that is not already in the connections list.
  '      ' Follow the tunnel from each door to see if it connects to a room that is not 
  '      ' showing in the connection list and add if found.
  '      ' If no connection found, add a utility tunnel to room left or right. 
  '      ' Choosing left-right utility tunnel connections to help with connecting all rooms.
  '      '
  '      ' Choose room with least number of connections in list. Choose the door in that room 
  '      ' closest to the room coming from.
  '      ' Utility direct tunnels should start from the first point outside the their door and 
  '      ' head directly to their destination. 
  '      '
  '      ' If a tunnel is found before the target door, follow it to make sure that it goes 
  '      ' to a door, even though tunnels do not exist without a door.
  '      ' If obstructed by a wall, step around it in the way away from the door (because 
  '      ' that will get to a corner sooner) and follow wall until can turn toward door.

  '      'Build list of connections between rooms

  '      Dim connectionList As New List(Of Coordinate)

  '      For Each connection In RoomConnections
  '        connectionList.Add(connection)
  '        If Not connection.X = connection.Y Then
  '          connectionList.Add(New Coordinate(connection.Y, connection.X))
  '        End If
  '      Next

  '      Dim cycle = 0

  '      Do While cycle < Param.MapHeight * Param.MapWidth

  '        ' Finally, walk through all the rooms to ensure that every room is accessible...

  '        Dim stepList = New List(Of Integer)

  '        For Each connection In connectionList
  '          Dim isFound = False
  '          For Each foundStep In stepList
  '            If foundStep = connection.X Then
  '              isFound = True
  '              Exit For
  '            End If
  '          Next
  '          If Not isFound Then
  '            stepList.Add(connection.X)
  '          End If
  '        Next

  '        'now aStepList contains a list of all the rooms that exist. 
  '        'Take first room in asteplist and put in atunnelroomlist
  '        '   remove the room from asteplist
  '        'while tunnelrooms contains entries and stepctr<max
  '        '  Take first room from tunnelrooms and make working room
  '        '     remove from tunnelrooms
  '        '     Add connections from working room to tunnelrooms if they are not already there and if they still exist in steplist
  '        '         rooms no longer in steplist have already been worked
  '        'loop until all rooms in tunnel rooms have been worked
  '        'any rooms remaining in steplist now need to be connected to a room not in step list
  '        'createutilitytunnel
  '        'Then go through this process again until steplist is empty at the end of the run.

  '        If stepList.Count > 0 Then

  '          Dim tunnelStepList = New List(Of Integer) From {
  '            stepList(0) 'TODO does this work the same as in prior version???
  '          }

  '          stepList.RemoveAt(0)

  '          Dim currentStep = tunnelStepList(0)

  '          tunnelStepList.RemoveAt(0)

  '          Dim count = 0

  '          Do While count < Param.MapHeight * Param.MapWidth

  '            For Each connection In connectionList

  '              Dim x = connection.X
  '              Dim y = connection.Y

  '              If x = currentStep Then

  '                ' This is a connection from the working room.
  '                ' See if it goes to a room still in stepList, but not yet in tunnelList...
  '                ' If so, remove it from stepList and add it to tunnelList.

  '                For i = 0 To stepList.Count - 1

  '                  If stepList(i) = y Then

  '                    ' Still in stepList so remove and add to tunnelList if not already there...

  '                    Dim isFound = False

  '                    For Each foundItem In tunnelStepList
  '                      If foundItem = y Then
  '                        isFound = True ' Already in tunnelList...
  '                        Exit For
  '                      End If
  '                    Next

  '                    If Not isFound Then
  '                      ' Not there, so add to tunnellist for further processing...
  '                      tunnelStepList.Add(stepList(i))
  '                    End If

  '                    stepList.RemoveAt(i)

  '                    Exit For

  '                  End If

  '                Next

  '              End If

  '            Next

  '            If tunnelStepList.Count = 0 Then
  '              Exit Do
  '            Else
  '              currentStep = tunnelStepList(0)
  '              tunnelStepList.RemoveAt(0)
  '            End If

  '            ' Try again until finished...

  '            count += 1

  '          Loop

  '          ' If stepList has any rooms left after tunnelList emptied then we need to add a tunnel...
  '          If stepList.Count = 0 Then
  '            Exit Do
  '          Else

  '            ' Need to tunnel between first room left in stepList to some room not in stepList...

  '            Dim fromDoorX = 0
  '            Dim fromDoorY = 0
  '            Dim toDoorX = 0
  '            Dim toDoorY = 0
  '            Dim fromRoom = stepList(0)
  '            Dim toRoom = 0

  '            For Each foundRoom In Rooms
  '              If foundRoom.GridPosition = fromRoom Then
  '                ' Get a door in this room to start from...
  '                fromDoorY = foundRoom.Doors(0).Y
  '                fromDoorX = foundRoom.Doors(0).X
  '                Exit For
  '              End If
  '            Next

  '            ' Now find a room not in stepList to connect to...
  '            ' Try rooms next door first...

  '            For Each foundRoom As Room In Rooms

  '              Dim isFound = False

  '              For Each foundString In stepList
  '                If foundString = foundRoom.GridPosition Then
  '                  isFound = True
  '                  Exit For
  '                End If
  '              Next

  '              If Not isFound Then
  '                ' This may be it!
  '                If (fromRoom + 1 = foundRoom.GridPosition) OrElse
  '                   (fromRoom - 1 = foundRoom.GridPosition) OrElse
  '                   (fromRoom - 3 = foundRoom.GridPosition) OrElse
  '                   (fromRoom + 3 = foundRoom.GridPosition) Then
  '                  toRoom = foundRoom.GridPosition
  '                  toDoorY = foundRoom.Doors(0).Y
  '                  toDoorX = foundRoom.Doors(0).X
  '                  Exit For
  '                End If
  '              End If

  '            Next

  '            If toRoom = 0 Then

  '              ' If could not find room next door then take any room...

  '              For Each foundRoom In Rooms

  '                Dim isFound = False

  '                For Each position In stepList
  '                  If position = foundRoom.GridPosition Then
  '                    isFound = True
  '                    Exit For
  '                  End If
  '                Next

  '                If Not isFound Then
  '                  ' This may be it!
  '                  toRoom = foundRoom.GridPosition
  '                  toDoorY = foundRoom.Doors(0).Y
  '                  toDoorX = foundRoom.Doors(0).X
  '                  Exit For
  '                End If

  '              Next

  '            End If

  '            Dim createdStepCount = CreateUtilityTunnel(fromDoorX, fromDoorY, toDoorX, toDoorY)

  '            If createdStepCount > 0 Then
  '              ' Add new tunnel to connectionslist and continue..
  '              connectionList.Add(New Coordinate(fromRoom, toRoom))
  '              connectionList.Add(New Coordinate(toRoom, fromRoom))
  '            End If

  '          End If

  '        Else

  '          Exit Do

  '        End If

  '        Debug.WriteLine("ConnectionList")
  '        For Each currentData In connectionList
  '          Debug.WriteLine(currentData)
  '        Next

  '        cycle += 1

  '      Loop

  '    End Sub

  '  End Class

End Namespace