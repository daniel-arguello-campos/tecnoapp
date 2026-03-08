Imports System.ComponentModel   ' Importa librerías necesarias para el programa
Imports System.Diagnostics      ' Permite ejecutar procesos externos (abrir el PDF)
Imports System.IO               ' Manejo de archivos y carpetas
Imports iText.IO.Font.Constants ' Constantes de fuentes (ej. Helvetica)
Imports iText.IO.Image          ' Para trabajar con imágenes
Imports iText.Kernel.Colors     ' Para usar colores
Imports iText.Kernel.Font       ' Para crear y usar fuentes
Imports iText.Kernel.Geom       ' Para manejar tamaños de página
Imports iText.Kernel.Pdf        ' Para crear documentos PDF
Imports iText.Layout            ' Para diseñar el contenido del PDF
Imports iText.Layout.Borders    ' Para manejar bordes de tablas y celdas
Imports iText.Layout.Element    ' Para manejar elementos como párrafos, tablas, imágenes
Imports iText.Layout.Properties ' Para propiedades de diseño (alineación, márgenes, etc.)

Public Class frmPrintTestTemplate

    Public Sub CrearTestImpresionCompleto()

        Dim cliente As String = "Daniela Vindas"
        Dim impresora As String = "Epson L4260"
        Dim serie As String = "XBRX204735"
        Dim fecha As String = Date.Now.ToShortDateString
        Dim hora As String = Date.Now.ToShortTimeString

        Dim carpeta As String = Application.StartupPath & "\PDF\"
        If Not Directory.Exists(carpeta) Then
            Directory.CreateDirectory(carpeta)
        End If

        Dim nombreArchivo As String = "Test_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"
        Dim ruta As String = carpeta & nombreArchivo

        Dim writer As New PdfWriter(ruta)
        Dim pdf As New PdfDocument(writer)
        Dim document As New Document(pdf, PageSize.LETTER)
        document.SetMargins(5, 30, 30, 30)

        Dim converter As New ImageConverter()
        Dim imgBytes() As Byte = CType(converter.ConvertTo(My.Resources.Banner, GetType(Byte())), Byte())
        Dim imgData = ImageDataFactory.Create(imgBytes)
        Dim imagen As New Image(imgData)
        imagen.SetHorizontalAlignment(HorizontalAlignment.LEFT)
        document.Add(imagen)

        Dim fechaHora As DateTime = DateTime.Now
        Dim cultura As New Globalization.CultureInfo("es-ES")
        Dim textoFechaBase As String = fechaHora.ToString("dddd d 'de' MMMM 'de' yyyy, hh:mm tt", cultura)
        Dim textoFecha As String = Char.ToUpper(textoFechaBase(0)) & textoFechaBase.Substring(1)

        Dim fechaLabel As New Paragraph(textoFecha)
        fechaLabel.SetFontSize(12)
        fechaLabel.SetFontColor(ColorConstants.BLACK)
        fechaLabel.SetTextAlignment(TextAlignment.RIGHT)
        fechaLabel.SetMarginRight(15)
        document.Add(fechaLabel)

        Dim clientlabel As New Paragraph("Cliente: " + cliente)
        clientlabel.SetFontSize(12).SetFontColor(ColorConstants.BLACK).SetTextAlignment(TextAlignment.LEFT).SetMarginLeft(15).SetMarginTop(0).SetMarginBottom(0)
        document.Add(clientlabel)

        Dim printernamelabel As New Paragraph("Modelo de impresora: " + impresora)
        printernamelabel.SetFontSize(12).SetFontColor(ColorConstants.BLACK).SetTextAlignment(TextAlignment.LEFT).SetMarginLeft(15).SetMarginTop(0).SetMarginBottom(0)
        document.Add(printernamelabel)

        Dim printermodellabel As New Paragraph("Serie: " + serie)
        printermodellabel.SetFontSize(12).SetFontColor(ColorConstants.BLACK).SetTextAlignment(TextAlignment.LEFT).SetMarginLeft(15).SetMarginTop(0).SetMarginBottom(0)
        document.Add(printermodellabel)

        document.Add(New Paragraph(" "))

        Dim TestCMYK As New Paragraph("Prueba CMYK")
        Dim fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)
        TestCMYK.SetFont(fontBold).SetTextAlignment(TextAlignment.CENTER)
        document.Add(TestCMYK)

        Dim tablaColores As New Table(UnitValue.CreatePercentArray(New Single() {25, 25, 25, 25}))
        tablaColores.SetWidth(UnitValue.CreatePercentValue(100)).SetHorizontalAlignment(HorizontalAlignment.CENTER)

        Dim tamaño As Integer = 25
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.BLACK))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(New DeviceRgb(0, 255, 255)))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(New DeviceRgb(255, 0, 255)))
        tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.YELLOW))

        tablaColores.AddCell(New Cell().Add(New Paragraph("Negro").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER))
        tablaColores.AddCell(New Cell().Add(New Paragraph("Cian").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER))
        tablaColores.AddCell(New Cell().Add(New Paragraph("Magenta").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER))
        tablaColores.AddCell(New Cell().Add(New Paragraph("Amarillo").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER))

        document.Add(tablaColores)
        document.Add(New Paragraph(" "))

        Dim DegradadoNegro As New Table(UnitValue.CreatePercentArray(Enumerable.Repeat(100.0F / 9, 9).ToArray()))
        DegradadoNegro.SetWidth(UnitValue.CreatePercentValue(100))
        For i As Integer = 0 To 8
            If i = 0 Then
                DegradadoNegro.AddCell(New Cell().Add(New Paragraph("Negro").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).SetBackgroundColor(New DeviceRgb(255, 255, 255)).SetBorder(Border.NO_BORDER))
            Else
                Dim intensidad As Integer = (i - 1) * 32
                DegradadoNegro.AddCell(New Cell().SetHeight(25).SetBackgroundColor(New DeviceRgb(intensidad, intensidad, intensidad)))
            End If
        Next
        document.Add(DegradadoNegro)

        Dim DegradadoCian As New Table(UnitValue.CreatePercentArray(Enumerable.Repeat(100.0F / 9, 9).ToArray()))
        DegradadoCian.SetWidth(UnitValue.CreatePercentValue(100))
        For i As Integer = 0 To 8
            If i = 0 Then
                DegradadoCian.AddCell(New Cell().Add(New Paragraph("Cian").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).SetBackgroundColor(New DeviceRgb(255, 255, 255)).SetBorder(Border.NO_BORDER))
            Else
                Dim intensidad As Integer = i * 32
                DegradadoCian.AddCell(New Cell().SetHeight(25).SetBackgroundColor(New DeviceRgb(intensidad, 255, 255)))
            End If
        Next
        document.Add(DegradadoCian)

        Dim DegradadoMagenta As New Table(UnitValue.CreatePercentArray(Enumerable.Repeat(100.0F / 9, 9).ToArray()))
        DegradadoMagenta.SetWidth(UnitValue.CreatePercentValue(100))
        For i As Integer = 0 To 8
            If i = 0 Then
                DegradadoMagenta.AddCell(New Cell().Add(New Paragraph("Magenta").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).SetBackgroundColor(New DeviceRgb(255, 255, 255)).SetBorder(Border.NO_BORDER))
            Else
                Dim intensidad As Integer = i * 32
                DegradadoMagenta.AddCell(New Cell().SetHeight(25).SetBackgroundColor(New DeviceRgb(255, intensidad, 255)))
            End If
        Next
        document.Add(DegradadoMagenta)

        Dim DegradadoAmarillo As New Table(UnitValue.CreatePercentArray(Enumerable.Repeat(100.0F / 9, 9).ToArray()))
        DegradadoAmarillo.SetWidth(UnitValue.CreatePercentValue(100))
        For i As Integer = 0 To 8
            If i = 0 Then
                DegradadoAmarillo.AddCell(New Cell().Add(New Paragraph("Amarillo").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHeight(25).SetBackgroundColor(New DeviceRgb(255, 255, 255)).SetBorder(Border.NO_BORDER))
            Else
                Dim intensidad As Integer = i * 32
                DegradadoAmarillo.AddCell(New Cell().SetHeight(25).SetBackgroundColor(New DeviceRgb(255, 255, intensidad)))
            End If
        Next
        document.Add(DegradadoAmarillo)

        Dim InyectorPattern As New Paragraph("Patrón de prueba de inyectores")
        Dim InyectorPatternfontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)
        InyectorPattern.SetFont(InyectorPatternfontBold).SetTextAlignment(TextAlignment.CENTER)
        document.Add(InyectorPattern)

        Dim InyectorTestConverter As New ImageConverter()
        Dim InyectorTestimgBytes() As Byte = CType(converter.ConvertTo(My.Resources.InyectorTest, GetType(Byte())), Byte())
        Dim InyectorTestimgData = ImageDataFactory.Create(InyectorTestimgBytes)
        Dim InyectorTestimagen As New Image(InyectorTestimgData)
        InyectorTestimagen.SetHorizontalAlignment(HorizontalAlignment.CENTER).SetWidth(200)
        document.Add(InyectorTestimagen)

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

        document.Add(New Paragraph("Patrón de prueba de inyectores").SetTextAlignment(TextAlignment.CENTER))
        document.Add(New Paragraph(" "))

        document.Close()

        Process.Start(New ProcessStartInfo(ruta) With {.UseShellExecute = True})

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CrearTestImpresionCompleto()
    End Sub
End Class