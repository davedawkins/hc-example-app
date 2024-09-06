module Helpers

open Sutil

module HtmlInput =
    open Browser.Types

    /// Various ways to supply a value for an input field
    type InputValue<'T> =
        | Constant of 'T
        | Observable of System.IObservable<'T>
        | Store of IStore<'T>

    /// Allowed attributes for Checkbox, plus an escape hatch for attributes like Placeholder etc
    type CheckboxAttrs = 
        | OnChecked of (bool -> unit)
        | Checked of InputValue<bool>
        | Attrs of Core.SutilElement seq

    let private targetAs<'a when 'a :> HTMLInputElement >(e : Event)  = (e.target :?> 'a)

    let private onChecked f  =
        Ev.onChange (fun e -> f( targetAs<HTMLInputElement>(e).``checked`` ))

    let private checkedValue (v : InputValue<bool>) =
        match v with
        | Constant b -> 
            Attr.value b
        | Observable b ->
            Bind.attr("checked", b)
        | Store b ->
            Bind.attr("checked", b)

    /// Checkbox helper for input [ type="checkbox" ]
    let Checkbox (attrs : CheckboxAttrs seq) =
        Html.input [
            Attr.typeCheckbox

            yield! (attrs 
                        |> Seq.choose (function OnChecked f ->  Some f | _ -> None)
                        |> Seq.map onChecked)

            yield! (attrs 
                        |> Seq.choose (function Checked v ->  Some v | _ -> None)
                        |> Seq.map checkedValue)

            yield! (attrs 
                        |> Seq.choose (function Attrs v ->  Some v | _ -> None)
                        |> Seq.collect id)

        ]