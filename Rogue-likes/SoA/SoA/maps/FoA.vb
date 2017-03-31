Public Class FoA
	
	Dim Controls As New Controls
	Dim TileSystem As New TileSystem
	Dim MapParser As New MapParser
	Dim cCon As New cCon
	Dim PortalHandler As New PortalHandler
	
	Sub FoAInit(x As Integer, y As Integer)

        'Priprava mapy

		buffercounter = 0

        MapParser.ReadMap("maps\FoA.map")

        'Nacteni textu

		cCon.WriteInit()
		
		Player.X = x
		Player.Y = y

        WriteToBuffer("Noone can believe anyone this day.")
        WriteToBuffer("Poeple say this place is still relatively safe.")
        WriteToBuffer("""Forest of Armheim""")

		While True
			
        	If Player.HP = 0 Then
        		Player.HP = 1
        		PortalInf(0) = 0
        		PortalInf(1) = 32
        		PortalInf(2) = 18
        		Console.Clear()
        		Exit Sub
        	End If
        	
        	'Vykresleni vykonu
        	cCon.WriteNPC(3,3)
        	cCon.WriteNPC(3,4)
        	cCon.WriteNPC(2,7)
        	cCon.WriteNPC(2,8)
        	cCon.WriteNPC(2,9)
            cCon.WritePlayer()
			
			'Vstup
			Controls.Checkinput()
			
			'Vykonani
			If InPortal = True Then
				PortalHandler.MovePortal()
				Return
			End If
			
		End While
		
	End Sub
	
End Class