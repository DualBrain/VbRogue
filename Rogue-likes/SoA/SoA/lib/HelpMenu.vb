Public Class HelpMenu
	
	Dim cCon As New cCon
	
	Sub Help()
		
		cCon.WriteHelp()
		
		Console.ReadKey(True)
		
		For i As Integer = 0 To 64
			For j As Integer = 0 To 20
				cCon.WriteTiles(i, j, Tiles(nMapArray(j, i)).Color, Tiles(nMapArray(j, i)).Background, Tiles(nMapArray(j, i)).Symbol)
			Next
    	Next

		cCon.WriteInit()
		
		WindowOpen = False
	End Sub
	
End Class
