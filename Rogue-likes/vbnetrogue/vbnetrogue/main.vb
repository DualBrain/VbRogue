Imports System.IO
Module main
    'Initialize Variables
    Public MAP_WIDTH As Integer = 64
    Public MAP_HEIGHT As Integer = 20

    Public nMapArray(MAP_WIDTH, MAP_HEIGHT) As Integer

    Dim Controls As New Controls
    Dim TileSystem As New TileSystem
    Dim MapParser As New MapParser

    Public Player As New Entity("placeholder", "@", ConsoleColor.White, 32, 10)

    Dim Readinput As ConsoleKeyInfo
    Dim cCon As New cCon



#Region "Tile Section"

    'Ensure that this is changed as tiles are added.
    Public Tiles(10) As TILE_TYPE

    Public Structure TILE_TYPE
        Dim ID As Integer
        Dim Symbol As Char
        Dim Name As String
        Dim Color As ConsoleColor
        Dim Passable As Boolean
    End Structure

#End Region


    Sub Main()
        cCon.Initialize() ' Format our console window
        TileSystem.InitializeTiles()
        PlayGame()

    End Sub


    

    Sub PutTiles(ByVal x As Integer, ByVal y As Integer)

        Dim tilesupperbound As Integer = UBound(Tiles)
        Dim tilenum As Integer = nMapArray(y, x)

        For n = 0 To tilesupperbound
            'Find our tile, print it
            If Tiles(n).ID = tilenum Then
                cCon.WriteAt(x, y, Tiles(n).Color, Tiles(n).Symbol)
                Exit For
            End If
        Next

    End Sub



    Sub PlayGame()

        Console.Clear()
        'ReadMap("maps\testmap.map")
        MapParser.ReadMap("maps\ronan.map")
        cCon.WriteToBuffer("On the way in, a sign reads: ""Welcome to the Town of Ronan""")


        While True

            'Output Phase
            cCon.WriteAt(Player.X, Player.Y, ConsoleColor.White, "@")

            'Input Phase
            Controls.Checkinput()


        End While
    End Sub

    

    Function CollisionDetect(ByVal x As Integer, ByVal y As Integer)

        If x <= MAP_WIDTH And y <= MAP_HEIGHT And x >= 0 And y >= 0 Then



            Dim tilecheck As Integer = nMapArray(y, x)

            If Tiles(tilecheck).Passable = False Then
                Return True ' It's true! We hit something!
            End If
        End If
        Return False 'We did not hit anything.

    End Function

    Sub UpdateTile(ByVal x As Integer, ByVal y As Integer, ByVal tileid As Integer)
        nMapArray(y, x) = tileid
        PutTiles(x, y)
    End Sub
End Module
