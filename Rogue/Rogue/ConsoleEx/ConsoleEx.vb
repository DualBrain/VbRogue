Option Explicit On
Option Strict On
Option Infer On

Imports System.Runtime.InteropServices
Imports Microsoft.Win32.SafeHandles

Namespace Global

  Public NotInheritable Class ConsoleEx

#Region "WIN32"

    Private Const HIDE_WINDOW As Integer = 0
    Private Const SHOW_WINDOW As Integer = 5

    Private Const LF_FACESIZE As Integer = 32

    Private Const SC_CLOSE As Integer = &HF060
    Private Const SC_MINIMIZE As Integer = &HF020
    Private Const SC_MAXIMIZE As Integer = &HF030
    Private Const SC_SIZE As Integer = &HF000

    Private Const MF_BYCOMMAND As Integer = &H0
    'Private Const MF_ENABLED As Integer = &H0
    'Private Const MF_DISABLED As Integer = &H2
    'Private Const MF_GRAYED As Integer = &H1
    Private Const MF_BYPOSITION As Integer = &H400

    Private Const STD_OUTPUT_HANDLE As Integer = -11 ' per WinBase.h
    Private Shared ReadOnly INVALID_HANDLE_VALUE As New IntPtr(-1) ' per WinBase.h

    <StructLayout(LayoutKind.Sequential)>
    Private Structure COORD
      Public X As Short
      Public Y As Short
      Public Sub New(ByVal X As Short, ByVal Y As Short)
        Me.X = X
        Me.Y = Y
      End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure CONSOLE_FONT_INFO_EX
      Friend cbSize As UInteger
      Friend nFont As UInteger
      Friend dwFontSize As COORD
      Friend FontFamily As Integer
      Friend FontWeight As Integer
      Friend FaceName() As Char
      Public Sub Initialize()
        ReDim Me.FaceName(LF_FACESIZE)
      End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure SMALL_RECT
      Friend Left As Short
      Friend Top As Short
      Friend Right As Short
      Friend Bottom As Short
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Friend Structure COLORREF

      Friend ColorDWORD As UInteger

      Friend Sub New(color As Drawing.Color)
        Me.ColorDWORD = CUInt(color.R) + ((CUInt(color.G)) << 8) + ((CUInt(color.B)) << 16)
      End Sub

      Friend Sub New(r As UInteger, g As UInteger, b As UInteger)
        Me.ColorDWORD = r + (g << 8) + (b << 16)
      End Sub

      Friend Function GetColor() As Drawing.Color
        Return Drawing.Color.FromArgb(CInt(&HFFUI And Me.ColorDWORD), CInt(&HFF00UI And Me.ColorDWORD) >> 8, CInt(&HFF0000UI And Me.ColorDWORD) >> 16)
      End Function

      Friend Sub SetColor(color As Drawing.Color)
        Me.ColorDWORD = CUInt(color.R) + ((CUInt(color.G)) << 8) + ((CUInt(color.B)) << 16)
      End Sub

    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure CONSOLE_SCREEN_BUFFER_INFO_EX
      Friend cbSize As Integer
      Friend dwSize As COORD
      Friend dwCursorPosition As COORD
      Friend wAttributes As UShort
      Friend srWindow As SMALL_RECT
      Friend dwMaximumWindowSize As COORD
      Friend wPopupAttributes As UShort
      Friend bFullscreenSupported As Boolean
      Friend black As COLORREF
      Friend darkBlue As COLORREF
      Friend darkGreen As COLORREF
      Friend darkCyan As COLORREF
      Friend darkRed As COLORREF
      Friend darkMagenta As COLORREF
      Friend darkYellow As COLORREF
      Friend gray As COLORREF
      Friend darkGray As COLORREF
      Friend blue As COLORREF
      Friend green As COLORREF
      Friend cyan As COLORREF
      Friend red As COLORREF
      Friend magenta As COLORREF
      Friend yellow As COLORREF
      Friend white As COLORREF
    End Structure

    Private Enum StdHandle As Integer
      STD_INPUT_HANDLE = -10
      STD_OUTPUT_HANDLE = -11
      STD_ERROR_HANDLE = -12
    End Enum

    Private Enum ConsoleMode As Integer
      ENABLE_ECHO_INPUT = &H4
      ENABLE_EXTENDED_FLAGS = &H80
      ENABLE_INSERT_MODE = &H20
      ENABLE_LINE_INPUT = &H2
      ENABLE_MOUSE_INPUT = &H10
      ENABLE_PROCESSED_INPUT = &H1
      ENABLE_QUICK_EDIT_MODE = &H40
      ENABLE_WINDOW_INPUT = &H8
      ENABLE_VIRTUAL_TERMINAL_INPUT = &H200
      'screen buffer handle
      ENABLE_PROCESSED_OUTPUT = &H1
      ENABLE_WRAP_AT_EOL_OUTPUT = &H2
      ENABLE_VIRTUAL_TERMINAL_PROCESSING = &H4
      DISABLE_NEWLINE_AUTO_RETURN = &H8
      ENABLE_LVB_GRID_WORLDWIDE = &H10
    End Enum

    Private Declare Function GetConsoleMode Lib "kernel32" (hConsoleHandle As IntPtr, <System.Runtime.InteropServices.Out()> ByRef lpMode As Integer) As Boolean
    Private Declare Function SetConsoleMode Lib "kernel32" (hConsoleHandle As IntPtr, dwMode As Integer) As Boolean

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Private Shared Function ReadConsoleOutputCharacter(hConsoleOutput As IntPtr,
                                                     <Out()> lpCharacter As Text.StringBuilder,
                                                     length As UInteger,
                                                     bufferCoord As COORD,
                                                     ByRef lpNumberOfCharactersRead As UInteger) As Boolean
    End Function

    <DllImport("kernel32.dll")>
    Private Shared Function ReadConsoleOutputAttribute(hConsoleOutput As IntPtr,
                                                     <Out()> ByVal lpAttribute() As UShort,
                                                     length As UInteger,
                                                     bufferCoord As COORD,
                                                     <System.Runtime.InteropServices.Out()> ByRef lpNumberOfAttrsRead As UInteger) As Boolean
    End Function

    'Private Declare Function ReadConsoleOutputCharacter Lib "kernel32" (hConsoleOutput As IntPtr,
    '                                                                    <Out()> lpCharacter As Text.StringBuilder,
    '                                                                    length As UInteger,
    '                                                                    bufferCoord As Coord,
    '                                                                    ByRef lpNumberOfCharactersRead As UInteger) As Boolean

    Private Declare Function DeleteMenu Lib "user32" (hMenu As IntPtr, nPosition As Integer, wFlags As Integer) As Integer
    Private Declare Function EnableMenuItem Lib "user32" (hMenu As IntPtr, itemId As Integer, uEnable As Integer) As Integer
    'Private Declare Function InsertMenu Lib "user32" (hMenu As IntPtr, nPosition As Integer, wFlags As Integer, wIDNewItem As Integer, lpNewItem As String) As Integer
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function InsertMenu(hMenu As IntPtr, uPosition As Integer, uFlags As Integer, uIDNewItem As Integer, lpNewItem As String) As Boolean
    End Function
    'Private Declare Function GetMenuString Lib "user32" (hMenu As IntPtr, itemId As Integer, <Out()> lpString As Text.StringBuilder, nMaxCount As Integer, uFlag As Integer) As Integer
    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function GetMenuString(hMenu As IntPtr, itemId As Integer, <Out()> lpString As Text.StringBuilder, nMaxCount As Integer, uFlag As Integer) As Integer
    End Function

    Private Declare Function GetSystemMenu Lib "user32" (hWnd As IntPtr, bRevert As Boolean) As IntPtr
    Private Declare Function GetConsoleWindow Lib "kernel32" () As IntPtr
    Private Declare Function CreateFile Lib "kernel32" (fileName As String,
                                                      <MarshalAs(UnmanagedType.U4)> fileAccess As UInteger,
                                                      <MarshalAs(UnmanagedType.U4)> fileShare As UInteger,
                                                      securityAttributes As IntPtr,
                                                      <MarshalAs(UnmanagedType.U4)> creationDisposition As IO.FileMode,
                                                      <MarshalAs(UnmanagedType.U4)> flags As Integer,
                                                      template As IntPtr) As SafeFileHandle
    Private Declare Function WriteConsoleOutputCharacter Lib "kernel32" (hConsoleOutput As SafeFileHandle,
                                                                       lpCharacter As String,
                                                                       nLength As Integer,
                                                                       dwWriteCoord As COORD,
                                                                       ByRef lpumberOfCharsWritten As Integer) As Boolean
    Private Declare Function FreeConsole Lib "kernel32" () As Boolean
    Private Declare Function ShowWindow Lib "user32" (hWnd As IntPtr, nCmdShow As Integer) As Boolean
    Private Declare Function GetStdHandle Lib "kernel32" (dwType As Integer) As IntPtr

    Private Declare Function GetConsoleScreenBufferInfoEx Lib "kernel32.dll" (hConsoleOutput As IntPtr, ByRef csbe As CONSOLE_SCREEN_BUFFER_INFO_EX) As Boolean
    Private Declare Function SetConsoleScreenBufferInfoEx Lib "kernel32.dll" (hConsoleOutput As IntPtr, ByRef csbe As CONSOLE_SCREEN_BUFFER_INFO_EX) As Boolean

