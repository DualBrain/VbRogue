Module Map
    Private mWidth As Integer

    Public Property Width() As Integer
        Get
            Return mWidth
        End Get
        Set(ByVal value As Integer)
            mWidth = value
        End Set
    End Property

    Private mHeight As Integer
    Public Property Height() As Integer
        Get
            Return mHeight
        End Get
        Set(ByVal value As Integer)
            mHeight = value
        End Set
    End Property

    Public mGrid(0, 0) As String

    Public Function SizeGrid(Optional ByVal iWidth As Integer = 0, Optional ByVal iHeight As Integer = 0) As Boolean
        If iWidth = 0 Or iHeight = 0 Then
            'Use standard width, height.
            ReDim mGrid(MAP_WIDTH, MAP_HEIGHT)
            Height = MAP_HEIGHT
            Width = MAP_WIDTH
        Else 'used passed numbers.
            ReDim mGrid(iWidth, iHeight)
            Height = iHeight
            Width = iWidth
        End If
        Debug.Print("Map resized to: " & mGrid.GetUpperBound(0) & " Height: " & mGrid.GetUpperBound(1))
    End Function

    Public Event MapUpdated()
    Public Event MapCharChanged(iLeft As Integer, iTop As Integer, sChar As String)

    Public Function Write(iLeft As Integer, iTop As Integer, sChar As String) As Boolean
        'Writes one character to grid at specified position
        'Check inputs
        If sChar Is Nothing OrElse sChar.Length > 1 OrElse sChar.Length < 1 OrElse sChar = "" Then 'Only one char at a time bub
            Return False
        End If

        If iLeft >= Map.mWidth Or iTop >= Map.mHeight Then
            Return False
        End If

        'Inputs ok, write to map in memory.
        mGrid(iLeft, iTop) = sChar
        'RaiseEvent MapUpdated() 'Can't do this here is going off repeatedly
        RaiseEvent MapCharChanged(iLeft, iTop, sChar)
    End Function

    Property WriteBuffer As String

    Public Function WriteMessage(sMessage As String) As Boolean
        Map.ClearMessage()
        'Writes a message down on the last 2 rows...
        Dim iRow1 As Integer = MAP_HEIGHT - 5
        Dim iMaxLength As Integer = MAP_WIDTH - 4
        Dim iCurRow As Integer = iRow1
        Dim iSavedLeft As Integer = Console.CursorLeft
        Dim iSavedTop As Integer = Console.CursorTop

        'Clear buffer.
        Dim iX As Integer
        If WriteBuffer IsNot Nothing AndAlso WriteBuffer.Length > 0 Then
            Dim Wbuffer() As String = Split(WriteBuffer, vbNewLine)
            Dim iLast As Integer = Wbuffer.GetUpperBound(0)

            WriteBuffer = ""
            For iX = 2 To 4
                If iX - 1 <= UBound(Wbuffer) And iX - 1 >= LBound(Wbuffer) Then
                    WriteBuffer = WriteBuffer & Wbuffer(iX - 1) & vbNewLine
                End If

            Next
        End If

        WriteBuffer = WriteBuffer & sMessage & vbNewLine
        Dim Wbuff() As String = Split(WriteBuffer, vbNewLine)
        For iX = 0 To 4
            If iX <= UBound(Wbuff) And iX >= LBound(Wbuff) Then
                Console.SetCursorPosition(0, iRow1 + iX)
                Console.Write(Wbuff(iX))
            End If
        Next

        Console.SetCursorPosition(iSavedLeft, iSavedTop)
    End Function

    Public SideWriteBuffer() As String

    Public Function SideWriteMessage(sMessage As String) As Boolean
        Dim iMaxTop = MAP_HEIGHT - 4
        Dim iMaxLength As Integer = 20 ' MAP_WIDTH - 4
        Dim iSavedLeft As Integer = Console.CursorLeft
        Dim iSavedTop As Integer = Console.CursorTop

        Dim iX As Integer

        For iX = iMaxTop To 0 Step -1
            If SideWriteBuffer Is Nothing OrElse UBound(SideWriteBuffer) < iMaxTop Then 'init
                ReDim SideWriteBuffer(0 To iMaxTop)
            End If

            'Ok otherwise need to eject last and move all forward
            If iX > LBound(SideWriteBuffer) Then
                SideWriteBuffer(iX) = SideWriteBuffer(iX - 1)
            Else 'last put in msg
                sMessage = Left(sMessage, iMaxLength)
                SideWriteBuffer(iX) = sMessage
            End If
        Next

        Dim sBlanks As New String(" ", iMaxLength)

        For iX = 0 To UBound(SideWriteBuffer)
            Console.SetCursorPosition(MAP_WIDTH - iMaxLength, iX + 2)
            Console.Write(sBlanks)
            Console.SetCursorPosition(MAP_WIDTH - iMaxLength, iX + 2)
            Console.Write(SideWriteBuffer(iX))
        Next

        Console.SetCursorPosition(iSavedLeft, iSavedTop)


        'Map.ClearSideMessage()
        'Writes a message down on the last 2 rows...
        'Dim iRow1 As Integer = 1 'MAP_HEIGHT - 5

        'Dim iCurRow As Integer = iRow1

        ''Clear buffer.

        'If SideWriteBuffer IsNot Nothing AndAlso SideWriteBuffer.Length > 0 Then
        '    Dim Wbuffer() As String = Split(SideWriteBuffer, vbNewLine)
        '    Dim iLast As Integer = Wbuffer.GetUpperBound(0)

        '    SideWriteBuffer = ""
        '    For iX = MAP_HEIGHT - UBound(Wbuffer) - 2 To UBound(Wbuffer) Step -1
        '        If iX - 1 <= UBound(Wbuffer) And iX - 1 >= LBound(Wbuffer) Then
        '            SideWriteBuffer = SideWriteBuffer & Wbuffer(iX - 1) & vbNewLine
        '        End If

        '    Next
        'End If

        'SideWriteBuffer = SideWriteBuffer & Left(sMessage, iMaxLength) & vbNewLine
        'Dim Wbuff() As String = Split(SideWriteBuffer, vbNewLine)
        'For iX = 0 To UBound(Wbuff)
        '    If iX <= UBound(Wbuff) And iX >= LBound(Wbuff) Then
        '        Console.SetCursorPosition(MAP_WIDTH - 20, iRow1 + iX)
        '        Console.Write(Wbuff(iX))
        '    End If
        'Next


    End Function

    Public Function ClearMessage() As Boolean
        Dim iRow1 As Integer = MAP_HEIGHT - 5
        Dim iSavedLeft As Integer = Console.CursorLeft
        Dim isavedtop As Integer = Console.CursorTop
        Dim sBlanks As New String(" ", MAP_WIDTH - 1)
        Dim ix As Integer = 5
        For ix = 4 To 0 Step -1
            Console.SetCursorPosition(0, iRow1 + ix)
            Console.Write(sBlanks)
        Next
        'Console.SetCursorPosition(0, iRow1)
        'Console.Write(sBlanks)
        'Console.SetCursorPosition(0, iRow1 + 1)
        'Console.Write(sBlanks)
        Console.SetCursorPosition(iSavedLeft, isavedtop)

    End Function

    Public Function OpenFromFile(ByVal sFileName As String) As Boolean
        'Always validate passed parameters (inputs)
        If sFileName = "" Or Not System.IO.File.Exists(sFileName) Then
            Debug.Print("Invalid filename: " & sFileName)
            Return False
        End If

        'Copied from 'OpenMap' function in main:
        'Open the file and put it into map grid memory.
        Dim File As New System.IO.StreamReader(sFileName)
        'So the file looks like this:
        '...------....
        '...|....|....
        'In math, you have x,y coordinates, on windows forms apps, you have Left,Top coordinates.
        'So Left-0, Top-0 would be the leftmost, topmost spot, or the dot in the top left corner
        'The higher the numbers get, the further away from the left side and the top you get
        'To copy the file to the array, we start with the first character in the file, which will be 0,0, the second will be 0,1 and so on...
        'when we get to the next line, the first character will be 1,0 the next 1,1 and so on.
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
            For iLeft = 0 To Len(sLine) - 1
                If iLeft <= mGrid.GetUpperBound(0) And iTop <= mGrid.GetUpperBound(1) Then 'NONE OF THESE WORK And sLine.Substring(iLeft, 1) <> vbNewLine And sLine.Substring(iLeft, 1) <> vbCr And sLine.Substring(iLeft, 1) <> vbLf Then
                    mGrid(iLeft, iTop) = sLine.Substring(iLeft, 1)
                End If
            Next
            iTop = iTop + 1
        Loop

        RaiseEvent MapUpdated()
        Return True
    End Function
End Module
