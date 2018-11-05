Imports System
Imports System.Text

Namespace Rogue.Lib

  ''' <summary>
  ''' Encode all aspects for a specific room
  ''' 
  ''' Initialize 
  ''' Ensures that the object has all properties set
  ''' 
  ''' 
  ''' 
  ''' </summary>
  Public Class LevelRoom
  Dim m_localRandom As New Random()

    Private m_ErrorHandler As New ErrorHandler
    Private m_CurrentObject As String = "LevelRoom"

#Region "Public Properties"

#Region "Private Properties"

    ''' <summary>
    ''' Set this value to true to test displaying a sample screen
    ''' </summary>
    Private m_TestMode As Boolean = False

  Private m_EntryStairLocation As String
  Private m_ExitStairLocation As String
  Private m_CurrentMapLevel As Integer
  Private m_MapGridRowLocation As Integer
  Private m_MapGridColumnLocation As Integer
    Private m_MapTopLocation As Integer
    Private m_MapLeftLocation As Integer
    Private m_ActualMapTopLocation As Integer
    Private m_ActualMapLeftLocation As Integer
    Private m_CurrentHeight As Integer
    Private m_CurrentWidth As Integer
  Private m_Row As Integer = 0
  Private m_Column As Integer = 0
  Private m_RoomNumber As Integer = 0 ' 1-9 to position room within grid of 3x3
  Private m_RoomDoors As List(Of String) = New List(Of String)  ' keep track of which doors exist for this room

#End Region

  ''' <summary>
  ''' Hold a pointer to the level of the current map
  ''' Bigger numbers means further down into the dungeon.
  ''' 0=not any level, 1=first or beginning level.
  ''' </summary>
  ''' <returns></returns>
  Public Property CurrentMapLevel() As Integer
    Get
      Return m_CurrentMapLevel
    End Get
    Set(ByVal value As Integer)
      m_CurrentMapLevel = value
    End Set
  End Property

  Public Property MapGridRowLocation() As Integer
    Get
      Return m_MapGridRowLocation
    End Get
    Set(ByVal value As Integer)
      m_MapGridRowLocation = value
    End Set
  End Property

  Public Property MapGridColumnLocation() As Integer
    Get
      Return m_MapGridColumnLocation
    End Get
    Set(ByVal value As Integer)
      m_MapGridColumnLocation = value
    End Set
  End Property

    Public Property MapTopLocation() As Integer
      Get
        Return m_MapTopLocation
      End Get
      Set(ByVal value As Integer)
        m_MapTopLocation = value
      End Set
    End Property

    Public Property MapLeftLocation() As Integer
      Get
        Return m_MapLeftLocation
      End Get
      Set(ByVal value As Integer)
        m_MapLeftLocation = value
      End Set
    End Property

    Public Property ActualMapTopLocation() As Integer
      Get
        m_ActualMapTopLocation = m_MapTopLocation + ((m_MapGridRowLocation - 1) * EnumsAndConsts.MapGridCellHeight)
        Return m_ActualMapTopLocation
      End Get
      Set(ByVal value As Integer)
        m_ActualMapTopLocation = value
      End Set
    End Property

    Public Property ActualMapLeftLocation() As Integer
      Get
        m_ActualMapLeftLocation = m_MapLeftLocation + ((m_MapGridColumnLocation - 1) * EnumsAndConsts.MapGridCellWidth)
        Return m_ActualMapLeftLocation
      End Get
      Set(ByVal value As Integer)
        m_ActualMapLeftLocation = value
      End Set
    End Property

    Public Property CurrentHeight() As Integer
      Get
        Return m_CurrentHeight
      End Get
      Set(ByVal value As Integer)
        m_CurrentHeight = value
      End Set
    End Property

    Public Property CurrentWidth() As Integer
    Get
      Return m_CurrentWidth
    End Get
    Set(ByVal value As Integer)
      m_CurrentWidth = value
    End Set
  End Property

  ''' <summary>
  ''' Hold a pointer to where on the level the player will come down the stairs
  ''' There must be a room here when level is generated
  ''' Number is YYXX 
  ''' YY being the row as two digits which may be zero padded
  ''' XX being the column as two digits which may be zero padded
  ''' </summary>
  ''' <returns></returns>
  Public Property EntryStairLocation() As String
    Get
      Return m_EntryStairLocation
    End Get
    Set(ByVal value As String)
      m_EntryStairLocation = value
    End Set
  End Property

  ''' <summary>
  ''' Hold a pointer to where on the level the player will go down to the next level
  ''' There must be a room here when level is generated
  ''' Number is YYXX 
  ''' YY being the row as two digits which may be zero padded
  ''' XX being the column as two digits which may be zero padded
  ''' </summary>
  ''' <returns></returns>
  Public Property ExitStairLocation() As String
    Get
      Return m_ExitStairLocation
    End Get
    Set(ByVal value As String)
      m_ExitStairLocation = value
    End Set
  End Property


  Public Property RoomNumber() As Integer
    Get
      Return m_RoomNumber
    End Get
    Set(ByVal value As Integer)
      m_RoomNumber = value
    End Set
  End Property

  Public Property RoomDoors() As List(Of String)
    Get
      Return m_RoomDoors
    End Get
    Set(ByVal value As List(Of String))
      m_RoomDoors = value
    End Set
  End Property


