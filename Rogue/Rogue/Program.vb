Option Explicit On
Option Strict On
Option Infer On

Imports System.Console
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

  Class Level

    Public Sub New()
      ReDim Me.Map(20, 79)
      ReDim Me.Lights(8)
    End Sub

    Public Property Name As String
    Public Property Lights As Boolean()
    Public Property Map As Tile(,)

  End Class

  Private m_levels As List(Of Level)
  Private m_level As Integer

  Private m_holeX% = -1
  Private m_holeY% = -1
  Private m_holeFound As Boolean = False

  Private m_heroX% = -1
  Private m_heroY% = -1
  Private m_heroLevel% = 0
  Private m_heroHp% = 1 ' 12
  Private m_heroHpMax% = 12
  Private m_heroStr% = 16
  Private m_heroStrMax% = 16
  Private m_heroGold% = 0
  Private m_heroArmor% = 5
  Private m_heroName As String = "Whimp"

  Private m_healCount% = 0
  Private m_healTurns% = 18
  Private m_healAmount% = 1

  Private m_moveCount% = 0

  Private m_accumulator$

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

  Private XpTable As XP() = {New XP(0, 0, 1, 18),
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

  Private Enum Display
    Level
    Inventory
    Help
  End Enum

  'Private Async Function MainAsync(args As String()) As Task
  Public Sub Main(args As String())

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

      ' Check to see if default dungeon exists...

      If Not IO.File.Exists("default.rogue") Then
        WriteLine("Missing 'default.rogue' file.")
        Return
      End If

      ' Load / parse the dungeon into memory...

      m_levels = LoadDungeon("default.rogue")

      m_heroName = GetCharacterName()

      ' Holds the current location of the "stairs" (aka hole).

      InitializeLevel()

      DisplayMessage($"Hello {m_heroName}.  Are you prepared to die?.")

      ' "Game Loop"

      Dim alt = True

      Dim cycles = 0

      Dim displaying = Display.Level

      Do

        Threading.Thread.Sleep(1)

        If m_holeFound AndAlso
           cycles > 250 AndAlso
           (m_holeY > -1 AndAlso m_holeX > -1) AndAlso
           Not (m_heroY = m_holeY AndAlso m_heroX = m_holeX) AndAlso
           displaying = Display.Level Then
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

          Dim ki = ReadKey(True)
          Dim key = ki.Key

          Dim isAction = False

          ' Translate "alternate keys" to "primary keys".

          If ki.Modifiers = ConsoleModifiers.Shift Then
            Select Case key
              Case ConsoleKey.OemPeriod : key = ConsoleKey.Insert
              Case ConsoleKey.Oem2 ' /
                key = ConsoleKey.F1
              Case Else
            End Select
          Else
            Select Case key
              Case ConsoleKey.F7 : key = ConsoleKey.I
              Case Else
            End Select
          End If

          Select Case key
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
              displaying = Display.Level

              'If displaying = Display.Inventory Then
              '  DrawLevel()
              '  displaying = Display.Level
              'Else
              '  DrawInventory()
              '  displaying = Display.Inventory
              'End If

            Case ConsoleKey.F1

              If key = ConsoleKey.F1 OrElse
                 (key = ConsoleKey.Oem2 AndAlso ki.Modifiers = ConsoleModifiers.Shift) Then
                DrawHelp()
                DrawLevel()
                displaying = Display.Level
              End If

              'If displaying = Display.Help Then
              '  DrawLevel()
              '  displaying = Display.Level
              'Else
              '  ResetColor()
              '  Clear()
              '  Write("Help")
              '  displaying = Display.Help
              'End If

            Case ConsoleKey.Q
              'TODO: Need to determine if uppercase Q.
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
              Stop
          End Select

          If isAction Then

              Dim repeatCount = 1
              If m_accumulator <> "" Then
                repeatCount = CInt(m_accumulator)
              End If

              For i = repeatCount To 1 Step -1

                ClearMessage()

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
                        If tile.Type = TileType.Door Then
                          Exit Do
                        End If
                      End If
                    Else

                      If Not initialMove Then
                        Dim tile = m_levels(m_level).Map(m_heroY, m_heroX)
                        If tile.Type = TileType.Tunnel Then

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

                  ' Handle regeneration (healing)...
                  If m_heroHp < m_heroHpMax Then
                    m_healCount += actions
                    If m_healCount >= m_healTurns Then
                      If m_healAmount = 1 Then
                        m_heroHp += m_healAmount
                      Else
                        m_heroHp += Randomizer.Next(1, m_healAmount)
                      End If
                      If m_heroHp > m_heroHpMax Then
                        m_heroHp = m_heroHpMax
                      End If
                      m_healCount = 0
                    End If
                  End If

                End If

                If m_accumulator <> "" Then
                  m_accumulator = i.ToString
                  DrawAccumulator()
                End If

                If abort Then
                  Exit For
                End If

              Next

              ClearAccumulator()

            End If

          End If

          cycles += 1

        SetCursorPosition(0, 25)
        BackgroundColor = ConsoleColor.Black
        ForegroundColor = ConsoleColor.Cyan
        Write($"{m_moveCount.ToString.PadRight(5)}")

        If displaying = Display.Level Then
          DrawHud()
          DrawClock()
        End If

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
    SetCursorPosition(0, 24)
    Write("-Press space to continue-")
    PressSpaceToContinue()
  End Sub

  Private Sub DrawHelp()
    ResetColor()
    Clear()
    WriteLine("F1      list of commands                F2      list of symbols")
    WriteLine("F3      repeat command                  F4      repeat message")
    WriteLine("F5      rename something                F6      recall what's been discovered")
    WriteLine("F7      inventory of your possessions   F8      <dir> identify trap type")
    WriteLine("F9      The Any Key (definable)         Alt F9  defines the Any Key")
    WriteLine("F10     Supervisor Key (fake dos)       Space   Clear -More- message")
    WriteLine("CR      the Enter Key                   Lt      left")
    WriteLine("Dn      down                            Up      up")
    WriteLine("Rt      right                           Home    up & left")
    WriteLine("PgUp    up & right                      End     down & left")
    WriteLine("PgDn    down & right                    Scroll  Fast Play mode")
    WriteLine(".       rest                            >       go down a staircase")
    WriteLine("<       go up the staircase             Esc     cancel command")
    WriteLine("d       drop object                     e       eat food")
    WriteLine("f       <dir> find something            q       quaff potion")
    WriteLine("r       read paper                      s       search for trap/secret door")
    WriteLine("t       <dir> throw something           w       wield a weapon")
    WriteLine("z       <dir> zap with a wand           B       run down & left")
    WriteLine("H       run left                        J       run down")
    WriteLine("K       run up                          L       run right")
    WriteLine("N       run down & right                U       run up & right")
    WriteLine("Y       run up & left                   W       wear armor")
    WriteLine("T       take armor off                  P       put on ring")
    SetCursorPosition(0, 24)
    Write("-Press space for more, Esc to continue-")
    Dim result = PressSpaceForMoreEscToContinue()
    If result Then
      Return
    Else
      Clear()
      WriteLine("Q       quit                            R       remove ring")
      WriteLine("S       save game                       ^       identify trap")
      WriteLine("?       help                            /       key")
      WriteLine("+       throw                           -       zap")
      WriteLine("Ctrl t  terse message format            Ctrl r  repeat message")
      WriteLine("Del     search for something hidden     Ins     <dir> find something")
      WriteLine("a       repeat command                  c       rename something")
      WriteLine("i       inventory                       v       version number")
      WriteLine("!       Supervisor Key (fake dos)       D       list what has been discovered")
      SetCursorPosition(0, 24)
      Write("-Press space to continue-")
      PressSpaceForMoreEscToContinue()
    End If
  End Sub

  Private Sub DisplayMessage(message$)
    ResetColor()
    SetCursorPosition(0, 0)
    Write(message.PadRight(80).Substring(0, 80))
  End Sub

  Private Sub ClearMessage()
    ResetColor()
    SetCursorPosition(0, 0)
    Write(New String(" "c, 80))
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

  Private Function LoadDungeon(path As String) As List(Of Level)

    Dim result = New List(Of Level)

    Dim lines = IO.File.ReadAllLines(path)

    Dim lineIndex = 0
    Dim level = -1
    Do

      If lineIndex > lines.Count - 1 Then
        Exit Do
      End If

      If lines(lineIndex).ToLower.StartsWith("level:") Then
        level = CInt(lines(lineIndex).Substring(6).Trim)
        result.Add(New Program.Level())
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
            result(level - 1).Map(i, c) = New Tile(lines(lineIndex)(c))
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
    SetCursorPosition(75, 24)
    Write($"{Now:h:mm}".PadLeft(5))
  End Sub

  Private Sub DrawHud()

    ForegroundColor = ConsoleColor.Yellow

    SetCursorPosition(0, 23)

    Dim level = $"{m_level + 1}".PadRight(5)
    Dim hits = $"{m_heroHp}({m_heroHpMax})".PadRight(8)
    Dim str = $"{m_heroStr}({m_heroStrMax})".PadRight(8)
    Dim gold = $"{m_heroGold}".PadRight(6)
    Dim armor = $"{m_heroArmor}".PadRight(7)

    Write($"Level:{level} Hits:{hits} Str:{str} Gold:{gold} Armor:{armor}{HeroLevel(m_heroLevel).PadRight(15)}")

  End Sub

  Private Function GetCharacterName() As String

    Clear()

    ForegroundColor = ConsoleColor.DarkYellow

    Write($"╔{New System.String("═"c, 78)}╗")
    For r = 1 To 21
      SetCursorPosition(0, r)
      Write($"║{Space(78)}║")
    Next
    SetCursorPosition(0, 22)
    Write($"╠{New System.String("═"c, 78)}╣")
    SetCursorPosition(0, 23)
    Write($"║{Space(78)}║")
    SetCursorPosition(0, 24)
    Write($"╚{New System.String("═"c, 78)}╝")

    BackgroundColor = ConsoleColor.Gray
    Center("ROGUE:  The Adventure Game", 2, ConsoleColor.Black)

    ResetColor()

    Center("The game of Rogue was originated by:", 4, ConsoleColor.Magenta)
    Center("Michael C. Toy", 6, ConsoleColor.White)
    Center("and", 7, ConsoleColor.Gray)
    Center("Kenneth C.R.C. Arnold", 8, ConsoleColor.White)

    Center("Adapted for the IBM PC by:", 10, ConsoleColor.Magenta)
    Center("Jon Lane", 12, ConsoleColor.White)
    Center("Significant design contributions by:", 14, ConsoleColor.Magenta)
    Center("Glenn Wichman and scores of others", 16, ConsoleColor.White)

    Center("(C) Copyright 1983", 18, ConsoleColor.Yellow)
    Center("Artificial Intelligence Design", 19, ConsoleColor.White)
    Center("All Rights Reserved", 20, ConsoleColor.Yellow)

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
    Dim c = (80 - Len(text)) \ 2
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

    Dim floors As New List(Of Tile)

    For y = 0 To 20
      For x = 0 To 79

        Dim tile = m_levels(m_level).Map(y, x)

        If tile.HeroStart OrElse tile.Type = TileType.Floor Then
          floors.Add(tile)
        End If

        If tile.HeroStart Then
          m_heroX = x : m_heroY = y
        ElseIf tile.Type = TileType.Hole Then
          m_holeX = x : m_holeY = y
        End If

      Next
    Next

    Dim goldCount = Randomizer.Next(2, 5)

    For gold = 1 To goldCount
      Dim tileNumber = Randomizer.Next(0, floors.Count - 1)
      floors(tileNumber).Gold = Randomizer.Next(1, 100)
      floors.RemoveAt(tileNumber)
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

    Dim zone = DetermineZone(m_heroX, m_heroY)
    Dim lit = If(zone > -1, m_levels(m_level).Lights(zone), False)
    If lit Then
      DrawRoom(m_heroX, m_heroY)
    End If
    PlaceHero()
  End Sub

  Private Function CanMove(x%, y%) As Boolean
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
    Dim floor = If(zone > -1, m_levels(m_level).Map(m_heroY, m_heroX).Type = TileType.Floor, False)

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

    Dim tile = m_levels(m_level).Map(m_heroY, m_heroX)

    Select Case tile.Type
      Case TileType.Floor
        For yy = -1 To 1
          For xx = -1 To 1
            Dim foundStairs = DrawRoomTile(m_heroX + xx, m_heroY + yy)
            If foundStairs Then
              m_holeFound = True
            End If
          Next
        Next
      Case TileType.Tunnel
        For yy = -1 To 1
          For xx = -1 To 1
            Dim target = m_levels(m_level).Map(m_heroY + yy, m_heroX + xx)
            Select Case target.Type
              Case TileType.Tunnel, TileType.Door
                DrawRoomTile(m_heroX + xx, m_heroY + yy)
              Case Else
            End Select
          Next
        Next
      Case TileType.Door, TileType.SecretHorizontal, TileType.SecretVertical
        If lit Then
          ' In the doorway, determine the floor to the left, right, top or bottom.
          ' Once found, use "paint" routine above...
          Dim coords = {New Coord(0, -1),
                        New Coord(1, 0),
                        New Coord(0, 1),
                        New Coord(-1, 0)}
          For Each coord In coords
            Dim target = m_levels(m_level).Map(m_heroY + coord.Y, m_heroX + coord.X)
            If target.Type = TileType.Floor Then
              ' found it...
              DrawRoom(m_heroX + coord.X, m_heroY + coord.Y)
              m_levels(m_level).Lights(zone) = False
              Exit For
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

    If m_levels(m_level).Map(m_heroY, m_heroX).Type = TileType.Hole Then
      result = True
      BackgroundColor = ConsoleColor.Green
    Else
      BackgroundColor = ConsoleColor.Black
    End If
    ForegroundColor = ConsoleColor.Yellow
    SetCursorPosition(m_heroX, m_heroY + 1)
    Write("☺")

    Return result

  End Function

  Private Sub DrawRoom(x As Integer, y As Integer)

    ' Find the top / left floor tile in this room.

    Dim xx = x
    Dim yy = y
    Do
      If m_levels(m_level).Map(yy, xx - 1).Type = TileType.Floor Then
        xx -= 1
      Else
        Exit Do
      End If
    Loop
    Do
      If m_levels(m_level).Map(yy - 1, xx).Type = TileType.Floor Then
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
        Select Case m_levels(m_level).Map(yyy, xxx).Type
          Case TileType.Void, TileType.Tunnel
            Exit Do
          Case Else
        End Select
      Loop

      xxx = xx - 1
      yyy += 1
      If yyy > 20 Then Exit Do
      Select Case m_levels(m_level).Map(yyy, xxx).Type
        Case TileType.Void, TileType.Tunnel
          Exit Do
        Case Else
      End Select

    Loop

  End Sub

  Private Function DrawRoomTile(x%, y%) As Boolean
    Dim result = False
    Dim tile = m_levels(m_level).Map(y, x)
    tile.Explored = True
    Dim output = tile.ToString
    Dim fg = ConsoleColor.DarkGray
    Dim bg = ConsoleColor.Black
    Select Case tile.Type
      Case TileType.WallTopLeft : fg = ConsoleColor.DarkYellow
      Case TileType.WallTopRight : fg = ConsoleColor.DarkYellow
      Case TileType.WallHorizontal : fg = ConsoleColor.DarkYellow
      Case TileType.WallVertical : fg = ConsoleColor.DarkYellow
      Case TileType.WallBottomLeft : fg = ConsoleColor.DarkYellow
      Case TileType.WallBottomRight : fg = ConsoleColor.DarkYellow
      Case TileType.SecretHorizontal : fg = ConsoleColor.DarkYellow
      Case TileType.SecretVertical : fg = ConsoleColor.DarkYellow
      Case TileType.Door : fg = ConsoleColor.DarkYellow
      Case TileType.Hole
        fg = ConsoleColor.Black : bg = ConsoleColor.Green
        result = True
      Case TileType.Floor
        If tile.Gold > 0 Then
          fg = ConsoleColor.Yellow
          output = "*"
        Else
          fg = ConsoleColor.DarkGreen
        End If
      Case TileType.Tunnel
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
