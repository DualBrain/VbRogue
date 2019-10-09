Option Explicit On
Option Strict On
Option Infer On

Imports ConsoleEx

Imports System.Console
Imports System.Runtime.InteropServices
Imports System.Text

Module Program

  'Sub Main(args As String())
  '  ' The following is from a sample by Lucian... 
  '  ' so I'll take it as the gospel. ;-)
  '  MainAsync(args).GetAwaiter().GetResult()
  'End Sub

  Private ReadOnly Hero As New Core.Hero
  Private m_levels As List(Of Core.Level)

#Region "XP Table"

  'Private Class XP
  '  Public Sub New(level%, points%, dice%, healRate%)
  '    Me.Level = level
  '    Me.Points = points
  '    Me.Dice = dice
  '    Me.HealRate = healRate
  '  End Sub
  '  Public ReadOnly Property Level As Integer
  '  Public ReadOnly Property Points As Integer
  '  Public ReadOnly Property Dice As Integer
  '  Public ReadOnly Property HealRate As Integer
  'End Class

  'Private ReadOnly XpTable As XP() = {New XP(0, 0, 1, 18),
  '                             New XP(1, 10, 1, 17),
  '                             New XP(2, 20, 1, 15),
  '                             New XP(3, 40, 1, 13),
  '                             New XP(4, 80, 1, 11),
  '                             New XP(5, 160, 1, 9),
  '                             New XP(6, 320, 1, 7),
  '                             New XP(7, 640, 1, 3),
  '                             New XP(8, 1280, 2, 3),
  '                             New XP(9, 2560, 3, 3),
  '                             New XP(10, 5120, 4, 3),
  '                             New XP(11, 10240, 5, 3),
  '                             New XP(12, 20480, 6, 3),
  '                             New XP(13, 40960, 7, 3),
  '                             New XP(14, 81920, 8, 3),
  '                             New XP(15, 163840, 9, 3),
  '                             New XP(16, 327680, 10, 3),
  '                             New XP(17, 655360, 11, 3),
  '                             New XP(18, 1310720, 12, 3),
  '                             New XP(19, 2621440, 13, 3),
  '                             New XP(20, 5242880, 14, 3)}

#End Region

  'Private Async Function MainAsync(args As String()) As Task
  Public Sub Main() 'args As String())

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

      'If 1 = 0 Then
      '  m_levels = LoadDungeon("default.rogue")
      'Else
      '  m_levels = New List(Of Core.Level)
      '  For index = 1 To 1 '26
      '    m_levels.Add(New Core.Level(index))
      '  Next
      'End If

      Core.Param.IsDebugMode = False

      If False Then
        m_levels = LoadDungeon("default.rogue")
      Else
        m_levels = New List(Of Core.Level) From {New Core.Level(1)}
        'Core.Map.SaveDungeon(m_levels)
      End If

      Hero.Name = GetCharacterName()

      InitializeLevel()

      DisplayMessage($"Hello {Hero.Name}.  Are you prepared to die?.")

      ' "Game Loop"

#Region "Variables to handle 'blink'"
      Dim blink = True
      Dim cycles = 0
