Public Class CoA
	
	Dim Controls As New Controls
    Dim TileSystem As New TileSystem
    Dim MapParser As New MapParser
    Dim cCon As New cCon
    Dim PortalHandler As New PortalHandler
    
    Sub CoAInit(x As Integer, y As Integer)
    	
    	'Priprava mapy
    	
    	BufferCounter = 0
    	MapParser.ReadMap("maps\CoA.map")
    	
    	'Nacteni textu
    	
        cCon.WriteInit()
        WriteToBuffer("....people were right, this castle is almost dead....")
        WriteToBuffer("After further examination, you are able to read: ""Castle of Armheim"".")
        WriteToBuffer("On the way in, you see a small sign covered in blood.")
        
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
        		Console.Clear()
        		Exit Sub
        	End If
        	
			'Vykresleni vykonu
			cCon.WriteNPC(0,0)
			cCon.WriteNPC(1,0)
			cCon.WriteNPC(1,1)
			cCon.WriteNPC(1,2)
			cCon.WriteNPC(0,1)
			cCon.WriteNPC(2,0)
			cCon.WriteNPC(2,1)
			cCon.WriteNPC(3,0)
            cCon.WritePlayer()
			
            'Vstup
            Controls.Checkinput()
            
            'Vykonani
            
            HostileDetect()
            
            If InPortal = true Then
				PortalHandler.MovePortal()
				Return
			End If
            
    	End While
    
    End Sub
End Class
