Imports System
Imports System.Text

''' <summary>
''' Encode all aspects of the map for a specific level
''' 
''' Initialize 
''' Ensures that the object has all properties set
''' 
''' CreateRandomLevel
''' Will create a new random level based on criteria set in some of the properties
''' 
''' DrawScreen 
''' Will draw the current level on the DOS screen
''' 
''' 
''' </summary>
Public Class LevelMap
  Dim m_localRandom As New Random()


#Region "Public Properties"

#Region "Private Properties"

  ''' <summary>
  ''' Set this value to true to test displaying a sample screen
  ''' </summary>
  Private m_TestMode As Boolean = False

  Private m_EntryStairGrid As String
  Private m_ExitStairGrid As String
  Private m_EntryStairLocation As String
  Private m_ExitStairLocation As String
  Private m_CurrentMapLevel As Integer
  Private m_Row As Integer = 0
  Private m_Column As Integer = 0


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

  ''' <summary>
  ''' Hold a pointer to which grid cell the EntryStairLocation points to
  ''' Number is YX 
  ''' Y being the row 
  ''' X being the column 
  ''' </summary>
  ''' <returns></returns>
  Public Property EntryStairGrid() As String
    Get
      If m_EntryStairLocation.Length > 3 Then
        m_Row = -1
        m_Column = -1
        Integer.TryParse(m_EntryStairLocation.Substring(0, 2), m_Row)
        Integer.TryParse(m_EntryStairLocation.Substring(2, 2), m_Column)

        If m_Row <= 6 Then
          If m_Column < 26 Then
            m_EntryStairGrid = "11"
          End If
          If m_Column >= 27 AndAlso m_Column < 52 Then
            m_EntryStairGrid = "12"
          End If
          If m_Column >= 53 Then
            m_EntryStairGrid = "13"
          End If
        End If
        If m_Row >= 7 AndAlso m_Row <= 13 Then
          If m_Column < 26 Then
            m_EntryStairGrid = "21"
          End If
          If m_Column >= 27 AndAlso m_Column < 52 Then
            m_EntryStairGrid = "22"
          End If
          If m_Column >= 53 Then
            m_EntryStairGrid = "23"
          End If
        End If
        If m_Row >= 14 Then
          If m_Column < 26 Then
            m_EntryStairGrid = "31"
          End If
          If m_Column >= 27 AndAlso m_Column < 52 Then
            m_EntryStairGrid = "32"
          End If
          If m_Column >= 53 Then
            m_EntryStairGrid = "33"
          End If
        End If

      End If
      Return m_EntryStairGrid
    End Get
    Set(ByVal value As String)
      m_EntryStairGrid = value
    End Set
  End Property

  ''' <summary>
  ''' Hold a pointer to which grid cell the ExitStairLocation points to
  ''' Number is YX 
  ''' Y being the row 
  ''' X being the column 
  ''' </summary>
  ''' <returns></returns>
  Public Property ExitStairGrid() As String
    Get
      If m_ExitStairLocation.Length > 3 Then
        m_Row = -1
        m_Column = -1
        Integer.TryParse(m_ExitStairLocation.Substring(0, 2), m_Row)
        Integer.TryParse(m_ExitStairLocation.Substring(2, 2), m_Column)

        If m_Row <= 6 Then
          If m_Column < 26 Then
            m_ExitStairGrid = "11"
          End If
          If m_Column >= 27 AndAlso m_Column < 52 Then
            m_ExitStairGrid = "12"
          End If
          If m_Column >= 53 Then
            m_ExitStairGrid = "13"
          End If
        End If
        If m_Row >= 7 AndAlso m_Row <= 13 Then
          If m_Column < 26 Then
            m_ExitStairGrid = "21"
          End If
          If m_Column >= 27 AndAlso m_Column < 52 Then
            m_ExitStairGrid = "22"
          End If
          If m_Column >= 53 Then
            m_ExitStairGrid = "23"
          End If
        End If
        If m_Row >= 14 Then
          If m_Column < 26 Then
            m_ExitStairGrid = "31"
          End If
          If m_Column >= 27 AndAlso m_Column < 52 Then
            m_ExitStairGrid = "32"
          End If
          If m_Column >= 53 Then
            m_ExitStairGrid = "33"
          End If
        End If

      End If
      Return m_ExitStairGrid
    End Get
    Set(ByVal value As String)
      m_ExitStairGrid = value
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



  ''' <summary>
  ''' Each cell of the array contains a indicator which defines what is in that location of the map defined in enum CellType
  ''' 0 - 99 are reserved for structural elements
  ''' 100 indicates the user
  ''' 101 - 199 are reserved for future multiplayer aspects
  ''' 201 - 999 are reserved for items that can be picked up or manipulated
  ''' 1001 - x are reserved for creatures
  ''' </summary>
  Public MapCellData(EnumsAndConsts.MapHeight, EnumsAndConsts.MapWidth) As Integer

  ''' <summary>
  ''' Used to determine if a particular cell of the map has been made visible to the user.
  ''' </summary>
  Public MapCellVisibility(EnumsAndConsts.MapHeight, EnumsAndConsts.MapWidth) As Boolean

  ''' <summary>
  ''' Used to store if a particular cell of the map has a room 
  ''' Used in tunnel creation.
  ''' </summary>
  Public MapCellHasRoom(EnumsAndConsts.MapLevelGridMax + 1, EnumsAndConsts.MapLevelGridMax + 1) As String


