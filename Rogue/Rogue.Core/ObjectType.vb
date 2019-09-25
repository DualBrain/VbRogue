Namespace Global.Rogue.Core

  Public Enum ObjectType
    None
    Gold
    Food
    Trap
    Armor
    Weapon
    Scroll
    Potion
    Ring
    Rod
  End Enum

  Public MustInherit Class ObjectBase

    Public Sub New(type As ObjectType, name As String, Optional hidden As Boolean = False)
      Me.Type = type
      Me.Name = name
      Me.Hidden = hidden
    End Sub

    Public ReadOnly Property Type As ObjectType
    Public ReadOnly Property Name As String
    Public Property Hidden As Boolean

    Public Overrides Function ToString() As String
      Return Name
    End Function

  End Class

  Public Enum ArmorType
    Leather
    RingMail
    StuddedLeather
    ScaleMail
    ChainMail
    SplintMail
    BandedMail
    PlateMail
  End Enum

  Public NotInheritable Class Armor
    Inherits ObjectBase

    Public Sub New(type As ArmorType)
      MyBase.New(ObjectType.Armor, Armor.Template(type).Name)
      AC = Armor.Template(type).AC
    End Sub

    Private Sub New(name As String, ac As Integer)
      MyBase.New(ObjectType.Armor, name)
      Me.AC = ac
    End Sub

    Public ReadOnly Property AC As Integer
    Public Property Magic As Integer

    Public Shared Function Template(type As ArmorType) As Armor

      Select Case type
        Case ArmorType.Leather : Return New Armor("leather", 3)
        Case ArmorType.RingMail : Return New Armor("ring mail", 4)
        Case ArmorType.StuddedLeather : Return New Armor("studded leather", 4)
        Case ArmorType.ScaleMail : Return New Armor("scale mail", 5)
        Case ArmorType.ChainMail : Return New Armor("chain mail", 6)
        Case ArmorType.SplintMail : Return New Armor("splint mail", 7)
        Case ArmorType.BandedMail : Return New Armor("banded mail", 7)
        Case ArmorType.PlateMail : Return New Armor("plate mail", 8)
        Case Else
          Return Nothing
      End Select

    End Function

    Public Overrides Function ToString() As String
      Return $"+{Magic} {Name} [armor class {AC + Magic}]"
    End Function

  End Class

  Public NotInheritable Class Trap
    Inherits ObjectBase

    Private Sub New(trapType As TrapType, name As String, damage As Dice)
      MyBase.New(ObjectType.Trap, name)
      Me.TrapType = trapType
      Me.Damage = damage
    End Sub

    Public Sub New(trapType As TrapType)
      MyBase.New(ObjectType.Trap, Weapon.Template(trapType).Name)
      Me.TrapType = trapType
      Damage = Weapon.Template(trapType).Damage
    End Sub

    Public ReadOnly Property TrapType As TrapType
    Public ReadOnly Property Damage As Dice

    Public Shared Function Template(type As TrapType) As Trap
      Select Case type
        Case TrapType.Hole : Return New Trap(type, "trap door", Nothing)
        Case TrapType.Bear : Return New Trap(type, "bear trap", New Dice(3, 4))
        Case TrapType.SleepingGas : Return New Trap(type, "sleeping-gas trap", Nothing)
        Case TrapType.Arrow : Return New Trap(type, "arrow trap", New Dice(2, 3))
        Case TrapType.PoisonDart : Return New Trap(type, "pioson dart trap", New Dice(1, 4))
        Case TrapType.Teleport : Return New Trap(type, "teleport trap", Nothing)
        Case Else
          Return Nothing
      End Select
    End Function

    Public Overrides Function ToString() As String
      Return $"{Name}"
    End Function

  End Class

  Public NotInheritable Class Weapon
    Inherits ObjectBase

    Private Sub New(name As String, damage As Dice, damageThrown As Dice)
      MyBase.New(ObjectType.Weapon, name)
      Me.Damage = damage
      Me.DamageThrown = damageThrown
    End Sub

    Public Sub New(type As WeaponType)
      MyBase.New(ObjectType.Weapon, Weapon.Template(type).Name)
      Damage = Weapon.Template(type).Damage
      DamageThrown = Weapon.Template(type).DamageThrown
    End Sub

    Public ReadOnly Property Damage As Dice
    Public Property Magic As Integer
    Public ReadOnly Property DamageThrown As Dice
    Public Property MagicThrown As Integer

    Public Shared Function Template(type As WeaponType) As Weapon
      Select Case type
        Case WeaponType.Mace : Return New Weapon("mace", New Dice(2, 4), New Dice(1, 3))
        Case WeaponType.LongSword : Return New Weapon("long sword", New Dice(3, 4), New Dice(1, 2))
        Case WeaponType.ShortBow : Return New Weapon("short bow", New Dice(1, 1), New Dice(1, 1))
        Case WeaponType.Arrow : Return New Weapon("arrow", New Dice(1, 1), New Dice(2, 3))
        Case WeaponType.Dagger : Return New Weapon("dagger", New Dice(1, 6), New Dice(1, 4))
        Case WeaponType.TwoHandedSword : Return New Weapon("two handed sword", New Dice(4, 4), New Dice(1, 2))
        Case WeaponType.Crossbow : Return New Weapon("crossbow", New Dice(1, 1), New Dice(1, 1))
        Case WeaponType.Bolt : Return New Weapon("bolt", New Dice(1, 2), New Dice(2, 5))
        Case WeaponType.Spear : Return New Weapon("spear", New Dice(2, 3), New Dice(1, 6))
        Case Else
          Return Nothing
      End Select
    End Function

    Public Overrides Function ToString() As String
      Return $"+{Magic},+{MagicThrown} {Name}"
    End Function

  End Class

  Public Enum TrapType
    Hole
    Bear
    SleepingGas
    Arrow
    PoisonDart
    Teleport
  End Enum

  Public Enum WeaponType
    Mace
    LongSword
    ShortBow
    Arrow
    Dagger
    TwoHandedSword
    Crossbow
    Bolt
    Spear
  End Enum

  Public NotInheritable Class Dice

    Public Sub New(count As Integer, sides As Integer)
      Me.Count = count
      Me.Sides = sides
    End Sub

    Public Property Count As Integer
    Public Property Sides As Integer

  End Class

End Namespace
