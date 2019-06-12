Option Explicit On
Option Strict On
Option Infer On

Imports System.Console
Imports System.Text
Imports ConsoleEx
Imports QB

Module Program

  Sub Main(args As String())
    ' The following is from a sample by Lucian... 
    ' so I'll take it as the gospel. ;-)
    MainAsync(args).GetAwaiter().GetResult()
  End Sub

  Private ReadOnly TUNNEL As Char = QBChar(178)
  Private ReadOnly WALL_TOP_LEFT As Char = QBChar(201)
  Private ReadOnly WALL_HORIZONTAL As Char = QBChar(205)
  'Private ReadOnly WALL_TOP_CENTER As Char = QBChar(203)
  Private ReadOnly WALL_TOP_RIGHT As Char = QBChar(187)
  Private ReadOnly WALL_DOOR As Char = QBChar(206)
  Private ReadOnly WALL_BOTTOM_LEFT As Char = QBChar(200)
  'Private ReadOnly WALL_BOTTOM_CENTER As Char = QBChar(202)
  Private ReadOnly WALL_BOTTOM_RIGHT As Char = QBChar(188)
  Private ReadOnly WALL_VERTICAL As Char = QBChar(186)
  Private ReadOnly FLOOR As Char = QBChar(250)

  Private Enum Objects
    Void = 0

    Floor = 1
    Tunnel = 2
    Door = 3
    Stairs = 10
    Trap = 11

    WallTopLeft = 4
    WallTopRight = 5
    WallHorizontal = 6
    WallVertical = 7
    WallBottomLeft = 8
    WallBottomRight = 9

    Character = 20

    Bat = 35
    Emu = 34
    Hobgoblin = 33
    Imp = 30
    Kestral = 31
    Orc = 36
    RattleSnake = 37
    Snake = 32
    Zombie = 38

    Gold = 50

    Potion = 51
    ClubQuestionMark = 52
    Ring = 53
    Armor = 54
    Scroll = 55
    Weapon = 56
    Food = 57
    Staff = 58
    Amulet = 59
    SafeMagic = 60 ' $
    PerilousMagic = 61 ' +

  End Enum

  Private Async Function MainAsync(args As String()) As Task

#Region "Configure Console Window"

    Title = "ROGUE"

    Dim OrgBufferHeight%, OrgBufferWidth%, OrgWindowHeight%, OrgWindowWidth%

    OrgBufferHeight = BufferHeight
    OrgBufferWidth = BufferWidth
    OrgWindowHeight = WindowHeight
    OrgWindowWidth = WindowWidth

    OutputEncoding = Encoding.UTF8
    Resize(80, 26)
    DisableMinimize()
    DisableMaximize()
    DisableResize()
    DisableQuickEditMode()

    CursorVisible = False

    ' MS-DOS Dark Yellow is more of a Brown.
    ConsoleEx.SetColor(ConsoleColor.DarkYellow, 170, 85, 0)
    ConsoleEx.SetColor(ConsoleColor.DarkMagenta, 85, 85, 255)

    Try

