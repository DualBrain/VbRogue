Public Class CoAD
	
	Dim Controls As New Controls
    Dim TileSystem As New TileSystem
    Dim MapParser As New MapParser
    Dim cCon As New cCon
    Dim PortalHandler As New PortalHandler
    
    Sub CoADInit(x As Integer, y As Integer)
    	
    	'Priprava mapy
    	
    	BufferCounter = 0
    	
    	main.Tiles(1).Background = ConsoleColor.DarkGray
    	main.Tiles(1).Color = ConsoleColor.DarkGray
    	main.Tiles(7).Background = ConsoleColor.Black
    	
    	MapParser.ReadMap("maps\CoAD.map")
    	
    	
    	'Nacteni textu
    	
        cCon.WriteInit()
        WriteToBuffer("I expected something...better...more dangerous.")
        WriteToBuffer("Only a small room?")
        WriteToBuffer("""Armheim Dungeon""")
        
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
        	cCon.WriteNPC(2,2)
        	cCon.WriteNPC(2,3)
        	cCon.WriteNPC(2,4)
        	cCon.WriteNPC(2,5)
        	cCon.WriteNPC(2,6)
        	cCon.WriteNPC(3,1)
        	cCon.WriteNPC(3,2)
            cCon.WritePlayer()
			
            'Vstup
            Controls.Checkinput()
            
            'Vykonani
            
            HostileDetect()
            
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