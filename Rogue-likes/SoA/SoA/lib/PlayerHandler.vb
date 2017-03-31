Public Class PlayerHandler

    ' Tato trida se zabyva entitou hrace
    
    Private _name As String
    Private _sex As String
    Private _race As String
    Private _classtype As Byte
    Private _symbol As Char
    Private _color As ConsoleColor
    Private _x As Integer
    Private _y As Integer
    Private _hp As Integer
    Private _hpmax As Integer
    Private _mp As Integer
    Private _mpmax As Integer
    Private _armor As Integer
    Private _mindmg As Integer
    Private _maxdmg As Integer
    Private _str As Integer
    Private _dex As Integer
    Private _int As Integer
    Private _wis As Integer
    Private _gold As Integer
    
    Public Property Name() As String
        Get
            Return _name
        End Get

        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    
    Public Property Race() As String
        Get
            Return _race
        End Get

        Set(ByVal value As String)
            _race = value
        End Set
    End Property
    
    Public Property Sex() As String
        Get
            Return _sex
        End Get

        Set(ByVal value As String)
            _sex = value
        End Set
    End Property
    
    Public Property ClassType() As Byte
    		Get
    			Return _classtype
    		End Get
    		
    		Set(ByVal value As Byte)
    			_classtype = value
    		End Set
    End Property

    Public Property Symbol() As Char
        Get
            Return _symbol
        End Get

        Set(ByVal value As Char)
            _symbol = value
        End Set

    End Property

    Public Property Color() As ConsoleColor
        Get
            Return _color
        End Get

        Set(ByVal value As ConsoleColor)
            _color = value
        End Set
    End Property

    Public Property X() As Integer
        Get
            Return _x
        End Get

        Set(ByVal value As Integer)
            _x = value
        End Set
    End Property

    Public Property Y() As Integer
        Get
            Return _y
        End Get

        Set(ByVal value As Integer)
            _y = value
        End Set

    End Property
    
    Public Property HP() As Integer
        Get
            Return _hp
        End Get

        Set(ByVal value As Integer)
            _hp = value
        End Set
    End Property
    
    Public Property HPMax() As Integer
        Get
            Return _hpmax
        End Get

        Set(ByVal value As Integer)
            _hpmax = value
        End Set
    End Property
    
    Public Property MP() As Integer
        Get
            Return _mp
        End Get

        Set(ByVal value As Integer)
            _mp = value
        End Set
    End Property
    
    Public Property MPMax() As Integer
        Get
            Return _mpmax
        End Get

        Set(ByVal value As Integer)
            _mpmax = value
        End Set
    End Property

    Public Property Armor() As Integer
        Get
            Return _armor
        End Get

        Set(ByVal value As Integer)
            _armor = value
        End Set
    End Property
    
    Public Property MinDmg() As Integer
        Get
            Return _mindmg
        End Get

        Set(ByVal value As Integer)
            _mindmg = value
        End Set
    End Property
    
    Public Property MaxDmg() As Integer
        Get
            Return _maxdmg
        End Get

        Set(ByVal value As Integer)
            _maxdmg = value
        End Set
    End Property
    
    Public Property Str() As Integer
        Get
            Return _str
        End Get

        Set(ByVal value As Integer)
            _str = value
        End Set
    End Property
    
    Public Property Dex() As Integer
        Get
            Return _dex
        End Get

        Set(ByVal value As Integer)
            _dex = value
        End Set
    End Property
    
    Public Property Int() As Integer
        Get
            Return _int
        End Get

        Set(ByVal value As Integer)
            _int = value
        End Set
    End Property
    
    Public Property Wis() As Integer
        Get
            Return _wis
        End Get

        Set(ByVal value As Integer)
            _wis = value
        End Set
    End Property
    
    Public Property Gold() As Integer
    	Get
    		Return _gold
    	End Get
    	
    	Set(ByVal value As Integer)
    		_gold = value
    	End Set
    End Property

    Public Sub New(ByVal Name As String, ByVal Race As String, ByVal Sex As String, ByVal ClassType As Byte, ByVal Symbol As Char, ByVal Color As ConsoleColor, ByVal X As Byte, ByVal Y As Byte, ByVal HP As Integer, ByVal HPMax As Integer, ByVal MP As Integer, ByVal MPMax As Integer, ByVal Armor As Integer, ByVal MinDmg As Integer, ByVal MaxDmg As Integer, ByVal Str As Integer, ByVal Dex As Integer,ByVal Int As Integer, ByVal Wis As Integer, ByVal Gold As Integer)

        _name = Name

        _race = Race

		_sex = Sex

		_classtype = ClassType

        _symbol = Symbol

        _color = Color

        _x = X

        _y = Y

        _hp = HP

        _hpmax = HPMax

        _mp = MP

        _mpmax = MPMax

		_armor = Armor

		_mindmg = MinDmg

		_maxdmg = MaxDmg

        _str = Str

        _dex = Dex

        _int = Int

        _wis = Wis

        _gold = Gold

    End Sub

End Class
