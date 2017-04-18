'Imports System.Windows.Media
'Found at: http://stackoverflow.com/questions/1988833/converting-color-to-consolecolor/29192463
Imports System.ComponentModel
Imports System.Drawing

Class NearestConsoleColor
    Private Shared Function ClosestConsoleColor(r As Byte, g As Byte, b As Byte) As ConsoleColor
        Dim ret As ConsoleColor = 0
        Dim rr As Double = r, gg As Double = g, bb As Double = b, delta As Double = Double.MaxValue

        For Each cc As ConsoleColor In [Enum].GetValues(GetType(ConsoleColor))
            Dim n = [Enum].GetName(GetType(ConsoleColor), cc)
            Dim c = System.Drawing.Color.FromName(If(n = "DarkYellow", "Orange", n)) 'Initially it was saying Color is not a member of System.Drawing, added reference to System.Drawing to fix.

            ' bug fix
            Dim t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0)
            If t = 0.0 Then
                Return cc
            End If
            If t < delta Then
                delta = t
                ret = cc
            End If
        Next
        Return ret
    End Function

    Public Shared Sub Main()
        For Each pi As Reflection.PropertyInfo In GetType(Color).GetProperties() 'Changed from 'var' to Reflection.PropertyInfo
            Dim cVerter As New ColorConverter 'Had to add this to resolve "Reference to a non-shared member requires an object reference"
            Debug.Print (pi.Name)
            
            If pi.PropertyType.Name = "Color" Then 'Added this check because reflection was chucking out "R,G,B,A etc at the end"
                Dim c = DirectCast(cVerter.ConvertFromString(pi.Name), Color)
                Dim cc = ClosestConsoleColor(c.R, c.G, c.B)
                Console.ForegroundColor = cc
                Console.WriteLine("{0,-20} {1} {2}", pi.Name, c, [Enum].GetName(GetType(ConsoleColor), cc))  
            Else       
                Console.WriteLine("Wrong property type, " & pi.Name & " was type: " & pi.PropertyType.Name)
            end If
        Next
    End Sub

End Class
