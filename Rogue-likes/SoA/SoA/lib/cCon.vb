Public Class cCon

    'Tato trida ovlada vpisovani textu do konzole. Kazda procedura ovlada nejake vlozeni textu/pozadi

    Sub WriteAt(ByVal x As Integer, ByVal y As Integer, ByVal color As ConsoleColor, ByVal text As String)
        Console.SetCursorPosition(x, y)
        Console.ForegroundColor = color
        Console.Write(text)
        Console.ResetColor()
    End Sub

    Sub WriteTiles(ByVal X As Integer, ByVal Y As Integer, ByVal Color As ConsoleColor, ByVal Background As ConsoleColor, ByVal text As String)
        Console.SetCursorPosition(X, Y)
        Console.ForegroundColor = Color
        Console.BackgroundColor = Background
        Console.Write(text)
        Console.ResetColor()
    End Sub

    Sub Initialize()
        Console.CursorVisible = False
        Console.SetBufferSize(80, 25)
    End Sub
    
    Sub WriteInit()
    	WriteChar()
    	WriteStats()
        WriteEquipment()
        WriteGold()
    End Sub

	Sub WritePlayer()
			WriteTiles(Player.X, Player.Y, Player.Color, Tiles(nMapArray(Player.Y, Player.X)).Background, Player.Symbol)
    End Sub
    
    Sub WriteChar()
    	WriteAt(66,1,ConsoleColor.White, "HP = ")
    	WriteAt(66,2,ConsoleColor.White, "MP = ")
    	WriteAt(66,3,ConsoleColor.White, "Str = ")
    	WriteAt(66,4,ConsoleColor.White, "Dex = ")
    	WriteAt(66,5,ConsoleColor.White, "Int = ")
        WriteAt(66,6, ConsoleColor.White, "Wis = ")
        WriteAt(66,17, ConsoleColor.White, "Gold = ")
    End Sub
    
    Sub WriteStats()
    	
    	WriteAt(71, 1, ConsoleColor.Green, "   ")
    	WriteAt(71, 2, ConsoleColor.Green, "   ")
    	
        WriteAt(72, 3, ConsoleColor.Yellow, "   ")
        WriteAt(72, 4, ConsoleColor.Yellow, "   ")
        WriteAt(72, 5, ConsoleColor.Yellow, "   ")
        WriteAt(72, 6, ConsoleColor.Yellow, "   ")
    	
    	If Player.HP > (Player.HPMax * 0.75) Then
    		WriteAt(71, 1, ConsoleColor.Green, Player.HP)
    	Else If Player.HP > (Player.HPMax * 0.25) Then
    		WriteAt(71, 1, ConsoleColor.Yellow, Player.HP)
    	Else
    		WriteAt(71, 1, ConsoleColor.Red, Player.HP)
    	End If
    	If Player.MP > (Player.MPMax * 0.75) Then
    		WriteAt(71, 2, ConsoleColor.Green, Player.MP)
    	Else If Player.MP > (Player.MPMax * 0.25) Then
    		WriteAt(71, 2, ConsoleColor.Yellow, Player.MP)
    	Else
    		WriteAt(71, 2, ConsoleColor.Red, Player.MP)
    	End If
        WriteAt(72, 3, ConsoleColor.Yellow, Player.Str)
        WriteAt(72, 4, ConsoleColor.Yellow, Player.Dex)
        WriteAt(72, 5, ConsoleColor.Yellow, Player.Int)
        WriteAt(72, 6, ConsoleColor.Yellow, Player.Wis)
    End Sub

    Sub WriteGold()
        WriteAt(73, 17, ConsoleColor.White, "    ")
        WriteAt(73, 17, ConsoleColor.Yellow, Player.Gold)
    End Sub
    
    Sub WriteNPC(Type As Integer, ID As Integer)
    	If NPC(Type, ID).HP > 0 Then
    		WriteTiles(main.NPC(Type, ID).X, main.NPC(Type, ID).Y, main.NPC(Type, ID).Color, Tiles(nMapArray(NPC(Type, ID).Y, NPC(Type, ID).X)).Background, main.NPC(Type, ID).Symbol)
    	End If
    End Sub
    
    Sub WriteEquipment()
    	
    	For i As Integer = 0 To 6
    		If main.Items(main.Equipment(i).SlotType, main.Equipment(i).ItemID).ID > 0 Then
    			WriteAt(66, 9+i, ConsoleColor.White, "              ")
    			WriteAt(66, 9+i, main.Items(main.Equipment(i).SlotType, main.Equipment(i).ItemID).Color, main.Items(main.Equipment(i).SlotType, main.Equipment(i).ItemID).Name)
    		End If
    	Next
    	
    End Sub
    
    Private Sub WriteBorder()
    	
    	For i As Integer = 0 To 64
    		For j As Integer = 0 To 20
    			If i = 0 Or i = 64 Or j = 0 Or j = 20 Then
    				WriteAt(i, j, ConsoleColor.White, "#")
    			Else
    				WriteAt(i, j, ConsoleColor.White, " ")
    			End if
    		Next
    	Next
    	
    End Sub
    
    Sub WriteInventory()
    	
    	WriteBorder()
    	
    	For i As Integer = 0 To 15
    		If i < 8 Then
    			WriteAt(2, 2 + 2 * i, ConsoleColor.White, "( ) " & i + 1 & " -")
    			If  Inventory(i).ItemID > 0 then
    				WriteAt(10, 2 + 2 * i, Items(Inventory(i).ItemType, Inventory(i).ItemID).Color, Items(Inventory(i).ItemType, Inventory(i).ItemID).Name)
    			End If
    		Else
    			WriteAt(32, 2 + 2 * (i - 8), ConsoleColor.White, "( ) " & i + 1 & " -")
    			If  Inventory(i).ItemID > 0 then
    				WriteAt(41, 2 + 2 * (i - 8), Items(Inventory(i).ItemType, Inventory(i).ItemID).Color, Items(Inventory(i).ItemType, Inventory(i).ItemID).Name)
    			End If
    		End If
    	Next
    	
    	If Items(Loot(0), Loot(1)).ID > 0 then
    		WriteAt(2, 18, Items(Loot(0), Loot(1)).Color, "Loot - " & Items(Loot(0), Loot(1)).Name)
    		WriteAt(32, 18, ConsoleColor.Yellow, "L - Take Loot")
    	End If
    	
    End Sub
    
    Sub WriteDialogue(ID As Integer)
    	
    	WriteBorder()
    	
    	WriteInit()
    	
    	If Dialogues(ID).Heal > 0 Then
    		WriteDialogueHealer(ID)
    		Exit Sub
    	Else If Dialogues(ID).ShopID > 0 Then
    		WriteDialogueShop(ID)
    		Exit Sub
    	End If
    	
    End Sub
    
    Private Sub WriteDialogueHealer(ID As Integer)
    	
    	WriteAt(2, 2, ConsoleColor.White, Dialogues(ID).Dialogue_0)
    	WriteAt(2, 4, ConsoleColor.White, Dialogues(ID).Dialogue_1)
    	If Player.Gold < 5 Then
    		WriteAt(2, 6, ConsoleColor.White, Dialogues(ID).Dialogue_6)
    		Exit Sub
    	Else
    		WriteAt(2, 6, ConsoleColor.White, Dialogues(ID).Dialogue_2)
    		If Player.HP = Player.HPMax Then
    			WriteAt(2, 8, ConsoleColor.White, Dialogues(ID).Dialogue_3)
    			Return
    		Else
    			WriteAt(2, 8, ConsoleColor.White, Dialogues(ID).Dialogue_4)
    			WriteAt(2, 10, ConsoleColor.White, Dialogues(ID).Dialogue_5)
    			Player.HP = Player.HP + Dialogues(ID).Heal
    			If Player.HP > Player.HPMax Then
    				Player.HP = Player.HPMax
                End If
    			Player.MP = Player.MP + Math.Ceiling(Dialogues(ID).Heal / 2)
    			If Player.MP > Player.MPMax Then
    				Player.MP = Player.MPMax
                End If
                Player.Gold = Player.Gold - 5
    			Return
    		End If
    	End If
    	
    End Sub
    
    Sub WriteDialogueShop(ID)

        WriteAt(2, 2, ConsoleColor.White, Dialogues(ID).Dialogue_0)
        WriteAt(2, 4, ConsoleColor.White, Dialogues(ID).Dialogue_1)
        WriteAt(2, 6, ConsoleColor.White, "( ) - Buy      ( ) - Sell")

    End Sub

    Sub WriteBuyShop(ByVal ShopID As Byte)

        WriteBorder()

        For i As Byte = 0 To 8
            WriteAt(2, 2 + (i * 2), ConsoleColor.White,  "( ) -")
        Next
        
        If Shops(ShopID).ItemType_0 > 0 Or Shops(ShopID).ItemID_0 > 0 Then
        	WriteAt(8, 2, Items(Shops(ShopID).ItemType_0, Shops(ShopID).ItemID_0).Color, Items(Shops(ShopID).ItemType_0, Shops(ShopID).ItemID_0).Name)
        	WriteAt(28, 2, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_0, Shops(ShopID).ItemID_0).Price & " Gold")
        End If
        
        If Shops(ShopID).ItemType_1 > 0 Or Shops(ShopID).ItemID_1 > 0 Then
        	WriteAt(8, 4, Items(Shops(ShopID).ItemType_1, Shops(ShopID).ItemID_1).Color, Items(Shops(ShopID).ItemType_1, Shops(ShopID).ItemID_1).Name)
        	WriteAt(28, 4, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_1, Shops(ShopID).ItemID_1).Price & " Gold")
        End If
        
        If Shops(ShopID).ItemType_2 > 0 Or Shops(ShopID).ItemID_2 > 0 Then
        	WriteAt(8, 6, Items(Shops(ShopID).ItemType_2, Shops(ShopID).ItemID_2).Color, Items(Shops(ShopID).ItemType_2, Shops(ShopID).ItemID_2).Name)
        	WriteAt(28, 6, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_2, Shops(ShopID).ItemID_2).Price & " Gold")
        End If
        
        If Shops(ShopID).ItemType_3 > 0 Or Shops(ShopID).ItemID_3 > 0 Then
        	WriteAt(8, 8, Items(Shops(ShopID).ItemType_3, Shops(ShopID).ItemID_3).Color, Items(Shops(ShopID).ItemType_3, Shops(ShopID).ItemID_3).Name)
        	WriteAt(28, 8, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_3, Shops(ShopID).ItemID_3).Price & " Gold")
        End If
        
        If Shops(ShopID).ItemType_4 > 0 Or Shops(ShopID).ItemID_4 > 0 Then
        	WriteAt(8, 10, Items(Shops(ShopID).ItemType_4, Shops(ShopID).ItemID_4).Color, Items(Shops(ShopID).ItemType_4, Shops(ShopID).ItemID_4).Name)
        	WriteAt(28, 10, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_4, Shops(ShopID).ItemID_4).Price & " Gold")
        End If
        
        If Shops(ShopID).ItemType_5 > 0 Or Shops(ShopID).ItemID_5 > 0 Then
        	WriteAt(8, 12, Items(Shops(ShopID).ItemType_5, Shops(ShopID).ItemID_5).Color, Items(Shops(ShopID).ItemType_5, Shops(ShopID).ItemID_5).Name)
        	WriteAt(28, 12, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_5, Shops(ShopID).ItemID_5).Price & " Gold")
        End If
        
        If Shops(ShopID).ItemType_6 > 0 Or Shops(ShopID).ItemID_6 > 0 Then
        	WriteAt(8, 14, Items(Shops(ShopID).ItemType_6, Shops(ShopID).ItemID_6).Color, Items(Shops(ShopID).ItemType_6, Shops(ShopID).ItemID_6).Name)
        	WriteAt(28, 14, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_6, Shops(ShopID).ItemID_6).Price & " Gold")
        End If
        
        If Shops(ShopID).ItemType_7 > 0 Or Shops(ShopID).ItemID_7 > 0 Then
        	WriteAt(8, 16, Items(Shops(ShopID).ItemType_7, Shops(ShopID).ItemID_7).Color, Items(Shops(ShopID).ItemType_7, Shops(ShopID).ItemID_7).Name)
        	WriteAt(28, 16, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_7, Shops(ShopID).ItemID_7).Price & " Gold")
        End If
        
        If Shops(ShopID).ItemType_8 > 0 Or Shops(ShopID).ItemID_8 > 0 Then
        	WriteAt(8, 18, Items(Shops(ShopID).ItemType_8, Shops(ShopID).ItemID_8).Color, Items(Shops(ShopID).ItemType_8, Shops(ShopID).ItemID_8).Name)
			WriteAt(28, 18, ConsoleColor.White, " - " & Items(Shops(ShopID).ItemType_8, Shops(ShopID).ItemID_8).Price & " Gold")
        End If
        
    End Sub
    
    Sub WriteFight(NPCType, NPCID)
    	
    	WriteBorder()
    	
    	WriteAt(2, 2, ConsoleColor.White, "Name : " & NPC(NPCType, NPCID).Name)
    	WriteAt(2, 4, ConsoleColor.White, "HP :   " & NPC(NPCType, NPCID).HP)
    	WriteAt(2, 6, ConsoleColor.White, "MP :   " & NPC(NPCType, NPCID).MP)
    	WriteAt(2, 8, ConsoleColor.White, "Dex :  " & NPC(NPCType, NPCID).Dex)
    	
    	WriteAt(32, 2, ConsoleColor.White, "MinDmg :  " & NPC(NPCType, NPCID).MinDmg)
    	WriteAt(32, 4, ConsoleColor.White, "MaxDmg :  " & NPC(NPCType, NPCID).MaxDmg)
    	WriteAt(32, 6, ConsoleColor.White, "Armor :   " & NPC(NPCType, NPCID).Armor)
    	WriteAt(32, 8, ConsoleColor.White, "Carries : " & Items(NPC(NPCType, NPCID).ItemType, NPC(NPCType, NPCID).ItemID).Name)
    	
    	WriteAt(10, 16, ConsoleColor.Yellow, "F - Attack")
    	WriteAt(26, 16, ConsoleColor.Yellow, "B - Block")
    	WriteAt(42, 16, ConsoleColor.Yellow, "M - Magic")
    	WriteAt(10, 18, ConsoleColor.Yellow, "Q - Quick")
    	WriteAt(26, 18, ConsoleColor.Yellow, "H - Heal")
    	WriteAt(42, 18, ConsoleColor.Yellow, "R - Restore MP")
    	
    End Sub
    
    Sub WriteCharacterScreen()
    	
    	WriteBorder()
    	
    	WriteAt(2, 2, ConsoleColor.White, "Name  : " & Player.Name)
    	WriteAt(2, 4, ConsoleColor.White, "Race  : " & Player.Race)
    	WriteAt(2, 6, ConsoleColor.White, "Sex   : " & Player.Sex)
    	WriteAt(2, 8, ConsoleColor.White, "HP    : " & Player.HP & "/" & Player.HPMax)
    	WriteAt(2, 10, ConsoleColor.White, "MP    : " & Player.MP & "/" & Player.MPMax)
        WriteAt(2, 12, ConsoleColor.White, "Armor : " & Player.Armor)
    	WriteAt(2, 14, ConsoleColor.White, "Gold  : " & Player.Gold)
    	
    	WriteAt(22, 2, ConsoleColor.White, "Strength  : " & Player.Str)
    	WriteAt(22, 4, ConsoleColor.White, "Dexterity : " & Player.Dex)
    	WriteAt(22, 6, ConsoleColor.White, "Intellect : " & Player.Int)
    	WriteAt(22, 8, ConsoleColor.White, "Wisdom    : " & Player.Wis)
    	WriteAt(22, 10, ConsoleColor.White, "Min Dmg   : " & Player.MinDmg)
    	WriteAt(22, 12, ConsoleColor.White, "Max Dmg   : " & Player.MaxDmg)
    	
    End Sub
    
    Sub WriteMainMenu()
    	For i As Integer = 0 To 24
			For j As Integer = 0 To 79
				WriteTiles(j, i, ConsoleColor.White, ConsoleColor.White, " ")
			Next
		Next
	
		WriteTiles(2, 1, ConsoleColor.Black, ConsoleColor.White, "          .                                                      .")
		WriteTiles(2, 2, ConsoleColor.Black, ConsoleColor.White, "        .n                   .                 .                  n.")
		WriteTiles(2, 3, ConsoleColor.Black, ConsoleColor.White, "  .   .dP                  dP                   9b                 9b.    .")
		WriteTiles(2, 4, ConsoleColor.Black, ConsoleColor.White, " 4    qXb         .       dX                     Xb       .        dXp     t")
		WriteTiles(2, 5, ConsoleColor.Black, ConsoleColor.White, "dX.    9Xb      .dXb    __                         __    dXb.     dXP     .Xb")
		WriteTiles(2, 6, ConsoleColor.Black, ConsoleColor.White, "9XXb._       _.dXXXXb dXXXXbo.                 .odXXXXb dXXXXb._       _.dXXP")
		WriteTiles(2, 7, ConsoleColor.Black, ConsoleColor.White, " 9XXXXXXXXXXXXXXXXXXXVXXXXXXXXOo.           .oOXXXXXXXXVXXXXXXXXXXXXXXXXXXXP")
		WriteTiles(2, 8, ConsoleColor.Black, ConsoleColor.White, "  `9XXXXXXXXXXXXXXXXXXXXX´~   ~`OOO8b   d8OOO´~   ~`XXXXXXXXXXXXXXXXXXXXXP´")
		WriteTiles(2, 9, ConsoleColor.Black, ConsoleColor.White, "    `9XXXXXXXXXXXP´ `9XX´          `98v8P´          `XXP´ `9XXXXXXXXXXXP´")
		WriteTiles(29, 9, ConsoleColor.Red, ConsoleColor.White, "SHADOWS")
		WriteTiles(45, 9, ConsoleColor.Red, ConsoleColor.White, "ARMHEIM")
		WriteTiles(2, 10, ConsoleColor.Black, ConsoleColor.White, "        ~~~~~~~       9X.          .db|db.          .XP       ~~~~~~~")
		WriteTiles(2, 11, ConsoleColor.Black, ConsoleColor.White, "                        )b.  .dbo.dP´`v´`9b.odb.  .dX(")
		WriteTiles(2, 12, ConsoleColor.Black, ConsoleColor.White, "                      ,dXXXXXXXXXXXb     dXXXXXXXXXXXb.")
		WriteTiles(2, 13, ConsoleColor.Black, ConsoleColor.White, "  P - Play Game      dXXXXXXXXXXXP´       `9XXXXXXXXXXXb     C - Credits")
		WriteTiles(38, 13, ConsoleColor.Red, ConsoleColor.White, "ov.er")
		WriteTiles(2, 14, ConsoleColor.Black, ConsoleColor.White, "                    dXXXXXXXXXXXXb   d|b   dXXXXXXXXXXXXb")
		WriteTiles(2, 15, ConsoleColor.Black, ConsoleColor.White, "  H - Help          9XXb´   `XXXXXb.dX|Xb.dXXXXX´   `dXXP    Esc - End Game")
		WriteTiles(2, 16, ConsoleColor.Black, ConsoleColor.White, "                     `´      9XXXXXX(   )XXXXXXP      `´")
		WriteTiles(2, 17, ConsoleColor.Black, ConsoleColor.White, "                              XXXX X.`v´.X XXXX")
		WriteTiles(2, 18, ConsoleColor.Black, ConsoleColor.White, "                              XP^X´`b   d´`X^XX")
		WriteTiles(2, 19, ConsoleColor.Black, ConsoleColor.White, "                              X. 9  `   ´  P )X")
		WriteTiles(2, 20, ConsoleColor.Black, ConsoleColor.White, "                              `b  `       ´  d´")
		WriteTiles(2, 21, ConsoleColor.Black, ConsoleColor.White, "                               `             ´")
		WriteTiles(13, 23, ConsoleColor.Black, ConsoleColor.White, "Tip : It is recommended to read Help before first game.")
	
    End Sub
    
    Sub WriteGameMenu(Star)
    	
    	For i As Integer = 23 To 42
    		For j As Integer = 5 To 15
    			If i = 23 Or i = 42 Or j = 5 Or j = 15 then
    				WriteAt(i, j, ConsoleColor.White, "#")
    			Else
    				WriteAt(i, j, ConsoleColor.White, " ")
    			End If
    		Next
    	Next
    	
    	WriteAt(25, 7, ConsoleColor.White, "( ) Character")
    	WriteAt(25, 9, ConsoleColor.White, "( ) Inventory")
    	WriteAt(25, 11, ConsoleColor.White, "( ) Help Page")
    	WriteAt(25, 13, ConsoleColor.White, "( ) Exit Game")
    End Sub
    
    Sub WriteChoosingName()
    	
    	For i As Integer = 0 To 24
			For j As Integer = 0 To 79
				WriteTiles(j, i, ConsoleColor.White, ConsoleColor.White, " ")
			Next
    	Next
    	
    	WriteTiles(20, 13, ConsoleColor.Gray, ConsoleColor.White, "Note : Name can't be longer than 10 characters")
    	WriteTiles(20, 11, ConsoleColor.Black, ConsoleColor.White, "Enter Your name here : ")
    	Console.BackgroundColor = ConsoleColor.White
    	Console.ForegroundColor = ConsoleColor.Black
    	
    End Sub
    
    Sub WriteChoosingGender()
    	
    	For i As Integer = 0 To 2
    		For j As Integer = 0 To 79
    			WriteTiles(j, 11 + i, ConsoleColor.White, ConsoleColor.White, " ")
    		Next
    	Next
    	
    	WriteTiles(28, 11, ConsoleColor.Black, ConsoleColor.White, "  Choose Your Gender")
    	WriteTiles(28, 13, ConsoleColor.Black, ConsoleColor.White, "1 - Male    2 - Female")
    	
    End Sub
    
    Sub WriteChoosingClass()
    	
    	For i As Integer = 0 To 2
    		For j As Integer = 0 To 79
    			WriteTiles(j, 11 + i, ConsoleColor.White, ConsoleColor.White, " ")
    		Next
    	Next
    	
    	WriteTiles(20, 11, ConsoleColor.Black, ConsoleColor.White, "             Choose Your Class")
    	WriteTiles(20, 13, ConsoleColor.Black, ConsoleColor.White, "1 - Paladin    2 - BattleMage    3 - Sorcerer")
    End Sub
    
    Sub WriteChoosingRace()
    	
    	For i As Integer = 0 To 2
    		For j As Integer = 0 To 79
    			WriteTiles(j, 11 + i, ConsoleColor.White, ConsoleColor.White, " ")
    		Next
    	Next
    	
    	WriteTiles(32, 2, ConsoleColor.Black, ConsoleColor.White, "Choose Your Race")
    	WriteTiles(17, 4, ConsoleColor.Black, ConsoleColor.White, "H - Human          Str + 2, Int + 2, HP + 5")
    	WriteTiles(17, 6, ConsoleColor.Black, ConsoleColor.White, "D - Dwarf          Str + 2, Dex + 2, HP + 5")
    	WriteTiles(17, 8, ConsoleColor.Black, ConsoleColor.White, "E - Elf            Int + 2, Wis + 2, Dex + 2")
    	WriteTiles(17, 10, ConsoleColor.Black, ConsoleColor.White, "O - Orc            Str + 4, HP + 5")
    	WriteTiles(17, 12, ConsoleColor.Black, ConsoleColor.White, "T - Troll          Int + 2, Dex + 4")
    	WriteTiles(17, 14, ConsoleColor.Black, ConsoleColor.White, "F - Faerie         Int + 2, Dex + 2, MP + 5")
    	WriteTiles(17, 16, ConsoleColor.Black, ConsoleColor.White, "M - Demon          Str + 2, Dex + 2, MP + 5")
    	WriteTiles(17, 18, ConsoleColor.Black, ConsoleColor.White, "G - Gnome          Int + 2, Wis + 2, MP + 5")
    	WriteTiles(17, 20, ConsoleColor.Black, ConsoleColor.White, "J - Djinn          Wis + 2, MP + 10")
    	WriteTiles(17, 22, ConsoleColor.Black, ConsoleColor.White, "R - Draconid       Dex + 2, HP + 5, MP + 5")
    End Sub
    
    Sub WriteItemInfo(Star As Byte)
    	
    	For i As Integer = 18 To 45
    		For j As Integer = 3 To 17
    			If i = 18 Or i = 45 Or j = 3 Or j = 17 then
    				WriteAt(i, j, ConsoleColor.White, "#")
    			Else
    				WriteAt(i, j, ConsoleColor.White, " ")
    			End If
    		Next
    	Next
    	
    	WriteAt(24, 5, ConsoleColor.White, "Name : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).Name)
    	
    	WriteAt(20, 7, ConsoleCOlor.White, "HP    : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).HP)
    	WriteAt(20, 9, ConsoleColor.White, "MP    : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).MP)
    	WriteAt(20, 11, ConsoleColor.White, "MinDmg: " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).MinDmg)
    	WriteAt(20, 13, ConsoleColor.White, "MaxDmg: " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).MaxDmg)
    	WriteAt(20, 15, ConsoleColor.White, "Armor : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).Armor)
    	
    	WriteAt(32, 7, ConsoleCOlor.White, "Str   : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).Str)
    	WriteAt(32, 9, ConsoleColor.White, "Dex   : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).Dex)
    	WriteAt(32, 11, ConsoleColor.White, "Int   : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).Int)
    	WriteAt(32, 13, ConsoleColor.White, "Wis   : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).Wis)
    	WriteAt(32, 15, ConsoleColor.White, "Price : " & Items(Inventory(Star).ItemType, Inventory(Star).ItemID).Price)
    	
    End Sub
    
    Sub WriteDeathMessage()
    	
    	For i As Integer = 0 To 24
			For j As Integer = 0 To 79
				WriteTiles(j, i, ConsoleColor.White, ConsoleColor.White, " ")
			Next
    	Next
    	
    	WriteTiles(32, 11, ConsoleColor.Red, ConsoleColor.White, "You have died.")
    	WriteTiles(13, 13, ConsoleColor.Black, ConsoleColor.White, "You have been given one more chance. Try harder this time.")
    	
    End Sub
    
    Sub WriteHelp()
    	
    	WriteBorder()
    	
    	WriteAt(2, 2, ConsoleColor.White, "Arrows - Map Movement")
    	WriteAt(2, 4, ConsoleColor.White, "Space  - Map Interaction")
    	WriteAt(2, 6, ConsoleColor.White, "Enter  - NPC Interaction")
    	WriteAt(2, 8, ConsoleColor.White, "C      - Character Screen")
    	WriteAt(2, 10, ConsoleColor.White, "I      - Inventory")
    	WriteAt(2, 12, ConsoleColor.White, "Esc    - Game Menu")
    	
    End Sub
    
    Sub WriteMMHelp1()
    	
    	For i As Integer = 0 To 24
			For j As Integer = 0 To 79
				WriteTiles(j, i, ConsoleColor.White, ConsoleColor.White, " ")
			Next
    	Next
    	
    	WriteTiles(8, 3, ConsoleColor.Black, ConsoleColor.White, "Arrows - Map Movement")
    	WriteTiles(8, 5, ConsoleColor.Black, ConsoleColor.White, "Space  - Map Interaction")
    	WriteTiles(8, 7, ConsoleColor.Black, ConsoleColor.White, "Enter  - NPC Interaction")
    	WriteTiles(8, 9, ConsoleColor.Black, ConsoleColor.White, "Esc    - Game Menu")
    	WriteTiles(8, 11, ConsoleColor.Black, ConsoleColor.White, "I      - Inventory")
    	WriteTiles(8, 13, ConsoleColor.Black, ConsoleColor.White, "C      - Character Screen")
    	WriteTiles(8, 15, ConsoleColor.Black, ConsoleColor.White, "Other controls are displayed ingame.")
    	
    End Sub
    
    Sub WriteMMHelp2()
    	
    	For i As Integer = 0 To 24
			For j As Integer = 0 To 79
				WriteTiles(j, i, ConsoleColor.White, ConsoleColor.White, " ")
			Next
    	Next
    	
    	WriteTiles(8, 6, ConsoleColor.Black, ConsoleColor.White, "Classes : ")
    	WriteTiles(8, 8, ConsoleColor.Black, ConsoleColor.White, "Paladin - High armor + melee DMG, low Heal and Magic DMG. 0 Mana regen.")
    	WriteTiles(8, 9, ConsoleColor.Black, ConsoleColor.White, "Battlemage - Hybrid between Paladin and Sorcerer.")
    	WriteTiles(8, 10, ConsoleColor.Black, ConsoleColor.White, "Sorcerer - Highest Heal and Magic DMG, low armor and melee DMG.")
    	WriteTiles(8, 12, ConsoleColor.Black, ConsoleColor.White, "Stats : ")
    	WriteTiles(8, 14, ConsoleColor.Black, ConsoleColor.White, "Str - Increases Your melee DMG")
    	WriteTiles(8, 15, ConsoleColor.Black, ConsoleColor.White, "Dex - Determines who attacks first in a fight.")
    	WriteTiles(8, 16, ConsoleColor.Black, ConsoleColor.White, "Int - Increases Magic DMG and Heal.")
    	WriteTiles(8, 17, ConsoleColor.Black, ConsoleColor.White, "Wis - Increases Mana regen.")
    	
    End Sub
End Class
