Public Class CoD
	
	Dim Controls As New Controls
	Dim TileSystem As New TileSystem
	Dim MapParser As New MapParser
	Dim cCon As New cCon
	Dim PortalHandler As New PortalHandler
	
	Sub CoDInit(x As Integer, y As Integer)

        'Priprava mapy

		buffercounter = 0

        MapParser.ReadMap("maps\CoD.map")

        'Nacteni textu

		cCon.WriteInit()
		
		Player.X = x
		Player.Y = y

        WriteToBuffer("It is time to end it now.")
        WriteToBuffer("So this is the place, where all that evil comes from.")
        WriteToBuffer("""Cathedral of Death""")

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
        	cCon.WriteNPC(1,5)
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