#End Region

      Do

        Threading.Thread.Sleep(1)

        If m_levels(Hero.DungeonLevel).StairsFound AndAlso
           cycles > 250 AndAlso
           (m_levels(Hero.DungeonLevel).StairCoords.Y > -1 AndAlso m_levels(Hero.DungeonLevel).StairCoords.X > -1) AndAlso
           Not (Hero.Y = m_levels(Hero.DungeonLevel).StairCoords.Y AndAlso Hero.X = m_levels(Hero.DungeonLevel).StairCoords.X) Then
          cycles = 0
          ' Draw stairway...
          ForegroundColor = ConsoleColor.Black
          BackgroundColor = ConsoleColor.Green
          SetCursorPosition(m_levels(Hero.DungeonLevel).StairCoords.X, m_levels(Hero.DungeonLevel).StairCoords.Y + 1)
          If blink Then
            Write(" ")
          Else
            Write("≡")
          End If
          ResetColor()
          blink = Not blink
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

            Case ConsoleKey.Backspace

              ' ??????

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

              If Hero.X = m_levels(Hero.DungeonLevel).StairCoords.X AndAlso
                 Hero.Y = m_levels(Hero.DungeonLevel).StairCoords.Y Then
                TravelDownAction()
              End If

            Case ConsoleKey.D, ConsoleKey.E

              ItemInteraction(ki)

            Case ConsoleKey.I

              DrawInventory(Nothing)
              DrawLevel()

            Case ConsoleKey.O
              DisplayMessage("I don't have any options, oh my!")

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

            Case ConsoleKey.F10
              Core.Map.SaveDungeon(m_levels)

            Case ConsoleKey.F12

              m_levels(Hero.DungeonLevel) = New Core.Level(Hero.DungeonLevel)
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
              Dim c = ki.KeyChar
              Select Case ki.Key
                Case ConsoleKey.Enter, ConsoleKey.Backspace
                  c = " "c
                Case Else
              End Select
              DisplayMessage($"Illegal command '{c}'")
          End Select

          If isAction Then

            Dim repeatCount = 1
            If Not String.IsNullOrWhiteSpace(m_accumulator) Then
              repeatCount = CInt(m_accumulator)
            End If

            For i = repeatCount To 1 Step -1

              'ClearMessage()

              Dim dirX = 0 'Hero.X
              Dim dirY = 0 'Hero.Y

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
                  If SearchAction() Then
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

                  If CanMove(Hero.X + dirX, Hero.Y + dirY) Then
                    DrawRoomTile(Hero.X, Hero.Y)
                    Hero.X += dirX
                    Hero.Y += dirY
                    Dim encounter = PlaceHero()
                    actions += 1
                    initialMove = False
                    If encounter OrElse Not run Then
                      Exit Do
                    Else
                      Dim tile = m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X)
                      If tile.Type = Core.TileType.Door Then
                        Exit Do
                      End If
                    End If
                  Else

                    If Not initialMove Then
                      Dim tile = m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X)
                      If tile.Type = Core.TileType.Tunnel Then

                        If dirX = 0 Then
                          If CanMove(Hero.X - 1, Hero.Y) Then
                            ' Can move to the left
                            dirX = -1 : dirY = 0
                          ElseIf CanMove(Hero.X + 1, Hero.Y) Then
                            ' Can move to the right
                            dirX = 1 : dirY = 0
                          Else
                            abort = True
                            Exit Do
                          End If
                        ElseIf dirY = 0 Then
                          If CanMove(Hero.X, Hero.Y - 1) Then
                            ' Can move up
                            dirX = 0 : dirY = -1
                          ElseIf CanMove(Hero.X, Hero.Y + 1) Then
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

                Dim currentHungerStage = Hero.HungerStage

                Hero.MoveCount += actions
                Hero.HungerCount += actions

                ' Handle regeneration (healing)...
                If Hero.HP < Hero.MaxHP Then
                  Hero.HealCount += actions
                  If Hero.HealCount >= Hero.HealTurns Then
                    If Hero.HealAmount = 1 Then
                      Hero.HP += Hero.HealAmount
                    Else
                      Hero.HP += Core.Param.Randomizer.Next(1, Hero.HealAmount + 1)
                    End If
                    If Hero.HP > Hero.MaxHP Then
                      Hero.HP = Hero.MaxHP
                    End If
                    Hero.HealCount = 0
                  End If
                End If

                ' Handle "hunger" (transition)...

                If currentHungerStage <> Hero.HungerStage Then
                  DisplayMessage("You are starting to get hungry")
                End If

              End If

              If Not String.IsNullOrWhiteSpace(m_accumulator) Then
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

        DrawHud()
        DrawClock()

        If Hero.Dead Then
          ' Exit game loop.
          Exit Do
        End If

        cycles += 1

      Loop

      ' Handle end-of-game stuff.

      ResetColor()
      Clear()

      SetCursorPosition(0, 0)
      If Hero.HP < 1 Then
        Write($"You DIED!") 'TODO: Need to determine message - play the original.
      Else
        Write($"You quit with {Hero.Gold} gold pieces")
      End If

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

      CursorVisible = True

      If Debugger.IsAttached Then
        WriteLine()
        WriteLine("Press enter to continue...")
        ReadLine()
      End If

    End Try

#End Region

  End Sub

#Region "Actions"

  Private Sub ItemInteraction(ki As ConsoleKeyInfo)

    Dim action As String = Nothing
    Dim filter As Core.ObjectType? = Nothing

    Select Case ki.Key
      Case ConsoleKey.D

        Dim tile = m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X)

        Dim canDrop = tile.Type = Core.TileType.Floor AndAlso
                      tile.ObjectType = Core.ObjectType.None

        If Not canDrop Then

          ' Can't drop where we just dropped an item as only one item can be
          ' on the floor tile at a given time.  If we move off of the tile we
          ' just dropped something and move back, we automatically pick it back
          ' up which clears the tile for another drop.

          DisplayMessage("There is something there already")

          Return

        End If

        action = "drop"
        filter = Core.ObjectType.None

      Case ConsoleKey.E
        action = "eat"
        filter = Core.ObjectType.Food
    End Select

    If action Is Nothing Then
      Return
    End If

