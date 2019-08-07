﻿Option Explicit On
Option Strict On
Option Infer On

Imports System.Console
Imports System.Runtime.InteropServices
Imports System.Text
Imports ConsoleEx
Imports QB

Module Program

  Friend Randomizer As New Random()

  'Sub Main(args As String())
  '  ' The following is from a sample by Lucian... 
  '  ' so I'll take it as the gospel. ;-)
  '  MainAsync(args).GetAwaiter().GetResult()
  'End Sub

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

  Private m_levels As List(Of Core.Level)
  Private m_level As Integer

  Private m_holeX% = -1
  Private m_holeY% = -1
  Private m_holeFound As Boolean = False

  Private m_heroX% = -1
  Private m_heroY% = -1
  Private ReadOnly m_heroLevel% = 0
  Private m_heroHp% = 1 ' 12
  Private ReadOnly m_heroHpMax% = 12
  Private ReadOnly m_heroStr% = 16
  Private ReadOnly m_heroStrMax% = 16
  Private m_heroGold% = 0
  Private ReadOnly m_heroArmor% = 5
  Private m_heroName As String = "Whimp"

  Private m_healCount% = 0
  Private ReadOnly m_healTurns% = 18
  Private ReadOnly m_healAmount% = 1

  Private m_moveCount% = 0
  Private m_hungerCount% = 0
  Private m_hungerStage As HungerStage

  Private m_accumulator$

  Private m_messages As New Queue(Of String)

  Private Enum HungerStage
    Healthy
    Hungry
    Weak
    Faint ' You feel very weak. you faint from lack of food -> You can move again
    Hungry3
    Dead
  End Enum

  Private Class XP
    Public Sub New(level%, points%, dice%, healRate%)
      Me.Level = level
      Me.Points = points
      Me.Dice = dice
      Me.HealRate = healRate
    End Sub
    Public ReadOnly Property Level As Integer
    Public ReadOnly Property Points As Integer
    Public ReadOnly Property Dice As Integer
    Public ReadOnly Property HealRate As Integer
  End Class

  Private ReadOnly XpTable As XP() = {New XP(0, 0, 1, 18),
                               New XP(1, 10, 1, 17),
                               New XP(2, 20, 1, 15),
                               New XP(3, 40, 1, 13),
                               New XP(4, 80, 1, 11),
                               New XP(5, 160, 1, 9),
                               New XP(6, 320, 1, 7),
                               New XP(7, 640, 1, 3),
                               New XP(8, 1280, 2, 3),
                               New XP(9, 2560, 3, 3),
                               New XP(10, 5120, 4, 3),
                               New XP(11, 10240, 5, 3),
                               New XP(12, 20480, 6, 3),
                               New XP(13, 40960, 7, 3),
                               New XP(14, 81920, 8, 3),
                               New XP(15, 163840, 9, 3),
                               New XP(16, 327680, 10, 3),
                               New XP(17, 655360, 11, 3),
                               New XP(18, 1310720, 12, 3),
                               New XP(19, 2621440, 13, 3),
                               New XP(20, 5242880, 14, 3)}

  'Private Enum Display
  '  Level
  '  Inventory
  '  Help
  'End Enum

  'Private Async Function MainAsync(args As String()) As Task
  Public Sub Main() 'args As String())

    'Dim r0 = 0
    'Dim r1 = 0
    'Dim r2 = 0
    'Dim r3 = 0

    'For x = 1 To 1000
    '  Select Case Rogue.Core.Param.Randomizer.Next(0, 3)
    '    Case 0 : r0 += 1
    '    Case 1 : r1 += 1
    '    Case 2 : r2 += 1
    '    Case 3 : r3 += 1
    '    Case Else
    '  End Select
    'Next

    'Console.WriteLine(r0)
    'Console.WriteLine(r1)
    'Console.WriteLine(r2)
    'Console.WriteLine(r3)

    'Console.ReadLine()

#Region "Configure Console Window"

    Title = "ROGUE"

    Dim OrgBufferHeight%, OrgBufferWidth%, OrgWindowHeight%, OrgWindowWidth%

    OrgBufferHeight = BufferHeight
    OrgBufferWidth = BufferWidth
    OrgWindowHeight = WindowHeight
    OrgWindowWidth = WindowWidth

    OutputEncoding = Encoding.UTF8

    If RuntimeInformation.IsOSPlatform(OSPlatform.Windows) Then

      Resize(80, 25)

      DisableMinimize()
      DisableMaximize()
      DisableResize()
      DisableQuickEditMode()

      ' MS-DOS Dark Yellow is more of a Brown.
      ConsoleEx.SetColor(ConsoleColor.DarkYellow, 170, 85, 0)
      'ConsoleEx.SetColor(ConsoleColor.DarkMagenta, 85, 85, 255)
      ConsoleEx.SetColor(ConsoleColor.Blue, 85, 85, 255)

    End If

    CursorVisible = False

    Try