#End Region

#Region "Public Methods"

  Public Sub New()
    Initialize(False) 'Sets the level to empty
  End Sub

  Public Sub New(ByVal whatEntryStairLocation As String, ByVal whatExitStairLocation As String)
    Initialize(False) 'Sets the level to empty
    EntryStairLocation = whatEntryStairLocation
    ExitStairLocation = whatExitStairLocation
  End Sub

  ''' <summary>
  ''' Draw the information contained in the current map level on the screen.
  ''' The current map is stored in MapCellData
  ''' MapCellVisibility(r,c) is used to control whether the cell is visible or not
  ''' </summary>
  Public Sub DrawScreen()
    Dim currentMethod As String = "DrawScreen"
    Dim currentData As String = ""
    Dim m_consoleController As New ConsoleController

    Try

      For rowPointer As Integer = 0 To EnumsAndConsts.MapHeight - 1
        For columnPointer As Integer = 0 To EnumsAndConsts.MapWidth - 1
          'If the map cell has been made visible then display proper character
          If MapCellVisibility(rowPointer, columnPointer) = True Then
            Select Case MapCellData(rowPointer, columnPointer)
              Case 0
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.Black, ConsoleColor.Black, " ")
              Case 1
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkGray, ConsoleColor.Black, "▓")
              Case 2
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, "═")
              Case 3
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, "║")
              Case 4
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, "╔")
              Case 5
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, "╗")
              Case 6
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, "╚")
              Case 7
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, "╝")
              Case 8
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, "╬")
              Case 9
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, "╬")
              Case 10
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.DarkYellow, ConsoleColor.Black, ".")
              Case 11
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.Yellow, ConsoleColor.Black, "▒")
              Case 100 'TODO could not find the proper happy face for the user
                'Dim b As Byte = Convert.ToByte(1)
                'Dim c As Char = Encoding.GetEncoding(437).GetChars(New Byte() {b})(0)
                'm_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.Yellow, ConsoleColor.Black, c)
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.Yellow, ConsoleColor.Black, "@")
              Case 203
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.Blue, ConsoleColor.Black, "¡")
              Case 204
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.Yellow, ConsoleColor.Black, "*")
              Case 205 'TODO could not find the proper RING character
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.Red, ConsoleColor.Black, "O")
              Case 301 'TODO for monsters should probably define their visual character as a property on the monster object and display that
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.White, ConsoleColor.Black, "K")
              Case 302 'TODO for monsters should probably define their visual character as a property on the monster object and display that
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.White, ConsoleColor.Black, "I")
            End Select
          Else
            'Not yet visible cells are just black
            m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.Black, ConsoleColor.Black, " ")
          End If
        Next
      Next
      m_consoleController.DisplayPlayerStats(currentPlayer)

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  ''' <summary>
  ''' Load all properties and variables with default values
  ''' </summary>
  Public Sub Initialize(ByVal whatGenerateRandomMapFlag As Boolean)
    Dim currentMethod As String = "Initialize"
    Dim currentData As String = ""

    Try
      'TODO for testing set these values
      'normally they will be set by the calling routine prior to creating the random level
      EntryStairLocation = "1831"
      ExitStairLocation = "0470"

      If whatGenerateRandomMapFlag = True Then
        CreateRandomLevel()
      Else
        ClearMapData()
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

