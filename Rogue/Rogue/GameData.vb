Module GameData

  Friend Structure Monster

    Public Sub New(symbol As Char,
                   name As String,
                   treasurePercent As Integer,
                   experience As Integer,
                   hitDie As Integer,
                   armorClass As Integer,
                   damage As String,
                   notes As String)

      Me.Symbol = symbol
      Me.Name = name
      Me.TreasurePercent = treasurePercent
      Me.Experience = experience
      Me.HitDie = hitDie
      Me.ArmorClass = armorClass
      Me.Damage = damage
      Me.Notes = notes

    End Sub

    Public Symbol As Char
    Public Name As String
    Public TreasurePercent As Integer
    Public Experience As Integer
    Public HitDie As Integer
    Public ArmorClass As Integer
    Public Damage As String
    Public Notes As String

    Public IsMean As Boolean
    Public IsFlying As Boolean
    Public IsRegerating As Boolean
    Public IsGreedy As Boolean
    Public IsInvisible As Boolean

  End Structure

  Public Structure Damage

    Public Sub New(count%, type%)
      DieCount = count
      DieType = type
    End Sub

    Public DieCount As Integer
    Public DieType As Integer

  End Structure

  Public Structure Weapon

    Sub New(name$,
            melee As Damage,
            thrown As Damage)
      Me.Name = name
      MeleeDamage = melee
      ThrownDamage = thrown
    End Sub

    Public Name As String
    Public MeleeDamage As Damage
    Public ThrownDamage As Damage

  End Structure

  Friend Structure Armor

    Public Sub New(name$, armorClass%)
      Me.Name = name
      Me.ArmorClass = armorClass
    End Sub

    Public Name As String
    Public ArmorClass As Integer

  End Structure

  ' Scrolls
  ' Potions
  ' Rings
  ' Rods

  ' Traps

  'ReadOnly armors As New List(Of Armor) From
  '  {New Armor("Leather", 3),
  '   New Armor("Ring Mail", 4),
  '   New Armor("Studded Leather", 4),
  '   New Armor("Scale Mail", 5),
  '   New Armor("Chain Mail", 6),
  '   New Armor("Splint Mail", 7),
  '   New Armor("Banded Mail", 7),
  '   New Armor("Plate Mail", 8)}

  'ReadOnly weapons As New List(Of Weapon) From
  '  {New Weapon("Mace", New Damage(2, 4), Nothing),
  '   New Weapon("Long Sword", New Damage(3, 4), Nothing),
  '   New Weapon("Short Bow", New Damage(1, 1), Nothing),
  '   New Weapon("Arrow", New Damage(2, 3), Nothing),
  '   New Weapon("Dagger", New Damage(1, 6), New Damage(1, 4)),
  '   New Weapon("Two-handed Sword", New Damage(4, 4), Nothing),
  '   New Weapon("Dart", Nothing, New Damage(1, 3)),
  '   New Weapon("Shuriken", Nothing, New Damage(2, 4)),
  '   New Weapon("Spear", New Damage(2, 3), New Damage(1, 6))}

  'ReadOnly monsters As New List(Of Monster) From
  '  {New Monster("A"c, "Aquator", 0, 20, 5, 2, "0d0/0d0", "Rusts armor") With {.IsMean = True},
  '   New Monster("B"c, "Bat", 0, 1, 1, 3, "1d2", "Flies Randomly") With {.IsFlying = True},
  '   New Monster("C"c, "Centaur", 15, 17, 4, 4, "1d2/1d5/1d5", Nothing),
  '   New Monster("D"c, "Dragon", 100, 5000, 10, -1, "1d8/1d8/3d10", "Ranged 6d6 flame attack") With {.IsMean = True},
  '   New Monster("E"c, "Emu", 0, 2, 1, 7, "1d2", Nothing) With {.IsMean = True},
  '   New Monster("F"c, "Venus Flytrap", 0, 80, 8, 3, "special", "Traps Player") With {.IsMean = True},
  '   New Monster("G"c, "Griffin", 20, 2000, 13, 2, "4d3/3d5", Nothing) With {.IsMean = True, .IsFlying = True, .IsRegerating = True},
  '   New Monster("H"c, "Hobgoblin", 0, 3, 1, 5, "1d8", Nothing) With {.IsMean = True},
  '   New Monster("I"c, "Ice Monster", 0, 5, 1, 9, "0d0", "Freezes Player"),
  '   New Monster("J"c, "Jabberwock", 70, 3000, 15, 6, "2d12/2d4", Nothing),
  '   New Monster("K"c, "Kestrel", 0, 1, 1, 7, "1d4", Nothing) With {.IsMean = True, .IsFlying = True},
  '   New Monster("L"c, "Leprechaun", 0, 10, 3, 8, "1d1", "Steels Gold"),
  '   New Monster("M"c, "Medusa", 40, 200, 8, 2, "3d4/3d4/2d5", "Confuses Player") With {.IsMean = True},
  '   New Monster("N"c, "Nymph", 100, 37, 3, 9, "0d0", "Steels Magic Item"),
  '   New Monster("O"c, "Orc", 15, 5, 1, 6, "1d8", "Greedy - Runs toward gold") With {.IsGreedy = True},
  '   New Monster("P"c, "Phantom", 0, 120, 8, 3, "4d4", "Invisible"),
  '   New Monster("Q"c, "Quagga", 0, 15, 3, 3, "1d5/1d5", Nothing) With {.IsInvisible = True},
  '   New Monster("R"c, "Rattlesnake", 0, 9, 2, 3, "1d6", "Reduces Strength") With {.IsMean = True},
  '   New Monster("S"c, "Snake", 0, 2, 1, 5, "1d3", Nothing) With {.IsMean = True},
  '   New Monster("T"c, "Troll", 50, 120, 6, 4, "1d8/1d8/2d6", Nothing) With {.IsMean = True, .IsRegerating = True},
  '   New Monster("U"c, "Ur-vile", 0, 190, 7, -1, "1d9/1d9/2d9", Nothing) With {.IsMean = True},
  '   New Monster("V"c, "Vampire", 20, 350, 8, 1, "1d10", "Drains Max HP") With {.IsMean = True, .IsRegerating = True},
  '   New Monster("W"c, "Wraith", 0, 55, 5, 4, "1d6", "Drains Exp"),
  '   New Monster("X"c, "Xeroc", 30, 100, 7, 7, "4d4", "Imitates an object"),
  '   New Monster("Y"c, "Yeti", 30, 50, 4, 6, "1d6/1d6", Nothing),
  '   New Monster("Z"c, "Zombie", 0, 6, 2, 8, "1d8", Nothing) With {.IsMean = True}}

End Module