#End Region

      ' Check to see if default dungeon exists...

      If Not IO.File.Exists("default.rogue") Then
        WriteLine("Missing 'default.rogue' file.")
        Return
      End If

      ' Load / parse the dungeon into memory...

      If 1 = 1 Then
        m_levels = LoadDungeon("default.rogue")
      Else
        m_levels = New List(Of Core.Level)
        For index = 1 To 1 '26
          m_levels.Add(New Core.Level(index))
        Next
      End If

      m_heroName = GetCharacterName()

      ' Holds the current location of the "stairs" (aka hole).

      InitializeLevel()

      DisplayMessage($"Hello {m_heroName}.  Are you prepared to die?.")

      ' "Game Loop"

      Dim alt = True

      Dim cycles = 0

      'Dim displaying = Display.Level

      Do

        Threading.Thread.Sleep(1)

        'If m_holeFound AndAlso
        '   cycles > 250 AndAlso
        '   (m_holeY > -1 AndAlso m_holeX > -1) AndAlso
        '   Not (m_heroY = m_holeY AndAlso m_heroX = m_holeX) AndAlso
        '   displaying = Display.Level Then
        If m_holeFound AndAlso
                   cycles > 250 AndAlso
                   (m_holeY > -1 AndAlso m_holeX > -1) AndAlso
                   Not (m_heroY = m_holeY AndAlso m_heroX = m_holeX) Then
          cycles = 0
          ' Draw stairway...
          ForegroundColor = ConsoleColor.Black
          BackgroundColor = ConsoleColor.Green
          SetCursorPosition(m_holeX, m_holeY + 1)
          If alt Then
            Write(" ")
          Else
            Write("≡")
          End If
          ResetColor()
          alt = Not alt
        End If

        If KeyAvailable Then

          ClearMessage()

          Dim ki = ReadKey(True)
          Dim key = ki.Key

          Dim isAction = False

          ' Translate "alternate keys" to "primary keys".

          If ki.Modifiers = ConsoleModifiers.Shift Then
            Select Case key
              Case ConsoleKey.OemPeriod ' >
                key = ConsoleKey.Insert
              Case ConsoleKey.Oem2 ' ?
                key = ConsoleKey.F1
              Case Else
            End Select
          Else
            Select Case key
              Case ConsoleKey.F7 : key = ConsoleKey.I
              Case ConsoleKey.OemPeriod ' .
                isAction = True
              Case ConsoleKey.Oem2 ' /
                key = ConsoleKey.F2
              Case Else
            End Select
          End If

          Select Case key

            Case ConsoleKey.Escape

              m_accumulator = ""
              DrawAccumulator()

            Case ConsoleKey.D0 : Accumulate("0"c)
            Case ConsoleKey.D1 : Accumulate("1"c)
            Case ConsoleKey.D2 : Accumulate("2"c)
            Case ConsoleKey.D3 : Accumulate("3"c)
            Case ConsoleKey.D4 : Accumulate("4"c)
            Case ConsoleKey.D5 : Accumulate("5"c)
            Case ConsoleKey.D6 : Accumulate("6"c)
            Case ConsoleKey.D7 : Accumulate("7"c)
            Case ConsoleKey.D8 : Accumulate("8"c)
            Case ConsoleKey.D9 : Accumulate("9"c)

            Case ConsoleKey.Insert

              If m_heroX = m_holeX AndAlso
                               m_heroY = m_holeY Then
                m_level += 1
                If m_level > m_levels.Count - 1 Then
                  Exit Do
                End If
                InitializeLevel()
              End If

            Case ConsoleKey.I

              DrawInventory()
              DrawLevel()

            Case ConsoleKey.Q
              If ki.Modifiers = ConsoleModifiers.Shift Then
                SetCursorPosition(0, 0)
                ForegroundColor = ConsoleColor.Gray
                BackgroundColor = ConsoleColor.Black
                Write("Do you wish to end your quest now (Yes/No) ?")
                ForegroundColor = ConsoleColor.Black
                BackgroundColor = ConsoleColor.Gray
                SetCursorPosition(35, 0)
                Write("Y")
                SetCursorPosition(39, 0)
                Write("N")
                Dim quit = False
                Do
                  If KeyAvailable Then
                    ki = ReadKey(True)
                    Select Case ki.Key
                      Case ConsoleKey.Y
                        quit = True
                        Exit Do
                      Case ConsoleKey.N
                        SetCursorPosition(0, 0)
                        ForegroundColor = ConsoleColor.Gray
                        BackgroundColor = ConsoleColor.Black
                        Write("                                            ")
                        Exit Do
                      Case Else
                    End Select
                  End If
                Loop
                If quit Then
                  Exit Do
                End If
              End If

            Case ConsoleKey.V

              If Not ki.Modifiers = ConsoleModifiers.Shift Then
                ' Version
                DisplayMessage($"Rogue OPEN SOURCE version")
              End If

            Case ConsoleKey.F1

              DrawHelp()
              DrawLevel()

            Case ConsoleKey.F2

              DrawSymbols()
              DrawLevel()

            Case ConsoleKey.F12
              'TEST:
              m_levels(m_level) = New Core.Level(m_level)
              InitializeLevel()

            Case ConsoleKey.UpArrow,
                             ConsoleKey.DownArrow,
                             ConsoleKey.LeftArrow,
                             ConsoleKey.RightArrow,
                             ConsoleKey.Home,
                             ConsoleKey.End,
                             ConsoleKey.PageUp,
                             ConsoleKey.PageDown,
                             ConsoleKey.S,
                             ConsoleKey.Y, ConsoleKey.U, ConsoleKey.H, ConsoleKey.J, ConsoleKey.K, ConsoleKey.L, ConsoleKey.B, ConsoleKey.N,
                             ConsoleKey.Delete

              isAction = True

            Case Else
              ' Do nothing...
              'Stop
          End Select

          If isAction Then

            Dim repeatCount = 1
            If m_accumulator <> "" Then
              repeatCount = CInt(m_accumulator)
            End If

            For i = repeatCount To 1 Step -1

              'ClearMessage()

              Dim dirX = 0 'm_heroX
              Dim dirY = 0 'm_heroY

              Dim run = False

              Dim actions = 0
              Dim abort = False
              Dim initialMove = True

              Select Case key

                Case ConsoleKey.Home : dirX -= 1 : dirY -= 1
                Case ConsoleKey.Y : dirX -= 1 : dirY -= 1 : run = (ki.Modifiers = ConsoleModifiers.Shift)
                Case ConsoleKey.End : dirX -= 1 : dirY += 1
                Case ConsoleKey.B : dirX -= 1 : dirY += 1 : run = (ki.Modifiers = ConsoleModifiers.Shift)
                Case ConsoleKey.PageUp : dirX += 1 : dirY -= 1
                Case ConsoleKey.U : dirX += 1 : dirY -= 1 : run = (ki.Modifiers = ConsoleModifiers.Shift)
                Case ConsoleKey.PageDown : dirX += 1 : dirY += 1
                Case ConsoleKey.N : dirX += 1 : dirY += 1 : run = (ki.Modifiers = ConsoleModifiers.Shift)

                Case ConsoleKey.UpArrow : dirY -= 1
                Case ConsoleKey.K : dirY -= 1 : run = (ki.Modifiers = ConsoleModifiers.Shift)
                Case ConsoleKey.DownArrow : dirY += 1
                Case ConsoleKey.J : dirY += 1 : run = (ki.Modifiers = ConsoleModifiers.Shift)
                Case ConsoleKey.LeftArrow : dirX -= 1
                Case ConsoleKey.H : dirX -= 1 : run = (ki.Modifiers = ConsoleModifiers.Shift)
                Case ConsoleKey.RightArrow : dirX += 1
                Case ConsoleKey.L : dirX += 1 : run = (ki.Modifiers = ConsoleModifiers.Shift)

                Case ConsoleKey.S, ConsoleKey.Delete
                  actions += 1
                  If PerformSearch() Then
                    abort = True
                  End If

                Case ConsoleKey.OemPeriod
                  actions += 1

                Case Else
              End Select

              If dirX <> 0 OrElse
                               dirY <> 0 Then

                ' If running and find "something", abort...

                Do

                  If CanMove(m_heroX + dirX, m_heroY + dirY) Then
                    DrawRoomTile(m_heroX, m_heroY)
                    m_heroX += dirX
                    m_heroY += dirY
                    Dim encounter = PlaceHero()
                    actions += 1
                    initialMove = False
                    If encounter OrElse Not run Then
                      Exit Do
                    Else
                      Dim tile = m_levels(m_level).Map(m_heroY, m_heroX)
                      If tile.Type = Core.TileType.Door Then
                        Exit Do
                      End If
                    End If
                  Else

                    If Not initialMove Then
                      Dim tile = m_levels(m_level).Map(m_heroY, m_heroX)
                      If tile.Type = Core.TileType.Tunnel Then

                        If dirX = 0 Then
                          If CanMove(m_heroX - 1, m_heroY) Then
                            ' Can move to the left
                            dirX = -1 : dirY = 0
                          ElseIf CanMove(m_heroX + 1, m_heroY) Then
                            ' Can move to the right
                            dirX = 1 : dirY = 0
                          Else
                            abort = True
                            Exit Do
                          End If
                        ElseIf dirY = 0 Then
                          If CanMove(m_heroX, m_heroY - 1) Then
                            ' Can move up
                            dirX = 0 : dirY = -1
                          ElseIf CanMove(m_heroX, m_heroY + 1) Then
                            ' Can move down
                            dirX = 0 : dirY = 1
                          Else
                            abort = True
                            Exit Do
                          End If
                        Else
                          abort = True
                          Exit Do
                        End If
                      Else
                        abort = True
                        Exit Do
                      End If
                    Else
                      abort = True
                      Exit Do
                    End If

                  End If
                Loop

              End If

              If actions > 0 Then

                m_moveCount += actions
                m_hungerCount += actions

                ' Handle regeneration (healing)...
                If m_heroHp < m_heroHpMax Then
                  m_healCount += actions
                  If m_healCount >= m_healTurns Then
                    If m_healAmount = 1 Then
                      m_heroHp += m_healAmount
                    Else
                      m_heroHp += Randomizer.Next(1, m_healAmount + 1)
                    End If
                    If m_heroHp > m_heroHpMax Then
                      m_heroHp = m_heroHpMax
                    End If
                    m_healCount = 0
                  End If
                End If

                ' Handle "hunger" (transition)...
                Dim currentHungerStage = m_hungerStage
                SetHungerStage()
                If currentHungerStage <> m_hungerStage Then
                  DisplayMessage("You are starting to get hungry")
                End If

              End If

              If m_accumulator <> "" Then
                m_accumulator = i.ToString
                DrawAccumulator()
                Threading.Thread.Sleep(10)
              End If

              If abort Then
                Exit For
              End If

            Next

            ClearAccumulator()

          End If

        End If

        cycles += 1

        'If displaying = Display.Level Then
        DrawHud()
        DrawClock()
        'End If

        ' DEBUG numbers...
        SetCursorPosition(0, 24)
        BackgroundColor = ConsoleColor.Black
        ForegroundColor = ConsoleColor.White
        Write($"{m_moveCount}")
        ForegroundColor = ConsoleColor.Gray
        Write(",")
        ForegroundColor = ConsoleColor.Red
        Write($"{m_hungerCount}".PadRight(4))

      Loop

      ResetColor()
      Clear()

      SetCursorPosition(0, 0)
      Write($"You quit with {m_heroGold} gold pieces")

      SetCursorPosition(0, 24)
      Write($"[Press Enter to see rankings]")

      Do
        If KeyAvailable Then
          Dim key = ReadKey(True)
          Select Case key.Key
            Case ConsoleKey.Enter
              Exit Do
            Case Else
          End Select
        End If
      Loop

      Clear()

      ForegroundColor = ConsoleColor.White
      WriteLine("Guildmaster's Hall of Fame:")
      WriteLine()
      ForegroundColor = ConsoleColor.Yellow
      WriteLine("Gold")
      WriteLine()
      ResetColor()
      WriteLine("TODO: Display hall-of-fame list.")
      WriteLine()
      WriteLine()
      WriteLine()
      WriteLine()

