Public Class BuyHandler
	
	Public Class DialogueHandler
	
	Dim cCon As New cCon
	Dim MapParser As New MapParser
	Dim WindowControls As New WindowControls
	
	Sub DialogueScreen(NPCType As Byte, NPCID As Byte)
		
		WindowOpen = true
		cCon.WriteDialogue(NPC(NPCType,NPCID).DialogueID)
		
		While True
			
			WindowControls.DialogueInput()
			
			
			
			If WindowOpen = False Then
				Exit While
			End If
			
		End While
		
		
		For i As Integer = 0 To 64
			For j As Integer = 0 To 20
				cCon.WriteTiles(i, j, Tiles(nMapArray(j, i)).Color, Tiles(nMapArray(j, i)).Background, Tiles(nMapArray(j, i)).Symbol)
			Next
		Next
		
	End Sub
	
End Class

	
End Class
