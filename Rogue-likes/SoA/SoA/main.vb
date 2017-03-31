Imports System.IO
Module main
    'Initialize Variables
    Public MAP_WIDTH As Integer = 64
    Public MAP_HEIGHT As Integer = 20

    Public nMapArray(MAP_WIDTH, MAP_HEIGHT) As Integer
    
    'Engine
    
    Dim Controls As New Controls
    Dim MapParser As New MapParser
    Dim Readinput As ConsoleKeyInfo
    Dim cCon As New cCon
    Dim PortalHandler As New PortalHandler
    Dim FightHandler As New FightHandler
    
    Public BufferCounter As Integer = 0
    
    'Pseudo Databaze
    
    Dim TileSystem As New TileSystem
    Dim ItemSystem As New ItemSystem
    Dim NPCSystem As New NPCSystem
    Dim DialogueSystem As New DialogueSystem
    Dim ShopSystem As New ShopSystem
    
    Public Loot(1) As Byte
    
    'Sprava oken
    
    Public WindowOpen As Boolean = true
    
    'Mapy
    
    Dim MainMenu As New MainMenu
    Dim CharacterCreation As New CharacterCreation
    Dim CoA As New CoA
    Dim CoAD As New CoAD
    Dim FoA As New FoA
    Dim DF As New DF
    Dim DFD As New DFD
    Dim CoD As New CoD
    
    'Tvorba Entity Hrace
    
    Public Player As New PlayerHandler("Drenni", "Succubu", "Female", 0, "&", ConsoleColor.Yellow, 32, 18, 20, 20, 10, 10, 0, 1, 1, 3, 3, 3, 3, 25)

	#Region "Array Section"
	
	'Tiles
	
    Public Tiles(20) As TILE_TYPE

    Public Structure TILE_TYPE
        Dim ID As Integer
        Dim Symbol As Char
        Dim Name As String
        Dim Color As ConsoleColor
        Dim Background As ConsoleColor
        Dim Passable As Boolean
    End Structure
    
    'Items
    
    Public Items(8,20) As ITEM_TYPE
    
    Public Structure ITEM_TYPE
    	Dim ID As Integer
    	Dim Name As String
    	Dim Color As ConsoleColor
    	Dim Price As Integer
    	Dim Armor As Integer
    	Dim MinDmg As Integer
    	Dim MaxDmg As Integer
    	Dim HP As Integer
    	Dim MP As Integer
    	Dim Str As Integer
    	Dim Dex As Integer
    	Dim Int As Integer
    	Dim Wis As Integer
    End Structure
    
    'NPCs
    
    Public NPC(3, 29) As NPC_TYPE
    
    Public Structure NPC_TYPE
    	Dim ID As Integer
    	Dim Name As String
    	Dim X As Byte
    	Dim Y As Byte
    	Dim Symbol As String
    	Dim Color As ConsoleColor
    	Dim HP As Integer
    	Dim MP As Integer
    	Dim MinDmg As Integer
    	Dim MaxDmg As Integer
    	Dim Armor As Integer
    	Dim Dex As Integer
    	Dim ItemID As Byte
    	Dim ItemType As Byte
    	Dim DialogueID As Byte
    	Dim MapID As Byte
    End Structure
    
    'Dialogues
    
    Public Dialogues(39) As DIALOGUE_TYPE
    
    Public Structure DIALOGUE_TYPE
    	Dim ID As Integer
    	Dim ShopID As Byte
    	Dim Heal As Integer
    	Dim Dialogue_0 As String
    	Dim Dialogue_1 As String
    	Dim Dialogue_2 As String
    	Dim Dialogue_3 As String
    	Dim Dialogue_4 As String
    	Dim Dialogue_5 As String
    	Dim Dialogue_6 As String
    	Dim Dialogue_7 As String
    	Dim Dialogue_8 As String
    	Dim Dialogue_9 As String
    End Structure
    
    'Shops
    
    Public Shops(8) As SHOP_TYPE
    
    Public Structure SHOP_TYPE
    	Dim ID As Integer
    	Dim ItemID_0 As Byte
    	Dim ItemType_0 As Byte
    	Dim ItemID_1 As Byte
    	Dim ItemType_1 As Byte
    	Dim ItemID_2 As Byte
    	Dim ItemType_2 As Byte
    	Dim ItemID_3 As Byte
    	Dim ItemType_3 As Byte
    	Dim ItemID_4 As Byte
    	Dim ItemType_4 As Byte
    	Dim ItemID_5 As Byte
    	Dim ItemType_5 As Byte
    	Dim ItemID_6 As Byte
    	Dim ItemType_6 As Byte
    	Dim ItemID_7 As Byte
    	Dim ItemType_7 As Byte
    	Dim ItemID_8 As Byte
    	Dim ItemType_8 As Byte
    End Structure
    
    'Equiment
    
    Public Equipment(6) As EQUIPMENT_TYPE
    
    Public Structure EQUIPMENT_TYPE
    	Dim SlotType As Integer
    	Dim ItemID As Integer
    End Structure
    
    'Inventory
    
    Public Inventory(15) As INVENTORY_TYPE
    
    Public Structure INVENTORY_TYPE
    	Dim ItemType As Integer
    	Dim ItemID As Integer
    	Dim Amount As Integer
    End Structure
    
	#End Region
	
	'Ovladani zmeny map
	
	Public InPortal As Boolean
	Public PortalInf(2) As Byte
	Public LRM As String
	
	'MainGame
	
	Sub Main()
		
		'Formatovani Obrazovky
		
		cCon.Initialize() 
		
		'Nacteni Databazi
		
        TileSystem.InitializeTiles()
        ItemSystem.InitializeItems()
        NPCSystem.InitializeNPCs()
        DialogueSystem.InitializeDialogues()
        ShopSystem.InitializeShops()
        InitializeInventory()
        
        'Nastaveni spawnu hrace
        
        PortalInf(1) = Player.X
        PortalInf(2) = Player.Y
        
        'Spusteni map a zmeny
        
        MainMenu.MainMenu()
        CharacterCreation.CreatingCharacter()
        Console.BackgroundColor = ConsoleColor.Black
        Console.Clear()
        
        While True
        	
        	Select Case PortalInf(0)
        		Case 0
        			CoA.CoAInit(PortalInf(1), PortalInf(2))
        		Case 1
        			FoA.FoAInit(PortalInf(1), PortalInf(2))
        		Case 2
        			DF.DFInit(PortalInf(1), PortalInf(2))
        		Case 3
        			DFD.DFDInit(PortalInf(1), PortalInf(2))
        		Case 4
        			CoD.CoDInit(PortalInf(1), PortalInf(2))
        		Case 5
        			CoAD.CoADInit(PortalInf(1), PortalInf(2))
        	End Select
        	
        End While
        
    End Sub
    
    'Vykresleni mapy
    
    Sub PutTiles(ByVal x As Integer, ByVal y As Integer)

        Dim TilesUpperBound As Integer = UBound(Tiles)
        Dim TileNum As Integer = nMapArray(y, x)

        For n = 0 To TilesUpperBound
            If Tiles(n).ID = tilenum Then
                cCon.WriteTiles(x, y, Tiles(n).Color, Tiles(n).Background, Tiles(n).Symbol)
                Exit For
            End If
        Next

    End Sub
    
    'Detekce moznosti pohybu
    
    Function CollisionDetect(ByVal x As Integer, ByVal y As Integer)

        If x <= MAP_WIDTH And y <= MAP_HEIGHT And x >= 0 And y >= 0 Then
			Dim TileCheck As Integer = nMapArray(y, x)
            If Tiles(TileCheck).Passable = False Then
                Return True
            End If
        End If
        
        Return False

    End Function
    
    Function EntityDetect(X As Integer, Y As Integer) as Boolean
        	
        For i As Integer = 0 To 2
        	For j As Integer = 0 To 29
        		If NPC(i, j).MapID = PortalInf(0) And NPC(i, j).HP > 0 Then
        			If X = NPC(i, j).X And Y = NPC(i, j).Y Then
        				Return True
        			End If
        		End If
        	Next
        Next
        	
        Return False
        	
    End Function
    
    Sub HostileDetect()
    	
    	For i As Integer = 0 To 29
    		If NPC(3,i).MapID = PortalInf(0) And NPC(3,i).HP > 0 Then
    			If (((Player.X + 1 = NPC(3,i).X) And (Player.Y = NPC(3,i).Y)) Or ((Player.X - 1 = NPC(3,i).X) And (Player.Y = NPC(3,i).Y)) Or ((Player.X = NPC(3,i).X) And (Player.Y + 1 = NPC(3,i).Y)) Or ((Player.X = NPC(3,i).X) And (Player.Y - 1 = NPC(3,i).Y))) Then
    				FightHandler.FightInitialization(3, i)
    			End If
    		End If
    	Next
    	
    End Sub
    
    'Obnoveni pole na mape
    
    Sub UpdateTile(ByVal x As Integer, ByVal y As Integer, ByVal tileid As Integer)
        nMapArray(y, x) = tileid
        PutTiles(x, y)
    End Sub

    'Jakoby faktorial uvedeneho cisla

    Function Faktor(val As Integer) As Integer
    	Dim Result As integer
    	For i As Integer = 1 To val + 1
    		Result = Result + i
    	Next
    	Return Result
    End Function
    
    Dim ClrString As String = "                                                                               "
    Dim TempString As String = ""
    Dim TempString2 As String = ""
    
    Sub WriteToBuffer(ByVal S As String)

        If BufferCounter = 0 Then

            cCon.WriteAt(1, 21, ConsoleColor.White, ClrString)
            cCon.WriteAt(1, 22, ConsoleColor.White, Clrstring)
            cCon.WriteAt(1, 23, ConsoleColor.White, Clrstring)
            cCon.WriteAt(1, 21, ConsoleColor.White, S)

            BufferCounter = BufferCounter + 1
            TempString = S

            Exit Sub
        End If

        If BufferCounter = 1 Then
            cCon.WriteAt(1, 21, ConsoleColor.White, ClrString)

            cCon.WriteAt(1, 22, ConsoleColor.White, TempString)

            cCon.WriteAt(1, 21, ConsoleColor.White, s)

            TempString2 = TempString
            TempString = S

            BufferCounter = BufferCounter + 1
            Exit Sub
        End If

        If BufferCounter = 2 Then
            cCon.WriteAt(1, 21, ConsoleColor.White, ClrString)
            cCon.WriteAt(1, 22, ConsoleColor.White, ClrString)

            cCon.WriteAt(1, 21, ConsoleColor.White, S)
            cCon.WriteAt(1, 22, ConsoleColor.White, TempString)
            cCon.WriteAt(1, 23, ConsoleColor.White, TempString2)

            BufferCounter = 0
            Exit Sub
        End If
    End Sub
    
    Sub InitializeInventory()
    	
    	For i As Integer = 0 To 15
    		Inventory(i).ItemType = 0
    		Inventory(i).ItemID = 0
    	Next
    	
    	For i As Integer = 0 To 6
    		Equipment(i).SlotType = i + 1
    		Equipment(i).ItemID = 0
    	Next
    	
    End Sub
    
End Module
