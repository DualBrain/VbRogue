Imports System
Imports System.Text

Namespace Rogue.Lib

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
    Private m_EntryStairLocation As String = ""
    Private m_ExitStairLocation As String = ""
    Private m_CurrentMapLevel As Integer
    Private m_Row As Integer = 0
    Private m_Column As Integer = 0
    Private m_Rooms As List(Of LevelRoom)
    Private m_RoomConnections As List(Of String) = New List(Of String)  ' keep track of which rooms this room is connected to

    Private m_ErrorHandler As New ErrorHandler
    Private m_CurrentObject As String = "LevelMap"

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
    Public MapCellHasRoom(EnumsAndConsts.MapLevelGridRowMax + 1, EnumsAndConsts.MapLevelGridColumnMax + 1) As String

    Public Property Rooms() As List(Of LevelRoom)
      Get
        Return m_Rooms
      End Get
      Set(ByVal value As List(Of LevelRoom))
        m_Rooms = value
      End Set
    End Property

    Public Property RoomConnections() As List(Of String)
      Get
        Return m_RoomConnections
      End Get
      Set(ByVal value As List(Of String))
        m_RoomConnections = value
      End Set
    End Property


#End Region

#Region "Public Methods"

    Public Sub New()
      Initialize(False) 'Sets the level to empty
    End Sub

    Public Sub New(ByVal whatTestMode As Boolean)
      m_TestMode = whatTestMode
      Initialize(whatTestMode) 'Sets the level to default map
      LoadDefaultMap()
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
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

    End Sub

    ''' <summary>
    ''' Load all properties and variables with default values
    ''' </summary>
    Public Sub Initialize(ByVal whatGenerateRandomMapFlag As Boolean)
      Dim currentMethod As String = "Initialize"
      Dim currentData As String = ""
      Dim aXPtr As Integer = 0
      Dim aYPtr As Integer = 0
      Dim aSeekXPtr As Integer = 0
      Dim aSeekYPtr As Integer = 0
      Dim aCurrentX As Integer = 0
      Dim aCurrentY As Integer = 0
      Dim aGridX As Integer = 0
      Dim aGridY As Integer = 0
      Dim m_randomNumber As Integer = m_localRandom.Next(0, 100)

      Try
        'TODO for testing set these values
        'normally they will be set by the calling routine prior to creating the random level
        EntryStairLocation = "1831"
        ExitStairLocation = "0470"

        m_Row = 0
        m_Column = 0
        m_Rooms = New List(Of LevelRoom)
        m_RoomConnections = New List(Of String)

        If whatGenerateRandomMapFlag = True Then
          'TODO following not yet working to randomly create entry and exit rooms
          'If EntryStairLocation = "" AndAlso ExitStairLocation = "" Then
          '  'these locations can be created by calling NEW with the correct parameters
          '  'if not, then randomly select them
          '  'first select a random grid cell, then select a random spot within the cell which is not on the edge so it can be within walls
          '  aSeekYPtr = m_localRandom.Next(0, EnumsAndConsts.MapLevelGridColumnMax * EnumsAndConsts.MapLevelGridRowMax) + 1
          '  aCurrentX = 0
          '  Do While aCurrentX < 10 AndAlso aSeekXPtr = 0
          '    aSeekXPtr = m_localRandom.Next(0, EnumsAndConsts.MapLevelGridColumnMax * EnumsAndConsts.MapLevelGridRowMax) + 1
          '    If aSeekXPtr = aSeekYPtr Then
          '      aSeekXPtr = 0 'cannot be in the same grid as entry
          '    End If
          '    aCurrentX = aCurrentX + 1
          '  Loop
          '  If aSeekXPtr = 0 Then
          '    'if could not creat random then set to next grid
          '    aSeekXPtr = aSeekYPtr + 1
          '    If aSeekXPtr > EnumsAndConsts.MapLevelGridColumnMax * EnumsAndConsts.MapLevelGridRowMax Then
          '      aSeekXPtr = 1
          '    End If
          '  End If
          '  'now we have the entry grid in aseekYptr and the exit grid in aseekXptr
          '  'pick a random location within the grid for the actual stairs making sure not to hit an edge so it will not conflict with walls
          '  aCurrentY = m_localRandom.Next(0, EnumsAndConsts.MapGridCellHeight - 2) + 2
          '  aCurrentX = m_localRandom.Next(0, EnumsAndConsts.MapGridCellWidth - 2) + 2
          '  'need to convert local grid coordinates into global map coordinates

          '  aGridY = 1
          '  aYPtr = aSeekYPtr
          '  Do While aYPtr > EnumsAndConsts.MapLevelGridColumnMax
          '    If aYPtr > EnumsAndConsts.MapLevelGridColumnMax Then
          '      aGridY = aGridY + 1
          '      aYPtr = aYPtr - EnumsAndConsts.MapLevelGridColumnMax
          '    End If
          '  Loop
          '  If aGridY = 0 Then
          '    aGridY = 1
          '  End If
          '  aGridX = aYPtr 'what is left is the x axis

          '  aCurrentY = ((aGridY - 1) * EnumsAndConsts.MapGridCellHeight) + aCurrentY
          '  aCurrentX = ((aGridX - 1) * EnumsAndConsts.MapGridCellWidth) + aCurrentX
          '  EntryStairLocation = GetCoordinateString(aCurrentY, aCurrentX)
          '  EntryStairGrid = aGridY.ToString & aGridX.ToString

          '  'pick a random location within the grid for the actual stairs making sure not to hit an edge so it will not conflict with walls
          '  aCurrentY = m_localRandom.Next(0, EnumsAndConsts.MapGridCellHeight - 2) + 2
          '  aCurrentX = m_localRandom.Next(0, EnumsAndConsts.MapGridCellWidth - 2) + 2
          '  'need to convert local grid coordinates into global map coordinates
          '  aGridY = 1
          '  aGridX = 0
          '  aYPtr = aSeekXPtr
          '  Do While aYPtr > EnumsAndConsts.MapLevelGridColumnMax
          '    If aYPtr > EnumsAndConsts.MapLevelGridColumnMax Then
          '      aGridY = aGridY + 1
          '      aYPtr = aYPtr - EnumsAndConsts.MapLevelGridColumnMax
          '    End If
          '  Loop
          '  If aGridY = 0 Then
          '    aGridY = 1
          '  End If
          '  aGridX = aYPtr 'what is left is the x axis

          '  aCurrentY = ((aGridY - 1) * EnumsAndConsts.MapGridCellHeight) + aCurrentY
          '  aCurrentX = ((aGridX - 1) * EnumsAndConsts.MapGridCellWidth) + aCurrentX
          '  ExitStairLocation = GetCoordinateString(aCurrentY, aCurrentX)
          '  ExitStairGrid = aGridY.ToString & aGridX.ToString


          'End If
          CreateRandomLevel()
        Else
          ClearMapData()
        End If
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Find out which directions are available for digging a tunnel from the current location
    ''' </summary>
    ''' <param name="whatCurrentRow"></param>
    ''' <param name="whatCurrentColumn"></param>
    ''' <returns>A string containing UDLR with unavailable directions blanked out</returns>
    Private Function CanDigNextTunnelSection(ByVal whatCurrentRow As Integer, ByVal whatCurrentColumn As Integer) As String
      Dim currentMethod As String = "CheckIfRandomRoomExists"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""
      Dim aSeekXPtr As Integer = 0
      Dim aSeekYPtr As Integer = 0
      Dim aUpString As String = " "
      Dim aDownString As String = " "
      Dim aLeftString As String = " "
      Dim aRightString As String = " "
      Dim aCellType As Integer = 0

      Try
        If whatCurrentColumn > -1 AndAlso whatCurrentColumn < EnumsAndConsts.MapWidth AndAlso whatCurrentRow > -1 AndAlso whatCurrentRow < EnumsAndConsts.MapWidth - 2 Then
          If whatCurrentRow > 1 Then
            aSeekXPtr = whatCurrentColumn
            aSeekYPtr = whatCurrentRow - 1 'up
            aCellType = MapCellData(aSeekYPtr, aSeekXPtr)
            If aCellType = EnumsAndConsts.CellType.StructureTunnel OrElse aCellType = EnumsAndConsts.CellType.StructureSolidStone Then
              aUpString = "U"
            End If
          End If

          If whatCurrentRow < EnumsAndConsts.MapHeight - 2 Then
            aSeekXPtr = whatCurrentColumn
            aSeekYPtr = whatCurrentRow + 1 'down
            aCellType = MapCellData(aSeekYPtr, aSeekXPtr)
            If aCellType = EnumsAndConsts.CellType.StructureTunnel OrElse aCellType = EnumsAndConsts.CellType.StructureSolidStone Then
              aDownString = "D"
            End If
          End If

          If whatCurrentColumn > 1 Then
            aSeekXPtr = whatCurrentColumn - 1 'left
            aSeekYPtr = whatCurrentRow
            aCellType = MapCellData(aSeekYPtr, aSeekXPtr)
            If aCellType = EnumsAndConsts.CellType.StructureTunnel OrElse aCellType = EnumsAndConsts.CellType.StructureSolidStone Then
              aLeftString = "L"
            End If
          End If

          If whatCurrentColumn < EnumsAndConsts.MapWidth - 1 Then
            aSeekXPtr = whatCurrentColumn + 1 'right
            aSeekYPtr = whatCurrentRow
            aCellType = MapCellData(aSeekYPtr, aSeekXPtr)
            If aCellType = EnumsAndConsts.CellType.StructureTunnel OrElse aCellType = EnumsAndConsts.CellType.StructureSolidStone Then
              aRightString = "R"
            End If
          End If
        End If

        aReturnValue = aUpString & aDownString & aLeftString & aRightString
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

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
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

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
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

    End Sub

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
      Dim aRoom As New LevelRoom

      Try

        aRoom = aRoom.CreateRandomEntryRoom(whatMapGridRow, whatMapGridColumn, whatEntryRow, whatEntryColumn, whatHeight, whatWidth)
        If aRoom.CurrentHeight > 0 Then
          Rooms.Add(aRoom)
          aReturnValue = aRoom.ToString
        End If
        UpdateMap(aRoom)
        MapCellData(whatEntryRow, whatEntryColumn) = EnumsAndConsts.CellType.StructureStairsDown
        MapCellVisibility(whatEntryRow, whatEntryColumn) = True 'TODO testing m_TestMode 'testing true makes all visible
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
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
      Dim aRoom As New LevelRoom

      Try

        aRoom = aRoom.CreateRandomExitRoom(whatMapGridRow, whatMapGridColumn, whatExitRow, whatExitColumn, whatHeight, whatWidth)
        If aRoom.CurrentHeight > 0 Then
          Rooms.Add(aRoom)
          aReturnValue = aRoom.ToString
        End If
        UpdateMap(aRoom)
        MapCellData(whatExitRow, whatExitColumn) = EnumsAndConsts.CellType.StructureStairsDown
        MapCellVisibility(whatExitRow, whatExitColumn) = True 'TODO testing m_TestMode 'testing true makes all visible
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
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
        ' m_TestMode = True
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

          For m_gridRowPtr = 1 To EnumsAndConsts.MapLevelGridRowMax
            For m_gridColumnPtr = 1 To EnumsAndConsts.MapLevelGridColumnMax
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
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

    End Sub

    Private Function CreateRandomRoom(ByVal whatMapGridRow As Integer, ByVal whatMapGridColumn As Integer, ByVal whatHeight As Integer, ByVal whatWidth As Integer) As String
      Dim currentMethod As String = "CreateRandomRoom"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""
      Dim X As Integer = 0 ' left to right
      Dim Y As Integer = 0 ' top to bottom
      Dim aRoom As New LevelRoom

      Try
        ''determine offset of top left corner of room based on size of room and random distance from top left of map grid
        'X = m_localRandom.Next(EnumsAndConsts.MapGridCellWidth - whatWidth) + 1
        'Y = m_localRandom.Next(EnumsAndConsts.MapGridCellHeight - whatHeight) + 1
        'If whatMapGridRow = 3 AndAlso Y < 2 Then
        '  Y = 2
        'End If
        'aReturnValue = Y.ToString & "-" & X.ToString
        aRoom = aRoom.CreateRandomRoom(whatMapGridRow, whatMapGridColumn, whatHeight, whatWidth)
        If aRoom.CurrentHeight > 0 Then
          Rooms.Add(aRoom)
          aReturnValue = aRoom.ToString
        End If
        UpdateMap(aRoom)
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    ''' <summary>
    ''' Make sure each door of each room connects to either another door (preferably in another room) or intersects a tunnel.
    ''' Make sure each room is connected to a least one other room and that every room on the level is accessible.
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
    Private Function CreateRandomTunnels() As String
      Dim currentMethod As String = "CreateRandomTunnels"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""
      Dim m_RowPtr As Integer = 0
      Dim m_ColumnPtr As Integer = 0
      Dim m_hasDoorFlag As Boolean = False
      Dim aTop As Integer = 0
      Dim aLeft As Integer = 0
      Dim aHeight As Integer = 0
      Dim aWidth As Integer = 0
      Dim dataArray() As String = {""}
      Dim numberArray() As String = {""}
      Dim aCellType As Integer = 0
      Dim aNextCellType As Integer = 0
      Dim aTunnelDirection As String = ""
      Dim aCurrentDoorLocation As String = ""
      Dim aNextDoorLocation As String = ""
      Dim aFromRowPtr As Integer = 0
      Dim aFromColumnPtr As Integer = 0
      Dim aToRowPtr As Integer = 0
      Dim aToColumnPtr As Integer = 0
      Dim aDoorColumnPtr As Integer = 0
      Dim aDoorRowPtr As Integer = 0
      Dim anyDoorFound As Boolean = False
      Dim thisDoorRow As Integer = 0
      Dim thisDoorColumn As Integer = 0
      Dim aRoom As New LevelRoom
      Dim aDoor As String = ""
      Dim aDataArray() As String = {""}
      Dim aCreatedStepCount As Integer = 0


      Try
        ' debug view rooms before tunnels drawn
        ' DrawScreen()
        'GetKeyBoardInput()

        For roomCtr As Integer = 0 To Rooms.Count - 1
          aRoom = Rooms(roomCtr)
          For doorCtr As Integer = 0 To aRoom.RoomDoors.Count - 1
            aDoor = aRoom.RoomDoors(doorCtr)
            aDataArray = aDoor.Split("|")
            If aDataArray.Length > 1 Then
              Integer.TryParse(aDataArray(0), m_RowPtr)
              Integer.TryParse(aDataArray(1), m_ColumnPtr)

            End If
            ' currentData = FindClosestDoor(m_RowPtr, m_ColumnPtr, m_RowPtr, m_ColumnPtr)
            currentData = FindClosestDoor(aRoom, aDoor)
            aDataArray = currentData.Split("|")
            If aDataArray.Length > 1 Then
              Integer.TryParse(aDataArray(0), aToRowPtr)
              Integer.TryParse(aDataArray(1), aToColumnPtr)

            End If
            Debug.Print(aRoom.ToString & "==" & aDoor & "===" & currentData)
            'set to tunnel to start just outside of the from door and to end just outside of the target door
            currentData = FindCellOutsideDoor(m_RowPtr, m_ColumnPtr)
            aDataArray = currentData.Split("|")
            If aDataArray.Length > 1 Then
              Integer.TryParse(aDataArray(0), aFromRowPtr)
              Integer.TryParse(aDataArray(1), aFromColumnPtr)

            End If
            DigNextTunnelSection(aFromRowPtr, aFromColumnPtr, 0)

            'set tunnel to end just outside target door
            currentData = FindCellOutsideDoor(aToRowPtr, aToColumnPtr)
            aDataArray = currentData.Split("|")
            If aDataArray.Length > 1 Then
              Integer.TryParse(aDataArray(0), aDoorRowPtr)
              Integer.TryParse(aDataArray(1), aDoorColumnPtr)

            End If

            ' aDoorRowPtr = aToRowPtr
            ' aDoorColumnPtr = aToColumnPtr
            Debug.Print(aFromRowPtr.ToString & "-" & aFromColumnPtr & " to " & aDoorRowPtr.ToString & "-" & aDoorColumnPtr.ToString)
            aCreatedStepCount = CreateUtilityTunnel(aFromColumnPtr, aFromRowPtr, aDoorColumnPtr, aDoorRowPtr, False)
            If aCreatedStepCount <= 0 Then
              aCreatedStepCount = aCreatedStepCount
            Else
              'Add tunnel to room connections
              currentData = aRoom.GetConnectionNumber(m_RowPtr, m_ColumnPtr, aDoorRowPtr, aDoorColumnPtr)
              RoomConnections.Add(currentData)

            End If

            ' DrawScreen()
            ' GetKeyBoardInput()
          Next
        Next



        'now that all tunnels that will be randomly created are in place
        'make sure that each room is accessible somehow.

        'DrawScreen()
        'GetKeyBoardInput()
        VerifyAllRoomsAccessible()

      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    Private Function CreateUtilityTunnel(ByVal whatFromColumnPtr As Integer, ByVal whatFromRowPtr As Integer, ByVal whatToColumnPtr As Integer, ByVal whatToRowPtr As Integer, ByVal whatFlag As Boolean) As Integer
      Dim currentMethod As String = "CreateUtilityTunnel"
      Dim currentData As String = ""
      Dim aReturnValue As Integer = 0
      Dim aRoom As New LevelRoom
      Dim aLRDifference As Integer = 0
      Dim aUDDifference As Integer = 0
      Dim aCurrentDirection As String = ""
      Dim aAvoidanceDirection As String = ""
      Dim aStepsInDirectionCtr As Integer = 0
      Dim aCurrentColumn As Integer = 0
      Dim aCurrentRow As Integer = 0
      Dim aSeekColumn As Integer = 0
      Dim aSeekRow As Integer = 0

      Try
        aLRDifference = whatFromColumnPtr - whatToColumnPtr
        aUDDifference = whatFromRowPtr - whatToRowPtr
        If Math.Abs(aUDDifference) > Math.Abs(aLRDifference) Then
          'if up-down farther then start left right
          If aLRDifference > 0 Then
            aCurrentDirection = "L"
          Else
            aCurrentDirection = "R"
          End If
          If aUDDifference > 0 Then
            aAvoidanceDirection = "U"
          Else
            aAvoidanceDirection = "D"
          End If
        Else
          If aUDDifference > 0 Then
            aCurrentDirection = "U"
          Else
            aCurrentDirection = "D"
          End If
          If aLRDifference > 0 Then
            aAvoidanceDirection = "L"
          Else
            aAvoidanceDirection = "R"
          End If
        End If

        aCurrentRow = whatFromRowPtr
        aCurrentColumn = whatFromColumnPtr

        For aReturnValue = 1 To (EnumsAndConsts.MapHeight * EnumsAndConsts.MapWidth)
          currentData = CanDigNextTunnelSection(aCurrentRow, aCurrentColumn)
          If aCurrentRow = whatToRowPtr Then
            If aCurrentColumn - whatToColumnPtr > 0 Then
              aCurrentDirection = "L"
            Else
              aCurrentDirection = "R"
            End If
          Else
            If aCurrentColumn = whatToColumnPtr Then
              If aCurrentRow - whatToRowPtr > 0 Then
                aCurrentDirection = "U"
              Else
                aCurrentDirection = "D"
              End If
            End If
          End If
          If currentData.Contains(aCurrentDirection) Then
            Select Case aCurrentDirection
              Case "U"
                aSeekRow = aCurrentRow - 1
                aSeekColumn = aCurrentColumn
              Case "D"
                aSeekRow = aCurrentRow + 1
                aSeekColumn = aCurrentColumn
              Case "L"
                aSeekRow = aCurrentRow
                aSeekColumn = aCurrentColumn - 1
              Case "R"
                aSeekRow = aCurrentRow
                aSeekColumn = aCurrentColumn + 1
            End Select
          Else
            If aCurrentDirection = aAvoidanceDirection Then
              'need to tunnel around object
              currentData = TunnelAroundObstacle(aCurrentRow, aCurrentColumn, whatToRowPtr, whatToColumnPtr, aCurrentDirection)
              currentData = currentData
              Dim aDataArray() As String
              aDataArray = currentData.Split("|")
              If aDataArray.Length > 1 Then
                Integer.TryParse(aDataArray(0), aSeekRow)
                Integer.TryParse(aDataArray(1), aSeekColumn)

              End If
              aCurrentColumn = aSeekColumn
              aCurrentRow = aSeekRow
            Else
              If currentData.Contains(aAvoidanceDirection) Then
                Select Case aAvoidanceDirection
                  Case "U"
                    aSeekRow = aCurrentRow - 1
                    aSeekColumn = aCurrentColumn
                  Case "D"
                    aSeekRow = aCurrentRow + 1
                    aSeekColumn = aCurrentColumn
                  Case "L"
                    aSeekRow = aCurrentRow
                    aSeekColumn = aCurrentColumn - 1
                  Case "R"
                    aSeekRow = aCurrentRow
                    aSeekColumn = aCurrentColumn + 1
                End Select
              Else
                'stuck
                currentData = currentData
                Exit For
              End If
            End If
          End If
          currentData = DigNextTunnelSection(aSeekRow, aSeekColumn, 0)
          aCurrentColumn = aSeekColumn
          aCurrentRow = aSeekRow

          If aCurrentRow = whatToRowPtr AndAlso aCurrentColumn = whatToColumnPtr Then
            Exit For
          End If
          If aCurrentRow = whatToRowPtr OrElse aCurrentColumn = whatToColumnPtr Then
            aStepsInDirectionCtr = 5
          End If
          'every X steps can change direction
          aStepsInDirectionCtr = aStepsInDirectionCtr + 1
          If aStepsInDirectionCtr > 5 Then
            aStepsInDirectionCtr = 0
            aLRDifference = aCurrentColumn - whatToColumnPtr
            aUDDifference = aCurrentRow - whatToRowPtr
            If aLRDifference = 0 Then
              If aUDDifference > 0 Then
                aCurrentDirection = "U"
              Else
                aCurrentDirection = "D"
              End If
            Else
              If aUDDifference = 0 Then
                If aLRDifference > 0 Then
                  aCurrentDirection = "L"
                Else
                  aCurrentDirection = "R"
                End If
              Else
                If Math.Abs(aUDDifference) > Math.Abs(aLRDifference) Then
                  'if up-down farther then start left right
                  If aLRDifference > 0 Then
                    aCurrentDirection = "L"
                  Else
                    aCurrentDirection = "R"
                  End If
                  If aUDDifference > 0 Then
                    aAvoidanceDirection = "U"
                  Else
                    aAvoidanceDirection = "D"
                  End If
                Else
                  If aUDDifference > 0 Then
                    aCurrentDirection = "U"
                  Else
                    aCurrentDirection = "D"
                  End If
                  If aLRDifference > 0 Then
                    aAvoidanceDirection = "L"
                  Else
                    aAvoidanceDirection = "R"
                  End If
                End If
              End If
            End If
          End If
        Next

        ' aReturnValue = 1
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
        aReturnValue = -1
      End Try

      Return aReturnValue
    End Function

    Private Function DigNextTunnelSection(ByVal whatRow As Integer, ByVal whatColumn As Integer, ByVal whatVisibilityRange As Integer) As String
      Dim currentMethod As String = "DigNextTunnelSection"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""

      Try
        If (whatRow > -1 AndAlso whatRow < EnumsAndConsts.MapHeight) AndAlso (whatColumn > -1 AndAlso whatColumn < EnumsAndConsts.MapWidth) Then
          MapCellData(whatRow, whatColumn) = EnumsAndConsts.CellType.StructureTunnel
          MapCellVisibility(whatRow, whatColumn) = True
          aReturnValue = whatRow.ToString & "|" & whatColumn.ToString
          Debug.Print("Stepping to " & aReturnValue)
        Else
          aReturnValue = ""
          Debug.Print("Trying to step off map")
        End If
        'TODO
        'need to not set visibility try in game
        'need to be able to use whatVisibilityRange to set visibility based on what may be in way
      Catch ex As Exception
        aReturnValue = ""
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    Private Function FindCellOutsideDoor(ByVal whatRow As Integer, ByVal whatColumn As Integer) As String
      Dim currentMethod As String = "FindCellOutsideDoor"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""
      Dim aCurrentX As Integer = 0
      Dim aCurrentY As Integer = 0
      Dim aCellType As Integer = 0
      Dim aLRDifference As Integer = 0
      Dim aUDDifference As Integer = 0
      Dim aSeekXPtr As Integer = 0
      Dim aSeekYPtr As Integer = 0
      Dim aCurrentDirection As String = ""
      Dim aLastDirection As String = ""
      Dim isDoorFound As Boolean = False
      Dim isFound As Boolean = False

      Try
        aCurrentX = whatColumn
        aCurrentY = whatRow
        'look for the first step out of the door
        'check each direction, only one will be tunnel/stone, others will be wall/floor
        aSeekXPtr = aCurrentX - 1 'left
        aSeekYPtr = aCurrentY
        aCellType = MapCellData(aSeekYPtr, aSeekXPtr)

        If aCellType = EnumsAndConsts.CellType.StructureTunnel Then
          'already a tunnel there so exit
          aReturnValue = aSeekYPtr.ToString & "|" & aSeekXPtr.ToString
          aCurrentX = aSeekXPtr
          aCurrentY = aSeekYPtr
          aLastDirection = "L"
        Else
          If aCellType = EnumsAndConsts.CellType.StructureSolidStone Then
            'go
            aReturnValue = aSeekYPtr.ToString & "|" & aSeekXPtr.ToString
            aCurrentX = aSeekXPtr
            aCurrentY = aSeekYPtr
            aLastDirection = "L"
          Else
            aSeekXPtr = aCurrentX + 1 'right
            aSeekYPtr = aCurrentY
            aCellType = MapCellData(aSeekYPtr, aSeekXPtr)
            If aCellType = EnumsAndConsts.CellType.StructureTunnel Then
              'already a tunnel there so exit
              aReturnValue = aSeekYPtr.ToString & "|" & aSeekXPtr.ToString
              aCurrentX = aSeekXPtr
              aCurrentY = aSeekYPtr
              aLastDirection = "R"
            Else
              If aCellType = EnumsAndConsts.CellType.StructureSolidStone Then
                'go
                aReturnValue = aSeekYPtr.ToString & "|" & aSeekXPtr.ToString
                aCurrentX = aSeekXPtr
                aCurrentY = aSeekYPtr
                aLastDirection = "R"
              Else
                aSeekXPtr = aCurrentX
                aSeekYPtr = aCurrentY - 1 'up
                aCellType = MapCellData(aSeekYPtr, aSeekXPtr)
                If aCellType = EnumsAndConsts.CellType.StructureTunnel Then
                  'already a tunnel there so exit
                  aReturnValue = aSeekYPtr.ToString & "|" & aSeekXPtr.ToString
                  aCurrentX = aSeekXPtr
                  aCurrentY = aSeekYPtr
                  aLastDirection = "U"
                Else
                  If aCellType = EnumsAndConsts.CellType.StructureSolidStone Then
                    'go
                    aReturnValue = aSeekYPtr.ToString & "|" & aSeekXPtr.ToString
                    aCurrentX = aSeekXPtr
                    aCurrentY = aSeekYPtr
                    aLastDirection = "U"
                  Else
                    aSeekXPtr = aCurrentX
                    aSeekYPtr = aCurrentY + 1 'down
                    aCellType = MapCellData(aSeekYPtr, aSeekXPtr)
                    If aCellType = EnumsAndConsts.CellType.StructureTunnel Then
                      'already a tunnel there so exit
                      aReturnValue = aSeekYPtr.ToString & "|" & aSeekXPtr.ToString
                      aCurrentX = aSeekXPtr
                      aCurrentY = aSeekYPtr
                      aLastDirection = "D"
                    Else
                      If aCellType = EnumsAndConsts.CellType.StructureSolidStone Then
                        'go
                        aReturnValue = aSeekYPtr.ToString & "|" & aSeekXPtr.ToString
                        aCurrentX = aSeekXPtr
                        aCurrentY = aSeekYPtr
                        aLastDirection = "D"
                      End If
                    End If
                  End If
                End If
              End If
            End If
          End If
        End If

      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    ''' <summary>
    ''' Try to find a door to tunnel to from the room and door provided
    ''' Try to find a next in one of the grid cells next to this one
    ''' else just find one
    ''' </summary>
    ''' <param name="whatRoom"></param>
    ''' <param name="whatDoor"></param>
    ''' <returns></returns>
    Private Function FindClosestDoor(ByRef whatRoom As LevelRoom, ByRef whatDoor As String) As String
      Dim currentMethod As String = "GetRoomNumberFromXY"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""
      Dim aFromRowPtr As Integer = 0
      Dim aFromColumnPtr As Integer = 0
      Dim aToRowPtr As Integer = 0
      Dim aToColumnPtr As Integer = 0
      Dim aDoorColumnPtr As Integer = 0
      Dim aDoorRowPtr As Integer = 0
      Dim anyDoorFound As Boolean = False
      Dim thisDoorRow As Integer = 0
      Dim thisDoorColumn As Integer = 0
      Dim aRoom As New LevelRoom
      Dim aDoor As String = ""
      Dim aDataArray() As String = {""}
      Dim canLookLeft As Boolean = False
      Dim canLookRight As Boolean = False
      Dim canLookUp As Boolean = False
      Dim canLookDown As Boolean = False
      Dim aFromRoomPtr As Integer = 0
      Dim aToRoomPtr As Integer = 0
      Dim isRoomFound As Boolean = False
      Dim aFoundRoomsList As New List(Of LevelRoom)
      Dim aPossibleConnection As String = ""
      Dim isFound As Boolean = False
      Dim reverseConnection As String = ""

      Try
        For aRowPtr As Integer = 1 To EnumsAndConsts.MapLevelGridRowMax
          For aColumnPtr As Integer = 1 To EnumsAndConsts.MapLevelGridColumnMax
            aFromRoomPtr = ((aRowPtr - 1) * EnumsAndConsts.MapLevelGridColumnMax) + aColumnPtr
            If aFromRoomPtr = whatRoom.RoomNumber Then
              If aRowPtr = 1 Then
                canLookUp = False
              Else
                canLookUp = True
                aToRoomPtr = ((aRowPtr - 2) * EnumsAndConsts.MapLevelGridColumnMax) + aColumnPtr
                For rPtr As Integer = 0 To Rooms.Count - 1
                  If aToRoomPtr = Rooms(rPtr).RoomNumber Then
                    currentData = whatRoom.RoomNumber.ToString & "|" & Rooms(rPtr).RoomNumber.ToString
                    reverseConnection = Rooms(rPtr).RoomNumber.ToString & "|" & whatRoom.RoomNumber.ToString
                    isFound = False
                    For cPtr As Integer = 0 To RoomConnections.Count - 1
                      If currentData = RoomConnections(cPtr) OrElse reverseConnection = RoomConnections(cPtr) Then
                        isFound = True
                        Exit For
                      End If
                    Next
                    If isFound = False Then
                      'do not use room if already has a connection
                      aFoundRoomsList.Add(Rooms(rPtr))
                    End If
                    Exit For
                  End If
                Next
              End If
              If aRowPtr = EnumsAndConsts.MapLevelGridRowMax Then
                canLookDown = False
              Else
                canLookDown = True
                aToRoomPtr = ((aRowPtr - 0) * EnumsAndConsts.MapLevelGridColumnMax) + aColumnPtr
                For rPtr As Integer = 0 To Rooms.Count - 1
                  If aToRoomPtr = Rooms(rPtr).RoomNumber Then
                    currentData = whatRoom.RoomNumber.ToString & "|" & Rooms(rPtr).RoomNumber.ToString
                    reverseConnection = Rooms(rPtr).RoomNumber.ToString & "|" & whatRoom.RoomNumber.ToString
                    isFound = False
                    For cPtr As Integer = 0 To RoomConnections.Count - 1
                      If currentData = RoomConnections(cPtr) OrElse reverseConnection = RoomConnections(cPtr) Then
                        isFound = True
                        Exit For
                      End If
                    Next
                    If isFound = False Then
                      'do not use room if already has a connection
                      aFoundRoomsList.Add(Rooms(rPtr))
                    End If
                    Exit For
                  End If
                Next
              End If
              If aColumnPtr = 1 Then
                canLookLeft = False
              Else
                canLookLeft = True
                aToRoomPtr = ((aRowPtr - 1) * EnumsAndConsts.MapLevelGridColumnMax) + aColumnPtr - 1
                For rPtr As Integer = 0 To Rooms.Count - 1
                  If aToRoomPtr = Rooms(rPtr).RoomNumber Then
                    currentData = whatRoom.RoomNumber.ToString & "|" & Rooms(rPtr).RoomNumber.ToString
                    reverseConnection = Rooms(rPtr).RoomNumber.ToString & "|" & whatRoom.RoomNumber.ToString
                    isFound = False
                    For cPtr As Integer = 0 To RoomConnections.Count - 1
                      If currentData = RoomConnections(cPtr) OrElse reverseConnection = RoomConnections(cPtr) Then
                        isFound = True
                        Exit For
                      End If
                    Next
                    If isFound = False Then
                      'do not use room if already has a connection
                      aFoundRoomsList.Add(Rooms(rPtr))
                    End If
                    Exit For
                  End If
                Next
              End If
              If aColumnPtr = EnumsAndConsts.MapLevelGridColumnMax Then
                canLookRight = False
              Else
                canLookRight = True
                aToRoomPtr = ((aRowPtr - 1) * EnumsAndConsts.MapLevelGridColumnMax) + aColumnPtr + 1
                For rPtr As Integer = 0 To Rooms.Count - 1
                  If aToRoomPtr = Rooms(rPtr).RoomNumber Then
                    currentData = whatRoom.RoomNumber.ToString & "|" & Rooms(rPtr).RoomNumber.ToString
                    reverseConnection = Rooms(rPtr).RoomNumber.ToString & "|" & whatRoom.RoomNumber.ToString
                    isFound = False
                    For cPtr As Integer = 0 To RoomConnections.Count - 1
                      If currentData = RoomConnections(cPtr) OrElse reverseConnection = RoomConnections(cPtr) Then
                        isFound = True
                        Exit For
                      End If
                    Next
                    If isFound = False Then
                      'do not use room if already has a connection
                      aFoundRoomsList.Add(Rooms(rPtr))
                    End If
                    Exit For
                  End If
                Next
              End If
            End If
          Next
        Next

        'now we know what ways we can look
        If aFoundRoomsList.Count > 0 Then
          'aReturnValue = aFoundRoomsList(0).RoomDoors(0).ToString ' need to actually pick one of these
          aToRoomPtr = m_localRandom.Next(aFoundRoomsList.Count - 1) '+ 1 do not need +1 because list is zero based
          aReturnValue = aFoundRoomsList(aToRoomPtr).RoomDoors(0).ToString
        Else
          'need to pick any random room
          For tryPtr As Integer = 0 To 10
            aToRoomPtr = m_localRandom.Next(Rooms.Count - 1) + 1
            If Not Rooms(aToRoomPtr).RoomNumber = whatRoom.RoomNumber Then
              aReturnValue = Rooms(aToRoomPtr).RoomDoors(0).ToString
              Exit For
            End If
          Next
        End If


      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
        aReturnValue = aReturnValue
      End Try

      Return aReturnValue
    End Function

    Private Function GetCoordinateString(ByVal whatRow As Integer, ByVal whatColumn As Integer) As String
      Dim currentMethod As String = "GetRoomNumberFromXY"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""

      Try
        currentData = "0" & whatRow.ToString
        aReturnValue = currentData.Substring(currentData.Length - 2)
        currentData = "0" & whatColumn.ToString
        aReturnValue = aReturnValue & currentData.Substring(currentData.Length - 2)

      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    Private Function GetRoomFromNumber(ByVal whatRoomNumber) As LevelRoom
      Dim currentMethod As String = "GetRoomNumberFromXY"
      Dim currentData As String = ""
      Dim aReturnValue As New LevelRoom

      Try
        For Each foundRoom As LevelRoom In Rooms
          If foundRoom.RoomNumber = whatRoomNumber Then
            aReturnValue = foundRoom
          End If
        Next
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    ''' <summary>
    ''' Find out which grid cell a specific point is within 
    ''' There may or not be a room there
    ''' </summary>
    ''' <param name="whatX">The column of the specific point</param>
    ''' <param name="whatY">The row of the specific point</param>
    ''' <returns>
    ''' the number of the room at the point would be at the point requested if it exists
    ''' </returns>
    Private Function GetRoomNumberFromXY(ByVal whatX As Integer, ByVal whatY As Integer) As Integer
      Dim currentMethod As String = "GetRoomNumberFromXY"
      Dim currentData As String = ""
      Dim aReturnValue As Integer = 0
      Dim aTop As Integer = 0
      Dim aLeft As Integer = 0
      Dim aBottom As Integer = 0
      Dim aRight As Integer = 0

      Try
        For rowPtr As Integer = 0 To EnumsAndConsts.MapLevelGridRowMax - 1
          For colPtr As Integer = 0 To EnumsAndConsts.MapLevelGridColumnMax - 1
            aTop = rowPtr * EnumsAndConsts.MapGridCellHeight
            aLeft = colPtr * EnumsAndConsts.MapGridCellWidth
            aBottom = aTop + EnumsAndConsts.MapGridCellHeight
            aRight = aLeft + EnumsAndConsts.MapGridCellWidth
            If aTop <= whatY AndAlso aBottom >= whatY AndAlso aLeft <= whatX AndAlso aRight >= whatX Then
              aReturnValue = rowPtr * 3 + colPtr + 1
              Exit For
            End If
          Next
          If aReturnValue > 0 Then
            Exit For
          End If
        Next

      Catch ex As Exception
        'MsgBox(ex.Message)
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
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

        'need to initialize Rooms array

      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

    End Sub

    Private Sub LoadEmptyMap()
      Dim currentMethod As String = "LoadEmptyMap"
      Dim currentData As String = ""

      Try
        'For now just create the prototype screen for the initial challenge

        'normally this would have to define the rooms and pathways in a random level
        'These strings will not be in the actual program but are used just to get the prototype laid out easily.
        Dim m(24) As String 'this is a temporary graphical representation of the static map for the prototype
        m(0) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(1) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(2) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(3) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(4) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(5) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(6) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(7) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(8) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(9) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(10) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(11) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(12) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(13) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(14) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(15) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(16) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(17) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(18) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(19) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(20) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
        m(21) = "000000000000000000000000000000000000000000000000000000000000000000000000000000"
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
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

    End Sub

    ''' <summary>
    ''' When a tunnel is being dug, if it hits an obstruction on the way to its goal, 
    ''' it needs to dig around the obstruction so that it can continue in the desired direction.
    ''' </summary>
    ''' <param name="whatCurrentRow"></param>
    ''' <param name="whatCurrentColumn"></param>
    ''' <param name="whatTargetRow"></param>
    ''' <param name="whatTargetColumn"></param>
    ''' <param name="whatCurrentDirection"></param>
    ''' <returns></returns>
    Private Function TunnelAroundObstacle(ByVal whatCurrentRow As Integer, ByVal whatCurrentColumn As Integer, ByVal whatTargetRow As Integer, ByVal whatTargetColumn As Integer, ByVal whatCurrentDirection As String) As String
      Dim currentMethod As String = "TunnelAroundObstacle"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""
      Dim aCurrentX As Integer = 0
      Dim aCurrentY As Integer = 0
      Dim aSeekXPtr As Integer = 0
      Dim aSeekYPtr As Integer = 0
      Dim aCellType As Integer = 0
      Dim isFound As Boolean = True
      Dim aStepCtr As Integer = 0
      Dim aAvoidanceDirection As String = ""
      Dim aBlockingRoomNumber As Integer = 0
      Dim aBlockingRoom As New LevelRoom
      Dim aDistanceOne As Integer = 0
      Dim aDistanceTwo As Integer = 0
      Dim aAvailableDirections As String = ""
      Dim aStatus As String = "B" ' B = going around block, C = continuing past block

      Try
        Debug.Print("TunnelAroundObstacle From: " & whatCurrentRow.ToString & "-" & whatCurrentColumn.ToString & " to " & whatTargetRow.ToString & "-" & whatTargetColumn.ToString & " Direction = " & whatCurrentDirection)
        aAvailableDirections = CanDigNextTunnelSection(whatCurrentRow, whatCurrentColumn)

        aCurrentY = whatCurrentRow
        aCurrentX = whatCurrentColumn
        'make sure current direction is blocked
        Select Case whatCurrentDirection
          Case "U"
            aSeekYPtr = aCurrentY - 1
            aSeekXPtr = aCurrentX
          Case "D"
            aSeekYPtr = aCurrentY + 1
            aSeekXPtr = aCurrentX
          Case "L"
            aSeekYPtr = aCurrentY
            aSeekXPtr = aCurrentX - 1
          Case "R"
            aSeekYPtr = aCurrentY
            aSeekXPtr = aCurrentX + 1
        End Select
        aCellType = MapCellData(aSeekYPtr, aSeekXPtr)
        If aCellType = EnumsAndConsts.CellType.StructureSolidStone OrElse aCellType = EnumsAndConsts.CellType.StructureTunnel Then
          'original direction clear
          DigNextTunnelSection(aSeekYPtr, aSeekXPtr, 0)

          'MapCellData(aSeekYPtr, aSeekXPtr) = EnumsAndConsts.CellType.StructureTunnel
          'MapCellVisibility(aSeekYPtr, aSeekXPtr) = True
          ' take the step in that direction then
          isFound = False 'the way is clear so exit
          aCurrentY = aSeekYPtr
          aCurrentX = aSeekXPtr
          aReturnValue = aCurrentY.ToString & "|" & aCurrentX.ToString
        Else
          aBlockingRoomNumber = GetRoomNumberFromXY(aSeekXPtr, aSeekYPtr)
          If aBlockingRoomNumber > 0 Then
            aBlockingRoom = GetRoomFromNumber(aBlockingRoomNumber)
          End If
          'find out the shortest side of the blocking room and go that way
          If whatCurrentDirection = "D" OrElse whatCurrentDirection = "U" Then
            aDistanceOne = aCurrentX - aBlockingRoom.ActualMapLeftLocation
            aDistanceTwo = (aBlockingRoom.ActualMapLeftLocation + aBlockingRoom.CurrentWidth) - aCurrentX
            If aDistanceOne > aDistanceTwo Then
              aAvoidanceDirection = "R"
            Else
              aAvoidanceDirection = "L"
            End If
          Else
            aDistanceOne = aCurrentY - aBlockingRoom.ActualMapTopLocation
            aDistanceTwo = (aBlockingRoom.ActualMapTopLocation + aBlockingRoom.CurrentHeight) - aCurrentY
            If aDistanceOne > aDistanceTwo Then
              aAvoidanceDirection = "D"
            Else
              aAvoidanceDirection = "U"
            End If
          End If

          isFound = True
          Do While isFound = True AndAlso aStepCtr < EnumsAndConsts.MapHeight * EnumsAndConsts.MapWidth
            aAvailableDirections = CanDigNextTunnelSection(aCurrentY, aCurrentX)
            If aAvailableDirections.Trim.Length > 0 Then
              'there are available moves from here
              If aAvailableDirections.Contains(whatCurrentDirection) Then
                'we can continue on our way
                'do so until block we were going around is cleared
                Select Case aAvoidanceDirection
                  Case "D"
                    If aAvailableDirections.Contains("U") Then
                      aStatus = "C"
                    Else
                      aStatus = "D" 'don going around
                    End If
                  Case "U"
                    If aAvailableDirections.Contains("D") Then
                      aStatus = "C"
                    Else
                      aStatus = "D" 'don going around
                    End If
                  Case "L"
                    If aAvailableDirections.Contains("R") Then
                      aStatus = "C"
                    Else
                      aStatus = "D" 'don going around
                    End If
                  Case "R"
                    If aAvailableDirections.Contains("L") Then
                      aStatus = "C"
                    Else
                      aStatus = "D" 'don going around
                    End If
                End Select
                If aStatus = "D" Then
                  'we are around block so exit 
                  aReturnValue = aCurrentY.ToString & "|" & aCurrentX.ToString
                  isFound = True 'found a door so exit
                Else
                  'we should continue
                  If whatCurrentDirection = "U" Then
                    aSeekYPtr = aCurrentY - 1
                    aSeekXPtr = aCurrentX
                  End If
                  If whatCurrentDirection = "D" Then
                    aSeekYPtr = aCurrentY + 1
                    aSeekXPtr = aCurrentX
                  End If
                  If whatCurrentDirection = "L" Then
                    aSeekYPtr = aCurrentY
                    aSeekXPtr = aCurrentX - 1
                  End If
                  If whatCurrentDirection = "R" Then
                    aSeekYPtr = aCurrentY
                    aSeekXPtr = aCurrentX + 1
                  End If
                  DigNextTunnelSection(aSeekYPtr, aSeekXPtr, 0)
                  aCurrentX = aSeekXPtr
                  aCurrentY = aSeekYPtr
                End If
              Else
                'if cannot go in current desired direction then continue in avoidance direction
                If aAvoidanceDirection = "U" Then
                  aSeekYPtr = aCurrentY - 1
                  aSeekXPtr = aCurrentX
                End If
                If aAvoidanceDirection = "D" Then
                  aSeekYPtr = aCurrentY + 1
                  aSeekXPtr = aCurrentX
                End If
                If aAvoidanceDirection = "L" Then
                  aSeekYPtr = aCurrentY
                  aSeekXPtr = aCurrentX - 1
                End If
                If aAvoidanceDirection = "R" Then
                  aSeekYPtr = aCurrentY
                  aSeekXPtr = aCurrentX + 1
                End If
                DigNextTunnelSection(aSeekYPtr, aSeekXPtr, 0)
                aCurrentX = aSeekXPtr
                aCurrentY = aSeekYPtr
              End If
            End If
            aStepCtr = aStepCtr + 1
          Loop
        End If

        If aStepCtr > 200 Then
          isFound = isFound
        End If
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function

    ''' <summary>
    ''' The the data from a recently created random room and put it in the MapData() object used by all the level functions
    ''' </summary>
    ''' <param name="whatRoom">The LevelRoom object to be placed within the MapData array.</param>
    ''' <returns>A string with the top and left point of the room encoded as T-L</returns>
    Private Function UpdateMap(ByRef whatRoom As LevelRoom) As String
      Dim currentMethod As String = "UpdateMap"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""
      Dim m_mapCharacterValue As Integer = 0
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
      Dim aTopPtr As Integer = EnumsAndConsts.MapHeight
      Dim aLeftPtr As Integer = EnumsAndConsts.MapWidth
      Dim aDirectionPtr As String = ""
      Dim aDoorCharacter As Integer = 0
      Dim xPtr As Integer = 0
      Dim yPtr As Integer = 0


      Try
        'now update the mapdata with the random room 
        'rptr will cycle from 1 to height
        'cptr will cycle from 1 to width
        For rPtr As Integer = 1 To (whatRoom.CurrentHeight)
          For cPtr As Integer = 1 To (whatRoom.CurrentWidth)
            m_mapCharacterValue = EnumsAndConsts.CellType.StructureSolidStone

            If rPtr = 1 AndAlso cPtr = 1 Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopLeftCorner
            End If
            If rPtr = 1 AndAlso cPtr > 1 AndAlso cPtr < whatRoom.CurrentWidth Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopBottom
            End If
            If rPtr = 1 AndAlso cPtr = (whatRoom.CurrentWidth) Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopRightCorner
            End If

            If rPtr > 1 AndAlso rPtr < whatRoom.CurrentHeight AndAlso (cPtr = 1) Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallSide
            End If
            If rPtr > 1 AndAlso rPtr < whatRoom.CurrentHeight AndAlso (cPtr = whatRoom.CurrentWidth) Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallSide
            End If

            If rPtr = whatRoom.CurrentHeight AndAlso cPtr = 1 Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallBottomLeftCorner
            End If
            If rPtr = whatRoom.CurrentHeight AndAlso cPtr > 1 AndAlso cPtr < whatRoom.CurrentWidth Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallTopBottom
            End If
            If rPtr = whatRoom.CurrentHeight AndAlso cPtr = (whatRoom.CurrentWidth) Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureWallBottomRightCorner
            End If
            If rPtr > 1 AndAlso rPtr < whatRoom.CurrentHeight AndAlso cPtr > 1 AndAlso cPtr < whatRoom.CurrentWidth Then
              m_mapCharacterValue = EnumsAndConsts.CellType.StructureFloor
            End If
            ''ensure that pointer into map data takes into account which grid cell is being processed
            yPtr = (rPtr + whatRoom.MapTopLocation + ((whatRoom.MapGridRowLocation - 1) * 7)) - 1
            xPtr = (cPtr + whatRoom.MapLeftLocation + ((whatRoom.MapGridColumnLocation - 1) * 26)) - 1
            MapCellData(yPtr, xPtr) = m_mapCharacterValue
            MapCellVisibility(yPtr, xPtr) = True ' testing m_TestMode 'testing true makes all visible
            If yPtr < aTopPtr Then
              aTopPtr = yPtr
            End If
            If xPtr < aLeftPtr Then
              aLeftPtr = xPtr
            End If
          Next
        Next
        'now show the doors
        For Each doorData As String In whatRoom.RoomDoors
          aDataArray = doorData.Split("|")
          If aDataArray.Length > 2 Then
            Integer.TryParse(aDataArray(0), yPtr)
            Integer.TryParse(aDataArray(1), xPtr)
            Integer.TryParse(aDataArray(2), m_mapCharacterValue)
          End If
          MapCellData(yPtr, xPtr) = m_mapCharacterValue
          MapCellVisibility(yPtr, xPtr) = True ' testing m_TestMode 'testing true makes all visible

        Next

        aReturnValue = aTopPtr.ToString & "-" & aLeftPtr.ToString
      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
        aReturnValue = ""
      End Try

      Return aReturnValue
    End Function

    Private Function VerifyAllRoomsAccessible() As String
      Dim currentMethod As String = "ExtendTunnel"
      Dim currentData As String = ""
      Dim aReturnValue As String = ""
      Dim aToRowPtr As Integer = 0
      Dim aToColumnPtr As Integer = 0
      Dim aFromDoorColumnPtr As Integer = 0
      Dim aFromDoorRowPtr As Integer = 0
      Dim aToDoorRowPtr As Integer = 0
      Dim aToDoorColumnPtr As Integer = 0
      Dim aConnectionList As New List(Of String)
      Dim aFromRoomPtr As Integer = 0
      Dim aToRoomPtr As Integer = 0
      Dim aFromPtr As Integer = 0
      Dim aToPtr As Integer = 0
      Dim aDataArray() As String = {""}
      Dim isFound As Boolean = False
      Dim aDoorCountLeft As Integer = 0
      Dim aDoorCountRight As Integer = 0
      Dim aPossiblePtr As Integer = 0
      Dim aConnectingPtr As Integer = 0
      Dim aRightRoom As New LevelRoom
      Dim aLeftRoom As New LevelRoom
      Dim aFromDoorData As String = ""
      Dim aTopBottomDoorData As String = ""
      Dim aOppositeDoorData As String = ""
      Dim aToDoorData As String = ""
      Dim aDirectionPointer As String = ""
      Dim aCurrentX As Integer = 0
      Dim aCurrentY As Integer = 0
      Dim aCellTypeUp As Integer = 0
      Dim aCellTypeDown As Integer = 0
      Dim aCellTypeLeft As Integer = 0
      Dim aCellTypeRight As Integer = 0
      Dim aStepList As New List(Of String)
      Dim aTunnelStepList As New List(Of String)
      Dim aSeekXPtr As Integer = 0
      Dim aSeekYPtr As Integer = 0
      Dim aPriorXPtr As Integer = 0
      Dim aPriorYPtr As Integer = 0
      Dim aStepFound As Boolean = False
      Dim aStepCtr As Integer = 0
      Dim aCurrentStepString As String = ""
      Dim isSameRoom As Boolean = False
      Dim canContinueFlag As Boolean = True
      Dim aCycleCtr As Integer = 0
      Dim aCreatedStepCount As Integer = 0
      Dim aDoorType As Integer = 0

      Try
        'Build list of connections between rooms
        For Each currentData In RoomConnections
          aDataArray = currentData.Split("|")
          If aDataArray.Length > 1 Then
            Integer.TryParse(aDataArray(0), aFromPtr)
            Integer.TryParse(aDataArray(1), aToPtr)

          End If
          aConnectionList.Add(currentData)
          If Not aFromPtr = aToPtr Then
            aConnectionList.Add(aToPtr.ToString & "|" & aFromPtr.ToString) 'make sure that the connects are showing both ways
          End If
        Next

        'DEBUGGING DATA
        Debug.Print("Room Connections")
        For Each currentData In RoomConnections
          Debug.Print(currentData)
        Next
        Debug.Print("ConnectionList")
        For Each currentData In aConnectionList
          Debug.Print(currentData)
        Next

        Debug.Print("Doors in Rooms")
        For Each foundRoom As LevelRoom In Rooms
          For Each existingDoor As String In foundRoom.RoomDoors
            Debug.Print(foundRoom.RoomNumber.ToString & "=" & existingDoor)
          Next
        Next

        'Now examine the doors in each room that is not already in the connections list.
        'Follow the tunnel from each door to see if it connects to a room that is not showing in the connection list and add if found.
        'If no connection found, add a utility tunnel to room left or right. 
        'Choosing left-right utility tunnel connections to help with interconnecting all rooms.
        '
        'Choose room with least number of connections in list. Chose the door in that room closest to the room coming from.
        'Utility direct tunnels should start from the first point outside the their door and head directly to their destination. 
        '   If a tunnel is found before the target door, follow it to make sure that it goes to a door, even though tunnels do not exist without a door.
        '   If obstructed by a wall, Step around it In the way away from the door (because that will Get To a corner sooner) 
        '   And follow wall until can turn toward door.

        aCycleCtr = 0
        Do While canContinueFlag = True AndAlso aCycleCtr < EnumsAndConsts.MapHeight * EnumsAndConsts.MapWidth

          'Finally walk through all the rooms to ensure that every room is accessible
          aStepCtr = 0
          aStepList = New List(Of String)
          aTunnelStepList = New List(Of String)
          For Each currentData In aConnectionList
            aDataArray = currentData.Split("|")
            If aDataArray.Length > 1 Then
              Integer.TryParse(aDataArray(0), aFromPtr)
            End If
            isFound = False
            For Each foundStep As String In aStepList
              If foundStep = aFromPtr.ToString Then
                isFound = True
                Exit For
              End If
            Next
            If isFound = False Then
              aStepList.Add(aFromPtr.ToString)
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
          If aStepList.Count > 0 Then
            aTunnelStepList = New List(Of String)
            aTunnelStepList.Add(aStepList(0))
            aStepList.RemoveAt(0)
            aStepCtr = 0

            aCurrentStepString = aTunnelStepList(0)
            aTunnelStepList.RemoveAt(0)
            Do While aStepCtr < EnumsAndConsts.MapHeight * EnumsAndConsts.MapWidth
              For Each currentData In aConnectionList
                aDataArray = currentData.Split("|")
                If aDataArray.Length > 1 Then
                  Integer.TryParse(aDataArray(0), aFromPtr)
                  Integer.TryParse(aDataArray(1), aToPtr)
                End If
                If aFromPtr.ToString = aCurrentStepString Then
                  'this is a connection from the working room.
                  'see if it goes to a room still in asteplist but not yet in atunnellist
                  'if so remove it from asteplist and add it to atunnellist
                  isFound = False
                  For lPtr As Integer = 0 To aStepList.Count - 1
                    If aStepList(lPtr) = aToPtr.ToString Then
                      'still in step list so remove and add to tunnellist if not already there
                      isFound = False
                      For Each foundItem As String In aTunnelStepList
                        If foundItem = aToPtr.ToString Then
                          isFound = True 'already in atunnellist
                          Exit For
                        End If
                      Next
                      If isFound = False Then
                        'not there then add to tunnellist for further processing
                        aTunnelStepList.Add(aStepList(lPtr))
                      End If
                      aStepList.RemoveAt(lPtr)
                      Exit For
                    End If
                  Next
                End If

              Next

              If aTunnelStepList.Count = 0 Then
                Exit Do
              Else
                aCurrentStepString = aTunnelStepList(0)
                aTunnelStepList.RemoveAt(0)
              End If
              'else try again till done
              aStepCtr = aStepCtr + 1
            Loop

            'if steplist has any rooms left after tunnellist emptied then add tunnel
            If aStepList.Count = 0 Then
              Exit Do
            Else
              'need to tunnel between first room left in asteplist to some room not in asteplist
              isFound = isFound
              aFromDoorColumnPtr = 0
              aFromDoorRowPtr = 0
              aToDoorColumnPtr = 0
              aToDoorRowPtr = 0
              Integer.TryParse(aStepList(0), aFromRoomPtr)
              aToRoomPtr = 0
              For Each foundRoom As LevelRoom In Rooms
                If foundRoom.RoomNumber = aFromRoomPtr Then
                  'get a door in this room to start from
                  currentData = foundRoom.RoomDoors(0)
                  aDataArray = currentData.Split("|")
                  If aDataArray.Length > 1 Then
                    Integer.TryParse(aDataArray(0), aFromDoorRowPtr)
                    Integer.TryParse(aDataArray(1), aFromDoorColumnPtr)
                    Integer.TryParse(aDataArray(2), aDoorType)
                  End If
                  Exit For
                End If
              Next
              'now find a room not in asteplist to connect to
              'try rooms next door first

              For Each foundRoom As LevelRoom In Rooms
                isFound = False
                For Each foundString As String In aStepList
                  If foundString = foundRoom.RoomNumber.ToString Then
                    isFound = True
                    Exit For
                  End If
                Next
                If isFound = False Then
                  'this may be it!
                  If (aFromRoomPtr + 1 = foundRoom.RoomNumber) OrElse (aFromRoomPtr - 1 = foundRoom.RoomNumber) OrElse (aFromRoomPtr - 3 = foundRoom.RoomNumber) OrElse (aFromRoomPtr + 3 = foundRoom.RoomNumber) Then
                    aToRoomPtr = foundRoom.RoomNumber
                    currentData = foundRoom.RoomDoors(0)
                    aDataArray = currentData.Split("|")
                    If aDataArray.Length > 1 Then
                      Integer.TryParse(aDataArray(0), aToDoorRowPtr)
                      Integer.TryParse(aDataArray(1), aToDoorColumnPtr)
                    End If

                    Exit For
                  End If
                End If
              Next
              If aToRoomPtr = 0 Then
                'if could not find room next door then take any room
                For Each foundRoom As LevelRoom In Rooms
                  isFound = False
                  For Each foundString As String In aStepList
                    If foundString = foundRoom.RoomNumber.ToString Then
                      isFound = True
                      Exit For
                    End If
                  Next
                  If isFound = False Then
                    'this may be it!
                    aToRoomPtr = foundRoom.RoomNumber
                    currentData = foundRoom.RoomDoors(0)
                    aDataArray = currentData.Split("|")
                    If aDataArray.Length > 1 Then
                      Integer.TryParse(aDataArray(0), aToDoorRowPtr)
                      Integer.TryParse(aDataArray(1), aToDoorColumnPtr)
                    End If

                    Exit For
                  End If
                Next
              End If

              aCreatedStepCount = CreateUtilityTunnel(aFromDoorColumnPtr, aFromDoorRowPtr, aToDoorColumnPtr, aToDoorRowPtr, True)
              If aCreatedStepCount > 0 Then
                'add new tunnel to connectionslist and continue
                aConnectionList.Add(aFromRoomPtr.ToString & "|" & aToRoomPtr.ToString)
                aConnectionList.Add(aToRoomPtr.ToString & "|" & aFromRoomPtr.ToString)
                'DrawScreen()
                'GetKeyBoardInput()

              End If
            End If

          Else
            canContinueFlag = False
            Exit Do
            'all done
          End If

          Debug.Print("ConnectionList")
          For Each currentData In aConnectionList
            Debug.Print(currentData)
          Next
          'DrawScreen()
          'GetKeyBoardInput()
          aCycleCtr = aCycleCtr + 1
          If aCycleCtr > 100 Then
            isFound = isFound
          End If
        Loop




      Catch ex As Exception
        m_ErrorHandler.NotifyError(m_CurrentObject, currentMethod, ex.Message, Now, ex)
      End Try

      Return aReturnValue
    End Function



