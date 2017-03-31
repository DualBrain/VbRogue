Public Class ShopSellHandler

'Tato trida se zabyva ovladanim, vykreslovanim a managementem hracova inventare

	Dim cCon As New cCon
	Dim MapParser As New MapParser
	Dim WindowControls As New WindowControls
	
	Sub InitializeShopSellHandler()
		
		WindowOpen = True
		
		Dim Star As Byte = 0
		
		cCon.WriteInventory()
		
		WriteToBuffer("Selling menu. Enter - Sell item / Esc - Exit")
		
		While True
			
			If Star < 8 Then
				cCon.WriteAt(3, 2 + 2 * Star, ConsoleColor.Yellow, "*")
			Elseif Star < 16 Then
				cCon.WriteAt(33, 2 + 2 * (Star - 8), ConsoleColor.Yellow, "*")
			End If
			
			WindowControls.ShopSellInput(Star)
			
			If WindowOpen = False Then
				Exit While
			End If
			
		End While
	End Sub
	
End Class
