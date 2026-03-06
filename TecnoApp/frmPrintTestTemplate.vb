Imports System.IO
Imports System.Diagnostics
Imports System.ComponentModel
Imports iText.Kernel.Pdf
Imports iText.Kernel.Geom
Imports iText.Kernel.Colors
Imports iText.IO.Image
Imports iText.Layout
Imports iText.Layout.Element
Imports iText.Layout.Properties
Imports iText.Layout.Borders
Public Class frmPrintTestTemplate


    Public Sub CrearTestImpresionCompleto()

        '====================================
        ' INFORMACIÓN DEL CLIENTE Y EQUIPO
        '====================================

        Dim cliente As String = "Daniela Vindas"
        Dim impresora As String = "Epson L4260"
        Dim serie As String = "XBRX204735"
        Dim fecha As String = Date.Now.ToShortDateString
        Dim hora As String = Date.Now.ToShortTimeString

        '====================================
        ' CREAR / VERIFICAR CARPETA PDF
        '====================================

        'Carpeta donde se guardarán los documentos generados
        Dim carpeta As String = Application.StartupPath & "\PDF\"

        'Si la carpeta no existe se crea automáticamente
        If Not Directory.Exists(carpeta) Then
            Directory.CreateDirectory(carpeta)
        End If

        'Ruta completa del archivo PDF
        Dim nombreArchivo As String = "Test_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"
        Dim ruta As String = carpeta & nombreArchivo

        '====================================
        ' CREACIÓN DEL DOCUMENTO PDF
        '====================================

        'Inicializa el escritor del PDF

        Dim writer As New PdfWriter(ruta)

        'Crea el documento PDF
        Dim pdf As New PdfDocument(writer)

        'Define el documento en tamaño carta
        Dim document As New Document(pdf, PageSize.LETTER)

        'Define márgenes del documento
        document.SetMargins(40, 40, 40, 40)

        '====================================
        ' TÍTULO DEL DOCUMENTO
        '====================================

        Dim titulo As New Paragraph("TEST DE IMPRESIÓN")
        titulo.SetFontSize(18)
        titulo.SetFontColor(ColorConstants.BLACK)
        titulo.SetTextAlignment(TextAlignment.CENTER)

        document.Add(titulo)
        document.Add(New Paragraph(" "))

        '====================================
        ' TABLA CON DATOS DEL CLIENTE
        '====================================

        Dim tablaDatos As New Table(2)
        tablaDatos.SetWidth(UnitValue.CreatePercentValue(60))
        tablaDatos.SetHorizontalAlignment(HorizontalAlignment.CENTER)

        tablaDatos.AddCell("Cliente:")
        tablaDatos.AddCell(cliente)

        tablaDatos.AddCell("Impresora:")
        tablaDatos.AddCell(impresora)

        tablaDatos.AddCell("Serie:")
        tablaDatos.AddCell(serie)

        tablaDatos.AddCell("Fecha:")
        tablaDatos.AddCell(fecha)

        tablaDatos.AddCell("Hora:")
        tablaDatos.AddCell(hora)

        document.Add(tablaDatos)

        document.Add(New Paragraph(" "))

        '====================================
        ' BARRAS DE COLORES
        'Sirven para verificar mezcla de tinta
        '====================================

        document.Add(New Paragraph("Barras de colores"))

        Dim tablaColores As New Table(4)
        tablaColores.SetWidth(UnitValue.CreatePercentValue(80))
        tablaColores.SetHorizontalAlignment(HorizontalAlignment.CENTER)

        Dim tamaño As Integer = 50

        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.BLACK))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(New DeviceRgb(0, 255, 255)))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(New DeviceRgb(255, 0, 255)))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.YELLOW))

        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.RED))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.GREEN))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.BLUE))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.GRAY))

        document.Add(tablaColores)

        document.Add(New Paragraph(" "))

        '====================================
        ' ESCALA DE GRISES
        'Permite revisar degradado de tinta
        '====================================

        document.Add(New Paragraph("Escala de grises"))

        Dim tablaGrises As New Table(10)
        tablaGrises.SetWidth(UnitValue.CreatePercentValue(100))

        For i As Integer = 0 To 9

            Dim gris As Integer = 255 - (i * 25)

            tablaGrises.AddCell(New Cell().
                SetHeight(40).
                SetBackgroundColor(New DeviceRgb(gris, gris, gris)))

        Next

        document.Add(tablaGrises)

        document.Add(New Paragraph(" "))

        '====================================
        ' PRUEBA DE LÍNEAS FINAS
        'Sirve para detectar inyectores tapados
        '====================================

        document.Add(New Paragraph("Prueba de líneas finas (inyectores)"))

        'For i As Integer = 1 To 15
        '    document.Add(New Paragraph("||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||"))
        'Next

        'document.Add(New Paragraph(" "))

        '====================================
        ' CUADROS DE DIAGNÓSTICO
        'Sirven para observar manchas o fallos
        '====================================

        document.Add(New Paragraph("Cuadros de diagnóstico"))

        Dim tablaDiag As New Table(6)
        tablaDiag.SetWidth(UnitValue.CreatePercentValue(90))

        For i As Integer = 1 To 12

            tablaDiag.AddCell(New Cell().
                SetHeight(40).
                SetBorder(New SolidBorder(1)))

        Next

        document.Add(tablaDiag)

        document.Add(New Paragraph(" "))

        '====================================
        ' IMAGEN DE PRUEBA DE INYECTORES
        'La imagen está almacenada en Resources/images
        'pero en código se accede como My.Resources.InyectorTest
        '====================================

        document.Add(New Paragraph("Patrón de prueba de inyectores").SetTextAlignment(TextAlignment.CENTER))
        document.Add(New Paragraph(" "))

        'Convertir la imagen del Resource a bytes
        Dim converter As New ImageConverter()
        Dim imgBytes() As Byte = CType(converter.ConvertTo(My.Resources.InyectorTest, GetType(Byte())), Byte())

        'Crear objeto de imagen para el PDF
        Dim imgData = ImageDataFactory.Create(imgBytes)

        Dim imagen As New Image(imgData)

        'Centrar la imagen
        imagen.SetHorizontalAlignment(HorizontalAlignment.CENTER)

        'Ajustar tamaño para que encaje en la página
        imagen.SetWidth(200)

        'Agregar la imagen al documento
        document.Add(imagen)

        '====================================
        ' CERRAR DOCUMENTO
        '====================================

        document.Close()

        '====================================
        ' ABRIR EL PDF CON EL VISOR DEL SISTEMA
        '====================================

        Process.Start(New ProcessStartInfo(ruta) With {.UseShellExecute = True})

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CrearTestImpresionCompleto()
    End Sub
End Class