Start:

    ClearMessage(True)
    SetCursorPosition(0, 0)
    ForegroundColor = ConsoleColor.Gray
    BackgroundColor = ConsoleColor.Black
    Write($"Which object do you want to {action}? (* for list):")

    Do
      If KeyAvailable Then

        ki = ReadKey(True)

        Dim kc As Char? = ki.KeyChar

        Select Case kc
          Case "*"c
            If filter = Core.ObjectType.Food AndAlso Hero.Rations = 0 Then
              ClearMessage(True)
              DisplayMessage("You don't have anything appropriate")
              Exit Do
            Else
              kc = DrawInventory(filter)
              DrawLevel()
            End If
          Case Else
        End Select

        'Dim low = 0
        Dim high = If(Hero.Rations > 0, 1, 0) + Hero.Inventory.Count

        Select Case kc
          Case "a"c To ChrW(AscW("a"c) + high - 1)
            Select Case filter
              Case Core.ObjectType.Food
                EatFoodAction(kc)
              Case Else
                DropItemAction(kc)
            End Select
            Exit Do
          Case ChrW(AscW("a"c) + high) To "z"c
            ClearMessage(True)
            SetCursorPosition(0, 0)
            Write($"Please specify a letter between 'a' and '{ChrW(AscW("a"c) + high - 1)}'")
            'Swap ForegroundColor, BackgroundColor
            ForegroundColor = ConsoleColor.Black
            BackgroundColor = ConsoleColor.Gray
            Write(" More ")
            Do
              If KeyAvailable Then
                ki = ReadKey(True)
                If ki.Key = ConsoleKey.Spacebar Then
                  Exit Do
                End If
              End If
            Loop
            GoTo Start
          Case Else
        End Select
      End If
    Loop

  End Sub

  Private Sub DropItemAction(item As Char?)

    If item Is Nothing Then Return

    ClearMessage(True)

    If item = "a"c AndAlso Hero.Rations > 0 Then
      Hero.Rations -= 1
      m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType = Core.ObjectType.Food
      m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectCount = 1
      Return
    End If

    Dim index = (AscW(CChar(item)) - AscW("a"c)) - (If(Hero.Rations > 0, 1, 0))

    Hero.Inventory(index).Equiped = False
    Dim description = Hero.Inventory(index).ToString
    m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType = Hero.Inventory(index).Object.Type
    m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).Object = Hero.Inventory(index).Object
    m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectCount = Hero.Inventory(index).Count
    Hero.Inventory.RemoveAt(index)

    DisplayMessage($"Dropped {description}")

  End Sub

  Private Sub EatFoodAction(item As Char?)

    ClearMessage(True)

    Select Case item
      Case "a"c
        If Hero.Rations > 0 Then
          Hero.HungerCount -= 1000
          Hero.Rations -= 1
          DisplayMessage("Yum, that tasted good")
          Return
        Else
          ' Fall through...
        End If
      Case "b"c To "e"c
        ' Fall through...
      Case Else
        Stop ' Should never land here...
    End Select

    DisplayMessage("Ugh, you would get ill if you ate that")

  End Sub

  Private Function SearchAction() As Boolean

    Dim result = False

    For yy = -1 To 1
      For xx = -1 To 1
        Dim tile = m_levels(Hero.DungeonLevel).Map(Hero.Y + yy, Hero.X + xx)
        If tile.FoundSecret(False) Then
          DrawRoomTile(Hero.X + xx, Hero.Y + yy)
          result = True
        End If
      Next
    Next

    Return result

  End Function

  Private Sub TravelDownAction()

    Hero.DungeonLevel += 1

    If Hero.DungeonLevel >= Core.Param.MapLevelsMax Then
      Hero.Dead = True 'HACK: We are killing of the hero because we have no more levels.
      'TODO: Should modify the game state so that we now have to find the staff and go back up the levels.
    Else
      m_levels.Add(New Core.Level(Hero.DungeonLevel))
      InitializeLevel()
    End If

    'If Hero.DungeonLevel > m_levels.Count - 1 Then
    '  Hero.Dead = True 'HACK: We are killing of the hero because we have no more levels.
    'Else
    '  InitializeLevel()
    'End If

  End Sub

