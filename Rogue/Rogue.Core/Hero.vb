﻿Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Rogue.Core

  Public NotInheritable Class Hero
    Inherits Entity

    Private m_hungerCount As Integer

    Public Sub New()

      'for prototype set the variables that will be displayed

      Name = "Whimp"

      AC = 5

      Level = 1

      MaxStr = 16
      Str = 16

      MaxHP = 12
      HP = 12

      Gold = 0

      Rations = 1

      MoveCount = 0

      HungerCount = 0

      Inventory.Add(New InventoryItem() With {.Object = New Armor(ArmorType.RingMail) With {.Magic = 1}, .Equiped = True})
      Inventory.Add(New InventoryItem() With {.Object = New Weapon(WeaponType.Mace) With {.Magic = 1, .MagicThrown = 1}, .Equiped = True})
      Inventory.Add(New InventoryItem() With {.Object = New Weapon(WeaponType.ShortBow) With {.Magic = 1}})
      Inventory.Add(New InventoryItem() With {.Object = New Weapon(WeaponType.Arrow), .Count = 30})

    End Sub

    Public Property Name As String = "Whimp"

    Public Property MaxStr As Integer = 0
    Public Property Str As Integer = 0
    Public Property Gold As Integer = 0
    Public Property Rations As Integer = 0
    Public Property Message As String = ""

    Public Property MoveCount As Integer

    Public Property HungerCount As Integer
      Get
        Return m_hungerCount
      End Get
      Set(value As Integer)
        m_hungercount = value
        If m_hungercount < 0 Then
          m_hungercount = 0
        End If
      End Set
    End Property

    Public ReadOnly Property HungerStage As HungerStage
      Get
        Select Case HungerCount
          Case <= 1000 : Return HungerStage.Healthy
          Case <= 2000 : Return HungerStage.Hungry
          Case <= 3000 : Return HungerStage.Weak
          Case <= 4000 : Return HungerStage.Faint
          Case <= 5000 : Return HungerStage.Hungry3
          Case Else
            Return Core.HungerStage.Dead
        End Select
      End Get
    End Property

    Public Property HealCount As Integer ' Tracks the number of turns that have transpired toward regneration (see HealTurns).
    Public Property HealTurns As Integer = 18 ' TODO: Need to change to a full property; returning amount based on hero level.
    Public Property HealAmount As Integer = 1 ' TODO: Need to change to a full property; returning amount based on hero level.

    Public Property Inventory As New List(Of InventoryItem)

  End Class

  Public Class InventoryItem

    Public Property [Object] As ObjectBase
    Public Property Count As Integer = 1
    Public Property Equiped As Boolean = False

    Public Overrides Function ToString() As String
      Return [Object].ToString
    End Function

  End Class

  Public Enum HungerStage
    Healthy
    Hungry
    Weak
    Faint ' You feel very weak. you faint from lack of food -> You can move again
    Hungry3
    Dead
  End Enum

End Namespace