#End Region

      Dim C = Objects.Character
      Dim K = Objects.Kestral
      Dim I = Objects.Imp
      Dim G = Objects.Gold
      Dim P = Objects.Potion
      Dim L = Objects.ClubQuestionMark
      Dim S = Objects.Snake
      Dim H = Objects.Hobgoblin
      Dim R = Objects.Ring
      Dim D = Objects.Stairs
      Dim A = Objects.Armor
      Dim T = Objects.Trap

      Dim room = {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 6, 6, 6, 6, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 1, 1, 1, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 1, 1, D, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 1, 1, 1, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 1, 1, 1, 1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 6, 6, 3, 6, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 4, 6, 6, 6, 6, 3, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 7, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 7, 1, 1, C, K, 1, 1, 1, 1, 1, I, 1, 1, 1, 1, 1, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                  {0, 0, 0, 7, 1, 1, 1, 1, 1, 1, 1, A, 1, 1, 1, 1, 1, 1, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 7, T, 1, 1, 1, 1, P, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 7, 1, 1, 1, L, G, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 8, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}}

      Dim stairCol = -1
      Dim stairRow = -1

      For row = 0 To room.GetUpperBound(0) - 1
        For col = 0 To room.GetUpperBound(1) - 1
          SetCursorPosition(col, row)
          Select Case CType(room(row, col), Objects)

            Case Objects.Trap
              ForegroundColor = ConsoleColor.Magenta
              Write(QBChar(4))
            Case Objects.Stairs
              ForegroundColor = ConsoleColor.Black
              BackgroundColor = ConsoleColor.Green
              Write(" ")
              stairCol = col
              stairRow = row
              ResetColor()
            Case Objects.Character
              ForegroundColor = ConsoleColor.Yellow
              Write(QBChar(1))
            Case Objects.Gold
              ForegroundColor = ConsoleColor.Yellow
              Write(QBChar(15))
            Case Objects.ClubQuestionMark
              ForegroundColor = ConsoleColor.Red
              Write(QBChar(5))
            Case Objects.Armor
              ForegroundColor = ConsoleColor.DarkMagenta
              Write(QBChar(8))
            Case Objects.Potion
              ForegroundColor = ConsoleColor.DarkMagenta
              Write(QBChar(173))
            Case Objects.Scroll
              ForegroundColor = ConsoleColor.DarkMagenta
              Write(QBChar(13))
            Case Objects.Ring
              ForegroundColor = ConsoleColor.DarkMagenta
              Write(QBChar(248)) ' Might be a 9

            Case Objects.Snake
              ForegroundColor = ConsoleColor.DarkGray
              Write("S")
            Case Objects.Hobgoblin
              ForegroundColor = ConsoleColor.DarkGray
              Write("H")
            Case Objects.Kestral
              ForegroundColor = ConsoleColor.DarkGray
              Write("K")
            Case Objects.Imp
              ForegroundColor = ConsoleColor.DarkGray
              Write("I")

            Case Objects.Void
              ForegroundColor = ConsoleColor.Black
              Write(" ")
            Case Objects.Tunnel
              ForegroundColor = ConsoleColor.DarkGray
              Write(TUNNEL)
            Case Objects.Floor
              ForegroundColor = ConsoleColor.DarkGreen
              Write(FLOOR)
            Case Objects.WallTopLeft
              ForegroundColor = ConsoleColor.DarkYellow
              Write(WALL_TOP_LEFT)
            Case Objects.WallTopRight
              ForegroundColor = ConsoleColor.DarkYellow
              Write(WALL_TOP_RIGHT)
            Case Objects.WallHorizontal
              ForegroundColor = ConsoleColor.DarkYellow
              Write(WALL_HORIZONTAL)
            Case Objects.WallVertical
              ForegroundColor = ConsoleColor.DarkYellow
              Write(WALL_VERTICAL)
            Case Objects.WallBottomLeft
              ForegroundColor = ConsoleColor.DarkYellow
              Write(WALL_BOTTOM_LEFT)
            Case Objects.WallBottomRight
              ForegroundColor = ConsoleColor.DarkYellow
              Write(WALL_BOTTOM_RIGHT)
            Case Objects.Door
              ForegroundColor = ConsoleColor.DarkYellow
              Write(WALL_DOOR)
          End Select
        Next
      Next

      Dim alt = True

      Do

        Await Task.Delay(200)

        If stairRow > -1 AndAlso stairCol > -1 Then
          ' Draw stairway...
          ForegroundColor = ConsoleColor.Black
          BackgroundColor = ConsoleColor.Green
          SetCursorPosition(staircol, stairrow)
          If alt Then
            Write(" ")
          Else
            Write(QBChar(240))
          End If
          ResetColor()
          alt = Not alt
        End If

        If KeyAvailable Then
          Dim key = ReadKey(True)
          If key.Key = ConsoleKey.Spacebar Then
            Exit Do
          End If
        End If

      Loop

#Region "Reset Console Window"

    Finally

      EnableMinimize()
      EnableMaximize()
      EnableResize()
      ForegroundColor = ConsoleColor.Gray
      BackgroundColor = ConsoleColor.Black

      SetBufferSize(OrgBufferWidth, OrgBufferHeight)
      SetWindowSize(OrgWindowWidth, OrgWindowHeight)

      EnableQuickEditMode()

      ResetColor()
      'CLS()

      CursorVisible = True

      If Debugger.IsAttached Then
        WriteLine()
        WriteLine("Press enter to continue...")
        ReadLine()
      End If

    End Try

#End Region

  End Function

End Module
