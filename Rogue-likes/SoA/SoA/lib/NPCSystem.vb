Public Class NPCSystem
	
	'Databaze Non Player Charakteru
	
	Sub InitializeNPCs()
		
		'Peaceful
		
		main.NPC(0,0).ID = 0
		main.NPC(0,0).Name = "Healer_1"
		main.NPC(0,0).X = 26
		main.NPC(0,0).Y = 8
		main.NPC(0,0).Symbol = "H"
		main.NPC(0,0).Color = ConsoleColor.DarkCyan
		main.NPC(0,0).HP = 10
		main.NPC(0,0).MP = 10
		main.NPC(0,0).MinDmg = 10
		main.NPC(0,0).MaxDmg = 10
		main.NPC(0,0).Armor = 10
		main.NPC(0,0).Dex = 10
		main.NPC(0,0).ItemID = 0
		main.NPC(0,0).DialogueID = 0
		main.NPC(0,0).MapID = 0
		
		'Shopkeeper
		
		main.NPC(1,0).ID = 0
		main.NPC(1,0).Name = "Shopkeeper_1"
		main.NPC(1,0).X = 4
		main.NPC(1,0).Y = 17
		main.NPC(1,0).Symbol = "$"
		main.NPC(1,0).Color = ConsoleColor.DarkCyan
		main.NPC(1,0).HP = 10
		main.NPC(1,0).DialogueID = 1
		main.NPC(1,0).MapID = 0
		
		main.NPC(1,1).ID = 1
		main.NPC(1,1).Name = "Shopkeeper_2"
		main.NPC(1,1).X = 60
		main.NPC(1,1).Y = 4
		main.NPC(1,1).Symbol = "$"
		main.NPC(1,1).Color = ConsoleColor.DarkCyan
		main.NPC(1,1).HP = 10
		main.NPC(1,1).DialogueID = 2
		main.NPC(1,1).MapID = 0
		
		main.NPC(1,2).ID = 2
		main.NPC(1,2).Name = "Shopkeeper_3"
		main.NPC(1,2).X = 60
		main.NPC(1,2).Y = 8
		main.NPC(1,2).Symbol = "$"
		main.NPC(1,2).Color = ConsoleColor.DarkCyan
		main.NPC(1,2).HP = 10
		main.NPC(1,2).DialogueID = 3
		main.NPC(1,2).MapID = 0
		
		main.NPC(1,3).ID = 3
		main.NPC(1,3).Name = "Shopkeeper_4"
		main.NPC(1,3).X = 61
		main.NPC(1,3).Y = 5
		main.NPC(1,3).Symbol = "$"
		main.NPC(1,3).Color = ConsoleColor.DarkCyan
		main.NPC(1,3).HP = 10
		main.NPC(1,3).DialogueID = 4
		main.NPC(1,3).MapID = 2
		
		main.NPC(1,4).ID = 4
		main.NPC(1,4).Name = "Shopkeeper_5"
		main.NPC(1,4).X = 40
		main.NPC(1,4).Y = 8
		main.NPC(1,4).Symbol = "$"
		main.NPC(1,4).Color = ConsoleColor.DarkCyan
		main.NPC(1,4).HP = 10
		main.NPC(1,4).DialogueID = 5
		main.NPC(1,4).MapID = 3
		
		main.NPC(1,5).ID = 5
		main.NPC(1,5).Name = "Shopkeeper_6"
		main.NPC(1,5).X = 18
		main.NPC(1,5).Y = 13
		main.NPC(1,5).Symbol = "$"
		main.NPC(1,5).Color = ConsoleColor.DarkCyan
		main.NPC(1,5).HP = 10
		main.NPC(1,5).DialogueID = 6
		main.NPC(1,5).MapID = 4
		
		'Neutral Mobs
		
		main.NPC(2,0).ID = 0
		main.NPC(2,0).Name = "Furious Rat"
		main.NPC(2,0).X = 58
		main.NPC(2,0).Y = 17
		main.NPC(2,0).Symbol = "R"
		main.NPC(2,0).Color = ConsoleColor.DarkYellow
		main.NPC(2,0).HP = 20
		main.NPC(2,0).MinDmg = 1
		main.NPC(2,0).MaxDmg = 4
		main.NPC(2,0).Armor = 2
		main.NPC(2,0).Dex = 4
		main.NPC(2,0).ItemID = 1
		main.NPC(2,0).ItemType = 8
		main.NPC(2,0).MapID = 0
		
		main.NPC(2,1).ID = 1
		main.NPC(2,1).Name = "Scared Rat"
		main.NPC(2,1).X = 60
		main.NPC(2,1).Y = 16
		main.NPC(2,1).Symbol = "R"
		main.NPC(2,1).Color = ConsoleColor.DarkYellow
		main.NPC(2,1).HP = 12
		main.NPC(2,1).MinDmg = 1
		main.NPC(2,1).MaxDmg = 3
		main.NPC(2,1).Armor = 2
		main.NPC(2,1).Dex = 4
		main.NPC(2,1).ItemID = 1
		main.NPC(2,1).ItemType = 8
		main.NPC(2,1).MapID = 0
		
		main.NPC(2,2).ID = 2
		main.NPC(2,2).Name = "Rat"
		main.NPC(2,2).X = 6
		main.NPC(2,2).Y = 9
		main.NPC(2,2).Symbol = "R"
		main.NPC(2,2).Color = ConsoleColor.DarkYellow
		main.NPC(2,2).HP = 18
		main.NPC(2,2).MinDmg = 2
		main.NPC(2,2).MaxDmg = 3
		main.NPC(2,2).Armor = 3
		main.NPC(2,2).Dex = 4
		main.NPC(2,2).ItemID = 1
		main.NPC(2,2).ItemType = 8
		main.NPC(2,2).MapID = 5
		
		main.NPC(2,3).ID = 3
		main.NPC(2,3).Name = "Rat"
		main.NPC(2,3).X = 11
		main.NPC(2,3).Y = 5
		main.NPC(2,3).Symbol = "R"
		main.NPC(2,3).Color = ConsoleColor.DarkYellow
		main.NPC(2,3).HP = 18
		main.NPC(2,3).MinDmg = 2
		main.NPC(2,3).MaxDmg = 3
		main.NPC(2,3).Armor = 3
		main.NPC(2,3).Dex = 4
		main.NPC(2,3).ItemID = 1
		main.NPC(2,3).ItemType = 8
		main.NPC(2,3).MapID = 5
		
		main.NPC(2,4).ID = 4
		main.NPC(2,4).Name = "Rat"
		main.NPC(2,4).X = 13
		main.NPC(2,4).Y = 8
		main.NPC(2,4).Symbol = "R"
		main.NPC(2,4).Color = ConsoleColor.DarkYellow
		main.NPC(2,4).HP = 18
		main.NPC(2,4).MinDmg = 2
		main.NPC(2,4).MaxDmg = 3
		main.NPC(2,4).Armor = 3
		main.NPC(2,4).Dex = 4
		main.NPC(2,4).ItemID = 1
		main.NPC(2,4).ItemType = 8
		main.NPC(2,4).MapID = 5
		
		main.NPC(2,5).ID = 5
		main.NPC(2,5).Name = "Rat"
		main.NPC(2,5).X = 8
		main.NPC(2,5).Y = 12
		main.NPC(2,5).Symbol = "R"
		main.NPC(2,5).Color = ConsoleColor.DarkYellow
		main.NPC(2,5).HP = 18
		main.NPC(2,5).MinDmg = 2
		main.NPC(2,5).MaxDmg = 3
		main.NPC(2,5).Armor = 3
		main.NPC(2,5).Dex = 4
		main.NPC(2,5).ItemID = 1
		main.NPC(2,5).ItemType = 8
		main.NPC(2,5).MapID = 5
		
		main.NPC(2,6).ID = 6
		main.NPC(2,6).Name = "Rat"
		main.NPC(2,6).X = 15
		main.NPC(2,6).Y = 8
		main.NPC(2,6).Symbol = "R"
		main.NPC(2,6).Color = ConsoleColor.DarkYellow
		main.NPC(2,6).HP = 18
		main.NPC(2,6).MinDmg = 2
		main.NPC(2,6).MaxDmg = 3
		main.NPC(2,6).Armor = 3
		main.NPC(2,6).Dex = 4
		main.NPC(2,6).ItemID = 1
		main.NPC(2,6).ItemType = 8
		main.NPC(2,6).MapID = 5
		
		main.NPC(2,7).ID = 7
		main.NPC(2,7).Name = "Sneaky Goblin"
		main.NPC(2,7).X = 39
		main.NPC(2,7).Y = 16
		main.NPC(2,7).Symbol = "G"
		main.NPC(2,7).Color = ConsoleColor.DarkYellow
		main.NPC(2,7).HP = 35
		main.NPC(2,7).MinDmg = 6
		main.NPC(2,7).MaxDmg = 10
		main.NPC(2,7).Armor = 6
		main.NPC(2,7).Dex = 4
		main.NPC(2,7).ItemID = 2
		main.NPC(2,7).ItemType = 8
		main.NPC(2,7).MapID = 1
		
		main.NPC(2,8).ID = 8
		main.NPC(2,8).Name = "Sneaky Goblin"
		main.NPC(2,8).X = 14
		main.NPC(2,8).Y = 9
		main.NPC(2,8).Symbol = "G"
		main.NPC(2,8).Color = ConsoleColor.DarkYellow
		main.NPC(2,8).HP = 35
		main.NPC(2,8).MinDmg = 6
		main.NPC(2,8).MaxDmg = 10
		main.NPC(2,8).Armor = 6
		main.NPC(2,8).Dex = 4
		main.NPC(2,8).ItemID = 2
		main.NPC(2,8).ItemType = 8
		main.NPC(2,8).MapID = 1
		
		main.NPC(2,9).ID = 9
		main.NPC(2,9).Name = "Sneaky Goblin"
		main.NPC(2,9).X = 51
		main.NPC(2,9).Y = 12
		main.NPC(2,9).Symbol = "G"
		main.NPC(2,9).Color = ConsoleColor.DarkYellow
		main.NPC(2,9).HP = 35
		main.NPC(2,9).MinDmg = 6
		main.NPC(2,9).MaxDmg = 10
		main.NPC(2,9).Armor = 6
		main.NPC(2,9).Dex = 4
		main.NPC(2,9).ItemID = 2
		main.NPC(2,9).ItemType = 8
		main.NPC(2,9).MapID = 1
		
		'Hostile Mobs
		
		main.NPC(3,0).ID = 0
		main.NPC(3,0).Name = "Furious Rat"
		main.NPC(3,0).X = 61
		main.NPC(3,0).Y = 18
		main.NPC(3,0).Symbol = "R"
		main.NPC(3,0).Color = ConsoleColor.Red
		main.NPC(3,0).HP = 26
		main.NPC(3,0).MinDmg = 4
		main.NPC(3,0).MaxDmg = 8
		main.NPC(3,0).Armor = 3
		main.NPC(3,0).Dex = 4
		main.NPC(3,0).ItemID = 1
		main.NPC(3,0).ItemType = 7
		main.NPC(3,0).MapID = 0
		
		main.NPC(3,1).ID = 1
		main.NPC(3,1).Name = "Furious Rat"
		main.NPC(3,1).X = 12
		main.NPC(3,1).Y = 13
		main.NPC(3,1).Symbol = "R"
		main.NPC(3,1).Color = ConsoleColor.Red
		main.NPC(3,1).HP = 30
		main.NPC(3,1).MinDmg = 6
		main.NPC(3,1).MaxDmg = 8
		main.NPC(3,1).Armor = 5
		main.NPC(3,1).Dex = 5
		main.NPC(3,1).ItemID = 1
		main.NPC(3,1).ItemType = 8
		main.NPC(3,1).MapID = 5
		
		main.NPC(3,2).ID = 2
		main.NPC(3,2).Name = "Furious Rat"
		main.NPC(3,2).X = 15
		main.NPC(3,2).Y = 10
		main.NPC(3,2).Symbol = "R"
		main.NPC(3,2).Color = ConsoleColor.Red
		main.NPC(3,2).HP = 30
		main.NPC(3,2).MinDmg = 6
		main.NPC(3,2).MaxDmg = 8
		main.NPC(3,2).Armor = 5
		main.NPC(3,2).Dex = 5
		main.NPC(3,2).ItemID = 2
		main.NPC(3,2).ItemType = 7
		main.NPC(3,2).MapID = 5
		
		main.NPC(3,3).ID = 3
		main.NPC(3,3).Name = "Fabulous Goblin"
		main.NPC(3,3).X = 22
		main.NPC(3,3).Y = 9
		main.NPC(3,3).Symbol = "G"
		main.NPC(3,3).Color = ConsoleColor.Red
		main.NPC(3,3).HP = 40
		main.NPC(3,3).MinDmg = 8
		main.NPC(3,3).MaxDmg = 14
		main.NPC(3,3).Armor = 9
		main.NPC(3,3).Dex = 4
		main.NPC(3,3).ItemID = 6
		main.NPC(3,3).ItemType = 2
		main.NPC(3,3).MapID = 1
		
		main.NPC(3,4).ID = 4
		main.NPC(3,4).Name = "Goblin Chief"
		main.NPC(3,4).X = 52
		main.NPC(3,4).Y = 3
		main.NPC(3,4).Symbol = "G"
		main.NPC(3,4).Color = ConsoleColor.Red
		main.NPC(3,4).HP = 60
		main.NPC(3,4).MinDmg = 14
		main.NPC(3,4).MaxDmg = 14
		main.NPC(3,4).Armor = 13
		main.NPC(3,4).Dex = 4
		main.NPC(3,4).ItemID = 5
		main.NPC(3,4).ItemType = 2
		main.NPC(3,4).MapID = 1
		
	End Sub
	
End Class
