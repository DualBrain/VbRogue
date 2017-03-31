Public Class InventoryHandler

    'Tato trida se zabyva ovladanim, vykreslovanim a managementem hracova inventare

	Dim cCon As New cCon
	Dim MapParser As New MapParser
	Dim WindowControls As New WindowControls
	
	Sub InventoryHandler()
		
		WindowOpen = True
		
		Dim Star As Byte = 0
		
		cCon.WriteInventory()
		
		WriteToBuffer("Inventory. Esc,I - Exit / Enter - Use / Del - Delete / Space - Examine") 
		
		While True
			
			If Star < 8 Then
				cCon.WriteAt(3, 2 + 2 * Star, ConsoleColor.Yellow, "*")
			Elseif Star < 16 Then
				cCon.WriteAt(33, 2 + 2 * (Star - 8), ConsoleColor.Yellow, "*")
			End If
			
			WindowControls.InventoryCheckInput(Star)
			
			If WindowOpen = False Then
				Exit While
			End If
			
			cCon.WriteStats()
			
		End While
		
		Dim MinDmg, MaxDmg As Integer
		
		For i As Integer = 0 To 6
			MinDmg = MinDmg + Items(Equipment(i).SlotType, Equipment(i).ItemID).MinDmg
			MaxDmg = MaxDmg + Items(Equipment(i).SlotType, Equipment(i).ItemID).MaxDmg
		Next
		
		Player.MinDmg = Math.Ceiling(MinDmg * (((Player.Str ^ 2) / (Math.Sqrt(Player.Str))/25)+1))
		Player.MaxDmg = Math.Ceiling(MaxDmg * (((Player.Str ^ 2) / (Math.Sqrt(Player.Str))/25)+1))
		
		For i As Integer = 0 To 64
			For j As Integer = 0 To 20
				cCon.WriteTiles(i, j, Tiles(nMapArray(j, i)).Color, Tiles(nMapArray(j, i)).Background, Tiles(nMapArray(j, i)).Symbol)
			Next
        Next

        cCon.WriteInit()
		
	End Sub
	
End Class
