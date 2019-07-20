Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Map

    Private ReadOnly ModifierUp As New Coordinate(0, -1)
    Private ReadOnly ModifierDown As New Coordinate(0, +1)
    Private ReadOnly ModifierLeft As New Coordinate(-1, 0)
    Private ReadOnly ModifierRight As New Coordinate(+1, 0)

    ' The cell type value of the square the character is currently location at...
    'Private m_CellTypeBeforeCharacterEntered As Integer = -1

    'Private ReadOnly m_IsFogOfWarActive As Boolean = True

    'Private m_EntryStairGrid As Coordinate
    'Private m_ExitStairGrid As Coordinate

    Public Sub New() 'depth%)
      'Me.m_TestMode = generate
      'Me.CurrentMapLevel = depth
      Me.Initialize() 'generate)
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

      Me.Rooms = New List(Of Room)
      Me.RoomConnections = New List(Of Coordinate)

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

      Me.EntryStairLocation = New Coordinate(currentX, currentY) ' Me.GetCoordinateString(currentY, currentX)
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

      Me.ExitStairLocation = New Coordinate(currentX, currentY) ' Me.GetCoordinateString(currentY, currentX)
      'Me.ExitStairGrid = New Coordinate(gridX, gridY) ' gridY.ToString & gridX.ToString

      'End If

      Me.CreateRandomLevel()

    End Sub

    'Public Function IsEntryCell(x%, y%) As Boolean
    '  Return Me.MapCellData(y, x) = CellType.Hero 'StructureStairsUp
    'End Function

    'Public Function IsExitCell(x%, y%) As Boolean
    '  Return Me.MapCellData(y, x) = CellType.StructureStairsDown
    'End Function

    Private Function MapCellTypeToChar(type As CellType) As Char
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
        For room = 0 To Me.Rooms.Count - 1
          Dim grid = ((Me.Rooms(room).MapGridRowLocation - 1) * 3) + Me.Rooms(room).MapGridColumnLocation
          If grid = light + 1 Then
            ' This room is at the currently pointed at location in the map grid...
            isLit = Me.Rooms(room).IsLit
            Exit For
          End If
        Next
        result.Lights(light) = isLit
      Next

      For r = 0 To Param.MapHeight - 1
        For c = 0 To Param.MapWidth - 1
          Dim ch = Me.MapCellTypeToChar(Me.MapCellData(r, c))
          result.Map(r, c) = New Tile(ch) With {.Explored = True}
        Next
      Next

      Return result

    End Function

    Private Function CheckIfRandomRoomExists(gridRow%, gridColumn%) As String

      Dim entryRow As Integer = 0
      Dim entryColumn As Integer = 0
      Dim entryRowPointer As Integer = 0
      Dim entryColumnPointer As Integer = 0
      Dim exitRow As Integer = 0
      Dim exitColumn As Integer = 0
      Dim exitRowPointer As Integer = 0
      Dim exitColumnPointer As Integer = 0
      Dim roomRowOffset As Integer = 0 'offset from top side of cell
      Dim roomColumnOffset As Integer = 0 'offset from left side of cell
      Dim isOK As Boolean = False
      Dim isEntryCell As Boolean = False ' Default to false and set true if found
      Dim isExitCell As Boolean = False ' Default to false and set true if found
      Dim fromX As Integer = 0 'These will hold the allowable random range that the top left corner of the room can be created in
      Dim toX As Integer = 0
      Dim fromY As Integer = 0
      Dim toY As Integer = 0
      Dim aString As String = ""

      'NOTE; MUST MAKE SURE TO INITIALIZE EntryStairGrid

      entryRow = Me.EntryStairLocation.Y
      entryColumn = Me.EntryStairLocation.X
      exitRow = Me.ExitStairLocation.Y
      exitColumn = Me.ExitStairLocation.X
      entryRowPointer = Me.EntryStairLocation.Y
      entryColumnPointer = Me.EntryStairLocation.X
      exitRowPointer = Me.ExitStairLocation.Y
      exitColumnPointer = Me.ExitStairLocation.X

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
        result &= Me.CreateRandomEntryRoom(gridRow, gridColumn) ', entryRowPointer, entryColumnPointer)
        isOK = True ' This is the cell for the entry into the level...
      End If

      If exitRow = gridRow AndAlso exitColumn = gridColumn Then
        result &= Me.CreateRandomExitRoom(gridRow, gridColumn) ', exitRowPointer, exitColumnPointer)
        isOK = True ' This is the cell for the exit from the level...
      End If

      If Not isOK Then

        ' If not an entry or exit cell, then randomly choose if room exists...

        Dim randomNumber = Param.Randomizer.Next(0, 100) + 1
        If randomNumber <= Param.MinHasRoomPercentage Then
          result &= Me.CreateRandomRoom(gridRow, gridColumn)
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
          Me.MapCellData(y, x) = 0
          'Me.MapCellVisibility(y, x) = False
        Next
      Next

    End Sub

    Private Function CreateRandomEntryRoom$(gridRow%, gridColumn%) ', entryRow%, entryColumn%)

      'Dim result As String = ""

      Dim rm = Room.CreateRandomRoom(gridRow, gridColumn)

      If rm.Height > 0 Then

        Me.Rooms.Add(rm)
        'result = rm.ToString

        Me.UpdateMap(rm)

        Dim x = Param.Randomizer.Next(1, rm.Width - 1)
        Dim y = Param.Randomizer.Next(1, rm.Height - 1)

        Dim r = rm.ActualMapTopLocation + y
        Dim c = rm.ActualMapLeftLocation + x

        Me.MapCellData(r, c) = CellType.Hero 'CellType.StructureStairsUp
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

        Me.Rooms.Add(rm)

        Me.UpdateMap(rm)

        Dim x = Param.Randomizer.Next(1, rm.Width - 1)
        Dim y = Param.Randomizer.Next(1, rm.Height - 1)

        Dim r = rm.ActualMapTopLocation + y
        Dim c = rm.ActualMapLeftLocation + x

        Me.MapCellData(r, c) = CellType.StructureStairsDown
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

      Dim m_hasRoomFlag As Boolean = False
      Dim m_entryRow As Integer = 0
      Dim m_entryColumn As Integer = 0
      Dim m_entryRowPointer As Integer = 0
      Dim m_entryColumnPointer As Integer = 0
      Dim m_exitRow As Integer = 0
      Dim m_exitColumn As Integer = 0
      Dim m_exitRowPointer As Integer = 0
      Dim m_exitColumnPointer As Integer = 0
      Dim m_roomHeight As Integer = 0
      Dim m_roomWidth As Integer = 0
      Dim m_roomRowOffset As Integer = 0 ' Offset from top side of cell
      Dim m_roomColumnOffset As Integer = 0 ' Offset from left side of cell
      Dim m_randomNumber As Integer = Param.Randomizer.Next()
      Dim m_mapCharacterValue As Integer = 0
      Dim m_ISOK As Boolean = False
      Dim m_ISEntryCell As Boolean = False
      Dim m_ISExitCell As Boolean = False
      Dim m_FromX As Integer = 0 ' These will hold the allowable random range that the top left corner of the room can be created in
      Dim m_ToX As Integer = 0
      Dim m_FromY As Integer = 0
      Dim m_ToY As Integer = 0
      Dim aString As String = ""

      Me.ClearMapData()

      m_entryRow = Me.EntryStairLocation.Y
      m_entryColumn = Me.EntryStairLocation.X
      m_exitRow = Me.ExitStairLocation.Y
      m_exitColumn = Me.ExitStairLocation.X

      m_entryRowPointer = Me.EntryStairLocation.Y
      m_entryColumnPointer = Me.EntryStairLocation.X
      m_exitRowPointer = Me.ExitStairLocation.Y
      m_exitColumnPointer = Me.ExitStairLocation.X

      For gridRow = 1 To Param.GridRowCount
        For gridColumn = 1 To Param.GridColumnCount
          aString = Me.CheckIfRandomRoomExists(gridRow, gridColumn)
          ' Keep track of which grid cells have rooms for tunnel creation...
          'astring will in the form of height-width|top-left or BLANK if no room exists
          'Me.MapCellHasRoom(gridRow, gridColumn) = aString
        Next
      Next

      Console.ResetColor()

      For y = 0 To Param.MapHeight - 1
        For x = 0 To Param.MapWidth - 1
          Console.SetCursorPosition(x, y + 1)
          Select Case Me.Tile(x, y)
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
      Me.CreateRandomTunnels()

    End Sub

    Private Function CreateRandomRoom$(gridRow%, gridColumn%)

      Dim result As String = ""

      Dim rm = Room.CreateRandomRoom(gridRow, gridColumn)

      If rm.Height > 0 Then
        Me.Rooms.Add(rm)
        result = rm.ToString
      End If

      Me.UpdateMap(rm)

      Return result

    End Function

    ''' <summary>
    ''' Make sure each door of each room connects to either another door (preferably 
    ''' in another room) or intersects a tunnel.
    ''' Make sure each room is connected to a least one other room and 
    ''' that every room on the level is accessible.
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

      For position = 0 To Me.Rooms.Count - 1

        Dim rm = Me.Rooms(position)

        For d = 0 To rm.Doors.Count - 1

          Dim door = rm.Doors(d)

          Dim startDoorY = door.Y
          Dim startDoorX = door.X

          Dim coord = Me.FindNearestDoor(rm, door)

          If coord Is Nothing Then Stop

          If coord IsNot Nothing Then

            Dim targetDoorY = coord.Y
            Dim targetDoorX = coord.X

            ' Set to tunnel to start just outside of the from door and 
            ' to end just outside of the target door...

            coord = Me.FindDoormat(startDoorX, startDoorY)

            If coord Is Nothing Then Stop

            If coord IsNot Nothing Then

              Dim startY = coord.Y
              Dim startX = coord.X

              Me.Dig(startX, startY)

              ' Set tunnel to end just outside target door...

              coord = Me.FindDoormat(targetDoorX, targetDoorY)

              If coord Is Nothing Then Stop

              If coord IsNot Nothing Then

                Dim targetY = coord.Y
                Dim targetX = coord.X

                Dim createdStepCount = Me.CreateUtilityTunnel(startX, startY, targetX, targetY)
                If createdStepCount > 0 Then
                  ' Add tunnel to room connections...
                  coord = rm.GetConnectionNumber(startDoorY, startDoorX, targetDoorY, targetDoorX)
                  If coord Is Nothing Then Stop
                  Me.RoomConnections.Add(coord)
                Else
                  Stop
                End If

              End If

            End If

          End If

        Next
      Next

      'now that all tunnels that will be randomly created are in place
      'make sure that each room is accessible somehow.

      Me.VerifyAllRoomsAccessible()

    End Sub

    Private Enum Direction
      Up
      Down
      Left
      Right
    End Enum

    Private Function CreateUtilityTunnel%(fromX%, fromY%, toX%, toY%)

      Dim result As Integer = 0

      'Dim aRoom As New Room

      Dim aStepsInDirectionCtr As Integer = 0
      Dim aSeekColumn As Integer = 0
      Dim aSeekRow As Integer = 0

      Dim horizontalDifference = fromX - toX
      Dim verticalDifference = fromY - toY

      Dim aCurrentDirection As Direction
      Dim aAvoidanceDirection As Direction

      If Math.Abs(verticalDifference) > Math.Abs(horizontalDifference) Then
        ' If verticlal is greater, start left to right...
        If horizontalDifference > 0 Then
          aCurrentDirection = Direction.Left
        Else
          aCurrentDirection = Direction.Right
        End If
        If verticalDifference > 0 Then
          aAvoidanceDirection = Direction.Up
        Else
          aAvoidanceDirection = Direction.Down
        End If
      Else
        If verticalDifference > 0 Then
          aCurrentDirection = Direction.Up
        Else
          aCurrentDirection = Direction.Down
        End If
        If horizontalDifference > 0 Then
          aAvoidanceDirection = Direction.Left
        Else
          aAvoidanceDirection = Direction.Right
        End If
      End If

      Dim aCurrentRow = fromY
      Dim aCurrentColumn = fromX

      For result = 1 To (Param.MapHeight * Param.MapWidth)

        Dim currentData = Me.DigDirections(aCurrentColumn, aCurrentRow)

        If aCurrentRow = toY Then
          If aCurrentColumn - toX > 0 Then
            aCurrentDirection = Direction.Left
          Else
            aCurrentDirection = Direction.Right
          End If
        Else
          If aCurrentColumn = toX Then
            If aCurrentRow - toY > 0 Then
              aCurrentDirection = Direction.Up
            Else
              aCurrentDirection = Direction.Down
            End If
          End If
        End If
        If currentData.Contains(aCurrentDirection) Then
          Select Case aCurrentDirection
            Case Direction.Up
              aSeekRow = aCurrentRow - 1
              aSeekColumn = aCurrentColumn
            Case Direction.Down
              aSeekRow = aCurrentRow + 1
              aSeekColumn = aCurrentColumn
            Case Direction.Left
              aSeekRow = aCurrentRow
              aSeekColumn = aCurrentColumn - 1
            Case Direction.Right
              aSeekRow = aCurrentRow
              aSeekColumn = aCurrentColumn + 1
          End Select
        Else
          If aCurrentDirection = aAvoidanceDirection Then
            'If 1 = 0 Then
            'need to tunnel around object
            Dim coord = Me.TunnelAroundObstacle(aCurrentRow, aCurrentColumn, toY, toX, aCurrentDirection)
            If coord IsNot Nothing Then
              aSeekRow = coord.Y
              aSeekColumn = coord.X
            End If
            aCurrentColumn = aSeekColumn
            aCurrentRow = aSeekRow
            'End If
          Else
            'TODO: Getting stuck here when it hits a wall where the 
            ' door is literally just around the corner...
            If currentData.Contains(aAvoidanceDirection) Then
              Select Case aAvoidanceDirection
                Case Direction.Up
                  aSeekRow = aCurrentRow - 1
                  aSeekColumn = aCurrentColumn
                Case Direction.Down
                  aSeekRow = aCurrentRow + 1
                  aSeekColumn = aCurrentColumn
                Case Direction.Left
                  aSeekRow = aCurrentRow
                  aSeekColumn = aCurrentColumn - 1
                Case Direction.Right
                  aSeekRow = aCurrentRow
                  aSeekColumn = aCurrentColumn + 1
              End Select
            Else
              'stuck
              Exit For
            End If
          End If
        End If

        Me.Dig(aSeekColumn, aSeekRow)

        aCurrentColumn = aSeekColumn
        aCurrentRow = aSeekRow

        If aCurrentRow = toY AndAlso aCurrentColumn = toX Then
          Exit For
        End If
        If aCurrentRow = toY OrElse aCurrentColumn = toX Then
          aStepsInDirectionCtr = 5
        End If
        'every X steps can change direction
        aStepsInDirectionCtr += 1
        If aStepsInDirectionCtr > 5 Then
          aStepsInDirectionCtr = 0
          horizontalDifference = aCurrentColumn - toX
          verticalDifference = aCurrentRow - toY
          If horizontalDifference = 0 Then
            If verticalDifference > 0 Then
              aCurrentDirection = Direction.Up
            Else
              aCurrentDirection = Direction.Down
            End If
          Else
            If verticalDifference = 0 Then
              If horizontalDifference > 0 Then
                aCurrentDirection = Direction.Left
              Else
                aCurrentDirection = Direction.Right
              End If
            Else
              If Math.Abs(verticalDifference) > Math.Abs(horizontalDifference) Then
                'if up-down farther then start left right
                If horizontalDifference > 0 Then
                  aCurrentDirection = Direction.Left
                Else
                  aCurrentDirection = Direction.Right
                End If
                If verticalDifference > 0 Then
                  aAvoidanceDirection = Direction.Up
                Else
                  aAvoidanceDirection = Direction.Down
                End If
              Else
                If verticalDifference > 0 Then
                  aCurrentDirection = Direction.Up
                Else
                  aCurrentDirection = Direction.Down
                End If
                If horizontalDifference > 0 Then
                  aAvoidanceDirection = Direction.Left
                Else
                  aAvoidanceDirection = Direction.Right
                End If
              End If
            End If
          End If
        End If
      Next

      Return result

    End Function

    Private Property Tile(x%, y%) As CellType
      Get

        If Not x.Between(0, Param.MapWidth - 1) Then
          Throw New ArgumentOutOfRangeException("x")
        End If

        If Not y.Between(0, Param.MapHeight - 1) Then
          Throw New ArgumentOutOfRangeException("y")
        End If

        Return Me.MapCellData(y, x)

      End Get
      Set(value As CellType)

        If Not x.Between(0, Param.MapWidth - 1) Then
          Throw New ArgumentOutOfRangeException("x")
        End If

        If Not y.Between(0, Param.MapHeight - 1) Then
          Throw New ArgumentOutOfRangeException("y")
        End If

        Me.MapCellData(y, x) = value

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

      Dim modifiers = {Me.ModifierUp,
                       Me.ModifierDown,
                       Me.ModifierLeft,
                       Me.ModifierRight}

      For Each modifier In modifiers
        Dim seekX = x + modifier.X
        Dim seekY = y + modifier.Y
        If seekX.Between(0, Param.MapWidth - 1) AndAlso
           seekY.Between(0, Param.MapHeight - 1) Then
          Select Case Me.Tile(seekX, seekY)
            Case CellType.Void, CellType.StructureTunnel
              Select Case modifier
                Case Me.ModifierUp : result.Add(Direction.Up)
                Case Me.ModifierDown : result.Add(Direction.Down)
                Case Me.ModifierLeft : result.Add(Direction.Left)
                Case Me.ModifierRight : result.Add(Direction.Right)
                Case Else
                  Stop
              End Select
            Case Else
          End Select
        End If
      Next

      Return result

    End Function

    Private Sub Dig(x%, y%)

      If Not x.Between(0, Param.MapWidth - 1) Then
        Throw New ArgumentOutOfRangeException("x")
      End If

      If Not y.Between(0, Param.MapHeight - 1) Then
        Throw New ArgumentOutOfRangeException("y")
      End If

      Me.Tile(x, y) = CellType.StructureTunnel

      If Debugger.IsAttached Then
        Console.SetCursorPosition(x, y + 1)
        Console.Write("*"c)
        Threading.Thread.Sleep(100)
        Console.SetCursorPosition(x, y + 1)
        Console.Write("X"c)
      End If

    End Sub

    Private Function FindDoormat(x%, y%) As Coordinate

      If Not x.Between(0, Param.MapWidth - 1) Then
        Throw New ArgumentOutOfRangeException("x")
      End If

      If Not y.Between(0, Param.MapHeight - 1) Then
        Throw New ArgumentOutOfRangeException("y")
      End If

      Dim modifiers = {Me.ModifierLeft,
                       Me.ModifierUp,
                       Me.ModifierRight,
                       Me.ModifierDown}

      For Each modifier In modifiers
        Dim seekX = x + modifier.X
        Dim seekY = y + modifier.Y
        If seekX.Between(0, Param.MapWidth - 1) AndAlso
           seekY.Between(0, Param.MapHeight - 1) Then
          Select Case Me.Tile(seekX, seekY)
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
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <param name="coord"></param>
    ''' <returns></returns>
    Private Function FindNearestDoor(cell As Room, coord As Coordinate) As Coordinate

      Dim foundList As New List(Of Room)

      For gridRow = 1 To Param.GridRowCount

        For gridColumn = 1 To Param.GridColumnCount

          Dim fromPosition = Param.GridPositionToIndex(gridColumn, gridRow)

          If fromPosition = cell.GridPosition Then

            If gridRow > 1 Then

              Dim toPosition = Param.GridPositionToIndex(gridColumn, gridRow - 1)

              For r = 0 To Me.Rooms.Count - 1

                If toPosition = Me.Rooms(r).GridPosition Then

                  Dim connection = New Coordinate(cell.GridPosition, Me.Rooms(r).GridPosition)
                  Dim reverseConnection = New Coordinate(Me.Rooms(r).GridPosition, cell.GridPosition)
                  Dim isFound = False

                  For c = 0 To Me.RoomConnections.Count - 1
                    If connection = Me.RoomConnections(c) OrElse
                       reverseConnection = Me.RoomConnections(c) Then
                      isFound = True
                      Exit For
                    End If
                  Next

                  If Not isFound Then
                    ' Do not use room if already has a connection...
                    foundList.Add(Me.Rooms(r))
                  End If

                  Exit For

                End If

              Next

            End If

            If gridRow < Param.GridRowCount Then

              Dim toPosition = Param.GridPositionToIndex(gridColumn, gridRow + 1)

              For r = 0 To Me.Rooms.Count - 1

                If toPosition = Me.Rooms(r).GridPosition Then

                  Dim connection = New Coordinate(cell.GridPosition, Me.Rooms(r).GridPosition)
                  Dim reverseConnection = New Coordinate(Me.Rooms(r).GridPosition, cell.GridPosition)
                  Dim isFound = False

                  For c = 0 To Me.RoomConnections.Count - 1
                    If connection = Me.RoomConnections(c) OrElse
                       reverseConnection = Me.RoomConnections(c) Then
                      isFound = True
                      Exit For
                    End If
                  Next

                  If Not isFound Then
                    ' Do not use room if already has a connection...
                    foundList.Add(Me.Rooms(r))
                  End If

                  Exit For

                End If

              Next

            End If

            If gridColumn > 1 Then

              Dim toPosition = Param.GridPositionToIndex(gridColumn - 1, gridRow)

              For r As Integer = 0 To Me.Rooms.Count - 1

                If toPosition = Me.Rooms(r).GridPosition Then

                  Dim connection = New Coordinate(cell.GridPosition, Me.Rooms(r).GridPosition)
                  Dim reverseConnection = New Coordinate(Me.Rooms(r).GridPosition, cell.GridPosition)
                  Dim isFound = False

                  For c As Integer = 0 To Me.RoomConnections.Count - 1
                    If connection = Me.RoomConnections(c) OrElse
                       reverseConnection = Me.RoomConnections(c) Then
                      isFound = True
                      Exit For
                    End If
                  Next

                  If Not isFound Then
                    ' Do not use room if already has a connection...
                    foundList.Add(Me.Rooms(r))
                  End If

                  Exit For

                End If

              Next

            End If

            If gridColumn < Param.GridColumnCount Then

              Dim toPosition = Param.GridPositionToIndex(gridColumn + 1, gridRow)

              For r = 0 To Me.Rooms.Count - 1

                If toPosition = Me.Rooms(r).GridPosition Then

                  Dim connection = New Coordinate(cell.GridPosition, Me.Rooms(r).GridPosition)
                  Dim reverseConnection = New Coordinate(Me.Rooms(r).GridPosition, cell.GridPosition)
                  Dim isFound = False

                  For connect = 0 To Me.RoomConnections.Count - 1
                    If connection = Me.RoomConnections(connect) OrElse
                       reverseConnection = Me.RoomConnections(connect) Then
                      isFound = True
                      Exit For
                    End If
                  Next

                  If Not isFound Then
                    ' Do not use room if already has a connection...
                    foundList.Add(Me.Rooms(r))
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
        Return foundList(toPosition).Doors(0)
      Else
        ' Need to pick any random room...
        For pass = 0 To 10
          Dim toPosition = Param.Randomizer.Next(Me.Rooms.Count)
          If Not Me.Rooms(toPosition).GridPosition = cell.GridPosition Then
            Return Me.Rooms(toPosition).Doors(0)
          End If
        Next
      End If

      Return Nothing

    End Function

    'Private Function GetCoordinateString$(row%, column%)

    '  Dim currentData = $"0{row}"
    '  Dim aReturnValue = currentData.Substring(currentData.Length - 2)
    '  currentData = $"0{column}"
    '  aReturnValue &= currentData.Substring(currentData.Length - 2)
    '  Return aReturnValue

    'End Function

    Private Function GetRoom(number%) As Room

      If Not number.Between(1, Param.GridRowCount * Param.GridColumnCount) Then
        Throw New ArgumentOutOfRangeException("number")
      End If

      For Each rm In Me.Rooms
        If rm.GridPosition = number Then
          Return rm
        End If
      Next

      Return Nothing

    End Function

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
        Dim number = ((row - 1) * Param.GridColumnCount) + col

        For Each rm In Me.Rooms
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

    '''' <summary>
    '''' Find out which grid cell a specific point is within 
    '''' There may or not be a room there
    '''' </summary>
    '''' <param name="x">The column of the specific point</param>
    '''' <param name="y">The row of the specific point</param>
    '''' <returns>
    '''' the number of the room at the point would be at the point requested if it exists
    '''' </returns>
    'Private Function GetRoomNumberFromXY%(x%, y%)

    '  'Dim sx = If(gridColumn = 1, 0, If(gridColumn = 2, 27, 55))
    '  'Dim sy = If(gridRow = 1, 0, If(gridRow = 2, 7, 15))

    '  Select Case x
    '    Case 0 To 25
    '      Select Case y
    '        Case 0 To 5 : Return 1
    '        Case 7 To 13 : Return 2
    '        Case 15 To 20 : Return 3
    '        Case Else
    '          Stop
    '      End Select
    '    Case 27 To 53
    '      Select Case y
    '        Case 0 To 5 : Return 4
    '        Case 7 To 13 : Return 5
    '        Case 15 To 20 : Return 6
    '        Case Else
    '          Stop
    '      End Select
    '    Case 55 To 80
    '      Select Case y
    '        Case 0 To 5 : Return 7
    '        Case 7 To 13 : Return 8
    '        Case 15 To 20 : Return 9
    '        Case Else
    '          Stop
    '      End Select
    '    Case Else
    '      Stop
    '  End Select

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

    '  'Return result

    'End Function

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
    ''' <param name="targetRow"></param>
    ''' <param name="targetColumn"></param>
    ''' <param name="direction"></param>
    ''' <returns></returns>
    Private Function TunnelAroundObstacle(currentRow%, currentColumn%, targetRow%, targetColumn%, direction As Direction) As Coordinate

      Dim aReturnValue As Coordinate
      'Dim isFound As Boolean = True
      'Dim aStepCtr As Integer = 0
      'Dim aAvoidanceDirection As Direction
      'Dim aBlockingRoomNumber As Integer = 0
      'Dim aBlockingRoom As New Room
      Dim aDistanceOne As Integer = 0
      Dim aDistanceTwo As Integer = 0
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

      Dim isFound = True

      If tile = CellType.Void OrElse tile = CellType.StructureTunnel Then

        ' Original direction clear...
        Me.Dig(seekX, seekY)

        ' Take the step in that direction then...
        isFound = False ' The way is clear so exit...
        aCurrentY = seekY
        aCurrentX = seekX
        aReturnValue = New Coordinate(aCurrentX, aCurrentY)

      Else

        'Dim aBlockingRoomNumber = Me.GetRoomNumberFromXY(seekX, seekY)

        'Dim aBlockingRoom As Room = Nothing

        'If aBlockingRoomNumber > 0 Then
        '  aBlockingRoom = Me.GetRoom(aBlockingRoomNumber)
        'End If

        Dim aBlockingRoom = Me.GetRoom(seekX, seekY)

        Dim aAvoidanceDirection As Direction

        If aBlockingRoom IsNot Nothing Then
          'find out the shortest side of the blocking room and go that way
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

          Dim availableDirections = Me.DigDirections(aCurrentX, aCurrentY)

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
                Me.Dig(seekX, seekY)
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
                Me.Dig(seekX, seekY)
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

      Dim aReturnValue As String = ""
      Dim isDoorTop As Boolean = False
      Dim isDoorLeft As Boolean = False
      Dim isDoorRight As Boolean = False
      Dim isDoorBottom As Boolean = False
      Dim isAnyDoor As Boolean = False
      Dim rNumber As Integer = 0
      Dim xNumber As Integer = 0
      Dim aDataArray() As String = {""}
      Dim aFirstPtr As Integer = 0
      Dim aSecondPtr As Integer = 0
      Dim aTopPtr As Integer = Param.MapHeight
      Dim aLeftPtr As Integer = Param.MapWidth
      Dim aDirectionPtr As String = ""
      Dim aDoorCharacter As Integer = 0
      Dim x As Integer = 0
      Dim y As Integer = 0

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

            Me.MapCellData(y, x) = CType(ch, CellType)
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

            Me.MapCellData(y, x) = cell
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
          Me.MapCellData(y, x) = CellType.StructureDoor ' ct
          'Me.MapCellVisibility(y, x) = Not Me.m_IsFogOfWarActive ' testing m_TestMode 'testing true makes all visible

        Next

      End If

      aReturnValue = aTopPtr.ToString & "-" & aLeftPtr.ToString

      Return aReturnValue

    End Function

    Private Sub VerifyAllRoomsAccessible()

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

      Dim connectionList As New List(Of Coordinate)

      For Each connection In Me.RoomConnections
        connectionList.Add(connection)
        If Not connection.X = connection.Y Then
          connectionList.Add(New Coordinate(connection.Y, connection.X))
        End If
      Next

      Dim cycle = 0

      Do While cycle < Param.MapHeight * Param.MapWidth

        ' Finally, walk through all the rooms to ensure that every room is accessible...

        Dim stepList = New List(Of Integer)

        For Each connection In connectionList
          Dim isFound = False
          For Each foundStep In stepList
            If foundStep = connection.X Then
              isFound = True
              Exit For
            End If
          Next
          If Not isFound Then
            stepList.Add(connection.X)
          End If
        Next

        'now aStepList contains a list of all the rooms that exist. 
        'Take first room in asteplist and put in atunnelroomlist
        '   remove the room from asteplist
        'while tunnelrooms contains entries and stepctr<max
        '  Take first room from tunnelrooms and make working room
        '     remove from tunnelrooms
        '     Add connections from working room to tunnelrooms if they are not already there and if they still exist in steplist
        '         rooms no longer in steplist have already been worked
        'loop until all rooms in tunnel rooms have been worked
        'any rooms remaining in steplist now need to be connected to a room not in step list
        'createutilitytunnel
        'Then go through this process again until steplist is empty at the end of the run.

        If stepList.Count > 0 Then

          Dim tunnelStepList = New List(Of Integer) From {
            stepList(0) 'TODO does this work the same as in prior version???
          }

          stepList.RemoveAt(0)

          Dim currentStep = tunnelStepList(0)

          tunnelStepList.RemoveAt(0)

          Dim count = 0

          Do While count < Param.MapHeight * Param.MapWidth

            For Each connection In connectionList

              Dim x = connection.X
              Dim y = connection.Y

              If x = currentStep Then

                ' This is a connection from the working room.
                ' See if it goes to a room still in stepList, but not yet in tunnelList...
                ' If so, remove it from stepList and add it to tunnelList.

                For i = 0 To stepList.Count - 1

                  If stepList(i) = y Then

                    ' Still in stepList so remove and add to tunnelList if not already there...

                    Dim isFound = False

                    For Each foundItem In tunnelStepList
                      If foundItem = y Then
                        isFound = True ' Already in tunnelList...
                        Exit For
                      End If
                    Next

                    If Not isFound Then
                      ' Not there, so add to tunnellist for further processing...
                      tunnelStepList.Add(stepList(i))
                    End If

                    stepList.RemoveAt(i)

                    Exit For

                  End If

                Next

              End If

            Next

            If tunnelStepList.Count = 0 Then
              Exit Do
            Else
              currentStep = tunnelStepList(0)
              tunnelStepList.RemoveAt(0)
            End If

            ' Try again until finished...

            count += 1

          Loop

          ' If stepList has any rooms left after tunnelList emptied then we need to add a tunnel...
          If stepList.Count = 0 Then
            Exit Do
          Else

            ' Need to tunnel between first room left in stepList to some room not in stepList...

            Dim fromDoorX = 0
            Dim fromDoorY = 0
            Dim toDoorX = 0
            Dim toDoorY = 0
            Dim fromRoom = stepList(0)
            Dim toRoom = 0

            For Each foundRoom In Me.Rooms
              If foundRoom.GridPosition = fromRoom Then
                ' Get a door in this room to start from...
                fromDoorY = foundRoom.Doors(0).Y
                fromDoorX = foundRoom.Doors(0).X
                Exit For
              End If
            Next

            ' Now find a room not in stepList to connect to...
            ' Try rooms next door first...

            For Each foundRoom As Room In Me.Rooms

              Dim isFound = False

              For Each foundString In stepList
                If foundString = foundRoom.GridPosition Then
                  isFound = True
                  Exit For
                End If
              Next

              If Not isFound Then
                ' This may be it!
                If (fromRoom + 1 = foundRoom.GridPosition) OrElse
                   (fromRoom - 1 = foundRoom.GridPosition) OrElse
                   (fromRoom - 3 = foundRoom.GridPosition) OrElse
                   (fromRoom + 3 = foundRoom.GridPosition) Then
                  toRoom = foundRoom.GridPosition
                  toDoorY = foundRoom.Doors(0).Y
                  toDoorX = foundRoom.Doors(0).X
                  Exit For
                End If
              End If

            Next

            If toRoom = 0 Then

              ' If could not find room next door then take any room...

              For Each foundRoom In Me.Rooms

                Dim isFound = False

                For Each position In stepList
                  If position = foundRoom.GridPosition Then
                    isFound = True
                    Exit For
                  End If
                Next

                If Not isFound Then
                  ' This may be it!
                  toRoom = foundRoom.GridPosition
                  toDoorY = foundRoom.Doors(0).Y
                  toDoorX = foundRoom.Doors(0).X
                  Exit For
                End If

              Next

            End If

            Dim createdStepCount = Me.CreateUtilityTunnel(fromDoorX, fromDoorY, toDoorX, toDoorY)

            If createdStepCount > 0 Then
              ' Add new tunnel to connectionslist and continue..
              connectionList.Add(New Coordinate(fromRoom, toRoom))
              connectionList.Add(New Coordinate(toRoom, fromRoom))
            End If

          End If

        Else

          Exit Do

        End If

        Debug.WriteLine("ConnectionList")
        For Each currentData In connectionList
          Debug.WriteLine(currentData)
        Next

        cycle += 1

      Loop

    End Sub

  End Class

End Namespace