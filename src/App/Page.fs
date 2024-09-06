module Page

open Sutil
open Types
open type Feliz.length

let viewPage (page : System.IObservable<Page>)  =
    Html.divc "app-page-container" [
        Bind.el( 
            page, 
            fun p ->
                match p with
                | Home -> Html.div "Home"
                | Login -> Html.div "Login"
                | Address -> Html.div "Address"
        )
    ]