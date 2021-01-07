Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Abandon()
        Session.Clear()
        Session.RemoveAll()
        Dim mycookies() As String = Request.Cookies.AllKeys
        System.Web.Security.FormsAuthentication.SignOut()
        For Each cookis In mycookies
            Response.Cookies(cookis).Expires = Now.AddDays(-1)
        Next
        Response.ClearContent()
        Response.Redirect("\htmlpages\Website_Bootstrap\Accueil\index.html")
    End Sub

End Class