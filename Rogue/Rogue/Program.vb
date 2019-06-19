Option Explicit On
Option Strict On
Option Infer On

Imports System.Console
Imports System.Text
Imports ConsoleEx
Imports QB

Module Program

  'Sub Main(args As String())
  '  ' The following is from a sample by Lucian... 
  '  ' so I'll take it as the gospel. ;-)
  '  MainAsync(args).GetAwaiter().GetResult()
  'End Sub

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

      Dim ruler = "
         1         2         3         4         5         6         7         8
12345678901234567890123456789012345678901234567890123456789012345678901234567890"
      Dim map = "
             [----]                                             [--------]      
             |....|                                             |........|      
             |....|                                     ########+........|      
             |....|                                     #       |........|      
             |....+###################                  #       {--------}      
             {--+-}                  ####################                       
        #########                      ############                             
   [----+----------]                  [+----]                                   
   |...............|                  |.....|      ################             
   |..@............+##################+.....|      #              #             
   |...............|                  |.....|      #              #             
   |...............|                  |.....+#######              #             
   |...............|                  |.....|                     #             
   {---------------}                  {+----}                     #             
                                ########                   ########             
                              [-+]                         #                    
                              |..+##########               #                    
                         #####+..|         #          [----+--------------]     
                              |..|         ###########+...................|     
                              |..|                    |...........=.......|     
                              {--}                    {-------------------}     
"

      ' Convert the map to what we want to display...

      Dim temp = Split(map, vbCrLf)
      Dim room(temp.Length - 3) As String ' Look for a cleaner way to handle this.
      For index = 0 To 20
        room(index) = temp(index + 1)
      Next

      ' Holds the current location of the "stairs" (aka hole).
      Dim stairX = -1
      Dim stairY = -1
      Dim stairFound = False

      Dim heroX = -1
      Dim heroY = -1

      Clear() ' Clear the screen.
      WriteLine() ' Start with a blank line...
      For y = 0 To 20
        For x = 0 To 79
          Dim c = room(y)(x)
          Select Case c
            Case "="c : stairX = x : stairY = y
            Case "@"c
              heroX = x : heroY = y
              mid(room(y), x + 1, 1) = "."c
            Case Else
          End Select
          'DrawRoomTile(room, x, y)
        Next
      Next

      PlaceHero(room, heroX, heroY, stairFound)

      Dim alt = True

      Dim cycles = 0

      Do

        'Await Task.Delay(1)

        Threading.Thread.Sleep(1)

        If stairFound AndAlso
           cycles > 250 AndAlso
           (stairY > -1 AndAlso stairX > -1) AndAlso
           Not (heroY = stairY AndAlso heroX = stairX) Then
          cycles = 0
          ' Draw stairway...
          ForegroundColor = ConsoleColor.Black
          BackgroundColor = ConsoleColor.Green
          SetCursorPosition(stairX, stairY + 1)
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
          Select Case key.Key
            Case ConsoleKey.UpArrow
              If CanMove(room, heroX, heroY - 1) Then
                DrawRoomTile(room, heroX, heroY)
                heroY -= 1
                PlaceHero(room, heroX, heroY, stairFound)
              End If
            Case ConsoleKey.DownArrow
              If CanMove(room, heroX, heroY + 1) Then
                DrawRoomTile(room, heroX, heroY)
                heroY += 1
                PlaceHero(room, heroX, heroY, stairFound)
              End If
            Case ConsoleKey.LeftArrow
              If CanMove(room, heroX - 1, heroY) Then
                DrawRoomTile(room, heroX, heroY)
                heroX -= 1
                PlaceHero(room, heroX, heroY, stairFound)
              End If
            Case ConsoleKey.RightArrow
              If CanMove(room, heroX + 1, heroY) Then
                DrawRoomTile(room, heroX, heroY)
                heroX += 1
                PlaceHero(room, heroX, heroY, stairFound)
              End If
            Case ConsoleKey.Spacebar, ConsoleKey.Escape
              Exit Do
            Case Else
          End Select
        End If

        cycles += 1

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

  End Sub

  Private Function CanMove(room$(), x%, y%) As Boolean
    Dim c = room(y)(x)
    Select Case c
      Case "."c, "+"c, "#"c, "="c
        Return True
      Case Else
        Return False
    End Select
  End Function

  Private Sub PlaceHero(room$(), x%, y%, ByRef stairFound As Boolean)
    For yy = -1 To 1
      For xx = -1 To 1
        Dim foundStairs = DrawRoomTile(room$, x + xx, y + yy)
        If foundStairs Then
          stairFound = True
        End If
      Next
    Next
    ForegroundColor = ConsoleColor.Yellow
    SetCursorPosition(x, y + 1)
    Write(QBChar(1))
  End Sub

  Private Function DrawRoomTile(room$(), x%, y%) As Boolean
    Dim result = False
    Dim output = room(y)(x)
    Dim fg = ConsoleColor.DarkGray
    Dim bg = ConsoleColor.Black
    Select Case output
      Case "["c : output = WALL_TOP_LEFT : fg = ConsoleColor.DarkYellow
      Case "]"c : output = WALL_TOP_RIGHT : fg = ConsoleColor.DarkYellow
      Case "-"c : output = WALL_HORIZONTAL : fg = ConsoleColor.DarkYellow
      Case "|"c : output = WALL_VERTICAL : fg = ConsoleColor.DarkYellow
      Case "{"c : output = WALL_BOTTOM_LEFT : fg = ConsoleColor.DarkYellow
      Case "}"c : output = WALL_BOTTOM_RIGHT : fg = ConsoleColor.DarkYellow
      Case "+"c : output = WALL_DOOR : fg = ConsoleColor.DarkYellow
      Case "="c
        output = " "c
        fg = ConsoleColor.Black : bg = ConsoleColor.Green
        result = True
      Case "@"c : output = QBChar(1) : fg = ConsoleColor.Yellow
      Case "!"c : output = QBChar(173) : fg = ConsoleColor.DarkMagenta
      Case "%"c : output = QBChar(4) : fg = ConsoleColor.Magenta
      Case "$"c : output = QBChar(15) : fg = ConsoleColor.Yellow
      Case "^"c : output = QBChar(24) : fg = ConsoleColor.DarkMagenta
      Case "\"c : output = QBChar(8) : fg = ConsoleColor.DarkMagenta
      Case "*"c : output = QBChar(5) : fg = ConsoleColor.Red
      Case "#"c : output = TUNNEL
      Case "@"c, "."c : output = "."c : fg = ConsoleColor.DarkGreen
      Case "0"c : output = QBChar(248) : fg = ConsoleColor.DarkMagenta
      Case "9"c : output = QBChar(13) : fg = ConsoleColor.DarkMagenta
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
