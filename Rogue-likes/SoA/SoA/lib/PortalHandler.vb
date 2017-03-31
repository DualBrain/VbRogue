Public Class PortalHandler

    'Tato trida se zabyva prenosem mezi mapami

	Dim cCon As New cCon
	
	Sub MovePortal()
		
		If main.PortalInf(0) = 0 Then
            If Player.Y = 19 then
            	main.PortalInf(0) = 1
            	main.PortalInf(1) = Player.X - 24
            	main.PortalInf(2) = 2
            	InPortal = false
            	Return
            Else
            	main.PortalInf(0) = 5
            	main.PortalInf(1) = Player.X
            	main.PortalInf(2) = Player.Y
            	InPortal = false
            	Return
            End If
		End If
		
		If main.PortalInf(0) = 1 Then
            If Player.Y = 1 then
            	main.PortalInf(0) = 0
            	main.PortalInf(1) = Player.X + 24
            	main.PortalInf(2) = 18
            	InPortal = false
            	Return
            Else
            	main.PortalInf(0) = 2
            	main.PortalInf(1) = Player.X + 61
            	main.PortalInf(2) = Player.Y
            	InPortal = false
            	Return
            End If
		End If
		
		If main.PortalInf(0) = 2 Then
			If Player.X = 63 Then
				main.PortalInf(0) = 1
				main.PortalInf(1) = Player.X - 61
				main.PortalInf(2) = Player.Y
				InPortal = False
				Return
			ElseIf Player.Y = 1 Then
				main.PortalInf(0) = 4
				main.PortalInf(1) = Player.X
				main.PortalInf(2) = Player.Y + 18
				InPortal = False
				Return
			Else
				main.PortalInf(0) = 3
				main.PortalInf(1) = Player.X
				main.PortalInf(2) = Player.Y
				InPortal = False
				Return
			End If
		End If
		
		If main.PortalInf(0) = 3 Then
				main.PortalInf(0) = 2
				main.PortalInf(1) = Player.X
				main.PortalInf(2) = Player.Y
				InPortal = False
				Return
		End If
		
		If main.PortalInf(0) = 4 Then
				main.PortalInf(0) = 2
				main.PortalInf(1) = Player.X
				main.PortalInf(2) = Player.Y - 18
				InPortal = False
				Return
		End If
		
		If main.PortalInf(0) = 5 Then
				main.PortalInf(0) = 0
				main.PortalInf(1) = Player.X
				main.PortalInf(2) = Player.Y
				InPortal = False
				Return
		End If
		
	End Sub
	
End Class
