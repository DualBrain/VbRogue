Public Class GameMenu

    'Tato trida se zabyva hernim menu po stisknuti klavesy Esc

	Dim cCon As New cCon
	Dim WindowControls As New WindowControls
	Dim CharacterScreenHandler As New CharacterScreenHandler
	Dim InventoryHandler As New InventoryHandler
	Dim HelpMenu As New HelpMenu
	
	Sub GameMenu
		
		Dim Star As Byte = 0
		Dim KeyPres As Boolean = False
		
		cCon.WriteGameMenu(Star)
		WindowOpen = True
		
		WriteToBuffer("Game menu. Enter - Choose option / Esc - Exit menu")
		
		While true
			
		cCon.WriteAt(26, 7 + 2 * Star, ConsoleColor.Yellow, "*")
			
		WindowControls.GameMenuInput(Star, KeyPres)
		
		If KeyPres = True Then
			If Star = 0 Then
				CharacterScreenHandler.CharacterScreen()
			ElseIf Star = 1 Then
				InventoryHandler.InventoryHandler()
			ElseIf Star = 2 Then
				HelpMenu.Help()
			ElseIf Star = 3 Then
				Environment.Exit(0)
			End If
		End If
		
		KeyPres = false
		
		If WindowOpen = False Then
			For i As Integer = 23 To 42
				For j As Integer = 5 To 15
					cCon.WriteTiles(i, j, Tiles(nMapArray(j, i)).Color, Tiles(nMapArray(j, i)).Background, Tiles(nMapArray(j, i)).Symbol)
				Next
			Next
			Exit While
		End If
		End while
		
	End Sub
	
End Class
