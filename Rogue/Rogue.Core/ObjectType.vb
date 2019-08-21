Namespace Global.Rogue.Core

  Public Enum ObjectType
    None
    Gold
    Food
    Armor
    Weapon
    Scroll
    Potion
    Ring
    Rod
  End Enum

  Public MustInherit Class ObjectBase

    Public Sub New(type As ObjectType, name As String)
      Me.Type = type
      Me.Name = name
    End Sub

    Public ReadOnly Property Type As ObjectType
    Public ReadOnly Property Name As String

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

    Public Property Count
    Public Property Sides

  End Class

End Namespace
