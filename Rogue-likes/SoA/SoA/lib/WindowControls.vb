Public Class WindowControls

    'Tato trida se zabyva ovladanim v oknech

	Dim Readinput As ConsoleKeyInfo
	Dim cCon As New cCon
	Dim MMHelp As New MainMenuHelp
	
	Sub FightInput(NPCType As Integer, NPCID As Integer, ByRef EndFight As Boolean)
		
		Dim Rnd As New Random
		Dim Var1, Var2, Var3 As Integer
		
		Readinput = Console.ReadKey(True)
		
		If Readinput.Key = 70 Then
			If NPC(NPCType, NPCID).Dex > Player.Dex Then
				
				Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
				If Var1 > Player.HP Then
					Var1 = Player.HP
				End If
				If Var1 < 0 Then
					Var1 = 0
				End If
				Player.HP = Player.HP - Var1
				If Player.HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				Var2 = ((Math.Ceiling(((Rnd.Next(Player.MinDmg, Player.MaxDmg)) - (NPC(NPCType, NPCID).Armor / 5))))/3) * Math.Abs((-3) + Player.ClassType)
				If Var2 > NPC(NPCType, NPCID).HP Then
					Var2 = NPC(NPCType, NPCID).HP
				End If
				NPC(NPCType, NPCID).HP = NPC(NPCType, NPCID).HP - Var2
				If NPC(NPCType, NPCID).HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				cCon.WriteAt(6, 4, ConsoleColor.White, "        ")
				cCon.WriteAt(9, 4, ConsoleColor.White, NPC(NPCType, NPCID).HP)
				cCon.WriteStats()
				
				WriteToBuffer("You were hurt for " & Var1 & "HP and You hurt " & NPC(NPCType, NPCID).Name & " for " & Var2 & "HP." )
				Exit Sub
			Else
				
				Var2 = ((Math.Ceiling(((Rnd.Next(Player.MinDmg, Player.MaxDmg)) - (NPC(NPCType, NPCID).Armor / 5))))/3) * Math.Abs((-3) + Player.ClassType)
				If Var2 > NPC(NPCType, NPCID).HP Then
					Var2 = NPC(NPCType, NPCID).HP
				ElseIf Var2 < 0 Then
					Var2 = 0
				End If
				NPC(NPCType, NPCID).HP = NPC(NPCType, NPCID).HP - Var2
				If NPC(NPCType, NPCID).HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
				If Var1 > Player.HP Then
					Var1 = Player.HP
				End If
				If Var1 < 0 Then
					Var1 = 0
				End If
				Player.HP = Player.HP - Var1
				If Player.HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				cCon.WriteAt(6, 4, ConsoleColor.White, "        ")
				cCon.WriteAt(9, 4, ConsoleColor.White, NPC(NPCType, NPCID).HP)
				cCon.WriteStats()
				
				WriteToBuffer("You hurt " & NPC(NPCType, NPCID).Name & " for " & Var2 & "HP and You were hurt for " & Var1 & "HP.")
				Exit Sub
			End If
		End If
		
		If Readinput.Key = 66 Then
			If NPC(NPCType, NPCID).Dex > Player.Dex Then
				
				Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 12.5) * Math.Abs((-3) + Player.ClassType)))
				If Var1 > Player.HP Then
					Var1 = Player.HP
				End If
				If Var1 < 0 Then
					Var1 = 0
				End If
				Player.HP = Player.HP - Var1
				If Player.HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				Var2 = ((Math.Ceiling(((Rnd.Next(Player.MinDmg, Player.MaxDmg)) - (NPC(NPCType, NPCID).Armor / 10))))/6) * Math.Abs((-3) + Player.ClassType)
				If Var2 > NPC(NPCType, NPCID).HP Then
					Var2 = NPC(NPCType, NPCID).HP
				End If
				NPC(NPCType, NPCID).HP = NPC(NPCType, NPCID).HP - Var2
				If NPC(NPCType, NPCID).HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				cCon.WriteAt(6, 4, ConsoleColor.White, "        ")
				cCon.WriteAt(9, 4, ConsoleColor.White, NPC(NPCType, NPCID).HP)
				cCon.WriteStats()
				
				WriteToBuffer("You were hurt for " & Var1 & "HP and You hurt " & NPC(NPCType, NPCID).Name & " for " & Var2 & "HP." )
				Exit Sub
			Else
				
				Var2 = (Math.Ceiling(((Rnd.Next(Player.MinDmg, Player.MaxDmg)) - (NPC(NPCType, NPCID).Armor / 10)))/6) * Math.Abs((-3) + Player.ClassType)
				If Var2 > NPC(NPCType, NPCID).HP Then
					Var2 = NPC(NPCType, NPCID).HP
				ElseIf Var2 < 0 Then
					Var2 = 0
				End If
				NPC(NPCType, NPCID).HP = NPC(NPCType, NPCID).HP - Var2
				If NPC(NPCType, NPCID).HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 12.5) * Math.Abs((-3) + Player.ClassType)))
				If Var1 > Player.HP Then
					Var1 = Player.HP
				End If
				If Var1 < 0 Then
					Var1 = 0
				End If
				Player.HP = Player.HP - Var1
				If Player.HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				cCon.WriteAt(6, 4, ConsoleColor.White, "        ")
				cCon.WriteAt(9, 4, ConsoleColor.White, NPC(NPCType, NPCID).HP)
				cCon.WriteStats()
				
				WriteToBuffer("You hurt " & NPC(NPCType, NPCID).Name & " for " & Var2 & "HP and You were hurt for " & Var1 & "HP.")
				Exit Sub
			End If
			
		End If
		
		If Readinput.Key = 72 Then
			
			If Player.MP < 3 Then
				
				WriteToBuffer("You do not have enough mana.")
				
			Else
			
				If NPC(NPCType, NPCID).Dex > Player.Dex Then
				
					Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
					If Var1 > Player.HP Then
						Var1 = Player.HP
					ElseIf Var1 < 0 Then
						Var1 = 0
					End If
					Player.HP = Player.HP - Var1
					If Player.HP < 1 Then
					EndFight = True
					Exit Sub
					End If
				
					Var3 = Player.Int * (2 + (Math.Ceiling(Player.ClassType/2)))
					Player.HP = Player.HP + Var3
					If Player.HP > Player.HPMax Then
						Player.HP = Player.HPMax
					End If
				
					Player.MP = Player.MP - 3
					cCon.WriteStats()
					
					WriteToBuffer(NPC(NPCType, NPCID).Name & " hurts You for " & var1 & "HP and You heal " & Var3 & "HP.")
					Exit Sub
				Else
				
					Var3 = Player.Int * (2 + (Math.Ceiling(Player.ClassType/2)))
					Player.HP = Player.HP + Var3
					If Player.HP > Player.HPMax Then
						Player.HP = Player.HPMax
					End If
				
					Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
					If Var1 > Player.HP Then
						Var1 = Player.HP
					End If
					If Var1 < 0 Then
						Var1 = 0
					End If
					Player.HP = Player.HP - Var1
					If Player.HP < 1 Then
					EndFight = True
					Exit Sub
					End If
				
					Player.MP = Player.MP - 3
					cCon.WriteStats()
				
					WriteToBuffer("You heal " & Var3 & "HP and " & NPC(NPCType, NPCID).Name & " hurts You for " & Var1 & "HP.")
					Exit Sub
				End If
			End If
		End If
		
		If Readinput.Key = 82 Then
			
			If NPC(NPCType, NPCID).Dex > Player.Dex Then
				
				Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
				If Var1 > Player.HP Then
					Var1 = Player.HP
				End If
				If Var1 < 0 Then
					Var1 = 0
				End If
				Player.HP = Player.HP - Var1
				If Player.HP < 1 Then
					EndFight = True
					Exit Sub
				End If
				
				Var3 = ((Math.Ceiling((Math.Sqrt(CDbl(Player.Wis))) / 3)) * (Player.ClassType * 2))
				Player.MP = Player.MP + Var3
					
				If Player.MP > Player.MPMax Then
					Player.MP = Player.MPMax
				End If
			
				cCon.WriteStats()
				
				WriteToBuffer(NPC(NPCType, NPCID).Name & " hurts You for " & var1 & "HP and You restore " & Var3 & "MP.")
				Exit Sub
			Else
				
				Var3 = ((Math.Ceiling((Math.Sqrt(CDbl(Player.Wis))) / 3)) * (Player.ClassType * 2))
				Player.MP = Player.MP + Var3
					
				If Player.MP > Player.MPMax Then
					Player.MP = Player.MPMax
				End If
				
				Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
				If Var1 > Player.HP Then
					Var1 = Player.HP
				End If
				If Var1 < 0 Then
					Var1 = 0
				End If
				Player.HP = Player.HP - Var1
				If Player.HP < 1 Then
					EndFight = True
					Exit Sub
				End If
			
				cCon.WriteStats()
				
				WriteToBuffer("You restore " & Var3 & "MP and " & NPC(NPCType, NPCID).Name & " hurts You for " & var1 & "HP.")
				Exit Sub
			End If
		End If
		
		If Readinput.Key = 77 Then
			
			If Player.MP < 3 Then
				
				WriteToBuffer("You do not have enough mana.")
				
			Else
				
				If NPC(NPCType, NPCID).Dex > Player.Dex Then
				
					Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
					If Var1 > Player.HP Then
						Var1 = Player.HP
					End If
					If Var1 < 0 Then
						Var1 = 0
					End If
					Player.HP = Player.HP - Var1
					If Player.HP < 1 Then
					EndFight = True
					Exit Sub
					End If
					
					Var2 = Math.Ceiling((Player.Int * (2 + Player.ClassType))/1.5)
					If Var2 > NPC(NPCType, NPCID).HP Then
						Var2 = NPC(NPCType, NPCID).HP
					End If
					NPC(NPCType, NPCID).HP = NPC(NPCType, NPCID).HP - Var2
					If NPC(NPCType, NPCID).HP < 1 Then
						EndFight = True
						Exit Sub
					End If
					
					Player.MP = Player.MP - 3
					cCon.WriteAt(6, 4, ConsoleColor.White, "        ")
					cCon.WriteAt(9, 4, ConsoleColor.White, NPC(NPCType, NPCID).HP)
					cCon.WriteStats()
					
					WriteToBuffer("You were hurt for " & Var1 & "HP and You hurt " & NPC(NPCType, NPCID).Name & " for " & Var2 & "HP." )
					Exit Sub
				Else
					
					Var2 = Math.Ceiling((Player.Int * (1 + Player.ClassType))/1.5)
					If Var2 > NPC(NPCType, NPCID).HP Then
						Var2 = NPC(NPCType, NPCID).HP
					End If
					NPC(NPCType, NPCID).HP = NPC(NPCType, NPCID).HP - Var2
					If NPC(NPCType, NPCID).HP < 1 Then
						EndFight = True
						Exit Sub
					End If
					
					Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
					If Var1 > Player.HP Then
						Var1 = Player.HP
					End If
					If Var1 < 0 Then
						Var1 = 0
					End If
					Player.HP = Player.HP - Var1
					If Player.HP < 1 Then
					EndFight = True
					Exit Sub
					End If
					
					Player.MP = Player.MP - 3
					cCon.WriteAt(6, 4, ConsoleColor.White, "        ")
					cCon.WriteAt(9, 4, ConsoleColor.White, NPC(NPCType, NPCID).HP)
					cCon.WriteStats()
					
					WriteToBuffer("You hurt " & NPC(NPCType, NPCID).Name & " for " & Var2 & "HP and You were hurt for " & Var1 & "HP.")
					Exit Sub
				End If
			End IF
		End If
		
		If Readinput.Key = 81 Then
			
			Var2 = Math.Ceiling((((Math.Ceiling(((Rnd.Next(Player.MinDmg, Player.MaxDmg)) - (NPC(NPCType, NPCID).Armor / 5))))/3) * Math.Abs((-3) + Player.ClassType)) * 0.75)
			If Var2 > NPC(NPCType, NPCID).HP Then
			Var2 = NPC(NPCType, NPCID).HP
			ElseIf Var2 < 0 Then
				Var2 = 0
			End If
			NPC(NPCType, NPCID).HP = NPC(NPCType, NPCID).HP - Var1
			If NPC(NPCType, NPCID).HP < 1 Then
				EndFight = True
				Exit Sub
			End If
			
			Var1 = Math.Ceiling((Rnd.Next(NPC(NPCType, NPCID).MinDmg, NPC(NPCType, NPCID).MaxDmg))-((Player.Armor / 30) * Math.Abs((-3) + Player.ClassType)))
			If Var1 > Player.HP Then
				Var1 = Player.HP
			End If
			Player.HP = Player.HP - Var1
			If Player.HP < 1 Then
					EndFight = True
					Exit Sub
			End If
			
			cCon.WriteAt(6, 4, ConsoleColor.White, "        ")
			cCon.WriteAt(9, 4, ConsoleColor.White, NPC(NPCType, NPCID).HP)
			cCon.WriteStats()
			
			WriteToBuffer("You hurt " & NPC(NPCType, NPCID).Name & " for " & Var2 & "HP and You were hurt for " & Var1 & "HP.")
			Exit Sub		
			
		End If
		
	End Sub
	
	Sub InventoryCheckInput(ByRef Star As Byte)
		
		Readinput = Console.ReadKey(True)
		
		If Readinput.Key = 73 Or Readinput.Key = 27 Then
        	WindowOpen = false
        	Exit Sub
		End If
		
		If Readinput.Key = 32 Then
			cCon.WriteItemInfo(Star)
			Readinput = Console.ReadKey(True)
			cCon.WriteInventory()
		End If
		
		If Readinput.Key = 76 Then
			If Loot(0) > 0 And Loot(1) > 0 Then
				For i As Integer = 0 To 15
					If Inventory(i).ItemID = 0 And Inventory(i).ItemType = 0 Then
						Inventory(i).ItemID = Loot(1)
						Inventory(i).ItemType = Loot(0)
						Loot(1) = 0
						Loot(0) = 0
						WriteToBuffer("You got " & Items(Inventory(i).ItemType, Inventory(i).ItemID).Name & ".")
						cCon.WriteInventory()
						Exit For
					End If
				Next
				If Loot(1) > 0 And Loot(0) > 0 Then
					WriteToBuffer("You have full inventory.")
				End If
			End If
		End If
		
		If Readinput.Key = 13 Then
            If Inventory(Star).ItemType > 0 And Inventory(Star).ItemType < 8 And Inventory(Star).ItemID > 0 Then

                Dim Var_0, Var_1 As Byte

                Player.HPMax = Player.HPMax - Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).HP
                Player.MPMax = Player.MPMax - Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).MP
                Player.Armor = Player.Armor - Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Armor
                Player.Str = Player.Str - Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Str
                Player.Dex = Player.Dex - Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Dex
                Player.Int = Player.Int - Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Int
                Player.Wis = Player.Wis - Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Wis
                Var_0 = Equipment(Inventory(Star).ItemType - 1).ItemID
                Var_1 = Equipment(Inventory(Star).ItemType - 1).SlotType
                Equipment(Inventory(Star).ItemType - 1).ItemID = Inventory(Star).ItemID
                Inventory(Star).ItemID = Var_0
                Inventory(Star).ItemType = Var_1
                Player.HPMax = Player.HPMax + Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).HP
                Player.MPMax = Player.MPMax + Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).MP
                Player.Armor = Player.Armor + Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Armor
                Player.Str = Player.Str + Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Str
                Player.Dex = Player.Dex + Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Dex
                Player.Int = Player.Int + Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Int
                Player.Wis = Player.Wis + Items(Equipment(Inventory(Star).ItemType - 1).SlotType, Equipment(Inventory(Star).ItemType - 1).ItemID).Wis
                InventorySort()
                cCon.WriteEquipment()
                cCon.WriteInventory()
            ElseIf Inventory(Star).ItemType = 0 And Inventory(Star).ItemID > 0 Then
            	Player.HP = Player.HP + Items(0, Inventory(Star).ItemID).HP
            	Player.MP = Player.MP + Items(0, Inventory(Star).ItemID).MP
            	If Player.HP > Player.HPMax Then
            		Player.HP = Player.HPMax
            	End If
            	If Player.MP > Player.MPMax Then
            		Player.MP = Player.MPMax
            	End If
            	Inventory(Star).ItemType = 0
            	Inventory(Star).ItemID = 0
            	InventorySort()
                cCon.WriteStats()
                cCon.WriteInventory()
            End If
		End If
		
		If Readinput.Key = 46 Then
			Inventory(Star).ItemType = 0
			Inventory(Star).ItemID = 0
			InventorySort()
			cCon.WriteInventory()
		End If
		
		If Star < 8 Then
				cCon.WriteAt(3, 2 + 2 * Star, ConsoleColor.Yellow, " ")
			Elseif Star < 16 Then
				cCon.WriteAt(33, 2 + 2 * (Star - 8), ConsoleColor.Yellow, " ")
		End If
		
		If Readinput.Key = 37 Or Readinput.Key = 39 Then
			If Star < 8 Then
				Star = Star + 8
			ElseIf Star < 16 Then
				Star = Star - 8
			End If
			Exit sub
		End If
		
		If Readinput.Key = 38 Then
			If Star = 0 Then 
				Star = 15
			Else
				Star = Star - 1
			End If
			Exit sub
		End If
		
		If Readinput.Key = 40 Then
			If Star = 15 Then 
				Star = 0
			Else
				Star = Star + 1
			End If
			Exit sub
		End If
		
	End Sub
	
	Private Sub InventorySort()
		For i As Integer = 0 To 15
			For j As Integer = i To 15
				If Inventory(i).ItemID = 0 Then
					Inventory(i).ItemType = Inventory(j).ItemType
					Inventory(i).ItemID = Inventory(j).ItemID
					Inventory(j).ItemType = 0
					Inventory(j).ItemID = 0
				End If
			Next
		Next
	End Sub
	
	Sub CharacterInput()
		
		Readinput = Console.ReadKey(True)
		
		If Readinput.Key = 27 Or Readinput.Key = 67 Then
			WindowOpen = False
			Exit sub
		End If
		
	End Sub
	
	Sub DialogueInput()
		
		Readinput = Console.ReadKey(True)
		
		If Readinput.Key = 13 Or Readinput.Key = 27 Then
			WindowOpen = False
			Exit Sub
		End If
		
    End Sub

    Sub DialogueShopInput(ByRef Star As Byte, ByRef Choice As Boolean)

        Readinput = Console.ReadKey(True)

        If Readinput.Key = 37 Or Readinput.Key = 39 Then
            If Star = 0 Then
                Star = 1
                Exit Sub
            Else
                Star = 0
                Exit Sub
            End If
        End If

        If Readinput.Key = 13 Then
            Choice = True
            Exit Sub
        End If

        If Readinput.Key = 27 Then
            WindowOpen = False
            Exit Sub
        End If

    End Sub

    Sub ShopBuyInput(ByRef Star As Byte, ByVal ShopID As Byte)
    	
    	Readinput = Console.ReadKey(True)
    	
    	If Readinput.Key = 27 Then
    		WindowOpen = False
    		Exit Sub
    	End If
    	
    	cCon.WriteAt(3, 2 + 2 * Star, ConsoleColor.White, " ")
    	
    	If Readinput.Key = 38 Then
    		If Star = 0 Then
    			Star = 8
    		Else
    			Star = Star - 1
    		End If
    	End If
    	
    	If Readinput.Key = 40 Then
    		If Star = 8 Then
    			Star = 0
    		Else
    			Star = Star + 1
    		End If
    	End If
    	
    	If Readinput.Key = 13 Then
    		
    		For i As Integer = 0 To 15
    			If Inventory(i).ItemType = 0 And Inventory(i).ItemID = 0 Then
    				Select Case Star
    					Case 0
    						If Player.Gold > Items(Shops(ShopID).ItemType_0, Shops(ShopID).ItemID_0).Price - 1 And (Shops(ShopID).ItemType_0 > 0 Or Shops(ShopID).ItemID_0 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_0
    							Inventory(i).ItemID = Shops(ShopID).ItemID_0
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_0, Shops(ShopID).ItemID_0).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_0, Shops(ShopID).ItemID_0).Price And (Shops(ShopID).ItemType_0 > 0 Or Shops(ShopID).ItemID_0 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    					Case 1
    						If Player.Gold > Items(Shops(ShopID).ItemType_1, Shops(ShopID).ItemID_1).Price - 1 And (Shops(ShopID).ItemType_1 > 0 Or Shops(ShopID).ItemID_1 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_1
    							Inventory(i).ItemID = Shops(ShopID).ItemID_1
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_1, Shops(ShopID).ItemID_1).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_1, Shops(ShopID).ItemID_1).Price And (Shops(ShopID).ItemType_1 > 0 Or Shops(ShopID).ItemID_1 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    					Case 2
    						If Player.Gold > Items(Shops(ShopID).ItemType_2, Shops(ShopID).ItemID_2).Price - 1 And (Shops(ShopID).ItemType_2 > 0 Or Shops(ShopID).ItemID_2 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_2
    							Inventory(i).ItemID = Shops(ShopID).ItemID_2
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_2, Shops(ShopID).ItemID_2).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_2, Shops(ShopID).ItemID_2).Price And (Shops(ShopID).ItemType_2 > 0 Or Shops(ShopID).ItemID_2 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    					Case 3
    						If Player.Gold > Items(Shops(ShopID).ItemType_3, Shops(ShopID).ItemID_3).Price - 1 And (Shops(ShopID).ItemType_3 > 0 Or Shops(ShopID).ItemID_3 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_3
    							Inventory(i).ItemID = Shops(ShopID).ItemID_3
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_3, Shops(ShopID).ItemID_3).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_3, Shops(ShopID).ItemID_3).Price And (Shops(ShopID).ItemType_3 > 0 Or Shops(ShopID).ItemID_3 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    					Case 4
    						If Player.Gold > Items(Shops(ShopID).ItemType_4, Shops(ShopID).ItemID_1).Price - 1 And (Shops(ShopID).ItemType_4 > 0 Or Shops(ShopID).ItemID_4 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_4
    							Inventory(i).ItemID = Shops(ShopID).ItemID_4
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_4, Shops(ShopID).ItemID_4).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_4, Shops(ShopID).ItemID_4).Price And (Shops(ShopID).ItemType_4 > 0 Or Shops(ShopID).ItemID_4 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    					Case 5
    						If Player.Gold > Items(Shops(ShopID).ItemType_5, Shops(ShopID).ItemID_1).Price - 1 And (Shops(ShopID).ItemType_5 > 0 Or Shops(ShopID).ItemID_5 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_5
    							Inventory(i).ItemID = Shops(ShopID).ItemID_5
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_5, Shops(ShopID).ItemID_5).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_5, Shops(ShopID).ItemID_5).Price And (Shops(ShopID).ItemType_5 > 0 Or Shops(ShopID).ItemID_5 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    					Case 6
    						If Player.Gold > Items(Shops(ShopID).ItemType_6, Shops(ShopID).ItemID_6).Price - 1 And (Shops(ShopID).ItemType_6 > 0 Or Shops(ShopID).ItemID_6 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_6
    							Inventory(i).ItemID = Shops(ShopID).ItemID_6
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_6, Shops(ShopID).ItemID_6).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_6, Shops(ShopID).ItemID_6).Price And (Shops(ShopID).ItemType_6 > 0 Or Shops(ShopID).ItemID_6 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    					Case 7
    						If Player.Gold > Items(Shops(ShopID).ItemType_7, Shops(ShopID).ItemID_7).Price - 1 And (Shops(ShopID).ItemType_7 > 0 Or Shops(ShopID).ItemID_7 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_7
    							Inventory(i).ItemID = Shops(ShopID).ItemID_7
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_7, Shops(ShopID).ItemID_7).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_7, Shops(ShopID).ItemID_7).Price And (Shops(ShopID).ItemType_7 > 0 Or Shops(ShopID).ItemID_7 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    					Case 8
    						If Player.Gold > Items(Shops(ShopID).ItemType_8, Shops(ShopID).ItemID_8).Price - 1 And (Shops(ShopID).ItemType_8 > 0 Or Shops(ShopID).ItemID_8 > 0) Then
    							Inventory(i).ItemType = Shops(ShopID).ItemType_8
    							Inventory(i).ItemID = Shops(ShopID).ItemID_8
    							Player.Gold = Player.Gold - Items(Shops(ShopID).ItemType_8, Shops(ShopID).ItemID_8).Price
    						ElseIf Player.Gold < Items(Shops(ShopID).ItemType_8, Shops(ShopID).ItemID_8).Price And (Shops(ShopID).ItemType_8 > 0 Or Shops(ShopID).ItemID_8 > 0) Then
    							WriteToBuffer("You don't have enough gold.")
    						End If
    						cCon.WriteGold()
    						Exit Sub
    				End Select
    				
    			End If
    		Next
    		
    	End If
    	
    End Sub
    
    Sub ShopSellInput(ByRef Star As Byte)
    	
		Readinput = Console.ReadKey(True)
    	
		If Readinput.Key = 27 Then
        	WindowOpen = false
        	Exit Sub
		End If
		
		If Readinput.Key = 13 Then
			If Inventory(Star).ItemType > 0 Or Inventory(Star).ItemID > 0 Then
				Player.Gold = Player.Gold + Math.Ceiling((Items(Inventory(Star).ItemType, Inventory(Star).ItemID).Price * 0.75))
				Inventory(Star).ItemType = 0
				Inventory(Star).ItemID = 0
				cCon.WriteGold()
				InventorySort()
				cCon.WriteInventory()
			End If
			Exit Sub
		End If
		
		If Star < 8 Then
				cCon.WriteAt(3, 2 + 2 * Star, ConsoleColor.Yellow, " ")
			Elseif Star < 16 Then
				cCon.WriteAt(33, 2 + 2 * (Star - 8), ConsoleColor.Yellow, " ")
		End If
		
		If Readinput.Key = 37 Or Readinput.Key = 39 Then
			If Star < 8 Then
				Star = Star + 8
			ElseIf Star < 16 Then
				Star = Star - 8
			End If
			Exit sub
		End If
		
		If Readinput.Key = 38 Then
			If Star = 0 Then 
				Star = 15
			Else
				Star = Star - 1
			End If
			Exit sub
		End If
		
		If Readinput.Key = 40 Then
			If Star = 15 Then 
				Star = 0
			Else
				Star = Star + 1
			End If
			Exit sub
		End If
    	
    End Sub
	
	Sub MainMenuInput()
		
		Readinput = Console.ReadKey(True)
		
		If Readinput.Key = 27 Then 
			Environment.Exit(0)
		ElseIf Readinput.Key = 80 Then
			WindowOpen = False
		ElseIf Readinput.Key = ConsoleKey.H Then
			MMHelp.MMHelp()
		End If
	End Sub
	
	Sub GameMenuInput(ByRef Star As Byte,ByRef KeyPres As Boolean)
		
		Readinput = Console.ReadKey(True)
		
		If Readinput.Key = 27 Then
			WindowOpen = False
			Exit Sub
		End If
		
		cCon.WriteAt(26, 7 + 2 * Star, ConsoleColor.Yellow, " ")
		
		If Readinput.Key = 40 Then
			If Star = 3 Then
				Star = 0
			Else 
				Star = Star + 1
			End If
		End If
		
		If Readinput.Key = 38 Then
			If Star = 0 Then
				Star = 3
			Else
				Star = Star - 1
			End If
		End If
		
		If Readinput.Key = 13 Then
			KeyPres = True
		End If
		
	End Sub
	
	Sub ChoosingGenderInput()
		
		Readinput = Console.ReadKey(True)
		
		If Readinput.Key = 49 Or Readinput.Key = 97 Then
			Player.Sex = "Male"
		Else
			Player.Sex = "Female"
		End If
		
	End Sub
	
	Sub ChoosingClassInput()
		
		Readinput = Console.ReadKey(True)
		
		Select Case Readinput.Key
				
			Case 49, 97
				Player.ClassType = 0
				Inventory(0).ItemType = 1
				Inventory(0).ItemID = 1
				Inventory(1).ItemType = 2
				Inventory(1).ItemID = 1
				Inventory(2).ItemType = 3
				Inventory(2).ItemID = 1
				Inventory(3).ItemType = 4
				Inventory(3).ItemID = 1
				Inventory(4).ItemType = 5
				Inventory(4).ItemID = 1
				Inventory(5).ItemType = 6
				Inventory(5).ItemID = 1
				Inventory(6).ItemType = 0
				Inventory(6).ItemID = 1
				Inventory(7).ItemType = 0
				Inventory(7).ItemID = 1
				
			Case 50, 98
				Player.ClassType = 1
				Inventory(0).ItemType = 1
				Inventory(0).ItemID = 1
				Inventory(1).ItemType = 2
				Inventory(1).ItemID = 1
				Inventory(2).ItemType = 3
				Inventory(2).ItemID = 2
				Inventory(3).ItemType = 4
				Inventory(3).ItemID = 2
				Inventory(4).ItemType = 5
				Inventory(4).ItemID = 2
				Inventory(5).ItemType = 6
				Inventory(5).ItemID = 2
				Inventory(6).ItemType = 0
				Inventory(6).ItemID = 1
				Inventory(7).ItemType = 0
				Inventory(7).ItemID = 1
				
			Case 51, 99
				Player.ClassType = 2
				Inventory(0).ItemType = 1
				Inventory(0).ItemID = 11
				Inventory(1).ItemType = 2
				Inventory(1).ItemID = 2
				Inventory(2).ItemType = 3
				Inventory(2).ItemID = 2
				Inventory(3).ItemType = 4
				Inventory(3).ItemID = 2
				Inventory(4).ItemType = 5
				Inventory(4).ItemID = 2
				Inventory(5).ItemType = 6
				Inventory(5).ItemID = 2
				Inventory(6).ItemType = 0
				Inventory(6).ItemID = 1
				Inventory(7).ItemType = 0
				Inventory(7).ItemID = 1
				
		End Select
		
	End Sub
	
	Sub ChoosingRaceInput()
		
		Readinput = Console.ReadKey(True)
		While True
		Select Case Readinput.Key
				
			Case 72
				Player.Race = "Human"
				Player.Str = Player.Str + 2
				Player.Int = Player.Int + 2
				Player.HPMax = Player.HPMax + 5
				Player.HP = Player.HP + 5
				Exit Sub
				
			Case 68
				Player.Race = "Dwarf"
				Player.Str = Player.Str + 2
				Player.Dex = Player.Dex + 2
				Player.HPMax = Player.HPMax + 5
				Player.HP = Player.HP + 5
				Exit Sub
				
			Case 69
				Player.Race = "Elf"
				Player.Wis = Player.Wis + 2
				Player.Int = Player.Int + 2
				Player.Dex = Player.Dex + 2
				Exit Sub
				
			Case 79
				Player.Race = "Orc"
				Player.Str = Player.Str + 4
				Player.HPMax = Player.HPMax + 5
				Player.HP = Player.HP + 5
				Exit Sub
				
			Case 84
				Player.Race = "Troll"
				Player.Int = Player.Int + 2
				Player.Dex = Player.Dex + 4
				Exit Sub
				
			Case 70
				Player.Race = "Faerie"
				Player.Int = Player.Int + 2
				Player.Dex = Player.Dex + 2
				Player.MPMax = Player.MPMax + 5
				Player.MP = Player.MP + 5
				Exit Sub
				
			Case 77
				Player.Race = "Demon"
				Player.Str = Player.Str + 2
				Player.Dex = Player.Dex + 2
				Player.MPMax = Player.MPMax + 5
				Player.MP = Player.MP + 5
				Exit Sub
				
			Case 71
				Player.Race = "Gnome"
				Player.Int = Player.Int + 2
				Player.Wis = Player.Wis + 2
				Player.MPMax = Player.MPMax + 5
				Player.MP = Player.MP + 5
				Exit Sub
				
			Case 74
				Player.Race = "Djinn"
				Player.Wis = Player.Wis + 2
				Player.MPMax = Player.MPMax + 10
				Player.MP = Player.MP + 10
				Exit Sub
				
			Case 82
				Player.Race = "Draconid"
				Player.Dex = Player.Dex + 2
				Player.HPMax = Player.HPMax + 5
				Player.HP = Player.HP + 5
				Player.MPMax = Player.MPMax + 5
				Player.MP = Player.MP + 5
				Exit Sub
				
		End Select
		End While
		
	End Sub
End Class