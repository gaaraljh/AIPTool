Imports System.Runtime.CompilerServices
Imports System.Drawing

Namespace InventorAddIn

    Public Module BitmapExtensions

        <Extension()>
        Public Function ToBitmap(bmp As Bitmap) As Bitmap
            ' 如果对象已经是 Bitmap，直接返回
            Return bmp
        End Function

    End Module

End Namespace