#End Region

#Region "'Screens'"

#Region "Intro Screen"

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

    Center("Adapted for .NET Core by:", 10, ConsoleColor.Magenta)
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
              If String.IsNullOrWhiteSpace(accumulator) Then
                Return "Whimp"
              Else
                Return accumulator
              End If
            Case ConsoleKey.Backspace
              If Not String.IsNullOrWhiteSpace(accumulator) Then
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

#End Region

#Region "Main Screen"

  Private Sub DrawLevel()

    ResetColor()
    Clear()

    For x = 0 To 79
      For y = 0 To 20
        If m_levels(Hero.DungeonLevel).Map(y, x).Explored Then
          DrawRoomTile(x, y)
        End If
      Next
    Next

    Dim zone = Core.Level.DetermineZone(Hero.X, Hero.Y)
    Dim lit = If(zone > -1, m_levels(Hero.DungeonLevel).Lights(zone), False)
    If lit Then
      DrawRoom(Hero.X, Hero.Y)
    End If

    PlaceHero()

  End Sub

  Private Sub DrawClock()
    ForegroundColor = ConsoleColor.Black
    BackgroundColor = ConsoleColor.Gray
    SetCursorPosition(74, 24)
    Write($"{Date.Now:h:mm}".PadLeft(5))
  End Sub

  Private Sub DrawHud()

    ResetColor()

    ForegroundColor = ConsoleColor.Yellow

    SetCursorPosition(0, 23)

    Dim level = $"{Hero.DungeonLevel + 1}".PadRight(5)
    Dim hits = $"{Hero.HP}({Hero.MaxHP})".PadRight(8)
    Dim str = $"{Hero.Str}({Hero.MaxStr})".PadRight(8)
    Dim gold = $"{Hero.Gold}".PadRight(6)
    Dim armor = $"{Hero.AC}".PadRight(7)

    Write($"Level:{level} Hits:{hits} Str:{str} Gold:{gold} Armor:{armor}{Core.Hero.HeroLevel(Hero.ExperienceLevel).PadRight(12)}")

    ' Handle "hunger"

    ResetColor()
    SetCursorPosition(57, 24)
    Write("".PadRight(8))

    If Hero.HungerStage > Core.HungerStage.Healthy Then
      SetCursorPosition(57, 24)
      Dim hunger$ = Hero.HungerStage.ToString
      ForegroundColor = ConsoleColor.Black
      BackgroundColor = ConsoleColor.Gray
      Write(hunger)
      ResetColor()
    End If

  End Sub

  Private Sub DrawRoom(x As Integer, y As Integer)

    ' Find the top / left floor tile in this room.

    Dim xx = x
    Dim yy = y
    Do
      If m_levels(Hero.DungeonLevel).Map(yy, xx - 1).Type = Core.TileType.Floor Then
        xx -= 1
      Else
        Exit Do
      End If
    Loop
    Do
      If m_levels(Hero.DungeonLevel).Map(yy - 1, xx).Type = Core.TileType.Floor Then
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
          m_levels(Hero.DungeonLevel).StairsFound = True
        End If
        xxx += 1
        If xxx > 79 Then Exit Do
        Select Case m_levels(Hero.DungeonLevel).Map(yyy, xxx).Type
          Case Core.TileType.Void, Core.TileType.Tunnel
            Exit Do
          Case Else
        End Select
      Loop

      xxx = xx - 1
      yyy += 1
      If yyy > 20 Then Exit Do
      Select Case m_levels(Hero.DungeonLevel).Map(yyy, xxx).Type
        Case Core.TileType.Void, Core.TileType.Tunnel
          Exit Do
        Case Else
      End Select

    Loop

  End Sub

  Private Function DrawRoomTile(x%, y%) As Boolean
    If y > 20 Then Return False
    Dim result = False
    Dim tile = m_levels(Hero.DungeonLevel).Map(y, x)
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
        Dim t = If(Not If(tile.Object?.Hidden, False), tile.ObjectType, Nothing)
        Select Case t 'tile.ObjectType
          Case Core.ObjectType.Trap
            fg = ConsoleColor.Red
            output = "♦"
          Case Core.ObjectType.Gold
            fg = ConsoleColor.Yellow
            output = "*"
          Case Core.ObjectType.Food
            fg = ConsoleColor.Red
            output = "♣"
          Case Core.ObjectType.Weapon
            fg = ConsoleColor.Blue
            output = "↑"
          Case Else
            fg = ConsoleColor.DarkGreen
        End Select
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

