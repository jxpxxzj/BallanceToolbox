Namespace Base
    Public MustInherit Class ReaderMain(Of T)
        Public MustOverride Function ReadFromjson(url As String) As List(Of T)
        Public MustOverride Function ReadFromDB(url As String) As List(Of T)
        Public MustOverride Function ReadFromXml(url As String) As List(Of T)

        Public Event ItemChanged As ItemEventHandler

        Public Delegate Sub ItemEventHandler(ByVal sender As Object, ByVal e As SingleEventArgs)
        Protected Friend Overridable Sub OnItemChanged(ByVal e As SingleEventArgs)
            RaiseEvent ItemChanged(Me, e)
        End Sub

    End Class
End Namespace