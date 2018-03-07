Imports System
Imports System.Text

''' <summary>
''' Encode all aspects of the map for a specific level
''' </summary>
Public Class LevelMap


#Region "Public Properties"

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


#End Region

#Region "Public Methods"

  Public Sub New()
    Initialize(False)
  End Sub

  ''' <summary>
  ''' Draw the information contained in the current map level on the screen.
  ''' The current map is stored in MapCellData
  ''' MapCellVisibility(r,c) is used to control whether the cell is visible or not
  ''' </summary>
  Public Sub DrawScreen()
    Dim m_consoleController As New ConsoleController

    Try

      For rowPointer As Integer = 0 To EnumsAndConsts.MapHeight - 1
        For columnPointer As Integer = 0 To EnumsAndConsts.MapWidth - 1
          If True Then 'always visible for prototype but for production need to check MapCellVisibility(columnPointer, rowPointer) = True
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
              Case 301 'TODO for monsters should probably defind their visual character as a property on the monster object and display that
                m_consoleController.WriteAt(columnPointer, rowPointer, ConsoleColor.White, ConsoleColor.Black, "K")
              Case 302 'TODO for monsters should probably defind their visual character as a property on the monster object and display that
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

    Try

      For rowPointer As Integer = 0 To EnumsAndConsts.MapHeight
        For columnPointer As Integer = 0 To EnumsAndConsts.MapWidth
          MapCellData(rowPointer, columnPointer) = 0
          MapCellVisibility(rowPointer, columnPointer) = False
        Next
      Next

      If whatGenerateRandomMapFlag = True Then
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

        For rowPtr As Integer = 0 To 22
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
          Next
        Next
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

#End Region

#Region "Private Methods"

#End Region

End Class