#End Region

#Region "Help Screen"

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

#Disable Warning IDE0028 ' Simplify collection initialization
    Dim commands = New List(Of KeyCommand)
#Enable Warning IDE0028 ' Simplify collection initialization

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
    commands.Add(New KeyCommand(".", "rest", True))
    commands.Add(New KeyCommand(">", "go down a staircase", True))
    commands.Add(New KeyCommand("<", "go up the staircase"))
    commands.Add(New KeyCommand("Esc", "cancel command", True))
    commands.Add(New KeyCommand("d", "drop object", True))
    commands.Add(New KeyCommand("e", "eat food", True))
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

#End Region

#Region "Inventory Screen"

  Private Function DrawInventory(filter As Core.ObjectType?) As Char?

    ResetColor()
    Clear()

    Dim page = 0
    Dim index = 0

    Do

      Console.Clear()

      Dim row = 0

      Dim letter = 0

      If filter Is Nothing OrElse
         filter = Core.ObjectType.None OrElse
         filter = Core.ObjectType.Food Then
        If Hero.Rations > 0 Then
          If Hero.Rations = 1 Then
            Console.Write($"{ChrW(CInt(AscW("a") + letter))}) Some food")
          Else
            Console.Write($"{ChrW(CInt(AscW("a") + letter))}) {Hero.Rations} rations of food")
          End If
          letter += 1
          row += 1
        End If
      End If

      Do Until index > 21

        If filter Is Nothing OrElse
           filter = Core.ObjectType.None OrElse
           Hero.Inventory(index).Object.Type = filter Then

          Console.SetCursorPosition(0, row)

          Select Case Hero.Inventory(index).Object.Type
            Case Core.ObjectType.Armor
              Console.Write($"{ChrW(CInt(AscW("a") + letter))}) {Hero.Inventory(index).ToString} {If(Hero.Inventory(index).Equiped, "(being worn)", "")}")
            Case Core.ObjectType.Weapon
              If Hero.Inventory(index).Object.Name.Contains("arrow") OrElse
                 Hero.Inventory(index).Object.Name.Contains("bolt") Then
                Console.Write($"{ChrW(CInt(AscW("a") + letter))}) {Hero.Inventory(index).Count} {Hero.Inventory(index).ToString}s {If(Hero.Inventory(index).Equiped, "(weapon in hand)", "")}")
              Else
                Console.Write($"{ChrW(CInt(AscW("a") + letter))}) {If(Hero.Inventory(index).Count = 1, "A", "????")} {Hero.Inventory(index).ToString} {If(Hero.Inventory(index).Equiped, "(weapon in hand)", "")}")
              End If
            Case Else
              Console.Write($"{ChrW(CInt(AscW("a") + letter))}) {Hero.Inventory(index).ToString}")
          End Select

          row += 1
          letter += 1

        End If

        index += 1

        If (page * 21) + index > Hero.Inventory.Count - 1 Then
          Exit Do
        End If

      Loop

      page += 1
      index = 0

      Select Case filter

        Case Core.ObjectType.None
          Return SelectItemEscToContinue("drop")
        Case Core.ObjectType.Food
          Return SelectItemEscToContinue("eat")

        Case Else

          If (page * 21) + index < Hero.Inventory.Count Then
            ' Still more items left, prompt as if there is another page to be viewed...
            Dim result = PressSpaceForMoreEscToContinue()
            If result Then
              Return Nothing
            End If
          Else
            ' This the last of this content, so prompt to "return to game".
            PressSpaceToContinue()
            Return Nothing
          End If

      End Select

    Loop

  End Function

  Private Function SelectItemEscToContinue(phrase As String) As Char?
    SetCursorPosition(0, 24)
    Write($"-Select item to {phrase}. Esc to cancel-")
    Do
      If KeyAvailable Then
        Dim key = ReadKey(True)
        Select Case key.Key
          Case ConsoleKey.A To ConsoleKey.Z
            Return key.KeyChar
          Case ConsoleKey.Escape
            Return Nothing
          Case Else
        End Select
      End If
    Loop
  End Function

#End Region

