Option Explicit On
Option Strict On
Option Infer On

Imports System.Console
Imports System.Text
Imports ConsoleEx
Imports QB

Module Program

  Friend SearchRandomizer As New Random()

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

  Private m_moveCount% = 0

  Private m_accumulator$

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

      ' Holds the current location of the "stairs" (aka hole).

      InitializeLevel()

      ' "Game Loop"

      Dim alt = True

      Dim cycles = 0

      Do

        'Await Task.Delay(1)

        Threading.Thread.Sleep(1)

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

          Dim key = ReadKey(True)

          Dim isAction = False

          Select Case key.Key
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
            Case ConsoleKey.Q
              'TODO: Need to determine if uppercase Q.
              If key.Modifiers = ConsoleModifiers.Shift Then
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
                    key = ReadKey(True)
                    Select Case key.Key
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
                 ConsoleKey.S

              isAction = True

            Case Else
              ' Do nothing...
          End Select

          If isAction Then

            Dim repeatCount = 1
            If m_accumulator <> "" Then
              repeatCount = CInt(m_accumulator)
            End If

            For i = repeatCount To 1 Step -1

              Select Case key.Key
                Case ConsoleKey.UpArrow
                  If CanMove(m_heroX, m_heroY - 1) Then
                    DrawRoomTile(m_heroX, m_heroY)
                    m_heroY -= 1
                    PlaceHero()
                  Else
                    Exit For
                  End If
                Case ConsoleKey.DownArrow
                  If CanMove(m_heroX, m_heroY + 1) Then
                    DrawRoomTile(m_heroX, m_heroY)
                    m_heroY += 1
                    PlaceHero()
                  Else
                    Exit For
                  End If
                Case ConsoleKey.LeftArrow
                  If CanMove(m_heroX - 1, m_heroY) Then
                    DrawRoomTile(m_heroX, m_heroY)
                    m_heroX -= 1
                    PlaceHero()
                  Else
                    Exit For
                  End If
                Case ConsoleKey.RightArrow
                  If CanMove(m_heroX + 1, m_heroY) Then
                    DrawRoomTile(m_heroX, m_heroY)
                    m_heroX += 1
                    PlaceHero()
                  Else
                    Exit For
                  End If
                Case ConsoleKey.S
                  If PerformSearch() Then
                    Exit For
                  End If
                Case Else
              End Select

              m_moveCount += 1

              If m_accumulator <> "" Then
                m_accumulator = i.ToString
                DrawAccumulator()
              End If

            Next

            ClearAccumulator()

          End If

        End If

        cycles += 1

        SetCursorPosition(0, 0)
        BackgroundColor = ConsoleColor.Black
        ForegroundColor = ConsoleColor.Cyan
        Write($"{m_moveCount}     ")

      Loop

      ResetColor()
      Clear()

      SetCursorPosition(0, 0)
      Write($"You quit with 0 gold pieces")

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
    For y = 0 To 20
      For x = 0 To 79
        Dim tile = m_levels(m_level).Map(y, x)
        If tile.HeroStart Then
          m_heroX = x : m_heroY = y
        ElseIf tile.Type = TileType.Hole Then
          m_holeX = x : m_holeY = y
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

  Private Sub PlaceHero()

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

    If m_levels(m_level).Map(m_heroY, m_heroX).Type = TileType.Hole Then
      BackgroundColor = ConsoleColor.Green
    Else
      BackgroundColor = ConsoleColor.Black
    End If
    ForegroundColor = ConsoleColor.Yellow
    SetCursorPosition(m_heroX, m_heroY + 1)
    Write("☺")

  End Sub

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
      Case TileType.Floor : fg = ConsoleColor.DarkGreen
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

End Module
