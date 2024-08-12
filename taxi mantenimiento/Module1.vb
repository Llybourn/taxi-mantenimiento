Imports System.IO

Module Module1

    Sub Main()
        Dim opcion As Char = ""
        Dim opcion2 As ConsoleKeyInfo
        Dim modelo As Integer
        Dim kilometraje As Integer
        Dim clasificacion As String
        Dim historialArchivo As String = "historial_taxis.txt"

        Do
            Try
                Console.Clear()
                Console.WriteLine("Menú de opciones:")
                Console.WriteLine("1. Clasificar Taxi")
                Console.WriteLine("2. Ver historial")
                Console.WriteLine("3. Borrar historial")
                Console.WriteLine("4. Salir")
                Console.WriteLine("Seleccione una opción:")
                opcion2 = Console.ReadKey()
                opcion = opcion2.KeyChar
                Console.Clear()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

            Select Case opcion
                Case "1"
                    Try
                        Console.Clear()
                        Console.WriteLine("Ingrese el modelo del taxi (año):")
                        modelo = Integer.Parse(Console.ReadLine())
                        Console.WriteLine("Ingrese el kilometraje del taxi:")
                        kilometraje = Integer.Parse(Console.ReadLine())
                        Console.Clear()

                        ' Clasificar el taxi
                        clasificacion = ClasificarTaxi(modelo, kilometraje)
                        Console.WriteLine("Clasificación del taxi: " & clasificacion)

                        ' Guardar en el historial
                        Using historial As StreamWriter = File.AppendText(historialArchivo)
                            historial.WriteLine("Modelo: " & modelo & ", Kilometraje: " & kilometraje & " Km, Clasificación: " & clasificacion)
                        End Using

                        Console.WriteLine("Resultado guardado en el historial.")
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                    Console.ReadKey()

                Case "2"
                    Console.Clear()
                    Try
                        If File.Exists(historialArchivo) Then
                            Using historial As StreamReader = File.OpenText(historialArchivo)
                                Console.WriteLine("Historial de Taxis:")
                                Do Until historial.EndOfStream
                                    Console.WriteLine(historial.ReadLine())
                                Loop
                            End Using
                        Else
                            Console.WriteLine("El archivo de historial no existe.")
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                    Console.WriteLine("")
                    Console.WriteLine("Presione cualquier tecla para continuar.")
                    Console.ReadKey()

                Case "3"
                    If File.Exists(historialArchivo) Then
                        File.Delete(historialArchivo)
                        Console.WriteLine("Historial borrado.")
                    Else
                        Console.WriteLine("El archivo de historial no existe.")
                    End If
                    Console.ReadKey()

                Case "4"
                    Console.Clear()
                    Console.WriteLine("Hasta pronto.")
                    Exit Do

                Case Else
                    Console.WriteLine("Opción no válida.")
                    Console.ReadKey()
            End Select
        Loop
    End Sub

    ' Función para clasificar el taxi
    Function ClasificarTaxi(ByVal modelo As Integer, ByVal kilometraje As Integer) As String
        If modelo < 2007 And kilometraje > 20000 Then
            Return "Debe renovarse"
        ElseIf modelo >= 2007 And modelo <= 2013 And kilometraje >= 20000 Then
            Return "Debe recibir mantenimiento"
        ElseIf modelo > 2013 And kilometraje < 10000 Then
            Return "Óptimas condiciones"
        Else
            Return "Mecánico"
        End If
    End Function
End Module