#Region "Symbol Screen"

  Private Sub DrawSymbols()

    ResetColor()
    Clear()

    ' Page 1
    Dim symbols = New List(Of Symbol) From {
      New Symbol(".", "the floor", ConsoleColor.DarkGreen),
      New Symbol("☺", "the hero", ConsoleColor.Yellow),
      New Symbol("♣", "some food", ConsoleColor.Red),
      New Symbol("♀", "the amulet of yendor", ConsoleColor.Blue),
      New Symbol("♪", "a scroll", ConsoleColor.Blue),
      New Symbol("↑", "a weapon", ConsoleColor.Blue),
      New Symbol("◘", "a piece of armor", ConsoleColor.Blue),
      New Symbol("✶", "some gold", ConsoleColor.Yellow),
      New Symbol("¥", "a magic staff", ConsoleColor.Blue),
      New Symbol("¡", "a potion", ConsoleColor.Blue),
      New Symbol("○", "a magic ring", ConsoleColor.Blue),
      New Symbol("▓", "a passage", ConsoleColor.Gray),
      New Symbol("╬", "a door", ConsoleColor.DarkYellow),
      New Symbol("╔", "an upper left corner", ConsoleColor.DarkYellow),
      New Symbol("♦", "a trap", ConsoleColor.Magenta),
      New Symbol("═", "a horizontal wall", ConsoleColor.DarkYellow),
      New Symbol("╝", "a lower right corner", ConsoleColor.DarkYellow),
      New Symbol("╚", "a lower left corner", ConsoleColor.DarkYellow),
      New Symbol("║", "a vertical wall", ConsoleColor.DarkYellow),
      New Symbol("╗", "an upper right corner", ConsoleColor.DarkYellow),
      New Symbol("≡", "a stair case", ConsoleColor.Green),
      New Symbol("$,+", "safe and perilous magic", ConsoleColor.Gray),
      New Symbol("A-Z", "26 different monsters", ConsoleColor.Gray)
    }

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

#End Region

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

#End Region

#Region "Message Handling"

  Private ReadOnly m_messages As New Queue(Of String) ' Holds "list" of messages; allowing for a stack.

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

  Private Sub ClearMessage(Optional clearOnly As Boolean = False)
    ResetColor()
    SetCursorPosition(0, 0)
    Write(New String(" "c, 80))
    m_displayingMessage = False
    If Not clearOnly AndAlso m_messages.Count > 0 Then
      Dim message = m_messages.Dequeue()
      DisplayMessage(message)
    End If
  End Sub

#End Region

#Region "Accumulator Handling"

  Private m_accumulator$ ' Holds the "keys" (numbers) pressed in order to do the "repeat" functionality.

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

#End Region

