Public Class ItemSystem
	
	'Databaze Itemu
	
	Sub InitializeItems()
		
		'Consumables
		
		main.Items(0,0).ID = 0 'Item ID
		main.Items(0,0).Name = "Test Consumable" 'Item Name
		main.Items(0,0).Color = ConsoleColor.Gray 'Item Quality
		main.Items(0,0).Price = 10 'Item Price
		main.Items(0,0).Armor = 10 'Bonus Armor
		main.Items(0,0).MinDmg = 1 'Bonus MinDmg
		main.Items(0,0).MaxDmg = 5 'Bonus MaxDmg
		main.Items(0,0).HP = 4 'Bonus Health Points
		main.Items(0,0).MP = 4 'Bonus Magic Points
		main.Items(0,0).Str = 1 'Bonus Strength
		main.Items(0,0).Dex = 1 'Bonus Dexterity
		main.Items(0,0).Int = 1 'Bonus Intellect
		main.Items(0,0).Wis = 1 'Bonus Wisdom
		
		main.Items(0,1).ID = 1
		main.Items(0,1).Name = "Water"
		main.Items(0,1).Color = ConsoleColor.White
		main.Items(0,1).Price = 5
		main.Items(0,1).HP = 6
		main.Items(0,1).MP = 6
		
		main.Items(0,2).ID = 2
		main.Items(0,2).Name = "Holy Water"
		main.Items(0,2).Color = ConsoleColor.White
		main.Items(0,2).Price = 10
		main.Items(0,2).HP = 11
		main.Items(0,2).MP = 11
		
		main.Items(0,3).ID = 3
		main.Items(0,3).Name = "S HP Potion"
		main.Items(0,3).Color = ConsoleColor.White
		main.Items(0,3).Price = 8
		main.Items(0,3).HP = 12
		
		main.Items(0,4).ID = 4
		main.Items(0,4).Name = "M HP Potion"
		main.Items(0,4).Color = ConsoleColor.White
		main.Items(0,4).Price = 16
		main.Items(0,4).HP = 24
		
		main.Items(0,5).ID = 5
		main.Items(0,5).Name = "L HP Potion"
		main.Items(0,5).Color = ConsoleColor.White
		main.Items(0,5).Price = 32
		main.Items(0,5).HP = 50
		
		main.Items(0,6).ID = 6
		main.Items(0,6).Name = "S MP Potion"
		main.Items(0,6).Color = ConsoleColor.White
		main.Items(0,6).Price = 8
		main.Items(0,6).MP = 12
		
		main.Items(0,7).ID = 7
		main.Items(0,7).Name = "M MP Potion"
		main.Items(0,7).Color = ConsoleColor.White
		main.Items(0,7).Price = 16
		main.Items(0,7).MP = 24
		
		main.Items(0,8).ID = 8
		main.Items(0,8).Name = "L MP Potion"
		main.Items(0,8).Color = ConsoleColor.White
		main.Items(0,8).Price = 32
		main.Items(0,8).MP = 50
		
		'Main Hand
		
		main.Items(1,0).ID = 0
		main.Items(1,0).Name = "Test Sword"
		main.Items(1,0).Color = ConsoleColor.Gray
		
		main.Items(1,1).ID = 1
		main.Items(1,1).Name = "Wd. Sword"
		main.Items(1,1).Color = ConsoleColor.Gray
		main.Items(1,1).Price = 10
		main.Items(1,1).Armor = 1
		main.Items(1,1).MinDmg = 2
		main.Items(1,1).MaxDmg = 3
		
		main.Items(1,2).ID = 2
		main.Items(1,2).Name = "Club"
		main.Items(1,2).Color = ConsoleColor.Gray
		main.Items(1,2).Price = 10
		main.Items(1,2).Armor = 1
		main.Items(1,2).MinDmg = 1
		main.Items(1,2).MaxDmg = 4
		
		main.Items(1,3).ID = 3
		main.Items(1,3).Name = "Irn. Sword"
		main.Items(1,3).Color = ConsoleColor.White
		main.Items(1,3).Price = 20
		main.Items(1,3).Armor = 2
		main.Items(1,3).MinDmg = 3
		main.Items(1,3).MaxDmg = 6
		main.Items(1,3).Str = 1
		main.Items(1,3).Dex = 1
	
		main.Items(1,4).ID = 4
		main.Items(1,4).Name = "Irn. Mace"
		main.Items(1,4).Color = ConsoleColor.White
		main.Items(1,4).Price = 20
		main.Items(1,4).Armor = 2
		main.Items(1,4).MinDmg = 2
		main.Items(1,4).MaxDmg = 8
		main.Items(1,4).Str = 2
		
		main.Items(1,5).ID = 5
		main.Items(1,5).Name = "Rapier"
		main.Items(1,5).Color = ConsoleColor.Green
		main.Items(1,5).Price = 40
		main.Items(1,5).Armor = 4
		main.Items(1,5).MinDmg = 5
		main.Items(1,5).MaxDmg = 9
		main.Items(1,5).HP = 3
		main.Items(1,5).Str = 2
		main.Items(1,5).Dex = 1
		
		main.Items(1,6).ID = 6
		main.Items(1,6).Name = "Warhammer"
		main.Items(1,6).Color = ConsoleColor.Green
		main.Items(1,6).Price = 40
		main.Items(1,6).Armor = 6
		main.Items(1,6).MinDmg = 3
		main.Items(1,6).MaxDmg = 12
		main.Items(1,6).HP = 6
		main.Items(1,6).Str = 2
		
		main.Items(1,7).ID = 7
		main.Items(1,7).Name = "Nightblade"
		main.Items(1,7).Color = ConsoleColor.Cyan
		main.Items(1,7).Price = 80
		main.Items(1,7).Armor = 6
		main.Items(1,7).MinDmg = 8
		main.Items(1,7).MaxDmg = 13
		main.Items(1,7).HP = 6
		main.Items(1,7).Str = 2
		main.Items(1,7).Dex = 2
		main.Items(1,7).Int = 1
		
		main.Items(1,8).ID = 8
		main.Items(1,8).Name = "Bonecrusher"
		main.Items(1,8).Color = ConsoleColor.Cyan
		main.Items(1,8).Price = 80
		main.Items(1,8).Armor = 8
		main.Items(1,8).MinDmg = 5
		main.Items(1,8).MaxDmg = 18
		main.Items(1,8).HP = 10
		main.Items(1,8).Str = 3
		main.Items(1,8).Int = 2
		
		main.Items(1,9).ID = 9
		main.Items(1,9).Name = "Deathbringer"
		main.Items(1,9).Color = ConsoleColor.Magenta
		main.Items(1,9).Price = 200
		main.Items(1,9).Armor = 10
		main.Items(1,9).MinDmg = 12
		main.Items(1,9).MaxDmg = 18
		main.Items(1,9).HP = 8
		main.Items(1,9).MP = 5
		main.Items(1,9).Str = 3
		main.Items(1,9).Dex = 3
		main.Items(1,9).Int = 3
		
		main.Items(1,10).ID = 10
		main.Items(1,10).Name = "Armageddon"
		main.Items(1,10).Color = ConsoleColor.Magenta
		main.Items(1,10).Price = 200
		main.Items(1,10).Armor = 14
		main.Items(1,10).MinDmg = 8
		main.Items(1,10).MaxDmg = 24
		main.Items(1,10).HP = 14
		main.Items(1,10).Str = 4
		main.Items(1,10).Dex = 2
		main.Items(1,10).Int = 2
		
		main.Items(1,11).ID = 11
		main.Items(1,11).Name = "Wand"
		main.Items(1,11).Color = ConsoleColor.Gray
		main.Items(1,11).Price = 10
		main.Items(1,11).MinDmg = 1
		main.Items(1,11).MaxDmg = 2
		main.Items(1,11).MP = 3
		
		main.Items(1,12).ID = 12
		main.Items(1,12).Name = "Scepter"
		main.Items(1,12).Color = ConsoleColor.Gray
		main.Items(1,12).Price = 10
		main.Items(1,12).MinDmg = 1
		main.Items(1,12).MaxDmg = 3
		main.Items(1,12).MP = 1
		main.Items(1,12).Wis = 1
		
		main.Items(1,13).ID = 13
		main.Items(1,13).Name = "Rnd. Wand"
		main.Items(1,13).Color = ConsoleColor.White
		main.Items(1,13).Price = 20
		main.Items(1,13).Armor = 1
		main.Items(1,13).MinDmg = 2
		main.Items(1,13).MaxDmg = 3
		main.Items(1,13).MP = 5
		main.Items(1,13).Int = 2
		
		main.Items(1,14).ID = 14
		main.Items(1,14).Name = "Gls. Scepter"
		main.Items(1,14).Color = ConsoleColor.White
		main.Items(1,14).Price = 10
		main.Items(1,14).Armor = 1
		main.Items(1,14).MinDmg = 2
		main.Items(1,14).MaxDmg = 4
		main.Items(1,14).MP = 3
		main.Items(1,14).Int = 1
		main.Items(1,14).Wis = 2
		
		main.Items(1,15).ID = 15
		main.Items(1,15).Name = "Staff"
		main.Items(1,15).Color = ConsoleColor.Green
		main.Items(1,15).Price = 40
		main.Items(1,15).Armor = 2
		main.Items(1,15).MinDmg = 4
		main.Items(1,15).MaxDmg = 5
		main.Items(1,15).MP = 8
		main.Items(1,15).Int = 3
		main.Items(1,15).Wis = 1
		
		main.Items(1,16).ID = 16
		main.Items(1,16).Name = "Malice"
		main.Items(1,16).Color = ConsoleColor.Green
		main.Items(1,16).Price = 40
		main.Items(1,16).Armor = 2
		main.Items(1,16).MinDmg = 4
		main.Items(1,16).MaxDmg = 6
		main.Items(1,16).MP = 5
		main.Items(1,16).Int = 2
		main.Items(1,16).Wis = 3
		
		main.Items(1,17).ID = 17
		main.Items(1,17).Name = "Arcane Staff"
		main.Items(1,17).Color = ConsoleColor.Cyan
		main.Items(1,17).Price = 80
		main.Items(1,17).Armor = 3
		main.Items(1,17).MinDmg = 6
		main.Items(1,17).MaxDmg = 8
		main.Items(1,17).MP = 12
		main.Items(1,17).Dex = 1
		main.Items(1,17).Int = 3
		main.Items(1,17).Wis = 2
		
		main.Items(1,18).ID = 18
		main.Items(1,18).Name = "Holy Malice"
		main.Items(1,18).Color = ConsoleColor.Cyan
		main.Items(1,18).Price = 80
		main.Items(1,18).Armor = 3
		main.Items(1,18).MinDmg = 6
		main.Items(1,18).MaxDmg = 9
		main.Items(1,18).MP = 7
		main.Items(1,18).Dex = 1
		main.Items(1,18).Int = 3
		main.Items(1,18).Wis = 4
		
		main.Items(1,19).ID = 19
		main.Items(1,19).Name = "Starshooter"
		main.Items(1,19).Color = ConsoleColor.Magenta
		main.Items(1,19).Price = 200
		main.Items(1,19).Armor = 4
		main.Items(1,19).MinDmg = 8
		main.Items(1,19).MaxDmg = 11
		main.Items(1,19).MP = 20
		main.Items(1,19).Dex = 2
		main.Items(1,19).Int = 4
		main.Items(1,19).Wis = 3
		
		main.Items(1,20).ID = 20
		main.Items(1,20).Name = "Heaven´s Will"
		main.Items(1,20).Color = ConsoleColor.Magenta
		main.Items(1,20).Price = 200
		main.Items(1,20).Armor = 4
		main.Items(1,20).MinDmg = 8
		main.Items(1,20).MaxDmg = 13
		main.Items(1,20).MP = 12
		main.Items(1,20).Dex = 2
		main.Items(1,20).Int = 4
		main.Items(1,20).Wis = 6
		
		'Off-hand
		
		main.Items(2,1).ID = 1
		main.Items(2,1).Name = "Wd. Shield"
		main.Items(2,1).Color = ConsoleColor.Gray
		main.Items(2,1).Price = 10
		main.Items(2,1).Armor = 5
		
		main.Items(2,2).ID = 2
		main.Items(2,2).Name = "Libram"
		main.Items(2,2).Color = ConsoleColor.Gray
		main.Items(2,2).Price = 10
		main.Items(2,2).Armor = 2
		main.Items(2,2).MP = 3
		
		main.Items(2,3).ID = 3
		main.Items(2,3).Name = "Irn. Shield"
		main.Items(2,3).Color = ConsoleColor.White
		main.Items(2,3).Price = 20
		main.Items(2,3).Armor = 8
		main.Items(2,3).HP = 1
		
		main.Items(2,4).ID = 4
		main.Items(2,4).Name = "Relic"
		main.Items(2,4).Color = ConsoleColor.White
		main.Items(2,4).Price = 20
		main.Items(2,4).Armor = 4
		main.Items(2,4).MP = 5
		main.Items(2,4).Wis = 1
		
		main.Items(2,5).ID = 5
		main.Items(2,5).Name = "Mith. Shield"
		main.Items(2,5).Color = ConsoleColor.Green
		main.Items(2,5).Price = 40
		main.Items(2,5).Armor = 12
		main.Items(2,5).HP = 2
		
		main.Items(2,6).ID = 6
		main.Items(2,6).Name = "Idol"
		main.Items(2,6).Color = ConsoleColor.Green
		main.Items(2,6).Price = 40
		main.Items(2,6).Armor = 6
		main.Items(2,6).HP = 1
		main.Items(2,6).MP = 5
		main.Items(2,6).Int = 1
		main.Items(2,6).Wis = 1
		
		main.Items(2,7).ID = 7
		main.Items(2,7).Name = "Bulwark"
		main.Items(2,7).Color = ConsoleColor.Cyan
		main.Items(2,7).Price = 80
		main.Items(2,7).Armor = 17
		main.Items(2,7).HP = 4
		
		main.Items(2,8).ID = 8
		main.Items(2,8).Name = "Grail"
		main.Items(2,8).Color = ConsoleColor.Gray
		main.Items(2,8).Price = 80
		main.Items(2,8).Armor = 8
		main.Items(2,8).HP = 2
		main.Items(2,8).MP = 8
		main.Items(2,8).Int = 1
		main.Items(2,8).Wis = 2
		
		main.Items(2,9).ID = 9
		main.Items(2,9).Name = "Wall of Death"
		main.Items(2,9).Color = ConsoleColor.Magenta
		main.Items(2,9).Price = 200
		main.Items(2,9).Armor = 23
		main.Items(2,9).HP = 8
		
		main.Items(2,10).ID = 10
		main.Items(2,10).Name = "Demon Skull"
		main.Items(2,10).Color = ConsoleColor.Magenta
		main.Items(2,10).Price = 200
		main.Items(2,10).Armor = 10
		main.Items(2,10).HP = 3
		main.Items(2,10).MP = 10
		main.Items(2,10).Int = 2
		main.Items(2,10).Wis = 3
		
		'Head
		
		main.Items(3,1).ID = 1
		main.Items(3,1).Name = "Lth. Helm"
		main.Items(3,1).Color = ConsoleColor.Gray
		main.Items(3,1).Price = 5
		main.Items(3,1).Armor = 1
		
		main.Items(3,2).ID = 2
		main.Items(3,2).Name = "Cape"
		main.Items(3,2).Color = ConsoleColor.Gray
		main.Items(3,2).Price = 5
		main.Items(3,2).HP = 1
		
		main.Items(3,3).ID = 3
		main.Items(3,3).Name = "Chn. Helm"
		main.Items(3,3).Color = ConsoleColor.White
		main.Items(3,3).Price = 10
		main.Items(3,3).Armor = 3
		main.Items(3,3).HP = 1
		
		main.Items(3,4).ID = 4
		main.Items(3,4).Name = "Silk Cape"
		main.Items(3,4).Color = ConsoleColor.White
		main.Items(3,4).Price = 20
		main.Items(3,4).Armor = 1
		main.Items(3,4).HP = 1
		
		main.Items(3,5).ID = 5
		main.Items(3,5).Name = "Irn. Helm"
		main.Items(3,5).Color = ConsoleColor.Green
		main.Items(3,5).Price = 20
		main.Items(3,5).Armor = 5
		main.Items(3,5).HP = 1
		
		main.Items(3,6).ID = 6
		main.Items(3,6).Name = "Runed Cape"
		main.Items(3,6).Color = ConsoleColor.Green
		main.Items(3,6).Price = 20
		main.Items(3,6).Armor = 2
		main.Items(3,6).HP = 2
		main.Items(3,6).Wis = 1
		
		main.Items(3,7).ID = 7
		main.Items(3,7).Name = "Plt. Helm"
		main.Items(3,7).Color = ConsoleColor.Cyan
		main.Items(3,7).Price = 40
		main.Items(3,7).Armor = 7
		main.Items(3,7).HP = 3
		
		main.Items(3,8).ID = 8
		main.Items(3,8).Name = "Crown"
		main.Items(3,8).Color = ConsoleColor.Cyan
		main.Items(3,8).Price = 40
		main.Items(3,8).Armor = 3
		main.Items(3,8).HP = 3
		main.Items(3,8).Int = 1
		
		main.Items(3,9).ID = 9
		main.Items(3,9).Name = "Horns"
		main.Items(3,9).Color = ConsoleColor.Magenta
		main.Items(3,9).Price = 100
		main.Items(3,9).Armor = 10
		main.Items(3,9).HP = 6
		
		main.Items(3,10).ID = 10
		main.Items(3,10).Name = "Bone Crown"
		main.Items(3,10).Color = ConsoleColor.Magenta
		main.Items(3,10).Price = 100
		main.Items(3,10).Armor = 4
		main.Items(3,10).HP = 5
		main.Items(3,10).Int = 2
		main.Items(3,10).Wis = 1
		
		'Chest
		
		main.Items(4,1).ID = 1
		main.Items(4,1).Name = "Lth. Armor"
		main.Items(4,1).Color = ConsoleColor.Gray
		main.Items(4,1).Price = 5
		main.Items(4,1).Armor = 1
		
		main.Items(4,2).ID = 2
		main.Items(4,2).Name = "Tunic"
		main.Items(4,2).Color = ConsoleColor.Gray
		main.Items(4,2).Price = 5
		main.Items(4,2).Armor = 1
		
		main.Items(4,3).ID = 3
		main.Items(4,3).Name = "Chn. Armor"
		main.Items(4,3).Color = ConsoleColor.White
		main.Items(4,3).Price = 10
		main.Items(4,3).Armor = 4
		main.Items(4,3).HP = 1
		main.Items(4,3).Str = 1
		
		main.Items(4,4).ID = 4
		main.Items(4,4).Name = "Silk Robe"
		main.Items(4,4).Color = ConsoleColor.White
		main.Items(4,4).Price = 20
		main.Items(4,4).Armor = 2
		main.Items(4,4).HP = 1
		main.Items(4,4).Int = 1
		
		main.Items(4,5).ID = 5
		main.Items(4,5).Name = "Irn. Armor"
		main.Items(4,5).Color = ConsoleColor.Green
		main.Items(4,5).Price = 20
		main.Items(4,5).Armor = 7
		main.Items(4,5).HP = 2
		main.Items(4,5).Str = 1
		
		main.Items(4,6).ID = 6
		main.Items(4,6).Name = "Runed Robe"
		main.Items(4,6).Color = ConsoleColor.Green
		main.Items(4,6).Price = 20
		main.Items(4,6).Armor = 4
		main.Items(4,6).HP = 2
		main.Items(4,6).Int = 1
		
		main.Items(4,7).ID = 7
		main.Items(4,7).Name = "Plt. Armor"
		main.Items(4,7).Color = ConsoleColor.Cyan
		main.Items(4,7).Price = 40
		main.Items(4,7).Armor = 10
		main.Items(4,7).HP = 4
		main.Items(4,7).Str = 2
		
		main.Items(4,8).ID = 8
		main.Items(4,8).Name = "Runed Vest"
		main.Items(4,8).Color = ConsoleColor.Cyan
		main.Items(4,8).Price = 40
		main.Items(4,8).Armor = 6
		main.Items(4,8).HP = 3
		main.Items(4,8).Int = 2
		
		main.Items(4,9).ID = 9
		main.Items(4,9).Name = "Mith. Armor"
		main.Items(4,9).Color = ConsoleColor.Magenta
		main.Items(4,9).Price = 100
		main.Items(4,9).Armor = 15
		main.Items(4,9).HP = 6
		main.Items(4,9).Str = 3
		
		main.Items(4,10).ID = 10
		main.Items(4,10).Name = "Bone Vest"
		main.Items(4,10).Color = ConsoleColor.Magenta
		main.Items(4,10).Price = 100
		main.Items(4,10).Armor = 8
		main.Items(4,10).HP = 5
		main.Items(4,10).Int = 3
		main.Items(4,10).Wis = 2
		
		'Legs
		
		main.Items(5,1).ID = 1
		main.Items(5,1).Name = "Lth. Pants"
		main.Items(5,1).Color = ConsoleColor.Gray
		main.Items(5,1).Price = 5
		main.Items(5,1).Armor = 1
		
		main.Items(5,2).ID = 2
		main.Items(5,2).Name = "Kilt"
		main.Items(5,2).Color = ConsoleColor.Gray
		main.Items(5,2).Price = 5
		main.Items(5,2).Armor = 1
		
		main.Items(5,3).ID = 3
		main.Items(5,3).Name = "Chn. Pants"
		main.Items(5,3).Color = ConsoleColor.White
		main.Items(5,3).Price = 10
		main.Items(5,3).Armor = 4
		main.Items(5,3).HP = 1
		main.Items(5,3).Str = 1
		
		main.Items(5,4).ID = 4
		main.Items(5,4).Name = "Silk Kilt"
		main.Items(5,4).Color = ConsoleColor.White
		main.Items(5,4).Price = 20
		main.Items(5,4).Armor = 2
		main.Items(5,4).HP = 1
		main.Items(5,4).Int = 1
	
		main.Items(5,5).ID = 5
		main.Items(5,5).Name = "Irn. Pants"
		main.Items(5,5).Color = ConsoleColor.Green
		main.Items(5,5).Price = 20
		main.Items(5,5).Armor = 7
		main.Items(5,5).HP = 2
		main.Items(5,5).Str = 1
		
		main.Items(5,6).ID = 6
		main.Items(5,6).Name = "Pants"
		main.Items(5,6).Color = ConsoleColor.Green
		main.Items(5,6).Price = 20
		main.Items(5,6).Armor = 4
		main.Items(5,6).HP = 2
		main.Items(5,6).Int = 1
		
		main.Items(5,7).ID = 7
		main.Items(5,7).Name = "Plt. Pants"
		main.Items(5,7).Color = ConsoleColor.Cyan
		main.Items(5,7).Price = 40
		main.Items(5,7).Armor = 10
		main.Items(5,7).HP = 4
		main.Items(5,7).Str = 2
		
		main.Items(5,8).ID = 8
		main.Items(5,8).Name = "Silk Pants"
		main.Items(5,8).Color = ConsoleColor.Cyan
		main.Items(5,8).Price = 40
		main.Items(5,8).Armor = 6
		main.Items(5,8).HP = 3
		main.Items(5,8).Int = 2
		
		main.Items(5,9).ID = 9
		main.Items(5,9).Name = "Mith. Pants"
		main.Items(5,9).Color = ConsoleColor.Magenta
		main.Items(5,9).Price = 100
		main.Items(5,9).Armor = 15
		main.Items(5,9).HP = 6
		main.Items(5,9).Str = 3
		
		main.Items(5,10).ID = 10
		main.Items(5,10).Name = "Runed Pants"
		main.Items(5,10).Color = ConsoleColor.Magenta
		main.Items(5,10).Price = 100
		main.Items(5,10).Armor = 8
		main.Items(5,10).HP = 5
		main.Items(5,10).Int = 3
		main.Items(5,10).Wis = 2
		
		'Arms
		
		main.Items(6,1).ID = 1
		main.Items(6,1).Name = "Lth. Gloves"
		main.Items(6,1).Color = ConsoleColor.Gray
		main.Items(6,1).Price = 5
		main.Items(6,1).Armor = 1
		
		main.Items(6,2).ID = 2
		main.Items(6,2).Name = "Gloves"
		main.Items(6,2).Color = ConsoleColor.Gray
		main.Items(6,2).Price = 5
		main.Items(6,2).HP = 1
		
		main.Items(6,3).ID = 3
		main.Items(6,3).Name = "Chn. Gloves"
		main.Items(6,3).Color = ConsoleColor.White
		main.Items(6,3).Price = 10
		main.Items(6,3).Armor = 3
		main.Items(6,3).HP = 1
		
		main.Items(6,4).ID = 4
		main.Items(6,4).Name = "Silk Gloves"
		main.Items(6,4).Color = ConsoleColor.White
		main.Items(6,4).Price = 10
		main.Items(6,4).Armor = 1
		main.Items(6,4).HP = 1
		
		main.Items(6,5).ID = 5
		main.Items(6,5).Name = "Irn. Gloves"
		main.Items(6,5).Color = ConsoleColor.Green
		main.Items(6,5).Price = 20
		main.Items(6,5).Armor = 5
		main.Items(6,5).HP = 1
		
		main.Items(6,6).ID = 6
		main.Items(6,6).Name = "Runed Gloves"
		main.Items(6,6).Color = ConsoleColor.Green
		main.Items(6,6).Price = 20
		main.Items(6,6).Armor = 2
		main.Items(6,6).HP = 2
		main.Items(6,6).Wis = 1
		
		main.Items(6,7).ID = 7
		main.Items(6,7).Name = "Plt. Gloves"
		main.Items(6,7).Color = ConsoleColor.Cyan
		main.Items(6,7).Price = 40
		main.Items(6,7).Armor = 7
		main.Items(6,7).HP = 3
		
		main.Items(6,8).ID = 8
		main.Items(6,8).Name = "Armband"
		main.Items(6,8).Color = ConsoleColor.Cyan
		main.Items(6,8).Price = 40
		main.Items(6,8).Armor = 3
		main.Items(6,8).HP = 3
		main.Items(6,8).Int = 1
		
		main.Items(6,9).ID = 9
		main.Items(6,9).Name = "Mith. Gloves"
		main.Items(6,9).Color = ConsoleColor.Magenta
		main.Items(6,9).Price = 100
		main.Items(6,9).Armor = 10
		main.Items(6,9).HP = 6
	
		main.Items(6,10).ID = 10
		main.Items(6,10).Name = "Bone Armband"
		main.Items(6,10).Color = ConsoleColor.Magenta
		main.Items(6,10).Price = 100
		main.Items(6,10).Armor = 4
		main.Items(6,10).HP = 5
		main.Items(6,10).Int = 2
		main.Items(6,10).Wis = 1
		
		'Rings
		
		main.Items(7,1).ID = 1
		main.Items(7,1).Name = "HP Ring"
		main.Items(7,1).Color = ConsoleColor.Green
		main.Items(7,1).Price = 50
		main.Items(7,1).HP = 10
		
		main.Items(7,2).ID = 1
		main.Items(7,2).Name = "MP Ring"
		main.Items(7,2).Color = ConsoleColor.Green
		main.Items(7,2).Price = 50
		main.Items(7,2).MP = 10
		
		main.Items(7,3).ID = 1
		main.Items(7,3).Name = "Armor Ring"
		main.Items(7,3).Color = ConsoleColor.Green
		main.Items(7,3).Price = 50
		main.Items(7,3).Armor = 15
		
		main.Items(7,4).ID = 1
		main.Items(7,4).Name = "Str Ring"
		main.Items(7,4).Color = ConsoleColor.Green
		main.Items(7,4).Price = 50
		main.Items(7,4).Str = 5
	
		main.Items(7,5).ID = 1
		main.Items(7,5).Name = "Dex Ring"
		main.Items(7,5).Color = ConsoleColor.Green
		main.Items(7,5).Price = 50
		main.Items(7,5).Dex = 5
		
		main.Items(7,6).ID = 1
		main.Items(7,6).Name = "Int Ring"
		main.Items(7,6).Color = ConsoleColor.Green
		main.Items(7,6).Price = 50
		main.Items(7,6).Int = 5
		
		main.Items(7,7).ID = 1
		main.Items(7,7).Name = "Wis Ring"
		main.Items(7,7).Color = ConsoleColor.Green
		main.Items(7,7).Price = 50
		main.Items(7,7).Wis = 5
		
		'Misc
		
		main.Items(8,1).ID = 1
		main.Items(8,1).Name = "Rat Tooth"
		main.Items(8,1).Color = ConsoleColor.Gray
		main.Items(8,1).Price = 5
		
		main.Items(8,2).ID = 1
		main.Items(8,2).Name = "Goblin Finger"
		main.Items(8,2).Color = ConsoleColor.Gray
		main.Items(8,2).Price = 10
		
    End Sub
End Class