#End Region

#Region "Public Methods"

  Public Sub New()
    Initialize() 'Sets the room to empty
  End Sub

  Public Sub New(ByVal whatEntryStairLocation As String, ByVal whatExitStairLocation As String)
    Initialize() 'Sets the room to empty
    EntryStairLocation = whatEntryStairLocation
    ExitStairLocation = whatExitStairLocation
  End Sub

  ''' <summary>
  ''' Load all properties and variables with default values
  ''' </summary>
  Public Sub Initialize()
    Dim currentMethod As String = "Initialize"
    Dim currentData As String = ""

    Try
      'TODO for testing set these values
      'normally they will be set by the calling routine prior to creating the random level
      EntryStairLocation = "1831"
      ExitStairLocation = "0470"

      m_CurrentMapLevel = 0
      m_MapGridRowLocation = 0
      m_MapGridColumnLocation = 0
        m_MapTopLocation = 0
        m_MapLeftLocation = 0
        m_ActualMapTopLocation = 0
        m_ActualMapLeftLocation = 0
        m_CurrentHeight = 0
        m_CurrentWidth = 0
      m_Row = 0
      m_Column = 0
      m_RoomNumber = 0
      m_RoomDoors = New List(Of String)

    Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try
    End Sub

    Public Function CreateRandomEntryRoom(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer, ByVal whatEntryRow As Integer, ByVal whatEntryColumn As Integer, ByVal whatHeight As Integer, ByVal whatWidth As Integer) As LevelRoom
      Dim currentMethod As String = "CreateRandomEntryRoom"
      Dim currentData As String = ""
      Dim aReturnValue As New LevelRoom
      Dim X As Integer = 0 ' left to right
      Dim Y As Integer = 0 ' top to bottom
      Dim X1 As Integer = 0 ' left to right
      Dim Y1 As Integer = 0 ' top to bottom
      Dim entryX As Integer = whatEntryColumn - ((whatMapGridColumn - 1) * 26)
      Dim entryY As Integer = whatEntryRow - ((whatMapGridRow - 1) * 7)
      Dim xOFF As Integer = (entryX - whatWidth) + 1
      Dim yOFF As Integer = (entryY - whatHeight) + 1
      Dim dataArray() As String = {""}
      Dim newX As Integer = 0
      Dim newY As Integer = 0
      Dim newType As Integer = 0

      Try
        aReturnValue = CreateRandomRoom(whatMapGridRow, whatMapGridColumn, whatHeight, whatWidth)

        X1 = (whatWidth) - 1
        X = m_localRandom.Next(X1) + xOFF + 1

        If X >= entryX Then
          X = entryX - 1
        End If
        If X <= (entryX - (whatWidth - 1)) Then
          X = (entryX - (whatWidth - 1)) + 1
        End If
        If X < 2 Then
          X = 2
        End If
        If X > EnumsAndConsts.MapGridCellWidth - whatWidth Then
          X = EnumsAndConsts.MapGridCellWidth - whatWidth
        End If
        Y1 = (whatHeight) - 1
        Y = m_localRandom.Next(Y1) + xOFF + 1

        If Y >= entryY Then
          Y = entryY - 1
        End If
        If Y <= (entryY - (whatHeight - 1)) Then
          Y = (entryY - (whatHeight - 1)) + 1
        End If
        If Y < 2 Then
          Y = 2
        End If
        If Y > EnumsAndConsts.MapGridCellHeight - whatHeight Then
          Y = EnumsAndConsts.MapGridCellHeight - whatHeight
        End If

        xOFF = aReturnValue.MapLeftLocation - X
        yOFF = aReturnValue.MapTopLocation - Y
        aReturnValue.MapTopLocation = Y
        aReturnValue.MapLeftLocation = X
        For dptr As Integer = 0 To aReturnValue.RoomDoors.Count - 1
          dataArray = aReturnValue.RoomDoors(dptr).Split("|")
          Integer.TryParse(dataArray(0), newY)
          Integer.TryParse(dataArray(1), newX)
          Integer.TryParse(dataArray(2), newType)
          newY = newY - yOFF
          newX = newX - xOFF
          If newX < aReturnValue.MapLeftLocation + 1 Then
            newX = aReturnValue.MapLeftLocation + 1
          End If
          If newY < aReturnValue.MapTopLocation + 1 Then
            newY = aReturnValue.MapTopLocation + 1
          End If
          aReturnValue.RoomDoors(dptr) = newY.ToString & "|" & newX.ToString & "|" & newType.ToString
        Next
        'aReturnValue.MapGridRowLocation = whatMapGridRow
        'aReturnValue.MapGridColumnLocation = whatMapGridColumn
        'aReturnValue.CurrentHeight = whatHeight
        'aReturnValue.CurrentWidth = whatWidth
        'aReturnValue.RoomNumber = ((whatMapGridRow - 1) * 3) + whatMapGridColumn
        ' aReturnValue.CreateRandomDoors()

      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    Public Function CreateRandomExitRoom(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer, ByVal whatExitRow As Integer, ByVal whatExitColumn As Integer, ByVal whatHeight As Integer, ByVal whatWidth As Integer) As LevelRoom
      Dim currentMethod As String = "CreateRandomExitRoom"
      Dim currentData As String = ""
      Dim aReturnValue As New LevelRoom
      Dim X As Integer = 0 ' left to right
      Dim Y As Integer = 0 ' top to bottom
      Dim X1 As Integer = 0 ' left to right
      Dim Y1 As Integer = 0 ' top to bottom
      Dim exitX As Integer = whatExitColumn - ((whatMapGridColumn - 1) * 26)
      Dim exitY As Integer = whatExitRow - ((whatMapGridRow - 1) * 7)
      Dim xOFF As Integer = (exitX - whatWidth) + 1
      Dim yOFF As Integer = (exitY - whatHeight) + 1
      Dim dataArray() As String = {""}
      Dim newX As Integer = 0
      Dim newY As Integer = 0
      Dim newType As Integer = 0

      Try
        aReturnValue = CreateRandomRoom(whatMapGridRow, whatMapGridColumn, whatHeight, whatWidth)

        X1 = (whatWidth) - 1
        X = m_localRandom.Next(X1) + xOFF + 1

        If X >= exitX Then
          X = exitX - 1
        End If
        If X <= (exitX - (whatWidth - 1)) Then
          X = (exitX - (whatWidth - 1)) + 1
        End If
        If X < 2 Then
          X = 2
        End If
        If X > EnumsAndConsts.MapGridCellWidth - whatWidth Then
          X = EnumsAndConsts.MapGridCellWidth - whatWidth
        End If

        If Y >= exitY Then
          Y = exitY - 1
        End If
        If Y <= (exitY - (whatHeight - 1)) Then
          Y = (exitY - (whatHeight - 1)) + 1
        End If
        If Y < 2 Then
          Y = 2
        End If
        If Y > EnumsAndConsts.MapGridCellHeight - whatHeight Then
          Y = EnumsAndConsts.MapGridCellHeight - whatHeight
        End If

        xOFF = aReturnValue.MapLeftLocation - X
        yOFF = aReturnValue.MapTopLocation - Y
        aReturnValue.MapTopLocation = Y
        aReturnValue.MapLeftLocation = X
        For dptr As Integer = 0 To aReturnValue.RoomDoors.Count - 1
          dataArray = aReturnValue.RoomDoors(dptr).Split("|")
          Integer.TryParse(dataArray(0), newY)
          Integer.TryParse(dataArray(1), newX)
          Integer.TryParse(dataArray(2), newType)
          newY = newY - yOFF
          newX = newX - xOFF
          aReturnValue.RoomDoors(dptr) = newY.ToString & "|" & newX.ToString & "|" & newType.ToString
        Next
        '        aReturnValue.MapTopLocation = Y
        '        aReturnValue.MapLeftLocation = X
        'aReturnValue.MapGridRowLocation = whatMapGridRow
        'aReturnValue.MapGridColumnLocation = whatMapGridColumn
        'aReturnValue.CurrentHeight = whatHeight
        'aReturnValue.CurrentWidth = whatWidth
        'aReturnValue.RoomNumber = ((whatMapGridRow - 1) * 3) + whatMapGridColumn
        '   aReturnValue.CreateRandomDoors()

      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    Public Function CreateRandomRoom(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer, ByVal whatHeight As Integer, ByVal whatWidth As Integer) As LevelRoom
      Dim currentMethod As String = "CreateRandomRoom"
      Dim currentData As String = ""
      Dim aReturnValue As New LevelRoom
      Dim X As Integer = 0 ' left to right
      Dim Y As Integer = 0 ' top to bottom
      Dim isDoorTop As Boolean = False
      Dim isDoorLeft As Boolean = False
      Dim isDoorRight As Boolean = False
      Dim isDoorBottom As Boolean = False
      Dim isAnyDoor As Boolean = False
      Dim rNumber As Integer = 0
      Dim xNumber As Integer = 0
      Dim aCellType As Integer = 0
      Dim rPtr As Integer = 0
      Dim cPtr As Integer = 0


      Try

        'determine offset of top left corner of room based on size of room and random distance from top left of map grid
        X = m_localRandom.Next(EnumsAndConsts.MapGridCellWidth - whatWidth) + 1
        Y = m_localRandom.Next(EnumsAndConsts.MapGridCellHeight - whatHeight) + 1
        If whatMapGridRow = 3 AndAlso Y < 2 Then
          Y = 2
        End If
        'see if there is a door on the top
        rNumber = m_localRandom.Next(10) + 1
        If rNumber < 3 Then
          xNumber = m_localRandom.Next(whatWidth - 2) + 1
          isDoorTop = True
          isAnyDoor = True
          aCellType = EnumsAndConsts.CellType.StructureDoorTopBottom
          rPtr = (Y + ((whatMapGridRow - 1) * EnumsAndConsts.MapGridCellHeight))
          cPtr = (X + ((whatMapGridColumn - 1) * EnumsAndConsts.MapGridCellWidth) + xNumber)
          If cPtr < X - 1 Then
            cPtr = X + 1
          End If
          If rPtr < Y - 1 Then
            rPtr = Y + 1
          End If
          currentData = rPtr.ToString & "|" & cPtr.ToString & "|" & aCellType.ToString & "|" & "T"
          aReturnValue.RoomDoors.Add(currentData)
          Debug.Print("top door=" & currentData)
        End If

        'see if there is a door on the left
        rNumber = m_localRandom.Next(10) + 1
        If rNumber < 3 Then
          xNumber = m_localRandom.Next(whatHeight - 2) + 1
          isDoorLeft = True
          isAnyDoor = True
          aCellType = EnumsAndConsts.CellType.StructureDoorSide
          cPtr = (X + ((whatMapGridColumn - 1) * EnumsAndConsts.MapGridCellWidth))
          rPtr = (Y + ((whatMapGridRow - 1) * EnumsAndConsts.MapGridCellHeight) + xNumber)
          If cPtr < X - 1 Then
            cPtr = X + 1
          End If
          If rPtr < Y - 1 Then
            rPtr = Y + 1
          End If
          currentData = rPtr.ToString & "|" & cPtr.ToString & "|" & aCellType.ToString & "|" & "L"
          aReturnValue.RoomDoors.Add(currentData)
          Debug.Print("Left door=" & currentData)
        End If

        'see if there is a door on the right
        rNumber = m_localRandom.Next(10) + 1
        If rNumber < 3 Then
          xNumber = m_localRandom.Next(whatHeight - 2) + 1
          isDoorRight = True
          isAnyDoor = True
          aCellType = EnumsAndConsts.CellType.StructureDoorSide
          cPtr = (X + ((whatMapGridColumn - 1) * EnumsAndConsts.MapGridCellWidth) + whatWidth - 1)
          rPtr = (Y + ((whatMapGridRow - 1) * EnumsAndConsts.MapGridCellHeight) + xNumber)
          If cPtr < X - 1 Then
            cPtr = X + 1
          End If
          If rPtr < Y - 1 Then
            rPtr = Y + 1
          End If
          currentData = rPtr.ToString & "|" & cPtr.ToString & "|" & aCellType.ToString & "|" & "R"
          aReturnValue.RoomDoors.Add(currentData)
          Debug.Print("right door=" & currentData)
        End If

        'see if there is a door on the bottom
        rNumber = m_localRandom.Next(10) + 1
        If rNumber < 3 Then
          xNumber = m_localRandom.Next(whatWidth - 2) + 1
          isDoorBottom = True
          isAnyDoor = True
          aCellType = EnumsAndConsts.CellType.StructureDoorTopBottom
          cPtr = (X + ((whatMapGridColumn - 1) * EnumsAndConsts.MapGridCellWidth) + xNumber)
          rPtr = (Y + ((whatMapGridRow - 1) * EnumsAndConsts.MapGridCellHeight) + whatHeight - 1)
          If cPtr < X - 1 Then
            cPtr = X + 1
          End If
          If rPtr < Y - 1 Then
            rPtr = Y + 1
          End If
          currentData = rPtr.ToString & "|" & cPtr.ToString & "|" & aCellType.ToString & "|" & "B"
          aReturnValue.RoomDoors.Add(currentData)
          Debug.Print("bottom door=" & currentData)
        End If

        If isAnyDoor = False Then
          rNumber = m_localRandom.Next(4) + 1
          Select Case rNumber
            Case 1 'top
              xNumber = m_localRandom.Next(whatWidth - 2) + 1
              isDoorTop = True
              isAnyDoor = True
              aCellType = EnumsAndConsts.CellType.StructureDoorTopBottom
              cPtr = (X + (whatMapGridColumn - 1) * EnumsAndConsts.MapGridCellWidth) + xNumber
              rPtr = (Y + ((whatMapGridRow - 1) * EnumsAndConsts.MapGridCellHeight))
              If cPtr < X - 1 Then
                cPtr = X + 1
              End If
              If rPtr < Y - 1 Then
                rPtr = Y + 1
              End If
              currentData = rPtr.ToString & "|" & cPtr.ToString & "|" & aCellType.ToString & "|" & "T"
              aReturnValue.RoomDoors.Add(currentData)
            Case 2 'left
              xNumber = m_localRandom.Next(whatHeight - 2) + 1
              isDoorLeft = True
              isAnyDoor = True
              aCellType = EnumsAndConsts.CellType.StructureDoorSide
              cPtr = (X + ((whatMapGridColumn - 1) * EnumsAndConsts.MapGridCellWidth))
              rPtr = (Y + ((whatMapGridRow - 1) * EnumsAndConsts.MapGridCellHeight) + xNumber)
              If cPtr < X - 1 Then
                cPtr = X + 1
              End If
              If rPtr < Y - 1 Then
                rPtr = Y + 1
              End If
              currentData = rPtr.ToString & "|" & cPtr.ToString & "|" & aCellType.ToString & "|" & "L"
              aReturnValue.RoomDoors.Add(currentData)
            Case 3 'right
              xNumber = m_localRandom.Next(whatHeight - 2) + 1
              isDoorRight = True
              isAnyDoor = True
              aCellType = EnumsAndConsts.CellType.StructureDoorSide
              cPtr = (X + ((whatMapGridColumn - 1) * EnumsAndConsts.MapGridCellWidth) + whatWidth - 1)
              rPtr = (Y + ((whatMapGridRow - 1) * EnumsAndConsts.MapGridCellHeight) + xNumber)
              If cPtr < X - 1 Then
                cPtr = X + 1
              End If
              If rPtr < Y - 1 Then
                rPtr = Y + 1
              End If
              currentData = rPtr.ToString & "|" & cPtr.ToString & "|" & aCellType.ToString & "|" & "R"
              aReturnValue.RoomDoors.Add(currentData)
            Case Else 'bottom
              xNumber = m_localRandom.Next(whatWidth - 2) + 1
              isDoorBottom = True
              isAnyDoor = True
              aCellType = EnumsAndConsts.CellType.StructureDoorTopBottom
              cPtr = (X + ((whatMapGridColumn - 1) * EnumsAndConsts.MapGridCellWidth) + xNumber)
              rPtr = (Y + ((whatMapGridRow - 1) * EnumsAndConsts.MapGridCellHeight) + whatHeight - 1)
              If cPtr < X - 1 Then
                cPtr = X + 1
              End If
              If rPtr < Y - 1 Then
                rPtr = Y + 1
              End If
              currentData = rPtr.ToString & "|" & cPtr.ToString & "|" & aCellType.ToString & "|" & "B"
              aReturnValue.RoomDoors.Add(currentData)
          End Select
          Debug.Print("Forced  door=" & rNumber.ToString & "==" & currentData)
        End If

        'aReturnValue = Y.ToString & "-" & X.ToString
        aReturnValue.MapTopLocation = Y
        aReturnValue.MapLeftLocation = X
        aReturnValue.MapGridRowLocation = whatMapGridRow
        aReturnValue.MapGridColumnLocation = whatMapGridColumn
        aReturnValue.CurrentHeight = whatHeight
        aReturnValue.CurrentWidth = whatWidth
        aReturnValue.RoomNumber = ((whatMapGridRow - 1) * 3) + whatMapGridColumn
        Debug.Print("room number: " & aReturnValue.RoomNumber.ToString & " door count=" & aReturnValue.RoomDoors.Count.ToString)

        '     aReturnValue.CreateRandomDoors()
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
  End Function

  Public Function GetConnectionNumber(ByVal whatFromDoorRow As Integer, ByVal whatFromDoorColumn As Integer, ByVal whatToDoorRow As Integer, ByVal whatToDoorColumn As Integer) As String
    Dim currentMethod As String = "GetConnectionNumber"
    Dim currentData As String = ""
    Dim aReturnValue As String = ""
    Dim X As Integer = 0 ' left to right
    Dim Y As Integer = 0 ' top to bottom
    Dim aGridRowCtr As Integer = 0
    Dim aGridColumnCtr As Integer = 0
    Dim aCellPtr As Integer = 0

    Try
      'Determine the mapgridNumber from the row/column information
      Y = whatFromDoorRow
      aGridRowCtr = 1
      Do While Y > EnumsAndConsts.MapGridCellHeight
        Y = Y - EnumsAndConsts.MapGridCellHeight
        aGridRowCtr = aGridRowCtr + 1
      Loop
      X = whatFromDoorColumn
      aGridColumnCtr = 1
      Do While X > EnumsAndConsts.MapGridCellWidth
        X = X - EnumsAndConsts.MapGridCellWidth
        aGridColumnCtr = aGridColumnCtr + 1
      Loop
      'now agridxx will point to proper grid cell
      aCellPtr = (aGridRowCtr - 1) * EnumsAndConsts.MapLevelGridRowMax + aGridColumnCtr

      aReturnValue = aCellPtr.ToString & "|"

      Y = whatToDoorRow
      aGridRowCtr = 1
      Do While Y > EnumsAndConsts.MapGridCellHeight
        Y = Y - EnumsAndConsts.MapGridCellHeight
        aGridRowCtr = aGridRowCtr + 1
      Loop
      X = whatToDoorColumn
      aGridColumnCtr = 1
      Do While X > EnumsAndConsts.MapGridCellWidth
        X = X - EnumsAndConsts.MapGridCellWidth
        aGridColumnCtr = aGridColumnCtr + 1
      Loop
      'now agridxx will point to proper grid cell
      aCellPtr = (aGridRowCtr - 1) * EnumsAndConsts.MapLevelGridRowMax + aGridColumnCtr

      aReturnValue = aReturnValue & aCellPtr.ToString

    Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try


      Return aReturnValue
  End Function

  Public Overrides Function ToString() As String
    Dim aReturnValue As String = ""

      aReturnValue = RoomNumber.ToString & "=" & MapTopLocation.ToString & "-" & MapLeftLocation.ToString & "|" & CurrentHeight.ToString & "-" & CurrentWidth.ToString

      Return aReturnValue
  End Function


#End Region

#Region "Private Methods"


#End Region

  End Class
End Namespace
