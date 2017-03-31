Public Class CharacterScreenHandler

    'Tato trida se zabyva oknem s atributy hrace

	Dim cCon As New cCon
	Dim MapParser As New MapParser
	Dim WindowControls As New WindowControls
	
	Sub CharacterScreen()
		
		WindowOpen = true
		cCon.WriteCharacterScreen()
		
		WriteToBuffer("Character menu. Esc,C - Exit")
		
		While True
			
			WindowControls.CharacterInput()
			
			If WindowOpen = False Then
				Exit While
			End If
			
		End While
		
		For i As Integer = 0 To 64
			For j As Integer = 0 To 20
				cCon.WriteTiles(i, j, Tiles(nMapArray(j, i)).Color, Tiles(nMapArray(j, i)).Background, Tiles(nMapArray(j, i)).Symbol)
			Next
        Next

        cCon.WriteInit()
		
	End Sub
	
End Class