#End Region

#If DEBUG Then
    'Because the community edition of VS2017 does not call private methods we need a public method to test the private method
    'Just take the same input, call the private method and return the output from the private method

    Public Function CallFindClosestDoor(ByVal whatRoom As LevelRoom, ByVal whatDoor As String) As String
      Dim aReturnValue As String = ""

      LoadDefaultMap() 'test against the default map
      aReturnValue = FindClosestDoor(whatRoom, whatDoor)

      Return aReturnValue
    End Function

    'Public Function CallFindClosestDoor(ByVal whatFromRowPtr As Integer, ByVal whatFromColumnPtr As Integer, ByVal whatDoorRowPtr As Integer, ByVal whatDoorColumnPtr As Integer) As String
    '  Dim aReturnValue As String = ""

    '  LoadDefaultMap() 'test against the default map
    '  aReturnValue = FindClosestDoor(whatFromRowPtr, whatFromColumnPtr, whatDoorRowPtr, whatDoorColumnPtr)

    '  Return aReturnValue
    'End Function

    'Public Function CallIsInSteps(ByVal whatXPtr As Integer, ByVal whatYPtr As Integer, ByRef whatStepsList As List(Of String)) As Boolean
    '  Dim aReturnValue As Boolean = False

    '  aReturnValue = IsInSteps(whatXPtr, whatYPtr, whatStepsList)

    '  Return aReturnValue
    'End Function

    'Public Function CallGetAvailableDirection(ByVal whatSeekYptr As Integer, ByVal whatSeekXPtr As Integer, ByVal whatCurrentDirection As String, ByVal whatDiffX As Integer, ByVal whatDiffY As Integer) As String
    '  Dim aReturnValue As String = ""

    '  LoadDefaultMap() 'test against the default map
    '  aReturnValue = GetAvailableDirection(whatSeekYptr, whatSeekXPtr, whatCurrentDirection, whatDiffX, whatDiffY)

    '  Return aReturnValue
    'End Function

    Public Function CallGetCoordinateString(ByVal whatRow As Integer, ByVal whatColumn As Integer) As String
      Dim aReturnValue As String = ""

      aReturnValue = GetCoordinateString(whatRow, whatColumn)

      Return aReturnValue
    End Function

    Public Function CallGetRoomNumberFromXY(ByVal whatXPtr As Integer, ByVal whatYPtr As Integer) As Integer
      Dim aReturnValue As Integer = 0

      aReturnValue = GetRoomNumberFromXY(whatXPtr, whatYPtr)

      Return aReturnValue
    End Function

    Public Function CallUpdateMap(ByRef whatRoom As LevelRoom) As String
      Dim aReturnValue As String = ""
      Dim aCellType As Integer = 0

      LoadEmptyMap() 'test against the empty map
      aReturnValue = UpdateMap(whatRoom)

      Return aReturnValue
    End Function

    'THIS FUNCTION IS FOR DEBUGGING ONLY
    'LEVEL MAP WILL NORNALLY NOT WAIT FOR KEYBOARD INPUT
    Public Function GetKeyBoardInput() As Char
      Dim aReturnValue As Char
      Dim aConsoleInfo As New ConsoleKeyInfo
      If m_TestMode = False Then
        'TODO this needs work, just returning any character for now
        aConsoleInfo = Console.ReadKey
        aReturnValue = aConsoleInfo.KeyChar
      End If

      Return aReturnValue
    End Function


#End If




  End Class
End Namespace
