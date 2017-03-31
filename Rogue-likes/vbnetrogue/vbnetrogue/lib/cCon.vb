Public Class cCon

    'Consolidate some code. Let me write at a certain X, Y, with what color I want and what text I want.
    Dim buffercounter As Integer = 0
    Dim clrstring As String = "                                                                               "
    Dim tempstring As String = ""
    Dim tempstring2 As String = ""

    Sub WriteAt(ByVal x As Integer, ByVal y As Integer, ByVal color As ConsoleColor, ByVal text As String)
        Console.SetCursorPosition(x, y)
        Console.ForegroundColor = color
        Console.Write(text)
        Console.ResetColor()
    End Sub

    Sub Initialize()
        Console.CursorVisible = False
        Console.SetBufferSize(80, 25)
    End Sub

    Sub WriteToBuffer(ByVal s As String)
      
        'Creates a message buffer, prints on first line, then second line, then third, then clears them. Begins again.

        If buffercounter = 0 Then

            'Clear all 3 lines.
            WriteAt(1, 21, ConsoleColor.White, clrstring)
            WriteAt(1, 22, ConsoleColor.White, clrstring)
            WriteAt(1, 23, ConsoleColor.White, clrstring)
            WriteAt(1, 21, ConsoleColor.White, s)

            buffercounter = buffercounter + 1
            tempstring = s

            Exit Sub
        End If

        If buffercounter = 1 Then
            'Clear out first line
            WriteAt(1, 21, ConsoleColor.White, clrstring)

            'Move line 1 down to line 2
            WriteAt(1, 22, ConsoleColor.White, tempstring)

            'Write new line on line 1
            WriteAt(1, 21, ConsoleColor.White, s)

            tempstring2 = tempstring
            tempstring = s

            buffercounter = buffercounter + 1
            Exit Sub
        End If

        If buffercounter = 2 Then
            'Clear out the top string
            WriteAt(1, 21, ConsoleColor.White, clrstring)
            WriteAt(1, 22, ConsoleColor.White, clrstring)

            'Write our three messages out in order
            WriteAt(1, 21, ConsoleColor.White, s)
            WriteAt(1, 22, ConsoleColor.White, tempstring)
            WriteAt(1, 23, ConsoleColor.White, tempstring2)

            'Reset our buffer counter.
            buffercounter = 0
            Exit Sub
        End If
    End Sub

End Class
