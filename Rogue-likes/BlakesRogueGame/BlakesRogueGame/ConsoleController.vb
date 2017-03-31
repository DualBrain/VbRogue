Module ConsoleController
    'Some methodes to control, write to, the console.
    Public Sub DrawWholeMapFromArray(Optional ByRef mGrid(,) As String = Nothing)
        If mGrid Is Nothing Then 'use main
            mGrid = Map.mGrid
        End If

        Console.Clear()
        Dim iLeft As Integer
        Dim iTop As Integer
        For iTop = 0 To mGrid.GetUpperBound(1) 'Start at top row and dow each
            For iLeft = 0 To mGrid.GetUpperBound(0) 'Start at left and go right
                Console.Write(mGrid(iLeft, iTop))
            Next
            Console.Write(vbNewLine)
        Next
        Debug.Print("Map redrawn")
    End Sub


End Module
