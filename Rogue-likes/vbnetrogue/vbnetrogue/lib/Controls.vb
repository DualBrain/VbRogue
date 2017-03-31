Public Class Controls
    Dim cCon As New cCon
    Dim Readinput As ConsoleKeyInfo
    Sub Checkinput()


        Readinput = Console.ReadKey(True) ' Do not display key that is read

        'clean up where player is now instead of redrawing map
        PutTiles(Player.X, Player.Y)

        '*************************************************************************
        'MOVEMENT
        '*************************************************************************

        'Downarrow on keyboard
        If Readinput.Key = 40 Then
            'Stop player from going out of bounds
            If Player.Y <> MAP_HEIGHT Then


                If CollisionDetect(Player.X, Player.Y + 1) = True Then
                    Exit Sub
                End If

                If Player.Y <= (MAP_HEIGHT - 1) Then
                    Player.Y = Player.Y + 1
                    Exit Sub
                End If
            End If
        End If

        'Rightarrow on keyboard


        If Readinput.Key = 39 Then
            If Player.X <> MAP_WIDTH Then
                'Stop player from going out of bounds
                If CollisionDetect(Player.X + 1, Player.Y) = True Then
                    Exit Sub
                End If

                If Player.X <= MAP_WIDTH - 1 Then
                    Player.X = Player.X + 1
                    Exit Sub
                End If
            End If

        End If

        'Uparrow on keyboard
        If Readinput.Key = 38 Then
            If Player.X <> 0 Then

                'Stop player from going out of bounds
                If CollisionDetect(Player.X, Player.Y - 1) = True Then
                    Exit Sub
                End If

                If Player.Y > 0 Then
                    Player.Y = Player.Y - 1
                    Exit Sub
                End If

            End If

        End If

        'Leftarrow on keyboard
        If Readinput.Key = 37 Then
            If Player.Y <> -1 Then

                'Stop player from going out of bounds
                If CollisionDetect(Player.X - 1, Player.Y) = True Then
                    Exit Sub
                End If

                If Player.X > 0 Then
                    Player.X = Player.X - 1
                    Exit Sub
                End If

            End If

        End If
        '*************************************************************************
        'END MOVEMENT
        '*************************************************************************




        '*************************************************************************
        'KEYBOARD ACTION CONTROLS
        '*************************************************************************

        ' the 'o' Key, to open/close doors (79)

        If Readinput.Key = 79 Then

            'SCAN ABOVE PLAYER
            If nMapArray((Player.Y - 1), Player.X) = 2 Or nMapArray((Player.Y - 1), Player.X) = 3 Then
                'Its a door above us. Open or close it.
                If nMapArray((Player.Y - 1), Player.X) = 3 Then
                    'close it
                    nMapArray((Player.Y - 1), Player.X) = 2
                    PutTiles(Player.X, (Player.Y - 1))
                    cCon.WriteToBuffer("You close the door.")
                    Exit Sub
                End If

                If nMapArray((Player.Y - 1), Player.X) = 2 Then
                    'Open it
                    nMapArray((Player.Y - 1), Player.X) = 3
                    PutTiles(Player.X, (Player.Y - 1))
                    cCon.WriteToBuffer("You open the door.")
                    Exit Sub
                End If
            End If

            'SCAN BELOW PLAYER
            If nMapArray((Player.Y + 1), Player.X) = 2 Or nMapArray((Player.Y + 1), Player.X) = 3 Then
                'Its a door above us. Open or close it.
                If nMapArray((Player.Y + 1), Player.X) = 3 Then
                    'close it
                    nMapArray((Player.Y + 1), Player.X) = 2
                    PutTiles(Player.X, (Player.Y + 1))
                    cCon.WriteToBuffer("You close the door.")
                    Exit Sub
                End If

                If nMapArray((Player.Y + 1), Player.X) = 2 Then
                    'Open it
                    nMapArray((Player.Y + 1), Player.X) = 3
                    PutTiles(Player.X, (Player.Y + 1))
                    cCon.WriteToBuffer("You open the door.")
                    Exit Sub
                End If
            End If

            'SCAN RIGHT PLAYER
            If nMapArray(Player.Y, (Player.X + 1)) = 2 Or nMapArray(Player.Y, (Player.X + 1)) = 3 Then
                'Its a door above us. Open or close it.
                If nMapArray(Player.Y, (Player.X + 1)) = 3 Then
                    'close it
                    nMapArray(Player.Y, (Player.X + 1)) = 2
                    PutTiles((Player.X + 1), Player.Y)
                    cCon.WriteToBuffer("You close the door.")
                    Exit Sub
                End If

                If nMapArray(Player.Y, (Player.X + 1)) = 2 Then
                    'Open it
                    nMapArray(Player.Y, (Player.X + 1)) = 3
                    PutTiles((Player.X + 1), Player.Y)
                    cCon.WriteToBuffer("You open the door.")
                    Exit Sub
                End If
            End If


            'SCAN LEFT PLAYER
            If nMapArray(Player.Y, (Player.X - 1)) = 2 Or nMapArray(Player.Y, (Player.X - 1)) = 3 Then
                'Its a door above us. Open or close it.
                If nMapArray(Player.Y, (Player.X - 1)) = 3 Then
                    'close it
                    nMapArray(Player.Y, (Player.X - 1)) = 2
                    PutTiles((Player.X - 1), Player.Y)
                    cCon.WriteToBuffer("You close the door.")
                    Exit Sub
                End If

                If nMapArray(Player.Y, (Player.X - 1)) = 2 Then
                    'Open it
                    nMapArray(Player.Y, (Player.X - 1)) = 3
                    PutTiles((Player.X - 1), Player.Y)
                    cCon.WriteToBuffer("You open the door.")
                    Exit Sub
                End If
            End If

            'END OPENING AND CLOSING DOORS

        End If


    End Sub
End Class
