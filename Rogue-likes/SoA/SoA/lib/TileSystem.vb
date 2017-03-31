Public Class TileSystem
	
	'Databaze Podlah
	
    Sub InitializeTiles()

        main.Tiles(0).ID = 0
        main.Tiles(0).Name = "Regular Floor"
        main.Tiles(0).Symbol = " "
        main.Tiles(0).Color = ConsoleColor.Black
        main.Tiles(0).Background = ConsoleColor.Gray
        main.Tiles(0).Passable = True

        main.Tiles(1).ID = 1
        main.Tiles(1).Name = "Wooden Wall"
        main.Tiles(1).Symbol = "#"
        main.Tiles(1).Color = ConsoleColor.DarkRed
        main.Tiles(1).Background = ConsoleColor.Darkred
        main.Tiles(1).Passable = False

        main.Tiles(2).ID = 2
        main.Tiles(2).Name = "Closed Door"
        main.Tiles(2).Symbol = "+"
        main.Tiles(2).Color = ConsoleColor.DarkYellow
        main.Tiles(2).Background = ConsoleColor.Gray
        main.Tiles(2).Passable = False

        main.Tiles(3).ID = 3
        main.Tiles(3).Name = "Open Door"
        main.Tiles(3).Symbol = "/"
        main.Tiles(3).Color = ConsoleColor.DarkYellow
        main.Tiles(3).Background = ConsoleColor.Gray
        main.Tiles(3).Passable = True

        main.Tiles(4).ID = 4
        main.Tiles(4).Name = "Stonebrick Wall"
        main.Tiles(4).Symbol = "#"
        main.Tiles(4).Color = ConsoleColor.Black
        main.Tiles(4).Background = ConsoleColor.DarkGray
        main.Tiles(4).Passable = False

        main.Tiles(5).ID = 5
        main.Tiles(5).Name = "Tree Trunk"
        main.Tiles(5).Symbol = "T"
        main.Tiles(5).Color = ConsoleColor.DarkGreen
        main.Tiles(5).Background = ConsoleColor.Green
        main.Tiles(5).Passable = False

        main.Tiles(6).ID = 6
        main.Tiles(6).Name = "Water"
        main.Tiles(6).Symbol = "~"
        main.Tiles(6).Color = ConsoleColor.DarkBlue
        main.Tiles(6).Background = ConsoleColor.Blue
        main.Tiles(6).Passable = False

        main.Tiles(7).ID = 7
        main.Tiles(7).Name = "Grass"
        main.Tiles(7).Symbol = " "
        main.Tiles(7).Color = ConsoleColor.DarkGreen
        main.Tiles(7).Background = ConsoleColor.Green
        main.Tiles(7).Passable = True
        
        main.Tiles(8).ID = 8
        main.Tiles(8).Name = "Flower"
        main.Tiles(8).Symbol = ","
        main.Tiles(8).Color = ConsoleColor.Yellow
        main.Tiles(8).Background = ConsoleColor.Green
        main.Tiles(8).Passable = True
        
        main.Tiles(9).ID = 9
        main.Tiles(9).Name = "Portal"
        main.Tiles(9).Symbol = "@"
        main.Tiles(9).Color = ConsoleColor.Magenta
        main.Tiles(9).Background = ConsoleColor.Gray
        main.Tiles(9).Passable = False
        
    End Sub
End Class
