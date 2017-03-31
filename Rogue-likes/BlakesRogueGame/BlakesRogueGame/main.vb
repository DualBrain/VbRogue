Module main
    Public MAP_WIDTH As Integer = 80
    Public MAP_HEIGHT As Integer = 56
    'TODO: at start set max width and height
    'Console.WindowHeight = 81
    'Console.WindowWidth = 121
    'test from blakes
    Public nMapArray(MAP_WIDTH, MAP_HEIGHT) As Integer

    Public CONSOLE_MODE As String

    Public Enum CModes
        MAP
        BEGINMENU
        ENDMENU
    End Enum
    Dim Player1 As New Animal
    Dim MoveCount As Long
    Dim Monsters As New List(Of Animal)

    Sub Main()
        'Clear the console
        Console.Clear()
        'Dim MySubVariable As String = "BLake"
        ''Tell player about game:
        'MsgBox("This program is by: " & MySubVariable & " and the map width is: " & MAP_WIDTH)

        'Dim Proceed As Boolean = False
        'If MsgBox("Are you sure you want to do this?", MsgBoxStyle.YesNo, "Choose") = MsgBoxResult.Yes Then
        '    Proceed = True
        'Else 'They answered no
        '    Proceed = False
        'End If

        'Making the map, being in MAP mode
        CONSOLE_MODE = CModes.MAP

        'Make screen bigger
        Console.WindowHeight = MAP_HEIGHT
        Console.WindowWidth = MAP_WIDTH
        ReDim ConsoleArray(Console.WindowWidth, Console.WindowHeight)

        AddHandler Map.MapUpdated, AddressOf MapUpdateHandler
        AddHandler PlayerMoved, AddressOf PlayerMoveHandler
        AddHandler Map.MapCharChanged, AddressOf MapCharChangeHandler

        SizeGrid()

        'Tell user to choose a map.
        Console.WriteLine("Choose a map to load: ")

        'Find all the map files and print them on console.
        ListMapsOnScreen()

        'Figure out which map they pressed enter on.
        'TODO make a variable to take path and send to loadfile function.
        Dim SelectedFile As String
        SelectedFile = FigureOutWhichMapSelected()

        'MsgBox("The function FigureOutWhichMapSelected returned the string : " & SelectedFile)
        'TODO: make a variable to hold the map, and have loadfile function fill the variable.

        'TODO: print the map on the screen and begin game
        'The filename is stored in SelectedFile, so just need to open it
        'OpenMap(SelectedFile)
        Map.OpenFromFile(SelectedFile)

        'Make a player to be me.

        Player1.TypeAnimal = Animal.AnType.Player
        Player1.Left = 0
        Player1.Top = 2
        Player1.Symbol = "8"
        Player1.HitPoints = Animal.GetMaxHitPoints(Animal.AnType.Player)

        'Have to read map first, so do that.
        'Move cursor on screen.
        'Console.SetCursorPosition(63, 19)
        'Print the 'treasure' on the screen
        'Console.Write("~")

        'Move cursor on screen.
        'Console.SetCursorPosition(50, 23)
        'Print the 'treasure' on the screen
        'Console.Write("$$$")
        'Make the cursor into an @ sign
        'Console.CursorVisible = False
        'Move the cursor back to starting position.
        Console.SetCursorPosition(0, 0)
        Console.CursorVisible = False
        Console.SetBufferSize(MAP_WIDTH, MAP_HEIGHT)
        Console.SetCursorPosition(0, 2)
        'Console.Write("asldklfjaskldjflasdkfsl") 'TODO: show blake diffeence between write and writeline and how when you press enter key no character appears but it's invisible
        While True 'Loop to read any key pressed by user.

            Dim ReadInput As ConsoleKeyInfo 'Private variable to hold the key user pressed.
            ReadInput = Console.ReadKey(True) 'Save the key the user pressed.
            RespondToKeyPress(ReadInput)

            If MoveCount Mod 8 = 0 Then
                GenerateMonster()
            End If
            'If the user lands on the location of the 'treasure', write a message:
            'If Console.CursorLeft = 63 And Console.CursorTop = 19 Then
            '    Console.Write("You found it!")
            'End If

            'If the user lands on the location of the 'treasure', write a message:
            'If Console.CursorLeft = 50 And Console.CursorTop = 23 Then
            '    Console.Write("You found more treasure!")
            'End If

            'Debug.Print("Cursor position: " & Console.CursorLeft & ", " & Console.CursorTop)

        End While

        'TODO: need to store map in memory because we don't want to reveal the entire map at beginning.
        'but at first just get it working
    End Sub '

    Private Sub MapUpdateHandler()
        'Map has been updated, redraw to screen
        ConsoleController.DrawWholeMapFromArray(Map.mGrid)
        'Console.SetCursorPosition(Player1.Left, Player1.Top)
    End Sub

    Private Sub MapCharChangeHandler(iLeft As Integer, iTop As Integer, sChar As String)
        'Only write the specific char that changed
        Dim iSavedLeft As Integer = Console.CursorLeft
        Dim iSavedTop As Integer = Console.CursorTop
        Console.SetCursorPosition(iLeft, iTop)
        Console.Write(sChar)
        Console.SetCursorPosition(iSavedLeft, iSavedTop)
    End Sub

    Structure Coordinates
        Dim Top As Integer
        Dim Left As Integer
    End Structure

    Function GetNewCoordinates(ByVal Coords As Coordinates, ByVal direction As Integer) As Coordinates
        Dim NewLeft As Integer
        Dim NewTop As Integer
        Select Case direction
            Case Animal.Directions.Up
                NewLeft = Coords.Left 'Stays the sacoords moving up.
                NewTop = Coords.Top - 1 'One up from current position.
            Case Animal.Directions.UpRight
                NewLeft = Coords.Left + 1 'Moving one right from current position.
                NewTop = Coords.Top - 1 'One up from current position.
            Case Animal.Directions.Right
                NewLeft = Coords.Left + 1 'Moving one right from current.
                NewTop = Coords.Top  'Not moving vertically, top stays sacoords.
            Case Animal.Directions.DownRight
                NewLeft = Coords.Left + 1 'Moving one right from current.
                NewTop = Coords.Top + 1 'Moving one DOWN from current.
            Case Animal.Directions.Down
                NewLeft = Coords.Left 'Not moving horizontally, left stays sacoords.
                NewTop = Coords.Top + 1 'Moving one DOWN from current.
            Case Animal.Directions.DownLeft
                NewLeft = Coords.Left - 1 'Moving one left from current.
                NewTop = Coords.Top + 1 'One DOWN from current position.
            Case Animal.Directions.Left
                NewLeft = Coords.Left - 1 'Moving one left horizontally.
                NewTop = Coords.Top  'Not moving vertically, top stays sacoords.
            Case Animal.Directions.UpLeft
                NewLeft = Coords.Left - 1 'Moving one left.
                NewTop = Coords.Top - 1 'One up from current position.
        End Select

        Dim NewCoords As New Coordinates
        NewCoords.Top = NewTop
        NewCoords.Left = NewLeft

        Return NewCoords
    End Function

    Public Function GetDistance(ByVal Anim As Coordinates, ByVal Player As Coordinates) As Integer
        Dim iLeft As Integer
        Dim iTop As Integer
        iLeft = Anim.Left - Player.Left
        iTop = Anim.Top - Player.Top
        iLeft = Math.Abs(iLeft)
        iTop = Math.Abs(iTop)

        Dim iDistance As Integer
        iDistance = iLeft + iTop
        iDistance = Math.Abs(iDistance)

        Return iDistance
    End Function
    Public Event PlayerMoved() 'insert params for where he moved? 

    Private Sub PlayerMoveHandler()
        'Fire anything that happens after a player has moved.
        'Map.ClearMessage()
        Dim rnd As New Random
        Dim Dead As Animal = Nothing
        'Dim iX As Integer = 0
        For Each mon As Animal In Monsters
            If mon.TypeAnimal = Animal.AnType.DeadAnimal Then
                Dead = mon
            End If

            If mon.IsPet Then
                Dim NewDirection As Integer
                Dim Distance As Integer = 100
                Dim NewCoords As Coordinates
                Dim iMaxTries As Integer = 20
                Dim iClosestDistance As Integer = 1000
                Dim iClosestDirection As Integer = 1000

                Do Until Distance < 10
                    NewDirection = rnd.Next(0, 7)
                    Dim CurrentCoords As Coordinates
                    CurrentCoords.Left = mon.Left
                    CurrentCoords.Top = mon.Top
                    NewCoords = GetNewCoordinates(CurrentCoords, NewDirection)
                    Dim MeCoords As Coordinates
                    MeCoords.Left = Player1.Left
                    MeCoords.Top = Player1.Top
                    Distance = GetDistance(NewCoords, MeCoords)
                    If Distance < iClosestDistance Then
                        iClosestDistance = Distance
                        iClosestDirection = NewDirection
                    End If

                    iMaxTries = iMaxTries - 1
                    If iMaxTries <= 0 Then 'bail
                        NewDirection = iClosestDirection
                        Exit Do
                    End If
                Loop

                mon.Move(NewDirection)

            Else 'not a pet
                If mon.Move(CType(rnd.Next(0, 7), Animal.AnType)) = True Then
                    If mon.AnimalsAround > 3 Then 'attack
                        mon.ReactToWhatIsNearby()
                    End If
                End If
            End If



            If mon.IsPet Then
                'Attack the other animals around pet
                mon.ReactToWhatIsNearby()
            End If

            SideWriteMessage(mon.TypeAnimal.ToString & mon.Index & " at: L" & mon.Left & " T" & mon.Top)
            'iX += 1
        Next

        If Dead IsNot Nothing Then
            Monsters.Remove(Dead)
        End If

        Map.WriteMessage(Monsters.Count.ToString & " monsters on map.")
        If Player1.TypeAnimal = Animal.AnType.DeadAnimal Then
            'How do we die?
            MsgBox("You died")
            End
        End If
        'MapUpdateHandler()
        Console.SetCursorPosition(Player1.Left, Player1.Top)
    End Sub

    Function RespondToKeyPress(ByVal ReadInput As ConsoleKeyInfo) As ConsoleKey
        'Debug.Print("You are over a: " & Map.mGrid(Console.CursorLeft, Console.CursorTop))
        Select Case ReadInput.Key
            Case ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.W, ConsoleKey.A, ConsoleKey.S, ConsoleKey.D, ConsoleKey.Q, ConsoleKey.E, ConsoleKey.Z, ConsoleKey.X, ConsoleKey.Spacebar, ConsoleKey.T
                'Any movement, raise move event.
                Select Case ReadInput.Key
                    Case ConsoleKey.UpArrow, ConsoleKey.W
                        If Player1.Move(Animal.Directions.Up) = True Then
                            'MapUpdateHandler()
                        Else
                            Beep()
                        End If
                    Case ConsoleKey.LeftArrow, ConsoleKey.A
                        If Player1.Move(Animal.Directions.Left) = True Then
                            'MapUpdateHandler()
                        Else
                            Beep()
                        End If
                    Case ConsoleKey.DownArrow, ConsoleKey.S
                        If Player1.Move(Animal.Directions.Down) = True Then
                            'MapUpdateHandler()
                        Else
                            Beep()
                        End If
                    Case ConsoleKey.RightArrow, ConsoleKey.D
                        If Player1.Move(Animal.Directions.Right) = True Then
                            'MapUpdateHandler()
                        Else
                            Beep()
                        End If
                    Case ConsoleKey.Q 'Up-Left
                        If Player1.Move(Animal.Directions.UpLeft) = True Then
                            'MapUpdateHandler()
                        Else
                            Beep()
                        End If
                    Case ConsoleKey.E 'Up-right
                        If Player1.Move(Animal.Directions.UpRight) = True Then
                            'MapUpdateHandler()
                        Else
                            Beep()
                        End If
                    Case ConsoleKey.Z 'Down left
                        If Player1.Move(Animal.Directions.DownLeft) = True Then
                            'MapUpdateHandler()
                        Else
                            Beep()
                        End If
                    Case ConsoleKey.X 'Down right
                        If Player1.Move(Animal.Directions.DownRight) = True Then
                            'MapUpdateHandler()
                        Else
                            Beep()
                        End If
                    Case ConsoleKey.Spacebar
                        Player1.AttemptHit(GetNearbyAnimal(Player1))
                    Case ConsoleKey.T
                        Player1.AttemptTame(GetNearbyAnimal(Player1))

                End Select 'End select of any button press that causes a move.

                MoveCount = MoveCount + 1
                RaiseEvent PlayerMoved() 'Raise this when player moves, these others below no.

                If ReadInput.Key <> ConsoleKey.Spacebar Then 'unless fighting, heal maybe
                    Player1.Heal() 'Maybe should put the heal in the move function of animal class?
                End If

            Case ConsoleKey.I 'Inventory
            Case ConsoleKey.E 'Eat
                'Case ConsoleKey.T 'Throw

            Case Else 'Some other key

                'Was going to do a select case here, any movement raises a move event and go ahead and move
                'the guy, saving the guys last position and then update the map in memory, every map update triggers
                'redraw of the screen, if he's now over unpassable terrain bump him back to original position and
                'not allow throw of other events associated with a move except message.
        End Select
        'Console.Write("@")
        'Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop)
        'TODO: To make sure our character is displayed instead of the cursor, at form load need to 
        'set cursor.visible to false.
        'Raise an event saying that cursor has moved, event handler will write the symbol and clean the last.
        'In the event, if the cursor has moved to a wall or something, beep and put it back where it was, variable to hold last postion needed.
        'In this function, switch to using this nested case statements.
        'Select Case ReadInput.Key
        '    Case ConsoleKey.DownArrow, ConsoleKey.UpArrow
        '        Debug.Print("movement")
        '        Select Case ReadInput.Key
        '            Case ConsoleKey.DownArrow
        '                Debug.Print("down")
        '            Case ConsoleKey.UpArrow
        '                Debug.Print("up")
        '        End Select
        'End Select
    End Function

    'Sub WriteAt(ByVal x As Integer, ByVal y As Integer, ByVal color As ConsoleColor, ByVal text As String)
    '    Console.SetCursorPosition(x, y)
    '    Console.ForegroundColor = color
    '    Console.Write(text)
    '    Console.ResetColor()
    'End Sub
    'SAVE THIS FOR SOMETHING ELSE
    'MsgBox(ReadInput.KeyChar)
    'If ReadInput.KeyChar = "k" Then
    '    If MsgBox("Are you sure you want to kill the little dog?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '        Console.Write("You killed the little dog!")
    '    Else
    '        Console.Write("The dog gives your hand a sloppy lick! Best friends for life.")
    '        Console.Beep()
    '    End If
    'End If
    Dim FolderList As New List(Of String)

    Private Sub ListMapsOnScreen()
        'look in the folder and print out all the maps we find.
        'where is the folder? \\LCD\Users\Public\Pictures\Programming\BlakesRogueGame\BlakesRogueGame\
        'What are the files named like? They have the .map extension.

        FolderList = System.IO.Directory.GetFiles("\\LCD\Users\Public\Pictures\Programming\BlakesRogueGame\BlakesRogueGame").ToList
        'Console.Write(FolderList)
        '0-map1
        '1-map2
        '2-hemanmap

        'For Each FileName As String In FolderList
        For iIndex As Integer = 0 To FolderList.Count - 1
            If FolderList.Item(iIndex).Contains(".map") Then         'FileName.Contains(".map") Then
                Console.Write("-" & iIndex & " " & System.IO.Path.GetFileName(FolderList.Item(iIndex)) & vbCr & vbLf)
            Else
                'Do not print this does nothing herel aldfkladlkfjaldkf
            End If
        Next

    End Sub

    Private Function FigureOutWhichMapSelected() As String
        While True 'Loop to read any key pressed by user.

            Dim ReadInput As ConsoleKeyInfo 'Private variable to hold the key user pressed.
            ReadInput = Console.ReadKey(True) 'Save the key the user pressed.

            'MsgBox("You chose: " & ReadInput.KeyChar.ToString)

            If IsNumeric(ReadInput.KeyChar.ToString) Then
                'MsgBox("Great you chose a number")
                Console.Write("You chose map: " & FolderList.Item(CInt(ReadInput.KeyChar.ToString)))
                Return FolderList.Item(CInt(ReadInput.KeyChar.ToString))
            Else
                MsgBox("Are you retarded?")
                Return "This guy is retarded"
            End If

        End While
    End Function
    Dim ConsoleArray(Console.WindowWidth, Console.WindowHeight) As String

    Private Sub OpenMap(SelectedFile As String)
        'Open the file and print it on the console screen
        Dim File As New System.IO.StreamReader(SelectedFile)


        'So the file looks like this:
        '...------....
        '...|....|....

        'In math, you have x,y coordinates, on windows forms apps, you have Left,Top coordinates.
        'So Left-0, Top-0 would be the leftmost, topmost spot, or the dot in the top left corner
        'The higher the numbers get, the further away from the left side and the top you get

        'To copy the file to the array, we start with the first character in the file, which will be 0,0, the second will be 0,1 and so on...
        'when we get to the next line, the first character will be 1,0 the next 1,1 and so on.
        Console.Clear()

        'How do we go through the file?
        'We want to print one line on the screen for each line in the file.
        'Here is another loop.
        'In other loops, we went through by number, or we went through a list For Each...
        'Here we do something until a condition is met. 
        'The condition should be when we reach the end of the file.
        'File was our file variable, it has many options.
        Dim iLeft As Integer
        Dim iTop As Integer
        Do Until File.EndOfStream 'In the old days we called this EOF - End of File.
            Dim sLine As String = File.ReadLine
            'Trim(sLine) THIS ALSO NOT WORK
            'sLine = sLine.Replace(vbCr, "")
            'sLine = sLine.Replace(vbLf, "")
            sLine = sLine.Replace(vbNewLine, "")
            Debug.Print("Length of sline: " & sLine)
            'ReDim Preserve ConsoleArray(Len(sLine), ConsoleArray.GetUpperBound(1)) 'Cannot redim the first dimension of array?
            For iLeft = 0 To Len(sLine) - 1
                If iLeft > 52 Then
                    Debug.Print("found")
                End If
                Debug.Print(sLine.Substring(iLeft, 1))
                If iLeft <= ConsoleArray.GetUpperBound(0) And iTop <= ConsoleArray.GetUpperBound(1) Then 'NONE OF THESE WORK And sLine.Substring(iLeft, 1) <> vbNewLine And sLine.Substring(iLeft, 1) <> vbCr And sLine.Substring(iLeft, 1) <> vbLf Then
                    ConsoleArray(iLeft, iTop) = sLine.Substring(iLeft, 1)
                End If
            Next
            iTop = iTop + 1
        Loop
        ReDim Preserve ConsoleArray(ConsoleArray.GetUpperBound(0), iTop)

        WriteMapToConsole()
    End Sub

    Private Sub WriteMapToConsole()
        For iTop = 0 To ConsoleArray.GetUpperBound(1)
            For iLeft = 0 To ConsoleArray.GetUpperBound(0)
                If ConsoleArray(iLeft, iTop) = vbNewLine Or ConsoleArray(iLeft, iTop) = vbCr Or ConsoleArray(iLeft, iTop) = vbLf Then
                    Debug.Print("Left: " & iLeft & " Top: " & iTop & "Array pulls: " & ConsoleArray(iLeft, iTop))
                End If

                Console.Write(ConsoleArray(iLeft, iTop))
            Next
            Console.Write(vbNewLine)
        Next
    End Sub

    'Junk not used:

    'From file read loop, apparently File.Read is really hard to work with, and file.peek idk what newline looks like so cant use that.
    'If iLeft <= ConsoleArray.GetUpperBound(0) And iTop <= ConsoleArray.GetUpperBound(1) And Chr(File.Peek) <> vbNewLine Then
    '    ConsoleArray(iLeft, iTop) = Chr(File.Read)
    'End If

    'If iLeft > 52 Then
    '    Debug.Print("found")
    'End If
    'iLeft = iLeft + 1
    'If Chr(File.Peek) = vbNewLine Then
    'iTop = iTop + 1
    'iLeft = 0
    'End If

    Private Sub GenerateMonster()
        'Make a new monster
        Dim Mon As New Animal
        Mon.Index = Monsters.Count - 1
        Dim rnd As New Random

        Do Until Map.mGrid(Mon.Left, Mon.Top) = "."
            Mon.Top = rnd.Next(2, MAP_HEIGHT)
            Mon.Left = rnd.Next(0, MAP_WIDTH)
        Loop

        Mon.StandingOver = Map.mGrid(Mon.Left, Mon.Top)
        Debug.Print(rnd.Next(0, 4)) 'Monster enum
        Dim iLastAnimalType As Integer
        iLastAnimalType = [Enum].GetValues(GetType(Animal.AnType)).Length
        iLastAnimalType = iLastAnimalType - 2

        Mon.TypeAnimal = CType(rnd.Next(0, iLastAnimalType), Animal.AnType)
        Mon.HitPoints = Animal.GetMaxHitPoints(Mon.TypeAnimal)

        Monsters.Add(Mon)
        Map.WriteMessage("You sense a new prescense.")
    End Sub

    Public Function GetNearbyAnimal(Mee As Animal) As Animal
        'Loop through list, looking for any animal right next to.
        For Each mon In Monsters
            If mon IsNot Mee Then
                If Mee.Left - mon.Left <= 1 And mon.Left - Mee.Left <= 1 Then
                    'They are within one to the left, checck top
                    If Mee.Top - mon.Top <= 1 And mon.Top - Mee.Top <= 1 Then
                        'Ok nearby
                        Return mon

                        'To do: Make the tamed pets not kill themselves.
                    End If
                End If
            End If
        Next

        Return Nothing
    End Function




End Module
