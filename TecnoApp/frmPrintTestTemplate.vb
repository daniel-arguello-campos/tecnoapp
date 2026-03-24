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
        'Declaración de variables de tipo de fuente y párrafo
        'Fuente negrita para títulos
        Dim BoldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)

        'Fuente normal para texto
        Dim NormalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA)

        'Declaración de cadena donde se almacenarán texto del PDF
        Dim p As New Paragraph()
        'p.Add(New Text("Cliente: ").SetFont(BoldFont))
        'p.Add(New Text(cliente).SetFont(fontNormal))
        'p.Add(New Text(vbLf))
        Dim Tittle As New Paragraph()





        Dim cliente As String = "CINDEA de Cóbano"
        Dim impresora As String = "Epson L1250"
        Dim serie As String = "XBFQ025529"
        Dim fecha As String = Date.Now.ToShortDateString
        Dim hora As String = Date.Now.ToShortTimeString

        ' Define la ruta de la carpeta "PDF" dentro del directorio donde se ejecuta la aplicación
        Dim carpeta As String = Application.StartupPath & "\PDF\"

        ' Verifica si la carpeta NO existe
        If Not Directory.Exists(carpeta) Then
            ' Si no existe, la crea automáticamente
            Directory.CreateDirectory(carpeta)
        End If

        ' Genera un nombre único para el archivo PDF usando la fecha y hora actual
        Dim nombreArchivo As String = "Test_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"

        ' Combina la ruta de la carpeta con el nombre del archivo para obtener la ruta completa
        Dim ruta As String = carpeta & nombreArchivo

        ' Crea un escritor de PDF que guardará el archivo en la ruta especificada
        Dim writer As New PdfWriter(ruta)

        ' Inicializa el documento PDF usando el escritor
        Dim pdf As New PdfDocument(writer)

        ' Crea el documento con tamaño carta (LETTER)
        Dim document As New Document(pdf, PageSize.LETTER)

        ' Establece los márgenes del documento (arriba, derecha, abajo, izquierda)
        document.SetMargins(5, 30, 30, 30)

        ' Crea un convertidor para transformar la imagen en bytes
        Dim converter As New ImageConverter()

        ' Convierte la imagen "Banner" de los recursos del proyecto a un arreglo de bytes
        Dim imgBytes() As Byte = CType(converter.ConvertTo(My.Resources.Banner, GetType(Byte())), Byte())

        ' Crea un objeto de datos de imagen a partir del arreglo de bytes
        Dim imgData = ImageDataFactory.Create(imgBytes)

        ' Crea un objeto imagen para insertarlo en el PDF
        Dim imagen As New Image(imgData)

        ' Alinea la imagen a la izquierda dentro del documento
        imagen.SetHorizontalAlignment(HorizontalAlignment.LEFT)

        ' Agrega la imagen al documento PDF
        document.Add(imagen)


        ' ================================
        ' FORMATO DE FECHA EN ESPAÑOL
        ' ================================

        Dim cultura As New Globalization.CultureInfo("es-ES")

        Dim DateTimeTittle As DateTime = DateTime.Now

        Dim textoFechatest As String = DateTimeTittle.ToString("dddd, d 'de' MMMM 'de' yyyy - hh:mm tt", cultura)

        If Not String.IsNullOrEmpty(textoFechatest) Then
            textoFechatest = Char.ToUpper(textoFechatest(0)) & textoFechatest.Substring(1)
        End If


        ' ================================
        ' CREACIÓN DE TABLA
        ' ================================

        Dim tablaInfo As New Table(UnitValue.CreatePercentArray(New Single() {50, 50}))
        tablaInfo.SetWidth(UnitValue.CreatePercentValue(100))
        tablaInfo.SetHorizontalAlignment(HorizontalAlignment.CENTER)

        ' 🔲 Borde externo de la tabla
        tablaInfo.SetBorder(New SolidBorder(1))


        ' ================================
        ' FUNCIÓN PARA PÁRRAFOS LIMPIOS
        ' ================================

        Dim crearParrafo As Func(Of String, PdfFont, Single, Paragraph) =
    Function(texto2, fuente, size)
        Return New Paragraph(texto2).
            SetFont(fuente).
            SetFontSize(size).
            SetTextAlignment(TextAlignment.LEFT).
            SetMarginTop(0).
            SetMarginBottom(0)
    End Function

        Dim sizeNormal As Single = 12
        Dim sizeTitulo As Single = sizeNormal - 3


        ' ================================
        ' FILA 1
        ' ================================

        ' Columna 1 → Cliente
        tablaInfo.AddCell(
    New Cell().
    Add(crearParrafo("Cliente:", BoldFont, sizeTitulo)). ' título normal
    Add(crearParrafo(cliente, NormalFont, sizeNormal)). ' valor en negrita
    SetBorder(Border.NO_BORDER).
    SetPadding(2)
)

        ' 🔄 Columna 2 → (ANTES era Tipo de papel, AHORA es Fecha)
        tablaInfo.AddCell(
    New Cell().
    Add(crearParrafo("Prueba #:", NormalFont, sizeTitulo)).
    Add(crearParrafo("1", BoldFont, sizeNormal)).
    SetBorder(Border.NO_BORDER).
    SetPadding(2)
)


        ' ================================
        ' FILA 2
        ' ================================

        ' Columna 1 → Modelo
        tablaInfo.AddCell(
    New Cell().
    Add(crearParrafo("Modelo:", NormalFont, sizeTitulo)).
    Add(crearParrafo(impresora, BoldFont, sizeNormal)).
    SetBorder(Border.NO_BORDER).
    SetPadding(2)
)

        ' 🔄 Columna 2 → (ANTES era Fecha, AHORA es Tipo de papel)
        tablaInfo.AddCell(
    New Cell().
    Add(crearParrafo("Tipo de papel:", NormalFont, sizeTitulo)).
    Add(crearParrafo("Normal", BoldFont, sizeNormal)).
    SetBorder(Border.NO_BORDER).
    SetPadding(2)
)


        ' ================================
        ' FILA 3
        ' ================================

        ' Columna 1 → Serie
        tablaInfo.AddCell(
    New Cell().
    Add(crearParrafo("Serie:", NormalFont, sizeTitulo)).
    Add(crearParrafo(serie, BoldFont, sizeNormal)).
    SetBorder(Border.NO_BORDER).
    SetPadding(2)
)

        ' Columna 2 → Prueba #
        tablaInfo.AddCell(
    New Cell().
    Add(crearParrafo("Fecha de impresión:", NormalFont, sizeTitulo)).
    Add(crearParrafo(textoFechatest, BoldFont, sizeNormal)).
    SetBorder(Border.NO_BORDER).
    SetPadding(2)
)



        ' ================================
        ' AGREGAR TABLA AL DOCUMENTO
        ' ================================

        document.Add(tablaInfo)



        'Dim clientlabel As New Paragraph("Cliente: " + cliente)
        'clientlabel.SetFontSize(12).SetFontColor(ColorConstants.BLACK).SetTextAlignment(TextAlignment.LEFT).SetMarginLeft(15).SetMarginTop(0).SetMarginBottom(0)
        'document.Add(clientlabel)

        'Dim printernamelabel As New Paragraph("Modelo de impresora: " + impresora)
        'printernamelabel.SetFontSize(12).SetFontColor(ColorConstants.BLACK).SetTextAlignment(TextAlignment.LEFT).SetMarginLeft(15).SetMarginTop(0).SetMarginBottom(0)
        'document.Add(printernamelabel)

        'Dim printermodellabel As New Paragraph("Serie: " + serie)
        'printermodellabel.SetFontSize(12).SetFontColor(ColorConstants.BLACK).SetTextAlignment(TextAlignment.LEFT).SetMarginLeft(15).SetMarginTop(0).SetMarginBottom(0)
        'document.Add(printermodellabel)

        document.Add(New Paragraph(" "))

        Dim TestCMYK As New Paragraph("Prueba de inyección CMYK")
        'COMENTARIO
        'COMENTARIO
        Tittle = New Paragraph("")
        Tittle.Add(New Text("Prueba de inyección CMYK").SetFont(BoldFont))
        Tittle.SetTextAlignment(TextAlignment.CENTER)
        document.Add(Tittle)

        'Dim fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)
        'TestCMYK.SetFont(fontBold).SetTextAlignment(TextAlignment.CENTER)
        'document.Add(TestCMYK)

        'Dim tablaColores As New Table(UnitValue.CreatePercentArray(New Single() {25, 25, 25, 25}))
        'tablaColores.SetWidth(UnitValue.CreatePercentValue(100)).SetHorizontalAlignment(HorizontalAlignment.CENTER)

        'Dim tamaño As Integer = 25
        'tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.BLACK))
        'tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(New DeviceRgb(0, 255, 255)))
        'tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(New DeviceRgb(255, 0, 255)))
        'tablaColores.AddCell(New Cell().SetHeight(tamaño).SetBackgroundColor(ColorConstants.YELLOW))

        'tablaColores.AddCell(New Cell().Add(New Paragraph("Negro").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER))
        'tablaColores.AddCell(New Cell().Add(New Paragraph("Cian").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER))
        'tablaColores.AddCell(New Cell().Add(New Paragraph("Magenta").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER))
        'tablaColores.AddCell(New Cell().Add(New Paragraph("Amarillo").SetTextAlignment(TextAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(Border.NO_BORDER))

        'document.Add(tablaColores)
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
        document.Add(New Paragraph(""))
        document.Add(New Paragraph(" "))
        'Dim InyectorPattern As New Paragraph("Patrón de prueba de inyectores")
        'Dim InyectorPatternfontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)
        'InyectorPattern.SetFont(InyectorPatternfontBold).SetTextAlignment(TextAlignment.CENTER)
        'document.Add(InyectorPattern)


        ' ================================
        ' CREAR TABLA 2 COLUMNAS (35% / 65%)
        ' ================================
        'Dim tablaImagenes As New Table(UnitValue.CreatePercentArray(New Single() {35, 65}))
        'tablaImagenes.SetWidth(UnitValue.CreatePercentValue(100))


        ' ================================
        ' FUENTE PARA TÍTULOS
        ' ================================
        Dim fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)



        ' ================================
        ' CREAR TABLA 2 COLUMNAS (35% / 65%)
        ' ================================
        Dim tablaImagenes As New Table(UnitValue.CreatePercentArray(New Single() {40, 60}))
        tablaImagenes.SetWidth(UnitValue.CreatePercentValue(100))


        ' ================================
        ' FUENTE PARA TÍTULOS
        ' ================================
        'Dim fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)


        ' ================================
        ' FILA 1 → TÍTULOS
        ' ================================

        ' Columna 1 → Título
        tablaImagenes.AddCell(
    New Cell().
    Add(New Paragraph("Patrón de prueba de inyectores").
        SetFont(fontBold).
        SetTextAlignment(TextAlignment.CENTER).
        SetMarginTop(0).
        SetMarginBottom(0)
    ).
    SetBorder(Border.NO_BORDER)
)

        ' Columna 2 → Título
        tablaImagenes.AddCell(
    New Cell().
    Add(New Paragraph("Prueba de impresión de imagen").
        SetFont(fontBold).
        SetTextAlignment(TextAlignment.CENTER).
        SetMarginTop(0).
        SetMarginBottom(0)
    ).
    SetBorder(Border.NO_BORDER)
)


        ' ================================
        ' CREAR IMAGEN (SE REUTILIZA)
        ' ================================
        Dim converter2 As New ImageConverter()
        Dim imgBytes2() As Byte = CType(converter2.ConvertTo(My.Resources.InyectorTest, GetType(Byte())), Byte())
        Dim imgData2 = ImageDataFactory.Create(imgBytes2)

        Dim converter3 As New ImageConverter()
        Dim imgBytes3() As Byte = CType(converter3.ConvertTo(My.Resources.image_test, GetType(Byte())), Byte())
        Dim imgData3 = ImageDataFactory.Create(imgBytes3)

        ' ================================
        ' FILA 2 → IMÁGENES
        ' ================================

        ' Columna 1 → Imagen
        tablaImagenes.AddCell(
    New Cell().
    Add(New Image(imgData2).
        SetHorizontalAlignment(HorizontalAlignment.CENTER).
        SetAutoScale(True) ' se adapta a la celda
    ).
    SetBorder(Border.NO_BORDER)
)

        ' Columna 2 → Imagen
        tablaImagenes.AddCell(
    New Cell().
    Add(New Image(imgData3).
        SetHorizontalAlignment(HorizontalAlignment.CENTER).
        SetAutoScale(True)
    ).
    SetBorder(Border.NO_BORDER)
)


        ' ================================
        ' AGREGAR TABLA AL DOCUMENTO
        ' ================================
        document.Add(tablaImagenes)




        'Dim InyectorTestConverter As New ImageConverter()
        'Dim InyectorTestimgBytes() As Byte = CType(converter.ConvertTo(My.Resources.InyectorTest, GetType(Byte())), Byte())
        'Dim InyectorTestimgData = ImageDataFactory.Create(InyectorTestimgBytes)
        'Dim InyectorTestimagen As New Image(InyectorTestimgData)
        'InyectorTestimagen.SetHorizontalAlignment(HorizontalAlignment.CENTER).SetWidth(200)
        'document.Add(InyectorTestimagen)

        document.Add(New Paragraph(""))
        document.Add(New Paragraph(" "))


        'Titulo Prueba de impresión de texto
        Tittle = New Paragraph("")
        Tittle.Add(New Text("Prueba de impresión de texto").SetFont(BoldFont))
        Tittle.SetTextAlignment(TextAlignment.CENTER)
        document.Add(Tittle)

        Dim texto As String = "Para mantener una impresora de inyección de tinta en buen estado es recomendable usarla con cierta regularidad y evitar largos periodos sin imprimir. Cuando pasa mucho tiempo sin uso, la tinta puede secarse en las mangueras y los inyectores, causando obstrucciones y mala calidad de impresión. Como buena práctica, se aconseja imprimir al menos una vez cada 7 a 10 días, utilizando todos los colores, para mantener el flujo de tinta activo y evitar que se solidifique en los conductos y cabezales." & vbLf

        Dim pa As New Paragraph(texto)

        pa.SetBorder(Border.NO_BORDER)
        pa.SetMarginLeft(0)
        pa.SetMarginRight(0)

        document.Add(pa)

        document.Add(New Paragraph(" "))
        'COMENTARIO
        'Tittle = New Paragraph("")
        'Tittle.Add(New Text("Prueba de imagen").SetFont(BoldFont))
        'Tittle.SetTextAlignment(TextAlignment.CENTER)
        'document.Add(Tittle)

        'Dim ImageTestConverter As New ImageConverter()
        'Dim ImageTestConverterimgBytes() As Byte = CType(converter.ConvertTo(My.Resources.image_test, GetType(Byte())), Byte())
        'Dim ImageTestConverterimgData = ImageDataFactory.Create(ImageTestConverterimgBytes)
        'Dim ImageTestConverterimagen As New Image(ImageTestConverterimgData)
        'ImageTestConverterimagen.SetHorizontalAlignment(HorizontalAlignment.CENTER).SetWidth(450)
        'document.Add(ImageTestConverterimagen)
        'COMENTARIO
        document.Add(New Paragraph(" "))
        p = New Paragraph("")
        p.Add(New Text("Resultado: ").SetFont(BoldFont))
        p.Add(New Text("( ) CMYK   ( ) Inyección    ( ) Texto   ( ) Imagen").SetFont(NormalFont))
        p.SetTextAlignment(TextAlignment.LEFT)
        p.SetMarginLeft(15)
        p.SetMarginTop(0)
        p.SetMarginBottom(0)

        document.Add(p)

        Dim pLeyenda As New Paragraph()

        ' Cuadro verde
        pLeyenda.Add(New Text("   ").
    SetBackgroundColor(New DeviceRgb(0, 176, 80)))

        pLeyenda.Add(New Text(" Correcto.   ").SetFont(NormalFont))

        ' Cuadro rojo
        pLeyenda.Add(New Text("   ").
    SetBackgroundColor(New DeviceRgb(255, 0, 0)))

        pLeyenda.Add(New Text(" Incorrecto.").SetFont(NormalFont))

        pLeyenda.SetMarginTop(5)
        pLeyenda.SetMarginBottom(5)

        pLeyenda.Add(New Text("   ").
    SetBackgroundColor(New DeviceRgb(0, 176, 80)))

        pLeyenda.Add(New Text("   ").
    SetBackgroundColor(New DeviceRgb(0, 176, 80)))

        document.Add(pLeyenda)

        pLeyenda.Add(New Text("● ").SetFontColor(New DeviceRgb(0, 176, 80)))
        pLeyenda.Add(New Text("Correcto   "))

        pLeyenda.Add(New Text("● ").SetFontColor(New DeviceRgb(255, 0, 0)))
        pLeyenda.Add(New Text("Incorrecto"))

        document.Add(pLeyenda)

        document.Close()

        Process.Start(New ProcessStartInfo(ruta) With {.UseShellExecute = True})

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CrearTestImpresionCompleto()
    End Sub


End Class