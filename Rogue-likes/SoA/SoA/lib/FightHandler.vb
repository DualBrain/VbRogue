Public Class FightHandler
	
	Dim cCon As New cCon
	Dim WindowControls As New WindowControls
	Dim Inventory As New InventoryHandler
	
	Sub FightInitialization(ByVal NPCType As Integer, NPCID As Integer)
		
		cCon.WriteFight(NPCType, NPCID)
		Dim EndFight As Boolean = False
		
		If NPCType = 2 Then
			WriteToBuffer("You entered a fight with " & NPC(2, NPCID).Name & ".")
		Else
			WriteToBuffer("You were ambushed by " & NPC(3, NPCID).Name & ".")
		End If
		
		While True
			
			WindowControls.FightInput(NPCType, NPCID, EndFight)
			
			If EndFight = True Then
				Exit While
			End If
			
		End While
		
		If Player.HP = 0 Then
			cCon.WriteDeathMessage()
			Console.ReadKey(True)
			Exit Sub
		End If
		
		Loot(0) = NPC(NPCType, NPCID).ItemType
		Loot(1) = NPC(NPCType, NPCID).ItemID
		
		Inventory.InventoryHandler()
		
		Loot(0) = 0
		Loot(1) = 0
		
	End Sub
	
End Class
