Public Class Thing
    'A thing is an inanimate object, wall, floor, door, knife...
    Enum ThingType
        Wall 'NOTE, may need later to differentiate between vertical wall or horizontal wall.
        Floor
        Door
        Knife 'NOTE, there is going to be a big list of items.
        Animal
    End Enum

    Private mTypeOfThing As ThingType
    Public Property TypeOfThing() As ThingType
        Get
            Return mTypeOfThing
        End Get
        Set(ByVal value As ThingType)
            mTypeOfThing = value
        End Set
    End Property

    Shared Function GetObjectType(GridItem As String) As ThingType
        'Depending on what the symbol is, assign what thingtype it is.
        'When we call this from Animal.Move, the Map module has already returned whatever string was in the position.
        Select Case GridItem
            Case "-"
                Return ThingType.Wall
            Case "."
                Return ThingType.Floor
            Case "+"
                Return ThingType.Door
            Case "/" 'NOTE: there may not be enough special characters on the keyboard to represent each type of item!
                Return ThingType.Knife
            Case " "
                Return ThingType.Floor
            Case Nothing
                Return ThingType.Floor
            Case Else
                Return ThingType.Animal
        End Select
    End Function

    Shared Function GetSymbol(Thing1 As Thing.ThingType) As String
        Select Case Thing1
            Case ThingType.Door
                Return "+"
            Case ThingType.Floor
                Return "."
            Case ThingType.Knife
                Return "/"
            Case ThingType.Wall
                Return "-" 'Note, see here, we need to make horizontal and vertical walls different.
        End Select

    End Function

End Class
