Imports System.IO

'Trida se zabyva nacitanim mapy z textoveho souboru a jejim zapsanim do pole a do konzole

Public Class MapParser
	
    Sub ReadMap(ByVal FileName As String)

        Dim num_rows As Long
        Dim num_cols As Long
        Dim x As Integer
        Dim y As Integer
        
        LRM = FileName

        If File.Exists(FileName) Then


            Dim TmpStream As StreamReader = File.OpenText(FileName)
            Dim StrLines() As String
            Dim StrLine() As String

            StrLines = TmpStream.ReadToEnd().Split(Environment.NewLine)

            num_rows = UBound(StrLines)
            StrLine = StrLines(0).Split(",")
            num_cols = UBound(StrLine)
            ReDim nMapArray(num_rows, num_cols)

            For x = 0 To num_rows
                StrLine = StrLines(x).Split(",")
                For y = 0 To num_cols
                    nMapArray(x, y) = StrLine(y)
                Next
            Next

            For y = 0 To MAP_HEIGHT
                For x = 0 To MAP_WIDTH
                    Console.SetCursorPosition(x, y)
                    PutTiles(x, y)
                Next
            Next
        End If

    End Sub
End Class
