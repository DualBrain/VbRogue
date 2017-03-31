Public Class DialogueHandler

    'Tato trida se zabyva vykreslovanim a ovladanim dialogu

	Dim cCon As New cCon
	Dim MapParser As New MapParser
    Dim WindowControls As New WindowControls
    Dim ShopBuyHandler As New ShopBuyHandler
    Dim ShopSellHandler As New ShopSellHandler
	
	Sub DialogueScreen(NPCType As Byte, NPCID As Byte)
		
		WindowOpen = true
        cCon.WriteDialogue(NPC(NPCType, NPCID).DialogueID)
        Dim IsShop As Boolean = False
        Dim ISQuest As Boolean = False
        Dim Star As Byte = 0
        Dim Choice As Boolean = False

        If Dialogues(NPC(NPCType, NPCID).DialogueID).ShopID > 0 Then
            IsShop = True
        End If


		While True

            If IsShop = True Then
                If Star = 0 Then
                    cCon.WriteAt(3, 6, ConsoleColor.Yellow, "*")
                    cCon.WriteAt(18, 6, ConsoleColor.Yellow, " ")
                Else
                    cCon.WriteAt(3, 6, ConsoleColor.Yellow, " ")
                    cCon.WriteAt(18, 6, ConsoleColor.Yellow, "*")
                End If
                WindowControls.DialogueShopInput(Star, Choice)
                If Star = 0 And Choice = True Then
                    ShopBuyHandler.InitializeShopBuyHandler(Dialogues(NPC(NPCType, NPCID).DialogueID).ShopID)
                    Exit While
                ElseIf Choice = True Then
                    ShopSellHandler.InitializeShopSellHandler()
                    Exit While
                End If
            ElseIf ISQuest = True Then

            Else
                WindowControls.DialogueInput()
            End If

            If WindowOpen = False Then
                Exit While
            End If

        End While
		
		
		For i As Integer = 0 To 64
			For j As Integer = 0 To 20
				cCon.WriteTiles(i, j, Tiles(nMapArray(j, i)).Color, Tiles(nMapArray(j, i)).Background, Tiles(nMapArray(j, i)).Symbol)
			Next
        Next

        cCon.WriteInit()
		
	End Sub
	
End Class
