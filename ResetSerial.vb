Imports System
Imports System.IO.Ports
Imports System.Threading

Module ResetSerial
    Dim _serialPort As SerialPort

    Sub Main(ByVal sArgs() As String)
        Dim ms As Int16
        'Look at arguments 
        If sArgs.Count = 0 Then ListArgs()
        If InStr(sArgs(0), "?") > 0 Then ListArgs()
        'try to open the port
        _serialPort = New SerialPort(sArgs(0)) ' Create a new SerialPort object
        Try
            _serialPort.Open()
        Catch ex As Exception
            Console.WriteLine("Port " & sArgs(0) & " doesn't seem to be valid, something like COM6 is required")
            End
        End Try
        ms = 500
        If sArgs.Length = 2 Then
            If IsNumeric(sArgs(1)) Then
                ms = CInt(sArgs(1))
            Else
                Console.WriteLine("A DTR high period of " & sArgs(0) & " isn't valid, something like 500 is required")
            End If
        End If
        _serialPort.DtrEnable = True
        Thread.Sleep(ms)
        _serialPort.DtrEnable = False
        _serialPort.Close()
        Console.WriteLine("DTR on port " & sArgs(0) & " set high for 500ms")

    End Sub

    Sub ListArgs()
        Console.WriteLine("Arguments:" & vbLf & _
        "arg1 required, serial port eg COM6" & vbLf & _
        "arg2 optional, period for DTR to be set high in ms (default 500)")
        End
    End Sub
End Module
