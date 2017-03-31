Public Class Entity

    ' This class will manage all of our player and NPC objects, since they are alike in stats/x/y/etc

    Private _name As String
    Private _symbol As Char
    Private _color As ConsoleColor
    Private _x As Integer
    Private _y As Integer

    Public Property Name() As String
        Get
            Return _name
        End Get

        Set(ByVal value As String)
            _name = value
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

    Public Sub New(ByVal name As String, ByVal symbol As Char, ByVal color As ConsoleColor, ByVal x As Integer, ByVal y As Integer)

        _name = name

        _symbol = symbol

        _color = color

        _x = x

        _y = y

    End Sub

End Class
