<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        ToolStrip1 = New ToolStrip()
        ToolStripDropDownButton1 = New ToolStripButton()
        ToolStripDropDownButton3 = New ToolStripDropDownButton()
        ToolStripTextBox1 = New ToolStripTextBox()
        ToolStripDropDownButton2 = New ToolStripDropDownButton()
        Opc1ToolStripMenuItem = New ToolStripMenuItem()
        IoxToolStripMenuItem = New ToolStripMenuItem()
        ToolStripDropDownButton4 = New ToolStripDropDownButton()
        TestDeImpresiónToolStripMenuItem = New ToolStripMenuItem()
        StatusStrip1 = New StatusStrip()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripDropDownButton1, ToolStripDropDownButton3, ToolStripTextBox1, ToolStripDropDownButton2, ToolStripDropDownButton4})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(700, 25)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripDropDownButton1
        ' 
        ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), Image)
        ToolStripDropDownButton1.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        ToolStripDropDownButton1.Size = New Size(56, 22)
        ToolStripDropDownButton1.Text = "inicio"
        ' 
        ' ToolStripDropDownButton3
        ' 
        ToolStripDropDownButton3.Alignment = ToolStripItemAlignment.Right
        ToolStripDropDownButton3.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton3.Image = CType(resources.GetObject("ToolStripDropDownButton3.Image"), Image)
        ToolStripDropDownButton3.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton3.Name = "ToolStripDropDownButton3"
        ToolStripDropDownButton3.Size = New Size(29, 22)
        ToolStripDropDownButton3.Text = "ToolStripDropDownButton3"
        ' 
        ' ToolStripTextBox1
        ' 
        ToolStripTextBox1.Alignment = ToolStripItemAlignment.Right
        ToolStripTextBox1.Name = "ToolStripTextBox1"
        ToolStripTextBox1.Size = New Size(23, 25)
        ' 
        ' ToolStripDropDownButton2
        ' 
        ToolStripDropDownButton2.DropDownItems.AddRange(New ToolStripItem() {Opc1ToolStripMenuItem, IoxToolStripMenuItem})
        ToolStripDropDownButton2.Image = My.Resources.Resources.InyectorTest
        ToolStripDropDownButton2.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton2.Name = "ToolStripDropDownButton2"
        ToolStripDropDownButton2.Size = New Size(78, 22)
        ToolStripDropDownButton2.Text = "Clientes"
        ' 
        ' Opc1ToolStripMenuItem
        ' 
        Opc1ToolStripMenuItem.Name = "Opc1ToolStripMenuItem"
        Opc1ToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.N
        Opc1ToolStripMenuItem.Size = New Size(197, 22)
        Opc1ToolStripMenuItem.Text = "Nuevo Cliente"
        ' 
        ' IoxToolStripMenuItem
        ' 
        IoxToolStripMenuItem.Name = "IoxToolStripMenuItem"
        IoxToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.L
        IoxToolStripMenuItem.Size = New Size(197, 22)
        IoxToolStripMenuItem.Text = "Lista de clientes"
        ' 
        ' ToolStripDropDownButton4
        ' 
        ToolStripDropDownButton4.DropDownItems.AddRange(New ToolStripItem() {TestDeImpresiónToolStripMenuItem})
        ToolStripDropDownButton4.Image = CType(resources.GetObject("ToolStripDropDownButton4.Image"), Image)
        ToolStripDropDownButton4.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton4.Name = "ToolStripDropDownButton4"
        ToolStripDropDownButton4.Size = New Size(107, 22)
        ToolStripDropDownButton4.Text = "Herramientas"
        ToolStripDropDownButton4.ToolTipText = "Herramientas"
        ' 
        ' TestDeImpresiónToolStripMenuItem
        ' 
        TestDeImpresiónToolStripMenuItem.Name = "TestDeImpresiónToolStripMenuItem"
        TestDeImpresiónToolStripMenuItem.Size = New Size(180, 22)
        TestDeImpresiónToolStripMenuItem.Text = "Test de impresión"
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Location = New Point(0, 316)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(700, 22)
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(700, 338)
        Controls.Add(StatusStrip1)
        Controls.Add(ToolStrip1)
        Margin = New Padding(3, 2, 3, 2)
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Contromático"
        WindowState = FormWindowState.Maximized
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripDropDownButton3 As ToolStripDropDownButton
    Friend WithEvents ToolStripDropDownButton1 As ToolStripButton
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripDropDownButton2 As ToolStripDropDownButton
    Friend WithEvents Opc1ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IoxToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripDropDownButton4 As ToolStripDropDownButton
    Friend WithEvents TestDeImpresiónToolStripMenuItem As ToolStripMenuItem

End Class