#Region "Helper Methods"

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
        result(level - 1).DetermineCoords()
      End If

    Loop

    Return result

  End Function

  Private Sub InitializeLevel()

    m_accumulator = Nothing

    Hero.MoveCount = 0
    Hero.X = -1
    Hero.Y = -1

    BackgroundColor = ConsoleColor.Black
    Clear()
    WriteLine()

    Dim floors As New List(Of Core.Tile)

    Hero.X = m_levels(Hero.DungeonLevel).StartCoords.X
    Hero.Y = m_levels(Hero.DungeonLevel).StartCoords.Y

    For y = 0 To 20
      For x = 0 To 79

        Dim tile = m_levels(Hero.DungeonLevel).Map(y, x)

        If Not tile.HeroStart AndAlso
           tile.Type = Core.TileType.Floor Then
          floors.Add(tile)
        End If

      Next
    Next

    Dim minGoldCount = 0
    Dim maxGoldCount = 3
    If minGoldCount > maxGoldCount Then minGoldCount = maxGoldCount

    Dim goldCount = Core.Param.Randomizer.Next(minGoldCount, maxGoldCount + 1)

    For gold = 1 To goldCount
      If floors.Count > 0 Then
        Dim tileNumber = Core.Param.Randomizer.Next(0, floors.Count - 1)
        'floors(tileNumber).Gold = Randomizer.Next(1, 100)
        floors(tileNumber).ObjectType = Core.ObjectType.Gold
        floors(tileNumber).ObjectCount = Core.Param.Randomizer.Next(1, 100)
        floors.RemoveAt(tileNumber)
      End If
    Next

    Dim minFoodCount = 0
    Dim maxFoodCount = 3
    If minFoodCount > maxFoodCount Then minFoodCount = maxFoodCount

    Dim foodCount = Core.Param.Randomizer.Next(minFoodCount, maxFoodCount + 1)

    For food = 1 To foodCount
      If floors.Count > 0 Then
        Dim tileNumber = Core.Param.Randomizer.Next(0, floors.Count - 1)
        floors(tileNumber).ObjectType = Core.ObjectType.Food
        floors(tileNumber).ObjectCount = 1
        floors.RemoveAt(tileNumber)
      End If
    Next

    Dim minTrapCount = 0 'If(Debugger.IsAttached, 10, 0)
    Dim maxTrapCount = 2 'If(Debugger.IsAttached, 10, 2)
    If minTrapCount > maxTrapCount Then minTrapCount = maxTrapCount

    Dim trapCount = Core.Param.Randomizer.Next(minTrapCount, maxTrapCount + 1)

    For trap = 1 To trapCount
      If floors.Count > 0 Then
        Dim tileNumber = Core.Param.Randomizer.Next(0, floors.Count - 1)
        floors(tileNumber).ObjectType = Core.ObjectType.Trap
        Dim trapType = Core.Param.Randomizer.Next(0, 3)
        Select Case trapType
          Case 0 : floors(tileNumber).Object = Core.Trap.Template(Core.TrapType.Hole)
          Case 1 : floors(tileNumber).Object = Core.Trap.Template(Core.TrapType.Teleport)
          Case 2 : floors(tileNumber).Object = Core.Trap.Template(Core.TrapType.Arrow)
          Case Else
            Stop
        End Select
        floors(tileNumber).Object.Hidden = True 'If(Debugger.IsAttached, False, True)
        floors.RemoveAt(tileNumber)
      End If
    Next

    DrawLevel()

  End Sub

  Private Function CanMove(x%, y%) As Boolean
    If x < 0 OrElse y < 0 OrElse x > 79 OrElse y > 20 Then Return False
    Return m_levels(Hero.DungeonLevel).Map(y, x).PassThrough
  End Function

  Private Function PlaceHero() As Boolean

    Dim result = False

    Dim zone = Core.Level.DetermineZone(Hero.X, Hero.Y)
    Dim lit = If(zone > -1, m_levels(Hero.DungeonLevel).Lights(zone), False)
    Dim floor = If(zone > -1, m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).Type = Core.TileType.Floor, False)

    'If lit AndAlso floor Then

    '  Dim xx = Hero.X
    '  Dim yy = Hero.Y
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

    If Hero.Y > -1 AndAlso Hero.X > -1 Then

      Dim tile = m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X)

      Select Case tile.Type
        Case Core.TileType.Floor
          For yy = -1 To 1
            For xx = -1 To 1
              Dim foundStairs = DrawRoomTile(Hero.X + xx, Hero.Y + yy)
              If foundStairs Then
                m_levels(Hero.DungeonLevel).StairsFound = True
              End If
            Next
          Next
        Case Core.TileType.Tunnel
          For yy = -1 To 1
            If Hero.Y + yy > 20 Then Continue For
            If Hero.Y + yy < 0 Then Continue For
            For xx = -1 To 1
              If Hero.X + xx > 79 Then Continue For
              If Hero.X + xx < 0 Then Continue For
              Dim target = m_levels(Hero.DungeonLevel).Map(Hero.Y + yy, Hero.X + xx)
              Select Case target.Type
                Case Core.TileType.Tunnel, Core.TileType.Door
                  DrawRoomTile(Hero.X + xx, Hero.Y + yy)
                Case Else
              End Select
            Next
          Next
        Case Core.TileType.Door, Core.TileType.SecretHorizontal, Core.TileType.SecretVertical
          If lit Then
            ' In the doorway, determine the floor to the left, right, top or bottom.
            ' Once found, use "paint" routine above...
            Dim coords = {New Core.Coordinate(0, -1),
                          New Core.Coordinate(1, 0),
                          New Core.Coordinate(0, 1),
                          New Core.Coordinate(-1, 0)}
            For Each coord In coords
              If Not Hero.Y + coord.Y > 20 Then
                Dim target = m_levels(Hero.DungeonLevel).Map(Hero.Y + coord.Y, Hero.X + coord.X)
                If target.Type = Core.TileType.Floor Then
                  ' found it...
                  DrawRoom(Hero.X + coord.X, Hero.Y + coord.Y)
                  m_levels(Hero.DungeonLevel).Lights(zone) = False
                  Exit For
                End If
              End If
            Next
          Else
            For yy = -1 To 1
              For xx = -1 To 1
                Dim foundStairs = DrawRoomTile(Hero.X + xx, Hero.Y + yy)
                If foundStairs Then
                  m_levels(Hero.DungeonLevel).StairsFound = True
                End If
              Next
            Next
          End If
        Case Else
      End Select

      'End If

      Select Case m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType
        Case Core.ObjectType.Trap
          Dim trap = DirectCast(m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).[Object], Core.Trap)

          DisplayMessage($"You stumbled upon a {trap.Name}") 'TODO: Need to determine the actual verbiage.

          Select Case trap.TrapType

            Case Core.TrapType.Hole

              m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType = Core.ObjectType.None

              TravelDownAction()

              ' Hero new level/floor start cannot contain any item, so return false.
              result = False

            Case Core.TrapType.Arrow

              Dim t = DirectCast(m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).Object, Core.Trap)

              m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType = Core.ObjectType.None

              Dim dmg = Core.dice.RollDamage(t.Damage)
              Hero.HP -= dmg
              DisplayMessage($"You've lost {dmg} hit point{If(dmg > 1, "s", "")}.")

              result = False

            Case Core.TrapType.Teleport

              Dim floors As New List(Of Core.Tile)

              ' Determine "room" excluded so that we ensure teleporting to a different room.
              Dim excludeZone = Core.Level.DetermineZone(Hero.X, Hero.Y)

              ' Do a two-pass review; first pass looking for unexplored areas - second pass, all areas.
              For pass = 0 To 1
                For yy = 0 To 20
                  For xx = 0 To 79
                    If Hero.X = xx AndAlso Hero.Y = yy Then Continue For
                    Dim t = m_levels(Hero.DungeonLevel).Map(yy, xx)
                    If t.Type = Core.TileType.Floor AndAlso
                       t.ObjectType = Core.ObjectType.None AndAlso
                       (Not t.Explored OrElse pass = 1) AndAlso
                       Core.Level.DetermineZone(xx, yy) <> excludeZone Then
                      floors.Add(t)
                    End If
                  Next
                Next
                If floors.Count > 0 Then Exit For
              Next

              ' Determine target location...
              Dim tileNumber = Core.Param.Randomizer.Next(0, floors.Count - 1)

              ' Move, redraw.
              For yy = 0 To 20
                For xx = 0 To 79
                  If floors(tileNumber) Is m_levels(Hero.DungeonLevel).Map(yy, xx) Then
                    ' Remove the trap...
                    m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType = Core.ObjectType.None
                    DrawRoomTile(Hero.X, Hero.Y)
                    ' Move hero to a new (random) location...
                    Hero.X = xx
                    Hero.Y = yy
                    ' Call PlaceHero() again since we've updated the hero's location.
                    PlaceHero()
                    ' Randomly placed hero should not (ie. can't) be on a stair, so return false.
                    Return False
                  End If
                Next
              Next

            Case Else
              Stop
          End Select

        Case Core.ObjectType.Gold
          DisplayMessage($"You found {m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectCount} gold pieces")
          Hero.Gold += m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectCount
          m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType = Core.ObjectType.None
          result = True
        Case Core.ObjectType.Food
          Hero.Rations += m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectCount
          DisplayMessage($"You now have {Hero.Rations} rations of food (a)")
          m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType = Core.ObjectType.None
          result = True
        Case Core.ObjectType.Weapon
          'TODO: Needs more work. Additionally, how should items be added to inventory (sort)?
          Hero.Inventory.Add(New Core.InventoryItem() With {.[Object] = m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).[Object], .Count = m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectCount})
          Dim index = Hero.Inventory.Count - 1
          DisplayMessage($"You now have {Hero.Inventory(index).ToString} ({ChrW(AscW("a"c) + index + (If(Hero.Rations > 0, 1, 0)))})")
          m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).ObjectType = Core.ObjectType.None
          result = True
        Case Else
          ' Floor
      End Select

      If m_levels(Hero.DungeonLevel).Map(Hero.Y, Hero.X).Type = Core.TileType.Hole Then
        result = True
        BackgroundColor = ConsoleColor.Green
      Else
        BackgroundColor = ConsoleColor.Black
      End If
      ForegroundColor = ConsoleColor.Yellow
      SetCursorPosition(Hero.X, Hero.Y + 1)
      Write("☺")

    End If

    Return result

  End Function

  Private Sub Center(text As String, row As Integer, fg As ConsoleColor)
    ForegroundColor = fg
    Dim c = (80 - text.Length) \ 2
    SetCursorPosition(c, row)
    Write(text)
  End Sub

#End Region

End Module
