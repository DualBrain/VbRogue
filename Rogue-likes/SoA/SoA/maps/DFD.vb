Public Class DFD
	
	Dim Controls As New Controls
    Dim TileSystem As New TileSystem
    Dim MapParser As New MapParser
    Dim cCon As New cCon
    Dim PortalHandler As New PortalHandler
    
    Sub DFDInit(x As Integer, y As Integer)
    	
    	'Priprava mapy
    	
    	buffercounter = 0
    	
    	main.Tiles(1).Background = ConsoleColor.DarkGray
    	main.Tiles(1).Color = ConsoleColor.DarkGray
    	main.Tiles(7).Background = ConsoleColor.Black
    	
    	MapParser.ReadMap("maps\DFD.map")
    	
    	
    	'Nacteni textu
    	
        cCon.WriteInit()
		WriteToBuffer("God, if this was Minecraft...")
		WriteToBuffer("I would never guess there are that many tunnels underground.")
		WriteToBuffer("""Dusk Forest Dungeon""")
        
        'Nastaveni spawnu hrace
        
        Player.X = x
        Player.Y = y
        
        'Hra na mape
        
        While True
        	
        	If Player.HP = 0 Then
        		Player.HP = 1
        		PortalInf(0) = 0
        		PortalInf(1) = 32
        		PortalInf(2) = 18
				main.Tiles(1).Background = ConsoleColor.DarkRed
				main.Tiles(1).Color = ConsoleColor.DarkRed
    			main.Tiles(7).Background = ConsoleColor.Green
        		Console.Clear()
        		Exit Sub
        	End If
        	
        	'Vykresleni vykonu
        	cCon.WriteNPC(1,4)
			cCon.WritePlayer()
			
            'Vstup
            Controls.Checkinput()
            
            'Vykonani
            If InPortal = true Then
				PortalHandler.MovePortal()
				main.Tiles(1).Background = ConsoleColor.DarkRed
				main.Tiles(1).Color = ConsoleColor.DarkRed
    			main.Tiles(7).Background = ConsoleColor.Green
				Return
			End If
            
        End While
        
    
    End Sub
End Class
