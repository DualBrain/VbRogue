Public Class Animal
    Enum AnType

        Human
        Dog
        Cat
        Dragon
        DeadAnimal
        Player
    End Enum

    Private mTypeAnimal As AnType

    Public Property TypeAnimal() As AnType
        'This property stores the type of animal.
        Get
            Return mTypeAnimal
        End Get
        Set(ByVal value As AnType)
            mTypeAnimal = value
        End Set
    End Property

    Private mSymbol As String
    Public Property Symbol() As String
        'This function is SPECIFIC for whatever animal, returns symbol for the animal.
        Get
            Select Case TypeAnimal
                Case AnType.Cat
                    Return "c"
                Case AnType.Dog
                    Return "d"
                Case AnType.Dragon
                    Return "D"
                Case AnType.Human
                    Return "@"
                Case AnType.Player
                    Return "8"
                Case AnType.DeadAnimal
                    Return "%"
            End Select
        End Get
        Set(ByVal value As String)
            mSymbol = value
        End Set
    End Property

    Public Property IsPet As Boolean

    Shared Function GetSymbol(Animal1 As Animal.AnType) As String
        'This function is SHARED.  That means it works the same regardless of which animal you use it with.
        'It is a generic function that just gives some info about Animal.vb, not a specific cat/dog whatever.
        Select Case Animal1 'They just want the symbol for the animal type.
            Case AnType.Cat
                Return "c"
            Case AnType.Dog
                Return "d"
            Case AnType.Dragon
                Return "D"
            Case AnType.Human
                Return "@" 'Note, see here, we need to make horizontal and vertical walls different.
            Case AnType.Player
                Return "8"
            Case AnType.DeadAnimal
                Return "%"
            Case Else
                Return "?"
        End Select
    End Function

    Shared Function GetAnimalType(sChar As String) As Animal.AnType
        'Another SHARED genric function that will be the same for all objects based off of Animal.vb
        Select Case sChar
            Case "c"
                Return AnType.Cat
            Case "d"
                Return AnType.Dog
            Case "D"
                Return AnType.Dragon
            Case "@" 'Note, see here, we need to make horizontal and vertical walls different.
                Return AnType.Human
            Case "%"
                Return AnType.DeadAnimal
            Case "8"
                Return AnType.Player

            Case Else
                Return AnType.Human
        End Select
    End Function

    Shared Function GetMaxHitPoints(an As AnType) As Integer
        'Another SHARED genric function that will be the same for all objects based off of Animal.vb
        Select Case an
            Case AnType.Cat
                Return 12
            Case AnType.Dog
                Return 15
            Case AnType.Dragon
                Return 35
            Case AnType.Human
                Return 18
            Case AnType.Player
                Return 20
            Case Else
                Return 12
        End Select
    End Function

    Private mHitPoints As Integer
    Public Property HitPoints() As Integer
        Get
            Return mHitPoints
        End Get
        Set(ByVal value As Integer)
            mHitPoints = value
        End Set
    End Property

    Private mHitStrength As Integer
    Public Property HitStrength() As Integer
        Get
            Dim iTotalHitStrengh As Integer 'Add up all factors in hitting power.
            iTotalHitStrengh = GetBaseHitPower(Me.TypeAnimal)
            'Would factor in weapons, strength and experience points here.
            Return iTotalHitStrengh
        End Get
        Set(ByVal value As Integer)
            mHitStrength = value
        End Set
    End Property

    Shared Function GetBaseHitPower(an As AnType) As Integer
        Select Case an
            Case AnType.Cat
                Return 1
            Case AnType.Dog
                Return 2
            Case AnType.Dragon
                Return 5
            Case AnType.Human
                Return 3
            Case AnType.Player
                Return 3
        End Select
    End Function

    Private mIndex As Integer
    Public Property Index() As Integer
        Get
            Return mIndex
        End Get
        Set(ByVal value As Integer)
            mIndex = value
        End Set
    End Property


    Public Function AttemptHit(ByRef Foe As Animal) As Integer
        'Attempt to hit the foe
        'Later on, will have to put some randomeness in here, for now, just hit every time.
        If Me Is Foe Then
            Map.WriteMessage(Me.TypeAnimal.ToString & " wants to hurt itself, but reconsiders.")
            Return 0
        End If
        If Foe IsNot Nothing Then
            Foe.ReceiveHit(Me)
            Select Case Me.TypeAnimal
                Case AnType.Player
                    Map.WriteMessage("You swing at the " & Foe.TypeAnimal.ToString)
                Case Else
                    If Me.AnimalsAround > 3 Then
                        Map.WriteMessage(Me.TypeAnimal.ToString & Me.Index & "(" & Me.HitPoints & ")" & " feels crowded and attacks " & Foe.TypeAnimal.ToString & Foe.Index & "(" & Foe.HitPoints & ")")
                    Else
                        Map.WriteMessage(Me.TypeAnimal.ToString & Me.Index & "(" & Me.HitPoints & ")" & " attacks " & Foe.TypeAnimal.ToString & Foe.Index & "(" & Foe.HitPoints & ")")
                    End If

            End Select
            If Foe.TypeAnimal = AnType.DeadAnimal Then 'You killed it
                Me.mHitStrength = Me.mHitStrength + 1 'Get a little stonger
                Me.AnimalsAround = Me.AnimalsAround - 1
            End If
        Else 'swinging at nothing
            Select Case Me.TypeAnimal
                Case AnType.Player
                    Map.WriteMessage("You take a swing at Nothing.  Nothing seems intimidated.")
                Case Else
                    'Map.WriteMessage(Me.TypeAnimal.ToString & " lashes out at nothing.")
            End Select

        End If

    End Function

    Public Function AttemptTame(ByRef WildAnimal As Animal) As Integer
        'Attempt to tame
        'Later on, will have to put some randomeness in here, for now, just hit every time.
        If WildAnimal IsNot Nothing Then
            If WildAnimal.IsPet Then
                Map.WriteMessage(WildAnimal.TypeAnimal.ToString & WildAnimal.Index & " is already a pet.")
                Return 0
            End If

            WildAnimal.ReceivePetting(Me)
            If WildAnimal.TypeAnimal = AnType.DeadAnimal Then 'You killed it
                Me.mHitStrength = Me.mHitStrength + 1 'Get a little stonger
            End If
        Else 'swinging at nothing
            Map.WriteMessage("You attempt to tame Nothing.  Nothing seems interested.")
        End If

    End Function

    Public Function ReceivePetting(ByRef WildAnimal As Animal) As Integer
        'When a human pets you, it turns you into a pet
        Me.IsPet = True
        Map.WriteMessage([Enum].GetName(GetType(Animal.AnType), WildAnimal.TypeAnimal) & "You have tamed a wild animal- " & Me.TypeAnimal.ToString & Me.Index)

    End Function

    Public Function ReceiveHit(ByRef Foe As Animal) As Integer
        'When an animal hits you, it decreases your health
        If Foe Is Me Then
            Return 0
        End If
        Me.HitPoints = Me.HitPoints - Foe.HitStrength
        'Determine if dead now
        If Me.HitPoints < 0 Then
            Map.WriteMessage([Enum].GetName(GetType(Animal.AnType), Me.TypeAnimal) & Me.Index & " dies. ")
            Me.Die()
            Exit Function
        End If
        If Me.TypeAnimal <> AnType.Player Then 'Retaliate
            If Me.IsPet Then
                'do nothing
            Else
                Me.AttemptHit(Foe)
                Map.WriteMessage(Me.TypeAnimal.ToString & Me.Index & "(" & Me.HitPoints & ")" & " strikes " & Foe.TypeAnimal.ToString & Foe.Index & "(" & Foe.HitPoints & ")")
            End If

        Else
            Map.WriteMessage([Enum].GetName(GetType(Animal.AnType), Foe.TypeAnimal) & " strikes! You are down to " & Me.HitPoints & " hitpoints!")
        End If

    End Function

    Public Function Heal() As Integer
        'Heal some...
        If Me.HitPoints >= Animal.GetMaxHitPoints(AnType.Player) Then
            Exit Function 'no healing above hitpoints
        End If
        Dim rnd As New Random
        Dim iCanHeal As Integer
        iCanHeal = rnd.Next(0, 3)
        'Give a 1 in 4 chance of healing
        If iCanHeal = 2 Then
            Me.HitPoints = Me.HitPoints + 1
            Map.WriteMessage("You heal somewhat to: " & Me.HitPoints & " hitpoints.")
        End If

    End Function

    Private mStandingOver As String
    Public Property StandingOver() As String
        'Symbol for whatever we are standing over so we can put it back
        Get
            Return mStandingOver
        End Get
        Set(ByVal value As String)
            mStandingOver = value
        End Set
    End Property

    Private mAnimalsAround As Integer
    Public Property AnimalsAround() As String
        Get
            Return mAnimalsAround
        End Get
        Set(ByVal value As String)
            mAnimalsAround = value
        End Set
    End Property



    Private mLeft As Integer
    Public Property Left() As Integer
        'This property stores the Left position number for the animal.
        Get
            Return mLeft
        End Get
        Set(ByVal value As Integer)
            mLeft = value
        End Set
    End Property

    Private mTop As Integer
    Public Property Top() As Integer
        'This property stores the Top position number for the animal.
        Get
            Return mTop
        End Get
        Set(ByVal value As Integer)
            mTop = value
        End Set
    End Property

    Enum Directions 'A list of all the directions a player or animal could possibly move.
        Left
        Right
        Up
        Down
        UpLeft
        UpRight
        DownLeft
        DownRight
    End Enum

    Public Function Move(ByVal Direction As Directions) As Boolean
        'Try to move the animal a certain way, and not allow if over wall etc.
        Dim NewLeft As Integer
        Dim NewTop As Integer

        Select Case Direction 'For whatever direction, check what is there before moving
            'Depending on which direction the user pressed, we just add or subtract their position numbers on the grid.
            Case Directions.Up
                NewLeft = Me.Left 'Stays the same moving up.
                NewTop = Me.Top - 1 'One up from current position.
            Case Directions.UpRight
                NewLeft = Me.Left + 1 'Moving one right from current position.
                NewTop = Me.Top - 1 'One up from current position.
            Case Directions.Right
                NewLeft = Me.Left + 1 'Moving one right from current.
                NewTop = Me.Top  'Not moving vertically, top stays same.
            Case Directions.DownRight
                NewLeft = Me.Left + 1 'Moving one right from current.
                NewTop = Me.Top + 1 'Moving one DOWN from current.
            Case Directions.Down
                NewLeft = Me.Left 'Not moving horizontally, left stays same.
                NewTop = Me.Top + 1 'Moving one DOWN from current.
            Case Directions.DownLeft
                NewLeft = Me.Left - 1 'Moving one left from current.
                NewTop = Me.Top + 1 'One DOWN from current position.
            Case Directions.Left
                NewLeft = Me.Left - 1 'Moving one left horizontally.
                NewTop = Me.Top  'Not moving vertically, top stays same.
            Case Directions.UpLeft
                NewLeft = Me.Left - 1 'Moving one left.
                NewTop = Me.Top - 1 'One up from current position.
        End Select

        If NewTop < 0 Or NewTop >= Map.mGrid.GetUpperBound(1) Or NewLeft < 0 Or NewLeft >= Map.mGrid.GetUpperBound(0) Then
            Debug.Print("Out of bounds: " & NewLeft & " top: " & NewTop)
            Return False
        End If

        Dim ObjectInPath As Thing.ThingType
        ObjectInPath = Thing.GetObjectType(Map.mGrid(NewLeft, NewTop)) 'Getting the thing in direction we are moving
        Dim CharInPath As String 'Just whatever string we are about to be over.  Floor, door or whatever.
        CharInPath = Map.mGrid(NewLeft, NewTop)

        'So we have the object type in our path of movement, nnow, can we walk over it?
        Select Case ObjectInPath
            Case Thing.ThingType.Wall
                'NO, cannot move, do not adjust player/animal position
                Debug.Print("There is a wall at: " & NewLeft & " top: " & NewTop)
                Return False
            Case Thing.ThingType.Door
                'Sure, move through the door unless locked or something.
                'First, erase where we were and put back symbol for floor.
                'Map.Write(Me.Left, Me.Top, Thing.GetSymbol(Thing.ThingType.Door)) 'Will always be floor? so we don't have to store what we were over
                'Me.Left = NewLeft
                'Me.Top = NewTop 'We have changed the position but need to move animal on the map to new spot.
            Case Thing.ThingType.Knife
                'Map.Write(Me.Left, Me.Top, Thing.GetSymbol(Thing.ThingType.Knife)) 'Will always be floor? so we don't have to store what we were over
                'Me.Left = NewLeft
                'Me.Top = NewTop 'We have changed the position but need to move animal on the map to new spot.
            Case Thing.ThingType.Floor
                'First, erase where we were and put back symbol for floor.
                'Map.Write(Me.Left, Me.Top, Thing.GetSymbol(Thing.ThingType.Floor)) 'Will always be floor? so we don't have to store what we were over
                'Me.Left = NewLeft
                'Me.Top = NewTop 'We have changed the position but need to move animal on the map to new spot.
            Case Thing.ThingType.Animal
                Me.AnimalsAround = Me.AnimalsAround + 1
                Return False
        End Select

        'First, erase ourselves and put back symbol we were on:
        Map.Write(Me.Left, Me.Top, Me.StandingOver)

        'Next, set our position to new coordinates:
        Me.Left = NewLeft
        Me.Top = NewTop

        'Next, save the character for whatever we will be standing over:
        Me.StandingOver = CharInPath
        'Finally, draw ourself on the map in memory.
        Map.Write(Me.Left, Me.Top, GetSymbol(Me.TypeAnimal)) 'OK need an animal.getsymbol
        If Me.TypeAnimal = AnType.DeadAnimal Then
            Map.Write(Me.Left, Me.Top, ".")
        End If

        'If near player do something.
        ReactToWhatIsNearby()
        Return True
    End Function

    Shared Function IsAnimal(sChar As String) As Boolean
        'Just check if somehting is an animal or not
        'Need to loop through all animal types
        For Each an As AnType In [Enum].GetValues(GetType(Animal.AnType))
            If GetSymbol(an) = sChar Then
                Return True
            End If
        Next
    End Function

    Public Sub ReactToWhatIsNearby()
        'Need to look around, see if anything near.
        'NOTE: THIS IS THE WRONG WAY TO DO THIS, INSTEAD NEED TO LOOP THROUGH MONSTERS AND CHECK COORDINATES
        '....cdc....
        '....c@d...
        '....cdd...
        Dim iLeft As Integer
        Dim iTop As Integer
        Debug.Print([Enum].GetName(GetType(Animal.AnType), Me.TypeAnimal) & " is at: " & Me.Left & " " & Me.Top)
        For iTop = -1 To 1
            For iLeft = -1 To 1
                Dim sChar As String
                If Me.Top + iTop > 0 And Me.Top + iTop < Map.mGrid.GetUpperBound(0) And Me.Left + iLeft > 0 And Me.Left + iLeft < Map.mGrid.GetUpperBound(1) Then
                    If Me.Top + iTop <> Me.Top Or Me.Left + iLeft <> Me.Left Then 'Do not check self
                        sChar = Map.mGrid(Me.Left + iLeft, Me.Top + iTop)
                        Debug.Print([Enum].GetName(GetType(Animal.AnType), Me.TypeAnimal) & " Checking coords: Left: " & Me.Left + iLeft & " top: " & Me.Top + iTop & " Found: " & sChar)
                        If IsAnimal(sChar) Then
                            If Me.IsPet Or Me.AnimalsAround > 3 Then
                                Me.AttemptHit(GetNearbyAnimal(Me))

                            Else
                                Dim sReaction As String
                                sReaction = GetReaction(Me.TypeAnimal, GetAnimalType(sChar))
                                If sReaction IsNot Nothing AndAlso sReaction <> "" Then
                                    Map.WriteMessage(sReaction)
                                End If 'End check if reaction string nothring
                            End If
  

                        End If 'End check if sChar is animal
                    End If 'end check for self spot.
                End If 'End check if within bounds.

            Next
        Next
        'Debug.Print(vbNewLine)
    End Sub

    Private Function GetReaction(I As AnType, Them As AnType) As String
        If I <> AnType.Player Then
            Return ""
        End If
        Select Case Them
            Case AnType.Cat 'i am a cat
                Return "The cat looks at you quizzically"
            Case AnType.Dog 'i am a dog
                Return "The dog gives you a bewildered look and sniffs the ground"
            Case AnType.Dragon 'i am a dragon
                Return "The mighty dragon seems uninterested"
            Case AnType.Human 'i am human
                Return "Who are you? What do you want? How do we get out of here... Leave me alone! "
            Case AnType.Player 'i am the player
                Return ""
        End Select
    End Function

    Private Sub Die()
        Me.TypeAnimal = AnType.DeadAnimal
        'Throw New NotImplementedException
    End Sub


    'Private mIsPet As Boolean
    'Public Property IsPet() As Boolean
    '    Get
    '        Return mIsPet
    '    End Get
    '    Set(ByVal value As Boolean)
    '        mIsPet = value
    '    End Set
    'End Property

    'Public Function Pet(ByRef anim As Animal) As Integer
    '    'Attempt to pet the animal
    '    'Later on, will have to put some randomeness in here, for now, just hit every time.
    '    If anim IsNot Nothing Then
    '        anim.ReceivePet(Me)
    '        If anim.TypeAnimal = AnType.DeadAnimal Then 'You killed it
    '            Me.mHitStrength = Me.mHitStrength + 1 'Get a little stonger
    '        End If
    '    Else 'swinging at nothing
    '        Map.WriteMessage("You take a swing at Nothing.  Nothing seems intimidated.")
    '    End If

    'End Function

    'Public Function ReceivePet(ByRef anim As Animal) As Integer
    '    'When human pets you, you become his pet
    '    Select Case anim.TypeAnimal
    '        Case AnType.Cat 'ok
    '        Case AnType.DeadAnimal 'no
    '            Return 0
    '        Case AnType.Dog 'ok
    '        Case AnType.Dragon 'no
    '            Return 0
    '        Case AnType.Human 'no
    '            Return 0
    '        Case AnType.Player 'self?

    '            Return 0
    '    End Select

    '    anim.IsPet = True
    '    Map.WriteMessage("The " & [Enum].GetName(GetType(Animal.AnType), anim.TypeAnimal) & " rubs it's nose against your leg, you have a new friend!")

    'End Function


End Class