#End Region

#Region "Private Methods"

  Private Sub ClearMapData()
    Dim currentMethod As String = "ClearMapData"
    Dim currentData As String = ""

    Try
      'Clear out the map variables
      For rowPointer As Integer = 0 To EnumsAndConsts.MapHeight
        For columnPointer As Integer = 0 To EnumsAndConsts.MapWidth
          MapCellData(rowPointer, columnPointer) = 0
          MapCellVisibility(rowPointer, columnPointer) = False
          ' testing -- make all the blank space tunnel character so it is easy to see
          'MapCellData(rowPointer, columnPointer) = EnumsAndConsts.CellType.StructureTunnel
          'MapCellVisibility(rowPointer, columnPointer) = True
        Next
      Next

    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Function CheckIfRandomRoomExists(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer) As String
    Dim currentMethod As String = "CheckIfRandomRoomExists"
    Dim currentData As String = ""
    Dim aReturnValue As String = ""
    'Dim m_hasRoomFlag As Boolean = False
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
    Dim m_roomRowOffset As Integer = 0 'offset from top side of cell
    Dim m_roomColumnOffset As Integer = 0 'offset from left side of cell
    Dim m_randomNumber As Integer = m_localRandom.Next(0, 100)
    Dim m_ISOK As Boolean = False
    Dim m_ISEntryCell As Boolean = False
    Dim m_ISExitCell As Boolean = False
    Dim m_FromX As Integer = 0 'These will hold the allowable random range that the top left corner of the room can be created in
    Dim m_ToX As Integer = 0
    Dim m_FromY As Integer = 0
    Dim m_ToY As Integer = 0
    Dim aString As String = ""

    Try
      aReturnValue = "" 'default to no room
      m_ISEntryCell = False 'default to false and set true if found
      m_ISExitCell = False 'default to false and set true if found

      'NOTE; MUST MAKE SURE TO INITIALIZE EntryStairGrid

      Integer.TryParse(EntryStairGrid.Substring(0, 1), m_entryRow)
      Integer.TryParse(EntryStairGrid.Substring(1, 1), m_entryColumn)
      Integer.TryParse(ExitStairGrid.Substring(0, 1), m_exitRow)
      Integer.TryParse(ExitStairGrid.Substring(1, 1), m_exitColumn)
      Integer.TryParse(EntryStairLocation.Substring(0, 2), m_entryRowPointer)
      Integer.TryParse(EntryStairLocation.Substring(2, 2), m_entryColumnPointer)
      Integer.TryParse(ExitStairLocation.Substring(0, 2), m_exitRowPointer)
      Integer.TryParse(ExitStairLocation.Substring(2, 2), m_exitColumnPointer)

      ' If a room has an entry or exit point, then the room must surround that point
      ' else it can be anywhere in the grid cell
      'Minimum room size if 4x4 so can have border with 2x2 inside


      m_roomWidth = m_localRandom.Next(0, EnumsAndConsts.MapGridCellWidth) + 2 'make sure there is room on the side for a corridor
      m_roomHeight = m_localRandom.Next(0, EnumsAndConsts.MapGridCellHeight - 2) + 1       'top and bottom can only be 6 vice 7 to make room for corridors
      'm_roomHeight = 4 ' testing
      'm_roomWidth = 4 ' testing
      If m_roomHeight < 4 Then
        m_roomHeight = 4 'room must have minimum floor space of 2 with wall on either side
      End If
      If m_roomWidth < 4 Then
        m_roomWidth = 4
      End If
      If m_roomHeight > EnumsAndConsts.MapGridCellHeight - 2 Then
        m_roomHeight = EnumsAndConsts.MapGridCellHeight - 2
      End If
      If m_roomWidth > EnumsAndConsts.MapGridCellWidth - 2 Then
        m_roomWidth = EnumsAndConsts.MapGridCellWidth - 2 'make sure there is room on the side for a corridor
      End If
      aReturnValue = aString & m_roomHeight.ToString & "-" & m_roomWidth.ToString & "|"


      If m_entryRow = whatMapGridRow AndAlso m_entryColumn = whatMapGridColumn Then
        aReturnValue = aReturnValue & CreateRandomEntryRoom(whatMapGridRow, whatMapGridColumn, m_entryRowPointer, m_entryColumnPointer, m_roomHeight, m_roomWidth)

        m_ISOK = True 'this is the cell for the entry into the level
      End If
      If m_exitRow = whatMapGridRow AndAlso m_exitColumn = whatMapGridColumn Then
        aReturnValue = aReturnValue & CreateRandomExitRoom(whatMapGridRow, whatMapGridColumn, m_exitRowPointer, m_exitColumnPointer, m_roomHeight, m_roomWidth)
        m_ISOK = True 'this is the cell for the exit from the level
      End If
      If m_ISOK = False Then
        'if not an entry or exit cell, then randomly choose if room exists

        Randomize()
        'generate a number from 1 to 100
        m_randomNumber = m_localRandom.Next(0, 100) + 1
        If m_randomNumber <= EnumsAndConsts.MinHasRoomPercentage Then
          aReturnValue = aReturnValue & CreateRandomRoom(whatMapGridRow, whatMapGridColumn, m_roomHeight, m_roomWidth)
          m_ISOK = True
        End If

      End If
      If m_ISOK = False Then
        aReturnValue = ""
      End If
      'MsgBox(aString)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Return aReturnValue
  End Function

  Private Function CreateRandomEntryRoom(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer, ByVal whatEntryRow As Integer, ByVal whatEntryColumn As Integer, ByVal whatHeight As Integer, ByVal whatWidth As Integer) As String
    Dim currentMethod As String = "CreateRandomEntryRoom"
    Dim currentData As String = ""
    Dim aReturnValue As String = ""
    Dim X As Integer = 0 ' left to right
    Dim Y As Integer = 0 ' top to bottom
    Dim X1 As Integer = 0 ' left to right
    Dim Y1 As Integer = 0 ' top to bottom
    Dim entryX As Integer = whatEntryColumn - ((whatMapGridColumn - 1) * 26)
    Dim entryY As Integer = whatEntryRow - ((whatMapGridRow - 1) * 7)
    Dim xOFF As Integer = (entryX - whatWidth) + 1
    Dim yOFF As Integer = (entryY - whatHeight) + 1

    Try
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
      aReturnValue = Y.ToString & "-" & X.ToString

      UpdateMap(whatMapGridRow, whatMapGridColumn, Y, X, whatHeight, whatWidth)
      MapCellData(whatEntryRow, whatEntryColumn) = EnumsAndConsts.CellType.StructureStairsDown
      MapCellVisibility(whatEntryRow, whatEntryColumn) = True 'TODO testing m_TestMode 'testing true makes all visible
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Return aReturnValue
  End Function

  Private Function CreateRandomExitRoom(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer, ByVal whatExitRow As Integer, ByVal whatExitColumn As Integer, ByVal whatHeight As Integer, ByVal whatWidth As Integer) As String
    Dim currentMethod As String = "CreateRandomExitRoom"
    Dim currentData As String = ""
    Dim aReturnValue As String = ""
    Dim X As Integer = 0 ' left to right
    Dim Y As Integer = 0 ' top to bottom
    Dim X1 As Integer = 0 ' left to right
    Dim Y1 As Integer = 0 ' top to bottom
    Dim exitX As Integer = whatExitColumn - ((whatMapGridColumn - 1) * 26)
    Dim exitY As Integer = whatExitRow - ((whatMapGridRow - 1) * 7)
    Dim xOFF As Integer = (exitX - whatWidth) + 1
    Dim yOFF As Integer = (exitY - whatHeight) + 1

    Try

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
      aReturnValue = Y.ToString & "-" & X.ToString


      'determine offset of top left corner of room based on size of room and random distance from top left of map grid
      'X = EnumsAndConsts.MapGridCellWidth - whatWidth
      'Y = EnumsAndConsts.MapGridCellHeight - whatHeight
      yOFF = (exitY - 1) - (exitY - (whatHeight - 2))
      xOFF = (exitX - 1) - ((whatWidth - 2))

      UpdateMap(whatMapGridRow, whatMapGridColumn, Y, X, whatHeight, whatWidth)
      MapCellData(whatExitRow, whatExitColumn) = EnumsAndConsts.CellType.StructureStairsDown
      MapCellVisibility(whatExitRow, whatExitColumn) = True 'TODO testing m_TestMode 'testing true makes all visible
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Return aReturnValue
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
    Dim currentMethod As String = "CreateRandomLevel"
    Dim currentData As String = ""
    Dim m_gridRowPtr As Integer = 0
    Dim m_gridColumnPtr As Integer = 0
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
    Dim m_roomRowOffset As Integer = 0 'offset from top side of cell
    Dim m_roomColumnOffset As Integer = 0 'offset from left side of cell
    Dim m_randomNumber As Integer = m_localRandom.Next()
    Dim m_mapCharacterValue As Integer = 0
    Dim m_ISOK As Boolean = False
    Dim m_ISEntryCell As Boolean = False
    Dim m_ISExitCell As Boolean = False
    Dim m_FromX As Integer = 0 'These will hold the allowable random range that the top left corner of the room can be created in
    Dim m_ToX As Integer = 0
    Dim m_FromY As Integer = 0
    Dim m_ToY As Integer = 0
    Dim aString As String = ""

    Try

      ClearMapData()

      If m_TestMode = False Then
        'NOTE; MUST MAKE SURE TO INITIALIZE EntryStairGrid

        Integer.TryParse(EntryStairGrid.Substring(0, 1), m_entryRow)
        Integer.TryParse(EntryStairGrid.Substring(1, 1), m_entryColumn)
        Integer.TryParse(ExitStairGrid.Substring(0, 1), m_exitRow)
        Integer.TryParse(ExitStairGrid.Substring(1, 1), m_exitColumn)
        Integer.TryParse(EntryStairLocation.Substring(0, 2), m_entryRowPointer)
        Integer.TryParse(EntryStairLocation.Substring(2, 2), m_entryColumnPointer)
        Integer.TryParse(ExitStairLocation.Substring(0, 2), m_exitRowPointer)
        Integer.TryParse(ExitStairLocation.Substring(2, 2), m_exitColumnPointer)

        For m_gridRowPtr = 1 To EnumsAndConsts.MapLevelGridMax
          For m_gridColumnPtr = 1 To EnumsAndConsts.MapLevelGridMax
            aString = CheckIfRandomRoomExists(m_gridRowPtr, m_gridColumnPtr)
            'Keep track of which grid cells have rooms for tunnel creation
            'astring will in the form of height-width|top-left or BLANK if no room exists
            MapCellHasRoom(m_gridRowPtr, m_gridColumnPtr) = aString
          Next
        Next
        'Create the tunnels between rooms.
        'Expects that there are at least two rooms (entry and exit)
        CreateRandomTunnels()
        'MsgBox(aString)
      Else
        LoadDefaultMap()
      End If
    Catch ex As Exception

      MsgBox(ex.Message)
    End Try

  End Sub

  Private Function CreateRandomRoom(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer, ByVal whatHeight As Integer, ByVal whatWidth As Integer) As String
    Dim currentMethod As String = "CreateRandomRoom"
    Dim currentData As String = ""
    Dim aReturnValue As String = ""
    Dim X As Integer = 0 ' left to right
    Dim Y As Integer = 0 ' top to bottom

    Try
      'determine offset of top left corner of room based on size of room and random distance from top left of map grid
      X = m_localRandom.Next(EnumsAndConsts.MapGridCellWidth - whatWidth) + 1
      Y = m_localRandom.Next(EnumsAndConsts.MapGridCellHeight - whatHeight) + 1
      If whatMapGridRow = 3 AndAlso Y < 2 Then
        Y = 2
      End If
      aReturnValue = Y.ToString & "-" & X.ToString
      UpdateMap(whatMapGridRow, whatMapGridColumn, Y, X, whatHeight, whatWidth)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Return aReturnValue
  End Function

  Private Function CreateRandomTunnels() As String
    Dim currentMethod As String = "CreateRandomTunnels"
    Dim currentData As String = ""
    Dim aReturnValue As String = ""
    Dim m_gridRowPtr As Integer = 0
    Dim m_gridColumnPtr As Integer = 0
    Dim m_hasDoorFlag As Boolean = False
    Dim aTop As Integer = 0
    Dim aLeft As Integer = 0
    Dim aHeight As Integer = 0
    Dim aWidth As Integer = 0
    Dim dataArray() As String = {""}
    Dim numberArray() As String = {""}
    Dim aCellType As Integer = 0

    Try
      For m_gridRowPtr = 1 To EnumsAndConsts.MapLevelGridMax
        For m_gridColumnPtr = 1 To EnumsAndConsts.MapLevelGridMax
          currentData = MapCellHasRoom(m_gridRowPtr, m_gridColumnPtr)
          If currentData.Length > 0 Then
            'there is a room here, so look for doors and create tunnels
            'current data now contains the dimension and location of the room
            'in the format height-width|top-left
            dataArray = currentData.Split("|")
            If dataArray.Length > 1 Then
              numberArray = dataArray(0).Split("-")
              If numberArray.Length > 1 Then
                Integer.TryParse(numberArray(0), aHeight)
                Integer.TryParse(numberArray(1), aWidth)
              End If
              numberArray = dataArray(1).Split("-")
              If numberArray.Length > 1 Then
                Integer.TryParse(numberArray(0), aTop)
                Integer.TryParse(numberArray(1), aLeft)
              End If
            End If
            m_hasDoorFlag = False
            'look at each wall to see if there is a door there
            'if there is then
            ' see how much distance exists between this door and what is in front of it
            ' start extending the tunnel out.
            ' make random turn left or right (with leaning towards potential door)
            ' keep digging until find another door or decide to stop, or gets into cul-de-sac and cannot continue
            '
            ' at least one tunnel from this room must go to another room, but some tunnels can dead end
            '
            'Look at left, top, right, then bottom walls
            'left wall will start at top, left and go down to top+height, left
            'top wall will start at top, left and go to top, left+width
            'right wall will start at top, left+width and go to top+height, left+width
            'bottom wall will start at top+height, left and go to top+height, left+width
            'examine each map location within the wall to look for a door character ()
            ' EnumsAndConsts.CellType.StructureDoorSide
            ' EnumsAndConsts.CellType.StructureDoorTopBottom
            '--------------------------------------------------------------------------------------
            'Examine LEFT wall
            For wallPtr As Integer = 0 To aHeight - 1
              'todo this does not look like it is working
              aCellType = MapCellData(aTop + wallPtr + ((m_gridRowPtr - 1) * 7), aLeft + ((m_gridColumnPtr - 1) * 26))
            Next
          End If
        Next
      Next
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Return aReturnValue
  End Function

  Private Sub LoadDefaultMap()
    Dim currentMethod As String = "LoadDefaultMap"
    Dim currentData As String = ""

    Try
      'For now just create the prototype screen for the initial challenge

      'normally this would have to define the rooms and pathways in a random level
      'These strings will not be in the actual program but are used just to get the prototype laid out easily.
      Dim m(24) As String 'this is a temporary graphical representation of the static map for the prototype
      m(0) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
      m(1) = "000000000000042222500000000000000000000000000000000000000000000422222222500000"
      m(2) = "00000000000003....3000000000000000000000000000000000000000000003........300000"
      m(3) = "00000000000003....3000000000000000000000000000000000011111111119........300000"
      m(4) = "00000000000003....3000000000000000000000000000000000010000000003........300000"
      m(5) = "00000000000003....911111111111111111100000000000000001000000000622222222700000"
      m(6) = "000000000000062282700000000000000000111111111111111111000000000000000000000000"
      m(7) = "000000001111111110000000000000000000001111111111100000000000000000000000000000"
      m(8) = "000422228222222222250000000000000000048222250000000000000000000000000000000000"
      m(9) = "0003...............3000000000000000003.....30000011111111111111110000000000000"
      m(10) = "0003..@K.....I.....9111111111111111119.....30000010000000000000010000000000000"
      m(11) = "0003...............3000000000000000003.....30000010000000000000010000000000000"
      m(12) = "0003.....!.........3000000000000000003.....91111110000000000000010000000000000"
      m(13) = "0003...O*..........3000000000000000003.....30000000000000000000010000000000000"
      m(14) = "000622222222222222270000000000000000068222270000000000000000000010000000000000"
      m(15) = "000000000000000000000000000000011111111000000000000000000111111110000000000000"
      m(16) = "000000000000000000000000000004285000000000000000000000000100000000000000000000"
      m(17) = "000000000000000000000000000003#.9111111111100000000000000100000000000000000000"
      m(18) = "000000000000000000000000111119..3000000000100000000042222822222222222222500000"
      m(19) = "000000000000000000000000000003..300000000011111111119...................300000"
      m(20) = "000000000000000000000000000003..300000000000000000003...................300000"
      m(21) = "000000000000000000000000000006227000000000000000000062222222222222222222700000"
      m(22) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
      m(23) = "Level:1    Hits:21(21)   Str:16(16)    Gold:50    Armor:5   Apprentice    "

      For rowPtr As Integer = 0 To EnumsAndConsts.MapHeight - 2
        For columnPtr As Integer = 0 To m(rowPtr).Length - 1
          Select Case m(rowPtr).Substring(columnPtr, 1)
            Case "0"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureSolidStone
            Case "1"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureTunnel
            Case "2"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureWallTopBottom
            Case "3"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureWallSide
            Case "4"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureWallTopLeftCorner
            Case "5"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureWallTopRightCorner
            Case "6"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureWallBottomLeftCorner
            Case "7"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureWallBottomRightCorner
            Case "8"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureDoorTopBottom
            Case "9"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureDoorSide
            Case "@"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.UserSelf
            Case "#"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureStairsDown
            Case "*"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.ItemGold
            Case "!"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.ItemPotion
            Case "O"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.ItemRing
            Case "K"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.MonsterKestrel
            Case "I"
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.MonsterInvisibleStalker
            Case Else
              MapCellData(rowPtr, columnPtr) = EnumsAndConsts.CellType.StructureFloor

          End Select
          'If rowPtr < 12 Then
          ' NOTE: Can test map visibility by uncommenting IF and ENDIF
          MapCellVisibility(rowPtr, columnPtr) = True
          'End If
        Next
      Next
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Private Function UpdateMap(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer, ByVal whatTLRow As Integer, ByVal whatTLColumn As Integer, ByVal whatHeight As Integer, ByVal whatWidth As Integer) As Boolean
    Dim currentMethod As String = "UpdateMap"
    Dim currentData As String = ""
    Dim aReturnValue As Boolean = True
    Dim m_mapCharacterValue As Integer = 0
    Dim isDoorTop As Boolean = False
    Dim isDoorLeft As Boolean = False
    Dim isDoorRight As Boolean = False
    Dim isDoorBottom As Boolean = False
    Dim isAnyDoor As Boolean = False
    Dim rNumber As Integer = 0
    Dim xNumber As Integer = 0

    Try
      'now update the mapdata with the random room 
      'rptr will cycle from 1 to height
      'cptr will cycle from 1 to width
      For rPtr As Integer = 1 To (whatHeight)
        For cPtr As Integer = 1 To (whatWidth)
          m_mapCharacterValue = EnumsAndConsts.CellType.StructureSolidStone

          If rPtr = 1 AndAlso cPtr = 1 Then
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopLeftCorner
          End If
          If rPtr = 1 AndAlso cPtr > 1 AndAlso cPtr < whatWidth Then
            If isDoorTop = False AndAlso whatMapGridRow > 1 Then 'cannot be a door on top on first row
              rNumber = m_localRandom.Next(10) + 1
              If rNumber < 2 Then
                isDoorTop = True
                isAnyDoor = True
                m_mapCharacterValue = EnumsAndConsts.CellType.StructureDoorTopBottom
              Else
                m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopBottom
              End If
            Else
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopBottom
            End If
          End If
          If rPtr = 1 AndAlso cPtr = (whatWidth) Then
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopRightCorner
          End If

          If rPtr > 1 AndAlso rPtr < whatHeight AndAlso (cPtr = 1) Then
            If isDoorLeft = False Then
              rNumber = m_localRandom.Next(10) + 1
              If rNumber < 2 Then
                isDoorLeft = True
                isAnyDoor = True
                m_mapCharacterValue = EnumsAndConsts.CellType.StructureDoorSide
              Else
                m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallSide
              End If
            Else
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallSide
            End If
          End If
          If rPtr > 1 AndAlso rPtr < whatHeight AndAlso (cPtr = whatWidth) Then
            If isDoorRight = False Then
              rNumber = m_localRandom.Next(10) + 1
              If rNumber < 2 Then
                isDoorRight = True
                isAnyDoor = True
                m_mapCharacterValue = EnumsAndConsts.CellType.StructureDoorSide
              Else
                m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallSide
              End If
            Else
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallSide
            End If
          End If

          If rPtr = whatHeight AndAlso cPtr = 1 Then
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallBottomLeftCorner
          End If
          If rPtr = whatHeight AndAlso cPtr > 1 AndAlso cPtr < whatWidth Then
            If isDoorBottom = False Then
              rNumber = m_localRandom.Next(10) + 1
              If rNumber < 2 Then
                isDoorBottom = True
                isAnyDoor = True
                m_mapCharacterValue = EnumsAndConsts.CellType.StructureDoorTopBottom
              Else
                m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopBottom
              End If
            Else
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopBottom
            End If
          End If
          If rPtr = whatHeight AndAlso cPtr = (whatWidth) Then
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallBottomRightCorner
          End If
          If rPtr > 1 AndAlso rPtr < whatHeight AndAlso cPtr > 1 AndAlso cPtr < whatWidth Then
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureFloor
          End If
          'ensure that pointer into map data takes into account which grid cell is being processed
          MapCellData((rPtr + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (cPtr + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
          MapCellVisibility((rPtr + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (cPtr + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = True 'TODO testing m_TestMode 'testing true makes all visible
        Next
      Next

      If isAnyDoor = False Then
        'there must be a least one door
        rNumber = m_localRandom.Next(4) + 1
        Select Case rNumber
          Case 1 'top
            xNumber = m_localRandom.Next(whatWidth - 2) + 2
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureDoorTopBottom
            If whatMapGridRow = 1 Then
              'if first row then put on bottom instead of top
              MapCellData((whatHeight + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (xNumber + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
            Else
              MapCellData((1 + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (xNumber + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
            End If
          Case 2 'bottom
            xNumber = m_localRandom.Next(whatWidth - 2) + 2
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureDoorTopBottom
            If whatMapGridRow = 3 Then
              'if bottom row then put on top instead of bottom
              MapCellData((1 + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (xNumber + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
            Else
              MapCellData((whatHeight + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (xNumber + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
            End If
          Case 3 'left
            xNumber = m_localRandom.Next(whatHeight - 2) + 2
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureDoorSide
            If whatMapGridColumn = 1 Then
              'left col put on right instead of left
              MapCellData((1 + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (xNumber + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
            Else
              MapCellData((xNumber + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (1 + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
            End If
          Case Else 'right
            xNumber = m_localRandom.Next(whatHeight - 2) + 2
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureDoorSide
            If whatMapGridColumn = 3 Then
              'right col put on right instead of left
              MapCellData((xNumber + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (1 + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
            Else
              MapCellData((1 + whatTLRow + ((whatMapGridRow - 1) * 7)) - 1, (xNumber + whatTLColumn + ((whatMapGridColumn - 1) * 26)) - 1) = m_mapCharacterValue
            End If

        End Select
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Return aReturnValue
  End Function



#End Region

End Class
