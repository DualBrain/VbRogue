Public Class Controls

    'Tato trida se zabyva ovladanim postavy na mape (Ne ovladani v oknech)

	Dim cCon As New cCon	
	Dim Readinput As ConsoleKeyInfo
	Dim PortalHandler As New PortalHandler
	Dim InventoryHandler As New InventoryHandler
	Dim CharacterScreenHandler As New CharacterScreenHandler
	Dim DialogueHandler As New DialogueHandler
	Dim GameMenu As New GameMenu
	Dim FightHandler As New FightHandler
	
	Sub Checkinput()
		
        Readinput = Console.ReadKey(True) 'Zajisti neukazani stisknuteho tlacitka

        'Prekresleni policka mapy po hracove pohybu
        PutTiles(Player.X, Player.Y)

        '*************************************************************************
        'Pohyb
        '*************************************************************************

        If Readinput.Key = 40 Then
        	
        	'Zastavi hrace pri nepovolenem pohybu
        	
            If Player.Y <> MAP_HEIGHT Then


                If CollisionDetect(Player.X, Player.Y + 1) = True Or EntityDetect(Player.X, Player.Y + 1) = True Then
                    Exit Sub
                End If

                If Player.Y <= (MAP_HEIGHT - 1) Then
                    Player.Y = Player.Y + 1
                    Exit Sub
                End If
            End If
        End If

        If Readinput.Key = 39 Then
        	If Player.X <> MAP_WIDTH Then
        		
        		'Zastavi hrace pri nepovolenem pohybu
        		
                If CollisionDetect(Player.X + 1, Player.Y) = True Or EntityDetect(Player.X + 1, Player.Y) = True Then
                    Exit Sub
                End If

                If Player.X <= MAP_WIDTH - 1 Then
                    Player.X = Player.X + 1
                    Exit Sub
                End If
            End If

        End If

        If Readinput.Key = 38 Then
            If Player.X <> 0 Then

				'Zastavi hrace pri nepovolenem pohybu

                If CollisionDetect(Player.X, Player.Y - 1) = True Or EntityDetect(Player.X, Player.Y - 1) = True Then
                    Exit Sub
                End If

                If Player.Y > 0 Then
                    Player.Y = Player.Y - 1
                    Exit Sub
                End If

            End If

        End If

        If Readinput.Key = 37 Then
            If Player.Y <> -1 Then

				'Zastavi hrace pri nepovolenem pohybu

                If CollisionDetect(Player.X - 1, Player.Y) = True Or EntityDetect(Player.X - 1, Player.Y) = True Then
                    Exit Sub
                End If
                
                If Player.X > 0 Then
                    Player.X = Player.X - 1
                    Exit Sub
                End If

            End If

        End If
        
        '*************************************************************************
        'Konec Pohybu
        '*************************************************************************



        '*************************************************************************
        'Hracova interakce
        '*************************************************************************

        'Space slouzi k interakci s mapou (dvere, portaly, tlacitka) (32)

        If Readinput.Key = 32 Then

			'Sken nad

            If nMapArray((Player.Y - 1), Player.X) = 2 Or nMapArray((Player.Y - 1), Player.X) = 3 Or nMapArray((Player.Y - 1), Player.X) = 9 Then
                
                If nMapArray((Player.Y - 1), Player.X) = 3 Then
                    'Zavreni dveri
                    nMapArray((Player.Y - 1), Player.X) = 2
                    PutTiles(Player.X, (Player.Y - 1))
                    WriteToBuffer("You close the door.")
                    Exit Sub
                End If

                If nMapArray((Player.Y - 1), Player.X) = 2 Then
                    'Otevreni dveri
                    nMapArray((Player.Y - 1), Player.X) = 3
                    PutTiles(Player.X, (Player.Y - 1))
                    WriteToBuffer("You open the door.")
                    Exit Sub
                End If
                
                If nMapArray((Player.Y - 1), Player.X) = 9 Then
                	InPortal = True
                	Exit Sub
                End If
            End If

			'Sken pod

            If nMapArray((Player.Y + 1), Player.X) = 2 Or nMapArray((Player.Y + 1), Player.X) = 3  Or nMapArray((Player.Y + 1), Player.X) = 9 Then
                
                If nMapArray((Player.Y + 1), Player.X) = 3 Then
                    'Zavreni dveri
                    nMapArray((Player.Y + 1), Player.X) = 2
                    PutTiles(Player.X, (Player.Y + 1))
                    WriteToBuffer("You close the door.")
                    Exit Sub
                End If

                If nMapArray((Player.Y + 1), Player.X) = 2 Then
                    'Otevreni dveri
                    nMapArray((Player.Y + 1), Player.X) = 3
                    PutTiles(Player.X, (Player.Y + 1))
                    WriteToBuffer("You open the door.")
                    Exit Sub
                End If
                
                If nMapArray((Player.Y + 1), Player.X) = 9 Then
                	InPortal = True
                	Exit Sub
                End If
            End If

			'Sken vpravo

            If nMapArray(Player.Y, (Player.X + 1)) = 2 Or nMapArray(Player.Y, (Player.X + 1)) = 3  Or nMapArray(Player.Y, (Player.X + 1)) = 9 Then
            	
            	If nMapArray(Player.Y, (Player.X + 1)) = 3 Then
                    'Zavreni dveri
                    nMapArray(Player.Y, (Player.X + 1)) = 2
                    PutTiles((Player.X + 1), Player.Y)
                    WriteToBuffer("You close the door.")
                    Exit Sub
                End If

                If nMapArray(Player.Y, (Player.X + 1)) = 2 Then
                    'Otevreni dveri
                    nMapArray(Player.Y, (Player.X + 1)) = 3
                    PutTiles((Player.X + 1), Player.Y)
                    WriteToBuffer("You open the door.")
                    Exit Sub
                End If
                
                If nMapArray(Player.Y, (Player.X + 1)) = 9 Then
                	InPortal = True
                	Exit Sub
                End If
            End If

			'Sken vlevo

            If nMapArray(Player.Y, (Player.X - 1)) = 2 Or nMapArray(Player.Y, (Player.X - 1)) = 3  Or nMapArray(Player.Y, (Player.X - 1)) = 9 Then
                
                If nMapArray(Player.Y, (Player.X - 1)) = 3 Then
                    'Zavreni dveri
                    nMapArray(Player.Y, (Player.X - 1)) = 2
                    PutTiles((Player.X - 1), Player.Y)
                    WriteToBuffer("You close the door.")
                    Exit Sub
                End If

                If nMapArray(Player.Y, (Player.X - 1)) = 2 Then
                    'Otevreni dveri
                    nMapArray(Player.Y, (Player.X - 1)) = 3
                    PutTiles((Player.X - 1), Player.Y)
                    WriteToBuffer("You open the door.")
                    Exit Sub
                End If
                
                If nMapArray(Player.Y, (Player.X - 1)) = 9 Then
                	InPortal = True
                	Exit Sub
                End If
            End If
        End If
        
        '*************************************************************************
        'Otevirani Oken
        '*************************************************************************
        
        ' "I" slouzi k otevreni inventare (73)
        
        If Readinput.Key = 73 Then
        	InventoryHandler.InventoryHandler()
        	Exit Sub
        End If
        
        ' "C" slouzi k otevreni character infa (67)
        
        If Readinput.Key = 67 Then
        	CharacterScreenHandler.CharacterScreen()
        	Exit Sub
        End If
        
        ' Enter slouzi k interakci s NPC (13)
        
        If Readinput.Key = 13 Then
        	For i As Integer = 0 To 2
        		For j As Integer = 0 To 29
        			If (((Player.X + 1) = NPC(i,j).X) And ((Player.Y) = NPC(i,j).Y))  Or (((Player.X - 1) = NPC(i,j).X) And ((Player.Y = NPC(i,j).Y))) Or ((Player.X = NPC(i,j).X) And ((Player.Y + 1) = NPC(i,j).Y)) Or ((Player.X = NPC(i,j).X) And ((Player.Y - 1) = NPC(i,j).Y)) Then
        				If NPC(i, j).MapID = PortalInf(0) Then
        					If i = 0 Or i = 1 then
        						DialogueHandler.DialogueScreen(i, j)
        						Exit Sub
        					Else
        						FightHandler.FightInitialization(i, j)
        					End If
        				End If
        			End If
        		Next
        	Next
        End If
        
        If Readinput.Key = 27 Then
        	GameMenu.GameMenu()
        	Exit Sub
        End If
        
	End Sub
	
End Class