#Region "Reset Console Window"

    Finally

      ForegroundColor = ConsoleColor.Gray
      BackgroundColor = ConsoleColor.Black

      If RuntimeInformation.IsOSPlatform(OSPlatform.Windows) Then

        EnableMinimize()
        EnableMaximize()
        EnableResize()

        SetBufferSize(OrgBufferWidth, OrgBufferHeight)
        SetWindowSize(OrgWindowWidth, OrgWindowHeight)

        EnableQuickEditMode()

      End If

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

  End Sub

  Private Sub DrawInventory()
    ResetColor()
    Clear()
    'WriteLine("a) Some food")
    'WriteLine("b) A scroll titled 'cir celxev goszur'")
    'WriteLine("c) +1 ring mail [armor class 5) (being worn)")
    'WriteLine("d) A +1,+1 mace (weapon in hand)")
    'WriteLine("e) A +1,+0 short bow")
    'WriteLine("f) 30 +0,+0 arrows")
    PressSpaceToContinue()
  End Sub

  Private Class KeyCommand

    Public Sub New(key As String, description As String, Optional implemented As Boolean = False)
      Me.Key = key
      Me.Description = description
      Me.Implemented = implemented
    End Sub

    Public ReadOnly Property Key As String
    Public ReadOnly Property Description As String
    Public ReadOnly Property Implemented As Boolean

  End Class

  Private Sub DrawHelp()
    ResetColor()
    Clear()

    Dim commands = New List(Of KeyCommand)

    ' Page 1

    commands.Add(New KeyCommand("F1", "list of commands", True))
    commands.Add(New KeyCommand("F2", "list Of symbols", True))
    commands.Add(New KeyCommand("F3", "repeat command"))
    commands.Add(New KeyCommand("F4", "repeat message"))
    commands.Add(New KeyCommand("F5", "rename something"))
    commands.Add(New KeyCommand("F6", "recall what's been discovered"))
    commands.Add(New KeyCommand("F7", "inventory Of your possessions", True))
    commands.Add(New KeyCommand("F8", "<dir> identify trap type"))
    commands.Add(New KeyCommand("F9", "The Any Key (definable)"))
    commands.Add(New KeyCommand("Alt F9", "defines the Any Key"))
    commands.Add(New KeyCommand("F10", "Supervisor Key (fake dos)"))
    commands.Add(New KeyCommand("Space", "Clear -More- message", True))
    commands.Add(New KeyCommand("◄┘", "the Enter Key"))
    commands.Add(New KeyCommand("←", "left", True))
    commands.Add(New KeyCommand("↓", "down", True))
    commands.Add(New KeyCommand("↑", "up", True))
    commands.Add(New KeyCommand("→", "right", True))
    commands.Add(New KeyCommand("Home", "up & left", True))
    commands.Add(New KeyCommand("PgUp", "up & right", True))
    commands.Add(New KeyCommand("End", "down & left", True))
    commands.Add(New KeyCommand("PgDn", "down & right", True))
    commands.Add(New KeyCommand("Scroll", "Fast Play mode"))
    commands.Add(New KeyCommand(".", "rest"))
    commands.Add(New KeyCommand(">", "go down a staircase", True))
    commands.Add(New KeyCommand("<", "go up the staircase"))
    commands.Add(New KeyCommand("Esc", "cancel command", True))
    commands.Add(New KeyCommand("d", "drop Object"))
    commands.Add(New KeyCommand("e", "eat food"))
    commands.Add(New KeyCommand("f", "<dir> find something"))
    commands.Add(New KeyCommand("q", "quaff potion"))
    commands.Add(New KeyCommand("r", "read paper"))
    commands.Add(New KeyCommand("s", "search for trap/secret door", True))
    commands.Add(New KeyCommand("t", "<dir> Throw something"))
    commands.Add(New KeyCommand("w", "wield a weapon"))
    commands.Add(New KeyCommand("z", "<dir> zap with a wand"))
    commands.Add(New KeyCommand("B", "run down & left", True))
    commands.Add(New KeyCommand("J", "run down", True))
    commands.Add(New KeyCommand("K", "run up", True))
    commands.Add(New KeyCommand("H", "run left", True))
    commands.Add(New KeyCommand("L", "run right", True))
    commands.Add(New KeyCommand("N", "run down & right", True))
    commands.Add(New KeyCommand("U", "run up & right", True))
    commands.Add(New KeyCommand("Y", "run up & left", True))
    commands.Add(New KeyCommand("W", "wear armor"))
    commands.Add(New KeyCommand("T", "take armor off"))
    commands.Add(New KeyCommand("P", "put on ring"))

    ' Page 2

    commands.Add(New KeyCommand("Q", "quit", True))
    commands.Add(New KeyCommand("R", "remove ring"))
    commands.Add(New KeyCommand("S", "save game"))
    commands.Add(New KeyCommand("^", "identify trap"))
    commands.Add(New KeyCommand("?", "help", True))
    commands.Add(New KeyCommand("/", "key", True))
    commands.Add(New KeyCommand("+", "throw"))
    commands.Add(New KeyCommand("-", "zap"))
    commands.Add(New KeyCommand("Ctrl t", "terse message format"))
    commands.Add(New KeyCommand("Ctrl r", "repeat message"))
    commands.Add(New KeyCommand("Del", "search for something hidden"))
    commands.Add(New KeyCommand("Ins", "<dir> find something"))
    commands.Add(New KeyCommand("a", "repeat command"))
    commands.Add(New KeyCommand("c", "rename something"))
    commands.Add(New KeyCommand("i", "inventory"))
    commands.Add(New KeyCommand("v", "version number", True))
    commands.Add(New KeyCommand("!", "Supervisor Key (fake dos)"))
    commands.Add(New KeyCommand("D", "list what has been discovered"))

    Dim page = 0
    Dim index = 0

    Do

      Console.Clear()

      Dim row = 0
      Dim column = 0

      Do Until index > 45

        Console.SetCursorPosition(column * 40, row)

        If commands((page * 46) + index).Implemented Then
          Console.ForegroundColor = ConsoleColor.White
        Else
          Console.ForegroundColor = ConsoleColor.DarkGray
        End If
        Console.Write(commands((page * 46) + index).Key.PadRight(8))
        Console.Write(commands((page * 46) + index).Description)

        If column = 1 Then
          row += 1
        End If

        column = If(column = 0, 1, 0)

        index += 1

        If (page * 46) + index > commands.Count - 1 Then
          Exit Do
        End If

      Loop

      page += 1
      index = 0

      If (page * 46) + index < commands.Count Then
        ' Still more commands left, prompt as if there is another page to be viewed...
        Dim result = PressSpaceForMoreEscToContinue()
        If result Then
          Return
        End If
      Else
        ' This the last of this content, so prompt to "return to game".
        PressSpaceToContinue()
        Return
      End If

    Loop

  End Sub

  Private Class Symbol

    Public Sub New(symbol As String, description As String, color As ConsoleColor)
      Me.Symbol = symbol
      Me.Description = description
      Me.Color = color
    End Sub

    Public ReadOnly Property Symbol As String
    Public ReadOnly Property Description As String
    Public ReadOnly Property Color As ConsoleColor

  End Class

  Private Sub DrawSymbols()

    ResetColor()
    Clear()

    Dim symbols = New List(Of Symbol)

    ' Page 1

    symbols.Add(New Symbol(".", "the floor", ConsoleColor.DarkGreen))
    symbols.Add(New Symbol("☺", "the hero", ConsoleColor.Yellow))
    symbols.Add(New Symbol("♣", "some food", ConsoleColor.Red))
    symbols.Add(New Symbol("♀", "the amulet of yendor", ConsoleColor.Blue))
    symbols.Add(New Symbol("♪", "a scroll", ConsoleColor.Blue))
    symbols.Add(New Symbol("↑", "a weapon", ConsoleColor.Blue))
    symbols.Add(New Symbol("◘", "a piece of armor", ConsoleColor.Blue))
    symbols.Add(New Symbol("✶", "some gold", ConsoleColor.Yellow))
    symbols.Add(New Symbol("¥", "a magic staff", ConsoleColor.Blue))
    symbols.Add(New Symbol("¡", "a potion", ConsoleColor.Blue))
    symbols.Add(New Symbol("○", "a magic ring", ConsoleColor.Blue))
    symbols.Add(New Symbol("▓", "a passage", ConsoleColor.Gray))
    symbols.Add(New Symbol("╬", "a door", ConsoleColor.DarkYellow))
    symbols.Add(New Symbol("╔", "an upper left corner", ConsoleColor.DarkYellow))
    symbols.Add(New Symbol("♦", "a trap", ConsoleColor.Magenta))
    symbols.Add(New Symbol("═", "a horizontal wall", ConsoleColor.DarkYellow))
    symbols.Add(New Symbol("╝", "a lower right corner", ConsoleColor.DarkYellow))
    symbols.Add(New Symbol("╚", "a lower left corner", ConsoleColor.DarkYellow))
    symbols.Add(New Symbol("║", "a vertical wall", ConsoleColor.DarkYellow))
    symbols.Add(New Symbol("╗", "an upper right corner", ConsoleColor.DarkYellow))
    symbols.Add(New Symbol("≡", "a stair case", ConsoleColor.Green))
    symbols.Add(New Symbol("$,+", "safe and perilous magic", ConsoleColor.Gray))
    symbols.Add(New Symbol("A-Z", "26 different monsters", ConsoleColor.Gray))

    Dim page = 0
    Dim index = 0

    Do

      Console.Clear()

      Dim row = 0
      Dim column = 0

      Do Until index > 45

        Console.SetCursorPosition(column * 40, row)

        If symbols((page * 46) + index).Color = ConsoleColor.Green Then
          Console.ForegroundColor = ConsoleColor.Black
          Console.BackgroundColor = ConsoleColor.Green
        Else
          Console.ForegroundColor = symbols((page * 46) + index).Color
        End If
        Console.Write(symbols((page * 46) + index).Symbol)
        Console.ResetColor()
        Console.Write($": {symbols((page * 46) + index).Description}")

        If column = 1 Then
          row += 1
        End If

        column = If(column = 0, 1, 0)

        index += 1

        If (page * 46) + index > symbols.Count - 1 Then
          Exit Do
        End If

      Loop

      page += 1
      index = 0

      If (page * 46) + index < symbols.Count Then
        ' Still more commands left, prompt as if there is another page to be viewed...
        Dim result = PressSpaceForMoreEscToContinue()
        If result Then
          Return
        End If
      Else
        ' This the last of this content, so prompt to "return to game".
        PressSpaceToContinue()
        Return
      End If

    Loop

  End Sub

  Private m_displayingMessage As Boolean
  Private m_messageLength As Integer = 0

  Private Sub DisplayMessage(message$)
    If m_displayingMessage Then
      m_messages.Enqueue(message)
      SetCursorPosition(m_messageLength, 0)
      ForegroundColor = ConsoleColor.Black
      BackgroundColor = ConsoleColor.Gray
      Write(" More ")
      ResetColor()
    Else
      ResetColor()
      SetCursorPosition(0, 0)
      'Write("".PadRight(80)) ' Clear...
      'SetCursorPosition(0, 0)
      Write(message)
      m_messageLength = message.Length
      m_displayingMessage = True
      If m_messages.Count > 0 Then
        ForegroundColor = ConsoleColor.Black
        BackgroundColor = ConsoleColor.Gray
        Write(" More ")
        ResetColor()
      End If
    End If
  End Sub

  Private Sub ClearMessage()
    ResetColor()
    SetCursorPosition(0, 0)
    Write(New String(" "c, 80))
    m_displayingMessage = False
    If m_messages.Count > 0 Then
      Dim message = m_messages.Dequeue()
      DisplayMessage(message)
    End If
  End Sub

  Private Sub Accumulate(c As Char)
    If m_accumulator Is Nothing Then
      m_accumulator = c
    ElseIf m_accumulator?.Length < 4 Then
      m_accumulator &= c
    End If
    DrawAccumulator()
  End Sub

  Private Sub DrawAccumulator()
    BackgroundColor = ConsoleColor.Black
    ForegroundColor = ConsoleColor.Gray
    SetCursorPosition(76, 23)
    Write(If(m_accumulator, "").PadRight(4))
  End Sub

  Private Sub ClearAccumulator()
    m_accumulator = Nothing
    DrawAccumulator()
  End Sub

  Private Function PerformSearch() As Boolean

    Dim result = False

    For yy = -1 To 1
      For xx = -1 To 1
        Dim tile = m_levels(m_level).Map(m_heroY + yy, m_heroX + xx)
        If tile.FoundSecret(False) Then
          DrawRoomTile(m_heroX + xx, m_heroY + yy)
          result = True
        End If
      Next
    Next

    Return result

  End Function

  Private Function LoadDungeon(path As String) As List(Of Core.Level)

    Dim result = New List(Of Core.Level)

    Dim lines = IO.File.ReadAllLines(path)

    Dim lineIndex = 0
    Dim level = -1
    Do

      If lineIndex > lines.Count - 1 Then
        Exit Do
      End If

      If lines(lineIndex).ToLower.StartsWith("level:") Then
        level = CInt(lines(lineIndex).Substring(6).Trim)
        result.Add(New Core.Level())
        lineIndex += 1
      ElseIf lines(lineIndex).ToLower.StartsWith("name:") Then
        result(level - 1).Name = lines(lineIndex).Substring(6).Trim
        lineIndex += 1
      ElseIf lines(lineIndex).ToLower.StartsWith("lights:") Then
        Dim lights = lines(lineIndex).Substring(7).Trim.Split(",")
        For i = 0 To 8
          result(level - 1).Lights(i) = If(lights(i).Trim = "1", True, False)
        Next
        lineIndex += 1
      ElseIf lines(lineIndex).ToLower.StartsWith("map:") Then
        lineIndex += 1
        For i = 0 To 20
          For c = 0 To 79
            result(level - 1).Map(i, c) = New Core.Tile(lines(lineIndex)(c))
          Next
          lineIndex += 1
        Next
      End If

    Loop

    Return result

  End Function

  Private Sub DrawClock()
    ForegroundColor = ConsoleColor.Black
    BackgroundColor = ConsoleColor.Gray
    SetCursorPosition(74, 24)
    Write($"{DateTime.Now:h:mm}".PadLeft(5))
  End Sub

  Private Sub DrawHud()

    ResetColor()

    ForegroundColor = ConsoleColor.Yellow

    SetCursorPosition(0, 23)

    Dim level = $"{m_level + 1}".PadRight(5)
    Dim hits = $"{m_heroHp}({m_heroHpMax})".PadRight(8)
    Dim str = $"{m_heroStr}({m_heroStrMax})".PadRight(8)
    Dim gold = $"{m_heroGold}".PadRight(6)
    Dim armor = $"{m_heroArmor}".PadRight(7)

    Write($"Level:{level} Hits:{hits} Str:{str} Gold:{gold} Armor:{armor}{HeroLevel(m_heroLevel).PadRight(12)}")

    ' Handle "hunger"

    ResetColor()
    SetCursorPosition(57, 24)
    Write("".PadRight(8))

    If m_hungerStage > HungerStage.Healthy Then
      SetCursorPosition(57, 24)
      Dim hunger$ = m_hungerStage.ToString
      ForegroundColor = ConsoleColor.Black
      BackgroundColor = ConsoleColor.Gray
      Write(hunger)
      ResetColor()
    End If

  End Sub

  Private Sub SetHungerStage()

    Select Case m_hungerCount
      Case <= 1000 : m_hungerStage = HungerStage.Healthy
      Case <= 2000 : m_hungerStage = HungerStage.Hungry
      Case <= 3000 : m_hungerStage = HungerStage.Weak
      Case <= 4000 : m_hungerStage = HungerStage.Faint
      Case <= 5000 : m_hungerStage = HungerStage.Hungry3
      Case Else
        m_hungerStage = HungerStage.Dead
    End Select

  End Sub

  Private Function GetCharacterName() As String

    Clear()

    ForegroundColor = ConsoleColor.DarkYellow

    Write($"╔{New System.String("═"c, 78)}╗")
    For r = 1 To 21
      SetCursorPosition(0, r)
      Write($"║{New String(" "c, 78)}║")
    Next
    SetCursorPosition(0, 22)
    Write($"╠{New System.String("═"c, 78)}╣")
    SetCursorPosition(0, 23)
    Write($"║{New String(" "c, 78)}║")
    SetCursorPosition(0, 24)
    'Write($"╚{New System.String("═"c, 78)}╝")
    Write($"╚{New System.String("═"c, 78)}")

    BackgroundColor = ConsoleColor.Gray
    Center("ROGUE: The Adventure Game", 2, ConsoleColor.Black)

    ResetColor()

    Center("The game of Rogue was originated by:", 4, ConsoleColor.Magenta)
    Center("Michael C. Toy", 6, ConsoleColor.White)
    Center("and", 7, ConsoleColor.Gray)
    Center("Kenneth C.R.C. Arnold", 8, ConsoleColor.White)

    Center("Adapted for the .NET Core by:", 10, ConsoleColor.Magenta)
    Center("Cory Smith", 12, ConsoleColor.White)
    Center("Significant design contributions by:", 14, ConsoleColor.Magenta)
    Center("Glenn Wichman and scores of others", 16, ConsoleColor.White)

    Center("(C) Copyright 2019", 18, ConsoleColor.Yellow)
    Center("Cory Smith", 19, ConsoleColor.White)
    Center("Made available as open source under the MIT license.", 20, ConsoleColor.Yellow)

    ResetColor()

    SetCursorPosition(2, 23)
    Write("Rogue's Name?")

    CursorVisible = True

    Try

      'TODO: If player presses ESC (regardless of what was typed) while entering the name...
      '      If player presses ENTER with a blank name...
      '      The name is set to Whimp.

      Dim accumulator = ""

      SetCursorPosition(16, 23)

      Do

        If KeyAvailable Then
          Dim k = ReadKey(True)
          Select Case k.Key
            Case ConsoleKey.Escape
              Return "Whimp"
            Case ConsoleKey.Enter
              If accumulator = "" Then
                Return "Whimp"
              Else
                Return accumulator
              End If
            Case ConsoleKey.Backspace
              If accumulator <> "" Then
                'SetCursorPosition(CursorTop, CursorLeft - 1)
                CursorLeft -= 1
                Write(" "c)
                CursorLeft -= 1
                accumulator = accumulator.Substring(0, accumulator.Length - 1)
              End If
            Case ConsoleKey.Spacebar, ConsoleKey.A To ConsoleKey.Z, ConsoleKey.D0 To ConsoleKey.D9
              Write(k.KeyChar)
              accumulator += k.KeyChar
            Case Else
          End Select
        End If

      Loop

    Finally
      CursorVisible = False
    End Try

  End Function

  Private Sub Center(text As String, row As Integer, fg As ConsoleColor)
    ForegroundColor = fg
    Dim c = (80 - text.Length) \ 2
    SetCursorPosition(c, row)
    Write(text)
  End Sub

  Private Sub InitializeLevel()

    m_accumulator = Nothing
    m_moveCount = 0

    m_holeX = -1
    m_holeY = -1
    m_holeFound = False

    m_heroX = -1
    m_heroY = -1

    BackgroundColor = ConsoleColor.Black
    Clear()
    WriteLine()

    Dim floors As New List(Of Core.Tile)

    For y = 0 To 20
      For x = 0 To 79

        Dim tile = m_levels(m_level).Map(y, x)

        If tile.HeroStart OrElse tile.Type = Core.TileType.Floor Then
          floors.Add(tile)
        End If

        If tile.HeroStart Then
          m_heroX = x : m_heroY = y
        ElseIf tile.Type = Core.TileType.Hole Then
          m_holeX = x : m_holeY = y
        End If

      Next
    Next

    Dim goldCount = Randomizer.Next(2, 5)

    For gold = 1 To goldCount
      If floors.Count > 0 Then
        Dim tileNumber = Randomizer.Next(0, floors.Count - 1)
        floors(tileNumber).Gold = Randomizer.Next(1, 100)
        floors.RemoveAt(tileNumber)
      End If
    Next

    DrawLevel()
  End Sub

  Private Sub DrawLevel()

    ResetColor()
    Clear()

    For x = 0 To 79
      For y = 0 To 20
        Dim explored = m_levels(m_level).Map(y, x).Explored
        If explored Then
          DrawRoomTile(x, y)
        End If
      Next
    Next

    'SetCursorPosition(0, 7)
    'Write(New String("-"c, 80))

    'SetCursorPosition(0, 15)
    'Write(New String("-"c, 80))

    'For y = 0 To 20
    '  SetCursorPosition(26, y + 1) : Write("|")
    '  SetCursorPosition(53, y + 1) : Write("|")
    'Next

    Dim zone = DetermineZone(m_heroX, m_heroY)
    Dim lit = If(zone > -1, m_levels(m_level).Lights(zone), False)
    If lit Then
      DrawRoom(m_heroX, m_heroY)
    End If

    PlaceHero()

  End Sub

  Private Function CanMove(x%, y%) As Boolean
    If x < 0 OrElse y < 0 OrElse x > 79 OrElse y > 20 Then Return False
    Return m_levels(m_level).Map(y, x).PassThrough
  End Function

  Private Function DetermineZone(x%, y%) As Integer
    '     |    |
    '   0 | 1  | 2
    '     |    |
    '  ------------
    '     |    |
    '   3 | 4  | 5
    '     |    |
    '  ------------
    '     |    |
    '   6 | 7  | 8
    '     |    |
    '
    '  26 columns per horizontal zone with 2 columns of separation.
    '  Row 1: 6 lines
    '  Row 2: 7 lines
    '  Row 3: 6 lines

    Dim row = -1
    Select Case y
      Case 0 To 5 : row = 0
      Case 7 To 13 : row = 1
      Case 15 To 20 : row = 2
      Case Else
    End Select

    If row > -1 Then
      Select Case x
        Case 0 To 25 ' 0, 1, 2
          Return (row * 3)
        Case 27 To 53 ' 3, 4, 5
          Return (row * 3) + 1
        Case 55 To 80 ' 6, 7, 8
          Return (row * 3) + 2
        Case Else
      End Select
    End If

    Return -1

  End Function

  Private Class Coord
    Public Sub New(x As Integer, y As Integer)
      Me.X = x
      Me.Y = y
    End Sub
    Public ReadOnly Property X As Integer
    Public ReadOnly Property Y As Integer
  End Class

  Private Function PlaceHero() As Boolean

    Dim result = False

    Dim zone = DetermineZone(m_heroX, m_heroY)
    Dim lit = If(zone > -1, m_levels(m_level).Lights(zone), False)
    Dim floor = If(zone > -1, m_levels(m_level).Map(m_heroY, m_heroX).Type = Core.TileType.Floor, False)

    'If lit AndAlso floor Then

    '  Dim xx = m_heroX
    '  Dim yy = m_heroY
    '  Do
    '    If m_levels(m_level).Map(yy, xx - 1).Type = TileType.Floor Then
    '      xx -= 1
    '    Else
    '      Exit Do
    '    End If
    '  Loop
    '  Do
    '    If m_levels(m_level).Map(yy - 1, xx).Type = TileType.Floor Then
    '      yy -= 1
    '    Else
    '      Exit Do
    '    End If
    '  Loop

    'Else

    If m_heroY > -1 AndAlso m_heroX > -1 Then

      Dim tile = m_levels(m_level).Map(m_heroY, m_heroX)

      Select Case tile.Type
        Case Core.TileType.Floor
          For yy = -1 To 1
            For xx = -1 To 1
              Dim foundStairs = DrawRoomTile(m_heroX + xx, m_heroY + yy)
              If foundStairs Then
                m_holeFound = True
              End If
            Next
          Next
        Case Core.TileType.Tunnel
          For yy = -1 To 1
            If m_heroY + yy > 20 Then Continue For
            If m_heroY + yy < 0 Then Continue For
            For xx = -1 To 1
              If m_heroX + xx > 79 Then Continue For
              If m_heroX + xx < 0 Then Continue For
              Dim target = m_levels(m_level).Map(m_heroY + yy, m_heroX + xx)
              Select Case target.Type
                Case Core.TileType.Tunnel, Core.TileType.Door
                  DrawRoomTile(m_heroX + xx, m_heroY + yy)
                Case Else
              End Select
            Next
          Next
        Case Core.TileType.Door, Core.TileType.SecretHorizontal, Core.TileType.SecretVertical
          If lit Then
            ' In the doorway, determine the floor to the left, right, top or bottom.
            ' Once found, use "paint" routine above...
            Dim coords = {New Coord(0, -1),
                                    New Coord(1, 0),
                                    New Coord(0, 1),
                                    New Coord(-1, 0)}
            For Each coord In coords
              If Not m_heroY + coord.Y > 20 Then
                Dim target = m_levels(m_level).Map(m_heroY + coord.Y, m_heroX + coord.X)
                If target.Type = Core.TileType.Floor Then
                  ' found it...
                  DrawRoom(m_heroX + coord.X, m_heroY + coord.Y)
                  m_levels(m_level).Lights(zone) = False
                  Exit For
                End If
              End If
            Next
          Else
            For yy = -1 To 1
              For xx = -1 To 1
                Dim foundStairs = DrawRoomTile(m_heroX + xx, m_heroY + yy)
                If foundStairs Then
                  m_holeFound = True
                End If
              Next
            Next
          End If
        Case Else
      End Select

      'End If

      If m_levels(m_level).Map(m_heroY, m_heroX).Gold > 0 Then
        DisplayMessage($"You found {m_levels(m_level).Map(m_heroY, m_heroX).Gold} gold pieces")
        m_heroGold += m_levels(m_level).Map(m_heroY, m_heroX).Gold
        m_levels(m_level).Map(m_heroY, m_heroX).Gold = 0
        result = True
      End If

      If m_levels(m_level).Map(m_heroY, m_heroX).Type = Core.TileType.Hole Then
        result = True
        BackgroundColor = ConsoleColor.Green
      Else
        BackgroundColor = ConsoleColor.Black
      End If
      ForegroundColor = ConsoleColor.Yellow
      SetCursorPosition(m_heroX, m_heroY + 1)
      Write("☺")

    End If

    Return result

  End Function

  Private Sub DrawRoom(x As Integer, y As Integer)

    ' Find the top / left floor tile in this room.

    Dim xx = x
    Dim yy = y
    Do
      If m_levels(m_level).Map(yy, xx - 1).Type = Core.TileType.Floor Then
        xx -= 1
      Else
        Exit Do
      End If
    Loop
    Do
      If m_levels(m_level).Map(yy - 1, xx).Type = Core.TileType.Floor Then
        yy -= 1
      Else
        Exit Do
      End If
    Loop

    Dim xxx = xx - 1
    Dim yyy = yy - 1

    Do

      Do
        Dim foundStairs = DrawRoomTile(xxx, yyy)
        If foundStairs Then
          m_holeFound = True
        End If
        xxx += 1
        If xxx > 79 Then Exit Do
        Select Case m_levels(m_level).Map(yyy, xxx).Type
          Case Core.TileType.Void, Core.TileType.Tunnel
            Exit Do
          Case Else
        End Select
      Loop

      xxx = xx - 1
      yyy += 1
      If yyy > 20 Then Exit Do
      Select Case m_levels(m_level).Map(yyy, xxx).Type
        Case Core.TileType.Void, Core.TileType.Tunnel
          Exit Do
        Case Else
      End Select

    Loop

  End Sub

  Private Function DrawRoomTile(x%, y%) As Boolean
    If y > 20 Then Return False
    Dim result = False
    Dim tile = m_levels(m_level).Map(y, x)
    tile.Explored = True
    Dim output = tile.ToString
    Dim fg = ConsoleColor.DarkGray
    Dim bg = ConsoleColor.Black
    Select Case tile.Type
      Case Core.TileType.WallTopLeft : fg = ConsoleColor.DarkYellow
      Case Core.TileType.WallTopRight : fg = ConsoleColor.DarkYellow
      Case Core.TileType.WallHorizontal : fg = ConsoleColor.DarkYellow
      Case Core.TileType.WallVertical : fg = ConsoleColor.DarkYellow
      Case Core.TileType.WallBottomLeft : fg = ConsoleColor.DarkYellow
      Case Core.TileType.WallBottomRight : fg = ConsoleColor.DarkYellow
      Case Core.TileType.SecretHorizontal : fg = ConsoleColor.DarkYellow
      Case Core.TileType.SecretVertical : fg = ConsoleColor.DarkYellow
      Case Core.TileType.Door : fg = ConsoleColor.DarkYellow
      Case Core.TileType.Hole
        fg = ConsoleColor.Black : bg = ConsoleColor.Green
        result = True
      Case Core.TileType.Floor
        If tile.Gold > 0 Then
          fg = ConsoleColor.Yellow
          output = "*"
        Else
          fg = ConsoleColor.DarkGreen
        End If
      Case Core.TileType.Tunnel
        'Case "@"c : output = QBChar(1) : fg = ConsoleColor.Yellow
        'Case "!"c : output = QBChar(173) : fg = ConsoleColor.DarkMagenta
        'Case "%"c : output = QBChar(4) : fg = ConsoleColor.Magenta
        'Case "$"c : output = QBChar(15) : fg = ConsoleColor.Yellow
        'Case "^"c : output = QBChar(24) : fg = ConsoleColor.DarkMagenta
        'Case "\"c : output = QBChar(8) : fg = ConsoleColor.DarkMagenta
        'Case "*"c : output = QBChar(5) : fg = ConsoleColor.Red
        'Case "#"c : output = TUNNEL
        'Case "@"c, "."c : output = "."c : fg = ConsoleColor.DarkGreen
        'Case "0"c : output = QBChar(248) : fg = ConsoleColor.DarkMagenta
        'Case "9"c : output = QBChar(13) : fg = ConsoleColor.DarkMagenta
      Case Else
    End Select
    If BackgroundColor <> bg Then
      BackgroundColor = bg
    End If
    If ForegroundColor <> fg Then
      ForegroundColor = fg
    End If
    SetCursorPosition(x, y + 1) : Write(output)
    Return result
  End Function

  Private Function HeroLevel(level As Integer) As String
    Select Case level
      Case 1 : Return "Guild Novice"
      Case 2 : Return "Apprentice"
      Case 3 : Return "Journeyman"
      Case 4 : Return "Adventurer"
      Case 5 : Return "Fighter"
      Case 6 : Return "Warrior"
      Case 7 : Return "Rogue"
      Case 8 : Return "Champion"
      Case 9 : Return "Master Rogue"
      Case 10 : Return "Warlord"
      Case 11 : Return "Hero"
      Case 12 : Return "Guild Master"
      Case 13 : Return "Dragonlord"
      Case 14 : Return "Wizard"
      Case 15 : Return "Rogue Geek"
      Case 16 : Return "Rogue Addict"
      Case 17 : Return "Schmendrick"
      Case 18 : Return "Gunfighter"
      Case 19 : Return "Time Waster"
      Case 20 : Return "Bug Chaser"
      Case Else
        Return ""
    End Select
  End Function

  Private Sub PressSpaceToContinue()
    SetCursorPosition(0, 24)
    Write("--press space to continue--")
    Do
      If KeyAvailable Then
        Dim key = ReadKey(True)
        Select Case key.Key
          Case ConsoleKey.A To ConsoleKey.Z, ConsoleKey.Spacebar
            Exit Do
          Case Else
        End Select
      End If
    Loop
  End Sub

  Private Function PressSpaceForMoreEscToContinue() As Boolean
    SetCursorPosition(0, 24)
    Write("--Press space for more, Esc to continue--")
    Do
      If KeyAvailable Then
        Dim key = ReadKey(True)
        Select Case key.Key
          Case ConsoleKey.Spacebar
            Return False
          Case ConsoleKey.Escape
            Return True
          Case Else
        End Select
      End If
    Loop
  End Function

End Module
