Option Explicit On
Option Strict On
Option Infer On
Imports System.Drawing

Namespace Global.Rogue.Core

  Public NotInheritable Class Room

    Public Sub New()
      Initialize() 'Sets the room to empty
    End Sub

    Public Property Connections As List(Of String)

    Public Property HasConnections As Boolean

    ''' <summary>
    ''' Hold a pointer to the level of the current map
    ''' Bigger numbers means further down into the dungeon.
    ''' 0=not any level, 1=first or beginning level.
    ''' </summary>
    ''' <returns></returns>
    Public Property Depth() As Integer
    Public Property MapGridRowLocation() As Integer
    Public Property MapGridColumnLocation() As Integer
    Public Property MapTopLocation() As Integer
    Public Property MapLeftLocation() As Integer

    Public ReadOnly Property ActualMapTopLocation() As Integer
      Get
        Dim sy = If(MapGridRowLocation = 1, 0, If(MapGridRowLocation = 2, 7, 15))
        Return sy + MapTopLocation '+ ((Me.MapGridRowLocation - 1) * Param.MapGridCellHeight)
      End Get
    End Property

    Public ReadOnly Property ActualMapLeftLocation() As Integer
      Get
        Dim sx = If(MapGridColumnLocation = 1, 0, If(MapGridColumnLocation = 2, 27, 54))
        Return sx + MapLeftLocation '+ ((Me.MapGridColumnLocation - 1) * Param.MapGridCellWidth)
      End Get
    End Property

    Public Property Height() As Integer
    Public Property Width() As Integer
    Public Property GridPosition() As Integer = 0 ' 1-9 to position room within grid of 3x3...
    Public Property Doors() As New List(Of Door) ' Keep track of which doors exist for this room...
    Public Property IsLit() As Boolean = False

    Public Sub Initialize()

      Depth = 0
      MapGridRowLocation = 0
      MapGridColumnLocation = 0
      MapTopLocation = 0
      MapLeftLocation = 0
      Height = 0
      Width = 0
      GridPosition = 0
      Doors = New List(Of Door)
      IsLit = False
      Dim possibilityLit = Param.Randomizer.NextDouble()
      If possibilityLit >= 0.8 Then
        IsLit = True
      End If

      Connections = New List(Of String)
      HasConnections = False

    End Sub

    'Public Shared Function CreateRandomEntryRoom(gridRow%, gridColumn%, entryRow%, entryColumn%) As Room

    '  Dim result = Room.CreateRandomRoom(gridRow, gridColumn)

    '  If 1 = 0 Then

    '    Dim x = result.MapLeftLocation
    '    Dim y = result.MapTopLocation

    '    Dim entryX = entryColumn - ((gridColumn - 1) * 26)
    '    Dim entryY = entryRow - ((gridRow - 1) * 7)

    '    If result.MapTopLocation > entryY - 1 Then
    '      y = entryY - 1
    '    End If
    '    If result.MapTopLocation + result.Height < entryY + 2 Then
    '      y = (entryY - result.Height) + 2
    '    End If
    '    If result.MapLeftLocation > entryX - 1 Then
    '      x = entryX - 1
    '    End If
    '    If result.MapLeftLocation + result.Width < entryX + 2 Then
    '      x = (entryX - result.Width) + 2
    '    End If

    '    Dim xOFF = result.MapLeftLocation - x
    '    Dim yOFF = result.MapTopLocation - y

    '    'result.MapTopLocation = y
    '    'result.MapLeftLocation = x

    '    For d = 0 To result.Doors.Count - 1

    '      Dim newX = result.Doors(d).X
    '      Dim newY = result.Doors(d).Y
    '      'Dim newType = result.Doors(d).Type

    '      newY -= yOFF
    '      newX -= xOFF

    '      If newX < result.MapLeftLocation + 1 Then
    '        newX = result.MapLeftLocation + 1
    '      End If
    '      If newY < result.MapTopLocation + 1 Then
    '        newY = result.MapTopLocation + 1
    '      End If

    '      result.Doors(d) = New Coordinate(newX, newY) ', newType)

    '    Next

    '  End If

    '  Return result

    'End Function

    'Public Function CreateRandomExitRoom(gridRow%, gridColumn%, exitRow%, exitColumn%) As Room

    '  Dim result = CreateRandomRoom(gridRow, gridColumn)

    '  If 1 = 0 Then

    '    Dim exitX = exitColumn - ((gridColumn - 1) * 26)

    '    Dim xOff = (exitX - result.Width) + 1

    '    Dim x1 = result.Width - 1
    '    Dim x = Param.Randomizer.Next(x1) + xOff + 1

    '    If x >= exitX Then
    '      x = exitX - 1
    '    End If
    '    If x <= (exitX - (result.Width - 1)) Then
    '      x = (exitX - (result.Width - 1)) + 1
    '    End If
    '    If x < 2 Then
    '      x = 2
    '    End If
    '    If x > Param.CellWidth(gridColumn) - result.Width Then
    '      x = Param.CellWidth(gridColumn) - result.Width
    '    End If

    '    Dim exitY As Integer = exitRow - ((gridRow - 1) * 7)
    '    Dim y = 0

    '    If y >= exitY Then
    '      y = exitY - 1
    '    End If
    '    If y <= (exitY - (result.Height - 1)) Then
    '      y = (exitY - (result.Height - 1)) + 1
    '    End If
    '    If y < 2 Then
    '      y = 2
    '    End If
    '    If y > Param.CellHeight(gridRow) - result.Height Then
    '      y = Param.CellHeight(gridRow) - result.Height
    '    End If

    '    xOff = result.MapLeftLocation - x
    '    Dim yOFF = result.MapTopLocation - y

    '    'result.MapTopLocation = y
    '    'result.MapLeftLocation = x

    '    For d = 0 To result.Doors.Count - 1

    '      Dim newX = result.Doors(d).X
    '      Dim newY = result.Doors(d).Y
    '      'Dim newType = result.Doors(d).Type

    '      newY -= yOFF
    '      newX -= xOff

    '      result.Doors(d) = New Coordinate(x, y) ', newType)

    '    Next

    '  End If

    '  Return result

    'End Function

    ''' <summary>
    ''' Determine if the whatRoom is connected to this room by examining the whatroom connections property
    ''' </summary>
    ''' <param name="whatRoom"></param>
    ''' <returns></returns>
    Public Function IsRoomConnected(whatRoom As Room) As Boolean
      Dim aReturnValue = False
      Dim aFromGrid = GridPosition
      Dim aToGrid = whatRoom.GridPosition
      Dim aConnection = aFromGrid.ToString & "|" & aToGrid.ToString

      For Each foundConnection In whatRoom.Connections
        If aConnection = foundConnection Then
          aReturnValue = True
        End If
      Next
      If aReturnValue = False Then
        'switch order of connections and try again
        aFromGrid = whatRoom.GridPosition
        aToGrid = GridPosition
        aConnection = $"{aFromGrid.ToString}|{aToGrid.ToString}"
        For Each foundConnection In whatRoom.Connections
          If aConnection = foundConnection Then
            aReturnValue = True
          End If
        Next

      End If

      Return aReturnValue

    End Function

    Public Shared Function CreateRoom(gridRow%, gridColumn%) As Room

      Dim result = New Room

      ' Determine the x and y of the grid cell...

      Dim sx = If(gridColumn = 1, 0, If(gridColumn = 2, 27, 54))
      Dim sy = If(gridRow = 1, 0, If(gridRow = 2, 7, 15))

      Dim gridWidth = 26
      Dim gridHeight = If(gridRow = 2, 7, 6)

      ' Determine the size of the room; must be:
      '  - minimum 4x4
      '  - no larger than the grid height/width
      '  - can be the whole grid size.
      Dim height = Param.Randomizer.Next(4, gridHeight + 1)
      Dim width = Param.Randomizer.Next(4, gridWidth + 1)

      ' Determine offset of top left corner of room based on size of room and 
      ' random distance from top left of map grid...

      ' Top, Left of room within the grid cell...
      Dim x = Param.Randomizer.Next(0, gridWidth - width + 1)
      Dim y = Param.Randomizer.Next(0, gridHeight - height + 1)

      Dim zone = ((gridRow - 1) * 3) + gridColumn

      Debug.WriteLine($"Room # {zone} ({x}, {y}) [{width}, {height}] |{sx},{sy}|")

      result.MapTopLocation = y
      result.MapLeftLocation = x
      result.MapGridRowLocation = gridRow
      result.MapGridColumnLocation = gridColumn
      result.Height = height
      result.Width = width
      result.GridPosition = ((gridRow - 1) * 3) + gridColumn

      Return result

    End Function

    Public Shared Function CreateRandomRoom(gridRow%, gridColumn%) As Room

      Dim result = New Room

      ' Determine the x and y of the grid cell...

      Dim sx = If(gridColumn = 1, 0, If(gridColumn = 2, 27, 54))
      Dim sy = If(gridRow = 1, 0, If(gridRow = 2, 7, 15))

      Dim gridWidth = 26
      Dim gridHeight = If(gridRow = 2, 7, 6)

      ' Determine the size of the room; must be:
      '  - minimum 4x4
      '  - no larger than the grid height/width
      '  - can be the whole grid size.
      Dim height = Param.Randomizer.Next(4, gridHeight + 1)
      Dim width = Param.Randomizer.Next(4, gridWidth + 1)

      ' Determine offset of top left corner of room based on size of room and 
      ' random distance from top left of map grid...

      ' Top, Left of room within the grid cell...
      Dim x = Param.Randomizer.Next(0, gridWidth - width + 1)
      'Debug.WriteLine($"Rolled a {x + 1} on a d{gridWidth - width + 1}.")
      Dim y = Param.Randomizer.Next(0, gridHeight - height + 1)
      'Debug.WriteLine($"Rolled a {y + 1} on a d{gridHeight - height + 1}.")

      Dim zone = ((gridRow - 1) * 3) + gridColumn

      Debug.WriteLine($"Room # {zone} ({x}, {y}) [{width}, {height}] |{sx},{sy}|")

      Dim allowTop = gridRow > 1 OrElse (gridRow = 1 AndAlso y > 0)
      Dim allowBottom = gridRow < 3
      Dim allowLeft = gridColumn > 1 OrElse (gridColumn = 1 AndAlso x > 0)
      Dim allowRight = gridColumn < 3

      Dim doorCount = 0

      Do

        If allowTop AndAlso doorCount = 0 Then

          ' See if there is a door on the top...

          Dim number = Param.Randomizer.Next(1, 11)

          If 1 = 1 Then ' number < 3 Then
            Dim n = Param.Randomizer.Next(1, width - 1)
            'n = 1 : n = width - 2
            'Dim type = CellType.StructureDoor ' StructureDoorTopBottom
            Dim r = sy + y
            Dim c = sx + x + n
            result.Doors.Add(New Door(c, r, Core.Face.Top, False))
            doorCount += 1
            Debug.WriteLine($"  Door (Top)={c}|{r}")
          End If

        End If

        If allowLeft AndAlso doorCount = 0 Then

          ' See if there is a door on the left...

          Dim number = Param.Randomizer.Next(1, 11)
          If 1 = 1 Then 'number < 3 Then
            Dim n = Param.Randomizer.Next(1, height - 1)
            'Dim type = CellType.StructureDoor ' StructureDoorSide
            Dim c = sx + x
            Dim r = sy + y + n
            result.Doors.Add(New Door(c, r, Core.Face.Left, False))
            doorCount += 1
            Debug.WriteLine($"  Door (Left)={c}|{r}")
          End If

        End If

        If allowRight AndAlso doorCount = 0 Then

          ' See if there is a door on the right...

          Dim number = Param.Randomizer.Next(1, 11)

          If 1 = 1 Then 'number < 3 Then
            Dim n = Param.Randomizer.Next(1, height - 1)
            'n = 1 : n = height - 2
            'Dim type = CellType.StructureDoor ' StructureDoorSide
            Dim c = sx + x
            Dim r = sy + y + n
            result.Doors.Add(New Door(c, r, Core.Face.Right, False))
            doorCount += 1
            Debug.WriteLine($"  Door (Right)={c}|{r}")
          End If

        End If

        If allowBottom AndAlso doorCount = 0 Then

          ' See if there is a door on the bottom...

          Dim number = Param.Randomizer.Next(1, 11)
          If 1 = 1 Then 'number < 3 Then
            Dim n = Param.Randomizer.Next(1, width - 1)
            'Dim type = CellType.StructureDoor ' StructureDoorTopBottom
            Dim c = sx + x + n
            Dim r = sy + y
            result.Doors.Add(New Door(c, r, Core.Face.Bottom, False))
            doorCount += 1
            Debug.WriteLine($"  Door (Bottom)={c}|{r}")
          End If

        End If

        If doorCount > 0 Then
          Exit Do
        End If

      Loop

      result.MapTopLocation = y
      result.MapLeftLocation = x
      result.MapGridRowLocation = gridRow
      result.MapGridColumnLocation = gridColumn
      result.Height = height
      result.Width = width
      result.GridPosition = ((gridRow - 1) * 3) + gridColumn

      Return result

    End Function

    Public Shared Function GetConnectionNumber(fromDoorRow%, fromDoorColumn%, toDoorRow%, toDoorColumn%) As Coordinate

      ' Determine the grid number from the row/column information...

      Dim y = fromDoorRow
      Dim gridRow = 1
      Do While y > Param.CellHeight(gridRow) 'Param.MapGridCellHeight
        y -= Param.CellHeight(gridRow) 'Param.MapGridCellHeight
        gridRow += 1
      Loop

      Dim x = fromDoorColumn
      Dim gridColumn = 1
      Do While x > Param.CellWidth(gridColumn) 'Param.MapGridCellWidth
        x -= Param.CellWidth(gridColumn) 'Param.MapGridCellWidth
        gridColumn += 1
      Loop

      ' Now cell will point to proper grid cell
      Dim zone = (gridRow - 1) * Param.GridRowCount + gridColumn

      Dim zone1 = zone ' $"{cell}|"

      y = toDoorRow
      gridRow = 1
      Do While y > Param.CellHeight(gridRow) 'Param.MapGridCellHeight
        y -= Param.CellHeight(gridRow) 'Param.MapGridCellHeight
        gridRow += 1
      Loop

      x = toDoorColumn
      gridColumn = 1
      Do While x > Param.CellHeight(gridRow) 'Param.MapGridCellWidth
        x -= Param.CellHeight(gridRow) 'Param.MapGridCellWidth
        gridColumn += 1
      Loop

      ' Now cell will point to proper grid cell...
      zone = (gridRow - 1) * Param.GridRowCount + gridColumn

      Return New Coordinate(zone1, zone)

    End Function

    Public Overrides Function ToString$()
      Return $"{GridPosition}={MapTopLocation}-{MapLeftLocation}|{Height}-{Width}{vbCrLf}"
    End Function

  End Class

End Namespace