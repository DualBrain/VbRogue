Public Class ShopBuyHandler

    Dim cCon As New cCon
    Dim WindowControls As New WindowControls

    Sub InitializeShopBuyHandler(ByVal ShopID As Byte)

        cCon.WriteBuyShop(ShopID)

		Dim Star As Byte = 0

		WriteToBuffer("Buying menu. Enter - Buy item / Esc - Exit")

		While True
			
			cCon.WriteAt(3, 2 + 2 * Star, ConsoleColor.Yellow, "*")
			
			WindowControls.ShopBuyInput(Star, ShopID)
			
			If WindowOpen = False Then
				Exit While
			End If
			
        End While

    End Sub
End Class