#End Region

    Private Sub New()
    End Sub

    Public Shared Sub EnableQuickEditMode()

      Dim consoleHandle As IntPtr = GetStdHandle(CInt(StdHandle.STD_INPUT_HANDLE))

      Dim mode As Integer
      GetConsoleMode(consoleHandle, mode)
      mode = mode Or (ConsoleMode.ENABLE_QUICK_EDIT_MODE)
      mode = mode Or (ConsoleMode.ENABLE_EXTENDED_FLAGS)

      SetConsoleMode(consoleHandle, mode)

    End Sub

    Public Shared Sub DisableQuickEditMode()

      Dim consoleHandle As IntPtr = GetStdHandle(CInt(StdHandle.STD_INPUT_HANDLE))

      Dim mode As Integer
      GetConsoleMode(consoleHandle, mode)
      mode = mode And Not (ConsoleMode.ENABLE_QUICK_EDIT_MODE)
      mode = mode Or (ConsoleMode.ENABLE_EXTENDED_FLAGS)

      SetConsoleMode(consoleHandle, mode)

    End Sub

    Private Shared CloseCaption As Text.StringBuilder = Nothing
    Private Shared MaximizeCaption As Text.StringBuilder = Nothing
    Private Shared MinimizeCaption As Text.StringBuilder = Nothing
    Private Shared MoveCaption As Text.StringBuilder = Nothing

    Public Shared Sub DisableClose()
      Dim handle = GetConsoleWindow()
      If handle <> IntPtr.Zero Then
        Dim sysMenu = GetSystemMenu(handle, False)
        If sysMenu <> IntPtr.Zero Then
          CloseCaption = New Text.StringBuilder(&H20)
          Dim result = GetMenuString(sysMenu, SC_SIZE, CloseCaption, CloseCaption.Capacity, MF_BYPOSITION)
          result = DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND)
          'Dim result = EnableMenuItem(sysMenu, SC_CLOSE, MF_BYCOMMAND Or MF_DISABLED Or MF_GRAYED)
        End If
      End If
    End Sub

    Public Shared Sub EnableClose()
      Dim handle = GetConsoleWindow()
      If handle <> IntPtr.Zero Then
        Dim sysMenu = GetSystemMenu(handle, False)
        If sysMenu <> IntPtr.Zero Then
          Dim value = CloseCaption.ToString
          If String.IsNullOrEmpty(value) Then
            value = "Close"
          End If
          Dim result = InsertMenu(sysMenu, 0, MF_BYPOSITION, SC_CLOSE, value)
          'Dim result = EnableMenuItem(sysMenu, SC_CLOSE, MF_BYCOMMAND Or MF_ENABLED)
        End If
      End If
    End Sub

    Public Shared Sub DisableMaximize()
      Dim handle = GetConsoleWindow()
      If handle <> IntPtr.Zero Then
        Dim sysMenu = GetSystemMenu(handle, False)
        If sysMenu <> IntPtr.Zero Then
          MaximizeCaption = New Text.StringBuilder(&H20)
          Dim result = GetMenuString(sysMenu, SC_SIZE, MaximizeCaption, MaximizeCaption.Capacity, MF_BYPOSITION)
          result = DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND)
          'Dim result = EnableMenuItem(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND Or MF_DISABLED Or MF_GRAYED)
        End If
      End If
    End Sub

    Public Shared Sub EnableMaximize()
      Dim handle = GetConsoleWindow()
      If handle <> IntPtr.Zero Then
        Dim sysMenu = GetSystemMenu(handle, False)
        If sysMenu <> IntPtr.Zero Then
          Dim value = MaximizeCaption.ToString
          If String.IsNullOrEmpty(value) Then
            value = "Maximize"
          End If
          Dim result = InsertMenu(sysMenu, 0, MF_BYPOSITION, SC_MAXIMIZE, value)
          'Dim result = EnableMenuItem(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND Or MF_ENABLED)
        End If
      End If
    End Sub

    Public Shared Sub DisableMinimize()
      Dim handle = GetConsoleWindow()
      If handle <> IntPtr.Zero Then
        Dim sysMenu = GetSystemMenu(handle, False)
        If sysMenu <> IntPtr.Zero Then
          MinimizeCaption = New Text.StringBuilder(&H20)
          Dim result = GetMenuString(sysMenu, SC_SIZE, MinimizeCaption, MinimizeCaption.Capacity, MF_BYPOSITION)
          result = DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND)
          'Dim result = EnableMenuItem(sysMenu, SC_MINIMIZE, MF_BYCOMMAND Or MF_DISABLED Or MF_GRAYED)
        End If
      End If
    End Sub

    Public Shared Sub EnableMinimize()
      Dim handle = GetConsoleWindow()
      If handle <> IntPtr.Zero Then
        Dim sysMenu = GetSystemMenu(handle, False)
        If sysMenu <> IntPtr.Zero Then
          Dim value = MinimizeCaption.ToString
          If String.IsNullOrEmpty(value) Then
            value = "Minimize"
          End If
          Dim result = InsertMenu(sysMenu, 0, MF_BYPOSITION, SC_MINIMIZE, value)
          'Dim result = EnableMenuItem(sysMenu, SC_MINIMIZE, MF_BYCOMMAND Or MF_ENABLED)
        End If
      End If
    End Sub

    Public Shared Sub DisableResize()
      Dim handle = GetConsoleWindow()
      If handle <> IntPtr.Zero Then
        Dim sysMenu = GetSystemMenu(handle, False)
        If sysMenu <> IntPtr.Zero Then
          MoveCaption = New Text.StringBuilder(&H20)
          Dim result = GetMenuString(sysMenu, SC_SIZE, MoveCaption, MoveCaption.Capacity, MF_BYPOSITION)
          result = DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND)
          'Dim result = EnableMenuItem(sysMenu, SC_SIZE, MF_BYCOMMAND Or MF_DISABLED Or MF_GRAYED)
        End If
      End If
    End Sub

    Public Shared Sub EnableResize()
      Dim handle = GetConsoleWindow()
      If handle <> IntPtr.Zero Then
        Dim sysMenu = GetSystemMenu(handle, False)
        If sysMenu <> IntPtr.Zero Then
          REM result = EnableMenuItem(sysMenu, SC_SIZE, MF_BYCOMMAND Or MF_ENABLED)
          Dim value = MoveCaption.ToString
          If String.IsNullOrEmpty(value) Then
            value = "Size"
          End If
          Dim result = InsertMenu(sysMenu, 0, MF_BYPOSITION, SC_SIZE, value)
        End If
      End If
    End Sub

    Public Shared Sub Hide()
      Dim result = ShowWindow(GetConsoleWindow(), HIDE_WINDOW)
    End Sub

    Public Shared Sub Kill()
      Dim result = FreeConsole()
    End Sub

    Public Shared Function ReadChar(left%, top%) As Char?
      Dim consoleHandle = GetStdHandle(-11)
      If consoleHandle = IntPtr.Zero Then
        Return Nothing
      End If
      Dim position = New COORD With {.X = CShort(left%), .Y = CShort(top%)}
      Dim result = New Text.StringBuilder(1)
      Dim read As UInteger = 0
      If ReadConsoleOutputCharacter(consoleHandle, result, 1, position, read) Then
        If read > 0 AndAlso
         result.Length > 0 Then
          Return result(0)
        Else
          Return Nothing
        End If
      Else
        Return Nothing
      End If
    End Function

    Private Structure CharInfo
      Public [Char] As Char
      Public Attr As UShort
    End Structure

    Public Shared Sub Snapshot(restore As Boolean)

      Static Snap(,) As CharInfo

      Dim consoleHandle = GetStdHandle(-11)
      If consoleHandle = IntPtr.Zero Then
        Return
      End If

      If Not restore Then

        ReDim Snap(Console.WindowHeight - 1, Console.WindowWidth - 1)

        For r As Short = 0 To CShort(Console.WindowHeight - 1)

          Dim position = New COORD(0, r)

          Dim attributes(Console.WindowWidth - 1) As UShort
          Dim read As UInteger = 0
          Dim success = ReadConsoleOutputAttribute(consoleHandle, attributes, Convert.ToUInt32(Console.WindowWidth), position, read)

          If Not success OrElse
           read <> Console.WindowWidth Then
            Stop
          End If

          Dim characters = New Text.StringBuilder(Console.WindowWidth - 1)
          read = 0
          success = ReadConsoleOutputCharacter(consoleHandle, characters, Convert.ToUInt32(Console.WindowWidth), position, read)

          If Not success OrElse
           read <> Console.WindowWidth Then
            Stop
          End If

          For c = 0 To attributes.Length - 1
            If c < characters.Length Then
              Snap(r, c).Char = characters(c)
            Else
              Debug.WriteLine("Char buffer < expected...")
            End If
            Snap(r, c).Attr = attributes(c)
          Next

        Next

      Else

        If Snap IsNot Nothing Then

          For r = 0 To Console.WindowHeight - 1
            Console.SetCursorPosition(0, r)
            For c = 0 To Console.WindowWidth - 1
              If r = Console.WindowHeight - 1 AndAlso c = Console.WindowWidth - 1 Then
                ' skip
              Else
                Dim character = Snap(r, c).Char
                Dim attribute = Snap(r, c).Attr
                Dim f = CType(attribute And &HF, ConsoleColor)
                Dim b = CType((((attribute And &HF0) >> 4) And &HF), ConsoleColor)
                If Console.ForegroundColor <> f Then Console.ForegroundColor = f
                If Console.BackgroundColor <> b Then Console.BackgroundColor = b
                'Console.Write(QB.QBChr(Asc(character)))
                Console.Write(character)
              End If
            Next
          Next

        End If

      End If

    End Sub

    Public Shared Sub Resize(cols%, rows%)
      Try
        Console.SetWindowSize(cols%, rows%) ' Set the windows size...
      Catch ex As Exception
        Console.WriteLine("1 - " & ex.ToString)
        'Threading.Thread.Sleep(5000)
      End Try
      Try
        Console.SetBufferSize(cols%, rows%) ' Then set the buffer size to the now window size...
      Catch ex As Exception
        Console.WriteLine("2 - " & ex.ToString)
        'Threading.Thread.Sleep(5000)
      End Try
      Try
        Console.SetWindowSize(cols%, rows%) ' Then set the window size again so that the scroll bar area is removed.
      Catch ex As Exception
        Console.WriteLine("3 - " & ex.ToString)
        'Threading.Thread.Sleep(5000)
      End Try
    End Sub

    Public Shared Sub Show()
      Dim result = ShowWindow(GetConsoleWindow(), SHOW_WINDOW)
    End Sub

    Public Shared Function SetColor(color As ConsoleColor, r As UInteger, g As UInteger, b As UInteger) As Integer

      Dim csbe As New CONSOLE_SCREEN_BUFFER_INFO_EX()
      csbe.cbSize = CInt(Marshal.SizeOf(csbe)) ' 96 = 0x60

      Dim hConsoleOutput As IntPtr = GetStdHandle(STD_OUTPUT_HANDLE) ' 7

      If hConsoleOutput = INVALID_HANDLE_VALUE Then
        Return Marshal.GetLastWin32Error()
      End If

      Dim brc As Boolean = GetConsoleScreenBufferInfoEx(hConsoleOutput, csbe)

      If Not brc Then
        Return Marshal.GetLastWin32Error()
      End If

      Select Case color
        Case ConsoleColor.Black
          csbe.black = New COLORREF(r, g, b)
        Case ConsoleColor.DarkBlue
          csbe.darkBlue = New COLORREF(r, g, b)
        Case ConsoleColor.DarkGreen
          csbe.darkGreen = New COLORREF(r, g, b)
        Case ConsoleColor.DarkCyan
          csbe.darkCyan = New COLORREF(r, g, b)
        Case ConsoleColor.DarkRed
          csbe.darkRed = New COLORREF(r, g, b)
        Case ConsoleColor.DarkMagenta
          csbe.darkMagenta = New COLORREF(r, g, b)
        Case ConsoleColor.DarkYellow
          csbe.darkYellow = New COLORREF(r, g, b)
        Case ConsoleColor.Gray
          csbe.gray = New COLORREF(r, g, b)
        Case ConsoleColor.DarkGray
          csbe.darkGray = New COLORREF(r, g, b)
        Case ConsoleColor.Blue
          csbe.blue = New COLORREF(r, g, b)
        Case ConsoleColor.Green
          csbe.green = New COLORREF(r, g, b)
        Case ConsoleColor.Cyan
          csbe.cyan = New COLORREF(r, g, b)
        Case ConsoleColor.Red
          csbe.red = New COLORREF(r, g, b)
        Case ConsoleColor.Magenta
          csbe.magenta = New COLORREF(r, g, b)
        Case ConsoleColor.Yellow
          csbe.yellow = New COLORREF(r, g, b)
        Case ConsoleColor.White
          csbe.white = New COLORREF(r, g, b)
      End Select

      csbe.srWindow.Bottom += CShort(1)
      csbe.srWindow.Right += CShort(1)

      brc = SetConsoleScreenBufferInfoEx(hConsoleOutput, csbe)

      If Not brc Then
        Return Marshal.GetLastWin32Error()
      End If

      Return 0

    End Function

  End Class

End Namespace