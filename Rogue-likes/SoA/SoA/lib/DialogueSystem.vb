Public Class DialogueSystem
	
	'Databaze dialogu
	
	Sub InitializeDialogues()
		
		main.Dialogues(0).ID = 0
		main.Dialogues(0).Heal = 20
		main.Dialogues(0).Dialogue_0 = "Healer: Hello there, " & Player.Name & ", would You like some healing?"
		main.Dialogues(0).Dialogue_1 = "Healer: Almost free, only five gold, best in town."
		main.Dialogues(0).Dialogue_2 = Player.Name & ": Ok, heal me up, beardy man."
		main.Dialogues(0).Dialogue_3 = "Healer: But You are already at full health, go away."
		main.Dialogues(0).Dialogue_4 = "Healer: Here we go. It will be 5 gold please."
		main.Dialogues(0).Dialogue_5 = Player.Name & ": Here is Your pay. Excellent job :)"
        main.Dialogues(0).Dialogue_6 = Player.Name & ": Oops. I don't have enough money."

        main.Dialogues(1).ID = 1
        main.Dialogues(1).ShopID = 1
        main.Dialogues(1).Dialogue_0 = "Vendor: Welcome " & Player.Name & ", I have some Potions for You."
        main.Dialogues(1).Dialogue_1 = "Vendor: You want some?"
        
        main.Dialogues(2).ID = 2
        main.Dialogues(2).ShopID = 2
        main.Dialogues(2).Dialogue_0 = "Vendor: Welcome " & Player.Name & ", I have some Equipment for Paladins."
        main.Dialogues(2).Dialogue_1 = "Vendor: You want some?"
        
        main.Dialogues(3).ID = 3
        main.Dialogues(3).ShopID = 3
        main.Dialogues(3).Dialogue_0 = "Vendor: Welcome " & Player.Name & ", I have some Equipment for Sorcerers."
        main.Dialogues(3).Dialogue_1 = "Vendor: You want some?"
        
        main.Dialogues(4).ID = 4
        main.Dialogues(4).ShopID = 4
        main.Dialogues(4).Dialogue_0 = "Vendor: Welcome " & Player.Name & ", I have some Equipment for You."
        main.Dialogues(4).Dialogue_1 = "Vendor: You want some?"
        
        main.Dialogues(5).ID = 5
        main.Dialogues(5).ShopID = 5
        main.Dialogues(5).Dialogue_0 = "Vendor: Welcome " & Player.Name & ", I have some Equipment for You."
        main.Dialogues(5).Dialogue_1 = "Vendor: You want some?"
        
        main.Dialogues(6).ID = 6
        main.Dialogues(6).ShopID = 6
        main.Dialogues(6).Dialogue_0 = "Vendor: Welcome " & Player.Name & ", I have some Equipment for You."
        main.Dialogues(6).Dialogue_1 = "Vendor: You want some?"
	End Sub
	
End Class
