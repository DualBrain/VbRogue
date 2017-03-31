Imports System.IO

Public Class MapParser
    Sub ReadMap(ByVal filename As String)

        Dim num_rows As Long
        Dim num_cols As Long
        Dim x As Integer
        Dim y As Integer


        'Check if file exist
        If File.Exists(filename) Then


            Dim tmpstream As StreamReader = File.OpenText(filename)
            Dim strlines() As String
            Dim strline() As String

            'Load content of file to strLines array
            strlines = tmpstream.ReadToEnd().Split(Environment.NewLine)

            ' Redimension the array.
            num_rows = UBound(strlines)
            strline = strlines(0).Split(",")
            num_cols = UBound(strline)
            ReDim nMapArray(num_rows, num_cols)


            ' Copy the data into the array.
            For x = 0 To num_rows
                strline = strlines(x).Split(",")
                For y = 0 To num_cols
                    nMapArray(x, y) = strline(y)
                Next
            Next

            For y = 0 To MAP_HEIGHT
                For x = 0 To MAP_WIDTH
                    Console.SetCursorPosition(x, y) ' Set our cursor to the appropriate position.
                    PutTiles(x, y) ' Print the appropriate Tile
                Next
            Next
        End If

    End Sub
End Class
