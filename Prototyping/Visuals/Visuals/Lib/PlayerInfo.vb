Namespace Rogue.Lib
  ''' <summary>
  ''' Keep Track of all information about the current player. 
  ''' </summary>
  Public Class PlayerInfo

    Private m_HitPoints As Integer
    Private m_CurrentHitPoints As Integer
    Private m_CurrentMapLevel As Integer
    Private m_CurrentStrength As Integer
    Private m_Strength As Integer
    Private m_Gold As Integer
    Private m_Armor As Integer
    Private m_CharacterClass As String

#Region "Properties"

    Public Property Armor As Integer
      Get
        Return m_Armor
      End Get
      Set(value As Integer)
        m_HitPoints = value
      End Set
    End Property

    Public Property CharacterClass As String
      Get
        Return m_CharacterClass
      End Get
      Set(value As String)
        m_CharacterClass = value
      End Set
    End Property

    Public Property CurrentHitPoints As Integer
      Get
        Return m_CurrentHitPoints
      End Get
      Set(value As Integer)
        m_CurrentHitPoints = value
      End Set
    End Property

    Public Property CurrentMapLevel As Integer
      Get
        Return m_CurrentMapLevel
      End Get
      Set(value As Integer)
        m_CurrentMapLevel = value
      End Set
    End Property

    Public Property CurrentStrength As Integer
      Get
        Return m_CurrentStrength
      End Get
      Set(value As Integer)
        m_CurrentHitPoints = value
      End Set
    End Property

    Public Property Gold As Integer
      Get
        Return m_Gold
      End Get
      Set(value As Integer)
        m_HitPoints = value
      End Set
    End Property

    Public Property HitPoints As Integer
      Get
        Return m_HitPoints
      End Get
      Set(value As Integer)
        m_HitPoints = value
      End Set
    End Property

    Public Property Strength As Integer
      Get
        Return m_Strength
      End Get
      Set(value As Integer)
        m_HitPoints = value
      End Set
    End Property


#End Region

#Region "Public Methods"

    Public Sub New()
      Initialize()
    End Sub


#End Region


#Region "Private Methods"

    Private Sub Initialize()
      m_Armor = 0
      m_CharacterClass = ""
      m_CurrentHitPoints = 0
      m_CurrentMapLevel = 0
      m_CurrentStrength = 0
      m_Gold = 0
      m_HitPoints = 0
      m_Strength = 0

      'for prototype set the variables that will be displayed
      m_Armor = 5
      m_CharacterClass = "Apprentice"
      m_CurrentHitPoints = 21
      m_CurrentMapLevel = 1
      m_CurrentStrength = 16
      m_Gold = 50
      m_HitPoints = 21
      m_Strength = 16


    End Sub


#End Region

  End Class
End Namespace
