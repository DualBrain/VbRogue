Public Class DF
	
	Dim Controls As New Controls
	Dim TileSystem As New TileSystem
	Dim MapParser As New MapParser
	Dim cCon As New cCon
	Dim PortalHandler As New PortalHandler
	
	Sub DFInit(x As Integer, y As Integer)

        'Priprava mapy
        
        buffercounter = 0
        
        MapParser.ReadMap("maps\DF.map")

        'Nacteni textu

		cCon.WriteInit()
		
		WriteToBuffer("However, that ruined cathedral over there looks interesting.")
		WriteToBuffer("Now i should be a little bit more careful...")
		WriteToBuffer("""Dusk Forest""")
		
		Player.X = x
		Player.Y = y
		
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
        	cCon.WriteNPC(1,3)
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
