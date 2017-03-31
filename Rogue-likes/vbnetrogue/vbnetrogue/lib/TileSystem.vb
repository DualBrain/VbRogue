Public Class TileSystem
    Sub InitializeTiles()

        main.Tiles(0).ID = 0
        main.Tiles(0).Name = "Regular Floor"
        main.Tiles(0).Symbol = "."
        main.Tiles(0).Color = ConsoleColor.Gray
        main.Tiles(0).Passable = True

        main.Tiles(1).ID = 1
        main.Tiles(1).Name = "Wall"
        main.Tiles(1).Symbol = "#"
        main.Tiles(1).Color = ConsoleColor.Gray
        main.Tiles(1).Passable = False

        main.Tiles(2).ID = 2
        main.Tiles(2).Name = "Closed Door"
        main.Tiles(2).Symbol = "+"
        main.Tiles(2).Color = ConsoleColor.DarkYellow
        main.Tiles(2).Passable = False

        main.Tiles(3).ID = 3
        main.Tiles(3).Name = "Open Door"
        main.Tiles(3).Symbol = "/"
        main.Tiles(3).Color = ConsoleColor.DarkYellow
        main.Tiles(3).Passable = True

        main.Tiles(4).ID = 4
        main.Tiles(4).Name = "Locked Door"
        main.Tiles(4).Symbol = "+"
        main.Tiles(4).Color = ConsoleColor.DarkYellow
        main.Tiles(4).Passable = False

        main.Tiles(5).ID = 5
        main.Tiles(5).Name = "Tree"
        main.Tiles(5).Symbol = "T"
        main.Tiles(5).Color = ConsoleColor.DarkGreen
        main.Tiles(5).Passable = False

        main.Tiles(6).ID = 6
        main.Tiles(6).Name = "Water"
        main.Tiles(6).Symbol = "~"
        main.Tiles(6).Color = ConsoleColor.Blue
        main.Tiles(6).Passable = False

        main.Tiles(7).ID = 7
        main.Tiles(7).Name = "Grass"
        main.Tiles(7).Symbol = "."
        main.Tiles(7).Color = ConsoleColor.Green
        main.Tiles(7).Passable = True



    End Sub
End Class
